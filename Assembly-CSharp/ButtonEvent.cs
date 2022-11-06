using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x02000432 RID: 1074
public class ButtonEvent : MonoBehaviour
{
	// Token: 0x17000480 RID: 1152
	// (get) Token: 0x0600209F RID: 8351 RVA: 0x000C3C88 File Offset: 0x000C1E88
	public bool IsTransform
	{
		get
		{
			return this.m_pageControl != null && this.m_pageControl.IsTransform;
		}
	}

	// Token: 0x060020A0 RID: 8352 RVA: 0x000C3CBC File Offset: 0x000C1EBC
	private void Start()
	{
		BackKeyManager.AddEventCallBack(base.gameObject);
	}

	// Token: 0x060020A1 RID: 8353 RVA: 0x000C3CCC File Offset: 0x000C1ECC
	public void OnStartMainMenu()
	{
		ButtonEvent.DebugInfoDraw("OnStartMainMenu");
		this.Initialize();
	}

	// Token: 0x060020A2 RID: 8354 RVA: 0x000C3CE0 File Offset: 0x000C1EE0
	private void Initialize()
	{
		this.m_menu_anim_obj = HudMenuUtility.GetMenuAnimUIObject();
		if (FontManager.Instance != null)
		{
			FontManager.Instance.ReplaceFont();
		}
		if (AtlasManager.Instance != null)
		{
			AtlasManager.Instance.ReplaceAtlasForMenu(true);
		}
		if (this.m_menu_anim_obj != null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_menu_anim_obj, "MainMenuCmnUI");
			if (gameObject != null)
			{
				gameObject.SetActive(true);
			}
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(this.m_menu_anim_obj, "MainMenuUI4");
			if (gameObject2 != null)
			{
				gameObject2.SetActive(true);
			}
		}
		if (this.m_timer == null)
		{
			this.m_timer = base.gameObject.AddComponent<ButtonEventTimer>();
		}
		if (this.m_backButton == null)
		{
			this.m_backButton = base.gameObject.AddComponent<ButtonEventBackButton>();
			this.m_backButton.Initialize(new ButtonEventBackButton.ButtonClickedCallback(this.ButtonClickedCallback));
		}
		if (this.m_pageControl == null)
		{
			this.m_pageControl = base.gameObject.AddComponent<ButtonEventPageControl>();
			this.m_pageControl.Initialize(new ButtonEventPageControl.ResourceLoadedCallback(this.OnPageResourceLoadedCallback));
		}
	}

	// Token: 0x060020A3 RID: 8355 RVA: 0x000C3E1C File Offset: 0x000C201C
	public void PageChange(ButtonInfoTable.ButtonType buttonType, bool isClearHistory, bool isButtonPressed)
	{
		if (this.m_pageControl == null)
		{
			return;
		}
		if (buttonType == ButtonInfoTable.ButtonType.UNKNOWN)
		{
			return;
		}
		if (this.m_timer == null)
		{
			return;
		}
		if (this.m_timer.IsWaiting())
		{
			return;
		}
		this.m_pageControl.PageChange(buttonType, isClearHistory, isButtonPressed);
		this.m_timer.SetWaitTimeDefault();
	}

	// Token: 0x060020A4 RID: 8356 RVA: 0x000C3E80 File Offset: 0x000C2080
	public void PageChangeMessage(MsgMenuButtonEvent msg)
	{
		ButtonInfoTable.ButtonType buttonType = msg.ButtonType;
		bool clearHistories = msg.m_clearHistories;
		bool isButtonPressed = false;
		this.PageChange(buttonType, clearHistories, isButtonPressed);
	}

	// Token: 0x060020A5 RID: 8357 RVA: 0x000C3EA8 File Offset: 0x000C20A8
	private void ButtonClickedCallback(ButtonInfoTable.ButtonType buttonType)
	{
		bool isClearHistory = false;
		bool isButtonPressed = true;
		this.PageChange(buttonType, isClearHistory, isButtonPressed);
	}

	// Token: 0x060020A6 RID: 8358 RVA: 0x000C3EC4 File Offset: 0x000C20C4
	private void OnClickPlatformBackButtonEvent()
	{
		if (this.m_timer == null)
		{
			return;
		}
		if (this.m_timer.IsWaiting())
		{
			return;
		}
		if (this.m_pageControl == null)
		{
			return;
		}
		this.m_pageControl.PageBack();
		this.m_timer.SetWaitTimeDefault();
	}

	// Token: 0x060020A7 RID: 8359 RVA: 0x000C3F1C File Offset: 0x000C211C
	private void OnPageResourceLoadedCallback()
	{
		if (FontManager.Instance != null)
		{
			FontManager.Instance.ReplaceFont();
		}
		if (AtlasManager.Instance != null)
		{
			AtlasManager.Instance.ReplaceAtlasForMenu(true);
		}
		if (this.m_backButton != null)
		{
			for (uint num = 0U; num < 49U; num += 1U)
			{
				this.m_backButton.SetupBackButton((ButtonInfoTable.ButtonType)num);
			}
		}
	}

	// Token: 0x060020A8 RID: 8360 RVA: 0x000C3F90 File Offset: 0x000C2190
	private void OnShopBackButtonClicked()
	{
		GameObjectUtil.SendMessageFindGameObject("ShopUI2", "OnShopBackButtonClicked", null, SendMessageOptions.DontRequireReceiver);
		HudMenuUtility.SendEnableShopButton(true);
	}

	// Token: 0x060020A9 RID: 8361 RVA: 0x000C3FAC File Offset: 0x000C21AC
	private void OnOptionBackButtonClicked()
	{
		GameObjectUtil.SendMessageFindGameObject("OptionUI", "OnEndOptionUI", null, SendMessageOptions.DontRequireReceiver);
		if (this.m_updateRanking)
		{
			HudMenuUtility.SendMsgUpdateRanking();
			this.m_updateRanking = false;
		}
	}

	// Token: 0x060020AA RID: 8362 RVA: 0x000C3FD8 File Offset: 0x000C21D8
	private void OnUpdateRankingFlag()
	{
		this.m_updateRanking = true;
	}

	// Token: 0x060020AB RID: 8363 RVA: 0x000C3FE4 File Offset: 0x000C21E4
	private void OnMenuEventClicked(GameObject menuObj)
	{
		base.StartCoroutine(this.LoadEventMenuResourceIfNotLoaded(menuObj));
	}

	// Token: 0x060020AC RID: 8364 RVA: 0x000C3FF4 File Offset: 0x000C21F4
	private IEnumerator LoadEventMenuResourceIfNotLoaded(GameObject menuObj)
	{
		yield return null;
		if (menuObj != null)
		{
			menuObj.SendMessage("OnButtonEventCallBack", null, SendMessageOptions.DontRequireReceiver);
		}
		yield break;
	}

	// Token: 0x060020AD RID: 8365 RVA: 0x000C4018 File Offset: 0x000C2218
	public static void DebugInfoDraw(string msg)
	{
	}

	// Token: 0x04001D33 RID: 7475
	private static bool m_debugInfo = true;

	// Token: 0x04001D34 RID: 7476
	private GameObject m_menu_anim_obj;

	// Token: 0x04001D35 RID: 7477
	private ButtonEventTimer m_timer;

	// Token: 0x04001D36 RID: 7478
	private ButtonEventBackButton m_backButton;

	// Token: 0x04001D37 RID: 7479
	private ButtonEventPageControl m_pageControl;

	// Token: 0x04001D38 RID: 7480
	private ButtonInfoTable m_info_table = new ButtonInfoTable();

	// Token: 0x04001D39 RID: 7481
	private bool m_updateRanking;
}
