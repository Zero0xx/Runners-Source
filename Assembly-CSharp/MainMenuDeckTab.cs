using System;
using System.Collections.Generic;
using Message;
using SaveData;
using UnityEngine;

// Token: 0x0200045C RID: 1116
public class MainMenuDeckTab : MonoBehaviour
{
	// Token: 0x06002195 RID: 8597 RVA: 0x000CA14C File Offset: 0x000C834C
	public void UpdateView()
	{
		this.m_currentDeckStock = DeckUtil.GetDeckCurrentStockIndex();
		this.SetupTabView();
	}

	// Token: 0x06002196 RID: 8598 RVA: 0x000CA160 File Offset: 0x000C8360
	private void ChaoSetLoad(int stock)
	{
		if (this.m_state == MainMenuDeckTab.State.DECK_CHANGING)
		{
			return;
		}
		this.m_currentDeckStock = stock;
		DeckUtil.SetDeckCurrentStockIndex(this.m_currentDeckStock);
		this.m_state = MainMenuDeckTab.State.DECK_CHANGING;
		CharaType charaType = CharaType.UNKNOWN;
		CharaType charaType2 = CharaType.UNKNOWN;
		int num = -1;
		int num2 = -1;
		DeckUtil.GetDeckData(this.m_currentDeckStock, ref charaType, ref charaType2, ref num, ref num2);
		CharaType charaType3 = CharaType.UNKNOWN;
		CharaType charaType4 = CharaType.UNKNOWN;
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null)
		{
			charaType3 = instance.PlayerData.MainChara;
			charaType4 = instance.PlayerData.SubChara;
		}
		bool flag = false;
		if (charaType != CharaType.UNKNOWN && charaType != charaType3)
		{
			flag = true;
		}
		else if (charaType2 != charaType4)
		{
			flag = true;
		}
		if (flag)
		{
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				int mainCharaId = -1;
				int subCharaId = -1;
				ServerCharacterState serverCharacterState = ServerInterface.PlayerState.CharacterState(charaType);
				ServerCharacterState serverCharacterState2 = ServerInterface.PlayerState.CharacterState(charaType2);
				if (serverCharacterState != null)
				{
					mainCharaId = serverCharacterState.Id;
				}
				if (serverCharacterState2 != null)
				{
					subCharaId = serverCharacterState2.Id;
				}
				loggedInServerInterface.RequestServerChangeCharacter(mainCharaId, subCharaId, base.gameObject);
				DeckUtil.UpdateCharacters(charaType, charaType2);
			}
			else
			{
				DeckUtil.UpdateCharacters(charaType, charaType2);
				this.ServerChangeCharacter_Succeeded(null);
			}
		}
		else
		{
			DeckUtil.UpdateCharacters(charaType, charaType2);
			this.ServerChangeCharacter_Succeeded(null);
		}
	}

	// Token: 0x06002197 RID: 8599 RVA: 0x000CA2A4 File Offset: 0x000C84A4
	private void SetupTabView()
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Anchor_5_MC");
		if (gameObject == null)
		{
			return;
		}
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "2_Character");
		if (gameObject2 == null)
		{
			return;
		}
		GameObject gameObject3 = GameObjectUtil.FindChildGameObject(gameObject2, "Deck_tab");
		if (gameObject3 == null)
		{
			return;
		}
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		list.Add("tab_1");
		list.Add("tab_2");
		list.Add("tab_3");
		list.Add("tab_4");
		list.Add("tab_5");
		list2.Add("OnClickTab1");
		list2.Add("OnClickTab2");
		list2.Add("OnClickTab3");
		list2.Add("OnClickTab4");
		list2.Add("OnClickTab5");
		this.m_deckColliderObject = GeneralUtil.SetToggleObject(base.gameObject, gameObject3, list2, list, this.m_currentDeckStock, true);
	}

	// Token: 0x06002198 RID: 8600 RVA: 0x000CA3A0 File Offset: 0x000C85A0
	private void OnClickTab1()
	{
		if (this.m_currentDeckStock != 0)
		{
			this.ChaoSetLoad(0);
			SoundManager.SePlay("sys_menu_decide", "SE");
		}
	}

	// Token: 0x06002199 RID: 8601 RVA: 0x000CA3D0 File Offset: 0x000C85D0
	private void OnClickTab2()
	{
		if (this.m_currentDeckStock != 1)
		{
			this.ChaoSetLoad(1);
			SoundManager.SePlay("sys_menu_decide", "SE");
		}
	}

	// Token: 0x0600219A RID: 8602 RVA: 0x000CA3F8 File Offset: 0x000C85F8
	private void OnClickTab3()
	{
		if (this.m_currentDeckStock != 2)
		{
			this.ChaoSetLoad(2);
			SoundManager.SePlay("sys_menu_decide", "SE");
		}
	}

	// Token: 0x0600219B RID: 8603 RVA: 0x000CA420 File Offset: 0x000C8620
	private void OnClickTab4()
	{
		if (this.m_currentDeckStock != 3)
		{
			this.ChaoSetLoad(3);
			SoundManager.SePlay("sys_menu_decide", "SE");
		}
	}

	// Token: 0x0600219C RID: 8604 RVA: 0x000CA448 File Offset: 0x000C8648
	private void OnClickTab5()
	{
		if (this.m_currentDeckStock != 4)
		{
			this.ChaoSetLoad(4);
			SoundManager.SePlay("sys_menu_decide", "SE");
		}
	}

	// Token: 0x0600219D RID: 8605 RVA: 0x000CA470 File Offset: 0x000C8670
	private void ServerChangeCharacter_Succeeded(MsgGetPlayerStateSucceed msg)
	{
		int num = -1;
		int num2 = -1;
		CharaType charaType = CharaType.SONIC;
		CharaType charaType2 = CharaType.TAILS;
		DeckUtil.GetDeckData(this.m_currentDeckStock, ref charaType, ref charaType2, ref num, ref num2);
		int num3 = -1;
		int num4 = -1;
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null)
		{
			num3 = instance.PlayerData.MainChaoID;
			num4 = instance.PlayerData.SubChaoID;
		}
		bool flag = false;
		if (num != num3)
		{
			flag = true;
		}
		else if (num2 != num4)
		{
			flag = true;
		}
		if (flag)
		{
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				loggedInServerInterface.RequestServerEquipChao((int)ServerItem.CreateFromChaoId(num).id, (int)ServerItem.CreateFromChaoId(num2).id, base.gameObject);
				DeckUtil.UpdateChaos(num, num2);
			}
			else
			{
				this.ServerEquipChao_Succeeded(null);
				DeckUtil.UpdateChaos(num, num2);
			}
		}
		else
		{
			DeckUtil.UpdateChaos(num, num2);
			this.ServerEquipChao_Succeeded(null);
		}
	}

	// Token: 0x0600219E RID: 8606 RVA: 0x000CA560 File Offset: 0x000C8760
	private void ServerEquipChao_Succeeded(MsgGetPlayerStateSucceed msg)
	{
		HudMenuUtility.SendMsgUpdateSaveDataDisplay();
		this.m_state = MainMenuDeckTab.State.IDLE;
	}

	// Token: 0x0600219F RID: 8607 RVA: 0x000CA570 File Offset: 0x000C8770
	private bool CheckExsitDeck()
	{
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				return systemdata.CheckExsitDeck();
			}
		}
		return false;
	}

	// Token: 0x060021A0 RID: 8608 RVA: 0x000CA5A4 File Offset: 0x000C87A4
	private void Start()
	{
		if (!this.CheckExsitDeck())
		{
			DeckUtil.SetFirstDeckData();
		}
		this.m_currentDeckStock = DeckUtil.GetDeckCurrentStockIndex();
		this.SetupTabView();
	}

	// Token: 0x060021A1 RID: 8609 RVA: 0x000CA5C8 File Offset: 0x000C87C8
	private void Update()
	{
		MainMenuDeckTab.State state = this.m_state;
		if (state != MainMenuDeckTab.State.IDLE)
		{
			if (state != MainMenuDeckTab.State.DECK_CHANGING)
			{
			}
		}
	}

	// Token: 0x04001E58 RID: 7768
	private int m_currentDeckStock;

	// Token: 0x04001E59 RID: 7769
	private GameObject m_deckColliderObject;

	// Token: 0x04001E5A RID: 7770
	private MainMenuDeckTab.State m_state;

	// Token: 0x0200045D RID: 1117
	private enum State
	{
		// Token: 0x04001E5C RID: 7772
		IDLE,
		// Token: 0x04001E5D RID: 7773
		DECK_CHANGING,
		// Token: 0x04001E5E RID: 7774
		NUM
	}
}
