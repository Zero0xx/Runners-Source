using System;
using UnityEngine;

// Token: 0x02000398 RID: 920
public class HudResultWindow : MonoBehaviour
{
	// Token: 0x06001B1C RID: 6940 RVA: 0x000A0C0C File Offset: 0x0009EE0C
	public void Setup(GameObject result, bool bossResult)
	{
		this.m_result = result;
		StageScoreManager instance = StageScoreManager.Instance;
		if (instance == null)
		{
			return;
		}
		SaveDataManager instance2 = SaveDataManager.Instance;
		if (instance2 == null)
		{
			return;
		}
		StageAbilityManager instance3 = StageAbilityManager.Instance;
		if (instance3 == null)
		{
			return;
		}
		this.SetScore(instance.BonusCountMainChaoData, instance3.MainChaoBonusValueRate, this.m_scoreInfos[0], bossResult);
		this.SetChaoTexture(this.m_chao1TexInfos, instance2.PlayerData.MainChaoID, instance3.MainChaoBonusValueRate, bossResult);
		this.SetScore(instance.BonusCountSubChaoData, instance3.SubChaoBonusValueRate, this.m_scoreInfos[1], bossResult);
		this.SetChaoTexture(this.m_chao2TexInfos, instance2.PlayerData.SubChaoID, instance3.SubChaoBonusValueRate, bossResult);
		this.SetScore(instance.BonusCountChaoCountData, instance3.CountChaoBonusValueRate, this.m_scoreInfos[2], bossResult);
		this.SetText(this.m_scoreInfos[2].m_ring, -1L);
		this.SetText(this.m_scoreInfos[2].m_animal, -1L);
		this.SetText(this.m_scoreInfos[2].m_distance, -1L);
		this.SetScore(instance.BonusCountCampaignData, GameResultUtility.GetCampaignBonusRate(instance3), this.m_scoreInfos[3], bossResult);
		this.SetText(this.m_scoreInfos[3].m_score, -1L);
		this.SetText(this.m_scoreInfos[3].m_animal, -1L);
		this.SetText(this.m_scoreInfos[3].m_distance, -1L);
		this.SetScore(instance.BonusCountMainCharaData, instance3.MainCharaBonusValueRate, this.m_scoreInfos[4], bossResult);
		this.SetCharaTexture(this.m_chara1TexInfos, instance2.PlayerData.MainChara, instance3.MainCharaBonusValueRate, bossResult);
		this.SetScore(instance.BonusCountSubCharaData, instance3.SubCharaBonusValueRate, this.m_scoreInfos[5], bossResult);
		this.SetCharaTexture(this.m_chara2TexInfos, instance2.PlayerData.SubChara, instance3.SubCharaBonusValueRate, bossResult);
		this.SetScore(instance.MileageBonusScoreData, instance3.MileageBonusScoreRate, this.m_scoreInfos[6], bossResult);
		if (bossResult)
		{
			this.SetText(this.m_scoreInfos[6].m_ring, -1L);
			this.SetText(this.m_totalScore, -1L);
		}
		else
		{
			this.SetText(this.m_totalScore, (instance3.MileageBonusScoreRate.final_score <= 0f) ? -1L : instance.MileageBonusScoreData.final_score);
		}
	}

	// Token: 0x06001B1D RID: 6941 RVA: 0x000A0E70 File Offset: 0x0009F070
	private void OnClickNoButton()
	{
		SoundManager.SePlay("sys_window_close", "SE");
		if (this.m_result != null)
		{
			this.m_result.SendMessage("OnClickDetailsEndButton");
		}
	}

	// Token: 0x06001B1E RID: 6942 RVA: 0x000A0EA4 File Offset: 0x0009F0A4
	private void SetScore(StageScoreManager.ResultData resultData, StageAbilityManager.BonusRate bonusRate, HudResultWindow.ScoreInfo scoreInfo, bool bossResult)
	{
		if (resultData == null)
		{
			return;
		}
		if (scoreInfo == null)
		{
			return;
		}
		this.SetText(scoreInfo.m_ring, (bonusRate.ring <= 0f) ? -1L : resultData.ring);
		if (bossResult)
		{
			this.SetText(scoreInfo.m_score, -1L);
			this.SetText(scoreInfo.m_animal, -1L);
			this.SetText(scoreInfo.m_distance, -1L);
		}
		else
		{
			this.SetText(scoreInfo.m_score, (bonusRate.score <= 0f) ? -1L : resultData.score);
			this.SetText(scoreInfo.m_animal, (bonusRate.animal <= 0f) ? -1L : resultData.animal);
			this.SetText(scoreInfo.m_distance, (bonusRate.distance <= 0f) ? -1L : resultData.distance);
		}
	}

