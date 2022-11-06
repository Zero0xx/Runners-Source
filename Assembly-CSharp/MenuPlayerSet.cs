using System;
using System.Collections;
using System.Collections.Generic;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x020004BE RID: 1214
public class MenuPlayerSet : MonoBehaviour
{
	// Token: 0x170004D6 RID: 1238
	// (get) Token: 0x060023F9 RID: 9209 RVA: 0x000D7E54 File Offset: 0x000D6054
	public bool SetUpped
	{
		get
		{
			return this.m_state == MenuPlayerSet.State.SETUPED;
		}
	}

	// Token: 0x060023FA RID: 9210 RVA: 0x000D7E68 File Offset: 0x000D6068
	public void StartMainCharacter()
	{
		if (MenuPlayerSetUtil.IsMarkCharaPage())
		{
			int pageIndexFromCharaType = MenuPlayerSetUtil.GetPageIndexFromCharaType(MenuPlayerSetUtil.MarkCharaType);
			base.StartCoroutine(this.JumpCharacterPage(pageIndexFromCharaType));
			MenuPlayerSetUtil.ResetMarkCharaPage();
		}
		else
		{
			CharaType mainChara = SaveDataManager.Instance.PlayerData.MainChara;
			int pageIndexFromCharaType2 = MenuPlayerSetUtil.GetPageIndexFromCharaType(mainChara);
			base.StartCoroutine(this.JumpCharacterPage(pageIndexFromCharaType2));
		}
		if (HudMenuUtility.IsTutorial_CharaLevelUp())
		{
			this.PrepareTutorialLevelUp();
		}
	}

	// Token: 0x060023FB RID: 9211 RVA: 0x000D7ED8 File Offset: 0x000D60D8
	public void StartSubCharacter()
	{
		if (MenuPlayerSetUtil.IsMarkCharaPage())
		{
			int pageIndexFromCharaType = MenuPlayerSetUtil.GetPageIndexFromCharaType(MenuPlayerSetUtil.MarkCharaType);
			base.StartCoroutine(this.JumpCharacterPage(pageIndexFromCharaType));
			MenuPlayerSetUtil.ResetMarkCharaPage();
		}
		else
		{
			CharaType subChara = SaveDataManager.Instance.PlayerData.SubChara;
			int pageIndexFromCharaType2 = MenuPlayerSetUtil.GetPageIndexFromCharaType(subChara);
			base.StartCoroutine(this.JumpCharacterPage(pageIndexFromCharaType2));
		}
		if (HudMenuUtility.IsTutorial_CharaLevelUp())
		{
			this.PrepareTutorialLevelUp();
		}
	}

	// Token: 0x060023FC RID: 9212 RVA: 0x000D7F48 File Offset: 0x000D6148
	public void StartCharacter(CharaType type)
	{
		int pageIndexFromCharaType = MenuPlayerSetUtil.GetPageIndexFromCharaType(type);
		base.StartCoroutine(this.JumpCharacterPage(pageIndexFromCharaType));
		if (HudMenuUtility.IsTutorial_CharaLevelUp())
		{
			this.PrepareTutorialLevelUp();
		}
	}

	// Token: 0x060023FD RID: 9213 RVA: 0x000D7F7C File Offset: 0x000D617C
	private IEnumerator JumpCharacterPage(int pageIndex)
	{
		while (this.m_state != MenuPlayerSet.State.SETUPED)
		{
			yield return null;
		}
		MenuPlayerSetContents contetns = base.gameObject.GetComponent<MenuPlayerSetContents>();
		if (contetns != null)
		{
			contetns.JumpCharacterPage(pageIndex);
		}
		yield break;
	}

