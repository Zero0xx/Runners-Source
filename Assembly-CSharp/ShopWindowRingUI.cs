using System;
using AnimationOrTween;
using Message;
using Text;
using UnityEngine;

// Token: 0x02000547 RID: 1351
public class ShopWindowRingUI : MonoBehaviour
{
	// Token: 0x060029D8 RID: 10712 RVA: 0x001026F8 File Offset: 0x001008F8
	private void Start()
	{
		this.m_windowObject = GameObjectUtil.FindChildGameObject(base.gameObject, "window");
	}

	// Token: 0x060029D9 RID: 10713 RVA: 0x00102710 File Offset: 0x00100910
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
		if (GeneralWindow.IsCreated("RingCountError") && GeneralWindow.IsButtonPressed)
		{
			GeneralWindow.Close();
		}
	}

	// Token: 0x060029DA RID: 10714 RVA: 0x001027A0 File Offset: 0x001009A0
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
		Animation animation = GameObjectUtil.FindGameObjectComponent<Animation>("shop_ring_window");
		if (animation != null)
		{
			ActiveAnimation.Play(animation, "ui_shop_window_Anim", Direction.Forward, EnableCondition.DoNothing, DisableCondition.DoNotDisable);
		}
	}

	// Token: 0x060029DB RID: 10715 RVA: 0x00102820 File Offset: 0x00100A20
	private void OnClickClose()
	{
		SoundManager.SePlay("sys_window_close", "SE");
	}

	// Token: 0x060029DC RID: 10716 RVA: 0x00102834 File Offset: 0x00100A34
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

	// Token: 0x060029DD RID: 10717 RVA: 0x0010289C File Offset: 0x00100A9C
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

	// Token: 0x060029DE RID: 10718 RVA: 0x00102A40 File Offset: 0x00100C40
	private void OnClickBuyRing()
	{
		SoundManager.SePlay("sys_window_close", "SE");
		if ((ulong)SaveDataManager.Instance.ItemData.RedRingCount < (ulong)((long)this.m_exchangeItem.m_storeItem.m_price))
		{
			bool flag = ServerInterface.IsRSREnable();
			GeneralWindow.ButtonType buttonType = (!flag) ? GeneralWindow.ButtonType.Ok : GeneralWindow.ButtonType.YesNo;
			string message = (!flag) ? TextUtility.GetCommonText("ChaoRoulette", "gw_cost_caption_text_2") : ShopUI.GetText("gw_rsring_short_error_text", null);
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "RingShortError",
				buttonType = buttonType,
				caption = ShopUI.GetText("gw_rsring_short_error_caption", null),
				message = message,
				isPlayErrorSe = true
			});
		}
		else if (SaveDataManager.Instance.ItemData.RingCount >= 9999999U || (ulong)(9999999U - SaveDataManager.Instance.ItemData.RingCount) < (ulong)((long)this.m_exchangeItem.quantity))
		{
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "RingCountError",
				buttonType = GeneralWindow.ButtonType.Ok,
				caption = ShopUI.GetText("gw_ring_count_error_caption", null),
				message = ShopUI.GetText("gw_ring_count_error_text", null),
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
				SaveDataManager.Instance.ItemData.RingCount += (uint)this.m_exchangeItem.quantity;
				HudMenuUtility.SendMsgUpdateSaveDataDisplay();
			}
		}
	}

	// Token: 0x060029DF RID: 10719 RVA: 0x00102C3C File Offset: 0x00100E3C
	private void ServerRedStarExchange_Succeeded(MsgGetPlayerStateSucceed msg)
	{
		SoundManager.SePlay("sys_buy", "SE");
		HudMenuUtility.SendMsgUpdateSaveDataDisplay();
	}

	// Token: 0x060029E0 RID: 10720 RVA: 0x00102C54 File Offset: 0x00100E54
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

	// Token: 0x040024F5 RID: 9461
	[SerializeField]
	private GameObject[] m_itemsGameObject = new GameObject[5];

	// Token: 0x040024F6 RID: 9462
	[SerializeField]
	private GameObject m_panelGameObject;

	// Token: 0x040024F7 RID: 9463
	[SerializeField]
	public UILabel m_quantityLabel;

	// Token: 0x040024F8 RID: 9464
	[SerializeField]
	public UILabel m_costLabel;

	// Token: 0x040024F9 RID: 9465
	[SerializeField]
	public GameObject m_bonusGameObject;

	// Token: 0x040024FA RID: 9466
	[SerializeField]
	public UILabel[] m_bonusLabels = new UILabel[2];

	// Token: 0x040024FB RID: 9467
	[SerializeField]
	public UISprite m_saleSprite;

	// Token: 0x040024FC RID: 9468
	private int m_productIndex;

	// Token: 0x040024FD RID: 9469
	private ShopUI.EexchangeItem m_exchangeItem;

	// Token: 0x040024FE RID: 9470
	private GameObject m_windowObject;

	// Token: 0x040024FF RID: 9471
	[SerializeField]
	public UILabel m_presentLabel;
}