	// Token: 0x06001B1F RID: 6943 RVA: 0x000A0FA0 File Offset: 0x0009F1A0
	private void SetText(UILabel label, long score)
	{
		if (label == null)
		{
			return;
		}
		if (score >= 0L)
		{
			label.text = GameResultUtility.GetScoreFormat(score);
			label.gameObject.SetActive(true);
		}
		else
		{
			label.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001B20 RID: 6944 RVA: 0x000A0FEC File Offset: 0x0009F1EC
	private void SetChaoTexture(HudResultWindow.TexInfo texInfo, int chaoId, StageAbilityManager.BonusRate bonusRate, bool bossResult)
	{
		HudUtility.SetChaoTexture(texInfo.m_ringTex, (bonusRate.ring <= 0f) ? -1 : chaoId, true);
		if (bossResult)
		{
			HudUtility.SetChaoTexture(texInfo.m_scoreTex, -1, true);
			HudUtility.SetChaoTexture(texInfo.m_animalTex, -1, true);
			HudUtility.SetChaoTexture(texInfo.m_distanceTex, -1, true);
		}
		else
		{
			HudUtility.SetChaoTexture(texInfo.m_scoreTex, (bonusRate.score <= 0f) ? -1 : chaoId, true);
			HudUtility.SetChaoTexture(texInfo.m_animalTex, (bonusRate.animal <= 0f) ? -1 : chaoId, true);
			HudUtility.SetChaoTexture(texInfo.m_distanceTex, (bonusRate.distance <= 0f) ? -1 : chaoId, true);
		}
	}

	// Token: 0x06001B21 RID: 6945 RVA: 0x000A10BC File Offset: 0x0009F2BC
	private void SetCharaTexture(HudResultWindow.SprInfo texInfo, CharaType charaType, StageAbilityManager.BonusRate bonusRate, bool bossResult)
	{
		GameResultUtility.SetCharaTexture(texInfo.m_ringTex, (bonusRate.ring <= 0f) ? CharaType.UNKNOWN : charaType);
		if (bossResult)
		{
			GameResultUtility.SetCharaTexture(texInfo.m_scoreTex, CharaType.UNKNOWN);
			GameResultUtility.SetCharaTexture(texInfo.m_animalTex, CharaType.UNKNOWN);
			GameResultUtility.SetCharaTexture(texInfo.m_distanceTex, CharaType.UNKNOWN);
		}
		else
		{
			GameResultUtility.SetCharaTexture(texInfo.m_scoreTex, (bonusRate.score <= 0f) ? CharaType.UNKNOWN : charaType);
			GameResultUtility.SetCharaTexture(texInfo.m_animalTex, (bonusRate.animal <= 0f) ? CharaType.UNKNOWN : charaType);
			GameResultUtility.SetCharaTexture(texInfo.m_distanceTex, (bonusRate.distance <= 0f) ? CharaType.UNKNOWN : charaType);
		}
	}

	// Token: 0x040018A5 RID: 6309
	[SerializeField]
	private HudResultWindow.ScoreInfo[] m_scoreInfos = new HudResultWindow.ScoreInfo[7];

	// Token: 0x040018A6 RID: 6310
	[SerializeField]
	private HudResultWindow.TexInfo m_chao1TexInfos = new HudResultWindow.TexInfo();

	// Token: 0x040018A7 RID: 6311
	[SerializeField]
	private HudResultWindow.TexInfo m_chao2TexInfos = new HudResultWindow.TexInfo();

	// Token: 0x040018A8 RID: 6312
	[SerializeField]
	private HudResultWindow.SprInfo m_chara1TexInfos = new HudResultWindow.SprInfo();

	// Token: 0x040018A9 RID: 6313
	[SerializeField]
	private HudResultWindow.SprInfo m_chara2TexInfos = new HudResultWindow.SprInfo();

	// Token: 0x040018AA RID: 6314
	[SerializeField]
	private UILabel m_totalScore;

	// Token: 0x040018AB RID: 6315
	private GameObject m_result;

	// Token: 0x02000399 RID: 921
	private enum Type
	{
		// Token: 0x040018AD RID: 6317
		CHAO_1,
		// Token: 0x040018AE RID: 6318
		CHAO_2,
		// Token: 0x040018AF RID: 6319
		CHAO_COUNT,
		// Token: 0x040018B0 RID: 6320
		CAMPAIGN,
		// Token: 0x040018B1 RID: 6321
		CHARA_1,
		// Token: 0x040018B2 RID: 6322
		CHARA_2,
		// Token: 0x040018B3 RID: 6323
		MILEAGE,
		// Token: 0x040018B4 RID: 6324
		NUM
	}

	// Token: 0x0200039A RID: 922
	[Serializable]
	private class ScoreInfo
	{
		// Token: 0x040018B5 RID: 6325
		[SerializeField]
		public UILabel m_score;

		// Token: 0x040018B6 RID: 6326
		[SerializeField]
		public UILabel m_ring;

		// Token: 0x040018B7 RID: 6327
		[SerializeField]
		public UILabel m_animal;

		// Token: 0x040018B8 RID: 6328
		[SerializeField]
		public UILabel m_distance;
	}

	// Token: 0x0200039B RID: 923
	[Serializable]
	private class TexInfo
	{
		// Token: 0x040018B9 RID: 6329
		[SerializeField]
		public UITexture m_scoreTex;

		// Token: 0x040018BA RID: 6330
		[SerializeField]
		public UITexture m_ringTex;

		// Token: 0x040018BB RID: 6331
		[SerializeField]
		public UITexture m_animalTex;

		// Token: 0x040018BC RID: 6332
		[SerializeField]
		public UITexture m_distanceTex;
	}

	// Token: 0x0200039C RID: 924
	[Serializable]
	private class SprInfo
	{
		// Token: 0x040018BD RID: 6333
		[SerializeField]
		public UISprite m_scoreTex;

		// Token: 0x040018BE RID: 6334
		[SerializeField]
		public UISprite m_ringTex;

		// Token: 0x040018BF RID: 6335
		[SerializeField]
		public UISprite m_animalTex;

		// Token: 0x040018C0 RID: 6336
		[SerializeField]
		public UISprite m_distanceTex;
	}
}
