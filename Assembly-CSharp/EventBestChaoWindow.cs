using System;
using System.Collections.Generic;
using AnimationOrTween;
using DataTable;
using Text;
using UnityEngine;

// Token: 0x0200035A RID: 858
public class EventBestChaoWindow : MonoBehaviour
{
	// Token: 0x170003CD RID: 973
	// (get) Token: 0x0600197D RID: 6525 RVA: 0x000945D0 File Offset: 0x000927D0
	public static bool Created
	{
		get
		{
			return EventBestChaoWindow.m_created;
		}
	}

	// Token: 0x0600197E RID: 6526 RVA: 0x000945D8 File Offset: 0x000927D8
	public static EventBestChaoWindow GetWindow()
	{
		GameObject gameObject = GameObject.Find("UI Root (2D)");
		if (gameObject != null)
		{
			return GameObjectUtil.FindChildGameObjectComponent<EventBestChaoWindow>(gameObject, "EventBestchaoWindow");
		}
		return null;
	}

	// Token: 0x0600197F RID: 6527 RVA: 0x0009460C File Offset: 0x0009280C
	public void OpenWindow(List<ServerItem> itemList)
	{
		if (itemList == null)
		{
			return;
		}
		SoundManager.SePlay("sys_window_open", "SE");
		BackKeyManager.AddWindowCallBack(base.gameObject);
		UIEffectManager instance = UIEffectManager.Instance;
		if (instance != null)
		{
			instance.SetActiveEffect(HudMenuUtility.EffectPriority.Menu, false);
		}
		EventBestChaoWindow.m_created = true;
		base.gameObject.SetActive(true);
		if (!this.m_isSetup)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_close");
			if (gameObject != null)
			{
				UIButtonMessage uibuttonMessage = gameObject.GetComponent<UIButtonMessage>();
				if (uibuttonMessage == null)
				{
					uibuttonMessage = gameObject.AddComponent<UIButtonMessage>();
				}
				uibuttonMessage.target = base.gameObject;
				uibuttonMessage.functionName = "CloseButtonClickedCallback";
			}
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_window_pager_L");
			if (gameObject2 != null)
			{
				UIButtonMessage uibuttonMessage2 = gameObject2.GetComponent<UIButtonMessage>();
				if (uibuttonMessage2 == null)
				{
					uibuttonMessage2 = gameObject2.AddComponent<UIButtonMessage>();
				}
				uibuttonMessage2.target = base.gameObject;
				uibuttonMessage2.functionName = "LeftButtonClickedCallback";
			}
			GameObject gameObject3 = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_window_pager_R");
			if (gameObject3 != null)
			{
				UIButtonMessage uibuttonMessage3 = gameObject3.GetComponent<UIButtonMessage>();
				if (uibuttonMessage3 == null)
				{
					uibuttonMessage3 = gameObject3.AddComponent<UIButtonMessage>();
				}
				uibuttonMessage3.target = base.gameObject;
				uibuttonMessage3.functionName = "RightButtonClickedCallback";
			}
			this.m_isSetup = false;
		}
		Animation animation = GameObjectUtil.FindChildGameObjectComponent<Animation>(base.gameObject, "bestchao_window");
		if (animation != null)
		{
			ActiveAnimation.Play(animation, Direction.Forward);
		}
		GameObject gameObject4 = GameObjectUtil.FindChildGameObject(base.gameObject, "chao_window");
		if (gameObject4 != null)
		{
			int childCount = gameObject4.transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				Transform child = gameObject4.transform.GetChild(i);
				if (!(child == null))
				{
					GameObject gameObject5 = child.gameObject;
					if (!(gameObject5 == null))
					{
						gameObject5.SetActive(false);
					}
				}
			}
		}
		if (this.m_chaoPage == null)
		{
			this.m_chaoPage = base.gameObject.AddComponent<EventBestChaoWindow.EventBestChaoPage>();
		}
		int count = itemList.Count;
		this.m_chaoPage.BeginSetup();
		for (int j = 0; j < count; j++)
		{
			this.m_chaoPage.AddItem(itemList[j]);
		}
		this.m_chaoPage.EndSetup();
		this.DisplayPageScrollButton();
	}

	// Token: 0x06001980 RID: 6528 RVA: 0x00094894 File Offset: 0x00092A94
	public void OpenWindow(List<int> chaoIdList)
	{
		if (chaoIdList == null)
		{
			return;
		}
		List<ServerItem> list = new List<ServerItem>();
		foreach (int chaoId in chaoIdList)
		{
			ServerItem item = ServerItem.CreateFromChaoId(chaoId);
			if (item.chaoId != -1)
			{
				list.Add(item);
			}
		}
		this.OpenWindow(list);
	}

	// Token: 0x06001981 RID: 6529 RVA: 0x00094924 File Offset: 0x00092B24
	private void Awake()
	{
	}

	// Token: 0x06001982 RID: 6530 RVA: 0x00094928 File Offset: 0x00092B28
	private void OnDestroy()
	{
	}

	// Token: 0x06001983 RID: 6531 RVA: 0x0009492C File Offset: 0x00092B2C
	private void DisplayPageScrollButton()
	{
		if (this.m_chaoPage == null)
		{
			return;
		}
		int currentPageNum = this.m_chaoPage.GetCurrentPageNum();
		int pageCount = this.m_chaoPage.GetPageCount();
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_window_pager_L");
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_window_pager_R");
		if (gameObject == null || gameObject2 == null)
		{
			return;
		}
		bool active = true;
		bool active2 = true;
		if (currentPageNum == 0)
		{
			active = false;
		}
		if (currentPageNum == pageCount - 1)
		{
			active2 = false;
		}
		gameObject.SetActive(active);
		gameObject2.SetActive(active2);
	}

	// Token: 0x06001984 RID: 6532 RVA: 0x000949CC File Offset: 0x00092BCC
	private void CloseButtonClickedCallback()
	{
		SoundManager.SePlay("sys_window_close", "SE");
		Animation animation = GameObjectUtil.FindChildGameObjectComponent<Animation>(base.gameObject, "bestchao_window");
		if (animation != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(animation, Direction.Reverse);
			EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OutAnimationFinishCallback), true);
		}
	}

	// Token: 0x06001985 RID: 6533 RVA: 0x00094A28 File Offset: 0x00092C28
	private void LeftButtonClickedCallback()
	{
		SoundManager.SePlay("sys_page_skip", "SE");
		if (this.m_chaoPage != null)
		{
			this.m_chaoPage.GoPrevPage();
		}
		this.DisplayPageScrollButton();
	}

	// Token: 0x06001986 RID: 6534 RVA: 0x00094A68 File Offset: 0x00092C68
	private void RightButtonClickedCallback()
	{
		SoundManager.SePlay("sys_page_skip", "SE");
		if (this.m_chaoPage != null)
		{
			this.m_chaoPage.GoNextPage();
		}
		this.DisplayPageScrollButton();
	}

	// Token: 0x06001987 RID: 6535 RVA: 0x00094AA8 File Offset: 0x00092CA8
	private void OutAnimationFinishCallback()
	{
		UIEffectManager instance = UIEffectManager.Instance;
		if (instance != null)
		{
			instance.SetActiveEffect(HudMenuUtility.EffectPriority.Menu, true);
		}
		BackKeyManager.RemoveWindowCallBack(base.gameObject);
		EventBestChaoWindow.m_created = false;
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001988 RID: 6536 RVA: 0x00094AEC File Offset: 0x00092CEC
	private void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		if (msg != null)
		{
			msg.StaySequence();
		}
		UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(base.gameObject, "Btn_close");
		if (uibuttonMessage != null)
		{
			uibuttonMessage.SendMessage("OnClick");
		}
	}

	// Token: 0x040016DB RID: 5851
	private EventBestChaoWindow.EventBestChaoPage m_chaoPage;

	// Token: 0x040016DC RID: 5852
	private bool m_isSetup;

	// Token: 0x040016DD RID: 5853
	private static bool m_created;

	// Token: 0x0200035B RID: 859
	private abstract class ButtonDecorate
	{
		// Token: 0x0600198A RID: 6538
		public abstract void Decorate(GameObject gameObject, ServerItem item);

		// Token: 0x0600198B RID: 6539
		public abstract void OpenWindow(ServerItem item);
	}

	// Token: 0x0200035C RID: 860
	private class ChaoButtonDecorate : EventBestChaoWindow.ButtonDecorate
	{
		// Token: 0x0600198D RID: 6541 RVA: 0x00094B50 File Offset: 0x00092D50
		public override void Decorate(GameObject gameObject, ServerItem item)
		{
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "chao");
			if (gameObject2 != null)
			{
				gameObject2.SetActive(true);
				UIButtonMessage uibuttonMessage = gameObject2.GetComponent<UIButtonMessage>();
				if (uibuttonMessage == null)
				{
					uibuttonMessage = gameObject2.AddComponent<UIButtonMessage>();
				}
				uibuttonMessage.target = gameObject;
				uibuttonMessage.functionName = "OnButtonClickedCallback";
			}
			GameObject gameObject3 = GameObjectUtil.FindChildGameObject(gameObject, "chara");
			if (gameObject3 != null)
			{
				gameObject3.SetActive(false);
			}
			ChaoData chaoData = ChaoTable.GetChaoData(item.chaoId);
			if (chaoData == null)
			{
				return;
			}
			UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject, "img_chao_bg");
			if (uisprite != null)
			{
				uisprite.spriteName = "ui_tex_chao_bg_" + ((int)chaoData.rarity).ToString();
			}
			UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject, "img_chao_type_icon");
			if (uisprite2 != null)
			{
				CharacterAttribute charaAtribute = chaoData.charaAtribute;
				string spriteName = "ui_chao_set_type_icon_" + charaAtribute.ToString().ToLower();
				uisprite2.spriteName = spriteName;
			}
			UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, "Lbl_chao_feature");
			if (uilabel != null)
			{
				string featuredDetail = chaoData.GetFeaturedDetail();
				if (!string.IsNullOrEmpty(featuredDetail))
				{
					uilabel.text = featuredDetail;
				}
			}
			UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, "Lbl_chao_name");
			if (uilabel2 != null)
			{
				string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_CHAO_TEXT, "Chao", string.Format("name{0:D4}", item.chaoId)).text;
				uilabel2.text = text;
			}
			UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(gameObject, "texture_chao");
			if (uitexture != null)
			{
				ChaoTextureManager.CallbackInfo info = new ChaoTextureManager.CallbackInfo(uitexture, null, true);
				ChaoTextureManager.Instance.GetTexture(item.chaoId, info);
			}
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x00094D18 File Offset: 0x00092F18
		public override void OpenWindow(ServerItem item)
		{
			ChaoSetWindowUI window = ChaoSetWindowUI.GetWindow();
			if (window != null)
			{
				ChaoData chaoData = ChaoTable.GetChaoData(item.chaoId);
				if (chaoData != null)
				{
					ChaoSetWindowUI.ChaoInfo chaoInfo = new ChaoSetWindowUI.ChaoInfo(chaoData);
					chaoInfo.level = ChaoTable.ChaoMaxLevel();
					chaoInfo.detail = chaoData.GetDetailLevelPlusSP(chaoInfo.level);
					if (chaoInfo.level == ChaoTable.ChaoMaxLevel())
					{
						chaoInfo.detail = chaoInfo.detail + "\n" + TextUtility.GetChaoText("Chao", "level_max");
					}
					window.OpenWindow(chaoInfo, ChaoSetWindowUI.WindowType.WINDOW_ONLY);
				}
			}
		}
	}

	// Token: 0x0200035D RID: 861
	private class CharaButtonDecorate : EventBestChaoWindow.ButtonDecorate
	{
		// Token: 0x06001990 RID: 6544 RVA: 0x00094DBC File Offset: 0x00092FBC
		public override void Decorate(GameObject gameObject, ServerItem item)
		{
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "chara");
			if (gameObject2 != null)
			{
				gameObject2.SetActive(true);
				UIButtonMessage uibuttonMessage = gameObject2.GetComponent<UIButtonMessage>();
				if (uibuttonMessage == null)
				{
					uibuttonMessage = gameObject2.AddComponent<UIButtonMessage>();
				}
				uibuttonMessage.target = gameObject;
				uibuttonMessage.functionName = "OnButtonClickedCallback";
			}
			GameObject gameObject3 = GameObjectUtil.FindChildGameObject(gameObject, "chao");
			if (gameObject3 != null)
			{
				gameObject3.SetActive(false);
			}
			CharaType charaType = item.charaType;
			UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject, "img_player_genus");
			if (uisprite != null)
			{
				uisprite.spriteName = HudUtility.GetTeamAttributeSpriteName(charaType);
				UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject, "img_player_speacies");
				if (uisprite2 != null)
				{
					uisprite2.spriteName = HudUtility.GetCharaAttributeSpriteName(charaType);
				}
			}
			UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, "Lbl_chara_feature");
			if (uilabel != null)
			{
				string cellName = "featured_footnote_chara" + item.idIndex.ToString("D4");
				string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_CHAO_TEXT, "Chao", cellName).text;
				uilabel.text = text;
			}
			UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, "Lbl_chara_name");
			if (uilabel2 != null)
			{
				string text2 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "CharaName", CharaName.Name[(int)charaType]).text;
				uilabel2.text = text2;
			}
			GameObject gameObject4 = GameObjectUtil.FindChildGameObject(gameObject, "sprite_chara");
			if (gameObject4 != null)
			{
				gameObject4.SetActive(false);
			}
			UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(gameObject, "img_player_tex");
			if (uitexture != null)
			{
				TextureRequestChara request = new TextureRequestChara(charaType, uitexture);
				TextureAsyncLoadManager.Instance.Request(request);
			}
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x00094F70 File Offset: 0x00093170
		public override void OpenWindow(ServerItem item)
		{
			CharaType charaType = item.charaType;
			PlayerSetWindowUI playerSetWindowUI = PlayerSetWindowUI.Create(charaType, null, PlayerSetWindowUI.WINDOW_MODE.DEFAULT);
			if (playerSetWindowUI != null)
			{
				playerSetWindowUI.Setup(charaType, null, PlayerSetWindowUI.WINDOW_MODE.DEFAULT);
			}
		}
	}

	// Token: 0x0200035E RID: 862
	private class ChaoWindowButton : MonoBehaviour
	{
		// Token: 0x06001993 RID: 6547 RVA: 0x00094FAC File Offset: 0x000931AC
		public void Setup()
		{
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x00094FB0 File Offset: 0x000931B0
		public void SetItemId(ServerItem item)
		{
			this.m_item = item;
			base.gameObject.SetActive(true);
			ServerItem.IdType idType = item.idType;
			if (idType != ServerItem.IdType.CHARA)
			{
				if (idType == ServerItem.IdType.CHAO)
				{
					this.m_decorater = new EventBestChaoWindow.ChaoButtonDecorate();
				}
			}
			else
			{
				this.m_decorater = new EventBestChaoWindow.CharaButtonDecorate();
			}
			if (this.m_decorater != null)
			{
				this.m_decorater.Decorate(base.gameObject, item);
			}
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x0009502C File Offset: 0x0009322C
		private void OnButtonClickedCallback()
		{
			if (this.m_decorater != null)
			{
				this.m_decorater.OpenWindow(this.m_item);
			}
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x0009504C File Offset: 0x0009324C
		private void OnSetChaoTexture(ChaoTextureManager.TextureData tex)
		{
			UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(base.gameObject, "texture_chao");
			if (uitexture == null)
			{
				return;
			}
			uitexture.gameObject.SetActive(true);
			uitexture.mainTexture = tex.tex;
		}

		// Token: 0x040016DE RID: 5854
		private ServerItem m_item;

		// Token: 0x040016DF RID: 5855
		private EventBestChaoWindow.ButtonDecorate m_decorater;
	}

	// Token: 0x0200035F RID: 863
	private class EventBestChaoPage : MonoBehaviour
	{
		// Token: 0x06001999 RID: 6553 RVA: 0x000950DC File Offset: 0x000932DC
		public void BeginSetup()
		{
			this.m_isInSetup = true;
			for (int i = 0; i < 3; i++)
			{
				if (!(this.m_commonButton[i] != null))
				{
					string name = EventBestChaoWindow.EventBestChaoPage.ButtonName[i];
					GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, name);
					if (!(gameObject == null))
					{
						EventBestChaoWindow.ChaoWindowButton chaoWindowButton = gameObject.AddComponent<EventBestChaoWindow.ChaoWindowButton>();
						chaoWindowButton.Setup();
						this.m_commonButton[i] = chaoWindowButton;
					}
				}
			}
			if (this.m_pageLabel == null)
			{
				this.m_pageLabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_page_number");
			}
			if (this.m_chaos != null)
			{
				this.m_chaos.Clear();
			}
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x00095194 File Offset: 0x00093394
		public void AddItem(ServerItem item)
		{
			if (!this.m_isInSetup)
			{
				return;
			}
			EventBestChaoWindow.EventBestChaoPage.ChaoInfo chaoInfo = new EventBestChaoWindow.EventBestChaoPage.ChaoInfo();
			chaoInfo.item = item;
			chaoInfo.button = null;
			this.m_chaos.Add(chaoInfo);
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x000951D0 File Offset: 0x000933D0
		public void EndSetup()
		{
			int count = this.m_chaos.Count;
			for (int i = 0; i < count - 1; i++)
			{
				if (i % EventBestChaoWindow.EventBestChaoPage.CHAO_MAX == 0)
				{
					EventBestChaoWindow.ChaoWindowButton chaoWindowButton = this.m_commonButton[0];
					if (!(chaoWindowButton == null))
					{
						this.m_chaos[i].button = chaoWindowButton;
					}
				}
				else
				{
					EventBestChaoWindow.ChaoWindowButton chaoWindowButton2 = this.m_commonButton[2];
					if (!(chaoWindowButton2 == null))
					{
						this.m_chaos[i].button = chaoWindowButton2;
					}
				}
			}
			if (count % EventBestChaoWindow.EventBestChaoPage.CHAO_MAX == 0)
			{
				EventBestChaoWindow.ChaoWindowButton chaoWindowButton3 = this.m_commonButton[2];
				if (chaoWindowButton3 == null)
				{
					return;
				}
				this.m_chaos[count - 1].button = chaoWindowButton3;
			}
			else
			{
				EventBestChaoWindow.ChaoWindowButton chaoWindowButton4 = this.m_commonButton[1];
				if (chaoWindowButton4 == null)
				{
					return;
				}
				this.m_chaos[count - 1].button = chaoWindowButton4;
			}
			this.PageChange(this.m_currentPageNum);
			this.m_isInSetup = false;
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x000952E4 File Offset: 0x000934E4
		public int GetPageCount()
		{
			int count = this.m_chaos.Count;
			int num = count / EventBestChaoWindow.EventBestChaoPage.CHAO_MAX;
			if (count % 2 != 0)
			{
				num++;
			}
			return num;
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x00095314 File Offset: 0x00093514
		public int GetCurrentPageNum()
		{
			return this.m_currentPageNum;
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x0009531C File Offset: 0x0009351C
		public void GoNextPage()
		{
			if (this.m_currentPageNum >= this.GetPageCount() - 1)
			{
				return;
			}
			this.m_currentPageNum++;
			this.PageChange(this.m_currentPageNum);
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x00095358 File Offset: 0x00093558
		public void GoPrevPage()
		{
			if (this.m_currentPageNum <= 0)
			{
				return;
			}
			this.m_currentPageNum--;
			this.PageChange(this.m_currentPageNum);
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x00095384 File Offset: 0x00093584
		private void PageChange(int pageIndex)
		{
			foreach (EventBestChaoWindow.ChaoWindowButton chaoWindowButton in this.m_commonButton)
			{
				if (!(chaoWindowButton == null))
				{
					chaoWindowButton.gameObject.SetActive(false);
				}
			}
			int num = EventBestChaoWindow.EventBestChaoPage.CHAO_MAX * pageIndex;
			int num2 = num + EventBestChaoWindow.EventBestChaoPage.CHAO_MAX;
			int count = this.m_chaos.Count;
			if (num2 > count)
			{
				num2 = count;
			}
			for (int j = num; j < num2; j++)
			{
				EventBestChaoWindow.EventBestChaoPage.ChaoInfo chaoInfo = this.m_chaos[j];
				if (chaoInfo != null)
				{
					ServerItem item = chaoInfo.item;
					EventBestChaoWindow.ChaoWindowButton button = chaoInfo.button;
					if (!(button == null))
					{
						button.SetItemId(item);
					}
				}
			}
			if (this.m_pageLabel != null)
			{
				int num3 = pageIndex + 1;
				int pageCount = this.GetPageCount();
				this.m_pageLabel.text = num3.ToString() + "/" + pageCount.ToString();
			}
		}

		// Token: 0x040016E0 RID: 5856
		private static readonly int CHAO_MAX = 2;

		// Token: 0x040016E1 RID: 5857
		private static readonly string[] ButtonName = new string[]
		{
			"2_1_window",
			"1_1_window",
			"2_2_window"
		};

		// Token: 0x040016E2 RID: 5858
		private EventBestChaoWindow.ChaoWindowButton[] m_commonButton = new EventBestChaoWindow.ChaoWindowButton[3];

		// Token: 0x040016E3 RID: 5859
		private UILabel m_pageLabel;

		// Token: 0x040016E4 RID: 5860
		private bool m_isInSetup;

		// Token: 0x040016E5 RID: 5861
		private List<EventBestChaoWindow.EventBestChaoPage.ChaoInfo> m_chaos = new List<EventBestChaoWindow.EventBestChaoPage.ChaoInfo>();

		// Token: 0x040016E6 RID: 5862
		private int m_currentPageNum;

		// Token: 0x02000360 RID: 864
		private enum Button
		{
			// Token: 0x040016E8 RID: 5864
			None = -1,
			// Token: 0x040016E9 RID: 5865
			Left,
			// Token: 0x040016EA RID: 5866
			Center,
			// Token: 0x040016EB RID: 5867
			Right,
			// Token: 0x040016EC RID: 5868
			Num
		}

		// Token: 0x02000361 RID: 865
		private class ChaoInfo
		{
			// Token: 0x040016ED RID: 5869
			public ServerItem item;

			// Token: 0x040016EE RID: 5870
			public EventBestChaoWindow.ChaoWindowButton button;
		}
	}
}
