using System;
using AnimationOrTween;
using Text;
using UnityEngine;

// Token: 0x020003FD RID: 1021
public class DailyInfo : MonoBehaviour
{
	// Token: 0x06001E5B RID: 7771 RVA: 0x000B2C50 File Offset: 0x000B0E50
	private void Start()
	{
	}

	// Token: 0x06001E5C RID: 7772 RVA: 0x000B2C54 File Offset: 0x000B0E54
	public static bool Open()
	{
		bool result = false;
		GameObject menuAnimUIObject = HudMenuUtility.GetMenuAnimUIObject();
		if (menuAnimUIObject != null)
		{
			DailyInfo dailyInfo = GameObjectUtil.FindChildGameObjectComponent<DailyInfo>(menuAnimUIObject, "DailyInfoUI");
			if (dailyInfo != null)
			{
				dailyInfo.Setup();
				result = true;
			}
		}
		return result;
	}

	// Token: 0x06001E5D RID: 7773 RVA: 0x000B2C98 File Offset: 0x000B0E98
	private void Setup()
	{
		if (this.m_mainPanel != null)
		{
			this.m_mainPanel.alpha = 0.1f;
		}
		this.m_isEnd = false;
		this.m_isClickClose = false;
		this.m_isHistory = false;
		if (this.m_animation != null)
		{
			ActiveAnimation.Play(this.m_animation, "ui_daily_challenge_infomation_intro_Anim", Direction.Forward);
		}
		GeneralUtil.SetButtonFunc(base.gameObject, "Btn_today", base.gameObject, "OnClickToggleToday");
		GeneralUtil.SetButtonFunc(base.gameObject, "Btn_history", base.gameObject, "OnClickToggleHistory");
		GeneralUtil.SetButtonFunc(base.gameObject, "Btn_help", base.gameObject, "OnClickHelp");
		this.SetupToggleBtn();
		this.ChangeInfo();
		base.gameObject.SetActive(true);
	}

	// Token: 0x06001E5E RID: 7774 RVA: 0x000B2D6C File Offset: 0x000B0F6C
	private void SetupToggleBtn()
	{
		this.m_isToggleLock = true;
		UIToggle uitoggle;
		if (this.m_isHistory)
		{
			uitoggle = GameObjectUtil.FindChildGameObjectComponent<UIToggle>(base.gameObject, "Btn_history");
			UIToggle uitoggle2 = GameObjectUtil.FindChildGameObjectComponent<UIToggle>(base.gameObject, "Btn_today");
			if (uitoggle2 != null)
			{
				uitoggle2.startsActive = false;
			}
		}
		else
		{
			uitoggle = GameObjectUtil.FindChildGameObjectComponent<UIToggle>(base.gameObject, "Btn_today");
			UIToggle uitoggle3 = GameObjectUtil.FindChildGameObjectComponent<UIToggle>(base.gameObject, "Btn_history");
			if (uitoggle3 != null)
			{
				uitoggle3.startsActive = false;
			}
		}
		uitoggle.startsActive = true;
		if (uitoggle != null)
		{
			uitoggle.SendMessage("Start");
		}
		this.m_isToggleLock = false;
	}

	// Token: 0x06001E5F RID: 7775 RVA: 0x000B2E24 File Offset: 0x000B1024
	private void OnClickBack()
	{
		this.m_isClickClose = true;
		SoundManager.SePlay("sys_menu_decide", "SE");
	}

	// Token: 0x06001E60 RID: 7776 RVA: 0x000B2E40 File Offset: 0x000B1040
	private void OnClickToggleToday()
	{
		if (!this.m_isToggleLock && this.m_isHistory)
		{
			if (GeneralUtil.IsNetwork())
			{
				this.m_isHistory = false;
				this.ChangeInfo();
			}
			else
			{
				this.SetupToggleBtn();
				GeneralUtil.ShowNoCommunication("ShowNoCommunicationDailyInfo");
			}
			SoundManager.SePlay("sys_menu_decide", "SE");
		}
	}

	// Token: 0x06001E61 RID: 7777 RVA: 0x000B2EA0 File Offset: 0x000B10A0
	private void OnClickToggleHistory()
	{
		if (!this.m_isToggleLock && !this.m_isHistory)
		{
			if (GeneralUtil.IsNetwork())
			{
				this.m_isHistory = true;
				this.ChangeInfo();
			}
			else
			{
				this.SetupToggleBtn();
				GeneralUtil.ShowNoCommunication("ShowNoCommunicationDailyInfo");
			}
			SoundManager.SePlay("sys_menu_decide", "SE");
		}
	}

