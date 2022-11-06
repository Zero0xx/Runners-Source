using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DataTable;
using NoahUnity;
using Text;
using UnityEngine;

// Token: 0x02000541 RID: 1345
public class ShopUI : MonoBehaviour
{
	// Token: 0x17000578 RID: 1400
	// (get) Token: 0x06002983 RID: 10627 RVA: 0x00100904 File Offset: 0x000FEB04
	public bool IsInitShop
	{
		get
		{
			return this.m_isInitShop;
		}
	}

	// Token: 0x06002984 RID: 10628 RVA: 0x0010090C File Offset: 0x000FEB0C
	private void SetCurrentType(ShopUI.ServerEexchangeType type)
	{
		for (int i = 0; i < 3; i++)
		{
			if (this.m_exchangeObjects[i] != null)
			{
				if (type == (ShopUI.ServerEexchangeType)i)
				{
					this.m_exchangeObjects[i].SetActive(true);
				}
				else
				{
					this.m_exchangeObjects[i].SetActive(false);
				}
			}
		}
	}

	// Token: 0x17000579 RID: 1401
	// (get) Token: 0x06002985 RID: 10629 RVA: 0x00100968 File Offset: 0x000FEB68
	// (set) Token: 0x06002986 RID: 10630 RVA: 0x00100970 File Offset: 0x000FEB70
	private bool isStarted { get; set; }

	// Token: 0x06002987 RID: 10631 RVA: 0x0010097C File Offset: 0x000FEB7C
	public static GameObject CalcSaleIconObject(GameObject rootObject, int itemIndex)
	{
		if (rootObject == null)
		{
			return null;
		}
		string name = "img_sale_icon_" + (itemIndex + 1).ToString();
		return GameObjectUtil.FindChildGameObject(rootObject, name);
	}

	// Token: 0x06002988 RID: 10632 RVA: 0x001009B8 File Offset: 0x000FEBB8
	private void Start()
	{
		this.isStarted = true;
	}

	// Token: 0x06002989 RID: 10633 RVA: 0x001009C4 File Offset: 0x000FEBC4
	private void OnStartShopRedStarRing()
	{
		this.SetCurrentType(ShopUI.ServerEexchangeType.RSRING);
		base.StartCoroutine(this.StartShopCoroutine("Btn_tab_rsring"));
	}

	// Token: 0x0600298A RID: 10634 RVA: 0x001009E0 File Offset: 0x000FEBE0
	private void OnStartShopRing()
	{
		this.SetCurrentType(ShopUI.ServerEexchangeType.RING);
		base.StartCoroutine(this.StartShopCoroutine("Btn_tab_ring"));
	}

	// Token: 0x0600298B RID: 10635 RVA: 0x001009FC File Offset: 0x000FEBFC
	private void OnStartShopChallenge()
	{
		this.SetCurrentType(ShopUI.ServerEexchangeType.CHALLENGE);
		base.StartCoroutine(this.StartShopCoroutine("Btn_tab_challenge"));
	}

	// Token: 0x0600298C RID: 10636 RVA: 0x00100A18 File Offset: 0x000FEC18
	private void OnStartShopEvent()
	{
		if (ShopUI.isRaidbossEvent())
		{
			base.StartCoroutine(this.StartShopCoroutine("Btn_tab_raidboss"));
		}
		else
		{
			this.OnStartShopChallenge();
		}
	}

	// Token: 0x0600298D RID: 10637 RVA: 0x00100A44 File Offset: 0x000FEC44
	private IEnumerator StartShopCoroutine(string buttonName)
	{
		yield return null;
		UIToggle uiToggle = GameObjectUtil.FindChildGameObjectComponent<UIToggle>(base.gameObject.transform.root.gameObject, buttonName);
		if (uiToggle != null)
		{
			uiToggle.value = true;
		}
		for (ShopUI.ServerEexchangeType type = ShopUI.ServerEexchangeType.RSRING; type < ShopUI.ServerEexchangeType.Count; type++)
		{
			ShopUI.EexchangeType exchangeType = this.m_exchangeTypes[(int)type];
			if (exchangeType != null)
			{
				foreach (ShopUI.EexchangeItem exchangeItem in exchangeType.m_exchangeItems)
				{
					if (exchangeItem != null)
					{
						exchangeItem.m_quantityLabel.gameObject.SetActive(false);
						exchangeItem.m_costLabel.gameObject.SetActive(false);
						if (exchangeItem.m_bonusGameObject != null)
						{
							exchangeItem.m_bonusGameObject.SetActive(false);
						}
						exchangeItem.m_saleSprite.gameObject.SetActive(false);
					}
				}
			}
		}
		this.OnStartShop();
		yield break;
	}

