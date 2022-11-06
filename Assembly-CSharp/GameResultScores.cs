using System;
using AnimationOrTween;
using Text;
using UnityEngine;

// Token: 0x0200037A RID: 890
public abstract class GameResultScores : MonoBehaviour
{
	// Token: 0x06001A3B RID: 6715 RVA: 0x00099ED8 File Offset: 0x000980D8
	private void Start()
	{
		this.m_fsm = (base.gameObject.AddComponent(typeof(TinyFsmBehavior)) as TinyFsmBehavior);
		if (this.m_fsm == null)
		{
			return;
		}
		TinyFsmBehavior.Description description = new TinyFsmBehavior.Description(this);
		description.initState = new TinyFsmState(new EventFunction(this.StateIdle));
		description.onFixedUpdate = true;
		this.m_fsm.SetUp(description);
		this.m_finished = false;
		this.m_isReplay = false;
	}

	// Token: 0x06001A3C RID: 6716 RVA: 0x00099F58 File Offset: 0x00098158
	public void Setup(GameObject resultObj, GameObject resultRoot, GameObject eventRoot)
	{
		this.DebugScoreLog();
		this.m_finished = false;
		this.m_isBossResult = false;
		if (resultObj == null)
		{
			return;
		}
		this.m_resultObj = resultObj;
		this.m_scoreManager = StageScoreManager.Instance;
		if (this.m_scoreManager == null)
		{
			return;
		}
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance == null)
		{
			return;
		}
		StageAbilityManager instance2 = StageAbilityManager.Instance;
		if (instance2 == null)
		{
			return;
		}
		if (resultRoot == null)
		{
			return;
		}
		this.m_resultRoot = resultRoot;
		if (eventRoot == null)
		{
			return;
		}
		this.m_eventRoot = eventRoot;
		if (StageModeManager.Instance != null)
		{
			this.m_isQuickMode = StageModeManager.Instance.IsQuickMode();
		}
		this.m_bonusEventAnim = this.m_eventRoot.GetComponent<Animation>();
		GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_resultRoot, "window_result");
		if (gameObject == null)
		{
			return;
		}
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(this.m_resultRoot, "window_bonus");
		if (gameObject2 == null)
		{
			return;
		}
		gameObject2.SetActive(true);
		this.m_DetailButtonLabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_resultRoot, "Lbl_word_bonus_details");
		this.m_DetailButtonLabel_Sh = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_resultRoot, "Lbl_word_bonus_details_sh");
		for (int i = 0; i < 3; i++)
		{
			this.m_bonusEventScores[i] = new BonusEventScore();
			this.m_bonusEventScores[i].obj = GameObjectUtil.FindChildGameObject(gameObject2, this.BonusCategoryData[i]);
			if (this.m_bonusEventScores[i].obj != null)
			{
				this.m_bonusEventScores[i].obj.SetActive(false);
			}
		}
		this.m_chaoCountBonusEventScores.obj = GameObjectUtil.FindChildGameObject(gameObject2, "chaototal_bonus");
		if (this.m_chaoCountBonusEventScores.obj != null)
		{
			this.m_chaoCountBonusEventScores.obj.SetActive(false);
		}
		this.m_RankBonusEventScores.obj = GameObjectUtil.FindChildGameObject(gameObject2, "player_bonus");
		if (this.m_RankBonusEventScores.obj != null)
		{
			this.m_RankBonusEventScores.obj.SetActive(false);
		}
		for (int j = 0; j < 7; j++)
		{
			UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, this.ResultObjParamTable[j].scoreLabel);
			if (uilabel != null)
			{
				this.m_scores[j] = new GameResultScoreInterporate();
				this.m_scores[j].Setup(uilabel);
				this.m_scores[j].SetLabelStartValue(0L);
			}
			int num = 0;
			GameObject obj = this.m_bonusEventScores[num].obj;
			if (obj != null)
			{
				this.m_bonusEventScores[num].bonusData[j] = new BonusData();
				this.m_bonusEventScores[num].bonusData[j].labelScore1 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(obj, this.ResultObjParamTable[j].chaoBonusLabel + "1");
				this.m_bonusEventScores[num].bonusData[j].uiTexture1 = GameObjectUtil.FindChildGameObjectComponent<UITexture>(obj, this.ResultObjParamTable[j].chaoBonusTexture + "1");
				this.m_bonusEventScores[num].bonusData[j].labelScore2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(obj, this.ResultObjParamTable[j].chaoBonusLabel + "2");
				this.m_bonusEventScores[num].bonusData[j].uiTexture2 = GameObjectUtil.FindChildGameObjectComponent<UITexture>(obj, this.ResultObjParamTable[j].chaoBonusTexture + "2");
				int num2 = (!this.IsBonusRate(instance2.MainChaoBonusValueRate, (ScoreDataType)j)) ? -1 : instance.PlayerData.MainChaoID;
				if (num2 >= 0)
				{
					this.SetActiveBonusEventScore1(num, j, true);
					HudUtility.SetChaoTexture(this.m_bonusEventScores[num].bonusData[j].uiTexture1, num2, true);
				}
				else
				{
					this.SetActiveBonusEventScore1(num, j, false);
				}
				int num3 = (!this.IsBonusRate(instance2.SubChaoBonusValueRate, (ScoreDataType)j)) ? -1 : instance.PlayerData.SubChaoID;
				if (num3 >= 0)
				{
					this.SetActiveBonusEventScore2(num, j, true);
					HudUtility.SetChaoTexture(this.m_bonusEventScores[num].bonusData[j].uiTexture2, num3, true);
				}
				else
				{
					this.SetActiveBonusEventScore2(num, j, false);
				}
			}
			GameObject obj2 = this.m_chaoCountBonusEventScores.obj;
			if (obj2 != null)
			{
				this.m_chaoCountBonusEventScores.bonusData[j] = new BonusData();
				this.m_chaoCountBonusEventScores.bonusData[j].labelScore1 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(obj2, this.ResultObjParamTable[j].chaoCountBonusLabel);
				this.SetActiveBonusEventScore(this.m_chaoCountBonusEventScores.bonusData[j].labelScore1, this.IsBonusRate(instance2.CountChaoBonusValueRate, (ScoreDataType)j));
			}
			int num4 = 1;
			GameObject obj3 = this.m_bonusEventScores[num4].obj;
			if (obj3 != null)
			{
				this.m_bonusEventScores[num4].bonusData[j] = new BonusData();
				this.m_bonusEventScores[num4].bonusData[j].labelScore1 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(obj3, this.ResultObjParamTable[j].campaignBonusLabel);
				this.SetActiveBonusEventScore(this.m_bonusEventScores[num4].bonusData[j].labelScore1, this.IsBonusRate(GameResultUtility.GetCampaignBonusRate(instance2), (ScoreDataType)j));
			}
			int num5 = 2;
			GameObject obj4 = this.m_bonusEventScores[num5].obj;
			if (obj4 != null)
			{
				this.m_bonusEventScores[num5].bonusData[j] = new BonusData();
				this.m_bonusEventScores[num5].bonusData[j].labelScore1 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(obj4, this.ResultObjParamTable[j].charaBonusLabel + "1");
				this.m_bonusEventScores[num5].bonusData[j].uiSprite1 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(obj4, this.ResultObjParamTable[j].charaBonusTexture + "1");
				this.m_bonusEventScores[num5].bonusData[j].labelScore2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(obj4, this.ResultObjParamTable[j].charaBonusLabel + "2");
				this.m_bonusEventScores[num5].bonusData[j].uiSprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(obj4, this.ResultObjParamTable[j].charaBonusTexture + "2");
				CharaType charaType = (!this.IsBonusRate(instance2.MainCharaBonusValueRate, (ScoreDataType)j)) ? CharaType.UNKNOWN : instance.PlayerData.MainChara;
				if (charaType != CharaType.UNKNOWN)
				{
					this.SetActiveBonusEventScore1(num5, j, true);
					GameResultUtility.SetCharaTexture(this.m_bonusEventScores[num5].bonusData[j].uiSprite1, charaType);
				}
				else
				{
					this.SetActiveBonusEventScore1(num5, j, false);
				}
				CharaType charaType2 = (!this.IsBonusRate(instance2.SubCharaBonusValueRate, (ScoreDataType)j)) ? CharaType.UNKNOWN : instance.PlayerData.SubChara;
				if (charaType2 != CharaType.UNKNOWN)
				{
					this.SetActiveBonusEventScore2(num5, j, true);
					GameResultUtility.SetCharaTexture(this.m_bonusEventScores[num5].bonusData[j].uiSprite2, charaType2);
				}
				else
				{
					this.SetActiveBonusEventScore2(num5, j, false);
				}
				GameObject obj5 = this.m_RankBonusEventScores.obj;
				if (obj5 != null)
				{
					this.m_RankBonusEventScores.bonusData[j] = new BonusData();
					this.m_RankBonusEventScores.bonusData[j].labelScore1 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(obj5, this.ResultObjParamTable[j].charaBonusRankScore);
					this.SetActiveBonusEventScore(this.m_RankBonusEventScores.bonusData[j].labelScore1, true);
					if (this.m_isQuickMode && this.m_RankBonusEventScores.bonusData[j].labelScore1 != null)
					{
						this.m_RankBonusEventScores.bonusData[j].labelScore1.gameObject.SetActive(false);
						GameObject gameObject3 = GameObjectUtil.FindChildGameObject(obj5, "player_rank_bonus_title");
						if (gameObject3 != null)
						{
							gameObject3.SetActive(false);
						}
					}
				}
			}
		}
		this.SetResultScore(this.m_scoreManager.CountData);
		for (int k = 0; k < 3; k++)
		{
			this.m_bonusEventInfos[k] = new BonusEventInfo();
		}
		if (this.m_bonusEventAnim != null)
		{
			int num6 = 0;
			this.m_bonusEventInfos[num6].obj = GameObjectUtil.FindChildGameObject(this.m_eventRoot, "chaobonus");
			this.m_bonusEventInfos[num6].animClipName = "ui_result_word_chaobonus_Anim";
			this.m_bonusEventInfos[num6].viewTime = this.m_bonusEventAnim[this.m_bonusEventInfos[num6].animClipName].length * 0.3f;
			this.m_bonusEventInfos[num6].valueText = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_bonusEventInfos[num6].obj, "Lbl_num_chaobonus_score");
			StageScoreManager.ResultData bonusCountMainChaoData = this.m_scoreManager.BonusCountMainChaoData;
			StageScoreManager.ResultData bonusCountSubChaoData = this.m_scoreManager.BonusCountSubChaoData;
			StageScoreManager.ResultData bonusCountChaoCountData = this.m_scoreManager.BonusCountChaoCountData;
			if (this.IsBonus(bonusCountMainChaoData, bonusCountSubChaoData, bonusCountChaoCountData))
			{
				this.SetupBonus(bonusCountMainChaoData, bonusCountSubChaoData, ref this.m_bonusEventScores[num6].bonusData);
				this.SetupBonus(bonusCountChaoCountData, null, ref this.m_chaoCountBonusEventScores.bonusData);
			}
			else
			{
				this.m_bonusEventInfos[num6].obj = null;
			}
			int num7 = 1;
			this.m_bonusEventInfos[num7].obj = GameObjectUtil.FindChildGameObject(this.m_eventRoot, "campaignbonus");
			this.m_bonusEventInfos[num7].animClipName = "ui_result_word_campaignbonus_Anim";
			this.m_bonusEventInfos[num7].viewTime = this.m_bonusEventAnim[this.m_bonusEventInfos[num7].animClipName].length * 0.3f;
			this.m_bonusEventInfos[num7].valueText = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_bonusEventInfos[num7].obj, "Lbl_num_campaignbonus_score");
			StageScoreManager.ResultData bonusCountCampaignData = this.m_scoreManager.BonusCountCampaignData;
			if (this.IsBonus(bonusCountCampaignData, null, null))
			{
				this.SetupBonus(bonusCountCampaignData, null, ref this.m_bonusEventScores[num7].bonusData);
			}
			else
			{
				this.m_bonusEventInfos[num7].obj = null;
			}
			int num8 = 2;
			this.m_bonusEventInfos[num8].obj = GameObjectUtil.FindChildGameObject(this.m_eventRoot, "playerbonus");
			this.m_bonusEventInfos[num8].animClipName = "ui_result_word_playerbonus_Anim";
			this.m_bonusEventInfos[num8].viewTime = this.m_bonusEventAnim[this.m_bonusEventInfos[num8].animClipName].length * 0.3f;
			this.m_bonusEventInfos[num8].valueText = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_bonusEventInfos[num8].obj, "Lbl_num_playerbonus_score");
			StageScoreManager.ResultData bonusCountMainCharaData = this.m_scoreManager.BonusCountMainCharaData;
			StageScoreManager.ResultData bonusCountSubCharaData = this.m_scoreManager.BonusCountSubCharaData;
			StageScoreManager.ResultData bonusCountRankData = this.m_scoreManager.BonusCountRankData;
			if (this.IsBonus(bonusCountMainCharaData, bonusCountSubCharaData, bonusCountRankData))
			{
				this.SetupBonus(bonusCountMainCharaData, bonusCountSubCharaData, ref this.m_bonusEventScores[num8].bonusData);
				this.SetupBonus(bonusCountRankData, null, ref this.m_RankBonusEventScores.bonusData);
			}
			else
			{
				this.m_bonusEventInfos[num8].obj = null;
			}
		}
		this.m_addScore = GameResultScores.Category.NONE;
		this.OnSetup(resultRoot);
	}

	// Token: 0x06001A3D RID: 6717 RVA: 0x0009AAA4 File Offset: 0x00098CA4
	public bool IsCampaignBonus()
	{
		return this.m_bonusEventInfos[1].obj != null;
	}

	// Token: 0x06001A3E RID: 6718 RVA: 0x0009AAC4 File Offset: 0x00098CC4
	private void SetResultScore(StageScoreManager.ResultData resultDataScore)
	{
		if (this.m_scores[0] != null)
		{
			this.m_scores[0].AddScore(resultDataScore.score);
		}
		if (this.m_scores[1] != null)
		{
			this.m_scores[1].AddScore(resultDataScore.ring);
		}
		if (this.m_scores[2] != null)
		{
			this.m_scores[2].AddScore(resultDataScore.red_ring);
		}
		if (this.m_scores[3] != null)
		{
			this.m_scores[3].AddScore(resultDataScore.animal);
		}
		if (this.m_scores[4] != null)
		{
			this.m_scores[4].AddScore(resultDataScore.distance);
		}
		if (EventManager.Instance != null && EventManager.Instance.IsRaidBossStage() && this.m_scores[6] != null)
		{
			this.m_scores[6].AddScore(resultDataScore.raid_boss_ring);
		}
	}

	// Token: 0x06001A3F RID: 6719 RVA: 0x0009ABB0 File Offset: 0x00098DB0
	private void SetActiveBonusEventScore1(int category, int scoreDataType, bool on)
	{
		if (this.m_bonusEventScores[category].bonusData[scoreDataType].labelScore1 != null)
		{
			this.m_bonusEventScores[category].bonusData[scoreDataType].labelScore1.gameObject.SetActive(on);
		}
		if (this.m_bonusEventScores[category].bonusData[scoreDataType].uiSprite1 != null)
		{
			this.m_bonusEventScores[category].bonusData[scoreDataType].uiSprite1.gameObject.SetActive(on);
		}
		if (this.m_bonusEventScores[category].bonusData[scoreDataType].uiTexture1 != null)
		{
			this.m_bonusEventScores[category].bonusData[scoreDataType].uiTexture1.gameObject.SetActive(on);
		}
	}

	// Token: 0x06001A40 RID: 6720 RVA: 0x0009AC78 File Offset: 0x00098E78
	private void SetActiveBonusEventScore2(int category, int scoreDataType, bool on)
	{
		if (this.m_bonusEventScores[category].bonusData[scoreDataType].labelScore2 != null)
		{
			this.m_bonusEventScores[category].bonusData[scoreDataType].labelScore2.gameObject.SetActive(on);
		}
		if (this.m_bonusEventScores[category].bonusData[scoreDataType].uiSprite2 != null)
		{
			this.m_bonusEventScores[category].bonusData[scoreDataType].uiSprite2.gameObject.SetActive(on);
		}
		if (this.m_bonusEventScores[category].bonusData[scoreDataType].uiTexture2 != null)
		{
			this.m_bonusEventScores[category].bonusData[scoreDataType].uiTexture2.gameObject.SetActive(on);
		}
	}

	// Token: 0x06001A41 RID: 6721 RVA: 0x0009AD40 File Offset: 0x00098F40
	private void SetActiveBonusEventScore(UILabel uiLavel, bool on)
	{
		if (uiLavel != null)
		{
			uiLavel.gameObject.SetActive(on);
		}
	}

	// Token: 0x06001A42 RID: 6722 RVA: 0x0009AD5C File Offset: 0x00098F5C
	private void SetupBonus(StageScoreManager.ResultData resultData1, StageScoreManager.ResultData resultData2, ref BonusData[] bonusScore)
	{
		if (bonusScore == null)
		{
			return;
		}
		if (resultData1 != null)
		{
			int num = 0;
			if (bonusScore[num] != null && bonusScore[num].labelScore1 != null)
			{
				bonusScore[num].labelScore1.text = GameResultUtility.GetScoreFormat(resultData1.score);
			}
			int num2 = 1;
			if (bonusScore[num2] != null && bonusScore[num2].labelScore1 != null)
			{
				bonusScore[num2].labelScore1.text = GameResultUtility.GetScoreFormat(resultData1.ring);
			}
			int num3 = 3;
			if (bonusScore[num3] != null && bonusScore[num3].labelScore1 != null)
			{
				bonusScore[num3].labelScore1.text = GameResultUtility.GetScoreFormat(resultData1.animal);
			}
			int num4 = 4;
			if (bonusScore[num4] != null && bonusScore[num4].labelScore1 != null)
			{
				bonusScore[num4].labelScore1.text = GameResultUtility.GetScoreFormat(resultData1.distance);
			}
			if (EventManager.Instance != null && EventManager.Instance.IsRaidBossStage())
			{
				int num5 = 6;
				if (bonusScore[num5] != null && bonusScore[num5].labelScore1 != null)
				{
					bonusScore[num5].labelScore1.text = GameResultUtility.GetScoreFormat(resultData1.raid_boss_ring);
				}
			}
		}
		if (resultData2 != null)
		{
			int num6 = 0;
			if (bonusScore[num6] != null && bonusScore[num6].labelScore2 != null)
			{
				bonusScore[num6].labelScore2.text = GameResultUtility.GetScoreFormat(resultData2.score);
			}
			int num7 = 1;
			if (bonusScore[num7] != null && bonusScore[num7].labelScore2 != null)
			{
				bonusScore[num7].labelScore2.text = GameResultUtility.GetScoreFormat(resultData2.ring);
			}
			int num8 = 3;
			if (bonusScore[num8] != null && bonusScore[num8].labelScore2 != null)
			{
				bonusScore[num8].labelScore2.text = GameResultUtility.GetScoreFormat(resultData2.animal);
			}
			int num9 = 4;
			if (bonusScore[num9] != null && bonusScore[num9].labelScore2 != null)
			{
				bonusScore[num9].labelScore2.text = GameResultUtility.GetScoreFormat(resultData2.distance);
			}
			if (EventManager.Instance != null && EventManager.Instance.IsRaidBossStage())
			{
				int num10 = 6;
				if (bonusScore[num10] != null && bonusScore[num10].labelScore2 != null)
				{
					bonusScore[num10].labelScore2.text = GameResultUtility.GetScoreFormat(resultData2.raid_boss_ring);
				}
			}
		}
	}

	// Token: 0x06001A43 RID: 6723 RVA: 0x0009B004 File Offset: 0x00099204
	private bool IsBonusRate(StageAbilityManager.BonusRate bonusRate, ScoreDataType scoreDataType)
	{
		switch (scoreDataType)
		{
		case ScoreDataType.SCORE:
			if (bonusRate.score > 0f)
			{
				return true;
			}
			break;
		case ScoreDataType.RING:
			if (bonusRate.ring > 0f)
			{
				return true;
			}
			break;
		case ScoreDataType.REDSTAR_RING:
			if (bonusRate.red_ring > 0f)
			{
				return true;
			}
			break;
		case ScoreDataType.ANIMAL:
			if (bonusRate.animal > 0f)
			{
				return true;
			}
			break;
		case ScoreDataType.DISTANCE:
			if (bonusRate.distance > 0f)
			{
				return true;
			}
			break;
		case ScoreDataType.RAIDBOSS_RING:
			if (bonusRate.raid_boss_ring > 0f)
			{
				return true;
			}
			break;
		}
		return false;
	}

	// Token: 0x06001A44 RID: 6724 RVA: 0x0009B0CC File Offset: 0x000992CC
	protected void SetBonusEventScoreActive(GameResultScores.Category category)
	{
		for (int i = 0; i < 3; i++)
		{
			if (this.m_bonusEventScores[i].obj != null)
			{
				this.m_bonusEventScores[i].obj.SetActive(category == (GameResultScores.Category)i);
			}
		}
		if (this.m_chaoCountBonusEventScores.obj != null)
		{
			this.m_chaoCountBonusEventScores.obj.SetActive(GameResultScores.Category.CHAO == category);
		}
		if (this.m_RankBonusEventScores.obj != null)
		{
			this.m_RankBonusEventScores.obj.SetActive(GameResultScores.Category.CHARA == category);
		}
		this.AddScore(category);
		if (this.m_scores[5] != null)
		{
			long bonusTotalScore = this.GetBonusTotalScore(category);
			this.m_scores[5].SetLabelStartValue(bonusTotalScore);
		}
	}

	// Token: 0x06001A45 RID: 6725 RVA: 0x0009B198 File Offset: 0x00099398
	private long GetBonusTotalScore(GameResultScores.Category category)
	{
		long result = 0L;
		switch (category)
		{
		case GameResultScores.Category.CHAO:
			result = this.m_scoreManager.ResultChaoBonusTotal;
			break;
		case GameResultScores.Category.CAMPAIGN:
			result = this.m_scoreManager.ResultCampaignBonusTotal;
			break;
		case GameResultScores.Category.CHARA:
			result = this.m_scoreManager.ResultPlayerBonusTotal;
			break;
		}
		return result;
	}

	// Token: 0x06001A46 RID: 6726 RVA: 0x0009B1F8 File Offset: 0x000993F8
	private void AddScore(GameResultScores.Category category)
	{
		switch (category)
		{
		case GameResultScores.Category.CHAO:
			if (this.m_addScore == GameResultScores.Category.NONE)
			{
				this.SetResultScore(this.m_scoreManager.BonusCountMainChaoData);
				this.SetResultScore(this.m_scoreManager.BonusCountSubChaoData);
				this.SetResultScore(this.m_scoreManager.BonusCountChaoCountData);
				this.m_addScore = GameResultScores.Category.CHAO;
			}
			break;
		case GameResultScores.Category.CAMPAIGN:
			if (this.m_addScore == GameResultScores.Category.CHAO)
			{
				this.SetResultScore(this.m_scoreManager.BonusCountCampaignData);
				this.m_addScore = GameResultScores.Category.CAMPAIGN;
			}
			break;
		case GameResultScores.Category.CHARA:
			if (this.m_addScore == GameResultScores.Category.CAMPAIGN)
			{
				this.SetResultScore(this.m_scoreManager.BonusCountMainCharaData);
				this.SetResultScore(this.m_scoreManager.BonusCountSubCharaData);
				this.SetResultScore(this.m_scoreManager.BonusCountRankData);
				this.m_addScore = GameResultScores.Category.CHARA;
			}
			break;
		}
	}

	// Token: 0x06001A47 RID: 6727 RVA: 0x0009B2DC File Offset: 0x000994DC
	public bool IsBonusEvent()
	{
		for (int i = 0; i < 3; i++)
		{
			if (this.m_bonusEventInfos[i].obj != null)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x170003DA RID: 986
	// (get) Token: 0x06001A48 RID: 6728 RVA: 0x0009B318 File Offset: 0x00099518
	public bool IsEnd
	{
		get
		{
			return this.m_finished;
		}
	}

	// Token: 0x06001A49 RID: 6729 RVA: 0x0009B320 File Offset: 0x00099520
	public void PlayStart()
	{
		TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(100);
		if (this.m_fsm != null)
		{
			this.m_fsm.Dispatch(signal);
		}
	}

	// Token: 0x06001A4A RID: 6730 RVA: 0x0009B354 File Offset: 0x00099554
	public void PlaySkip()
	{
		TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(101);
		if (this.m_fsm != null)
		{
			this.m_fsm.Dispatch(signal);
		}
	}

	// Token: 0x06001A4B RID: 6731 RVA: 0x0009B388 File Offset: 0x00099588
	public void SetActiveDetailsButton(bool on)
	{
		this.OnSetActiveDetailsButton(on);
	}

	// Token: 0x06001A4C RID: 6732 RVA: 0x0009B394 File Offset: 0x00099594
	public void AllSkip()
	{
		TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(102);
		if (this.m_fsm != null)
		{
			this.m_fsm.Dispatch(signal);
		}
	}

	// Token: 0x06001A4D RID: 6733 RVA: 0x0009B3C8 File Offset: 0x000995C8
	public void OnFinishScore()
	{
		this.OnFinish();
	}

	// Token: 0x06001A4E RID: 6734 RVA: 0x0009B3D0 File Offset: 0x000995D0
	private void OnStartBonusScore(GameResultScores.Category category)
	{
		GameObject obj = this.m_bonusEventInfos[(int)category].obj;
		if (this.m_isReplay)
		{
			if (obj != null)
			{
				obj.SetActive(false);
			}
			this.m_eventUpdateState = GameResultScores.EventUpdateState.Idle;
			GameResultScores.Category nextCategory = this.GetNextCategory(category);
			this.ReplaceDitailButtonLabel(nextCategory);
			if (this.m_bonusEventInfos[(int)category].obj != null)
			{
				this.SetBonusEventScoreActive(category);
			}
			else
			{
				this.AddScore(category);
			}
		}
		else if (obj != null && this.m_bonusEventAnim != null)
		{
			obj.SetActive(true);
			this.m_bonusEventAnim.Rewind(this.m_bonusEventInfos[(int)category].animClipName);
			if (this.m_isBossResult)
			{
				this.m_bonusEventInfos[(int)category].valueText.text = string.Empty;
			}
			else
			{
				this.m_bonusEventInfos[(int)category].valueText.text = this.GetBonusTotalScore(category).ToString();
			}
			ActiveAnimation.Play(this.m_bonusEventAnim, this.m_bonusEventInfos[(int)category].animClipName, Direction.Forward, false);
			SoundManager.SePlay("sys_bonus", "SE");
			this.m_eventUpdateState = GameResultScores.EventUpdateState.Start;
		}
		else
		{
			this.m_eventUpdateState = GameResultScores.EventUpdateState.Idle;
		}
	}

	// Token: 0x06001A4F RID: 6735 RVA: 0x0009B514 File Offset: 0x00099714
	private GameResultScores.Category GetNextCategory(GameResultScores.Category nowCategory)
	{
		GameResultScores.Category category = GameResultScores.Category.CHAO;
		if (nowCategory == GameResultScores.Category.CHAO)
		{
			category = GameResultScores.Category.CAMPAIGN;
		}
		else if (nowCategory == GameResultScores.Category.CAMPAIGN)
		{
			category = GameResultScores.Category.CHARA;
		}
		else if (nowCategory == GameResultScores.Category.CHARA)
		{
			category = GameResultScores.Category.NUM;
		}
		if (category != GameResultScores.Category.NUM && this.m_bonusEventInfos[(int)category].obj == null)
		{
			category = this.GetNextCategory(category);
		}
		return category;
	}

	// Token: 0x06001A50 RID: 6736 RVA: 0x0009B570 File Offset: 0x00099770
	private void ReplaceDitailButtonLabel(GameResultScores.Category category)
	{
		if (this.m_DetailButtonLabel != null)
		{
			this.m_DetailButtonLabel.text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Result", this.BonusCategoryButtonLabel[(int)category]).text;
		}
		if (this.m_DetailButtonLabel_Sh != null)
		{
			this.m_DetailButtonLabel_Sh.text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Result", this.BonusCategoryButtonLabel[(int)category]).text;
		}
	}

	// Token: 0x06001A51 RID: 6737 RVA: 0x0009B5E8 File Offset: 0x000997E8
	private void OnUpdateBonusScore(GameResultScores.Category category)
	{
		GameObject obj = this.m_bonusEventInfos[(int)category].obj;
		if (obj != null && this.m_bonusEventAnim != null)
		{
			GameResultScores.EventUpdateState eventUpdateState = this.m_eventUpdateState;
			if (eventUpdateState != GameResultScores.EventUpdateState.Start)
			{
				if (eventUpdateState == GameResultScores.EventUpdateState.Wait)
				{
					this.m_timer -= Time.deltaTime;
					if (this.m_timer < 0f)
					{
						this.m_eventUpdateState = GameResultScores.EventUpdateState.Idle;
					}
				}
			}
			else
			{
				float time = this.m_bonusEventAnim[this.m_bonusEventInfos[(int)category].animClipName].time;
				if (time > this.m_bonusEventInfos[(int)category].viewTime)
				{
					this.SetBonusEventScoreActive(category);
					this.m_timer = 2f;
					this.m_eventUpdateState = GameResultScores.EventUpdateState.Wait;
				}
			}
		}
	}

	// Token: 0x06001A52 RID: 6738 RVA: 0x0009B6B8 File Offset: 0x000998B8
	private void OnSkipBonusScore(GameResultScores.Category category)
	{
		this.AddScore(category);
	}

	// Token: 0x06001A53 RID: 6739 RVA: 0x0009B6C4 File Offset: 0x000998C4
	private void OnEndBonusScore(GameResultScores.Category category)
	{
		GameObject obj = this.m_bonusEventInfos[(int)category].obj;
		if (obj != null && this.m_bonusEventAnim != null)
		{
			this.SetBonusEventScoreActive(category);
			this.m_bonusEventAnim.Play();
			float length = this.m_bonusEventAnim[this.m_bonusEventInfos[(int)category].animClipName].length;
			this.m_bonusEventAnim[this.m_bonusEventInfos[(int)category].animClipName].time = length;
			this.m_bonusEventAnim.Sample();
			this.m_bonusEventAnim.Stop();
			if (this.m_isReplay)
			{
				obj.SetActive(false);
			}
		}
		else
		{
			this.AddScore(category);
		}
	}

	// Token: 0x06001A54 RID: 6740 RVA: 0x0009B780 File Offset: 0x00099980
	private bool IsEndBonusScore(GameResultScores.Category category)
	{
		return this.m_eventUpdateState == GameResultScores.EventUpdateState.Idle && (!(this.m_bonusEventInfos[(int)category].obj != null) || !this.m_isReplay);
	}

	// Token: 0x06001A55 RID: 6741 RVA: 0x0009B7B8 File Offset: 0x000999B8
	protected void SetEnableNextButton(bool enabled)
	{
		if (this.m_resultObj != null)
		{
			this.m_resultObj.SendMessage("OnSetEnableNextButton", enabled);
		}
	}

	// Token: 0x06001A56 RID: 6742 RVA: 0x0009B7E4 File Offset: 0x000999E4
	protected void SetEnableDetailsButton(bool enabled)
	{
		if (this.m_resultObj != null)
		{
			this.m_resultObj.SendMessage("OnSetEnableDetailsButton", enabled);
		}
	}

	// Token: 0x06001A57 RID: 6743 RVA: 0x0009B810 File Offset: 0x00099A10
	protected virtual void OnSetActiveDetailsButton(bool on)
	{
	}

	// Token: 0x06001A58 RID: 6744
	protected abstract bool IsBonus(StageScoreManager.ResultData data1, StageScoreManager.ResultData data2, StageScoreManager.ResultData data3);

	// Token: 0x06001A59 RID: 6745
	protected abstract void OnSetup(GameObject resultRoot);

	// Token: 0x06001A5A RID: 6746
	protected abstract void OnFinish();

	// Token: 0x06001A5B RID: 6747 RVA: 0x0009B814 File Offset: 0x00099A14
	protected virtual void OnStartMileageBonusScore()
	{
	}

	// Token: 0x06001A5C RID: 6748 RVA: 0x0009B818 File Offset: 0x00099A18
	protected virtual void OnUpdateMileageBonusScore()
	{
	}

	// Token: 0x06001A5D RID: 6749 RVA: 0x0009B81C File Offset: 0x00099A1C
	protected virtual void OnEndMileageBonusScore()
	{
	}

	// Token: 0x06001A5E RID: 6750 RVA: 0x0009B820 File Offset: 0x00099A20
	protected virtual void OnSkipMileageBonusScore()
	{
	}

	// Token: 0x06001A5F RID: 6751 RVA: 0x0009B824 File Offset: 0x00099A24
	protected virtual bool IsEndMileageBonusScore()
	{
		return true;
	}

	// Token: 0x06001A60 RID: 6752 RVA: 0x0009B828 File Offset: 0x00099A28
	protected virtual void OnStartFinished()
	{
	}

	// Token: 0x06001A61 RID: 6753 RVA: 0x0009B82C File Offset: 0x00099A2C
	protected virtual void OnUpdateFinished()
	{
	}

	// Token: 0x06001A62 RID: 6754 RVA: 0x0009B830 File Offset: 0x00099A30
	protected virtual void OnEndFinished()
	{
	}

	// Token: 0x06001A63 RID: 6755 RVA: 0x0009B834 File Offset: 0x00099A34
	protected virtual void OnSkipFinished()
	{
	}

	// Token: 0x06001A64 RID: 6756 RVA: 0x0009B838 File Offset: 0x00099A38
	protected virtual bool IsEndFinished()
	{
		return true;
	}

	// Token: 0x06001A65 RID: 6757 RVA: 0x0009B83C File Offset: 0x00099A3C
	protected virtual void OnStartBeginning()
	{
	}

	// Token: 0x06001A66 RID: 6758 RVA: 0x0009B840 File Offset: 0x00099A40
	protected virtual void OnUpdateBeginning()
	{
	}

	// Token: 0x06001A67 RID: 6759 RVA: 0x0009B844 File Offset: 0x00099A44
	protected virtual void OnEndBeginning()
	{
	}

	// Token: 0x06001A68 RID: 6760 RVA: 0x0009B848 File Offset: 0x00099A48
	protected virtual void OnSkipBeginning()
	{
	}

	// Token: 0x06001A69 RID: 6761 RVA: 0x0009B84C File Offset: 0x00099A4C
	protected virtual bool IsEndBeginning()
	{
		return true;
	}

	// Token: 0x06001A6A RID: 6762 RVA: 0x0009B850 File Offset: 0x00099A50
	protected virtual void OnScoreInAnimation(EventDelegate.Callback callback)
	{
	}

	// Token: 0x06001A6B RID: 6763 RVA: 0x0009B854 File Offset: 0x00099A54
	protected virtual void OnScoreOutAnimation(EventDelegate.Callback callback)
	{
	}

	// Token: 0x06001A6C RID: 6764 RVA: 0x0009B858 File Offset: 0x00099A58
	public void PlayScoreInAnimation(EventDelegate.Callback callback)
	{
		this.OnScoreInAnimation(callback);
	}

	// Token: 0x06001A6D RID: 6765 RVA: 0x0009B864 File Offset: 0x00099A64
	public void PlayScoreOutAnimation(EventDelegate.Callback callback)
	{
		this.OnScoreOutAnimation(callback);
	}

	// Token: 0x06001A6E RID: 6766 RVA: 0x0009B870 File Offset: 0x00099A70
	private TinyFsmState StateIdle(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		default:
			if (signal != 100)
			{
				return TinyFsmState.End();
			}
			this.m_skip = false;
			this.m_allSkip = false;
			this.m_category = GameResultScores.Category.CHAO;
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateBeginning)));
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
	}

	// Token: 0x06001A6F RID: 6767 RVA: 0x0009B8FC File Offset: 0x00099AFC
	private TinyFsmState StateBeginning(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.OnStartBeginning();
			return TinyFsmState.End();
		default:
			if (signal != 101 && signal != 102)
			{
				return TinyFsmState.End();
			}
			this.OnSkipBeginning();
			return TinyFsmState.End();
		case 4:
			this.OnUpdateBeginning();
			if (this.IsEndBeginning())
			{
				this.OnEndBeginning();
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateBonusUpdate)));
			}
			return TinyFsmState.End();
		}
	}

	// Token: 0x06001A70 RID: 6768 RVA: 0x0009B9A0 File Offset: 0x00099BA0
	private TinyFsmState StateBonusUpdate(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.m_skip = false;
			this.OnStartBonusScore(this.m_category);
			return TinyFsmState.End();
		default:
			if (signal == 101)
			{
				this.OnSkipBonusScore(this.m_category);
				this.m_skip = true;
				return TinyFsmState.End();
			}
			if (signal != 102)
			{
				return TinyFsmState.End();
			}
			this.OnSkipBonusScore(this.m_category);
			this.m_allSkip = true;
			return TinyFsmState.End();
		case 4:
			this.OnUpdateBonusScore(this.m_category);
			if (this.IsEndBonusScore(this.m_category) || this.m_skip || this.m_allSkip)
			{
				this.OnEndBonusScore(this.m_category);
				if (this.m_allSkip)
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateMileageBonusUpdate)));
				}
				else if (this.m_category == GameResultScores.Category.CHAO)
				{
					this.m_category = GameResultScores.Category.CAMPAIGN;
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateBonusUpdate)));
				}
				else if (this.m_category == GameResultScores.Category.CAMPAIGN)
				{
					this.m_category = GameResultScores.Category.CHARA;
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateBonusUpdate)));
				}
				else
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateMileageBonusUpdate)));
				}
			}
			return TinyFsmState.End();
		}
	}

	// Token: 0x06001A71 RID: 6769 RVA: 0x0009BB34 File Offset: 0x00099D34
	private TinyFsmState StateMileageBonusUpdate(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.AddScore(GameResultScores.Category.CHAO);
			this.AddScore(GameResultScores.Category.CAMPAIGN);
			this.AddScore(GameResultScores.Category.CHARA);
			this.m_skip = false;
			this.OnStartMileageBonusScore();
			return TinyFsmState.End();
		default:
			if (signal != 101 && signal != 102)
			{
				return TinyFsmState.End();
			}
			this.OnSkipMileageBonusScore();
			this.m_skip = true;
			this.m_allSkip = true;
			return TinyFsmState.End();
		case 4:
			this.OnUpdateMileageBonusScore();
			if (this.IsEndMileageBonusScore() || this.m_skip || this.m_allSkip)
			{
				if (this.m_allSkip)
				{
					this.OnSkipMileageBonusScore();
				}
				this.OnEndMileageBonusScore();
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateFinished)));
			}
			return TinyFsmState.End();
		}
	}

	// Token: 0x06001A72 RID: 6770 RVA: 0x0009BC28 File Offset: 0x00099E28
	private TinyFsmState StateFinished(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.OnStartFinished();
			return TinyFsmState.End();
		default:
			if (signal != 101 && signal != 102)
			{
				return TinyFsmState.End();
			}
			if (!this.m_finished)
			{
				this.OnSkipFinished();
			}
			return TinyFsmState.End();
		case 4:
			if (!this.m_finished)
			{
				this.OnUpdateFinished();
				if (this.IsEndFinished())
				{
					this.OnEndFinished();
					this.m_finished = true;
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateIdle)));
					this.m_isReplay = true;
					GameResultScores.Category nextCategory = this.GetNextCategory(GameResultScores.Category.NUM);
					this.ReplaceDitailButtonLabel(nextCategory);
				}
			}
			return TinyFsmState.End();
		}
	}

	// Token: 0x06001A73 RID: 6771 RVA: 0x0009BD00 File Offset: 0x00099F00
	private void DebugScoreLog()
	{
	}

	// Token: 0x06001A74 RID: 6772 RVA: 0x0009BD10 File Offset: 0x00099F10
	private void DebugScoreLogResultData(string msg, StageScoreManager.ResultData resultData)
	{
	}

	// Token: 0x040017B2 RID: 6066
	private bool m_debugInfo;

	// Token: 0x040017B3 RID: 6067
	protected StageScoreManager m_scoreManager;

	// Token: 0x040017B4 RID: 6068
	protected GameObject m_resultRoot;

	// Token: 0x040017B5 RID: 6069
	private ResultObjParam[] ResultObjParamTable = new ResultObjParam[]
	{
		new ResultObjParam("Lbl_score", "Lbl_chao_score", "img_chao_score", "Lbl_player_score", "img_player_score", string.Empty, "Lbl_chaototal_score", "Lbl_player_rank_score1"),
		new ResultObjParam("Lbl_ring", "Lbl_chao_ring", "img_chao_ring", "Lbl_player_ring", "img_player_ring", "Lbl_campaign_ring", string.Empty, string.Empty),
		new ResultObjParam("Lbl_rsring", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty),
		new ResultObjParam("Lbl_animal", "Lbl_chao_animal", "img_chao_animal", "Lbl_player_animal", "img_player_animal", string.Empty, string.Empty, string.Empty),
		new ResultObjParam("Lbl_distance", "Lbl_chao_distance", "img_chao_distance", "Lbl_player_distance", "img_player_distance", string.Empty, string.Empty, string.Empty),
		new ResultObjParam("Lbl_totalscore", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty),
		new ResultObjParam(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)
	};

	// Token: 0x040017B6 RID: 6070
	private string[] BonusCategoryData = new string[]
	{
		"chao_bonus",
		"campaign_bonus",
		"player_bonus"
	};

	// Token: 0x040017B7 RID: 6071
	private string[] BonusCategoryButtonLabel = new string[]
	{
		"ui_Lbl_word_chao_bonus",
		"ui_Lbl_word_campaign_bonus",
		"ui_Lbl_word_player_bonus",
		"ui_Lbl_word_bonus_details_button"
	};

	// Token: 0x040017B8 RID: 6072
	private TinyFsmBehavior m_fsm;

	// Token: 0x040017B9 RID: 6073
	private GameResultScores.Category m_category;

	// Token: 0x040017BA RID: 6074
	private GameResultScoreInterporate[] m_scores = new GameResultScoreInterporate[7];

	// Token: 0x040017BB RID: 6075
	private BonusEventScore[] m_bonusEventScores = new BonusEventScore[3];

	// Token: 0x040017BC RID: 6076
	private BonusEventScore m_chaoCountBonusEventScores = new BonusEventScore();

	// Token: 0x040017BD RID: 6077
	private BonusEventScore m_RankBonusEventScores = new BonusEventScore();

	// Token: 0x040017BE RID: 6078
	private BonusEventInfo[] m_bonusEventInfos = new BonusEventInfo[3];

	// Token: 0x040017BF RID: 6079
	private Animation m_bonusEventAnim;

	// Token: 0x040017C0 RID: 6080
	private GameObject m_eventRoot;

	// Token: 0x040017C1 RID: 6081
	private GameObject m_resultObj;

	// Token: 0x040017C2 RID: 6082
	private bool m_finished;

	// Token: 0x040017C3 RID: 6083
	private bool m_skip;

	// Token: 0x040017C4 RID: 6084
	private bool m_allSkip;

	// Token: 0x040017C5 RID: 6085
	protected bool m_isReplay;

	// Token: 0x040017C6 RID: 6086
	private float m_timer;

	// Token: 0x040017C7 RID: 6087
	private GameResultScores.EventUpdateState m_eventUpdateState;

	// Token: 0x040017C8 RID: 6088
	private GameResultScores.Category m_addScore;

	// Token: 0x040017C9 RID: 6089
	private UILabel m_DetailButtonLabel;

	// Token: 0x040017CA RID: 6090
	private UILabel m_DetailButtonLabel_Sh;

	// Token: 0x040017CB RID: 6091
	protected bool m_isBossResult;

	// Token: 0x040017CC RID: 6092
	private bool m_isQuickMode;

	// Token: 0x0200037B RID: 891
	protected enum Category
	{
		// Token: 0x040017CE RID: 6094
		CHAO,
		// Token: 0x040017CF RID: 6095
		CAMPAIGN,
		// Token: 0x040017D0 RID: 6096
		CHARA,
		// Token: 0x040017D1 RID: 6097
		NUM,
		// Token: 0x040017D2 RID: 6098
		NONE
	}

	// Token: 0x0200037C RID: 892
	private enum EventSignal
	{
		// Token: 0x040017D4 RID: 6100
		PLAY_START = 100,
		// Token: 0x040017D5 RID: 6101
		SKIP,
		// Token: 0x040017D6 RID: 6102
		ALL_SKIP
	}

	// Token: 0x0200037D RID: 893
	private enum EventUpdateState
	{
		// Token: 0x040017D8 RID: 6104
		Idle,
		// Token: 0x040017D9 RID: 6105
		Start,
		// Token: 0x040017DA RID: 6106
		Wait
	}
}
