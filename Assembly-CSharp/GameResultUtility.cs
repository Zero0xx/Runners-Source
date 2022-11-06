using System;
using UnityEngine;

// Token: 0x02000388 RID: 904
public class GameResultUtility
{
	// Token: 0x06001AC0 RID: 6848 RVA: 0x0009E27C File Offset: 0x0009C47C
	public static Animation SearchAnimation(GameObject gameObject)
	{
		if (gameObject == null)
		{
			return null;
		}
		return gameObject.GetComponent<Animation>();
	}

	// Token: 0x06001AC1 RID: 6849 RVA: 0x0009E2A0 File Offset: 0x0009C4A0
	public static void SaveOldBestScore()
	{
		RankingUtil.RankingMode rankingMode = RankingUtil.RankingMode.ENDLESS;
		if (StageModeManager.Instance != null && StageModeManager.Instance.IsQuickMode())
		{
			rankingMode = RankingUtil.RankingMode.QUICK;
		}
		GameResultUtility.m_oldBestScore = RankingManager.GetMyHiScore(rankingMode, false);
		RankingManager.SavePlayerRankingData(rankingMode);
	}

	// Token: 0x06001AC2 RID: 6850 RVA: 0x0009E2E4 File Offset: 0x0009C4E4
	public static long GetOldBestScore()
	{
		return GameResultUtility.m_oldBestScore;
	}

	// Token: 0x06001AC3 RID: 6851 RVA: 0x0009E2EC File Offset: 0x0009C4EC
	public static long GetNewBestScore()
	{
		StageScoreManager instance = StageScoreManager.Instance;
		long result = 0L;
		if (instance != null)
		{
			result = instance.FinalScore;
		}
		return result;
	}

	// Token: 0x06001AC4 RID: 6852 RVA: 0x0009E318 File Offset: 0x0009C518
	public static void SetBossDestroyFlag(bool flag)
	{
		GameResultUtility.m_isBossDestroy = flag;
	}

	// Token: 0x06001AC5 RID: 6853 RVA: 0x0009E320 File Offset: 0x0009C520
	public static bool GetBossDestroyFlag()
	{
		return GameResultUtility.m_isBossDestroy;
	}

	// Token: 0x06001AC6 RID: 6854 RVA: 0x0009E328 File Offset: 0x0009C528
	public static void SetRaidbossBeatBonus(int value)
	{
		GameResultUtility.m_RaidbossBeatBonus = value;
	}

	// Token: 0x06001AC7 RID: 6855 RVA: 0x0009E330 File Offset: 0x0009C530
	public static int GetRaidbossBeatBonus()
	{
		return GameResultUtility.m_RaidbossBeatBonus;
	}

	// Token: 0x06001AC8 RID: 6856 RVA: 0x0009E338 File Offset: 0x0009C538
	public static void SetActiveBonus(GameObject parentAnimObject, string labelName, float score)
	{
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(parentAnimObject, labelName);
		if (uilabel == null)
		{
			return;
		}
		if (score > 0f)
		{
			uilabel.text = string.Format("{0:0.0}", score) + "%";
			parentAnimObject.gameObject.SetActive(true);
		}
	}

	// Token: 0x06001AC9 RID: 6857 RVA: 0x0009E394 File Offset: 0x0009C594
	public static void SetCharaTexture(UISprite uiTex, CharaType charaType)
	{
		if (uiTex == null)
		{
			return;
		}
		if (charaType != CharaType.UNKNOWN)
		{
			uiTex.spriteName = HudUtility.MakeCharaTextureName(charaType, HudUtility.TextureType.TYPE_S);
			uiTex.gameObject.SetActive(true);
		}
		else
		{
			uiTex.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001ACA RID: 6858 RVA: 0x0009E3E0 File Offset: 0x0009C5E0
	public static StageAbilityManager.BonusRate GetCampaignBonusRate(StageAbilityManager stageAbilityManager)
	{
		StageAbilityManager.BonusRate result = default(StageAbilityManager.BonusRate);
		if (stageAbilityManager != null)
		{
			result.score = 0f;
			result.ring = stageAbilityManager.CampaignValueRate;
			result.red_ring = 0f;
			result.animal = 0f;
			result.distance = 0f;
		}
		return result;
	}

	// Token: 0x06001ACB RID: 6859 RVA: 0x0009E440 File Offset: 0x0009C640
	public static string GetScoreFormat(long val)
	{
		return HudUtility.GetFormatNumString<long>(val);
	}

	// Token: 0x04001829 RID: 6185
	private static long m_oldBestScore;

	// Token: 0x0400182A RID: 6186
	private static bool m_isBossDestroy;

	// Token: 0x0400182B RID: 6187
	private static int m_RaidbossBeatBonus;

	// Token: 0x0400182C RID: 6188
	public static float ScoreInterpolateTime = 1.5f;
}
