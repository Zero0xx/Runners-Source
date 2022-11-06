using System;
using AnimationOrTween;
using DataTable;
using Text;
using UnityEngine;

// Token: 0x0200055B RID: 1371
public class ItemGetWindow : WindowBase
{
	// Token: 0x06002A52 RID: 10834 RVA: 0x00106584 File Offset: 0x00104784
	private void Start()
	{
	}

	// Token: 0x06002A53 RID: 10835 RVA: 0x00106588 File Offset: 0x00104788
	private void OnDestroy()
	{
		base.Destroy();
	}

	// Token: 0x06002A54 RID: 10836 RVA: 0x00106590 File Offset: 0x00104790
	private void SetWindowData()
	{
		base.gameObject.SetActive(true);
		this.SetDisableButton(this.m_disableButton);
		bool active = false;
		bool active2 = false;
		ItemGetWindow.ButtonType buttonType = this.m_info.buttonType;
		if (buttonType != ItemGetWindow.ButtonType.Ok)
		{
			if (buttonType == ItemGetWindow.ButtonType.TweetCancel)
			{
				active2 = true;
			}
		}
		else
		{
			active = true;
		}
		Transform transform = base.gameObject.transform.Find("window/pattern_btn_use");
		Transform transform2 = base.gameObject.transform.Find("window/pattern_btn_use/pattern_0");
		Transform transform3 = base.gameObject.transform.Find("window/pattern_btn_use/pattern_1");
		if (transform != null)
		{
			transform.gameObject.SetActive(true);
		}
		if (transform2 != null)
		{
			transform2.gameObject.SetActive(active);
		}
		if (transform3 != null)
		{
			transform3.gameObject.SetActive(active2);
			UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(transform3.gameObject, "Btn_post");
			if (uiimageButton != null)
			{
				if (RegionManager.Instance != null)
				{
					uiimageButton.isEnabled = RegionManager.Instance.IsUseSNS();
				}
				else
				{
					uiimageButton.isEnabled = false;
				}
			}
		}
		Transform transform4 = base.gameObject.transform.Find("window/Lbl_caption");
		if (transform4 != null)
		{
			UILabel component = transform4.GetComponent<UILabel>();
			if (component != null)
			{
				component.text = this.m_info.caption;
			}
		}
		if (this.m_imgView != null)
		{
			this.m_imgView.SetActive(true);
		}
		bool active3 = true;
		bool active4 = true;
		bool active5 = true;
		string text = string.Empty;
		ServerItem serverItem = new ServerItem((ServerItem.Id)this.m_info.serverItemId);
		if (serverItem.idType == ServerItem.IdType.CHAO)
		{
			int chaoId = serverItem.chaoId;
			if (ChaoTextureManager.Instance != null)
			{
				ChaoTextureManager.CallbackInfo info = new ChaoTextureManager.CallbackInfo(this.m_uiTexture, null, true);
				ChaoTextureManager.Instance.GetTexture(chaoId, info);
			}
			ChaoData chaoData = ChaoTable.GetChaoData(chaoId);
			if (chaoData != null)
			{
				text = chaoData.name;
				this.m_info.imageCount = TextUtility.GetTextLevel(chaoData.level.ToString());
			}
			GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_imgEventTex, "img_tex_flame");
			if (gameObject != null)
			{
				gameObject.SetActive(false);
			}
			active3 = false;
		}
		else if (serverItem.rewardType != RewardType.NONE)
		{
			if (serverItem.id == ServerItem.Id.JACKPOT)
			{
				this.m_itemImageSpriteName = "ui_cmn_icon_item_" + 8;
				active4 = false;
				text = TextUtility.GetCommonText("Item", "ring");
			}
			else
			{
				this.m_itemImageSpriteName = "ui_cmn_icon_item_" + (int)serverItem.rewardType;
				active4 = false;
				text = serverItem.serverItemName;
			}
		}
		else
		{
			this.m_itemImageSpriteName = null;
			active3 = false;
			active4 = false;
			active5 = false;
		}
		if (this.m_imgEventTex != null)
		{
			this.m_imgEventTex.SetActive(active4);
		}
		if (this.m_imgItem != null)
		{
			this.m_imgItem.SetActive(active3);
		}
		if (this.m_imgItemSprite != null)
		{
			this.m_imgItemSprite.spriteName = this.m_itemImageSpriteName;
		}
		if (this.m_imgName != null)
		{
			this.m_imgName.text = text;
		}
		if (this.m_imgCount != null)
		{
			this.m_imgCount.text = this.m_info.imageCount;
		}
		if (this.m_imgDecoEff != null)
		{
			this.m_imgDecoEff.SetActive(active5);
		}
	}

	// Token: 0x06002A55 RID: 10837 RVA: 0x00106948 File Offset: 0x00104B48
	public GameObject Create(ItemGetWindow.CInfo info)
	{
		RouletteManager.OpenRouletteWindow();
		this.m_info = info;
		this.m_disableButton = info.disableButton;
		this.m_pressed.m_isButtonPressed = false;
		this.m_pressed.m_isOkButtonPressed = false;
		this.m_pressed.m_isYesButtonPressed = false;
		this.m_pressed.m_isNoButtonPressed = false;
		this.m_isEnd = false;
		this.m_isOpened = false;
		this.SetWindowData();
		SoundManager.SePlay("sys_window_open", "SE");
		Animation component = base.gameObject.GetComponent<Animation>();
		if (component != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(component, Direction.Forward);
			if (activeAnimation != null)
			{
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnFinishedAnimationCallback), true);
			}
		}
		return base.gameObject;
	}

	// Token: 0x06002A56 RID: 10838 RVA: 0x00106A0C File Offset: 0x00104C0C
	public void Reset()
	{
		this.m_info = new ItemGetWindow.CInfo();
		this.m_disableButton = false;
		this.m_pressed.m_isButtonPressed = false;
		this.m_pressed.m_isOkButtonPressed = false;
		this.m_pressed.m_isYesButtonPressed = false;
		this.m_pressed.m_isNoButtonPressed = false;
		this.m_isEnd = true;
		this.m_isOpened = false;
	}

	// Token: 0x17000587 RID: 1415
	// (get) Token: 0x06002A57 RID: 10839 RVA: 0x00106A6C File Offset: 0x00104C6C
	public ItemGetWindow.CInfo Info
	{
		get
		{
			return this.m_info;
		}
	}

	// Token: 0x06002A58 RID: 10840 RVA: 0x00106A74 File Offset: 0x00104C74
	public bool IsCreated(string name)
	{
		return this.m_info.name == name;
	}

	// Token: 0x17000588 RID: 1416
	// (get) Token: 0x06002A59 RID: 10841 RVA: 0x00106A88 File Offset: 0x00104C88
	public bool IsOkButtonPressed
	{
		get
		{
			return this.m_pressed.m_isOkButtonPressed;
		}
	}

	// Token: 0x17000589 RID: 1417
	// (get) Token: 0x06002A5A RID: 10842 RVA: 0x00106A98 File Offset: 0x00104C98
	public bool IsYesButtonPressed
	{
		get
		{
			return this.m_pressed.m_isYesButtonPressed;
		}
	}

	// Token: 0x1700058A RID: 1418
	// (get) Token: 0x06002A5B RID: 10843 RVA: 0x00106AA8 File Offset: 0x00104CA8
	public bool IsNoButtonPressed
	{
		get
		{
			return this.m_pressed.m_isNoButtonPressed;
		}
	}

	// Token: 0x1700058B RID: 1419
	// (get) Token: 0x06002A5C RID: 10844 RVA: 0x00106AB8 File Offset: 0x00104CB8
	public bool IsEnd
	{
		get
		{
			return this.m_isEnd;
		}
	}

	// Token: 0x06002A5D RID: 10845 RVA: 0x00106AC0 File Offset: 0x00104CC0
	private void OnClickOkButton()
	{
		RouletteManager.CloseRouletteWindow();
		if (!this.m_pressed.m_isButtonPressed)
		{
			SoundManager.SePlay("sys_menu_decide", "SE");
			this.m_pressed.m_isOkButtonPressed = true;
			this.m_pressed.m_isButtonPressed = true;
		}
		this.m_isOpened = false;
	}

	// Token: 0x06002A5E RID: 10846 RVA: 0x00106B14 File Offset: 0x00104D14
	private void OnClickYesButton()
	{
		RouletteManager.CloseRouletteWindow();
		if (!this.m_pressed.m_isButtonPressed)
		{
			SoundManager.SePlay("sys_menu_decide", "SE");
			this.m_pressed.m_isYesButtonPressed = true;
			this.m_pressed.m_isButtonPressed = true;
		}
		this.m_isOpened = false;
	}

	// Token: 0x06002A5F RID: 10847 RVA: 0x00106B68 File Offset: 0x00104D68
	private void OnClickNoButton()
	{
		RouletteManager.CloseRouletteWindow();
		if (!this.m_pressed.m_isButtonPressed)
		{
			SoundManager.SePlay("sys_menu_decide", "SE");
			this.m_pressed.m_isNoButtonPressed = true;
			this.m_pressed.m_isButtonPressed = true;
		}
		this.m_isOpened = false;
	}

	// Token: 0x06002A60 RID: 10848 RVA: 0x00106BBC File Offset: 0x00104DBC
	private void OnSetChaoTexture(ChaoTextureManager.TextureData data)
	{
		if (data.tex != null && this.m_uiTexture != null)
		{
			this.m_uiTexture.enabled = true;
			this.m_uiTexture.mainTexture = data.tex;
		}
	}

	// Token: 0x06002A61 RID: 10849 RVA: 0x00106C08 File Offset: 0x00104E08
	public void OnFinishedAnimationCallback()
	{
		this.m_isOpened = true;
	}

	// Token: 0x06002A62 RID: 10850 RVA: 0x00106C14 File Offset: 0x00104E14
	public void OnFinishedCloseAnim()
	{
		RouletteManager.CloseRouletteWindow();
		this.m_isEnd = true;
		if (this.m_info.finishedCloseDelegate != null)
		{
			this.m_info.finishedCloseDelegate();
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06002A63 RID: 10851 RVA: 0x00106C50 File Offset: 0x00104E50
	public void SetDisableButton(bool disableButton)
	{
		this.m_disableButton = disableButton;
		foreach (UIButton uibutton in base.gameObject.GetComponentsInChildren<UIButton>(true))
		{
			uibutton.isEnabled = !this.m_disableButton;
		}
		foreach (UIImageButton uiimageButton in base.gameObject.GetComponentsInChildren<UIImageButton>(true))
		{
			uiimageButton.isEnabled = !this.m_disableButton;
		}
	}

	// Token: 0x06002A64 RID: 10852 RVA: 0x00106CD4 File Offset: 0x00104ED4
	private void SendButtonMessage(string patternName, string btnName)
	{
		Transform transform = base.gameObject.transform.Find(patternName);
		if (transform != null)
		{
			UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(transform.gameObject, btnName);
			if (uibuttonMessage != null)
			{
				uibuttonMessage.SendMessage("OnClick");
			}
		}
	}

	// Token: 0x06002A65 RID: 10853 RVA: 0x00106D24 File Offset: 0x00104F24
	public override void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (this.m_isOpened)
		{
			ItemGetWindow.ButtonType buttonType = this.m_info.buttonType;
			if (buttonType != ItemGetWindow.ButtonType.Ok)
			{
				if (buttonType == ItemGetWindow.ButtonType.TweetCancel)
				{
					this.SendButtonMessage("window/pattern_btn_use/pattern_1", "Btn_ok");
				}
			}
			else
			{
				this.SendButtonMessage("window/pattern_btn_use/pattern_0", "Btn_ok");
			}
		}
		if (msg != null)
		{
			msg.StaySequence();
		}
	}

	// Token: 0x040025B4 RID: 9652
	private ItemGetWindow.CInfo m_info = new ItemGetWindow.CInfo();

	// Token: 0x040025B5 RID: 9653
	private ItemGetWindow.Pressed m_pressed;

	// Token: 0x040025B6 RID: 9654
	[SerializeField]
	private GameObject m_imgView;

	// Token: 0x040025B7 RID: 9655
	[SerializeField]
	private GameObject m_imgEventTex;

	// Token: 0x040025B8 RID: 9656
	[SerializeField]
	private UITexture m_uiTexture;

	// Token: 0x040025B9 RID: 9657
	[SerializeField]
	private GameObject m_imgItem;

	// Token: 0x040025BA RID: 9658
	[SerializeField]
	private UISprite m_imgItemSprite;

	// Token: 0x040025BB RID: 9659
	[SerializeField]
	private UILabel m_imgName;

	// Token: 0x040025BC RID: 9660
	[SerializeField]
	private UILabel m_imgCount;

	// Token: 0x040025BD RID: 9661
	[SerializeField]
	private GameObject m_imgDecoEff;

	// Token: 0x040025BE RID: 9662
	private string m_itemImageSpriteName;

	// Token: 0x040025BF RID: 9663
	private bool m_disableButton;

	// Token: 0x040025C0 RID: 9664
	private bool m_isEnd = true;

	// Token: 0x040025C1 RID: 9665
	private bool m_isOpened;

	// Token: 0x0200055C RID: 1372
	public enum ButtonType
	{
		// Token: 0x040025C3 RID: 9667
		Ok,
		// Token: 0x040025C4 RID: 9668
		TweetCancel
	}

	// Token: 0x0200055D RID: 1373
	public class CInfo
	{
		// Token: 0x040025C5 RID: 9669
		public bool disableButton;

		// Token: 0x040025C6 RID: 9670
		public ItemGetWindow.ButtonType buttonType;

		// Token: 0x040025C7 RID: 9671
		public string name;

		// Token: 0x040025C8 RID: 9672
		public string caption;

		// Token: 0x040025C9 RID: 9673
		public int serverItemId = -1;

		// Token: 0x040025CA RID: 9674
		public string imageCount;

		// Token: 0x040025CB RID: 9675
		public ItemGetWindow.CInfo.FinishedCloseDelegate finishedCloseDelegate;

		// Token: 0x02000AA0 RID: 2720
		// (Invoke) Token: 0x060048BA RID: 18618
		public delegate void FinishedCloseDelegate();
	}

	// Token: 0x0200055E RID: 1374
	private struct Pressed
	{
		// Token: 0x040025CC RID: 9676
		public bool m_isButtonPressed;

		// Token: 0x040025CD RID: 9677
		public bool m_isOkButtonPressed;

		// Token: 0x040025CE RID: 9678
		public bool m_isYesButtonPressed;

		// Token: 0x040025CF RID: 9679
		public bool m_isNoButtonPressed;
	}
}
