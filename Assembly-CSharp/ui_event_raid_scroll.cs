using System;
using System.Collections.Generic;
using Message;
using Text;
using UnityEngine;

// Token: 0x02000574 RID: 1396
public class ui_event_raid_scroll : MonoBehaviour
{
	// Token: 0x06002ADB RID: 10971 RVA: 0x00108CD0 File Offset: 0x00106ED0
	private void Start()
	{
		this.m_timeCounter = 0.25f;
	}

	// Token: 0x06002ADC RID: 10972 RVA: 0x00108CE0 File Offset: 0x00106EE0
	private void Update()
	{
		this.m_timeCounter -= this.m_targetFrameTime;
		if (this.m_timeCounter <= 0f)
		{
			this.PeriodicUpdate();
			this.m_timeCounter = 0.25f;
		}
		if (this.m_reloadDelay > 0f && this.m_infoWindow && this.m_parent != null)
		{
			this.m_reloadDelay -= Time.deltaTime;
			if (this.m_reloadDelay <= 0f)
			{
				if (this.m_rightsideBtn0 != null)
				{
					UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(this.m_rightsideBtn0, "Btn_log");
					UIImageButton uiimageButton2 = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(this.m_rightsideBtn0, "Btn_play");
					if (uiimageButton != null)
					{
						uiimageButton.isEnabled = false;
					}
					if (uiimageButton2 != null)
					{
						uiimageButton2.isEnabled = false;
					}
				}
				if (this.m_parent.isLoading || !GeneralUtil.IsNetwork())
				{
					this.m_reloadDelay = 1f;
					global::Debug.Log("ui_event_raid_scroll reload retry!  IsNetwork:" + GeneralUtil.IsNetwork());
				}
				else
				{
					RaidBossInfo.currentRaidData = null;
					this.m_parent.RequestServerGetEventUserRaidBossList();
					this.m_reloadDelay = 0f;
				}
			}
		}
	}

	// Token: 0x06002ADD RID: 10973 RVA: 0x00108E28 File Offset: 0x00107028
	private void PeriodicUpdate()
	{
		if (this.m_bossData != null)
		{
			this.m_limit.text = this.m_bossData.GetTimeLimitString(true);
			if (this.m_reloadDelay <= 0f && this.m_infoWindow && this.m_parent != null && !this.m_bossData.end && this.m_bossData.GetTimeLimit().Ticks <= 0L)
			{
				this.m_reloadDelay = 1f;
			}
		}
	}

