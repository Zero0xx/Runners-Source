using System;
using System.Collections.Generic;
using SaveData;
using UnityEngine;

// Token: 0x02000A42 RID: 2626
public class DeckUtil
{
	// Token: 0x06004607 RID: 17927 RVA: 0x0016CAEC File Offset: 0x0016ACEC
	public static void SetFirstDeckData()
	{
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				SaveDataManager instance2 = SaveDataManager.Instance;
				if (instance2 != null)
				{
					systemdata.SaveDeckData(0, instance2.PlayerData.MainChara, instance2.PlayerData.SubChara, instance2.PlayerData.MainChaoID, instance2.PlayerData.SubChaoID);
				}
			}
		}
	}

	// Token: 0x06004608 RID: 17928 RVA: 0x0016CB60 File Offset: 0x0016AD60
	public static int GetDeckCurrentStockIndex()
	{
		int deckCurrentStockIndex = DeckUtil.s_chaoSetCurrentStockIndex;
		if (deckCurrentStockIndex < 0)
		{
			SystemSaveManager instance = SystemSaveManager.Instance;
			if (instance != null)
			{
				SystemData systemdata = instance.GetSystemdata();
				if (systemdata != null)
				{
					SaveDataManager instance2 = SaveDataManager.Instance;
					if (instance2 != null)
					{
						deckCurrentStockIndex = systemdata.GetDeckCurrentStockIndex();
						DeckUtil.s_chaoSetCurrentStockIndex = deckCurrentStockIndex;
					}
				}
			}
		}
		return deckCurrentStockIndex;
	}

	// Token: 0x06004609 RID: 17929 RVA: 0x0016CBBC File Offset: 0x0016ADBC
	public static void SetDeckCurrentStockIndex(int index)
	{
		if (index < 0 || index >= 6)
		{
			return;
		}
		DeckUtil.s_chaoSetCurrentStockIndex = index;
	}

	// Token: 0x0600460A RID: 17930 RVA: 0x0016CBD4 File Offset: 0x0016ADD4
	public static void CharaSetSaveAuto(int currentMainId, int currentSubId)
	{
		int deckCurrentStockIndex = DeckUtil.GetDeckCurrentStockIndex();
		DeckUtil.CharaSetSave(deckCurrentStockIndex, currentMainId, currentSubId);
	}

	// Token: 0x0600460B RID: 17931 RVA: 0x0016CBF0 File Offset: 0x0016ADF0
	private static bool CharaSetSave(int stock, int currentMainId, int currentSubId)
	{
		if (stock < 0 || stock >= 6)
		{
			return false;
		}
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				SaveDataManager instance2 = SaveDataManager.Instance;
				PlayerData playerData = instance2.PlayerData;
				ServerItem serverItem = new ServerItem((ServerItem.Id)currentMainId);
				playerData.MainChara = serverItem.charaType;
				PlayerData playerData2 = instance2.PlayerData;
				ServerItem serverItem2 = new ServerItem((ServerItem.Id)currentSubId);
				playerData2.SubChara = serverItem2.charaType;
				instance2.SavePlayerData();
				systemdata.SaveDeckDataChara(stock);
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600460C RID: 17932 RVA: 0x0016CC78 File Offset: 0x0016AE78
	public static void ChaoSetSaveAuto(int currentMainId, int currentSubId)
	{
		int deckCurrentStockIndex = DeckUtil.GetDeckCurrentStockIndex();
		DeckUtil.ChaoSetSave(deckCurrentStockIndex, currentMainId - 400000, currentSubId - 400000);
	}

	// Token: 0x0600460D RID: 17933 RVA: 0x0016CCA0 File Offset: 0x0016AEA0
	private static bool ChaoSetSave(int stock, int currentMainId, int currentSubId)
	{
		if (stock < 0 || stock >= 6)
		{
			return false;
		}
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				SaveDataManager instance2 = SaveDataManager.Instance;
				instance2.PlayerData.MainChaoID = currentMainId;
				instance2.PlayerData.SubChaoID = currentSubId;
				instance2.SavePlayerData();
				systemdata.SaveDeckDataChao(stock);
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600460E RID: 17934 RVA: 0x0016CD0C File Offset: 0x0016AF0C
	public static bool DeckReset(int stock)
	{
		if (stock < 0 || stock >= 6)
		{
			return false;
		}
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				systemdata.RestDeckData(stock);
			}
		}
		return true;
	}

	// Token: 0x0600460F RID: 17935 RVA: 0x0016CD50 File Offset: 0x0016AF50
	public static bool DeckSetSave(int stock, CharaType currentMainCharaType, CharaType currentSubCharaType, int currentMainId, int currentSubId)
	{
		if (stock < 0 || stock >= 6)
		{
			return false;
		}
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				systemdata.SaveDeckData(stock, currentMainCharaType, currentSubCharaType, currentMainId, currentSubId);
			}
		}
		return true;
	}

	// Token: 0x06004610 RID: 17936 RVA: 0x0016CD9C File Offset: 0x0016AF9C
	public static bool DeckSetLoad(int stock, GameObject callbackObject)
	{
		if (stock < 0 || stock >= 6)
		{
			return false;
		}
		SaveDataManager instance = SaveDataManager.Instance;
		CharaType mainChara = instance.PlayerData.MainChara;
		CharaType subChara = instance.PlayerData.SubChara;
		int mainChaoID = instance.PlayerData.MainChaoID;
		int subChaoID = instance.PlayerData.SubChaoID;
		bool flag = DeckUtil.DeckSetLoad(stock, ref mainChara, ref subChara, ref mainChaoID, ref subChaoID, callbackObject);
		if (flag)
		{
			instance.PlayerData.MainChara = mainChara;
			instance.PlayerData.SubChara = subChara;
			instance.PlayerData.MainChaoID = mainChaoID;
			instance.PlayerData.SubChaoID = subChaoID;
		}
		return flag;
	}

	// Token: 0x06004611 RID: 17937 RVA: 0x0016CE3C File Offset: 0x0016B03C
	public static bool DeckSetLoad(int stock, ref CharaType currentMainCharaType, ref CharaType currentSubCharaType, ref int currentMainId, ref int currentSubId, GameObject callbackObject = null)
	{
		if (stock < 0 || stock >= 6)
		{
			return false;
		}
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				SaveDataManager instance2 = SaveDataManager.Instance;
				CharaType charaType = CharaType.SONIC;
				CharaType charaType2 = CharaType.UNKNOWN;
				int num = -1;
				int num2 = -1;
				systemdata.GetDeckData(stock, out charaType, out charaType2, out num, out num2);
				CharaType mainChara = instance2.PlayerData.MainChara;
				CharaType subChara = instance2.PlayerData.SubChara;
				int mainChaoID = instance2.PlayerData.MainChaoID;
				int subChaoID = instance2.PlayerData.SubChaoID;
				currentMainCharaType = charaType;
				currentSubCharaType = charaType2;
				currentMainId = num;
				currentSubId = num2;
				ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
				if (num != mainChaoID || num2 != subChaoID)
				{
					if (loggedInServerInterface != null && callbackObject != null)
					{
						loggedInServerInterface.RequestServerEquipChao((int)ServerItem.CreateFromChaoId(num).id, (int)ServerItem.CreateFromChaoId(num2).id, callbackObject);
					}
				}
				else if (callbackObject != null)
				{
					callbackObject.SendMessage("ServerEquipChao_Dummy", SendMessageOptions.DontRequireReceiver);
				}
				if (charaType != mainChara || charaType2 != subChara)
				{
					if (loggedInServerInterface != null && callbackObject != null)
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
						loggedInServerInterface.RequestServerChangeCharacter(mainCharaId, subCharaId, callbackObject);
					}
				}
				else if (callbackObject != null)
				{
					callbackObject.SendMessage("RequestServerChangeCharacter_Dummy", SendMessageOptions.DontRequireReceiver);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004612 RID: 17938 RVA: 0x0016CFF4 File Offset: 0x0016B1F4
	public static void GetDeckData(int stock, ref CharaType currentMainCharaType, ref CharaType currentSubCharaType, ref int currentMainId, ref int currentSubId)
	{
		if (stock < 0 || stock >= 6)
		{
			return;
		}
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance == null)
		{
			return;
		}
		SystemData systemdata = instance.GetSystemdata();
		if (systemdata == null)
		{
			return;
		}
		CharaType charaType = CharaType.SONIC;
		CharaType charaType2 = CharaType.UNKNOWN;
		int num = -1;
		int num2 = -1;
		systemdata.GetDeckData(stock, out charaType, out charaType2, out num, out num2);
		currentMainCharaType = charaType;
		currentSubCharaType = charaType2;
		currentMainId = num;
		currentSubId = num2;
	}

	// Token: 0x06004613 RID: 17939 RVA: 0x0016D05C File Offset: 0x0016B25C
	public static void GetDeckData(int stock, ref CharaType currentMainCharaType, ref CharaType currentSubCharaType)
	{
		if (stock < 0 || stock >= 6)
		{
			return;
		}
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance == null)
		{
			return;
		}
		SystemData systemdata = instance.GetSystemdata();
		if (systemdata == null)
		{
			return;
		}
		CharaType charaType = CharaType.SONIC;
		CharaType charaType2 = CharaType.UNKNOWN;
		int num = -1;
		int num2 = -1;
		systemdata.GetDeckData(stock, out charaType, out charaType2, out num, out num2);
		currentMainCharaType = charaType;
		currentSubCharaType = charaType2;
	}

	// Token: 0x06004614 RID: 17940 RVA: 0x0016D0B8 File Offset: 0x0016B2B8
	public static void UpdateCharacters(CharaType mainChara, CharaType subChara)
	{
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance == null)
		{
			return;
		}
		instance.PlayerData.MainChara = mainChara;
		instance.PlayerData.SubChara = subChara;
	}

	// Token: 0x06004615 RID: 17941 RVA: 0x0016D0F0 File Offset: 0x0016B2F0
	public static void UpdateChaos(int mainChaoId, int subChaoId)
	{
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance == null)
		{
			return;
		}
		instance.PlayerData.MainChaoID = mainChaoId;
		instance.PlayerData.SubChaoID = subChaoId;
	}

	// Token: 0x06004616 RID: 17942 RVA: 0x0016D128 File Offset: 0x0016B328
	public static bool IsChaoSetSave(int stock)
	{
		bool result = true;
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				result = systemdata.IsSaveDeckData(stock);
			}
		}
		return result;
	}

	// Token: 0x06004617 RID: 17943 RVA: 0x0016D160 File Offset: 0x0016B360
	public static List<DeckUtil.DeckSet> GetDeckList()
	{
		List<DeckUtil.DeckSet> list = new List<DeckUtil.DeckSet>();
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				for (int i = 0; i < 6; i++)
				{
					DeckUtil.DeckSet deckSet = new DeckUtil.DeckSet();
					systemdata.GetDeckData(i, out deckSet.charaMain, out deckSet.charaSub, out deckSet.chaoMain, out deckSet.chaoSub);
					list.Add(deckSet);
				}
			}
		}
		int deckCurrentStockIndex = DeckUtil.GetDeckCurrentStockIndex();
		if (list.Count > deckCurrentStockIndex)
		{
			list[deckCurrentStockIndex].isCurrentSelect = true;
		}
		return list;
	}

	// Token: 0x04003AB9 RID: 15033
	private static int s_chaoSetCurrentStockIndex = -1;

	// Token: 0x02000A43 RID: 2627
	public class DeckSet
	{
		// Token: 0x04003ABA RID: 15034
		public CharaType charaMain;

		// Token: 0x04003ABB RID: 15035
		public CharaType charaSub = CharaType.UNKNOWN;

		// Token: 0x04003ABC RID: 15036
		public int chaoMain = -1;

		// Token: 0x04003ABD RID: 15037
		public int chaoSub = -1;

		// Token: 0x04003ABE RID: 15038
		public bool isCurrentSelect;
	}
}
