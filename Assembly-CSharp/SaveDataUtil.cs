using System;
using SaveData;
using UnityEngine;

// Token: 0x020002B2 RID: 690
public class SaveDataUtil : MonoBehaviour
{
	// Token: 0x0600136C RID: 4972 RVA: 0x00069D64 File Offset: 0x00067F64
	public static void ReflctResultsData()
	{
		StageScoreManager instance = StageScoreManager.Instance;
		if (instance != null)
		{
			uint num = (uint)instance.FinalScore;
			SaveDataManager instance2 = SaveDataManager.Instance;
			if (instance2 != null)
			{
				PlayerData playerData = instance2.PlayerData;
				if (playerData.BestScore < (long)((ulong)num))
				{
					playerData.BestScore = (long)((ulong)num);
				}
			}
		}
	}

	// Token: 0x0600136D RID: 4973 RVA: 0x00069DBC File Offset: 0x00067FBC
	public static uint GetCharaLevel(CharaType chara_type)
	{
		if (CharaType.SONIC <= chara_type && chara_type < CharaType.NUM)
		{
			SaveDataManager instance = SaveDataManager.Instance;
			if (instance != null)
			{
				CharaAbility[] abilityArray = instance.CharaData.AbilityArray;
				if (abilityArray != null)
				{
					CharaAbility charaAbility = abilityArray[(int)chara_type];
					if (charaAbility != null)
					{
						return charaAbility.GetTotalLevel();
					}
				}
			}
		}
		return 0U;
	}

	// Token: 0x0600136E RID: 4974 RVA: 0x00069E10 File Offset: 0x00068010
	public static void SetBeforeDailyMissionSaveData(ServerPlayerState serverPlayerState)
	{
		if (SaveDataManager.Instance != null)
		{
			PlayerData playerData = SaveDataManager.Instance.PlayerData;
			if (playerData != null)
			{
				DailyMissionData beforeDailyMissionData = playerData.BeforeDailyMissionData;
				playerData.DailyMission.CopyTo(beforeDailyMissionData);
			}
		}
	}
}