	// Token: 0x06002ADE RID: 10974 RVA: 0x00108EB8 File Offset: 0x001070B8
	public void UpdateView(RaidBossData bossData, RaidBossWindow parent, bool infoWindow = false)
	{
		this.m_parent = parent;
		this.m_bossData = bossData;
		this.m_timeCounter = 0.25f;
		this.m_targetFrameTime = 1f / (float)Application.targetFrameRate;
		this.m_infoWindow = infoWindow;
		this.m_reloadDelay = 0f;
		if (this.m_bossData != null)
		{
			if (this.m_bossName != null && this.m_bossNameSh != null)
			{
				UILabel bossName = this.m_bossName;
				string name = this.m_bossData.name;
				this.m_bossNameSh.text = name;
				bossName.text = name;
			}
			if (this.m_discoverer != null)
			{
				if (this.m_bossData.IsDiscoverer())
				{
					ServerSettingState settingState = ServerInterface.SettingState;
					if (settingState != null)
					{
						this.m_discoverer.text = settingState.m_userName;
					}
					else
					{
						this.m_discoverer.text = this.m_bossData.discoverer;
					}
				}
				else
				{
					this.m_discoverer.text = this.m_bossData.discoverer;
				}
			}
			if (this.m_bossLife != null)
			{
				this.m_bossLife.text = string.Format("{0}/{1}", this.m_bossData.hp, this.m_bossData.hpMax);
			}
			if (this.m_bossLifeBar != null)
			{
				this.m_bossLifeBar.value = this.m_bossData.GetHpRate();
				this.m_bossLifeBar.numberOfSteps = 1;
				this.m_bossLifeBar.ForceUpdate();
			}
			if (this.m_bossLv != null)
			{
				string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "ui_LevelNumber").text;
				this.m_bossLv.text = TextUtility.Replace(text, "{PARAM}", this.m_bossData.lv.ToString());
			}
			if (this.m_bossIcon != null && this.m_bossRarity != null)
			{
				this.m_bossIcon.spriteName = "ui_gp_gauge_boss_icon_raid_" + this.m_bossData.rarity;
				if (this.m_bossData.rarity >= 2)
				{
					this.m_bossRarity.spriteName = "ui_event_raidboss_window_bosslevel_2";
					this.m_bossRarity.enabled = true;
				}
				else if (this.m_bossData.rarity >= 1)
				{
					this.m_bossRarity.spriteName = "ui_event_raidboss_window_bosslevel_1";
					this.m_bossRarity.enabled = true;
				}
				else
				{
					this.m_bossRarity.spriteName = "ui_event_raidboss_window_bosslevel_0";
					this.m_bossRarity.enabled = true;
				}
			}
			UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "bar_base");
			if (uisprite != null)
			{
				if (this.m_bossData.IsDiscoverer())
				{
					uisprite.spriteName = "ui_event_raidboss_window_boss_bar_1";
				}
				else
				{
					uisprite.spriteName = "ui_event_raidboss_window_boss_bar_0";
				}
			}
		}
		this.PeriodicUpdate();
		this.UpdateBtn(infoWindow);
	}

	// Token: 0x06002ADF RID: 10975 RVA: 0x001091B8 File Offset: 0x001073B8
	private void UpdateBtn(bool infoWindow = false)
	{
		if (this.m_bossData != null)
		{
			if (infoWindow)
			{
				bool flag = false;
				if (this.m_rightsideBtns != null)
				{
					this.m_rightsideBtns.SetActive(true);
					if (this.m_rightsideBtn0 != null)
					{
						if (!this.m_bossData.end)
						{
							this.m_rightsideBtn0.SetActive(true);
							UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(this.m_rightsideBtn0, "Btn_log");
							UIImageButton uiimageButton2 = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(this.m_rightsideBtn0, "Btn_play");
							if (uiimageButton != null && this.m_bossData != null)
							{
								if (this.m_bossData.IsDiscoverer() && !this.m_bossData.participation)
								{
									uiimageButton.isEnabled = false;
								}
								else
								{
									uiimageButton.isEnabled = true;
								}
							}
							if (uiimageButton2 != null && this.m_bossData != null)
							{
								uiimageButton2.isEnabled = true;
							}
							flag = true;
						}
						else
						{
							this.m_rightsideBtn0.SetActive(false);
						}
					}
					if (this.m_rightsideBtn1 != null)
					{
						if (!flag)
						{
							if (this.m_bossData.end && this.m_bossData.clear)
							{
								this.m_rightsideBtn1.SetActive(true);
								flag = true;
							}
							else
							{
								this.m_rightsideBtn1.SetActive(false);
							}
						}
						else
						{
							this.m_rightsideBtn1.SetActive(false);
						}
					}
					if (this.m_rightsideBtn2 != null)
					{
						if (!flag)
						{
							if ((this.m_bossData.participation || this.m_bossData.IsDiscoverer()) && this.m_bossData.end && !this.m_bossData.clear)
							{
								this.m_rightsideBtn2.SetActive(true);
							}
							else
							{
								this.m_rightsideBtn2.SetActive(false);
							}
						}
						else
						{
							this.m_rightsideBtn2.SetActive(false);
						}
					}
				}
			}
			else if (this.m_rightsideBtns != null)
			{
				this.m_rightsideBtns.SetActive(false);
				if (this.m_rightsideBtn0 != null)
				{
					this.m_rightsideBtn0.SetActive(false);
				}
				if (this.m_rightsideBtn1 != null)
				{
					this.m_rightsideBtn1.SetActive(false);
				}
				if (this.m_rightsideBtn2 != null)
				{
					this.m_rightsideBtn2.SetActive(false);
				}
			}
		}
	}

	// Token: 0x06002AE0 RID: 10976 RVA: 0x00109428 File Offset: 0x00107628
	private void OnClickPlay()
	{
		if (this.m_bossData != null && this.m_parent != null)
		{
			if (GeneralUtil.IsNetwork())
			{
				TimeSpan timeLimit = this.m_bossData.GetTimeLimit();
				if (timeLimit.TotalSeconds > 0.5 || timeLimit.TotalSeconds < -0.25)
				{
					this.m_parent.OnClickBossPlayButton(this.m_bossData);
				}
			}
			else
			{
				this.m_parent.ShowNoCommunication();
			}
		}
	}

	// Token: 0x06002AE1 RID: 10977 RVA: 0x001094B4 File Offset: 0x001076B4
	private void OnClickInfo()
	{
		if (this.m_bossData != null && this.m_parent != null)
		{
			if (GeneralUtil.IsNetwork())
			{
				ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
				if (loggedInServerInterface != null)
				{
					int eventId = -1;
					if (EventManager.Instance != null)
					{
						eventId = EventManager.Instance.Id;
					}
					loggedInServerInterface.RequestServerGetEventRaidBossUserList(eventId, this.m_bossData.id, base.gameObject);
				}
				else
				{
					this.ServerGetEventRaidBossUserList_Succeeded(null);
				}
			}
			else
			{
				this.m_parent.ShowNoCommunication();
			}
		}
	}

	// Token: 0x06002AE2 RID: 10978 RVA: 0x0010954C File Offset: 0x0010774C
	private void ServerGetEventRaidBossUserList_Succeeded(MsgGetEventRaidBossUserListSucceed msg)
	{
		if (this.m_bossData != null && EventManager.Instance != null)
		{
			RaidBossInfo raidBossInfo = EventManager.Instance.RaidBossInfo;
			if (raidBossInfo != null)
			{
				List<RaidBossData> raidData = raidBossInfo.raidData;
				foreach (RaidBossData raidBossData in raidData)
				{
					if (raidBossData != null && raidBossData.id == this.m_bossData.id)
					{
						this.m_bossData = raidBossData;
						break;
					}
				}
			}
			if (this.m_bossData.end && this.m_bossData.clear)
			{
				this.m_parent.OnClickBossRewardButton(this.m_bossData);
				this.SetMessageBoxConnect();
			}
			else
			{
				this.m_parent.OnClickBossInfoButton(this.m_bossData);
			}
		}
	}

	// Token: 0x06002AE3 RID: 10979 RVA: 0x00109650 File Offset: 0x00107850
	private void ServerGetEventRaidBossUserList_Failed(MsgGetEventRaidBossUserListSucceed msg)
	{
		this.m_parent.OnClickBossRewardButton(this.m_bossData);
	}

	// Token: 0x06002AE4 RID: 10980 RVA: 0x00109664 File Offset: 0x00107864
	private void SetMessageBoxConnect()
	{
		if (SaveDataManager.Instance != null && SaveDataManager.Instance.ConnectData != null)
		{
			SaveDataManager.Instance.ConnectData.ReplaceMessageBox = true;
		}
	}

	// Token: 0x04002643 RID: 9795
	private const float UPDATE_TIME = 0.25f;

	// Token: 0x04002644 RID: 9796
	private const float RELOAD_DELAY_TIME = 1f;

	// Token: 0x04002645 RID: 9797
	[SerializeField]
	private UISprite m_bossIcon;

	// Token: 0x04002646 RID: 9798
	[SerializeField]
	private UISprite m_bossRarity;

	// Token: 0x04002647 RID: 9799
	[SerializeField]
	private UILabel m_bossName;

	// Token: 0x04002648 RID: 9800
	[SerializeField]
	private UILabel m_bossNameSh;

	// Token: 0x04002649 RID: 9801
	[SerializeField]
	private UILabel m_bossLv;

	// Token: 0x0400264A RID: 9802
	[SerializeField]
	private UILabel m_bossLife;

	// Token: 0x0400264B RID: 9803
	[SerializeField]
	private UISlider m_bossLifeBar;

	// Token: 0x0400264C RID: 9804
	[SerializeField]
	private UILabel m_discoverer;

	// Token: 0x0400264D RID: 9805
	[SerializeField]
	private UILabel m_limit;

	// Token: 0x0400264E RID: 9806
	[SerializeField]
	private GameObject m_rightsideBtns;

	// Token: 0x0400264F RID: 9807
	[SerializeField]
	private GameObject m_rightsideBtn0;

	// Token: 0x04002650 RID: 9808
	[SerializeField]
	private GameObject m_rightsideBtn1;

	// Token: 0x04002651 RID: 9809
	[SerializeField]
	private GameObject m_rightsideBtn2;

	// Token: 0x04002652 RID: 9810
	private RaidBossWindow m_parent;

	// Token: 0x04002653 RID: 9811
	private RaidBossData m_bossData;

	// Token: 0x04002654 RID: 9812
	private bool m_infoWindow;

	// Token: 0x04002655 RID: 9813
	private float m_targetFrameTime = 0.016666668f;

	// Token: 0x04002656 RID: 9814
	private float m_reloadDelay;

	// Token: 0x04002657 RID: 9815
	private float m_timeCounter = 0.25f;
}