	// Token: 0x060023FE RID: 9214 RVA: 0x000D7FA8 File Offset: 0x000D61A8
	private void PrepareTutorialLevelUp()
	{
		HudMenuUtility.SetConnectAlertSimpleUI(true);
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = "chara_level_up_explan",
			buttonType = GeneralWindow.ButtonType.Ok,
			caption = TextUtility.GetCommonText("MainMenu", "chara_level_up_explan_caption"),
			message = TextUtility.GetCommonText("MainMenu", "chara_level_up_explan"),
			finishedCloseDelegate = new GeneralWindow.CInfo.FinishedCloseDelegate(this.GeneralWindowCharaLevelUpCloseCallback)
		});
		string[] value = new string[1];
		SendApollo.GetTutorialValue(ApolloTutorialIndex.START_STEP7, ref value);
		this.m_sendApollo = SendApollo.CreateRequest(ApolloType.TUTORIAL_START, value);
		this.m_tutorialMode = MenuPlayerSet.TutorialMode.WaitWindow;
	}

	// Token: 0x060023FF RID: 9215 RVA: 0x000D8040 File Offset: 0x000D6240
	private void GeneralWindowCharaLevelUpCloseCallback()
	{
		HudMenuUtility.SetConnectAlertSimpleUI(false);
		TutorialCursor.StartTutorialCursor(TutorialCursor.Type.CHARASELECT_LEVEL_UP);
		this.m_tutorialMode = MenuPlayerSet.TutorialMode.WaitClickLevelUpButton;
	}

	// Token: 0x06002400 RID: 9216 RVA: 0x000D8058 File Offset: 0x000D6258
	private void OnClickedLevelUpButton()
	{
		TutorialCursor.DestroyTutorialCursor();
		HudMenuUtility.SaveSystemDataFlagStatus(SystemData.FlagStatus.CHARA_LEVEL_UP_EXPLAINED);
		string[] value = new string[1];
		SendApollo.GetTutorialValue(ApolloTutorialIndex.START_STEP7, ref value);
		this.m_sendApollo = SendApollo.CreateRequest(ApolloType.TUTORIAL_END, value);
		this.m_tutorialMode = MenuPlayerSet.TutorialMode.EndClickLevelUpButton;
	}

	// Token: 0x06002401 RID: 9217 RVA: 0x000D8098 File Offset: 0x000D6298
	private void Start()
	{
		this.m_partsList = new List<MenuPlayerSetPartsBase>();
		base.StartCoroutine(this.Setup());
	}

	// Token: 0x06002402 RID: 9218 RVA: 0x000D80B4 File Offset: 0x000D62B4
	private void OnEnable()
	{
		base.StartCoroutine(this.ReSetUp());
	}

	// Token: 0x06002403 RID: 9219 RVA: 0x000D80C4 File Offset: 0x000D62C4
	private void Update()
	{
		switch (this.m_tutorialMode)
		{
		case MenuPlayerSet.TutorialMode.WaitWindow:
			if (this.m_sendApollo != null && this.m_sendApollo.IsEnd() && GeneralWindow.IsCreated("chara_level_up_explan") && GeneralWindow.IsOkButtonPressed)
			{
				GeneralWindow.Close();
				UnityEngine.Object.Destroy(this.m_sendApollo.gameObject);
				this.m_sendApollo = null;
			}
			break;
		case MenuPlayerSet.TutorialMode.EndClickLevelUpButton:
			if (this.m_sendApollo != null && this.m_sendApollo.IsEnd())
			{
				UnityEngine.Object.Destroy(this.m_sendApollo.gameObject);
				this.m_sendApollo = null;
				this.m_tutorialMode = MenuPlayerSet.TutorialMode.Idle;
			}
			break;
		}
	}

	// Token: 0x06002404 RID: 9220 RVA: 0x000D81A0 File Offset: 0x000D63A0
	private IEnumerator Setup()
	{
		this.m_state = MenuPlayerSet.State.SETUPING;
		UIRectItemStorage itemStoragePage = GameObjectUtil.FindChildGameObjectComponent<UIRectItemStorage>(base.gameObject, "grid_slot");
		if (itemStoragePage != null)
		{
			int charaCount = MenuPlayerSetUtil.GetOpenedCharaCount();
			itemStoragePage.maxRows = 1;
			itemStoragePage.maxColumns = charaCount;
			itemStoragePage.maxItemCount = charaCount;
			itemStoragePage.m_activeType = UIRectItemStorage.ActiveType.NOT_ACTTIVE;
		}
		GameObject gripRootObject = GameObjectUtil.FindChildGameObject(base.gameObject, "player_set_pager_alpha_clip");
		if (gripRootObject != null)
		{
			UIRectItemStorage itemStorageShotCut = GameObjectUtil.FindChildGameObjectComponent<UIRectItemStorage>(gripRootObject, "slot");
			if (itemStorageShotCut != null)
			{
				int charaCount2 = MenuPlayerSetUtil.GetOpenedCharaCount();
				itemStorageShotCut.maxRows = 1;
				itemStorageShotCut.maxColumns = charaCount2;
				itemStorageShotCut.maxItemCount = charaCount2;
			}
		}
		this.m_partsList.Add(base.gameObject.AddComponent<MenuPlayerSetBG>());
		this.m_partsList.Add(base.gameObject.AddComponent<MenuPlayerSetGrip>());
		this.m_partsList.Add(base.gameObject.AddComponent<MenuPlayerSetGripL>());
		this.m_partsList.Add(base.gameObject.AddComponent<MenuPlayerSetGripR>());
		this.m_partsList.Add(base.gameObject.AddComponent<MenuPlayerSetReturnButton>());
		this.m_partsList.Add(base.gameObject.AddComponent<MenuPlayerSetContents>());
		MenuPlayerSetContents contents = base.gameObject.GetComponent<MenuPlayerSetContents>();
		if (contents != null)
		{
			contents.SetCallback(new MenuPlayerSetContents.PageChangeCallback(this.PageChangeCallback));
			for (;;)
			{
				bool isEndSetup = contents.IsEndSetup;
				if (isEndSetup)
				{
					break;
				}
				yield return null;
			}
		}
		MenuPlayerSetGripL gripL = base.gameObject.GetComponent<MenuPlayerSetGripL>();
		if (gripL != null)
		{
			gripL.SetCallback(new MenuPlayerSetGripL.ButtonClickCallback(this.OnClickLeftScrollButton));
		}
		MenuPlayerSetGripR gripR = base.gameObject.GetComponent<MenuPlayerSetGripR>();
		if (gripR != null)
		{
			gripR.SetCallback(new MenuPlayerSetGripR.ButtonClickCallback(this.OnClickRightScrollButton));
		}
		this.m_state = MenuPlayerSet.State.SETUPED;
		yield break;
	}

	// Token: 0x06002405 RID: 9221 RVA: 0x000D81BC File Offset: 0x000D63BC
	private IEnumerator ReSetUp()
	{
		if (this.m_state == MenuPlayerSet.State.SETUPED)
		{
			UIRectItemStorage itemStoragePage = GameObjectUtil.FindChildGameObjectComponent<UIRectItemStorage>(base.gameObject, "grid_slot");
			if (itemStoragePage != null)
			{
				int charaCount = MenuPlayerSetUtil.GetOpenedCharaCount();
				if (itemStoragePage.maxColumns != charaCount)
				{
					this.m_state = MenuPlayerSet.State.SETUPING;
					itemStoragePage.maxColumns = charaCount;
					itemStoragePage.maxItemCount = charaCount;
					itemStoragePage.Restart();
					GameObject gripRootObject = GameObjectUtil.FindChildGameObject(base.gameObject, "player_set_pager_alpha_clip");
					if (gripRootObject != null)
					{
						UIRectItemStorage itemStorageShotCut = GameObjectUtil.FindChildGameObjectComponent<UIRectItemStorage>(gripRootObject, "slot");
						if (itemStorageShotCut != null)
						{
							itemStorageShotCut.maxColumns = charaCount;
							itemStorageShotCut.maxItemCount = charaCount;
							itemStorageShotCut.Restart();
						}
					}
					MenuPlayerSetContents contents = base.gameObject.GetComponent<MenuPlayerSetContents>();
					if (contents != null)
					{
						contents.Reset();
						contents.SetCallback(new MenuPlayerSetContents.PageChangeCallback(this.PageChangeCallback));
						for (;;)
						{
							bool isEndSetup = contents.IsEndSetup;
							if (isEndSetup)
							{
								break;
							}
							yield return null;
						}
					}
					this.m_state = MenuPlayerSet.State.SETUPED;
				}
			}
			MenuPlayerSetContents contents2 = base.gameObject.GetComponent<MenuPlayerSetContents>();
			if (contents2 != null)
			{
				contents2.UpdateRibbon();
			}
		}
		yield break;
	}

	// Token: 0x06002406 RID: 9222 RVA: 0x000D81D8 File Offset: 0x000D63D8
	private void PageChangeCallback(int pageIndex)
	{
		MenuPlayerSetGripL component = base.gameObject.GetComponent<MenuPlayerSetGripL>();
		if (component != null)
		{
			if (pageIndex <= 0)
			{
				component.SetDisplayFlag(false);
			}
			else
			{
				component.SetDisplayFlag(true);
			}
		}
		MenuPlayerSetGripR component2 = base.gameObject.GetComponent<MenuPlayerSetGripR>();
		if (component2 != null)
		{
			int openedCharaCount = MenuPlayerSetUtil.GetOpenedCharaCount();
			if (pageIndex >= openedCharaCount - 1)
			{
				component2.SetDisplayFlag(false);
			}
			else
			{
				component2.SetDisplayFlag(true);
			}
		}
		this.m_currentPage = pageIndex;
	}

	// Token: 0x06002407 RID: 9223 RVA: 0x000D8258 File Offset: 0x000D6458
	private void OnClickLeftScrollButton()
	{
		this.m_currentPage--;
		if (this.m_currentPage < 0)
		{
			this.m_currentPage = 0;
		}
		base.StartCoroutine(this.JumpCharacterPage(this.m_currentPage));
	}

	// Token: 0x06002408 RID: 9224 RVA: 0x000D829C File Offset: 0x000D649C
	private void OnClickRightScrollButton()
	{
		this.m_currentPage++;
		int openedCharaCount = MenuPlayerSetUtil.GetOpenedCharaCount();
		if (this.m_currentPage >= openedCharaCount - 1)
		{
			this.m_currentPage = openedCharaCount - 1;
		}
		base.StartCoroutine(this.JumpCharacterPage(this.m_currentPage));
	}

	// Token: 0x0400209B RID: 8347
	private MenuPlayerSet.TutorialMode m_tutorialMode;

	// Token: 0x0400209C RID: 8348
	private List<MenuPlayerSetPartsBase> m_partsList;

	// Token: 0x0400209D RID: 8349
	private SendApollo m_sendApollo;

	// Token: 0x0400209E RID: 8350
	private MenuPlayerSet.State m_state;

	// Token: 0x0400209F RID: 8351
	private int m_currentPage;

	// Token: 0x020004BF RID: 1215
	private enum TutorialMode
	{
		// Token: 0x040020A1 RID: 8353
		Idle,
		// Token: 0x040020A2 RID: 8354
		WaitWindow,
		// Token: 0x040020A3 RID: 8355
		WaitClickLevelUpButton,
		// Token: 0x040020A4 RID: 8356
		EndClickLevelUpButton
	}

	// Token: 0x020004C0 RID: 1216
	private enum State
	{
		// Token: 0x040020A6 RID: 8358
		NOT_SETUP,
		// Token: 0x040020A7 RID: 8359
		SETUPING,
		// Token: 0x040020A8 RID: 8360
		SETUPED
	}
}
