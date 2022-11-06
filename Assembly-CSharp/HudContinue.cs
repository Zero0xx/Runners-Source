using System;
using Message;
using Text;
using UnityEngine;

// Token: 0x0200036B RID: 875
public class HudContinue : MonoBehaviour
{
	// Token: 0x060019F2 RID: 6642 RVA: 0x00097DA8 File Offset: 0x00095FA8
	private void Start()
	{
		if (ServerInterface.SettingState != null)
		{
			this.m_cmWatchingMaxCount = ServerInterface.SettingState.m_onePlayCmCount;
		}
	}

	// Token: 0x060019F3 RID: 6643 RVA: 0x00097DC4 File Offset: 0x00095FC4
	public void SetTimeUp(bool timeUp)
	{
		if (this.m_continueWindow != null)
		{
			this.m_continueWindow.SetTimeUpObj(timeUp);
		}
	}

	// Token: 0x060019F4 RID: 6644 RVA: 0x00097DE4 File Offset: 0x00095FE4
	public void PlayStart()
	{
		this.m_state = HudContinue.State.ASK_CONTINUE_START;
		this.m_beforeRsRingCount = HudContinue.GetCurrentRsRingCount();
	}

	// Token: 0x060019F5 RID: 6645 RVA: 0x00097DF8 File Offset: 0x00095FF8
	public void PushBackKey()
	{
		if (this.m_state == HudContinue.State.ASK_CONTINUE)
		{
			if (this.m_continueWindow != null)
			{
				this.m_continueWindow.OnPushBackKey();
			}
		}
		else if (this.m_state == HudContinue.State.BUY_RED_STAR_RING && this.m_buyRsRingWindow != null)
		{
			this.m_buyRsRingWindow.OnPushBackKey();
		}
	}

