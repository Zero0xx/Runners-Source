using System;
using System.Collections;
using System.Collections.Generic;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x020004D0 RID: 1232
public class MenuPlayerSetContents : MenuPlayerSetPartsBase
{
	// Token: 0x0600246B RID: 9323 RVA: 0x000DAE50 File Offset: 0x000D9050
	public MenuPlayerSetContents() : base("player_set_contents")
	{
	}

	// Token: 0x170004DC RID: 1244
	// (get) Token: 0x0600246C RID: 9324 RVA: 0x000DAE6C File Offset: 0x000D906C
	// (set) Token: 0x0600246D RID: 9325 RVA: 0x000DAE74 File Offset: 0x000D9074
	public bool IsEndSetup
	{
		get
		{
			return this.m_isEndSetup;
		}
		private set
		{
		}
	}

	// Token: 0x0600246E RID: 9326 RVA: 0x000DAE78 File Offset: 0x000D9078
	public void SetCallback(MenuPlayerSetContents.PageChangeCallback callback)
	{
		this.m_callback = callback;
	}

	// Token: 0x0600246F RID: 9327 RVA: 0x000DAE84 File Offset: 0x000D9084
	protected override void OnSetup()
	{
		base.StartCoroutine(this.SetupCoroutine());
	}

	// Token: 0x06002470 RID: 9328 RVA: 0x000DAE94 File Offset: 0x000D9094
	private void OnDestroy()
	{
		foreach (MenuPlayerSetCharaPage menuPlayerSetCharaPage in this.m_charaPage)
		{
			if (!(menuPlayerSetCharaPage == null))
			{
				UnityEngine.Object.Destroy(menuPlayerSetCharaPage);
			}
		}
		if (this.m_scrollBar != null)
		{
			this.m_scrollBar.SetPageChangeCallback(null);
			UnityEngine.Object.Destroy(this.m_scrollBar);
		}
	}

	// Token: 0x06002471 RID: 9329 RVA: 0x000DAF00 File Offset: 0x000D9100
	private IEnumerator SetupCoroutine()
	{
		this.m_isEndSetup = false;
		GameObject playerSetRoot = MenuPlayerSetUtil.GetPlayerSetRoot();
		GameObject pageRootParent = GameObjectUtil.FindChildGameObject(playerSetRoot, "grid_slot");
		List<GameObject> pageList = null;
		int openedCharaCount = MenuPlayerSetUtil.GetOpenedCharaCount();
		for (;;)
		{
			pageList = GameObjectUtil.FindChildGameObjects(pageRootParent, "ui_player_set_2_cell(Clone)");
			if (pageList == null)
			{
				break;
			}
			if (pageList.Count >= openedCharaCount)
			{
				break;
			}
			yield return null;
		}
		for (int index = 0; index < pageList.Count; index++)
		{
			GameObject pageObject = pageList[index];
			if (!(pageObject == null))
			{
				MenuPlayerSetCharaPage page = pageObject.AddComponent<MenuPlayerSetCharaPage>();
				CharaType charaType = MenuPlayerSetUtil.GetCharaTypeFromPageIndex(index);
				if (charaType != CharaType.UNKNOWN)
				{
					page.Setup(pageObject, charaType);
					while (!page.IsEndSetUp)
					{
						yield return null;
					}
					MenuPlayerSetUtil.ActivateCharaPageObjects(page.gameObject, false);
					this.m_charaPage[index] = page;
				}
			}
		}
		yield return null;
		for (int index2 = 0; index2 < pageList.Count; index2++)
		{
			GameObject pageObject2 = pageList[index2];
			if (!(pageObject2 == null))
			{
				MenuPlayerSetUtil.ActivateCharaPageObjects(this.m_charaPage[index2].gameObject, false);
			}
		}
		GameObject gripRootObject = GameObjectUtil.FindChildGameObject(base.gameObject, "player_set_pager_alpha_clip");
		if (gripRootObject != null)
		{
			this.m_shortCutMenu = gripRootObject.AddComponent<MenuPlayerSetShortCutMenu>();
			if (this.m_shortCutMenu != null)
			{
				this.m_shortCutMenu.Setup(new MenuPlayerSetShortCutMenu.ShortCutCallback(this.ShortCutButtonClickedCallback));
			}
			while (!(this.m_shortCutMenu != null) || !this.m_shortCutMenu.IsEndSetup)
			{
				yield return null;
			}
		}
		if (this.m_scrollBar == null)
		{
			this.m_scrollBar = base.gameObject.AddComponent<HudScrollBar>();
		}
		UIScrollBar scrollBar = GameObjectUtil.FindChildGameObjectComponent<UIScrollBar>(base.gameObject, "player_set_SB");
		int pageCount = pageList.Count;
		this.m_scrollBar.Setup(scrollBar, pageCount);
		this.m_scrollBar.SetPageChangeCallback(new HudScrollBar.PageChangeCallback(this.ChangePageCallback));
		this.m_isEndSetup = true;
		yield break;
	}

