using System;
using UnityEngine;

// Token: 0x02000552 RID: 1362
public class InformationWindow : WindowBase
{
	// Token: 0x06002A25 RID: 10789 RVA: 0x001051BC File Offset: 0x001033BC
	public InformationWindow()
	{
		string[,] array = new string[18, 3];
		array[0, 0] = "pattern_0";
		array[0, 1] = "Btn_1_ok";
		array[1, 0] = "pattern_0";
		array[1, 1] = "Btn_1_ok";
		array[2, 0] = "pattern_0";
		array[2, 1] = "Btn_2_post";
		array[3, 0] = "pattern_2";
		array[3, 1] = "Btn_cancel";
		array[3, 2] = "Btn_shop";
		array[4, 0] = "pattern_3";
		array[4, 1] = "Btn_1_browser";
		array[4, 2] = "Btn_post";
		array[5, 0] = "pattern_3";
		array[5, 1] = "Btn_3_roulette";
		array[5, 2] = "Btn_post";
		array[6, 0] = "pattern_3";
		array[6, 1] = "Btn_4_shop";
		array[6, 2] = "Btn_post";
		array[7, 0] = "pattern_3";
		array[7, 1] = "Btn_5_event";
		array[7, 2] = "Btn_post";
		array[8, 0] = "pattern_3";
		array[8, 1] = "Btn_6_event_list";
		array[8, 2] = "Btn_post";
		array[9, 0] = "pattern_0";
		array[9, 1] = "Btn_3_browser";
		array[10, 0] = "pattern_0";
		array[10, 1] = "Btn_5_roulette";
		array[11, 0] = "pattern_0";
		array[11, 1] = "Btn_6_shop";
		array[12, 0] = "pattern_0";
		array[12, 1] = "Btn_7_event";
		array[13, 0] = "pattern_0";
		array[13, 1] = "Btn_8_event_list";
		array[14, 0] = "pattern_0";
		array[14, 1] = "Btn_1_ok";
		array[15, 0] = "pattern_0";
		array[15, 1] = "Btn_1_ok";
		array[16, 0] = "pattern_0";
		array[16, 1] = "Btn_1_ok";
		array[17, 0] = "pattern_0";
		array[17, 1] = "Btn_1_ok";
		this.ButtonName = array;
		this.CallbackFuncName = new string[]
		{
			"OnClickLeftButton",
			"OnClickRightButton",
			"OnClickCloseButton"
		};
		this.m_pressedFlag = new bool[3];
		base..ctor();
	}

	// Token: 0x06002A26 RID: 10790 RVA: 0x00105444 File Offset: 0x00103644
	public bool IsButtonPress(InformationWindow.ButtonType type)
	{
		return this.m_pressedFlag[(int)type];
	}

	// Token: 0x06002A27 RID: 10791 RVA: 0x00105458 File Offset: 0x00103658
	public bool IsEnd()
	{
		return this.m_endFlag;
	}

