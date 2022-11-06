using System;
using System.Collections.Generic;
using AnimationOrTween;
using Text;
using UnityEngine;

// Token: 0x02000226 RID: 550
public class RaidBossDamageRewardWindow : WindowBase
{
	// Token: 0x17000230 RID: 560
	// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x00054EF8 File Offset: 0x000530F8
	// (set) Token: 0x06000EA5 RID: 3749 RVA: 0x00054F00 File Offset: 0x00053100
	public bool useResult
	{
		get
		{
			return this.m_useResult;
		}
		set
		{
			this.m_useResult = value;
		}
	}

	// Token: 0x06000EA6 RID: 3750 RVA: 0x00054F0C File Offset: 0x0005310C
	private void Start()
	{
	}

	// Token: 0x06000EA7 RID: 3751 RVA: 0x00054F10 File Offset: 0x00053110
	private void Update()
	{
	}

	// Token: 0x06000EA8 RID: 3752 RVA: 0x00054F14 File Offset: 0x00053114
	private void CallbackRaidBossDataUpdate(RaidBossData data)
	{
		this.m_bossData = data;
		if (this.m_bossData != null)
		{
			this.m_listPanel.enabled = true;
			this.m_storage = this.m_listPanel.GetComponentInChildren<UIRectItemStorage>();
			if (this.m_headerLabel != null)
			{
				if (this.m_bossData.end && this.m_bossData.clear)
				{
					this.m_headerLabel.text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "ui_Header_raid_reward").text;
				}
				else
				{
					this.m_headerLabel.text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "ui_Header_raid_damage").text;
				}
			}
			if (this.m_topRewardItem != null && this.m_bossData.end && this.m_bossData.clear)
			{
				string rewardText = this.m_bossData.GetRewardText();
				if (!string.IsNullOrEmpty(rewardText))
				{
					string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "raid_reward_text").text;
					this.m_topRewardItem.text = TextUtility.Replaces(text, new Dictionary<string, string>
					{
						{
							"{PARAM}",
							rewardText
						}
					});
				}
			}
			if (this.m_bossData.end && this.m_bossData.clear)
			{
				this.m_topInfo.SetActive(false);
				this.m_topReward.SetActive(true);
				UIRectItemStorage[] componentsInChildren = this.m_topReward.GetComponentsInChildren<UIRectItemStorage>();
				if (componentsInChildren != null)
				{
					foreach (UIRectItemStorage uirectItemStorage in componentsInChildren)
					{
						uirectItemStorage.Restart();
					}
				}
				this.m_bossObject = this.m_topReward.GetComponentInChildren<ui_event_raid_scroll>();
				this.m_playerObject = null;
			}
			else
			{
				this.m_topInfo.SetActive(true);
				this.m_topReward.SetActive(false);
				UIRectItemStorage[] componentsInChildren2 = this.m_topInfo.GetComponentsInChildren<UIRectItemStorage>();
				if (componentsInChildren2 != null)
				{
					foreach (UIRectItemStorage uirectItemStorage2 in componentsInChildren2)
					{
						uirectItemStorage2.Restart();
					}
				}
				this.m_bossObject = this.m_topInfo.GetComponentInChildren<ui_event_raid_scroll>();
				this.m_playerObject = this.m_topInfo.GetComponentInChildren<ui_damage_reward_scroll>();
				this.m_playerObject.SetClickCollision(!this.m_useResult);
			}
			int num = 0;
			long num2 = 0L;
			if (this.m_bossData.listData != null && this.m_bossData.listData.Count > 0)
			{
				num = this.m_bossData.listData.Count;
				foreach (RaidBossUser raidBossUser in this.m_bossData.listData)
				{
					if (num2 < raidBossUser.damage)
					{
						num2 = raidBossUser.damage;
					}
				}
			}
			this.m_storage.maxItemCount = (this.m_storage.maxRows = num);
			this.m_storage.Restart();
			if (this.m_storage != null)
			{
				ui_damage_reward_scroll[] componentsInChildren3 = this.m_storage.GetComponentsInChildren<ui_damage_reward_scroll>();
				if (componentsInChildren3 != null)
				{
					for (int k = 0; k < componentsInChildren3.Length; k++)
					{
						if (num > k)
						{
							if (this.m_bossData.listData[k].damage == num2)
							{
								this.m_bossData.listData[k].rankIndex = 0;
							}
							else
							{
								this.m_bossData.listData[k].rankIndex = 1;
							}
							componentsInChildren3[k].UpdateView(this.m_bossData.listData[k], this.m_bossData);
							componentsInChildren3[k].SetClickCollision(!this.m_useResult);
							if (this.m_bossData.myData != null && this.m_bossData.myData.id == this.m_bossData.listData[k].id)
							{
								componentsInChildren3[k].SetMyRanker(true);
							}
						}
						else
						{
							UnityEngine.Object.Destroy(componentsInChildren3[k].gameObject);
						}
					}
				}
			}
			if (this.m_bossObject != null)
			{
				this.m_bossObject.UpdateView(this.m_bossData, null, false);
			}
			if (this.m_playerObject != null)
			{
				if (this.m_bossData.myData != null)
				{
					if (this.m_bossData.myData.damage == num2)
					{
						this.m_bossData.myData.rankIndex = 0;
					}
					else
					{
						this.m_bossData.myData.rankIndex = 1;
					}
					this.m_playerObject.UpdateView(this.m_bossData.myData, this.m_bossData);
					this.m_playerObject.SetMyRanker(true);
				}
				else
				{
					UnityEngine.Object.Destroy(this.m_playerObject.gameObject);
					this.m_playerObject = null;
				}
			}
			if (this.m_listPanel != null)
			{
				this.m_listPanel.ResetPosition();
			}
		}
	}

	// Token: 0x06000EA9 RID: 3753 RVA: 0x00055454 File Offset: 0x00053654
	public void Setup(RaidBossData data, RaidBossWindow parent)
	{
		BackKeyManager.AddWindowCallBack(base.gameObject);
		this.m_parent = parent;
		this.mainPanel.alpha = 1f;
		this.m_close = false;
		this.m_btnAct = RaidBossDamageRewardWindow.BUTTON_ACT.NONE;
		this.m_useResult = false;
		if (this.m_storage != null)
		{
			this.m_storage.maxItemCount = (this.m_storage.maxRows = 0);
			this.m_storage.Restart();
		}
		if (data != null)
		{
			this.m_bossData = data;
			this.m_topInfo.SetActive(false);
			this.m_topReward.SetActive(false);
			if (this.m_headerLabel != null)
			{
				if (this.m_bossData.end && this.m_bossData.clear)
				{
					this.m_headerLabel.text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "ui_Header_raid_reward").text;
				}
				else
				{
					this.m_headerLabel.text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "ui_Header_raid_damage").text;
				}
			}
			if (this.m_topRewardItem != null && this.m_bossData.end && this.m_bossData.clear)
			{
				string rewardText = this.m_bossData.GetRewardText();
				if (!string.IsNullOrEmpty(rewardText))
				{
					string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "raid_reward_text").text;
					this.m_topRewardItem.text = text.Replace("{PARAM}", rewardText);
				}
				else
				{
					this.m_topRewardItem.text = string.Empty;
				}
			}
		}
		if (this.m_animation != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_cmn_window_Anim", Direction.Forward);
			EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.WindowAnimationFinishCallback), true);
			SoundManager.SePlay("sys_window_open", "SE");
		}
	}

	// Token: 0x06000EAA RID: 3754 RVA: 0x00055640 File Offset: 0x00053840
	public void OnClickClose()
	{
		this.m_btnAct = RaidBossDamageRewardWindow.BUTTON_ACT.CLOSE;
		this.m_close = true;
		SoundManager.SePlay("sys_window_close", "SE");
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_close");
		UIPlayAnimation component = gameObject.GetComponent<UIPlayAnimation>();
		if (component != null)
		{
			EventDelegate.Add(component.onFinished, new EventDelegate.Callback(this.WindowAnimationFinishCallback), true);
			component.Play(true);
		}
	}

	// Token: 0x06000EAB RID: 3755 RVA: 0x000556B0 File Offset: 0x000538B0
	private void WindowAnimationFinishCallback()
	{
		if (this.m_close)
		{
			RaidBossDamageRewardWindow.BUTTON_ACT btnAct = this.m_btnAct;
			if (btnAct != RaidBossDamageRewardWindow.BUTTON_ACT.INFO)
			{
				if (this.m_parent != null)
				{
					TimeSpan timeLimit = this.m_bossData.GetTimeLimit();
					if (this.m_bossData.end || timeLimit.Ticks <= 0L)
					{
						RaidBossInfo.currentRaidData = null;
						this.m_parent.RequestServerGetEventUserRaidBossList();
					}
				}
				base.gameObject.SetActive(false);
			}
			else
			{
				base.gameObject.SetActive(false);
			}
		}
		else
		{
			this.CallbackRaidBossDataUpdate(this.m_bossData);
		}
	}

	// Token: 0x06000EAC RID: 3756 RVA: 0x0005575C File Offset: 0x0005395C
	public override void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (ranking_window.isActive)
		{
			return;
		}
		if (msg != null)
		{
			msg.StaySequence();
		}
		this.OnClickClose();
	}

	// Token: 0x06000EAD RID: 3757 RVA: 0x0005577C File Offset: 0x0005397C
	public static bool IsEnabled()
	{
		bool result = false;
		if (RaidBossDamageRewardWindow.s_instance != null)
		{
			result = RaidBossDamageRewardWindow.s_instance.gameObject.activeSelf;
		}
		return result;
	}

	// Token: 0x06000EAE RID: 3758 RVA: 0x000557AC File Offset: 0x000539AC
	public static RaidBossDamageRewardWindow Create(RaidBossData data, RaidBossWindow parent = null)
	{
		if (RaidBossDamageRewardWindow.s_instance != null)
		{
			RaidBossDamageRewardWindow.s_instance.gameObject.SetActive(true);
			RaidBossDamageRewardWindow.s_instance.Setup(data, parent);
			return RaidBossDamageRewardWindow.s_instance;
		}
		return null;
	}

	// Token: 0x17000231 RID: 561
	// (get) Token: 0x06000EAF RID: 3759 RVA: 0x000557EC File Offset: 0x000539EC
	private static RaidBossDamageRewardWindow Instance
	{
		get
		{
			return RaidBossDamageRewardWindow.s_instance;
		}
	}

	// Token: 0x06000EB0 RID: 3760 RVA: 0x000557F4 File Offset: 0x000539F4
	private void Awake()
	{
		this.SetInstance();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000EB1 RID: 3761 RVA: 0x00055808 File Offset: 0x00053A08
	private void OnDestroy()
	{
		if (RaidBossDamageRewardWindow.s_instance == this)
		{
			RaidBossDamageRewardWindow.s_instance = null;
		}
	}

	// Token: 0x06000EB2 RID: 3762 RVA: 0x00055820 File Offset: 0x00053A20
	private void SetInstance()
	{
		if (RaidBossDamageRewardWindow.s_instance == null)
		{
			RaidBossDamageRewardWindow.s_instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000C8A RID: 3210
	private const float UPDATE_TIME = 0.25f;

	// Token: 0x04000C8B RID: 3211
	[SerializeField]
	private UIPanel mainPanel;

	// Token: 0x04000C8C RID: 3212
	[SerializeField]
	private Animation m_animation;

	// Token: 0x04000C8D RID: 3213
	[SerializeField]
	private UIDraggablePanel m_listPanel;

	// Token: 0x04000C8E RID: 3214
	[SerializeField]
	private GameObject m_topInfo;

	// Token: 0x04000C8F RID: 3215
	[SerializeField]
	private GameObject m_topReward;

	// Token: 0x04000C90 RID: 3216
	[SerializeField]
	private UILabel m_topRewardItem;

	// Token: 0x04000C91 RID: 3217
	[SerializeField]
	private UILabel m_headerLabel;

	// Token: 0x04000C92 RID: 3218
	private bool m_close;

	// Token: 0x04000C93 RID: 3219
	private RaidBossDamageRewardWindow.BUTTON_ACT m_btnAct = RaidBossDamageRewardWindow.BUTTON_ACT.NONE;

	// Token: 0x04000C94 RID: 3220
	private RaidBossData m_bossData;

	// Token: 0x04000C95 RID: 3221
	private UIRectItemStorage m_storage;

	// Token: 0x04000C96 RID: 3222
	private ui_event_raid_scroll m_bossObject;

	// Token: 0x04000C97 RID: 3223
	private ui_damage_reward_scroll m_playerObject;

	// Token: 0x04000C98 RID: 3224
	private RaidBossWindow m_parent;

	// Token: 0x04000C99 RID: 3225
	private bool m_useResult;

	// Token: 0x04000C9A RID: 3226
	private static RaidBossDamageRewardWindow s_instance;

	// Token: 0x02000227 RID: 551
	private enum BUTTON_ACT
	{
		// Token: 0x04000C9C RID: 3228
		CLOSE,
		// Token: 0x04000C9D RID: 3229
		INFO,
		// Token: 0x04000C9E RID: 3230
		NONE
	}
}