	// Token: 0x06002472 RID: 9330 RVA: 0x000DAF1C File Offset: 0x000D911C
	public void JumpCharacterPage(int pageIndex)
	{
		base.StartCoroutine(this.JumpCharaPageCoroutine(pageIndex));
	}

	// Token: 0x06002473 RID: 9331 RVA: 0x000DAF2C File Offset: 0x000D912C
	public void ChangeMainChara(CharaType newCharaType)
	{
		PlayerData playerData = SaveDataManager.Instance.PlayerData;
		CharaType mainChara = playerData.MainChara;
		if (mainChara == newCharaType)
		{
			return;
		}
		CharaType subChara = playerData.SubChara;
		if (subChara == newCharaType)
		{
			CharaType mainChara2 = playerData.MainChara;
			playerData.MainChara = playerData.SubChara;
			playerData.SubChara = mainChara2;
		}
		else
		{
			if (playerData.SubChara == CharaType.UNKNOWN)
			{
				playerData.SubChara = playerData.MainChara;
			}
			playerData.MainChara = newCharaType;
		}
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			int mainCharaId = -1;
			ServerCharacterState serverCharacterState = ServerInterface.PlayerState.CharacterState(playerData.MainChara);
			if (serverCharacterState != null)
			{
				mainCharaId = serverCharacterState.Id;
			}
			int subCharaId = -1;
			ServerCharacterState serverCharacterState2 = ServerInterface.PlayerState.CharacterState(playerData.SubChara);
			if (serverCharacterState2 != null)
			{
				subCharaId = serverCharacterState2.Id;
			}
			loggedInServerInterface.RequestServerChangeCharacter(mainCharaId, subCharaId, base.gameObject);
		}
		this.UpdateRibbon();
	}

	// Token: 0x06002474 RID: 9332 RVA: 0x000DB014 File Offset: 0x000D9214
	public void ChangeSubChara(CharaType newCharaType)
	{
		PlayerData playerData = SaveDataManager.Instance.PlayerData;
		CharaType subChara = playerData.SubChara;
		if (subChara == newCharaType)
		{
			return;
		}
		CharaType mainChara = playerData.MainChara;
		if (mainChara == newCharaType)
		{
			CharaType subChara2 = playerData.SubChara;
			if (subChara2 == CharaType.UNKNOWN)
			{
				GeneralWindow.CInfo info = default(GeneralWindow.CInfo);
				string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "PlayerSet", "ui_Lbl_player_config").text;
				string text2 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "WindowText", "change_chara_error").text;
				info.caption = text;
				info.message = text2;
				info.anchor_path = "Camera/menu_Anim/PlayerSet_2_UI/Anchor_5_MC";
				info.buttonType = GeneralWindow.ButtonType.Ok;
				info.isPlayErrorSe = true;
				GeneralWindow.Create(info);
				return;
			}
			playerData.SubChara = playerData.MainChara;
			playerData.MainChara = subChara2;
		}
		playerData.SubChara = newCharaType;
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			int mainCharaId = -1;
			ServerCharacterState serverCharacterState = ServerInterface.PlayerState.CharacterState(playerData.MainChara);
			if (serverCharacterState != null)
			{
				mainCharaId = serverCharacterState.Id;
			}
			int subCharaId = -1;
			ServerCharacterState serverCharacterState2 = ServerInterface.PlayerState.CharacterState(playerData.SubChara);
			if (serverCharacterState2 != null)
			{
				subCharaId = serverCharacterState2.Id;
			}
			loggedInServerInterface.RequestServerChangeCharacter(mainCharaId, subCharaId, base.gameObject);
		}
		this.UpdateRibbon();
	}

	// Token: 0x06002475 RID: 9333 RVA: 0x000DB154 File Offset: 0x000D9354
	public void UnlockedChara(CharaType unlockedChara)
	{
		if (MenuPlayerSetUtil.GetPlayableCharaCount() == 2)
		{
			this.ChangeSubChara(unlockedChara);
		}
		else
		{
			int pageIndexFromCharaType = MenuPlayerSetUtil.GetPageIndexFromCharaType(unlockedChara);
			MenuPlayerSetCharaPage menuPlayerSetCharaPage = this.m_charaPage[pageIndexFromCharaType];
			if (menuPlayerSetCharaPage != null)
			{
				menuPlayerSetCharaPage.OnSelected();
			}
		}
		if (this.m_shortCutMenu != null)
		{
			this.m_shortCutMenu.UnclockCharacter(unlockedChara);
		}
		AchievementManager.RequestUpdate();
	}

	// Token: 0x06002476 RID: 9334 RVA: 0x000DB1BC File Offset: 0x000D93BC
	protected override void OnPlayStart()
	{
	}

	// Token: 0x06002477 RID: 9335 RVA: 0x000DB1C0 File Offset: 0x000D93C0
	protected override void OnPlayEnd()
	{
	}

	// Token: 0x06002478 RID: 9336 RVA: 0x000DB1C4 File Offset: 0x000D93C4
	protected override void OnUpdate(float deltaTime)
	{
		if (GeneralWindow.IsOkButtonPressed)
		{
			GeneralWindow.Close();
		}
	}

	// Token: 0x06002479 RID: 9337 RVA: 0x000DB1D8 File Offset: 0x000D93D8
	private IEnumerator JumpCharaPageCoroutine(int pageIndex)
	{
		while (!this.m_isEndSetup)
		{
			yield return null;
		}
		if (this.m_scrollBar != null)
		{
			this.m_scrollBar.PageJump(pageIndex, true);
		}
		yield break;
	}

	// Token: 0x0600247A RID: 9338 RVA: 0x000DB204 File Offset: 0x000D9404
	public void UpdateRibbon()
	{
		for (int i = 0; i < 29; i++)
		{
			MenuPlayerSetCharaPage menuPlayerSetCharaPage = this.m_charaPage[i];
			if (!(menuPlayerSetCharaPage == null))
			{
				menuPlayerSetCharaPage.UpdateRibbon();
			}
		}
	}

	// Token: 0x0600247B RID: 9339 RVA: 0x000DB244 File Offset: 0x000D9444
	private void ShortCutButtonClickedCallback(CharaType charaType)
	{
		if (this.m_scrollBar != null)
		{
			int pageIndexFromCharaType = MenuPlayerSetUtil.GetPageIndexFromCharaType(charaType);
			this.m_scrollBar.PageJump(pageIndexFromCharaType, false);
		}
	}

	// Token: 0x0600247C RID: 9340 RVA: 0x000DB278 File Offset: 0x000D9478
	private void ChangePageCallback(int prevPage, int currentPage)
	{
		int openedCharaCount = MenuPlayerSetUtil.GetOpenedCharaCount();
		int num = Mathf.Clamp(prevPage - 1, 0, openedCharaCount - 1);
		int num2 = Mathf.Clamp(prevPage + 1, 0, openedCharaCount - 1);
		MenuPlayerSetUtil.ActivateCharaPageObjects(this.m_charaPage[num].gameObject, false);
		MenuPlayerSetUtil.ActivateCharaPageObjects(this.m_charaPage[prevPage].gameObject, false);
		MenuPlayerSetUtil.ActivateCharaPageObjects(this.m_charaPage[num2].gameObject, false);
		int num3 = Mathf.Clamp(currentPage - 1, 0, openedCharaCount - 1);
		int num4 = Mathf.Clamp(currentPage + 1, 0, openedCharaCount - 1);
		MenuPlayerSetUtil.ActivateCharaPageObjects(this.m_charaPage[num3].gameObject, true);
		MenuPlayerSetUtil.ActivateCharaPageObjects(this.m_charaPage[currentPage].gameObject, true);
		MenuPlayerSetUtil.ActivateCharaPageObjects(this.m_charaPage[num4].gameObject, true);
		if (this.m_shortCutMenu != null)
		{
			CharaType charaTypeFromPageIndex = MenuPlayerSetUtil.GetCharaTypeFromPageIndex(currentPage);
			this.m_shortCutMenu.SetActiveCharacter(charaTypeFromPageIndex, false);
		}
		if (this.m_callback != null)
		{
			this.m_callback(currentPage);
		}
		SoundManager.SePlay("sys_page_skip", "SE");
	}

	// Token: 0x040020E7 RID: 8423
	private MenuPlayerSetCharaPage[] m_charaPage = new MenuPlayerSetCharaPage[29];

	// Token: 0x040020E8 RID: 8424
	private MenuPlayerSetShortCutMenu m_shortCutMenu;

	// Token: 0x040020E9 RID: 8425
	private HudScrollBar m_scrollBar;

	// Token: 0x040020EA RID: 8426
	private MenuPlayerSetContents.PageChangeCallback m_callback;

	// Token: 0x040020EB RID: 8427
	private bool m_isEndSetup;

	// Token: 0x02000A96 RID: 2710
	// (Invoke) Token: 0x06004892 RID: 18578
	public delegate void PageChangeCallback(int pageIndex);
}