	// Token: 0x06002A28 RID: 10792 RVA: 0x00105460 File Offset: 0x00103660
	private void ResetScrollBar()
	{
		if (this.m_prefab != null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_prefab, "textView");
			if (gameObject != null)
			{
				UIScrollBar uiscrollBar = GameObjectUtil.FindChildGameObjectComponent<UIScrollBar>(gameObject, "Scroll_Bar");
				if (uiscrollBar != null)
				{
					uiscrollBar.value = 0f;
				}
			}
		}
	}

	// Token: 0x06002A29 RID: 10793 RVA: 0x001054C0 File Offset: 0x001036C0
	private void SetRootObjActive(string rootName, bool activeFlag)
	{
		if (this.m_prefab != null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_prefab, rootName);
			if (gameObject != null)
			{
				gameObject.SetActive(activeFlag);
			}
		}
	}

	// Token: 0x06002A2A RID: 10794 RVA: 0x00105500 File Offset: 0x00103700
	private void SetObjActive(GameObject obj, bool activeFlag)
	{
		if (obj != null)
		{
			obj.SetActive(activeFlag);
		}
	}

	// Token: 0x06002A2B RID: 10795 RVA: 0x00105518 File Offset: 0x00103718
	private void SetClickBtnCallBack(GameObject buttonRoot, string objectName, string callbackFuncName)
	{
		if (buttonRoot != null && !string.IsNullOrEmpty(objectName))
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(buttonRoot, objectName);
			if (gameObject != null)
			{
				UIPlayAnimation component = gameObject.GetComponent<UIPlayAnimation>();
				if (component != null)
				{
					component.onFinished.Clear();
					EventDelegate.Add(component.onFinished, new EventDelegate.Callback(this.OnFinishedAnimationCallback), false);
				}
				UIButtonMessage component2 = gameObject.GetComponent<UIButtonMessage>();
				if (component2 != null)
				{
					component2.target = base.gameObject;
					component2.functionName = callbackFuncName;
				}
			}
		}
	}

	// Token: 0x06002A2C RID: 10796 RVA: 0x001055B8 File Offset: 0x001037B8
	private void SetActiveBtn(GameObject buttonRoot, string objectName)
	{
		if (buttonRoot != null && !string.IsNullOrEmpty(objectName))
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(buttonRoot, objectName);
			if (gameObject != null)
			{
				gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x06002A2D RID: 10797 RVA: 0x001055FC File Offset: 0x001037FC
	public void SetTexture(Texture2D texture)
	{
		if (texture != null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_prefab, "img_ad_tex");
			if (gameObject != null)
			{
				UITexture component = gameObject.GetComponent<UITexture>();
				if (component != null)
				{
					gameObject.SetActive(true);
					component.enabled = true;
					component.height = texture.height;
					component.mainTexture = texture;
				}
			}
		}
	}

	// Token: 0x06002A2E RID: 10798 RVA: 0x00105668 File Offset: 0x00103868
	public void Create(InformationWindow.Information info, GameObject newsWindowObj)
	{
		this.m_info = info;
		this.m_endFlag = false;
		for (int i = 0; i < this.m_pressedFlag.Length; i++)
		{
			this.m_pressedFlag[i] = false;
		}
		switch (info.rankingType)
		{
		case InformationWindow.RankingType.NON:
			this.m_created = true;
			this.CreateNormal(info, newsWindowObj);
			break;
		case InformationWindow.RankingType.WORLD:
			this.CreateWorldRanking(info);
			break;
		case InformationWindow.RankingType.LEAGUE:
			this.CreateLeagueRanking(info, false);
			break;
		case InformationWindow.RankingType.EVENT:
			this.CreateEvent(info);
			break;
		case InformationWindow.RankingType.QUICK_LEAGUE:
			this.CreateLeagueRanking(info, true);
			break;
		}
	}

	// Token: 0x17000585 RID: 1413
	// (get) Token: 0x06002A2F RID: 10799 RVA: 0x00105714 File Offset: 0x00103914
	public UITexture bannerTexture
	{
		get
		{
			return base.gameObject.GetComponentInChildren<UITexture>();
		}
	}

	// Token: 0x06002A30 RID: 10800 RVA: 0x00105730 File Offset: 0x00103930
	private void CreateLeagueRanking(InformationWindow.Information info, bool quick)
	{
		this.m_rankingResultLeague = RankingResultLeague.Create(info.bodyText, quick);
	}

	// Token: 0x06002A31 RID: 10801 RVA: 0x00105748 File Offset: 0x00103948
	private void CreateWorldRanking(InformationWindow.Information info)
	{
		this.m_rankingResultWorld = RankingResultWorldRanking.GetResultWorldRanking();
		if (this.m_rankingResultWorld != null)
		{
			this.m_rankingResultWorld.Setup(RankingResultWorldRanking.ResultType.WORLD_RANKING, info.bodyText);
			this.m_rankingResultWorld.PlayStart();
		}
	}

	// Token: 0x06002A32 RID: 10802 RVA: 0x00105790 File Offset: 0x00103990
	private void CreateEvent(InformationWindow.Information info)
	{
		this.m_eventRankingResult = RankingResultWorldRanking.GetResultWorldRanking();
		if (this.m_eventRankingResult != null)
		{
			this.m_eventRankingResult.Setup(RankingResultWorldRanking.ResultType.EVENT_RANKING, info.bodyText);
			this.m_eventRankingResult.PlayStart();
		}
	}

	// Token: 0x06002A33 RID: 10803 RVA: 0x001057D8 File Offset: 0x001039D8
	private void CreateNormal(InformationWindow.Information info, GameObject newsWindowObj)
	{
		if (this.m_prefab == null)
		{
			this.m_prefab = newsWindowObj;
		}
		if (this.m_prefab != null)
		{
			this.SetCallBack();
			this.m_prefab.SetActive(false);
			this.SetRootObjActive("pattern_0", false);
			this.SetRootObjActive("pattern_1", false);
			this.SetRootObjActive("pattern_2", false);
			this.SetRootObjActive("pattern_3", false);
			this.SetRootObjActive("pattern_close", true);
			this.SetRootObjActive("textView", true);
			this.ResetScrollBar();
			int pattern = (int)info.pattern;
			string name = this.ButtonName[pattern, 0];
			GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_prefab, name);
			this.SetObjActive(gameObject, true);
			int childCount = gameObject.transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				GameObject gameObject2 = gameObject.transform.GetChild(i).gameObject;
				if (!(gameObject2 == null))
				{
					gameObject2.SetActive(false);
				}
			}
			this.SetActiveBtn(gameObject, this.ButtonName[pattern, 1]);
			this.SetActiveBtn(gameObject, this.ButtonName[pattern, 2]);
			GameObject buttonRoot = GameObjectUtil.FindChildGameObject(this.m_prefab, "pattern_close");
			this.SetActiveBtn(buttonRoot, "Btn_close");
			GameObject gameObject3 = GameObjectUtil.FindChildGameObject(this.m_prefab, "Lbl_body");
			GameObject gameObject4 = GameObjectUtil.FindChildGameObject(this.m_prefab, "img_ad_tex");
			if (gameObject4 != null)
			{
				UITexture component = gameObject4.GetComponent<UITexture>();
				if (component != null)
				{
					component.enabled = false;
				}
			}
			this.SetObjActive(gameObject3, true);
			this.SetObjActive(gameObject4, false);
			UILabel component2 = gameObject3.GetComponent<UILabel>();
			if (component2 != null)
			{
				switch (info.rankingType)
				{
				case InformationWindow.RankingType.NON:
					component2.text = info.bodyText;
					break;
				}
			}
			UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_prefab, "Lbl_caption");
			if (uilabel != null)
			{
				uilabel.text = info.caption;
			}
			if (info.pattern != InformationWindow.ButtonPattern.TEXT && info.pattern != InformationWindow.ButtonPattern.DESIGNATED_AREA_TEXT)
			{
				if (info.rankingType == InformationWindow.RankingType.NON)
				{
					this.SetObjActive(gameObject3, false);
				}
				InformationImageManager instance = InformationImageManager.Instance;
				if (instance != null && !string.IsNullOrEmpty(info.imageId))
				{
					instance.Load(info.imageId, false, new Action<Texture2D>(this.OnLoadedTextureCallback));
				}
			}
			this.SetESRB(info.pattern, gameObject);
			this.PlayAnimation();
			base.enabled = true;
		}
	}

	// Token: 0x06002A34 RID: 10804 RVA: 0x00105AAC File Offset: 0x00103CAC
	private void PlayAnimation()
	{
		if (this.m_prefab != null)
		{
			this.m_prefab.SetActive(true);
			UIPlayAnimation uiplayAnimation = base.gameObject.GetComponent<UIPlayAnimation>();
			if (uiplayAnimation == null)
			{
				uiplayAnimation = base.gameObject.AddComponent<UIPlayAnimation>();
			}
			if (uiplayAnimation != null)
			{
				Animation component = this.m_prefab.GetComponent<Animation>();
				uiplayAnimation.target = component;
				uiplayAnimation.clipName = "ui_cmn_window_Anim";
				uiplayAnimation.Play(true);
			}
		}
	}

	// Token: 0x06002A35 RID: 10805 RVA: 0x00105B2C File Offset: 0x00103D2C
	private void OnClickLeftButton()
	{
		this.m_pressedFlag[0] = true;
		this.m_created = false;
		SoundManager.SePlay("sys_menu_decide", "SE");
	}

	// Token: 0x06002A36 RID: 10806 RVA: 0x00105B5C File Offset: 0x00103D5C
	private void OnClickRightButton()
	{
		this.m_pressedFlag[1] = true;
		this.m_created = false;
		SoundManager.SePlay("sys_menu_decide", "SE");
	}

	// Token: 0x06002A37 RID: 10807 RVA: 0x00105B8C File Offset: 0x00103D8C
	private void OnClickCloseButton()
	{
		this.m_pressedFlag[2] = true;
		this.m_created = false;
		SoundManager.SePlay("sys_window_close", "SE");
	}

	// Token: 0x06002A38 RID: 10808 RVA: 0x00105BBC File Offset: 0x00103DBC
	private void Start()
	{
	}

	// Token: 0x06002A39 RID: 10809 RVA: 0x00105BC0 File Offset: 0x00103DC0
	private void OnDestroy()
	{
		base.Destroy();
	}

	// Token: 0x06002A3A RID: 10810 RVA: 0x00105BC8 File Offset: 0x00103DC8
	private void Update()
	{
		switch (this.m_info.rankingType)
		{
		case InformationWindow.RankingType.NON:
			base.enabled = false;
			break;
		case InformationWindow.RankingType.WORLD:
			if (this.m_rankingResultWorld != null && this.m_rankingResultWorld.IsEnd)
			{
				this.m_endFlag = true;
				base.enabled = false;
			}
			break;
		case InformationWindow.RankingType.LEAGUE:
		case InformationWindow.RankingType.QUICK_LEAGUE:
			if (this.m_rankingResultLeague != null && this.m_rankingResultLeague.IsEnd())
			{
				this.m_endFlag = true;
				base.enabled = false;
			}
			break;
		case InformationWindow.RankingType.EVENT:
			if (this.m_eventRankingResult != null && this.m_eventRankingResult.IsEnd)
			{
				this.m_endFlag = true;
				base.enabled = false;
			}
			break;
		}
	}

	// Token: 0x06002A3B RID: 10811 RVA: 0x00105CA8 File Offset: 0x00103EA8
	private void SetESRB(InformationWindow.ButtonPattern pattern, GameObject parentObj)
	{
		if (parentObj != null)
		{
			GameObject gameObject;
			switch (pattern)
			{
			case InformationWindow.ButtonPattern.FEED:
				gameObject = GameObjectUtil.FindChildGameObject(parentObj, this.ButtonName[(int)pattern, 1]);
				goto IL_6C;
			case InformationWindow.ButtonPattern.FEED_BROWSER:
			case InformationWindow.ButtonPattern.FEED_ROULETTE:
			case InformationWindow.ButtonPattern.FEED_SHOP:
			case InformationWindow.ButtonPattern.FEED_EVENT:
			case InformationWindow.ButtonPattern.FEED_EVENT_LIST:
				gameObject = GameObjectUtil.FindChildGameObject(parentObj, this.ButtonName[(int)pattern, 2]);
				goto IL_6C;
			}
			return;
			IL_6C:
			if (gameObject != null && RegionManager.Instance != null && !RegionManager.Instance.IsUseSNS())
			{
				UIImageButton component = gameObject.GetComponent<UIImageButton>();
				if (component != null)
				{
					component.isEnabled = false;
				}
			}
		}
	}

	// Token: 0x06002A3C RID: 10812 RVA: 0x00105D68 File Offset: 0x00103F68
	private void SetCallBack()
	{
		this.SetClickBtnCallBack(this.m_prefab, "Btn_close", this.CallbackFuncName[2]);
		GameObject buttonRoot = GameObjectUtil.FindChildGameObject(this.m_prefab, "pattern_0");
		this.SetClickBtnCallBack(buttonRoot, "Btn_1_ok", this.CallbackFuncName[1]);
		this.SetClickBtnCallBack(buttonRoot, "Btn_2_post", this.CallbackFuncName[1]);
		this.SetClickBtnCallBack(buttonRoot, "Btn_3_browser", this.CallbackFuncName[1]);
		this.SetClickBtnCallBack(buttonRoot, "Btn_5_roulette", this.CallbackFuncName[1]);
		this.SetClickBtnCallBack(buttonRoot, "Btn_6_shop", this.CallbackFuncName[1]);
		this.SetClickBtnCallBack(buttonRoot, "Btn_7_event", this.CallbackFuncName[1]);
		this.SetClickBtnCallBack(buttonRoot, "Btn_8_event_list", this.CallbackFuncName[1]);
		GameObject buttonRoot2 = GameObjectUtil.FindChildGameObject(this.m_prefab, "pattern_2");
		this.SetClickBtnCallBack(buttonRoot2, "Btn_shop", this.CallbackFuncName[1]);
		this.SetClickBtnCallBack(buttonRoot2, "Btn_cancel", this.CallbackFuncName[0]);
		GameObject buttonRoot3 = GameObjectUtil.FindChildGameObject(this.m_prefab, "pattern_3");
		this.SetClickBtnCallBack(buttonRoot3, "Btn_post", this.CallbackFuncName[1]);
		this.SetClickBtnCallBack(buttonRoot3, "Btn_0_unpost", this.CallbackFuncName[0]);
		this.SetClickBtnCallBack(buttonRoot3, "Btn_1_browser", this.CallbackFuncName[0]);
		this.SetClickBtnCallBack(buttonRoot3, "Btn_2_item", this.CallbackFuncName[0]);
		this.SetClickBtnCallBack(buttonRoot3, "Btn_3_roulette", this.CallbackFuncName[0]);
		this.SetClickBtnCallBack(buttonRoot3, "Btn_4_shop", this.CallbackFuncName[0]);
		this.SetClickBtnCallBack(buttonRoot3, "Btn_5_event", this.CallbackFuncName[0]);
		this.SetClickBtnCallBack(buttonRoot3, "Btn_6_event_list", this.CallbackFuncName[0]);
	}

	// Token: 0x06002A3D RID: 10813 RVA: 0x00105F18 File Offset: 0x00104118
	private void OnFinishedAnimationCallback()
	{
		if (this.m_prefab != null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_prefab, "img_ad_tex");
			if (gameObject != null)
			{
				UITexture component = gameObject.GetComponent<UITexture>();
				if (component != null && component.mainTexture != null)
				{
					component.mainTexture = null;
				}
			}
			this.m_prefab.SetActive(false);
		}
		this.m_endFlag = true;
	}

	// Token: 0x06002A3E RID: 10814 RVA: 0x00105F94 File Offset: 0x00104194
	private void OnLoadedTextureCallback(Texture2D texture)
	{
		this.SetTexture(texture);
	}

	// Token: 0x06002A3F RID: 10815 RVA: 0x00105FA0 File Offset: 0x001041A0
	public override void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (this.m_created)
		{
			if (msg != null)
			{
				msg.StaySequence();
			}
			if (this.m_prefab != null)
			{
				GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_prefab, "Btn_close");
				if (gameObject != null && gameObject.activeSelf)
				{
					UIButtonMessage component = gameObject.GetComponent<UIButtonMessage>();
					if (component != null)
					{
						component.SendMessage("OnClick");
					}
				}
			}
		}
	}

	// Token: 0x0400256E RID: 9582
	private GameObject m_prefab;

	// Token: 0x0400256F RID: 9583
	private readonly string[,] ButtonName;

	// Token: 0x04002570 RID: 9584
	private readonly string[] CallbackFuncName;

	// Token: 0x04002571 RID: 9585
	private bool[] m_pressedFlag;

	// Token: 0x04002572 RID: 9586
	private InformationWindow.Information m_info;

	// Token: 0x04002573 RID: 9587
	private RankingResultWorldRanking m_rankingResultWorld;

	// Token: 0x04002574 RID: 9588
	private RankingResultWorldRanking m_eventRankingResult;

	// Token: 0x04002575 RID: 9589
	private RankingResultLeague m_rankingResultLeague;

	// Token: 0x04002576 RID: 9590
	private bool m_created;

	// Token: 0x04002577 RID: 9591
	private bool m_endFlag;

	// Token: 0x02000553 RID: 1363
	public enum ButtonPattern
	{
		// Token: 0x04002579 RID: 9593
		NONE = -1,
		// Token: 0x0400257A RID: 9594
		TEXT,
		// Token: 0x0400257B RID: 9595
		OK,
		// Token: 0x0400257C RID: 9596
		FEED,
		// Token: 0x0400257D RID: 9597
		SHOP_CANCEL,
		// Token: 0x0400257E RID: 9598
		FEED_BROWSER,
		// Token: 0x0400257F RID: 9599
		FEED_ROULETTE,
		// Token: 0x04002580 RID: 9600
		FEED_SHOP,
		// Token: 0x04002581 RID: 9601
		FEED_EVENT,
		// Token: 0x04002582 RID: 9602
		FEED_EVENT_LIST,
		// Token: 0x04002583 RID: 9603
		BROWSER,
		// Token: 0x04002584 RID: 9604
		ROULETTE,
		// Token: 0x04002585 RID: 9605
		SHOP,
		// Token: 0x04002586 RID: 9606
		EVENT,
		// Token: 0x04002587 RID: 9607
		EVENT_LIST,
		// Token: 0x04002588 RID: 9608
		ROULETTE_IFNO,
		// Token: 0x04002589 RID: 9609
		QUICK_EVENT_INFO,
		// Token: 0x0400258A RID: 9610
		DESIGNATED_AREA_TEXT,
		// Token: 0x0400258B RID: 9611
		DESIGNATED_AREA_IMAGE,
		// Token: 0x0400258C RID: 9612
		NUM
	}

	// Token: 0x02000554 RID: 1364
	public struct Information
	{
		// Token: 0x0400258D RID: 9613
		public string imageId;

		// Token: 0x0400258E RID: 9614
		public string caption;

		// Token: 0x0400258F RID: 9615
		public string bodyText;

		// Token: 0x04002590 RID: 9616
		public string url;

		// Token: 0x04002591 RID: 9617
		public Texture2D texture;

		// Token: 0x04002592 RID: 9618
		public InformationWindow.ButtonPattern pattern;

		// Token: 0x04002593 RID: 9619
		public InformationWindow.RankingType rankingType;
	}

	// Token: 0x02000555 RID: 1365
	public enum RankingType
	{
		// Token: 0x04002595 RID: 9621
		NON,
		// Token: 0x04002596 RID: 9622
		WORLD,
		// Token: 0x04002597 RID: 9623
		LEAGUE,
		// Token: 0x04002598 RID: 9624
		EVENT,
		// Token: 0x04002599 RID: 9625
		QUICK_LEAGUE
	}

	// Token: 0x02000556 RID: 1366
	public enum ButtonType
	{
		// Token: 0x0400259B RID: 9627
		NONE = -1,
		// Token: 0x0400259C RID: 9628
		LEFT,
		// Token: 0x0400259D RID: 9629
		RIGHT,
		// Token: 0x0400259E RID: 9630
		CLOSE,
		// Token: 0x0400259F RID: 9631
		NUM
	}

	// Token: 0x02000557 RID: 1367
	public enum ObjNameType
	{
		// Token: 0x040025A1 RID: 9633
		ROOT,
		// Token: 0x040025A2 RID: 9634
		LEFT,
		// Token: 0x040025A3 RID: 9635
		RIGHT,
		// Token: 0x040025A4 RID: 9636
		NUM
	}
}
