using System;
using Message;
using Text;
using UnityEngine;

// Token: 0x02000414 RID: 1044
public class ui_mm_new_page_ad_banner : MonoBehaviour
{
	// Token: 0x06001F55 RID: 8021 RVA: 0x000B9CD0 File Offset: 0x000B7ED0
	private void Start()
	{
		base.enabled = false;
		UIButtonMessage component = base.gameObject.GetComponent<UIButtonMessage>();
		if (component != null)
		{
			component.functionName = "OnClickScroll";
		}
	}

	// Token: 0x06001F56 RID: 8022 RVA: 0x000B9D08 File Offset: 0x000B7F08
	public void Update()
	{
		if (this.m_easySnsFeed != null)
		{
			EasySnsFeed.Result result = this.m_easySnsFeed.Update();
			if (result == EasySnsFeed.Result.COMPLETED || result == EasySnsFeed.Result.FAILED)
			{
				this.m_easySnsFeed = null;
				base.enabled = false;
			}
		}
		if (this.m_infoWindow != null)
		{
			if (this.m_buttonPressFlag)
			{
				if (this.m_infoWindow.IsEnd() && this.m_easySnsFeed == null)
				{
					base.enabled = false;
				}
			}
			else if (this.m_infoWindow.IsButtonPress(InformationWindow.ButtonType.LEFT))
			{
				this.ClickLeftButton();
				this.m_buttonPressFlag = true;
			}
			else if (this.m_infoWindow.IsButtonPress(InformationWindow.ButtonType.RIGHT))
			{
				this.ClickRightButton();
				this.m_buttonPressFlag = true;
			}
			else if (this.m_infoWindow.IsButtonPress(InformationWindow.ButtonType.CLOSE))
			{
				this.ClickCloseButton();
				this.m_buttonPressFlag = true;
			}
		}
	}

	// Token: 0x06001F57 RID: 8023 RVA: 0x000B9DFC File Offset: 0x000B7FFC
	private void CreateEasySnsFeed()
	{
		if (this.m_bannerInfo != null)
		{
			this.m_easySnsFeed = new EasySnsFeed(base.gameObject, "Camera/Anchor_5_MC", TextUtility.GetCommonText("ItemRoulette", "feed_jackpot_caption"), this.m_bannerInfo.info.bodyText, null);
		}
	}

	// Token: 0x06001F58 RID: 8024 RVA: 0x000B9E4C File Offset: 0x000B804C
	private void RequestGetEventList()
	{
		if (EventManager.Instance != null)
		{
			if (EventManager.Instance.IsSetEventStateInfo)
			{
				this.RequestLoadEventResource();
			}
			else
			{
				ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
				if (loggedInServerInterface != null)
				{
					loggedInServerInterface.RequestServerGetEventReward(EventManager.Instance.Id, base.gameObject);
				}
			}
		}
	}

