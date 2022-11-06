using System;
using UnityEngine;

// Token: 0x02000466 RID: 1126
public class MainMenuUI : MonoBehaviour
{
	// Token: 0x060021C4 RID: 8644 RVA: 0x000CB50C File Offset: 0x000C970C
	public void UpdateView()
	{
		if (this.m_episodeBanner != null)
		{
			this.m_episodeBanner.UpdateView();
		}
		if (this.m_deckTab != null)
		{
			this.m_deckTab.UpdateView();
		}
		if (this.m_dailyBattleButton != null)
		{
			this.m_dailyBattleButton.UpdateView();
		}
		if (this.m_quickCampainBaner != null)
		{
			this.m_quickCampainBaner.UpdateView();
		}
		if (this.m_endlessCampainBaner != null)
		{
			this.m_endlessCampainBaner.UpdateView();
		}
	}

	// Token: 0x060021C5 RID: 8645 RVA: 0x000CB59C File Offset: 0x000C979C
	private void Start()
	{
		if (this.m_deckTab == null)
		{
			this.m_deckTab = base.gameObject.AddComponent<MainMenuDeckTab>();
		}
		if (this.m_episodeButton == null)
		{
			this.m_episodeButton = new HudEpisodeButton();
			bool isBossStage = MileageMapUtility.IsBossStage();
			int mileageStageIndex = MileageMapDataManager.Instance.MileageStageIndex;
			CharacterAttribute[] characterAttribute = MileageMapUtility.GetCharacterAttribute(mileageStageIndex);
			CharacterAttribute charaAttribute = characterAttribute[0];
			this.m_episodeButton.Initialize(base.gameObject, isBossStage, charaAttribute);
		}
		if (this.m_episodeBanner == null)
		{
			this.m_episodeBanner = new HudEpisodeBanner();
			this.m_episodeBanner.Initialize(base.gameObject);
		}
		if (this.m_quickCampainBaner == null)
		{
			this.m_quickCampainBaner = base.gameObject.AddComponent<HudCampaignBanner>();
			this.m_quickCampainBaner.Initialize(base.gameObject, true);
		}
		if (this.m_endlessCampainBaner == null)
		{
			this.m_endlessCampainBaner = base.gameObject.AddComponent<HudCampaignBanner>();
			this.m_endlessCampainBaner.Initialize(base.gameObject, false);
		}
		if (this.m_quickModeStagePicture == null)
		{
			this.m_quickModeStagePicture = new HudQuickModeStagePicture();
			this.m_quickModeStagePicture.Initialize(base.gameObject);
		}
		if (this.m_endlessModeRankingButton == null)
		{
			this.m_endlessModeRankingButton = new HudMainMenuRankingButton();
			this.m_endlessModeRankingButton.Intialize(base.gameObject, false);
		}
		if (this.m_quickModeRankingButton == null)
		{
			this.m_quickModeRankingButton = new HudMainMenuRankingButton();
			this.m_quickModeRankingButton.Intialize(base.gameObject, true);
		}
		if (this.m_dailyBattleButton == null)
		{
			this.m_dailyBattleButton = new HudDailyBattleButton();
			this.m_dailyBattleButton.Initialize(base.gameObject);
		}
		if (this.m_quickModeQuestionButton == null)
		{
			this.m_quickModeQuestionButton = base.gameObject.AddComponent<HudQuestionButton>();
			this.m_quickModeQuestionButton.Initialize(true);
		}
		if (this.m_endlessModeQuestionButton == null)
		{
			this.m_endlessModeQuestionButton = base.gameObject.AddComponent<HudQuestionButton>();
			this.m_endlessModeQuestionButton.Initialize(false);
		}
		HudMenuUtility.SendChangeHeaderText("ui_Header_MainPage2");
		BackKeyManager.AddMainMenuUI(base.gameObject);
	}

	// Token: 0x060021C6 RID: 8646 RVA: 0x000CB7B0 File Offset: 0x000C99B0
	private void Update()
	{
		if (this.m_endlessModeRankingButton != null)
		{
			this.m_endlessModeRankingButton.Update();
		}
		if (this.m_quickModeRankingButton != null)
		{
			this.m_quickModeRankingButton.Update();
		}
		if (this.m_dailyBattleButton != null)
		{
			this.m_dailyBattleButton.Update();
		}
	}

	// Token: 0x060021C7 RID: 8647 RVA: 0x000CB800 File Offset: 0x000C9A00
	private void OnUpdateQuickModeData()
	{
		if (this.m_quickModeStagePicture != null)
		{
			this.m_quickModeStagePicture.UpdateDisplay();
		}
		if (this.m_quickCampainBaner != null)
		{
			this.m_quickCampainBaner.UpdateView();
		}
		if (this.m_endlessCampainBaner != null)
		{
			this.m_endlessCampainBaner.UpdateView();
		}
	}

	// Token: 0x04001E79 RID: 7801
	private MainMenuDeckTab m_deckTab;

	// Token: 0x04001E7A RID: 7802
	private HudEpisodeButton m_episodeButton;

	// Token: 0x04001E7B RID: 7803
	private HudEpisodeBanner m_episodeBanner;

	// Token: 0x04001E7C RID: 7804
	private HudCampaignBanner m_quickCampainBaner;

	// Token: 0x04001E7D RID: 7805
	private HudCampaignBanner m_endlessCampainBaner;

	// Token: 0x04001E7E RID: 7806
	private HudQuickModeStagePicture m_quickModeStagePicture;

	// Token: 0x04001E7F RID: 7807
	private HudMainMenuRankingButton m_endlessModeRankingButton;

	// Token: 0x04001E80 RID: 7808
	private HudMainMenuRankingButton m_quickModeRankingButton;

	// Token: 0x04001E81 RID: 7809
	private HudDailyBattleButton m_dailyBattleButton;

	// Token: 0x04001E82 RID: 7810
	private HudQuestionButton m_quickModeQuestionButton;

	// Token: 0x04001E83 RID: 7811
	private HudQuestionButton m_endlessModeQuestionButton;
}