	// Token: 0x0600298E RID: 10638 RVA: 0x00100A70 File Offset: 0x000FEC70
	private void OnStartShop()
	{
		HudMenuUtility.SendEnableShopButton(false);
		if (this.m_tabRSR == null)
		{
			this.m_tabRSR = GameObjectUtil.FindChildGameObject(base.gameObject.transform.root.gameObject, "tab_rsring");
		}
		if (!ServerInterface.IsRSREnable())
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject.transform.root.gameObject, "Btn_tab_rsring");
			if (gameObject != null)
			{
				gameObject.SetActive(false);
			}
		}
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			this.SetExchangeItems(ShopUI.ServerEexchangeType.RSRING, ServerInterface.RedStarItemList);
			this.SetExchangeItems(ShopUI.ServerEexchangeType.RING, ServerInterface.RedStarExchangeRingItemList);
			this.SetExchangeItems(ShopUI.ServerEexchangeType.CHALLENGE, ServerInterface.RedStarExchangeEnergyItemList);
		}
		NativeObserver instance = NativeObserver.Instance;
		List<string> list = new List<string>();
		for (int i = 0; i < NativeObserver.ProductCount; i++)
		{
			list.Add(instance.GetProductName(i));
		}
		instance.RequestProductsInfo(list, new NativeObserver.IAPDelegate(this.FinishedToGetProductList));
		this.m_freeGetButton = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_shop_free_get_rt");
		if (this.m_freeGetButton != null)
		{
			this.m_freeGetButton.SetActive(false);
			UIButtonMessage uibuttonMessage = this.m_freeGetButton.GetComponent<UIButtonMessage>();
			if (uibuttonMessage == null)
			{
				uibuttonMessage = this.m_freeGetButton.AddComponent<UIButtonMessage>();
			}
			uibuttonMessage.target = base.gameObject;
			uibuttonMessage.functionName = "OnClickGetFreeRsRing";
			uibuttonMessage.enabled = true;
		}
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_shop_free_get_ct");
		if (gameObject2 != null)
		{
			gameObject2.SetActive(false);
		}
		GameObject gameObject3 = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_shop_legal");
		if (gameObject3 != null)
		{
			RegionManager instance2 = RegionManager.Instance;
			if (instance2 != null)
			{
				bool active = false;
				if (instance2.IsJapan())
				{
					active = true;
				}
				gameObject3.SetActive(active);
			}
		}
		GameObject gameObject4 = GameObjectUtil.FindChildGameObject(base.gameObject, "billing_notice");
		if (gameObject4 != null && loggedInServerInterface != null && ServerInterface.SettingState != null)
		{
			if (ServerInterface.SettingState.m_isPurchased || !ServerInterface.IsRSREnable())
			{
				gameObject4.SetActive(false);
			}
			else
			{
				gameObject4.SetActive(true);
			}
		}
		GameObject gameObject5 = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_tab_raidboss");
		if (gameObject5 != null)
		{
			gameObject5.SetActive(false);
		}
		this.m_isInitShop = true;
	}

	// Token: 0x0600298F RID: 10639 RVA: 0x00100CF4 File Offset: 0x000FEEF4
	private void SetExchangeItems(ShopUI.ServerEexchangeType type, List<ServerRedStarItemState> storeItems)
	{
		ShopUI.EexchangeType eexchangeType = this.m_exchangeTypes[(int)type];
		if (eexchangeType == null)
		{
			return;
		}
		for (int i = 0; i < 5; i++)
		{
			if (i < storeItems.Count)
			{
				ShopUI.EexchangeItem eexchangeItem = eexchangeType.m_exchangeItems[i];
				eexchangeItem.m_storeItem = storeItems[i];
				eexchangeItem.m_campaignData = null;
				foreach (Constants.Campaign.emType campaignType in ShopUI.s_campaignTypes[(int)type])
				{
					eexchangeItem.m_campaignData = ServerInterface.CampaignState.GetCampaignInSession(campaignType, eexchangeItem.m_storeItem.m_storeItemId);
					if (eexchangeItem.m_campaignData != null)
					{
						break;
					}
				}
			}
			else if (type == ShopUI.ServerEexchangeType.RSRING)
			{
				this.SetRSRItemBtn(i);
			}
		}
	}

	// Token: 0x06002990 RID: 10640 RVA: 0x00100DE0 File Offset: 0x000FEFE0
	private void SetRSRItemBtn(int index)
	{
		if (this.m_tabRSR != null)
		{
			int num = index + 1;
			GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_tabRSR, "item_" + num.ToString());
			if (gameObject != null)
			{
				GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "Btn_item_rs_" + num.ToString());
				if (gameObject2 != null)
				{
					UIImageButton component = gameObject2.GetComponent<UIImageButton>();
					if (component != null)
					{
						component.isEnabled = false;
					}
				}
			}
		}
	}

	// Token: 0x06002991 RID: 10641 RVA: 0x00100E6C File Offset: 0x000FF06C
	private void OnShopBackButtonClicked()
	{
		NativeObserver instance = NativeObserver.Instance;
		if (instance != null)
		{
			instance.ResetIapDelegate();
		}
		HudMenuUtility.SendEnableShopButton(true);
	}

	// Token: 0x06002992 RID: 10642 RVA: 0x00100E98 File Offset: 0x000FF098
	private void FinishedToGetProductList(NativeObserver.IAPResult result)
	{
		string text = string.Format("Finished to get Product List from AppStore!(result:{0})", (int)result);
		ShopUI.EexchangeType eexchangeType = this.m_exchangeTypes[0];
		if (eexchangeType == null)
		{
			return;
		}
		for (int i = 0; i < 5; i++)
		{
			ShopUI.EexchangeItem eexchangeItem = eexchangeType.m_exchangeItems[i];
			if (eexchangeItem.m_storeItem != null)
			{
				string productPrice = NativeObserver.Instance.GetProductPrice(eexchangeItem.m_storeItem.m_productId);
				if (productPrice != null)
				{
					this.m_redStarPrice[i] = productPrice;
				}
				else
				{
					this.m_redStarPrice[i] = eexchangeItem.m_storeItem.m_priceDisp;
				}
			}
			else
			{
				this.m_redStarPrice[i] = string.Empty;
			}
		}
		Noah.Instance.Connect(NoahHandler.consumer_key, NoahHandler.secret_key, NoahHandler.action_id);
		this.UpdateView();
	}

	// Token: 0x06002993 RID: 10643 RVA: 0x00100F60 File Offset: 0x000FF160
	private void UpdateView()
	{
		if (!this.m_isInitShop)
		{
			return;
		}
		for (ShopUI.ServerEexchangeType serverEexchangeType = ShopUI.ServerEexchangeType.RSRING; serverEexchangeType < ShopUI.ServerEexchangeType.Count; serverEexchangeType++)
		{
			ShopUI.EexchangeType eexchangeType = this.m_exchangeTypes[(int)serverEexchangeType];
			if (eexchangeType != null)
			{
				for (int i = 0; i < 5; i++)
				{
					ShopUI.EexchangeItem eexchangeItem = eexchangeType.m_exchangeItems[i];
					if (eexchangeItem != null)
					{
						if (eexchangeItem.m_storeItem != null)
						{
							int num = eexchangeItem.m_storeItem.m_numItem - this.GetBonusCount(serverEexchangeType, i);
							eexchangeItem.m_quantityLabel.text = HudUtility.GetFormatNumString<int>(num);
							eexchangeItem.m_quantityLabel.gameObject.SetActive(true);
							if (serverEexchangeType == ShopUI.ServerEexchangeType.RSRING)
							{
								if (!string.IsNullOrEmpty(this.m_redStarPrice[i]))
								{
									eexchangeItem.m_costLabel.text = this.m_redStarPrice[i];
								}
							}
							else
							{
								eexchangeItem.m_costLabel.text = HudUtility.GetFormatNumString<int>(eexchangeItem.m_storeItem.m_price);
							}
							eexchangeItem.m_costLabel.gameObject.SetActive(true);
							string presentString = this.GetPresentString(serverEexchangeType, i);
							bool active = presentString != null;
							eexchangeItem.m_saleSprite.gameObject.SetActive(active);
							bool isCampaign = eexchangeItem.isCampaign;
							GameObject gameObject = ShopUI.CalcSaleIconObject(eexchangeItem.m_saleSprite.gameObject, i);
							if (gameObject != null)
							{
								gameObject.SetActive(isCampaign);
							}
							if (eexchangeItem.m_bonusGameObject != null && presentString != null)
							{
								eexchangeItem.m_saleQuantityLabel.text = presentString;
								foreach (UILabel uilabel in eexchangeItem.m_bonusLabels)
								{
									uilabel.text = presentString;
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06002994 RID: 10644 RVA: 0x00101118 File Offset: 0x000FF318
	private string GetPresentString(ShopUI.ServerEexchangeType type, int itemIndex)
	{
		ShopUI.EexchangeType eexchangeType = this.m_exchangeTypes[(int)type];
		if (eexchangeType == null)
		{
			return null;
		}
		if (eexchangeType.m_exchangeItems[itemIndex] == null)
		{
			return null;
		}
		int numItem = eexchangeType.m_exchangeItems[itemIndex].m_storeItem.m_numItem;
		ShopUI.EexchangeItem eexchangeItem = eexchangeType.m_exchangeItems[itemIndex];
		bool isCampaign = eexchangeItem.isCampaign;
		if (isCampaign)
		{
			int quantity = eexchangeItem.quantity;
			int num = quantity - numItem;
			int bonusCount = this.GetBonusCount(type, itemIndex);
			int bonusCount2 = num + bonusCount;
			return this.CalcBonusString(type, bonusCount2);
		}
		return this.CalcBonusString(type, this.GetBonusCount(type, itemIndex));
	}

	// Token: 0x06002995 RID: 10645 RVA: 0x001011A8 File Offset: 0x000FF3A8
	private int GetBonusCount(ShopUI.ServerEexchangeType type, int itemIndex)
	{
		ShopUI.EexchangeType eexchangeType = this.m_exchangeTypes[(int)type];
		if (eexchangeType == null)
		{
			return 0;
		}
		int numItem = eexchangeType.m_exchangeItems[0].m_storeItem.m_numItem;
		int num = (type != ShopUI.ServerEexchangeType.RSRING) ? eexchangeType.m_exchangeItems[0].m_storeItem.m_price : HudUtility.GetMixedStringToInt(eexchangeType.m_exchangeItems[0].m_costLabel.text);
		ShopUI.EexchangeItem eexchangeItem = eexchangeType.m_exchangeItems[itemIndex];
		if (eexchangeItem == null)
		{
			return 0;
		}
		int numItem2 = eexchangeType.m_exchangeItems[itemIndex].m_storeItem.m_numItem;
		int num2 = (type != ShopUI.ServerEexchangeType.RSRING) ? eexchangeItem.m_storeItem.m_price : HudUtility.GetMixedStringToInt(eexchangeItem.m_costLabel.text);
		return (int)((float)numItem2 - (float)numItem * (float)num2 / (float)num);
	}

	// Token: 0x06002996 RID: 10646 RVA: 0x00101270 File Offset: 0x000FF470
	private string CalcBonusString(ShopUI.ServerEexchangeType type, int bonusCount)
	{
		string result = null;
		if (bonusCount > 0)
		{
			result = ShopUI.GetText("label_" + type.ToString().ToLower() + "_bonus", new Dictionary<string, string>
			{
				{
					"{COUNT}",
					HudUtility.GetFormatNumString<int>(bonusCount)
				}
			});
		}
		return result;
	}

	// Token: 0x06002997 RID: 10647 RVA: 0x001012C4 File Offset: 0x000FF4C4
	private void Update()
	{
		this.UpdateView();
		if (GeneralWindow.IsCreated("RsrBuyLegal") && GeneralWindow.IsButtonPressed)
		{
			GeneralWindow.Close();
		}
		if (GeneralWindow.IsCreated("EventEndError") && GeneralWindow.IsButtonPressed)
		{
			GeneralWindow.Close();
			UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(base.gameObject, "Btn_cmn_back");
			if (uibuttonMessage != null)
			{
				uibuttonMessage.SendMessage("OnClick");
			}
		}
		if (this.m_freeGetButton != null && !this.m_freeGetButton.activeSelf)
		{
			bool flag = true;
			RegionManager instance = RegionManager.Instance;
			if (instance != null && !instance.IsJapan())
			{
				flag = false;
			}
			if (!NoahHandler.Instance.IsEndConnect)
			{
				flag = false;
			}
			else if (!NoahHandler.Instance.IsEndSetGUID)
			{
				flag = false;
			}
			else if (!Noah.Instance.GetOfferFlag())
			{
				flag = false;
			}
			if (flag)
			{
				this.m_freeGetButton.SetActive(true);
			}
		}
	}

	// Token: 0x06002998 RID: 10648 RVA: 0x001013D0 File Offset: 0x000FF5D0
	private void OnClickRsr1()
	{
		this.BuyRsring(0);
	}

	// Token: 0x06002999 RID: 10649 RVA: 0x001013DC File Offset: 0x000FF5DC
	private void OnClickRsr2()
	{
		this.BuyRsring(1);
	}

	// Token: 0x0600299A RID: 10650 RVA: 0x001013E8 File Offset: 0x000FF5E8
	private void OnClickRsr3()
	{
		this.BuyRsring(2);
	}

	// Token: 0x0600299B RID: 10651 RVA: 0x001013F4 File Offset: 0x000FF5F4
	private void OnClickRsr4()
	{
		this.BuyRsring(3);
	}

	// Token: 0x0600299C RID: 10652 RVA: 0x00101400 File Offset: 0x000FF600
	private void OnClickRsr5()
	{
		this.BuyRsring(4);
	}

	// Token: 0x0600299D RID: 10653 RVA: 0x0010140C File Offset: 0x000FF60C
	private void OnClickRing1()
	{
		this.BuyRing(0);
	}

	// Token: 0x0600299E RID: 10654 RVA: 0x00101418 File Offset: 0x000FF618
	private void OnClickRing2()
	{
		this.BuyRing(1);
	}

	// Token: 0x0600299F RID: 10655 RVA: 0x00101424 File Offset: 0x000FF624
	private void OnClickRing3()
	{
		this.BuyRing(2);
	}

	// Token: 0x060029A0 RID: 10656 RVA: 0x00101430 File Offset: 0x000FF630
	private void OnClickRing4()
	{
		this.BuyRing(3);
	}

	// Token: 0x060029A1 RID: 10657 RVA: 0x0010143C File Offset: 0x000FF63C
	private void OnClickRing5()
	{
		this.BuyRing(4);
	}

	// Token: 0x060029A2 RID: 10658 RVA: 0x00101448 File Offset: 0x000FF648
	private void OnClickChallenge1()
	{
		this.BuyChallenge(0);
	}

	// Token: 0x060029A3 RID: 10659 RVA: 0x00101454 File Offset: 0x000FF654
	private void OnClickChallenge2()
	{
		this.BuyChallenge(1);
	}

	// Token: 0x060029A4 RID: 10660 RVA: 0x00101460 File Offset: 0x000FF660
	private void OnClickChallenge3()
	{
		this.BuyChallenge(2);
	}

	// Token: 0x060029A5 RID: 10661 RVA: 0x0010146C File Offset: 0x000FF66C
	private void OnClickChallenge4()
	{
		this.BuyChallenge(3);
	}

	// Token: 0x060029A6 RID: 10662 RVA: 0x00101478 File Offset: 0x000FF678
	private void OnClickChallenge5()
	{
		this.BuyChallenge(4);
	}

	// Token: 0x060029A7 RID: 10663 RVA: 0x00101484 File Offset: 0x000FF684
	private void OnClickRaidbossEnergy1()
	{
		this.BuyRaidbossEnergy(0);
	}

	// Token: 0x060029A8 RID: 10664 RVA: 0x00101490 File Offset: 0x000FF690
	private void OnClickRaidbossEnergy2()
	{
		this.BuyRaidbossEnergy(1);
	}

	// Token: 0x060029A9 RID: 10665 RVA: 0x0010149C File Offset: 0x000FF69C
	private void OnClickRaidbossEnergy3()
	{
		this.BuyRaidbossEnergy(2);
	}

	// Token: 0x060029AA RID: 10666 RVA: 0x001014A8 File Offset: 0x000FF6A8
	private void OnClickRaidbossEnergy4()
	{
		this.BuyRaidbossEnergy(3);
	}

	// Token: 0x060029AB RID: 10667 RVA: 0x001014B4 File Offset: 0x000FF6B4
	private void OnClickRaidbossEnergy5()
	{
		this.BuyRaidbossEnergy(4);
	}

	// Token: 0x060029AC RID: 10668 RVA: 0x001014C0 File Offset: 0x000FF6C0
	private void BuyRsring(int i)
	{
		GameObject gameObject = base.gameObject.transform.parent.gameObject;
		ShopWindowRsringUI shopWindowRsringUI = GameObjectUtil.FindChildGameObjectComponent<ShopWindowRsringUI>(gameObject, "ShopWindowRsringUI");
		if (shopWindowRsringUI != null)
		{
			shopWindowRsringUI.gameObject.SetActive(true);
			shopWindowRsringUI.OpenWindow(i, this.m_exchangeTypes[0].m_exchangeItems[i], new ShopWindowRsringUI.PurchaseEndCallback(this.PurchaseSuccessCallback));
		}
	}

	// Token: 0x060029AD RID: 10669 RVA: 0x0010152C File Offset: 0x000FF72C
	private void BuyRing(int i)
	{
		GameObject gameObject = base.gameObject.transform.parent.gameObject;
		ShopWindowRingUI shopWindowRingUI = GameObjectUtil.FindChildGameObjectComponent<ShopWindowRingUI>(gameObject, "ShopWindowRingUI");
		if (shopWindowRingUI != null)
		{
			shopWindowRingUI.gameObject.SetActive(true);
			shopWindowRingUI.OpenWindow(i, this.m_exchangeTypes[1].m_exchangeItems[i]);
		}
	}

	// Token: 0x060029AE RID: 10670 RVA: 0x0010158C File Offset: 0x000FF78C
	private void BuyChallenge(int i)
	{
		GameObject gameObject = base.gameObject.transform.parent.gameObject;
		ShopWindowChallengeUI shopWindowChallengeUI = GameObjectUtil.FindChildGameObjectComponent<ShopWindowChallengeUI>(gameObject, "ShopWindowChallengeUI");
		if (shopWindowChallengeUI != null)
		{
			shopWindowChallengeUI.gameObject.SetActive(true);
			shopWindowChallengeUI.OpenWindow(i, this.m_exchangeTypes[2].m_exchangeItems[i]);
		}
	}

	// Token: 0x060029AF RID: 10671 RVA: 0x001015EC File Offset: 0x000FF7EC
	private void BuyRaidbossEnergy(int i)
	{
		if (!ShopUI.isRaidbossEvent())
		{
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "EventEndError",
				buttonType = GeneralWindow.ButtonType.Ok,
				caption = ShopUI.GetText("gw_raid_count_error_caption", null),
				message = ShopUI.GetText("gw_raid_count_error_text", null),
				isPlayErrorSe = true
			});
			return;
		}
		GameObject gameObject = base.gameObject.transform.parent.gameObject;
		ShopWindowRaidUI shopWindowRaidUI = GameObjectUtil.FindChildGameObjectComponent<ShopWindowRaidUI>(gameObject, "ShopWindowRaidUI");
		if (shopWindowRaidUI != null)
		{
			shopWindowRaidUI.gameObject.SetActive(true);
		}
	}

	// Token: 0x060029B0 RID: 10672 RVA: 0x00101690 File Offset: 0x000FF890
	private void OnClickLegal()
	{
		base.StartCoroutine(this.OpenLegalWindow());
	}

	// Token: 0x060029B1 RID: 10673 RVA: 0x001016A0 File Offset: 0x000FF8A0
	private IEnumerator OpenLegalWindow()
	{
		BackKeyManager.InvalidFlag = true;
		HudMenuUtility.SetConnectAlertMenuButtonUI(true);
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
			BackKeyManager.InvalidFlag = false;
			HudMenuUtility.SetConnectAlertMenuButtonUI(false);
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "RsrBuyLegal",
				buttonType = GeneralWindow.ButtonType.Ok,
				caption = TextUtility.GetCommonText("Shop", "ui_Lbl_word_legal_caption"),
				message = legalText
			});
		}
		yield break;
	}

	// Token: 0x060029B2 RID: 10674 RVA: 0x001016B4 File Offset: 0x000FF8B4
	private void SetShopBtnObj(bool flag)
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Anchor_8_BC");
		if (gameObject != null)
		{
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "shop_btn");
			if (gameObject2 != null)
			{
				gameObject2.SetActive(flag);
			}
		}
	}

	// Token: 0x060029B3 RID: 10675 RVA: 0x00101700 File Offset: 0x000FF900
	private void OnClickGetFreeRsRing()
	{
		global::Debug.Log("ShopUI.OnClickGetFreeRsRing");
		SaveDataManager.Instance.ConnectData.ReplaceMessageBox = true;
		Noah.Instance.Offer(NoahHandler.Instance.GetGUID(), Noah.Orientation.Landscape);
	}

	// Token: 0x060029B4 RID: 10676 RVA: 0x0010173C File Offset: 0x000FF93C
	public void OnRingTabChange()
	{
		this.SetCurrentType(ShopUI.ServerEexchangeType.RING);
		this.SetShopBtnObj(true);
		UIToggle uitoggle = GameObjectUtil.FindChildGameObjectComponent<UIToggle>(base.gameObject.transform.root.gameObject, "Btn_tab_ring");
		if (uitoggle != null && uitoggle.value && this.isStarted)
		{
			SoundManager.SePlay("sys_page_skip", "SE");
		}
	}

	// Token: 0x060029B5 RID: 10677 RVA: 0x001017AC File Offset: 0x000FF9AC
	public void OnRsringTabChange()
	{
		this.SetCurrentType(ShopUI.ServerEexchangeType.RSRING);
		this.SetShopBtnObj(true);
		UIToggle uitoggle = GameObjectUtil.FindChildGameObjectComponent<UIToggle>(base.gameObject.transform.root.gameObject, "Btn_tab_rsring");
		if (uitoggle != null && uitoggle.value && this.isStarted)
		{
			SoundManager.SePlay("sys_page_skip", "SE");
		}
	}

	// Token: 0x060029B6 RID: 10678 RVA: 0x0010181C File Offset: 0x000FFA1C
	public void OnChallengeTabChange()
	{
		this.SetCurrentType(ShopUI.ServerEexchangeType.CHALLENGE);
		this.SetShopBtnObj(true);
		UIToggle uitoggle = GameObjectUtil.FindChildGameObjectComponent<UIToggle>(base.gameObject.transform.root.gameObject, "Btn_tab_challenge");
		if (uitoggle != null && uitoggle.value && this.isStarted)
		{
			SoundManager.SePlay("sys_page_skip", "SE");
		}
	}

	// Token: 0x060029B7 RID: 10679 RVA: 0x0010188C File Offset: 0x000FFA8C
	public void OnRaidbossEnergyTabChange()
	{
		this.SetShopBtnObj(false);
		UIToggle uitoggle = GameObjectUtil.FindChildGameObjectComponent<UIToggle>(base.gameObject.transform.root.gameObject, "Btn_tab_raidboss");
		if (uitoggle != null && uitoggle.value && this.isStarted)
		{
			SoundManager.SePlay("sys_page_skip", "SE");
		}
	}

	// Token: 0x060029B8 RID: 10680 RVA: 0x001018F4 File Offset: 0x000FFAF4
	public static string GetText(string cellName, Dictionary<string, string> dicReplaces = null)
	{
		string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Shop", cellName).text;
		if (dicReplaces != null)
		{
			text = TextUtility.Replaces(text, dicReplaces);
		}
		return text;
	}

	// Token: 0x060029B9 RID: 10681 RVA: 0x00101924 File Offset: 0x000FFB24
	[Conditional("DEBUG_INFO")]
	private static void DebugLog(List<ServerRedStarItemState> itemList)
	{
		foreach (ServerRedStarItemState serverRedStarItemState in itemList)
		{
		}
	}

	// Token: 0x060029BA RID: 10682 RVA: 0x00101980 File Offset: 0x000FFB80
	[Conditional("DEBUG_INFO")]
	private static void DebugLog(string s)
	{
		global::Debug.Log("@ms " + s);
	}

	// Token: 0x060029BB RID: 10683 RVA: 0x00101994 File Offset: 0x000FFB94
	[Conditional("DEBUG_INFO")]
	private static void DebugLogWarning(string s)
	{
		global::Debug.LogWarning("@ms " + s);
	}

	// Token: 0x060029BC RID: 10684 RVA: 0x001019A8 File Offset: 0x000FFBA8
	public void OnApplicationPause(bool flag)
	{
		if (this.m_freeGetButton != null)
		{
			this.m_freeGetButton.SetActive(false);
		}
	}

	// Token: 0x060029BD RID: 10685 RVA: 0x001019C8 File Offset: 0x000FFBC8
	private void PurchaseSuccessCallback(bool isSuccess)
	{
		if (isSuccess)
		{
			this.SetExchangeItems(ShopUI.ServerEexchangeType.RSRING, ServerInterface.RedStarItemList);
			Noah.Instance.Connect(NoahHandler.consumer_key, NoahHandler.secret_key, NoahHandler.action_id);
			this.UpdateView();
		}
	}

	// Token: 0x060029BE RID: 10686 RVA: 0x001019FC File Offset: 0x000FFBFC
	public static bool isRaidbossEvent()
	{
		bool result = false;
		EventManager instance = EventManager.Instance;
		if (instance != null && instance.Type == EventManager.EventType.RAID_BOSS)
		{
			result = instance.IsChallengeEvent();
		}
		return result;
	}

	// Token: 0x040024C7 RID: 9415
	public const int EXCHANGE_ITEM_PACK_COUNT = 5;

	// Token: 0x040024C8 RID: 9416
	[SerializeField]
	private ShopUI.EexchangeType[] m_exchangeTypes = new ShopUI.EexchangeType[3];

	// Token: 0x040024C9 RID: 9417
	[SerializeField]
	private GameObject[] m_exchangeObjects = new GameObject[3];

	// Token: 0x040024CA RID: 9418
	[SerializeField]
	public int m_tabOffsetX;

	// Token: 0x040024CB RID: 9419
	private GameObject m_freeGetButton;

	// Token: 0x040024CC RID: 9420
	private GameObject m_tabRSR;

	// Token: 0x040024CD RID: 9421
	private string[] m_redStarPrice = new string[5];

	// Token: 0x040024CE RID: 9422
	private bool m_isInitShop;

	// Token: 0x040024CF RID: 9423
	private static List<Constants.Campaign.emType>[] s_campaignTypes = new List<Constants.Campaign.emType>[]
	{
		new List<Constants.Campaign.emType>
		{
			Constants.Campaign.emType.PurchaseAddRsrings,
			Constants.Campaign.emType.PurchaseAddRsringsNoChargeUser
		},
		new List<Constants.Campaign.emType>
		{
			Constants.Campaign.emType.PurchaseAddRings
		},
		new List<Constants.Campaign.emType>
		{
			Constants.Campaign.emType.PurchaseAddEnergys
		}
	};

	// Token: 0x02000542 RID: 1346
	private enum ServerEexchangeType
	{
		// Token: 0x040024D2 RID: 9426
		RSRING,
		// Token: 0x040024D3 RID: 9427
		RING,
		// Token: 0x040024D4 RID: 9428
		CHALLENGE,
		// Token: 0x040024D5 RID: 9429
		Count
	}

	// Token: 0x02000543 RID: 1347
	[Serializable]
	public class EexchangeItem
	{
		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060029C0 RID: 10688 RVA: 0x00101A48 File Offset: 0x000FFC48
		[HideInInspector]
		public bool isCampaign
		{
			get
			{
				return this.m_campaignData != null;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060029C1 RID: 10689 RVA: 0x00101A58 File Offset: 0x000FFC58
		[HideInInspector]
		public int quantity
		{
			get
			{
				if (this.m_storeItem == null)
				{
					return 0;
				}
				float num = ServerCampaignData.fContentBasis;
				if (this.m_campaignData != null)
				{
					num = (float)this.m_campaignData.iContent;
				}
				return (int)((float)this.m_storeItem.m_numItem * num / ServerCampaignData.fContentBasis);
			}
		}

		// Token: 0x040024D6 RID: 9430
		[SerializeField]
		public UILabel m_quantityLabel;

		// Token: 0x040024D7 RID: 9431
		[SerializeField]
		public UILabel m_costLabel;

		// Token: 0x040024D8 RID: 9432
		[SerializeField]
		public GameObject m_bonusGameObject;

		// Token: 0x040024D9 RID: 9433
		[SerializeField]
		public UILabel[] m_bonusLabels = new UILabel[2];

		// Token: 0x040024DA RID: 9434
		[SerializeField]
		public UISprite m_saleSprite;

		// Token: 0x040024DB RID: 9435
		[SerializeField]
		public UILabel m_saleQuantityLabel;

		// Token: 0x040024DC RID: 9436
		[HideInInspector]
		public ServerRedStarItemState m_storeItem;

		// Token: 0x040024DD RID: 9437
		[HideInInspector]
		public ServerCampaignData m_campaignData;
	}

	// Token: 0x02000544 RID: 1348
	[Serializable]
	private class EexchangeType
	{
		// Token: 0x040024DE RID: 9438
		[SerializeField]
		public ShopUI.EexchangeItem[] m_exchangeItems = new ShopUI.EexchangeItem[5];
	}
}