	// Token: 0x06001F59 RID: 8025 RVA: 0x000B9EAC File Offset: 0x000B80AC
	private void ClickLeftButton()
	{
		if (this.m_bannerInfo != null)
		{
			switch (this.m_bannerInfo.info.pattern)
			{
			case InformationWindow.ButtonPattern.FEED_BROWSER:
				Application.OpenURL(this.m_bannerInfo.info.url);
				break;
			case InformationWindow.ButtonPattern.FEED_ROULETTE:
				HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.CHAO_ROULETTE, false);
				break;
			case InformationWindow.ButtonPattern.FEED_SHOP:
				HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.REDSTAR_TO_SHOP, false);
				break;
			case InformationWindow.ButtonPattern.FEED_EVENT:
				HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.PLAY_EVENT, false);
				break;
			case InformationWindow.ButtonPattern.FEED_EVENT_LIST:
				this.RequestGetEventList();
				break;
			}
		}
	}

	// Token: 0x06001F5A RID: 8026 RVA: 0x000B9F44 File Offset: 0x000B8144
	private void ClickRightButton()
	{
		if (this.m_bannerInfo != null)
		{
			switch (this.m_bannerInfo.info.pattern)
			{
			case InformationWindow.ButtonPattern.FEED:
				this.CreateEasySnsFeed();
				break;
			case InformationWindow.ButtonPattern.SHOP_CANCEL:
				HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.REDSTAR_TO_SHOP, false);
				break;
			case InformationWindow.ButtonPattern.FEED_BROWSER:
				this.CreateEasySnsFeed();
				break;
			case InformationWindow.ButtonPattern.FEED_ROULETTE:
				this.CreateEasySnsFeed();
				break;
			case InformationWindow.ButtonPattern.FEED_SHOP:
				this.CreateEasySnsFeed();
				break;
			case InformationWindow.ButtonPattern.FEED_EVENT:
				this.CreateEasySnsFeed();
				break;
			case InformationWindow.ButtonPattern.BROWSER:
				Application.OpenURL(this.m_bannerInfo.info.url);
				break;
			case InformationWindow.ButtonPattern.ROULETTE:
				HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.CHAO_ROULETTE, false);
				break;
			case InformationWindow.ButtonPattern.SHOP:
				HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.REDSTAR_TO_SHOP, false);
				break;
			case InformationWindow.ButtonPattern.EVENT:
				HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.PLAY_EVENT, false);
				break;
			case InformationWindow.ButtonPattern.EVENT_LIST:
				this.RequestGetEventList();
				break;
			}
		}
	}

	// Token: 0x06001F5B RID: 8027 RVA: 0x000BA03C File Offset: 0x000B823C
	private void ClickCloseButton()
	{
	}

	// Token: 0x06001F5C RID: 8028 RVA: 0x000BA040 File Offset: 0x000B8240
	private void SaveInformation()
	{
		if (this.m_bannerInfo != null && this.m_bannerInfo.item != null && !ServerInterface.NoticeInfo.IsCheckedForMenuIcon(this.m_bannerInfo.item))
		{
			ServerInterface.NoticeInfo.m_isShowedNoticeInfo = true;
			ServerInterface.NoticeInfo.UpdateChecked(this.m_bannerInfo.item);
			if (this.m_badgeAlert != null)
			{
				this.m_badgeAlert.SetActive(false);
			}
		}
	}

	// Token: 0x06001F5D RID: 8029 RVA: 0x000BA0C0 File Offset: 0x000B82C0
	public void UpdateView(ui_mm_new_page_ad_banner.BannerInfo bannerinfo)
	{
		this.m_bannerInfo = bannerinfo;
		if (this.m_bannerInfo == null)
		{
			return;
		}
		switch (this.m_bannerInfo.info.rankingType)
		{
		case InformationWindow.RankingType.NON:
			this.SetInfoBanner();
			break;
		case InformationWindow.RankingType.WORLD:
			this.SetRankingBannerAll();
			break;
		case InformationWindow.RankingType.LEAGUE:
			this.SetEndlessRankingBannerLeague();
			break;
		case InformationWindow.RankingType.EVENT:
			this.SetInfoBanner();
			break;
		case InformationWindow.RankingType.QUICK_LEAGUE:
			this.SetQuickRankingBannerLeague();
			break;
		}
		if (this.m_bannerInfo != null && this.m_bannerInfo.item != null && !ServerInterface.NoticeInfo.IsCheckedForMenuIcon(this.m_bannerInfo.item) && this.m_badgeAlert != null)
		{
			this.m_badgeAlert.SetActive(true);
		}
		this.m_init = true;
	}

	// Token: 0x06001F5E RID: 8030 RVA: 0x000BA1A4 File Offset: 0x000B83A4
	private void SetInfoBanner()
	{
		if (this.m_bannerInfo != null && !string.IsNullOrEmpty(this.m_bannerInfo.info.imageId))
		{
			InformationImageManager.Instance.Load(this.m_bannerInfo.info.imageId, true, new Action<Texture2D>(this.OnLoadCallback));
		}
	}

	// Token: 0x06001F5F RID: 8031 RVA: 0x000BA200 File Offset: 0x000B8400
	private bool SetRankingBannerAll()
	{
		bool result = false;
		if (this.m_uiTexture != null)
		{
			string name = "ui_tex_ranking_all";
			GameObject gameObject = GameObject.Find(name);
			if (gameObject != null)
			{
				AssetBundleTexture component = gameObject.GetComponent<AssetBundleTexture>();
				this.m_uiTexture.mainTexture = component.m_tex;
			}
		}
		return result;
	}

	// Token: 0x06001F60 RID: 8032 RVA: 0x000BA254 File Offset: 0x000B8454
	private bool SetEndlessRankingBannerLeague()
	{
		bool result = false;
		if (this.m_uiTexture != null)
		{
			string name = "ui_tex_ranking_rival_endless_" + TextUtility.GetSuffixe();
			GameObject gameObject = GameObject.Find(name);
			if (gameObject != null)
			{
				AssetBundleTexture component = gameObject.GetComponent<AssetBundleTexture>();
				this.m_uiTexture.mainTexture = component.m_tex;
			}
		}
		return result;
	}

	// Token: 0x06001F61 RID: 8033 RVA: 0x000BA2B0 File Offset: 0x000B84B0
	private void SetQuickRankingBannerLeague()
	{
		if (this.m_uiTexture != null)
		{
			string name = "ui_tex_ranking_rival_quick_" + TextUtility.GetSuffixe();
			GameObject gameObject = GameObject.Find(name);
			if (gameObject != null)
			{
				AssetBundleTexture component = gameObject.GetComponent<AssetBundleTexture>();
				this.m_uiTexture.mainTexture = component.m_tex;
			}
		}
	}

	// Token: 0x06001F62 RID: 8034 RVA: 0x000BA30C File Offset: 0x000B850C
	public void OnLoadCallback(Texture2D texture)
	{
		if (this.m_uiTexture != null && texture != null)
		{
			this.m_uiTexture.mainTexture = texture;
		}
	}

	// Token: 0x06001F63 RID: 8035 RVA: 0x000BA338 File Offset: 0x000B8538
	public void UpdateTexture(Texture texture)
	{
		if (texture != null)
		{
			this.m_uiTexture.mainTexture = texture;
		}
	}

	// Token: 0x06001F64 RID: 8036 RVA: 0x000BA354 File Offset: 0x000B8554
	private void OnClickScroll()
	{
		if (!this.m_init)
		{
			return;
		}
		GameObject cameraUIObject = HudMenuUtility.GetCameraUIObject();
		if (cameraUIObject != null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(cameraUIObject, "NewsWindow");
			if (gameObject != null)
			{
				SoundManager.SePlay("sys_menu_decide", "SE");
				if (this.m_bannerInfo != null)
				{
					base.enabled = true;
					this.m_buttonPressFlag = false;
					this.m_infoWindow = base.gameObject.GetComponent<InformationWindow>();
					if (this.m_infoWindow == null)
					{
						this.m_infoWindow = base.gameObject.AddComponent<InformationWindow>();
					}
					if (this.m_infoWindow != null)
					{
						this.m_infoWindow.Create(this.m_bannerInfo.info, gameObject);
					}
				}
			}
		}
		this.SaveInformation();
	}

	// Token: 0x06001F65 RID: 8037 RVA: 0x000BA424 File Offset: 0x000B8624
	public void OnEndChildPage()
	{
	}

	// Token: 0x06001F66 RID: 8038 RVA: 0x000BA428 File Offset: 0x000B8628
	public void OnButtonEventCallBack()
	{
		this.CreateEeventList();
	}

	// Token: 0x06001F67 RID: 8039 RVA: 0x000BA430 File Offset: 0x000B8630
	private void CreateEeventList()
	{
		if (EventManager.Instance != null)
		{
			switch (EventManager.Instance.Type)
			{
			case EventManager.EventType.SPECIAL_STAGE:
				EventRewardWindow.Create(EventManager.Instance.SpecialStageInfo);
				break;
			case EventManager.EventType.RAID_BOSS:
				EventRewardWindow.Create(EventManager.Instance.RaidBossInfo);
				break;
			case EventManager.EventType.COLLECT_OBJECT:
				EventRewardWindow.Create(EventManager.Instance.EtcEventInfo);
				break;
			}
		}
	}

	// Token: 0x06001F68 RID: 8040 RVA: 0x000BA4B0 File Offset: 0x000B86B0
	private void RequestLoadEventResource()
	{
		GameObjectUtil.SendMessageFindGameObject("MainMenuButtonEvent", "OnMenuEventClicked", base.gameObject, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001F69 RID: 8041 RVA: 0x000BA4CC File Offset: 0x000B86CC
	private void ServerGetEventReward_Succeeded(MsgGetEventRewardSucceed msg)
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetEventState(EventManager.Instance.Id, base.gameObject);
		}
	}

	// Token: 0x06001F6A RID: 8042 RVA: 0x000BA504 File Offset: 0x000B8704
	private void ServerGetEventState_Succeeded(MsgGetEventStateSucceed msg)
	{
		if (EventManager.Instance != null)
		{
			EventManager.Instance.SetEventInfo();
		}
		this.RequestLoadEventResource();
	}

	// Token: 0x04001C6C RID: 7276
	[SerializeField]
	private UITexture m_uiTexture;

	// Token: 0x04001C6D RID: 7277
	[SerializeField]
	private GameObject m_badgeAlert;

	// Token: 0x04001C6E RID: 7278
	private EasySnsFeed m_easySnsFeed;

	// Token: 0x04001C6F RID: 7279
	private ui_mm_new_page_ad_banner.BannerInfo m_bannerInfo;

	// Token: 0x04001C70 RID: 7280
	private InformationWindow m_infoWindow;

	// Token: 0x04001C71 RID: 7281
	private bool m_buttonPressFlag;

	// Token: 0x04001C72 RID: 7282
	private bool m_init;

	// Token: 0x02000415 RID: 1045
	public class BannerInfo
	{
		// Token: 0x04001C73 RID: 7283
		public int index;

		// Token: 0x04001C74 RID: 7284
		public int type;

		// Token: 0x04001C75 RID: 7285
		public NetNoticeItem item;

		// Token: 0x04001C76 RID: 7286
		public InformationWindow.Information info;
	}
}
