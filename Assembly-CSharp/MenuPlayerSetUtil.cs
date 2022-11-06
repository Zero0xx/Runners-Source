using System;
using System.Collections.Generic;
using SaveData;
using UnityEngine;

// Token: 0x020004D8 RID: 1240
public class MenuPlayerSetUtil
{
	// Token: 0x060024BB RID: 9403 RVA: 0x000DBAF8 File Offset: 0x000D9CF8
	public static GameObject GetUIRoot()
	{
		return GameObject.Find("UI Root (2D)");
	}

	// Token: 0x060024BC RID: 9404 RVA: 0x000DBB04 File Offset: 0x000D9D04
	public static GameObject GetPlayerSetRoot()
	{
		GameObject uiroot = MenuPlayerSetUtil.GetUIRoot();
		return GameObjectUtil.FindChildGameObject(uiroot, "PlayerSet_2_UI");
	}

	// Token: 0x060024BD RID: 9405 RVA: 0x000DBB24 File Offset: 0x000D9D24
	public static int GetOpenedCharaCount()
	{
		int num = 0;
		for (int i = 0; i < 29; i++)
		{
			if (MenuPlayerSetUtil.IsOpenedCharacter((CharaType)i))
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x060024BE RID: 9406 RVA: 0x000DBB58 File Offset: 0x000D9D58
	public static bool IsOpenedCharacter(CharaType charaType)
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			ServerPlayerState playerState = ServerInterface.PlayerState;
			if (playerState != null)
			{
				ServerCharacterState serverCharacterState = playerState.CharacterState(charaType);
				if (serverCharacterState != null && serverCharacterState.Id > 0)
				{
					return true;
				}
			}
		}
		else if (charaType <= CharaType.KNUCKLES)
		{
			return true;
		}
		return false;
	}