	// Token: 0x06001E62 RID: 7778 RVA: 0x000B2F00 File Offset: 0x000B1100
	private void OnClickHelp()
	{
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = "ShowDailyBattleHelp",
			buttonType = GeneralWindow.ButtonType.Ok,
			caption = TextUtility.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "DailyMission", "battle_help_caption"),
			message = TextUtility.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "DailyMission", "battle_help_text")
		});
		SoundManager.SePlay("sys_menu_decide", "SE");
	}

	// Token: 0x06001E63 RID: 7779 RVA: 0x000B2F70 File Offset: 0x000B1170
	private void ChangeInfo()
	{
		if (!this.m_isHistory)
		{
			this.SetupMainObject();
		}
		else
		{
			this.SetupHistoryObject();
		}
	}

	// Token: 0x06001E64 RID: 7780 RVA: 0x000B2F90 File Offset: 0x000B1190
	private void SetupMainObject()
	{
		if (this.m_battleObject == null)
		{
			this.m_battleObject = GameObjectUtil.FindChildGameObject(base.gameObject, "battle_set");
		}
		if (this.m_historyObject == null)
		{
			this.m_historyObject = GameObjectUtil.FindChildGameObject(base.gameObject, "history_set");
		}
		if (this.m_battleObject != null)
		{
			this.m_battleObject.SetActive(true);
			DailyInfoBattle dailyInfoBattle = GameObjectUtil.FindChildGameObjectComponent<DailyInfoBattle>(this.m_battleObject, "battle_set");
			if (dailyInfoBattle == null)
			{
				dailyInfoBattle = this.m_battleObject.AddComponent<DailyInfoBattle>();
			}
			if (dailyInfoBattle != null)
			{
				dailyInfoBattle.Setup(this);
			}
		}
		if (this.m_historyObject != null)
		{
			this.m_historyObject.SetActive(false);
		}
	}

	// Token: 0x06001E65 RID: 7781 RVA: 0x000B3064 File Offset: 0x000B1264
	private void SetupHistoryObject()
	{
		if (this.m_battleObject == null)
		{
			this.m_battleObject = GameObjectUtil.FindChildGameObject(base.gameObject, "battle_set");
		}
		if (this.m_historyObject == null)
		{
			this.m_historyObject = GameObjectUtil.FindChildGameObject(base.gameObject, "history_set");
		}
		if (this.m_battleObject != null)
		{
			this.m_battleObject.SetActive(false);
		}
		if (this.m_historyObject != null)
		{
			this.m_historyObject.SetActive(true);
			DailyInfoHistory dailyInfoHistory = GameObjectUtil.FindChildGameObjectComponent<DailyInfoHistory>(this.m_historyObject, "history_set");
			if (dailyInfoHistory == null)
			{
				dailyInfoHistory = this.m_historyObject.AddComponent<DailyInfoHistory>();
			}
			if (dailyInfoHistory != null)
			{
				dailyInfoHistory.Setup(this);
			}
		}
	}

	// Token: 0x06001E66 RID: 7782 RVA: 0x000B3138 File Offset: 0x000B1338
	public void OnClosedWindowAnim()
	{
		this.m_isEnd = true;
		this.m_isHistory = false;
		if (this.m_mainPanel != null)
		{
			this.m_mainPanel.alpha = 0f;
		}
		this.ChangeInfo();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001E67 RID: 7783 RVA: 0x000B3188 File Offset: 0x000B1388
	public void OnClickBackButton()
	{
		if (this.m_isEnd)
		{
			return;
		}
		if (!this.m_isClickClose)
		{
			this.m_isClickClose = true;
			ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_daily_challenge_infomation_intro_Anim", Direction.Reverse);
			if (activeAnimation != null)
			{
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnClosedWindowAnim), true);
			}
		}
	}

	// Token: 0x04001BB8 RID: 7096
	[SerializeField]
	private Animation m_animation;

	// Token: 0x04001BB9 RID: 7097
	[SerializeField]
	private UIPanel m_mainPanel;

	// Token: 0x04001BBA RID: 7098
	[SerializeField]
	private GameObject m_battleObject;

	// Token: 0x04001BBB RID: 7099
	[SerializeField]
	private GameObject m_historyObject;

	// Token: 0x04001BBC RID: 7100
	private bool m_isClickClose;

	// Token: 0x04001BBD RID: 7101
	private bool m_isEnd;

	// Token: 0x04001BBE RID: 7102
	private bool m_isHistory;

	// Token: 0x04001BBF RID: 7103
	private bool m_isToggleLock;
}