	// Token: 0x060019F6 RID: 6646 RVA: 0x00097E5C File Offset: 0x0009605C
	public void Setup(bool bossStage)
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Continue_window");
		if (gameObject != null)
		{
			this.m_continueWindow = gameObject.AddComponent<HudContinueWindow>();
			this.m_continueWindow.Setup(bossStage);
		}
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, "simple_shop_window");
		if (gameObject2 != null)
		{
			this.m_buyRsRingWindow = gameObject2.AddComponent<HudContinueBuyRsRing>();
			this.m_buyRsRingWindow.Setup();
		}
	}

	// Token: 0x060019F7 RID: 6647 RVA: 0x00097ED4 File Offset: 0x000960D4
	private void Update()
	{
		switch (this.m_state)
		{
		case HudContinue.State.ASK_CONTINUE_START:
			if (this.m_continueWindow != null)
			{
				this.m_continueWindow.SetVideoButton(this.m_watchingCount < this.m_cmWatchingMaxCount);
				this.m_continueWindow.PlayStart();
			}
			this.m_state = HudContinue.State.ASK_CONTINUE;
			break;
		case HudContinue.State.ASK_CONTINUE:
			if (this.m_continueWindow != null)
			{
				int currentRsRingCount = HudContinue.GetCurrentRsRingCount();
				if (this.m_continueWindow.IsYesButtonPressed)
				{
					int redStarRingCount = HudContinueUtility.GetRedStarRingCount();
					int continueCost = HudContinueUtility.GetContinueCost();
					if (redStarRingCount >= continueCost)
					{
						ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
						if (loggedInServerInterface != null)
						{
							loggedInServerInterface.RequestServerActRetry(base.gameObject);
							this.m_state = HudContinue.State.SERVER_CONNECT_WAIT;
						}
						else
						{
							this.ServerActRetry_Succeeded(null);
						}
					}
					else
					{
						this.m_state = HudContinue.State.BUY_RED_STAR_RING_START;
					}
				}
				else if (this.m_continueWindow.IsNoButtonPressed)
				{
					MsgContinueResult value = new MsgContinueResult(false);
					GameObjectUtil.SendMessageFindGameObject("GameModeStage", "OnSendToGameModeStage", value, SendMessageOptions.DontRequireReceiver);
					this.m_state = HudContinue.State.IDLE;
				}
				else if (this.m_continueWindow.IsVideoButtonPressed)
				{
					this.m_state = HudContinue.State.WAIT_VIDEO_RESPONSE;
				}
				else if (this.m_beforeRsRingCount < currentRsRingCount)
				{
					GeneralWindow.CInfo info = default(GeneralWindow.CInfo);
					info.caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ItemRoulette", "gw_item_caption").text;
					string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Item", "red_star_ring").text;
					text += " ";
					TextObject text2 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Shop", "gw_purchase_success_text");
					text2.ReplaceTag("{COUNT}", (currentRsRingCount - this.m_beforeRsRingCount).ToString());
					text += text2.text;
					info.message = text;
					info.anchor_path = "Camera/Anchor_5_MC";
					info.buttonType = GeneralWindow.ButtonType.Ok;
					info.name = "PurchaseCompleted";
					GeneralWindow.Create(info);
					this.m_beforeRsRingCount = currentRsRingCount;
					this.m_state = HudContinue.State.PURCHASE_COMPLETED;
				}
			}
			break;
		case HudContinue.State.BUY_RED_STAR_RING_START:
			if (ServerInterface.IsRSREnable())
			{
				if (this.m_buyRsRingWindow != null)
				{
					this.m_buyRsRingWindow.PlayStart();
				}
			}
			else
			{
				GeneralWindow.Create(new GeneralWindow.CInfo
				{
					name = "ErrorRSRing",
					buttonType = GeneralWindow.ButtonType.Ok,
					caption = TextUtility.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ChaoRoulette", "gw_cost_caption"),
					message = TextUtility.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ChaoRoulette", "gw_cost_caption_text_2"),
					isPlayErrorSe = true
				});
			}
			this.m_state = HudContinue.State.BUY_RED_STAR_RING;
			break;
		case HudContinue.State.BUY_RED_STAR_RING:
			if (ServerInterface.IsRSREnable())
			{
				if (this.m_buyRsRingWindow != null && this.m_buyRsRingWindow.IsEndPlay)
				{
					if (this.m_buyRsRingWindow.IsSuccess || this.m_buyRsRingWindow.IsCanceled)
					{
						this.m_state = HudContinue.State.ASK_CONTINUE_START;
					}
					else if (this.m_buyRsRingWindow.IsFailed)
					{
						this.m_state = HudContinue.State.BUY_RED_STAR_RING_START;
					}
				}
			}
			else if (GeneralWindow.IsCreated("ErrorRSRing") && GeneralWindow.IsButtonPressed)
			{
				this.m_state = HudContinue.State.ASK_CONTINUE_START;
				GeneralWindow.Close();
			}
			break;
		case HudContinue.State.PURCHASE_COMPLETED:
			if (GeneralWindow.IsCreated("PurchaseCompleted") && GeneralWindow.IsButtonPressed)
			{
				this.m_state = HudContinue.State.ASK_CONTINUE_START;
			}
			break;
		}
	}

	// Token: 0x060019F8 RID: 6648 RVA: 0x00098260 File Offset: 0x00096460
	private void SendContinuResult(bool continueFlag)
	{
		MsgContinueResult value = new MsgContinueResult(continueFlag);
		GameObjectUtil.SendMessageFindGameObject("GameModeStage", "OnSendToGameModeStage", value, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x060019F9 RID: 6649 RVA: 0x00098288 File Offset: 0x00096488
	private void ServerActRetry_Succeeded(MsgActRetrySucceed msg)
	{
		int continueCost = HudContinueUtility.GetContinueCost();
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			ServerInterface.PlayerState.m_numRedRings -= continueCost;
			SaveDataManager.Instance.ItemData.RedRingCount = (uint)ServerInterface.PlayerState.m_numRedRings;
		}
		this.SendContinuResult(true);
		this.m_state = HudContinue.State.IDLE;
	}

	// Token: 0x060019FA RID: 6650 RVA: 0x000982E8 File Offset: 0x000964E8
	private void ServerActRetryFree_Succeeded(MsgActRetryFreeSucceed msg)
	{
		this.SendContinuResult(true);
		this.m_state = HudContinue.State.IDLE;
	}

	// Token: 0x060019FB RID: 6651 RVA: 0x000982F8 File Offset: 0x000964F8
	private static int GetCurrentRsRingCount()
	{
		int result = 0;
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			result = ServerInterface.PlayerState.m_numRedRings;
		}
		else
		{
			SaveDataManager instance = SaveDataManager.Instance;
			if (instance != null)
			{
				result = (int)instance.ItemData.RedRingCount;
			}
		}
		return result;
	}

	// Token: 0x0400175A RID: 5978
	private HudContinueWindow m_continueWindow;

	// Token: 0x0400175B RID: 5979
	private HudContinueBuyRsRing m_buyRsRingWindow;

	// Token: 0x0400175C RID: 5980
	private int m_cmWatchingMaxCount;

	// Token: 0x0400175D RID: 5981
	private int m_watchingCount;

	// Token: 0x0400175E RID: 5982
	private int m_beforeRsRingCount;

	// Token: 0x0400175F RID: 5983
	private HudContinue.State m_state;

	// Token: 0x0200036C RID: 876
	private enum State
	{
		// Token: 0x04001761 RID: 5985
		IDLE,
		// Token: 0x04001762 RID: 5986
		SERVER_CONNECT_WAIT,
		// Token: 0x04001763 RID: 5987
		ASK_CONTINUE_START,
		// Token: 0x04001764 RID: 5988
		ASK_CONTINUE,
		// Token: 0x04001765 RID: 5989
		BUY_RED_STAR_RING_START,
		// Token: 0x04001766 RID: 5990
		BUY_RED_STAR_RING,
		// Token: 0x04001767 RID: 5991
		PURCHASE_COMPLETED,
		// Token: 0x04001768 RID: 5992
		WAIT_VIDEO_RESPONSE,
		// Token: 0x04001769 RID: 5993
		NUM
	}
}
