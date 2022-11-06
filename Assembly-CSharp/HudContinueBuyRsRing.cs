using System;
using System.Collections;
using System.Collections.Generic;
using AnimationOrTween;
using DataTable;
using Message;
using Text;
using UnityEngine;

// Token: 0x0200036D RID: 877
public class HudContinueBuyRsRing : MonoBehaviour
{
	// Token: 0x060019FE RID: 6654 RVA: 0x00098384 File Offset: 0x00096584
	public void Setup()
	{
		UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(base.gameObject, "Btn_item_rs_1");
		if (uibuttonMessage != null)
		{
			uibuttonMessage.target = base.gameObject;
			uibuttonMessage.functionName = "OnClickBuyButton";
		}
		UIButtonMessage uibuttonMessage2 = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(base.gameObject, "Btn_shop_legal");
		if (uibuttonMessage2 != null)
		{
			uibuttonMessage2.target = base.gameObject;
			uibuttonMessage2.functionName = "OnClickTradeLowButton";
		}
		UIButtonMessage uibuttonMessage3 = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(base.gameObject, "Btn_close");
		if (uibuttonMessage3 != null)
		{
			uibuttonMessage3.target = base.gameObject;
			uibuttonMessage3.functionName = "OnClickCloseButton";
		}
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "img_sale_icon_1");
		if (gameObject != null)
		{
			gameObject.SetActive(false);
		}
		NativeObserver instance = NativeObserver.Instance;
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (instance != null && loggedInServerInterface != null)
		{
			this.FinishedToGetProductList(NativeObserver.IAPResult.ProductsRequestCompleted);
		}
	}

	// Token: 0x060019FF RID: 6655 RVA: 0x00098480 File Offset: 0x00096680
	public void PlayStart()
	{
		base.gameObject.SetActive(true);
		Animation component = base.gameObject.GetComponent<Animation>();
		if (component != null)
		{
			ActiveAnimation.Play(component, Direction.Forward);
		}
		ServerCampaignData serverCampaignData = null;
		int num = 10;
		int num2 = num;
		if (GameObject.Find("ServerInterface") != null && ServerInterface.RedStarItemList != null && ServerInterface.RedStarItemList.Count > 0)
		{
			ServerRedStarItemState serverRedStarItemState = ServerInterface.RedStarItemList[HudContinueBuyRsRing.ProductIndex];
			if (serverRedStarItemState != null)
			{
				foreach (Constants.Campaign.emType campaignType in this.CampaignTypeList)
				{
					int storeItemId = serverRedStarItemState.m_storeItemId;
					serverCampaignData = ServerInterface.CampaignState.GetCampaignInSession(campaignType, storeItemId);
					if (serverCampaignData != null)
					{
						break;
					}
				}
				float num3 = ServerCampaignData.fContentBasis;
				if (serverCampaignData != null)
				{
					num3 = (float)serverCampaignData.iContent;
				}
				num2 = serverRedStarItemState.m_numItem;
				num = (int)((float)num2 * num3 / ServerCampaignData.fContentBasis);
			}
		}
		bool flag = serverCampaignData != null;
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "img_sale_icon_1");
		if (gameObject != null)
		{
			gameObject.SetActive(flag);
		}
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, "img_bonus_icon_bg_1");
		if (gameObject2 != null)
		{
			gameObject2.SetActive(flag);
		}
		if (flag)
		{
			GameObject gameObject3 = GameObjectUtil.FindChildGameObject(base.gameObject, "img_sale_icon_bg_1");
			if (gameObject3 != null)
			{
				gameObject3.SetActive(true);
				UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject3, "Lbl_rs_gift_1");
				if (uilabel != null)
				{
					int num4 = num - num2;
					TextObject text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Shop", "label_rsring_bonus");
					if (text != null)
					{
						text.ReplaceTag("{COUNT}", HudUtility.GetFormatNumString<int>(num4));
						uilabel.text = text.text;
						UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(uilabel.gameObject, "Lbl_bonus_icon_sdw_1");
						if (uilabel2 != null)
						{
							uilabel2.text = text.text;
						}
					}
				}
			}
		}
		else
		{
			GameObject gameObject4 = GameObjectUtil.FindChildGameObject(base.gameObject, "img_sale_icon_bg_1");
			if (gameObject4 != null)
			{
				gameObject4.SetActive(false);
			}
		}
		UILabel uilabel3 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_rs_quantity_1");
		if (uilabel3 != null)
		{
			uilabel3.text = num2.ToString();
		}
		this.m_isEndPlay = false;
		this.m_state = HudContinueBuyRsRing.State.WAIT_CLICK_BUTTON;
		this.m_result = HudContinueBuyRsRing.Result.NONE;
	}

	// Token: 0x170003D3 RID: 979
	// (get) Token: 0x06001A00 RID: 6656 RVA: 0x00098730 File Offset: 0x00096930
	// (set) Token: 0x06001A01 RID: 6657 RVA: 0x00098738 File Offset: 0x00096938
	public bool IsEndPlay
	{
		get
		{
			return this.m_isEndPlay;
		}
		private set
		{
		}
	}

	// Token: 0x170003D4 RID: 980
	// (get) Token: 0x06001A02 RID: 6658 RVA: 0x0009873C File Offset: 0x0009693C
	public bool IsSuccess
	{
		get
		{
			return this.m_result == HudContinueBuyRsRing.Result.SUCCESS;
		}
	}

	// Token: 0x170003D5 RID: 981
	// (get) Token: 0x06001A03 RID: 6659 RVA: 0x00098748 File Offset: 0x00096948
	public bool IsFailed
	{
		get
		{
			return this.m_result == HudContinueBuyRsRing.Result.FAILED;
		}
	}

	// Token: 0x170003D6 RID: 982
	// (get) Token: 0x06001A04 RID: 6660 RVA: 0x00098754 File Offset: 0x00096954
	public bool IsCanceled
	{
		get
		{
			return this.m_result == HudContinueBuyRsRing.Result.CANCELED;
		}
	}

	// Token: 0x06001A05 RID: 6661 RVA: 0x00098760 File Offset: 0x00096960
	private void Start()
	{
	}

	// Token: 0x06001A06 RID: 6662 RVA: 0x00098764 File Offset: 0x00096964
	private void Update()
	{
		switch (this.m_state)
		{
		case HudContinueBuyRsRing.State.SHOW_RESULT:
			if (GeneralWindow.IsOkButtonPressed)
			{
				GeneralWindow.Close();
			}
			break;
		}
		if ((GeneralWindow.IsCreated("RsrBuyLegal") || GeneralWindow.IsCreated("AgeVerificationError")) && GeneralWindow.IsButtonPressed)
		{
			GeneralWindow.Close();
		}
	}

	// Token: 0x06001A07 RID: 6663 RVA: 0x000987E8 File Offset: 0x000969E8
	private void FinishedToGetProductList(NativeObserver.IAPResult result)
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "item_1");
		if (gameObject == null)
		{
			return;
		}
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, "Lbl_price_1");
		if (uilabel == null)
		{
			return;
		}
		NativeObserver instance = NativeObserver.Instance;
		if (instance == null)
		{
			return;
		}
		string productPrice = instance.GetProductPrice(instance.GetProductName(HudContinueBuyRsRing.ProductIndex));
		if (productPrice == null)
		{
			return;
		}
		uilabel.text = productPrice;
	}

	// Token: 0x06001A08 RID: 6664 RVA: 0x00098860 File Offset: 0x00096A60
	private void OnClickBuyButton()
	{
		SoundManager.SePlay("sys_menu_decide", "SE");
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			ServerSettingState settingState = ServerInterface.SettingState;
			NativeObserver instance = NativeObserver.Instance;
			string productName = instance.GetProductName(HudContinueBuyRsRing.ProductIndex);
			int mixedStringToInt = HudUtility.GetMixedStringToInt(instance.GetProductPrice(productName));
			if (!HudUtility.CheckPurchaseOver(settingState.m_birthday, settingState.m_monthPurchase, mixedStringToInt))
			{
				instance.BuyProduct(productName, new NativeObserver.PurchaseSuccessCallback(this.PurchaseSuccessCallback), new NativeObserver.PurchaseFailedCallback(this.PurchaseFailedCallback), new Action(this.PurchaseCanceledCallback));
			}
			else
			{
				GeneralWindow.Create(new GeneralWindow.CInfo
				{
					name = "AgeVerificationError",
					caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Shop", "gw_age_verification_error_caption").text,
					message = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Shop", "gw_age_verification_error_text").text,
					anchor_path = "Camera/Anchor_5_MC",
					buttonType = GeneralWindow.ButtonType.Ok
				});
			}
		}
	}

	// Token: 0x06001A09 RID: 6665 RVA: 0x0009896C File Offset: 0x00096B6C
	private void OnClickTradeLowButton()
	{
		base.StartCoroutine(this.OpenLegalWindow());
	}

	// Token: 0x06001A0A RID: 6666 RVA: 0x0009897C File Offset: 0x00096B7C
	private IEnumerator OpenLegalWindow()
	{
		SoundManager.SePlay("sys_menu_decide", "SE");
		string url = NetUtil.GetWebPageURL(InformationDataTable.Type.SHOP_LEGAL);
		GameObject htmlParserGameObject = HtmlParserFactory.Create(url, HtmlParser.SyncType.TYPE_ASYNC, HtmlParser.SyncType.TYPE_ASYNC);
		if (htmlParserGameObject == null)
		{
			yield return null;
		}
		HtmlParser htmlParser = htmlParserGameObject.GetComponent<HtmlParser>();
		if (htmlParser == null)
		{
			yield return null;
		}
		if (htmlParser != null)
		{
			while (!htmlParser.IsEndParse)
			{
				yield return null;
			}
			string legalText = htmlParser.ParsedString;
			UnityEngine.Object.Destroy(htmlParserGameObject);
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "RsrBuyLegal",
				anchor_path = "Camera/Anchor_5_MC",
				buttonType = GeneralWindow.ButtonType.Ok,
				caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Title", "gw_legal_caption").text,
				message = legalText
			});
		}
		yield break;
	}

	// Token: 0x06001A0B RID: 6667 RVA: 0x00098990 File Offset: 0x00096B90
	private void OnClickCloseButton()
	{
		SoundManager.SePlay("sys_window_close", "SE");
		Animation component = base.gameObject.GetComponent<Animation>();
		if (component != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(base.animation, Direction.Reverse);
			if (activeAnimation != null)
			{
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnFinishedAnimationCallbck), true);
			}
		}
	}

	// Token: 0x06001A0C RID: 6668 RVA: 0x000989F8 File Offset: 0x00096BF8
	public void OnPushBackKey()
	{
		this.OnClickCloseButton();
	}

	// Token: 0x06001A0D RID: 6669 RVA: 0x00098A00 File Offset: 0x00096C00
	private void PurchaseSuccessCallback(int result)
	{
		SoundManager.SePlay("sys_buy_real_money", "SE");
		global::Debug.Log("HudContinue.PurchaseSuccessCallback");
		string replaceString = "1";
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			ServerRedStarItemState serverRedStarItemState = ServerInterface.RedStarItemList[HudContinueBuyRsRing.ProductIndex];
			if (serverRedStarItemState != null)
			{
				replaceString = serverRedStarItemState.m_numItem.ToString();
			}
		}
		TextObject text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Shop", "gw_purchase_success_text");
		if (text != null)
		{
			text.ReplaceTag("{COUNT}", replaceString);
		}
		if (ServerInterface.LoggedInServerInterface != null)
		{
			int storeItemId = ServerInterface.RedStarItemList[HudContinueBuyRsRing.ProductIndex].m_storeItemId;
			global::Debug.Log("HudContinueBuyRsRing.PurchaseSuccessCallback:result = " + storeItemId.ToString());
		}
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetRedStarExchangeList(0, base.gameObject);
		}
		else
		{
			this.ServerGetRedStarExchangeList_Succeeded(null);
		}
	}

	// Token: 0x06001A0E RID: 6670 RVA: 0x00098AE8 File Offset: 0x00096CE8
	private void ServerGetRedStarExchangeList_Succeeded(MsgGetRedStarExchangeListSucceed msg)
	{
		Animation component = base.gameObject.GetComponent<Animation>();
		if (component != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(base.animation, Direction.Reverse);
			if (activeAnimation != null)
			{
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnFinishedAnimationCallbck), true);
			}
		}
		this.m_state = HudContinueBuyRsRing.State.SHOW_RESULT;
		this.m_result = HudContinueBuyRsRing.Result.SUCCESS;
	}

	// Token: 0x06001A0F RID: 6671 RVA: 0x00098B4C File Offset: 0x00096D4C
	private void PurchaseFailedCallback(NativeObserver.FailStatus status)
	{
		string cellID = "gw_purchase_failed_caption";
		string cellID2 = "gw_purchase_failed_text";
		if (status == NativeObserver.FailStatus.Deferred)
		{
			cellID = "gw_purchase_deferred_caption";
			cellID2 = "gw_purchase_deferred_text";
		}
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			caption = TextUtility.GetCommonText("Shop", cellID),
			message = TextUtility.GetCommonText("Shop", cellID2),
			anchor_path = "Camera/Anchor_5_MC",
			buttonType = GeneralWindow.ButtonType.Ok,
			isPlayErrorSe = true
		});
		this.m_state = HudContinueBuyRsRing.State.SHOW_RESULT;
		this.m_result = HudContinueBuyRsRing.Result.FAILED;
	}

	// Token: 0x06001A10 RID: 6672 RVA: 0x00098BD8 File Offset: 0x00096DD8
	private void PurchaseCanceledCallback()
	{
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			caption = TextUtility.GetCommonText("Shop", "gw_purchase_canceled_caption"),
			message = TextUtility.GetCommonText("Shop", "gw_purchase_canceled_text"),
			anchor_path = "Camera/Anchor_5_MC",
			buttonType = GeneralWindow.ButtonType.Ok,
			isPlayErrorSe = true
		});
		this.m_state = HudContinueBuyRsRing.State.SHOW_RESULT;
		this.m_result = HudContinueBuyRsRing.Result.CANCELED;
	}

	// Token: 0x06001A11 RID: 6673 RVA: 0x00098C4C File Offset: 0x00096E4C
	private void OnFinishedAnimationCallbck()
	{
		base.gameObject.SetActive(false);
		this.m_isEndPlay = true;
		this.m_state = HudContinueBuyRsRing.State.IDLE;
		this.m_result = HudContinueBuyRsRing.Result.CANCELED;
	}

	// Token: 0x0400176A RID: 5994
	private bool m_isEndPlay;

	// Token: 0x0400176B RID: 5995
	private HudContinueBuyRsRing.State m_state;

	// Token: 0x0400176C RID: 5996
	private HudContinueBuyRsRing.Result m_result = HudContinueBuyRsRing.Result.NONE;

	// Token: 0x0400176D RID: 5997
	private static readonly int ProductIndex;

	// Token: 0x0400176E RID: 5998
	public ServerCampaignData m_campaignData;

	// Token: 0x0400176F RID: 5999
	private List<Constants.Campaign.emType> CampaignTypeList = new List<Constants.Campaign.emType>
	{
		Constants.Campaign.emType.PurchaseAddRsrings,
		Constants.Campaign.emType.PurchaseAddRsringsNoChargeUser
	};

	// Token: 0x0200036E RID: 878
	private enum State
	{
		// Token: 0x04001771 RID: 6001
		IDLE,
		// Token: 0x04001772 RID: 6002
		WAIT_CLICK_BUTTON,
		// Token: 0x04001773 RID: 6003
		PURCHASING,
		// Token: 0x04001774 RID: 6004
		SHOW_RESULT
	}

	// Token: 0x0200036F RID: 879
	private enum Result
	{
		// Token: 0x04001776 RID: 6006
		NONE = -1,
		// Token: 0x04001777 RID: 6007
		SUCCESS,
		// Token: 0x04001778 RID: 6008
		FAILED,
		// Token: 0x04001779 RID: 6009
		CANCELED
	}
}
