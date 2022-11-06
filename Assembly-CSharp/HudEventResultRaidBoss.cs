using System;
using System.Collections.Generic;
using AnimationOrTween;
using Message;
using Text;
using UI;
using UnityEngine;

// Token: 0x02000393 RID: 915
public class HudEventResultRaidBoss : HudEventResultParts
{
	// Token: 0x06001B06 RID: 6918 RVA: 0x0009F9DC File Offset: 0x0009DBDC
	public override bool IsBackkeyEnable()
	{
		return this.m_isBackkeyEnable;
	}

	// Token: 0x06001B07 RID: 6919 RVA: 0x0009F9E4 File Offset: 0x0009DBE4
	public override void Init(GameObject resultRootObject, long beforeTotalPoint, HudEventResult.AnimationEndCallback callback)
	{
		global::Debug.Log("HudEventResultRaidBoss:Init");
		this.m_resultRootObject = resultRootObject;
		this.m_callback = callback;
		this.m_isDamageDetailsWindowOpen = false;
		this.m_isHelpRequestWindowOpen = false;
		this.m_isHelpRequestReady = false;
		this.m_isBackkeyEnable = true;
		this.m_info = EventManager.Instance.RaidBossInfo;
		if (this.m_info != null)
		{
			this.m_animation = GameObjectUtil.FindChildGameObjectComponent<Animation>(base.gameObject, "EventResult_Anim");
			if (this.m_animation == null)
			{
				return;
			}
			this.m_helpRequestAnimation = GameObjectUtil.FindChildGameObjectComponent<Animation>(base.gameObject, "help_request");
			if (this.m_helpRequestAnimation != null)
			{
				this.m_helpRequestAnimation.gameObject.SetActive(false);
			}
			this.m_DamageDetailsButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(this.m_resultRootObject, "Btn_damage_details");
			if (this.m_DamageDetailsButton != null)
			{
				this.m_DamageDetailsButton.isEnabled = false;
				UIButtonMessage uibuttonMessage = this.m_DamageDetailsButton.gameObject.GetComponent<UIButtonMessage>();
				if (uibuttonMessage == null)
				{
					uibuttonMessage = this.m_DamageDetailsButton.gameObject.AddComponent<UIButtonMessage>();
				}
				if (uibuttonMessage != null)
				{
					uibuttonMessage.enabled = true;
					uibuttonMessage.trigger = UIButtonMessage.Trigger.OnClick;
					uibuttonMessage.target = base.gameObject;
					uibuttonMessage.functionName = "OnClickDamageDetailsButton";
				}
				if (GameResultUtility.GetBossDestroyFlag())
				{
					string text = TextUtility.GetText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "Menu", "ui_Lbl_word_reward_get");
					UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_DamageDetailsButton.gameObject, "Lbl_word_damage_details");
					if (uilabel != null)
					{
						uilabel.text = text;
						UILocalizeText component = uilabel.gameObject.GetComponent<UILocalizeText>();
						if (component != null)
						{
							component.enabled = false;
						}
					}
					UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_DamageDetailsButton.gameObject, "Lbl_word_damage_details_sh");
					if (uilabel2 != null)
					{
						uilabel2.text = text;
						UILocalizeText component2 = uilabel2.gameObject.GetComponent<UILocalizeText>();
						if (component2 != null)
						{
							component2.enabled = false;
						}
					}
				}
			}
			this.m_helpRequestToggle = GameObjectUtil.FindChildGameObjectComponent<UIToggle>(this.m_resultRootObject, "img_check_box_0");
			if (this.m_helpRequestToggle != null)
			{
				UIButtonMessage uibuttonMessage2 = this.m_helpRequestToggle.gameObject.AddComponent<UIButtonMessage>();
				if (uibuttonMessage2 != null)
				{
					uibuttonMessage2.enabled = true;
					uibuttonMessage2.trigger = UIButtonMessage.Trigger.OnClick;
					uibuttonMessage2.target = base.gameObject;
					uibuttonMessage2.functionName = "OnClickHelpRequestButton";
				}
			}
			GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_resultRootObject, "object_get");
			if (gameObject != null)
			{
				gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06001B08 RID: 6920 RVA: 0x0009FC7C File Offset: 0x0009DE7C
	private bool isHelpRequest()
	{
		bool flag = true;
		if (this.m_info != null && RaidBossInfo.currentRaidData != null && RaidBossInfo.currentRaidData.IsDiscoverer())
		{
			flag = GameResultUtility.GetBossDestroyFlag();
			flag |= RaidBossInfo.currentRaidData.crowded;
		}
		return flag;
	}

	// Token: 0x06001B09 RID: 6921 RVA: 0x0009FCC4 File Offset: 0x0009DEC4
	public override void PlayAnimation(HudEventResult.AnimType animType)
	{
		this.m_currentAnimType = animType;
		global::Debug.Log("HudEventResultRaidBoss:PlayAnimation >> " + this.m_currentAnimType);
		switch (animType)
		{
		case HudEventResult.AnimType.IN:
			this.m_isHelpRequestIn = false;
			if (!this.isHelpRequest())
			{
				if (this.m_helpRequestAnimation != null)
				{
					this.m_helpRequestAnimation.gameObject.SetActive(true);
					ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_helpRequestAnimation, "ui_EventResult_raidboss_help_request_intro_Anim", Direction.Forward, true);
					EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.AnimationFinishCallback), true);
					this.m_isHelpRequestIn = true;
				}
				else
				{
					global::Debug.Log("HudEventResultRaidBoss:PlayAnimation >> help request animation not found!!");
					this.m_callback(HudEventResult.AnimType.OUT_WAIT);
				}
			}
			else
			{
				this.m_callback(HudEventResult.AnimType.OUT_WAIT);
			}
			break;
		case HudEventResult.AnimType.IN_BONUS:
			this.AnimationFinishCallback();
			break;
		case HudEventResult.AnimType.WAIT_ADD_COLLECT_OBJECT:
			this.SetEnableDamageDetailsButton(true);
			break;
		case HudEventResult.AnimType.ADD_COLLECT_OBJECT:
			this.AnimationFinishCallback();
			break;
		case HudEventResult.AnimType.SHOW_QUOTA_LIST:
			this.AnimationFinishCallback();
			break;
		case HudEventResult.AnimType.OUT:
			if (this.m_isHelpRequestIn)
			{
				ActiveAnimation activeAnimation2 = ActiveAnimation.Play(this.m_helpRequestAnimation, "ui_EventResult_raidboss_help_request_outro_Anim", Direction.Forward, true);
				if (this.m_helpRequestToggle != null)
				{
					if (this.m_helpRequestToggle.value)
					{
						if (this.m_info != null && !this.m_isHelpRequestReady)
						{
							ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
							if (loggedInServerInterface != null)
							{
								List<string> list = new List<string>();
								SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
								if (socialInterface != null && socialInterface.IsLoggedIn)
								{
									List<SocialUserData> friendList = socialInterface.FriendList;
									foreach (SocialUserData socialUserData in friendList)
									{
										string gameId = socialUserData.CustomData.GameId;
										list.Add(gameId);
									}
								}
								loggedInServerInterface.RequestServerGetEventRaidBossDesiredList(EventManager.Instance.Id, RaidBossInfo.currentRaidData.id, list, base.gameObject);
								this.m_raidResultState = HudEventResultRaidBoss.RaidResultState.WAIT_HELP_REQUEST;
								this.m_isHelpRequestReady = true;
							}
						}
					}
					else
					{
						this.m_raidResultState = HudEventResultRaidBoss.RaidResultState.OUT;
						if (activeAnimation2 != null)
						{
							EventDelegate.Add(activeAnimation2.onFinished, new EventDelegate.Callback(this.AnimationFinishCallback), true);
						}
					}
				}
			}
			else
			{
				this.AnimationFinishCallback();
				this.m_raidResultState = HudEventResultRaidBoss.RaidResultState.OUT;
			}
			if (this.m_raidBossDamageWindow != null)
			{
				this.m_raidBossDamageWindow.OnClickClose();
			}
			this.SetEnableDamageDetailsButton(false);
			break;
		}
	}

	// Token: 0x06001B0A RID: 6922 RVA: 0x0009FF88 File Offset: 0x0009E188
	private void Update()
	{
		switch (this.m_raidResultState)
		{
		case HudEventResultRaidBoss.RaidResultState.WAIT_HELP:
			if (this.m_helpRequestWindow != null && this.m_isHelpRequestWindowOpen && this.m_helpRequestWindow.isFinished())
			{
				this.m_isHelpRequestWindowOpen = false;
				this.m_isHelpRequestIn = false;
				this.m_raidResultState = HudEventResultRaidBoss.RaidResultState.OUT;
				this.AnimationFinishCallback();
			}
			break;
		case HudEventResultRaidBoss.RaidResultState.WAIT_HELP_FAILURE:
			if (GeneralWindow.IsCreated("HelpRequestFailure") && GeneralWindow.IsButtonPressed)
			{
				this.m_isHelpRequestWindowOpen = false;
				this.m_isHelpRequestIn = false;
				this.m_raidResultState = HudEventResultRaidBoss.RaidResultState.OUT;
				this.AnimationFinishCallback();
				GeneralWindow.Close();
			}
			break;
		}
		if (!this.m_isBackkeyEnable && this.m_raidBossDamageWindow != null && !RaidBossDamageRewardWindow.IsEnabled())
		{
			this.m_isBackkeyEnable = true;
		}
	}

	// Token: 0x06001B0B RID: 6923 RVA: 0x000A0084 File Offset: 0x0009E284
	private void AnimationFinishCallback()
	{
		if (this.m_callback != null)
		{
			this.m_callback(this.m_currentAnimType);
		}
	}

	// Token: 0x06001B0C RID: 6924 RVA: 0x000A00A4 File Offset: 0x0009E2A4
	private void QuotaPlayEndCallback()
	{
		if (this.m_callback != null)
		{
			this.m_callback(this.m_currentAnimType);
		}
	}

	// Token: 0x06001B0D RID: 6925 RVA: 0x000A00C4 File Offset: 0x0009E2C4
	private void OnClickDamageDetailsButton()
	{
		global::Debug.Log("HudEventResultRaidBoss:OnClickDamageDetailsButton");
		SoundManager.SePlay("sys_menu_decide", "SE");
		if (this.m_info != null)
		{
			if (!this.m_isDamageDetailsWindowOpen)
			{
				this.m_isDamageDetailsWindowOpen = true;
				ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
				if (loggedInServerInterface != null)
				{
					int eventId = -1;
					if (EventManager.Instance != null)
					{
						eventId = EventManager.Instance.Id;
					}
					loggedInServerInterface.RequestServerGetEventRaidBossUserList(eventId, RaidBossInfo.currentRaidData.id, base.gameObject);
				}
				else
				{
					this.ServerGetEventRaidBossUserList_Succeeded(null);
				}
			}
			else
			{
				this.DamageDetailsWindowOpen();
			}
			this.m_isBackkeyEnable = false;
		}
	}

	// Token: 0x06001B0E RID: 6926 RVA: 0x000A016C File Offset: 0x0009E36C
	private void DamageDetailsWindowOpen()
	{
		this.m_raidBossDamageWindow = RaidBossDamageRewardWindow.Create(RaidBossInfo.currentRaidData, null);
		if (this.m_raidBossDamageWindow != null)
		{
			GameObject gameObject = this.m_raidBossDamageWindow.gameObject;
			if (gameObject != null)
			{
				Vector3 localPosition = gameObject.transform.localPosition;
				Vector3 localScale = gameObject.transform.localScale;
				gameObject.transform.parent = this.m_resultRootObject.transform;
				gameObject.transform.localPosition = localPosition;
				gameObject.transform.localScale = localScale;
			}
			this.m_raidBossDamageWindow.useResult = true;
		}
	}

	// Token: 0x06001B0F RID: 6927 RVA: 0x000A0208 File Offset: 0x0009E408
	private void OnClickHelpRequestButton()
	{
		global::Debug.Log("HudEventResultRaidBoss:OnClickHelpRequestButton");
		SoundManager.SePlay("sys_menu_decide", "SE");
	}

	// Token: 0x06001B10 RID: 6928 RVA: 0x000A0224 File Offset: 0x0009E424
	private void ServerGetEventRaidBossUserList_Succeeded(MsgGetEventRaidBossUserListSucceed msg)
	{
		this.DamageDetailsWindowOpen();
	}

	// Token: 0x06001B11 RID: 6929 RVA: 0x000A022C File Offset: 0x0009E42C
	private void ServerGetEventRaidBossDesiredList_Succeeded(MsgEventRaidBossDesiredListSucceed msg)
	{
		this.m_desiredList = msg.m_desiredList;
		if (this.m_desiredList != null)
		{
			if (this.m_desiredList.Count > 0)
			{
				this.m_raidResultState = HudEventResultRaidBoss.RaidResultState.WAIT_HELP;
				this.m_helpRequestWindow = RaidBosshelpRequestWindow.Create(this.m_desiredList);
				if (this.m_helpRequestWindow != null)
				{
					GameObject gameObject = this.m_helpRequestWindow.gameObject;
					if (gameObject != null)
					{
						Vector3 localPosition = gameObject.transform.localPosition;
						Vector3 localScale = gameObject.transform.localScale;
						gameObject.transform.parent = this.m_resultRootObject.transform;
						gameObject.transform.localPosition = localPosition;
						gameObject.transform.localScale = localScale;
					}
					this.m_isHelpRequestWindowOpen = true;
				}
			}
			else
			{
				this.m_raidResultState = HudEventResultRaidBoss.RaidResultState.WAIT_HELP_FAILURE;
				GeneralWindow.Create(new GeneralWindow.CInfo
				{
					name = "HelpRequestFailure",
					buttonType = GeneralWindow.ButtonType.Ok,
					caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "Result", "Lbl_caption_help_request_failure").text,
					message = TextManager.GetText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "Result", "Lbl_help_request_failure_text").text
				});
			}
		}
	}

	// Token: 0x06001B12 RID: 6930 RVA: 0x000A0358 File Offset: 0x0009E558
	public void SetEnableDamageDetailsButton(bool flag)
	{
		if (this.m_DamageDetailsButton != null)
		{
			this.m_DamageDetailsButton.isEnabled = flag;
		}
	}

	// Token: 0x04001873 RID: 6259
	private GameObject m_resultRootObject;

	// Token: 0x04001874 RID: 6260
	private HudEventResult.AnimationEndCallback m_callback;

	// Token: 0x04001875 RID: 6261
	private Animation m_animation;

	// Token: 0x04001876 RID: 6262
	private Animation m_helpRequestAnimation;

	// Token: 0x04001877 RID: 6263
	private HudEventResult.AnimType m_currentAnimType;

	// Token: 0x04001878 RID: 6264
	private RaidBossInfo m_info;

	// Token: 0x04001879 RID: 6265
	private GameResultScoreInterporate m_score;

	// Token: 0x0400187A RID: 6266
	private GameResultScoreInterporate m_redRingScore;

	// Token: 0x0400187B RID: 6267
	private UIImageButton m_DamageDetailsButton;

	// Token: 0x0400187C RID: 6268
	private UIToggle m_helpRequestToggle;

	// Token: 0x0400187D RID: 6269
	private RaidBossDamageRewardWindow m_raidBossDamageWindow;

	// Token: 0x0400187E RID: 6270
	private RaidBosshelpRequestWindow m_helpRequestWindow;

	// Token: 0x0400187F RID: 6271
	private bool m_isDamageDetailsWindowOpen;

	// Token: 0x04001880 RID: 6272
	private bool m_isHelpRequestWindowOpen;

	// Token: 0x04001881 RID: 6273
	private bool m_isHelpRequestIn;

	// Token: 0x04001882 RID: 6274
	private bool m_isHelpRequestReady;

	// Token: 0x04001883 RID: 6275
	private List<ServerEventRaidBossDesiredState> m_desiredList;

	// Token: 0x04001884 RID: 6276
	private HudEventResultRaidBoss.RaidResultState m_raidResultState;

	// Token: 0x04001885 RID: 6277
	private bool m_isBackkeyEnable = true;

	// Token: 0x02000394 RID: 916
	private enum RaidResultState
	{
		// Token: 0x04001887 RID: 6279
		INIT,
		// Token: 0x04001888 RID: 6280
		IN_BG,
		// Token: 0x04001889 RID: 6281
		WAIT_HELP_REQUEST,
		// Token: 0x0400188A RID: 6282
		WAIT_HELP,
		// Token: 0x0400188B RID: 6283
		WAIT_HELP_FAILURE,
		// Token: 0x0400188C RID: 6284
		DAMAGE_WINDOW,
		// Token: 0x0400188D RID: 6285
		OUT,
		// Token: 0x0400188E RID: 6286
		END
	}
}
