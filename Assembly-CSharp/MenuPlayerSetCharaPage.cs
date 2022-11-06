using System;
using System.Collections;
using System.Collections.Generic;
using SaveData;
using UnityEngine;

// Token: 0x020004CF RID: 1231
public class MenuPlayerSetCharaPage : MonoBehaviour
{
	// Token: 0x170004DB RID: 1243
	// (get) Token: 0x06002462 RID: 9314 RVA: 0x000DAB7C File Offset: 0x000D8D7C
	// (set) Token: 0x06002463 RID: 9315 RVA: 0x000DAB84 File Offset: 0x000D8D84
	public bool IsEndSetUp
	{
		get
		{
			return this.m_isEndSetup;
		}
		private set
		{
		}
	}

	// Token: 0x06002464 RID: 9316 RVA: 0x000DAB88 File Offset: 0x000D8D88
	public void Setup(GameObject pageRoot, CharaType charaType)
	{
		base.gameObject.SetActive(true);
		this.m_isEndSetup = false;
		this.m_pageRoot = pageRoot;
		this.m_charaType = charaType;
		if (this.m_pageRoot == null)
		{
			return;
		}
		foreach (string text in MenuPlayerSetCharaPage.HideObjectName)
		{
			if (text != null)
			{
				GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, text);
				if (!(gameObject == null))
				{
					gameObject.SetActive(true);
				}
			}
		}
		base.StartCoroutine(this.SetupCoroutine());
	}

	// Token: 0x06002465 RID: 9317 RVA: 0x000DAC24 File Offset: 0x000D8E24
	private IEnumerator SetupCoroutine()
	{
		this.m_charaButton = base.gameObject.AddComponent<MenuPlayerSetCharaButton>();
		if (this.m_charaButton != null)
		{
			this.m_charaButton.Setup(this.m_charaType, this.m_pageRoot);
		}
		this.m_levelUpButton = base.gameObject.AddComponent<MenuPlayerSetLevelUpButton>();
		if (this.m_levelUpButton != null)
		{
			this.m_levelUpButton.Setup(this.m_charaType, this.m_pageRoot);
			this.m_levelUpButton.SetCallback(new MenuPlayerSetLevelUpButton.LevelUpCallback(this.LevelUppedCallback));
		}
		GameObject buttonRoot = GameObjectUtil.FindChildGameObject(this.m_pageRoot, "slot");
		if (buttonRoot != null)
		{
			List<GameObject> buttonList = null;
			for (;;)
			{
				buttonList = GameObjectUtil.FindChildGameObjects(buttonRoot, "ui_player_set_item_2_cell(Clone)");
				if (buttonList != null && buttonList.Count >= 10)
				{
					break;
				}
				yield return null;
			}
			if (buttonList != null)
			{
				for (int index = 0; index < buttonList.Count; index++)
				{
					GameObject buttonObjectRoot = buttonList[index];
					if (!(buttonObjectRoot == null))
					{
						MenuPlayerSetAbilityButton button = buttonObjectRoot.AddComponent<MenuPlayerSetAbilityButton>();
						if (!(button == null))
						{
							AbilityType abilityType = MenuPlayerSetUtil.AbilityLevelUpOrder[index];
							button.Setup(this.m_charaType, abilityType);
							this.m_abilityButton[(int)abilityType] = button;
						}
					}
				}
			}
		}
		AbilityType nextLevelUpAbility = MenuPlayerSetUtil.GetNextLevelUpAbility(this.m_charaType);
		MenuPlayerSetAbilityButton nextLevelUpButton = this.m_abilityButton[(int)nextLevelUpAbility];
		if (nextLevelUpButton != null)
		{
			nextLevelUpButton.SetActive(true);
		}
		if (ServerInterface.LoggedInServerInterface != null)
		{
			ServerPlayerState playerState = ServerInterface.PlayerState;
			if (playerState != null)
			{
				ServerCharacterState charaState = playerState.CharacterState(this.m_charaType);
				if (charaState != null && !charaState.IsUnlocked)
				{
					this.m_unlocked = base.gameObject.AddComponent<MenuPlayerSetUnlockedChara>();
					this.m_unlocked.Setup(this.m_charaType, this.m_pageRoot);
				}
			}
		}
		else
		{
			SaveDataManager save_data_manager = SaveDataManager.Instance;
			if (save_data_manager != null)
			{
				CharaData charaData = save_data_manager.CharaData;
				if (charaData.Status[(int)this.m_charaType] == 0)
				{
					this.m_unlocked = base.gameObject.AddComponent<MenuPlayerSetUnlockedChara>();
					this.m_unlocked.Setup(this.m_charaType, this.m_pageRoot);
				}
			}
		}
		GameObject playerSetRoot = MenuPlayerSetUtil.GetPlayerSetRoot();
		if (playerSetRoot != null)
		{
			this.m_blinderObject = GameObjectUtil.FindChildGameObject(playerSetRoot, "blinder");
			if (this.m_blinderObject != null)
			{
				UIButtonMessage buttonMessage = this.m_blinderObject.AddComponent<UIButtonMessage>();
				buttonMessage.target = base.gameObject;
				buttonMessage.functionName = "OnClickedSkipButton";
			}
		}
		this.m_isEndSetup = true;
		yield break;
	}

	// Token: 0x06002466 RID: 9318 RVA: 0x000DAC40 File Offset: 0x000D8E40
	public void OnSelected()
	{
		if (this.m_charaButton != null)
		{
			this.m_charaButton.OnSelected();
		}
	}

	// Token: 0x06002467 RID: 9319 RVA: 0x000DAC60 File Offset: 0x000D8E60
	public void UpdateRibbon()
	{
		if (this.m_charaButton != null)
		{
			this.m_charaButton.UpdateRibbon();
		}
	}

	// Token: 0x06002468 RID: 9320 RVA: 0x000DAC80 File Offset: 0x000D8E80
	private void LevelUppedCallback(AbilityType levelUppedAbility)
	{
		if (this.m_charaButton != null)
		{
			this.m_charaButton.LevelUp(new MenuPlayerSetCharaButton.AnimEndCallback(this.LevelUpAnimationEndCallback));
		}
		if (this.m_blinderObject != null)
		{
			this.m_blinderObject.SetActive(true);
		}
		MenuPlayerSetAbilityButton menuPlayerSetAbilityButton = this.m_abilityButton[(int)levelUppedAbility];
		if (menuPlayerSetAbilityButton != null)
		{
			menuPlayerSetAbilityButton.LevelUp(new MenuPlayerSetAbilityButton.AnimEndCallback(this.LevelUpAnimationEndCallback));
		}
		AbilityType nextLevelUpAbility = MenuPlayerSetUtil.GetNextLevelUpAbility(this.m_charaType);
		MenuPlayerSetAbilityButton menuPlayerSetAbilityButton2 = this.m_abilityButton[(int)nextLevelUpAbility];
		if (menuPlayerSetAbilityButton2 != null)
		{
			menuPlayerSetAbilityButton2.SetActive(true);
		}
		if (this.m_levelUpButton != null)
		{
			this.m_levelUpButton.InitCostLabel();
		}
	}

	// Token: 0x06002469 RID: 9321 RVA: 0x000DAD40 File Offset: 0x000D8F40
	private void OnClickedSkipButton()
	{
		foreach (MenuPlayerSetAbilityButton menuPlayerSetAbilityButton in this.m_abilityButton)
		{
			if (!(menuPlayerSetAbilityButton == null))
			{
				menuPlayerSetAbilityButton.SkipLevelUp();
			}
		}
		if (this.m_charaButton != null)
		{
			this.m_charaButton.SkipLevelUp();
		}
	}

	// Token: 0x0600246A RID: 9322 RVA: 0x000DADA0 File Offset: 0x000D8FA0
	private void LevelUpAnimationEndCallback()
	{
		if (this.m_charaButton != null && !this.m_charaButton.AnimEnd)
		{
			return;
		}
		if (this.m_abilityButton != null)
		{
			foreach (MenuPlayerSetAbilityButton menuPlayerSetAbilityButton in this.m_abilityButton)
			{
				if (!(menuPlayerSetAbilityButton == null))
				{
					if (!menuPlayerSetAbilityButton.AnimEnd)
					{
						return;
					}
				}
			}
		}
		if (this.m_blinderObject != null)
		{
			this.m_blinderObject.SetActive(false);
		}
		if (this.m_levelUpButton != null)
		{
			this.m_levelUpButton.OnLevelUpEnd();
		}
	}

	// Token: 0x040020DE RID: 8414
	private static readonly string[] HideObjectName = new string[]
	{
		"Btn_lv_up",
		"Btn_player_main",
		"_guide",
		"slot"
	};

	// Token: 0x040020DF RID: 8415
	private GameObject m_pageRoot;

	// Token: 0x040020E0 RID: 8416
	private CharaType m_charaType;

	// Token: 0x040020E1 RID: 8417
	private MenuPlayerSetUnlockedChara m_unlocked;

	// Token: 0x040020E2 RID: 8418
	private MenuPlayerSetCharaButton m_charaButton;

	// Token: 0x040020E3 RID: 8419
	private MenuPlayerSetLevelUpButton m_levelUpButton;

	// Token: 0x040020E4 RID: 8420
	private MenuPlayerSetAbilityButton[] m_abilityButton = new MenuPlayerSetAbilityButton[10];

	// Token: 0x040020E5 RID: 8421
	private GameObject m_blinderObject;

	// Token: 0x040020E6 RID: 8422
	private bool m_isEndSetup;
}
