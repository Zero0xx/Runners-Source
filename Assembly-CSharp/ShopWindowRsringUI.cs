using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using AnimationOrTween;
using Message;
using UnityEngine;

// Token: 0x02000548 RID: 1352
public class ShopWindowRsringUI : MonoBehaviour
{
	// Token: 0x060029E2 RID: 10722 RVA: 0x00102CD4 File Offset: 0x00100ED4
	private void Start()
	{
		if (this.m_blinderGameObject == null)
		{
			this.m_blinderGameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "blinder");
		}
		this.m_windowObject = GameObjectUtil.FindChildGameObject(base.gameObject, "window");
	}

	// Token: 0x060029E3 RID: 10723 RVA: 0x00102D20 File Offset: 0x00100F20
	private void Update()
	{
		this.UpdateView();
		if (this.m_ageVerification != null)
		{
			if (this.m_ageVerification.IsEnd)
			{
				this.m_ageVerification = null;
				this.OpenWindow(this.m_productIndex, this.m_exchangeItem, this.m_callback);
			}
		}
		else if (GeneralWindow.IsCreated("RsringCountError") || GeneralWindow.IsCreated("PurchaseFailed") || GeneralWindow.IsCreated("PurchaseCanceled") || GeneralWindow.IsCreated("AgeVerificationError"))
		{
			if (GeneralWindow.IsButtonPressed)
			{
				GeneralWindow.Close();
			}
		}
		else
		{
			ItemGetWindow itemGetWindow = ItemGetWindowUtil.GetItemGetWindow();
			if (itemGetWindow != null && itemGetWindow.IsCreated("PurchaseSuccess") && itemGetWindow.IsEnd)
			{
				itemGetWindow.Reset();
			}
		}
	}

	// Token: 0x060029E4 RID: 10724 RVA: 0x00102DFC File Offset: 0x00100FFC
	public void OpenWindow(int productIndex, ShopUI.EexchangeItem exchangeItem, ShopWindowRsringUI.PurchaseEndCallback callback)
	{
		BackKeyManager.AddWindowCallBack(base.gameObject);
		this.m_productIndex = productIndex;
		this.m_exchangeItem = exchangeItem;
		this.m_callback = callback;
		if (this.m_windowObject != null)
		{
			this.m_windowObject.SetActive(true);
		}
		SoundManager.SePlay("sys_window_open", "SE");
		this.UpdateView();
		Animation animation = GameObjectUtil.FindGameObjectComponent<Animation>("shop_rsring_window");
		if (animation != null)
		{
			ActiveAnimation.Play(animation, "ui_shop_window_Anim", Direction.Forward, EnableCondition.DoNothing, DisableCondition.DoNotDisable);
		}
	}

	// Token: 0x060029E5 RID: 10725 RVA: 0x00102E84 File Offset: 0x00101084
	private void OnClickClose()
	{
		SoundManager.SePlay("sys_window_close", "SE");
	}

	// Token: 0x060029E6 RID: 10726 RVA: 0x00102E98 File Offset: 0x00101098
	public void OnFinishedCloseAnim()
	{
		for (int i = 0; i < this.m_itemsGameObject.Length; i++)
		{
			this.m_itemsGameObject[i].SetActive(false);
		}
		this.m_panelGameObject.SetActive(false);
		if (this.m_windowObject != null)
		{
			this.m_windowObject.SetActive(false);
		}
		BackKeyManager.RemoveWindowCallBack(base.gameObject);
	}

	// Token: 0x060029E7 RID: 10727 RVA: 0x00102F00 File Offset: 0x00101100
	private void UpdateView()
	{
		if (this.m_exchangeItem == null)
		{
			return;
		}
		for (int i = 0; i < this.m_itemsGameObject.Length; i++)
		{
			this.m_itemsGameObject[i].SetActive(i == this.m_productIndex);
		}
		this.m_panelGameObject.SetActive(true);
		this.m_quantityLabel.text = this.m_exchangeItem.m_quantityLabel.text;
		this.m_costLabel.text = this.m_exchangeItem.m_costLabel.text;
		this.m_bonusGameObject.SetActive(this.m_exchangeItem.m_bonusGameObject != null && this.m_exchangeItem.m_bonusGameObject.activeSelf);
		if (this.m_exchangeItem.m_bonusGameObject != null)
		{
			for (int j = 0; j < this.m_bonusLabels.Length; j++)
			{
				this.m_bonusLabels[j].text = this.m_exchangeItem.m_bonusLabels[j].text;
			}
			this.m_presentLabel.text = this.m_exchangeItem.m_bonusLabels[0].text;
		}
		this.m_saleSprite.gameObject.SetActive(this.m_exchangeItem.m_saleSprite.gameObject.activeSelf);
		GameObject gameObject = ShopUI.CalcSaleIconObject(this.m_exchangeItem.m_saleSprite.gameObject, this.m_productIndex);
		if (gameObject != null)
		{
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(this.m_saleSprite.gameObject, "img_sale_icon");
			if (gameObject2 != null)
			{
				gameObject2.SetActive(gameObject.activeSelf);
			}
		}
	}

	// Token: 0x060029E8 RID: 10728 RVA: 0x001030A4 File Offset: 0x001012A4
	private void OnClickOk()
	{
		if ((ulong)(9999999U - SaveDataManager.Instance.ItemData.RedRingCount) < (ulong)((long)this.m_exchangeItem.quantity))
		{
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "RsringCountError",
				buttonType = GeneralWindow.ButtonType.Ok,
				caption = ShopUI.GetText("gw_rsring_count_error_caption", null),
				message = ShopUI.GetText("gw_rsring_count_error_text", null),
				isPlayErrorSe = true
			});
		}
		else if (!this.CheckPurchaseOver(HudUtility.GetMixedStringToInt(this.m_exchangeItem.m_costLabel.text)))
		{
			SoundManager.SePlay("sys_menu_decide", "SE");
			this.m_blinderGameObject.SetActive(true);
			NativeObserver instance = NativeObserver.Instance;
			bool flag = ServerInterface.LoggedInServerInterface != null;
			if (flag && instance != null)
			{
				instance.BuyProduct(instance.GetProductName(this.m_productIndex), new NativeObserver.PurchaseSuccessCallback(this.PurchaseSuccessCallback), new NativeObserver.PurchaseFailedCallback(this.PurchaseFailedCallback), new Action(this.PurchaseCanceledCallback));
			}
			else
			{
				base.StartCoroutine(this.PurchaseCallbackEmulateCoroutine(UnityEngine.Random.Range(0, 3)));
			}
		}
	}

	// Token: 0x060029E9 RID: 10729 RVA: 0x001031E8 File Offset: 0x001013E8
	private IEnumerator PurchaseCallbackEmulateCoroutine(int recultCode)
	{
		yield return new WaitForSeconds(1f);
		switch (recultCode)
		{
		case 0:
			if (ServerInterface.LoggedInServerInterface == null)
			{
				SaveDataManager.Instance.ItemData.RedRingCount += (uint)this.m_exchangeItem.quantity;
			}
			this.PurchaseSuccessCallback(0);
			break;
		case 1:
			this.PurchaseFailedCallback(NativeObserver.FailStatus.Deferred);
			break;
		case 2:
			this.PurchaseCanceledCallback();
			break;
		}
		yield break;
	}

	// Token: 0x060029EA RID: 10730 RVA: 0x00103214 File Offset: 0x00101414
	private bool CheckPurchaseOver(int addPurchase)
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		ServerSettingState serverSettingState;
		if (loggedInServerInterface != null)
		{
			serverSettingState = ServerInterface.SettingState;
		}
		else
		{
			serverSettingState = new ServerSettingState();
			serverSettingState.m_monthPurchase = 1000;
			serverSettingState.m_birthday = string.Empty;
			serverSettingState.m_birthday = "1990-9-30";
		}
		if (string.IsNullOrEmpty(serverSettingState.m_birthday))
		{
			this.m_ageVerification = base.gameObject.GetComponent<AgeVerification>();
			if (this.m_ageVerification == null)
			{
				this.m_ageVerification = base.gameObject.AddComponent<AgeVerification>();
			}
			this.m_ageVerification.Setup("Camera/menu_Anim/ShopPage/" + base.gameObject.name + "/Anchor_5_MC");
			this.m_ageVerification.PlayStart();
			return true;
		}
		bool flag = HudUtility.CheckPurchaseOver(serverSettingState.m_birthday, serverSettingState.m_monthPurchase, addPurchase);
		if (flag)
		{
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "AgeVerificationError",
				buttonType = GeneralWindow.ButtonType.Ok,
				caption = ShopUI.GetText("gw_age_verification_error_caption", null),
				message = ShopUI.GetText("gw_age_verification_error_text", new Dictionary<string, string>
				{
					{
						"{PURCHASE}",
						HudUtility.GetFormatNumString<int>(serverSettingState.m_monthPurchase)
					}
				}),
				isPlayErrorSe = true
			});
			return true;
		}
		return false;
	}

	// Token: 0x060029EB RID: 10731 RVA: 0x00103368 File Offset: 0x00101568
	private void PurchaseSuccessCallback(int scValue)
	{
		HudMenuUtility.SendMsgUpdateSaveDataDisplay();
		ItemGetWindow itemGetWindow = ItemGetWindowUtil.GetItemGetWindow();
		if (itemGetWindow != null)
		{
			itemGetWindow.Create(new ItemGetWindow.CInfo
			{
				name = "PurchaseSuccess",
				caption = ShopUI.GetText("gw_purchase_success_caption", null),
				serverItemId = this.m_exchangeItem.m_storeItem.m_itemId,
				imageCount = ShopUI.GetText("gw_purchase_success_text", new Dictionary<string, string>
				{
					{
						"{COUNT}",
						HudUtility.GetFormatNumString<int>(this.m_exchangeItem.quantity)
					}
				})
			});
		}
		SoundManager.SePlay("sys_buy_real_money", "SE");
		if (this.m_exchangeItem != null && this.m_exchangeItem.m_storeItem != null)
		{
			int price = this.m_exchangeItem.m_storeItem.m_price;
		}
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetRedStarExchangeList(0, base.gameObject);
		}
		else
		{
			this.ServerGetRedStarExchangeList_Succeeded(null);
		}
	}

	// Token: 0x060029EC RID: 10732 RVA: 0x0010346C File Offset: 0x0010166C
	private void PurchaseFailedCallback(NativeObserver.FailStatus status)
	{
		string cellName = "gw_purchase_failed_caption";
		string cellName2 = "gw_purchase_failed_text";
		if (status == NativeObserver.FailStatus.Deferred)
		{
			cellName = "gw_purchase_deferred_caption";
			cellName2 = "gw_purchase_deferred_text";
		}
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = "PurchaseFailed",
			buttonType = GeneralWindow.ButtonType.Ok,
			caption = ShopUI.GetText(cellName, null),
			message = ShopUI.GetText(cellName2, null),
			isPlayErrorSe = true
		});
		this.m_blinderGameObject.SetActive(false);
		if (this.m_callback != null)
		{
			this.m_callback(false);
			this.m_callback = null;
		}
	}

	// Token: 0x060029ED RID: 10733 RVA: 0x0010350C File Offset: 0x0010170C
	private void PurchaseCanceledCallback()
	{
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = "PurchaseCanceled",
			buttonType = GeneralWindow.ButtonType.Ok,
			caption = ShopUI.GetText("gw_purchase_canceled_caption", null),
			message = ShopUI.GetText("gw_purchase_canceled_text", null),
			isPlayErrorSe = true
		});
		this.m_blinderGameObject.SetActive(false);
		if (this.m_callback != null)
		{
			this.m_callback(false);
			this.m_callback = null;
		}
	}

	// Token: 0x060029EE RID: 10734 RVA: 0x00103594 File Offset: 0x00101794
	[Conditional("DEBUG_INFO")]
	private static void DebugLog(string s)
	{
		global::Debug.Log("@ms " + s);
	}

	// Token: 0x060029EF RID: 10735 RVA: 0x001035A8 File Offset: 0x001017A8
	[Conditional("DEBUG_INFO")]
	private static void DebugLogWarning(string s)
	{
		global::Debug.LogWarning("@ms " + s);
	}

	// Token: 0x060029F0 RID: 10736 RVA: 0x001035BC File Offset: 0x001017BC
	private void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (this.m_windowObject == null)
		{
			return;
		}
		if (!this.m_windowObject.activeSelf)
		{
			return;
		}
		msg.StaySequence();
		UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(base.gameObject, "Btn_close");
		if (uibuttonMessage != null)
		{
			uibuttonMessage.SendMessage("OnClick");
		}
	}

	// Token: 0x060029F1 RID: 10737 RVA: 0x0010361C File Offset: 0x0010181C
	private void ServerGetRedStarExchangeList_Succeeded(MsgGetRedStarExchangeListSucceed msg)
	{
		this.m_blinderGameObject.SetActive(false);
		if (this.m_callback != null)
		{
			this.m_callback(true);
			this.m_callback = null;
		}
	}

	// Token: 0x04002500 RID: 9472
	[SerializeField]
	private GameObject[] m_itemsGameObject = new GameObject[5];

	// Token: 0x04002501 RID: 9473
	[SerializeField]
	private GameObject m_panelGameObject;

	// Token: 0x04002502 RID: 9474
	[SerializeField]
	public UILabel m_quantityLabel;

	// Token: 0x04002503 RID: 9475
	[SerializeField]
	public UILabel m_costLabel;

	// Token: 0x04002504 RID: 9476
	[SerializeField]
	public GameObject m_bonusGameObject;

	// Token: 0x04002505 RID: 9477
	[SerializeField]
	public UILabel[] m_bonusLabels = new UILabel[2];

	// Token: 0x04002506 RID: 9478
	[SerializeField]
	public UISprite m_saleSprite;

	// Token: 0x04002507 RID: 9479
	[SerializeField]
	private GameObject m_blinderGameObject;

	// Token: 0x04002508 RID: 9480
	private int m_productIndex;

	// Token: 0x04002509 RID: 9481
	private ShopUI.EexchangeItem m_exchangeItem;

	// Token: 0x0400250A RID: 9482
	private AgeVerification m_ageVerification;

	// Token: 0x0400250B RID: 9483
	private GameObject m_windowObject;

	// Token: 0x0400250C RID: 9484
	private ShopWindowRsringUI.PurchaseEndCallback m_callback;

	// Token: 0x0400250D RID: 9485
	[SerializeField]
	public UILabel m_presentLabel;

	// Token: 0x02000A9E RID: 2718
	// (Invoke) Token: 0x060048B2 RID: 18610
	public delegate void PurchaseEndCallback(bool isSuccess);
}
