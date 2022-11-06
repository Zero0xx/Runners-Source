using System;
using AnimationOrTween;
using Message;
using Text;
using UnityEngine;

// Token: 0x02000545 RID: 1349
public class ShopWindowChallengeUI : MonoBehaviour
{
	// Token: 0x060029C4 RID: 10692 RVA: 0x00101ADC File Offset: 0x000FFCDC
	private void Start()
	{
		this.m_windowObject = GameObjectUtil.FindChildGameObject(base.gameObject, "window");
	}

	// Token: 0x060029C5 RID: 10693 RVA: 0x00101AF4 File Offset: 0x000FFCF4
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

	// Token: 0x060029C6 RID: 10694 RVA: 0x00101B84 File Offset: 0x000FFD84
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
		Animation animation = GameObjectUtil.FindGameObjectComponent<Animation>("shop_challenge_window");
		if (animation != null)
		{
			ActiveAnimation.Play(animation, "ui_shop_window_Anim", Direction.Forward, EnableCondition.DoNothing, DisableCondition.DoNotDisable);
		}
	}

	// Token: 0x060029C7 RID: 10695 RVA: 0x00101C04 File Offset: 0x000FFE04
	private void OnClickClose()
	{
		SoundManager.SePlay("sys_window_close", "SE");
	}

	// Token: 0x060029C8 RID: 10696 RVA: 0x00101C18 File Offset: 0x000FFE18
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

	// Token: 0x060029C9 RID: 10697 RVA: 0x00101C88 File Offset: 0x000FFE88
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

	// Token: 0x060029CA RID: 10698 RVA: 0x00101E40 File Offset: 0x00100040
	private void OnClickBuyChallenge()
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
		else if (SaveDataManager.Instance.PlayerData.ChallengeCount >= 99999U || (ulong)(99999U - SaveDataManager.Instance.PlayerData.ChallengeCount) < (ulong)((long)this.m_exchangeItem.quantity))
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
				SaveDataManager.Instance.PlayerData.ChallengeCount += (uint)this.m_exchangeItem.quantity;
				HudMenuUtility.SendMsgUpdateSaveDataDisplay();
			}
		}
	}

	// Token: 0x060029CB RID: 10699 RVA: 0x0010203C File Offset: 0x0010023C
	private void ServerRedStarExchange_Succeeded(MsgGetPlayerStateSucceed msg)
	{
		SoundManager.SePlay("sys_buy", "SE");
		HudMenuUtility.SendMsgUpdateSaveDataDisplay();
	}

	// Token: 0x060029CC RID: 10700 RVA: 0x00102054 File Offset: 0x00100254
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

	// Token: 0x040024DF RID: 9439
	[SerializeField]
	private GameObject[] m_itemsGameObject = new GameObject[5];

	// Token: 0x040024E0 RID: 9440
	[SerializeField]
	private GameObject m_panelGameObject;

	// Token: 0x040024E1 RID: 9441
	[SerializeField]
	public UILabel m_quantityLabel;

	// Token: 0x040024E2 RID: 9442
	[SerializeField]
	public UILabel m_costLabel;

	// Token: 0x040024E3 RID: 9443
	[SerializeField]
	public GameObject m_bonusGameObject;

	// Token: 0x040024E4 RID: 9444
	[SerializeField]
	public UILabel[] m_bonusLabels = new UILabel[2];

	// Token: 0x040024E5 RID: 9445
	[SerializeField]
	public UISprite m_saleSprite;

	// Token: 0x040024E6 RID: 9446
	private int m_productIndex;

	// Token: 0x040024E7 RID: 9447
	private ShopUI.EexchangeItem m_exchangeItem;

	// Token: 0x040024E8 RID: 9448
	private GameObject m_windowObject;

	// Token: 0x040024E9 RID: 9449
	[SerializeField]
	public UILabel m_presentLabel;
}