	// Token: 0x060024BF RID: 9407 RVA: 0x000DBBB0 File Offset: 0x000D9DB0
	public static bool IsCharacterLevelMax(CharaType charaType)
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			ServerPlayerState playerState = ServerInterface.PlayerState;
			if (playerState != null)
			{
				ServerCharacterState serverCharacterState = playerState.CharacterState(charaType);
				if (serverCharacterState != null && serverCharacterState.Status == ServerCharacterState.CharacterStatus.MaxLevel)
				{
					return true;
				}
			}
		}
		else
		{
			int totalLevel = MenuPlayerSetUtil.GetTotalLevel(charaType);
			if (totalLevel >= 100)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060024C0 RID: 9408 RVA: 0x000DBC10 File Offset: 0x000D9E10
	public static CharaType GetCharaTypeFromPageIndex(int pageIndex)
	{
		CharaType result = CharaType.UNKNOWN;
		int num = 0;
		for (int i = 0; i < 29; i++)
		{
			CharaType charaType = (CharaType)i;
			if (MenuPlayerSetUtil.IsOpenedCharacter(charaType))
			{
				if (num == pageIndex)
				{
					result = charaType;
					break;
				}
				num++;
			}
		}
		return result;
	}

	// Token: 0x060024C1 RID: 9409 RVA: 0x000DBC54 File Offset: 0x000D9E54
	public static int GetPageIndexFromCharaType(CharaType charaType)
	{
		int num = 0;
		for (int i = 0; i < (int)charaType; i++)
		{
			if (MenuPlayerSetUtil.IsOpenedCharacter((CharaType)i))
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x060024C2 RID: 9410 RVA: 0x000DBC88 File Offset: 0x000D9E88
	public static void ActivateCharaPageObjects(GameObject parentGameObject, bool isActive)
	{
		if (parentGameObject == null)
		{
			return;
		}
		int childCount = parentGameObject.transform.childCount;
		bool flag = false;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = parentGameObject.transform.GetChild(i).gameObject;
			if (!(gameObject == null))
			{
				if (gameObject.name == "_guide")
				{
					flag = true;
				}
				for (int j = 1; j < 3; j++)
				{
					if (gameObject.name == "eff_set" + j)
					{
						gameObject.SetActive(false);
					}
				}
			}
		}
		if (!flag)
		{
			return;
		}
		string[] array = new string[]
		{
			"Btn_lv_up",
			"Btn_player_main",
			"slot",
			"deck_tab"
		};
		for (int k = 0; k < childCount; k++)
		{
			GameObject gameObject2 = parentGameObject.transform.GetChild(k).gameObject;
			if (!(gameObject2 == null))
			{
				if (gameObject2.name == "_guide")
				{
					gameObject2.SetActive(true);
				}
				else
				{
					bool flag2 = false;
					foreach (string text in array)
					{
						if (!string.IsNullOrEmpty(text))
						{
							if (gameObject2.name == text)
							{
								flag2 = true;
							}
						}
					}
					if (flag2)
					{
						global::Debug.Log("GetChild.Name" + k.ToString() + " = " + gameObject2.name);
						if (isActive)
						{
							if (gameObject2.activeInHierarchy)
							{
								goto IL_1D6;
							}
						}
						else if (!gameObject2.activeInHierarchy)
						{
							goto IL_1D6;
						}
						gameObject2.SetActive(isActive);
					}
				}
			}
			IL_1D6:;
		}
	}

	// Token: 0x060024C3 RID: 9411 RVA: 0x000DBE7C File Offset: 0x000DA07C
	public static AbilityType GetNextLevelUpAbility(CharaType charaType)
	{
		int num = 10000;
		for (int i = 0; i < 10; i++)
		{
			AbilityType abilityType = MenuPlayerSetUtil.AbilityLevelUpOrder[i];
			int level = MenuPlayerSetUtil.GetLevel(charaType, abilityType);
			if (num > level)
			{
				num = level;
			}
		}
		ImportAbilityTable instance = ImportAbilityTable.GetInstance();
		int num2 = 0;
		List<AbilityType> list = new List<AbilityType>();
		for (int j = 0; j < 10; j++)
		{
			bool flag = true;
			AbilityType abilityType2 = MenuPlayerSetUtil.AbilityLevelUpOrder[j];
			int level2 = MenuPlayerSetUtil.GetLevel(charaType, abilityType2);
			if (level2 - num >= MenuPlayerSetUtil.CandidateRange)
			{
				flag = false;
			}
			int maxLevel = instance.GetMaxLevel(abilityType2);
			if (level2 >= maxLevel)
			{
				flag = false;
			}
			if (flag)
			{
				list.Add(abilityType2);
				num2++;
			}
		}
		if (num2 <= 0)
		{
			return AbilityType.LASER;
		}
		int index = UnityEngine.Random.Range(0, num2);
		return list[index];
	}

	// Token: 0x060024C4 RID: 9412 RVA: 0x000DBF5C File Offset: 0x000DA15C
	public static int GetLevel(CharaType charaType, AbilityType abilityType)
	{
		CharaData charaData = SaveDataManager.Instance.CharaData;
		CharaAbility charaAbility = charaData.AbilityArray[(int)charaType];
		int value = (int)charaAbility.Ability[(int)abilityType];
		ImportAbilityTable instance = ImportAbilityTable.GetInstance();
		int maxLevel = instance.GetMaxLevel(abilityType);
		return Mathf.Clamp(value, 0, maxLevel);
	}

	// Token: 0x060024C5 RID: 9413 RVA: 0x000DBFA0 File Offset: 0x000DA1A0
	public static float GetLevelAbility(CharaType charaType, AbilityType abilityType, int level)
	{
		ImportAbilityTable instance = ImportAbilityTable.GetInstance();
		return instance.GetAbilityPotential(abilityType, level);
	}

	// Token: 0x060024C6 RID: 9414 RVA: 0x000DBFBC File Offset: 0x000DA1BC
	public static int GetTotalLevel(CharaType charaType)
	{
		int num = 0;
		for (int i = 0; i < 10; i++)
		{
			AbilityType abilityType = (AbilityType)i;
			num += MenuPlayerSetUtil.GetLevel(charaType, abilityType);
		}
		return num;
	}

	// Token: 0x060024C7 RID: 9415 RVA: 0x000DBFEC File Offset: 0x000DA1EC
	public static int GetAbilityCost(CharaType charaType)
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			ServerItem serverItem = new ServerItem(charaType);
			int id = (int)serverItem.id;
			ServerCampaignData campaignInSession = ServerInterface.CampaignState.GetCampaignInSession(Constants.Campaign.emType.CharacterUpgradeCost, id);
			if (campaignInSession != null)
			{
				return campaignInSession.iContent;
			}
			ServerCharacterState serverCharacterState = ServerInterface.PlayerState.CharacterState(charaType);
			if (serverCharacterState != null)
			{
				return serverCharacterState.Cost;
			}
		}
		return 1000;
	}

	// Token: 0x060024C8 RID: 9416 RVA: 0x000DC054 File Offset: 0x000DA254
	public static int GetCurrentExp(CharaType charaType)
	{
		int result = 0;
		ServerCharacterState serverCharacterState = ServerInterface.PlayerState.CharacterState(charaType);
		if (serverCharacterState != null)
		{
			result = serverCharacterState.Exp;
		}
		return result;
	}

	// Token: 0x060024C9 RID: 9417 RVA: 0x000DC080 File Offset: 0x000DA280
	public static float GetCurrentExpRatio(CharaType charaType)
	{
		int abilityCost = MenuPlayerSetUtil.GetAbilityCost(charaType);
		int currentExp = MenuPlayerSetUtil.GetCurrentExp(charaType);
		if (abilityCost == 0)
		{
			return 1f;
		}
		return (float)currentExp / (float)abilityCost;
	}

	// Token: 0x060024CA RID: 9418 RVA: 0x000DC0B0 File Offset: 0x000DA2B0
	public static int TransformServerAbilityID(AbilityType abilityType)
	{
		ServerItem serverItem = new ServerItem(abilityType);
		return (int)serverItem.id;
	}

	// Token: 0x060024CB RID: 9419 RVA: 0x000DC0CC File Offset: 0x000DA2CC
	public static int GetPlayableCharaCount()
	{
		int num = 0;
		for (int i = 0; i < 29; i++)
		{
			CharaData charaData = SaveDataManager.Instance.CharaData;
			if (charaData.Status[i] == 1)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x060024CC RID: 9420 RVA: 0x000DC10C File Offset: 0x000DA30C
	public static void SetMarkCharaPage(CharaType type)
	{
		MenuPlayerSetUtil.charaType = type;
	}

	// Token: 0x060024CD RID: 9421 RVA: 0x000DC114 File Offset: 0x000DA314
	public static void ResetMarkCharaPage()
	{
		MenuPlayerSetUtil.charaType = CharaType.UNKNOWN;
	}

	// Token: 0x060024CE RID: 9422 RVA: 0x000DC11C File Offset: 0x000DA31C
	public static bool IsMarkCharaPage()
	{
		return MenuPlayerSetUtil.charaType != CharaType.UNKNOWN;
	}

	// Token: 0x170004DF RID: 1247
	// (get) Token: 0x060024CF RID: 9423 RVA: 0x000DC12C File Offset: 0x000DA32C
	public static CharaType MarkCharaType
	{
		get
		{
			return MenuPlayerSetUtil.charaType;
		}
	}

	// Token: 0x040020FA RID: 8442
	public static float FadeInTime = 1f;

	// Token: 0x040020FB RID: 8443
	public static float FadeOutTime = 1f;

	// Token: 0x040020FC RID: 8444
	private static CharaType charaType = CharaType.UNKNOWN;

	// Token: 0x040020FD RID: 8445
	private static readonly int CandidateRange = 5;

	// Token: 0x040020FE RID: 8446
	public static readonly AbilityType[] AbilityLevelUpOrder = new AbilityType[]
	{
		AbilityType.INVINCIBLE,
		AbilityType.MAGNET,
		AbilityType.TRAMPOLINE,
		AbilityType.COMBO,
		AbilityType.LASER,
		AbilityType.DRILL,
		AbilityType.ASTEROID,
		AbilityType.DISTANCE_BONUS,
		AbilityType.RING_BONUS,
		AbilityType.ANIMAL
	};
}
