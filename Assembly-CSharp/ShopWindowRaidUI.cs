using System;
using AnimationOrTween;
using Message;
using UnityEngine;

// Token: 0x02000546 RID: 1350
public class ShopWindowRaidUI : MonoBehaviour
{
	// Token: 0x060029CE RID: 10702 RVA: 0x001020D4 File Offset: 0x001002D4
	private void Start()
	{
		this.m_windowObject = GameObjectUtil.FindChildGameObject(base.gameObject, "window");
	}

	// Token: 0x060029CF RID: 10703 RVA: 0x001020EC File Offset: 0x001002EC
	private void Update()
	{
		this.UpdateView();
		if (GeneralWindow.IsCreated("RingShortError") && GeneralWindow.IsButtonPressed)
		{
			if (GeneralWindow.IsYesButtonPressed)
			{
				UIToggle uitoggle = GameObjectUtil.FindChildGameObjectComponent<UIToggle>(base.gameObject.transform.root.gameObject, "Btn_tab_rsring");
				if (uitoggle != null)
				{
					uitoggle.value = true;
				}
			}
			GeneralWindow.Close();
		}
		if (GeneralWindow.IsCreated("EventEndError") && GeneralWindow.IsButtonPressed)
		{
			GeneralWindow.Close();
			HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.SHOP_BACK, false);
		}
		if (GeneralWindow.IsCreated("RingCountError") && GeneralWindow.IsButtonPressed)
		{
			GeneralWindow.Close();
		}
	}

	// Token: 0x060029D0 RID: 10704 RVA: 0x001021A4 File Offset: 0x001003A4
	public void OpenWindow(int productIndex, ShopUI.EexchangeItem exchangeItem)
	{
		BackKeyManager.AddWindowCallBack(base.gameObject);
		this.m_productIndex = productIndex;
		this.m_exchangeItem = exchangeItem;
		if (this.m_windowObject != null)
		{
			this.m_windowObject.SetActive(true);
		}
		SoundManager.SePlay("sys_window_open", "SE");
		this.UpdateView();
		Animation animation = GameObjectUtil.FindGameObjectComponent<Animation>("shop_raid_window");
		if (animation != null)
		{
			ActiveAnimation.Play(animation, "ui_shop_window_Anim", Direction.Forward, EnableCondition.DoNothing, DisableCondition.DoNotDisable);
		}
	}

	// Token: 0x060029D1 RID: 10705 RVA: 0x00102224 File Offset: 0x00100424
	private void OnClickClose()
	{
		SoundManager.SePlay("sys_window_close", "SE");
	}

	// Token: 0x060029D2 RID: 10706 RVA: 0x00102238 File Offset: 0x00100438
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
		this.m_productIndex = -1;
	}

	// Token: 0x060029D3 RID: 10707 RVA: 0x001022A8 File Offset: 0x001004A8
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
			if (this.m_presentLabel != null)
			{
				this.m_presentLabel.text = this.m_exchangeItem.m_bonusLabels[0].text;
			}
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

	// Token: 0x060029D4 RID: 10708 RVA: 0x00102460 File Offset: 0x00100660
	private void OnClickBuyRaidEnergy()
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
		SoundManager.SePlay("sys_window_close", "SE");
		if ((ulong)SaveDataManager.Instance.ItemData.RedRingCount < (ulong)((long)this.m_exchangeItem.m_storeItem.m_price))
		{
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "RingShortError",
				buttonType = GeneralWindow.ButtonType.YesNo,
				caption = ShopUI.GetText("gw_rsring_short_error_caption", null),
				message = ShopUI.GetText("gw_rsring_short_error_text", null),
				isPlayErrorSe = true
			});
		}
		else if ((long)EventManager.Instance.RaidbossChallengeCount >= 99999L || 99999L - (long)EventManager.Instance.RaidbossChallengeCount < (long)this.m_exchangeItem.quantity)
		{
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "ChallengeCountError",
				buttonType = GeneralWindow.ButtonType.Ok,
				caption = ShopUI.GetText("gw_challenge_count_error_caption", null),
				message = ShopUI.GetText("gw_challenge_count_error_text", null),
				isPlayErrorSe = true
			});
		}
		else
		{
			SoundManager.SePlay("sys_menu_decide", "SE");
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				loggedInServerInterface.RequestServerRedStarExchange(this.m_exchangeItem.m_storeItem.m_storeItemId, base.gameObject);
			}
			else
			{
				SoundManager.SePlay("sys_buy", "SE");
				SaveDataManager.Instance.ItemData.RedRingCount -= (uint)this.m_exchangeItem.m_storeItem.m_price;
				HudMenuUtility.SendMsgUpdateSaveDataDisplay();
			}
		}
	}

	// Token: 0x060029D5 RID: 10709 RVA: 0x0010265C File Offset: 0x0010085C
	private void ServerRedStarExchange_Succeeded(MsgGetPlayerStateSucceed msg)
	{
		SoundManager.SePlay("sys_buy", "SE");
		HudMenuUtility.SendMsgUpdateSaveDataDisplay();
	}

	// Token: 0x060029D6 RID: 10710 RVA: 0x00102674 File Offset: 0x00100874
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

	// Token: 0x040024EA RID: 9450
	[SerializeField]
	private GameObject[] m_itemsGameObject = new GameObject[5];

	// Token: 0x040024EB RID: 9451
	[SerializeField]
	private GameObject m_panelGameObject;

	// Token: 0x040024EC RID: 9452
	[SerializeField]
	public UILabel m_quantityLabel;

	// Token: 0x040024ED RID: 9453
	[SerializeField]
	public UILabel m_costLabel;

	// Token: 0x040024EE RID: 9454
	[SerializeField]
	public GameObject m_bonusGameObject;

	// Token: 0x040024EF RID: 9455
	[SerializeField]
	public UILabel[] m_bonusLabels = new UILabel[2];

	// Token: 0x040024F0 RID: 9456
	[SerializeField]
	public UISprite m_saleSprite;

	// Token: 0x040024F1 RID: 9457
	private int m_productIndex;

	// Token: 0x040024F2 RID: 9458
	private ShopUI.EexchangeItem m_exchangeItem;

	// Token: 0x040024F3 RID: 9459
	private GameObject m_windowObject;

	// Token: 0x040024F4 RID: 9460
	[SerializeField]
	public UILabel m_presentLabel;
}
