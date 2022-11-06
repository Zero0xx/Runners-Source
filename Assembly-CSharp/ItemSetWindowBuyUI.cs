using System;
using Message;
using Text;
using UnityEngine;

// Token: 0x0200042C RID: 1068
public class ItemSetWindowBuyUI : MonoBehaviour
{
	// Token: 0x1700047C RID: 1148
	// (get) Token: 0x06002067 RID: 8295 RVA: 0x000C2864 File Offset: 0x000C0A64
	private int buyItemCount
	{
		get
		{
			UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_buy_volume");
			if (uilabel != null)
			{
				return int.Parse(uilabel.text);
			}
			return 0;
		}
	}

	// Token: 0x1700047D RID: 1149
	// (get) Token: 0x06002068 RID: 8296 RVA: 0x000C289C File Offset: 0x000C0A9C
	private int buyNeedRingCount
	{
		get
		{
			if (this.m_shopItemData != null)
			{
				return this.m_shopItemData.rings * this.buyItemCount;
			}
			return 0;
		}
	}

	// Token: 0x06002069 RID: 8297 RVA: 0x000C28C0 File Offset: 0x000C0AC0
	private void Start()
	{
		this.m_id = -1;
	}

	// Token: 0x0600206A RID: 8298 RVA: 0x000C28CC File Offset: 0x000C0ACC
	private void Update()
	{
		if (this.m_isUnsetTriggerOfPopupList)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Drop-down List");
			if (gameObject != null)
			{
				foreach (BoxCollider boxCollider in GameObjectUtil.FindChildGameObjectsComponents<BoxCollider>(gameObject, "Label"))
				{
					boxCollider.isTrigger = false;
				}
			}
			this.m_isUnsetTriggerOfPopupList = false;
		}
		if (GeneralWindow.IsCreated("ItemBuyOverError") && GeneralWindow.IsButtonPressed)
		{
			GeneralWindow.Close();
			this.OpenWindow(this.m_id, this.m_count);
		}
		if (GeneralWindow.IsCreated("ItemBuyRingError") && GeneralWindow.IsButtonPressed)
		{
			GeneralWindow.Close();
			if (GeneralWindow.IsYesButtonPressed)
			{
				GameObjectUtil.SendMessageFindGameObject("ItemSetUI", "OnClose", "ShopUI.OnOpenRing", SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Token: 0x0600206B RID: 8299 RVA: 0x000C29D8 File Offset: 0x000C0BD8
	public void OpenWindow(int id, int count)
	{
		this.m_id = id;
		this.m_count = count;
		this.m_shopItemData = ShopItemTable.GetShopItemData(id);
		SoundManager.SePlay("sys_window_open", "SE");
		UIPopupList uipopupList = GameObjectUtil.FindChildGameObjectComponent<UIPopupList>(base.gameObject, "Ppl_buy_volume");
		if (uipopupList != null)
		{
			uipopupList.value = uipopupList.items[0];
		}
		this.UpdateView();
	}

	// Token: 0x0600206C RID: 8300 RVA: 0x000C2A44 File Offset: 0x000C0C44
	private void UpdateView()
	{
		if (this.m_id == -1)
		{
			return;
		}
		if (this.m_shopItemData != null)
		{
			UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_item_name");
			if (uilabel != null)
			{
				uilabel.text = this.m_shopItemData.name;
			}
			UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbt_item_effect");
			if (uilabel2 != null)
			{
				uilabel2.text = ItemSetWindowEquipUI.GetItemDetailsText(this.m_shopItemData);
			}
			UILabel uilabel3 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_price");
			if (uilabel3 != null)
			{
				uilabel3.text = HudUtility.GetFormatNumString<int>(this.m_shopItemData.rings);
			}
		}
		UILabel uilabel4 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_item_volume");
		if (uilabel4 != null)
		{
			uilabel4.text = HudUtility.GetFormatNumString<int>(this.m_count);
		}
		UILabel uilabel5 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_total_price");
		if (uilabel5 != null)
		{
			uilabel5.text = HudUtility.GetFormatNumString<int>(this.buyNeedRingCount);
		}
		UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_item");
		if (uisprite != null)
		{
			uisprite.spriteName = "ui_cmn_icon_item_" + this.m_id.ToString();
		}
	}

	// Token: 0x0600206D RID: 8301 RVA: 0x000C2B94 File Offset: 0x000C0D94
	private void OnClickBuy()
	{
		int itemCount = (int)SaveDataManager.Instance.ItemData.GetItemCount((ItemType)this.m_id);
		if ((long)itemCount >= 99L || 99L - (long)itemCount < (long)this.buyItemCount)
		{
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "ItemBuyOverError",
				buttonType = GeneralWindow.ButtonType.Ok,
				caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ItemSet", "gw_buy_over_error_caption").text,
				message = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ItemSet", "gw_buy_over_error_text").text,
				parentGameObject = base.gameObject
			});
		}
		else if ((ulong)SaveDataManager.Instance.ItemData.RingCount < (ulong)((long)this.buyNeedRingCount))
		{
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "ItemBuyRingError",
				buttonType = GeneralWindow.ButtonType.ShopCancel,
				caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ItemSet", "gw_buy_ring__error_caption").text,
				message = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ItemSet", "gw_buy_ring__error_text").text,
				parentGameObject = base.gameObject
			});
		}
		else
		{
			SoundManager.SePlay("sys_menu_decide", "SE");
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				ServerInterface serverInterface = loggedInServerInterface;
				ServerItem serverItem = new ServerItem((ItemType)this.m_id);
				serverInterface.RequestServerRingExchange((int)serverItem.id, this.buyItemCount, base.gameObject);
			}
			else
			{
				SoundManager.SePlay("sys_buy", "SE");
				SaveDataManager.Instance.ItemData.RingCount -= (uint)this.buyNeedRingCount;
				SaveDataManager.Instance.ItemData.SetItemCount((ItemType)this.m_id, SaveDataManager.Instance.ItemData.GetItemCount((ItemType)this.m_id) + (uint)this.buyItemCount);
				ItemSetWindowBuyUI.UpdateItemSetUIView();
			}
		}
	}

	// Token: 0x0600206E RID: 8302 RVA: 0x000C2D78 File Offset: 0x000C0F78
	private void ServerRingExchange_Succeeded(MsgGetPlayerStateSucceed msg)
	{
		SoundManager.SePlay("sys_buy", "SE");
		ItemSetWindowBuyUI.UpdateItemSetUIView();
	}

	// Token: 0x0600206F RID: 8303 RVA: 0x000C2D90 File Offset: 0x000C0F90
	public static void UpdateItemSetUIView()
	{
		ItemSetUI itemSetUI = GameObjectUtil.FindGameObjectComponent<ItemSetUI>("ItemSetUI");
		if (itemSetUI != null)
		{
			itemSetUI.UpdateView();
		}
		HudMenuUtility.SendMsgUpdateSaveDataDisplay();
	}

	// Token: 0x06002070 RID: 8304 RVA: 0x000C2DC0 File Offset: 0x000C0FC0
	private void OnClickClose()
	{
		SoundManager.SePlay("sys_window_close", "SE");
	}

	// Token: 0x06002071 RID: 8305 RVA: 0x000C2DD4 File Offset: 0x000C0FD4
	public void OnValueChangePopupList()
	{
		this.UpdateView();
	}

	// Token: 0x06002072 RID: 8306 RVA: 0x000C2DDC File Offset: 0x000C0FDC
	public void OnClickPopupList()
	{
		this.m_isUnsetTriggerOfPopupList = true;
	}

	// Token: 0x04001D13 RID: 7443
	private int m_id = -1;

	// Token: 0x04001D14 RID: 7444
	private int m_count;

	// Token: 0x04001D15 RID: 7445
	private ShopItemData m_shopItemData;

	// Token: 0x04001D16 RID: 7446
	private bool m_isUnsetTriggerOfPopupList;
}
