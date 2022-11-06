using System;
using UnityEngine;

// Token: 0x0200041A RID: 1050
public class ItemButton : MonoBehaviour
{
	// Token: 0x06001F92 RID: 8082 RVA: 0x000BB5E8 File Offset: 0x000B97E8
	public void Setup(ItemType itemType, GameObject bgObject)
	{
		this.m_itemType = itemType;
		this.m_bgObject = bgObject;
		this.CheckItemLock();
		this.m_ringManagement = ItemSetUtility.GetItemSetRingManagement();
		if (this.m_bgObject == null)
		{
			return;
		}
		UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_item");
		if (uisprite != null)
		{
			UISprite uisprite2 = uisprite;
			string str = "ui_cmn_icon_item_";
			int itemType2 = (int)this.m_itemType;
			uisprite2.spriteName = str + itemType2.ToString();
		}
		if (this.m_bgObject != null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_bgObject, "Btn_toggle");
			if (gameObject != null)
			{
				UIButtonMessage uibuttonMessage = gameObject.GetComponent<UIButtonMessage>();
				if (uibuttonMessage == null)
				{
					uibuttonMessage = gameObject.AddComponent<UIButtonMessage>();
				}
				uibuttonMessage.target = base.gameObject;
				uibuttonMessage.functionName = "OnClickButton";
			}
		}
		this.OnEnable();
		this.RemoveCursor();
	}

	// Token: 0x06001F93 RID: 8083 RVA: 0x000BB6D0 File Offset: 0x000B98D0
	private void OnEnable()
	{
		this.CheckItemLock();
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "badge");
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, "Lbl_ring_number");
		if (gameObject != null && gameObject2 != null)
		{
			int itemNum = ItemSetUtility.GetItemNum(this.m_itemType);
			if (itemNum > 0)
			{
				gameObject.SetActive(true);
				gameObject2.SetActive(false);
			}
			else
			{
				gameObject.SetActive(false);
				gameObject2.SetActive(true);
			}
		}
		this.UpdateItemCount();
		this.UpdateCampaignView();
		this.m_isTutorialEnd = true;
	}

	// Token: 0x06001F94 RID: 8084 RVA: 0x000BB768 File Offset: 0x000B9968
	public void SetCallback(ItemButton.ClickCallback callback)
	{
		this.m_callback = callback;
	}

	// Token: 0x06001F95 RID: 8085 RVA: 0x000BB774 File Offset: 0x000B9974
	public bool IsEquiped()
	{
		return this.m_isEquiped;
	}

	// Token: 0x06001F96 RID: 8086 RVA: 0x000BB77C File Offset: 0x000B997C
	public void UpdateItemCount()
	{
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_item_volume");
		if (uilabel != null)
		{
			uilabel.text = ItemSetUtility.GetItemNum(this.m_itemType).ToString();
		}
	}

	// Token: 0x06001F97 RID: 8087 RVA: 0x000BB7C0 File Offset: 0x000B99C0
	public void SetButtonActive(bool isActive)
	{
		if (this.itemLock)
		{
			return;
		}
		this.m_isActive = isActive;
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "disabled_filter");
		if (gameObject != null)
		{
			gameObject.SetActive(!this.m_isActive);
		}
		UIButtonScale uibuttonScale = GameObjectUtil.FindChildGameObjectComponent<UIButtonScale>(this.m_bgObject, "Btn_toggle");
		if (uibuttonScale != null)
		{
			uibuttonScale.enabled = isActive;
		}
		UIToggle uitoggle = GameObjectUtil.FindChildGameObjectComponent<UIToggle>(this.m_bgObject, "Btn_toggle");
		if (uitoggle != null)
		{
			uitoggle.enabled = this.m_isActive;
		}
	}

	// Token: 0x06001F98 RID: 8088 RVA: 0x000BB85C File Offset: 0x000B9A5C
	public void SetCursor(ItemButton.CursorColor cursorColor)
	{
		if (this.itemLock)
		{
			return;
		}
		this.m_isEquiped = true;
		this.m_cursorColor = cursorColor;
		GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_bgObject, "img_cursor");
		if (gameObject != null)
		{
			UISprite component = gameObject.GetComponent<UISprite>();
			if (component != null)
			{
				UISprite uisprite = component;
				string str = "ui_itemset_3_cursor_";
				int num = (int)cursorColor;
				uisprite.spriteName = str + num.ToString();
				component.alpha = 1f;
				component.color = Color.white;
			}
			gameObject.SetActive(true);
		}
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(this.m_bgObject, "Btn_toggle");
		if (gameObject2 != null)
		{
			UIToggle component2 = gameObject2.GetComponent<UIToggle>();
			if (component2 != null)
			{
				component2.value = true;
			}
		}
		this.UpdateButtonState();
	}

	// Token: 0x06001F99 RID: 8089 RVA: 0x000BB928 File Offset: 0x000B9B28
	public void RemoveCursor()
	{
		this.m_cursorColor = ItemButton.CursorColor.NONE;
		GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_bgObject, "img_cursor");
		if (gameObject != null)
		{
			UISprite component = gameObject.GetComponent<UISprite>();
			if (component != null)
			{
				component.spriteName = string.Empty;
			}
			gameObject.SetActive(false);
		}
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(this.m_bgObject, "Btn_toggle");
		if (gameObject2 != null)
		{
			UIToggle component2 = gameObject2.GetComponent<UIToggle>();
			if (component2 != null)
			{
				component2.value = false;
			}
		}
		this.m_isEquiped = false;
		this.UpdateButtonState();
	}

	// Token: 0x06001F9A RID: 8090 RVA: 0x000BB9C4 File Offset: 0x000B9BC4
	public ItemButton.CursorColor GetCursorColor()
	{
		return this.m_cursorColor;
	}

	// Token: 0x06001F9B RID: 8091 RVA: 0x000BB9CC File Offset: 0x000B9BCC
	public bool IsButtonActive()
	{
		return this.m_isActive;
	}

	// Token: 0x06001F9C RID: 8092 RVA: 0x000BB9D4 File Offset: 0x000B9BD4
	private void Start()
	{
	}

	// Token: 0x06001F9D RID: 8093 RVA: 0x000BB9D8 File Offset: 0x000B9BD8
	private void Update()
	{
		this.UpdateCampaignView();
	}

	// Token: 0x06001F9E RID: 8094 RVA: 0x000BB9E0 File Offset: 0x000B9BE0
	private void OnClickButton()
	{
		if (this.m_isActive)
		{
			this.m_isEquiped = !this.m_isEquiped;
		}
		string cueName = (!this.m_isEquiped) ? "sys_cancel" : "sys_menu_decide";
		SoundManager.SePlay(cueName, "SE");
		if (this.m_callback != null)
		{
			this.m_callback(this.m_itemType, this.m_isEquiped);
		}
	}

	// Token: 0x06001F9F RID: 8095 RVA: 0x000BBA50 File Offset: 0x000B9C50
	private bool IsFreeItem()
	{
		bool result = false;
		if (this.m_isTutorialEnd && this.m_freeItemCount > 0)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x06001FA0 RID: 8096 RVA: 0x000BBA7C File Offset: 0x000B9C7C
	public void UpdateFreeItemCount(int count)
	{
		this.m_freeItemCount = count;
		this.UpdateCampaignView();
	}

	// Token: 0x06001FA1 RID: 8097 RVA: 0x000BBA8C File Offset: 0x000B9C8C
	private void UpdateCampaignView()
	{
		bool active = false;
		bool flag = this.IsFreeItem();
		ServerItem serverItem = new ServerItem(this.m_itemType);
		ServerCampaignData campaignDataInSession = ItemSetUtility.GetCampaignDataInSession((int)serverItem.id);
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "img_free_icon");
		if (gameObject != null)
		{
			gameObject.SetActive(flag);
		}
		if (!flag && campaignDataInSession != null)
		{
			active = true;
		}
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, "img_sale_icon");
		if (gameObject2 != null)
		{
			gameObject2.SetActive(active);
		}
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_ring_number");
		if (uilabel != null)
		{
			uilabel.text = ItemSetUtility.GetOneItemCostString(this.m_itemType);
		}
	}

	// Token: 0x06001FA2 RID: 8098 RVA: 0x000BBB4C File Offset: 0x000B9D4C
	private void UpdateButtonState()
	{
		bool flag = this.IsFreeItem();
		int itemNum = ItemSetUtility.GetItemNum(this.m_itemType);
		if (!flag)
		{
			if (itemNum > 0)
			{
				int num;
				if (this.m_isEquiped)
				{
					num = itemNum;
				}
				else
				{
					num = itemNum;
				}
				UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_item_volume");
				if (uilabel != null)
				{
					uilabel.text = num.ToString();
				}
			}
			else
			{
				int oneItemCost = ItemSetUtility.GetOneItemCost(this.m_itemType);
				if (this.m_isEquiped)
				{
					if (this.m_ringManagement != null)
					{
						this.m_ringManagement.AddOffset(-oneItemCost);
					}
				}
				else if (this.m_ringManagement != null)
				{
					this.m_ringManagement.AddOffset(oneItemCost);
				}
			}
		}
	}

	// Token: 0x1700046F RID: 1135
	// (get) Token: 0x06001FA3 RID: 8099 RVA: 0x000BBC20 File Offset: 0x000B9E20
	public ItemType itemType
	{
		get
		{
			return this.m_itemType;
		}
	}

	// Token: 0x17000470 RID: 1136
	// (get) Token: 0x06001FA4 RID: 8100 RVA: 0x000BBC28 File Offset: 0x000B9E28
	public bool itemLock
	{
		get
		{
			bool result = false;
			if (this.m_bgObject != null)
			{
				bool flag = false;
				if (StageModeManager.Instance != null)
				{
					flag = StageModeManager.Instance.IsQuickMode();
				}
				switch (HudMenuUtility.itemSelectMode)
				{
				case HudMenuUtility.ITEM_SELECT_MODE.NORMAL:
					if (MileageMapUtility.IsBossStage() && !flag && (this.itemType == ItemType.COMBO || this.itemType == ItemType.TRAMPOLINE))
					{
						result = true;
					}
					break;
				case HudMenuUtility.ITEM_SELECT_MODE.EVENT_STAGE:
					result = false;
					break;
				case HudMenuUtility.ITEM_SELECT_MODE.EVENT_BOSS:
					if (!flag && (this.itemType == ItemType.COMBO || this.itemType == ItemType.TRAMPOLINE))
					{
						result = true;
					}
					break;
				}
			}
			return result;
		}
	}

	// Token: 0x06001FA5 RID: 8101 RVA: 0x000BBCE0 File Offset: 0x000B9EE0
	private bool CheckItemLock()
	{
		bool itemLock = this.itemLock;
		if (this.m_bgObject != null)
		{
			UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_bgObject.gameObject, "img_bg");
			BoxCollider boxCollider = GameObjectUtil.FindChildGameObjectComponent<BoxCollider>(this.m_bgObject.gameObject, "Btn_toggle");
			if (itemLock)
			{
				if (uisprite != null)
				{
					uisprite.spriteName = "ui_itemset_3_bost_4";
				}
				if (boxCollider != null)
				{
					boxCollider.enabled = false;
				}
			}
			else
			{
				if (uisprite != null)
				{
					uisprite.spriteName = "ui_itemset_3_bost_or_1";
				}
				if (boxCollider != null)
				{
					boxCollider.enabled = true;
				}
			}
		}
		return itemLock;
	}

	// Token: 0x04001C8B RID: 7307
	private ItemType m_itemType;

	// Token: 0x04001C8C RID: 7308
	private GameObject m_bgObject;

	// Token: 0x04001C8D RID: 7309
	private bool m_isEquiped;

	// Token: 0x04001C8E RID: 7310
	private ItemSetRingManagement m_ringManagement;

	// Token: 0x04001C8F RID: 7311
	private ItemButton.ClickCallback m_callback;

	// Token: 0x04001C90 RID: 7312
	private bool m_isActive = true;

	// Token: 0x04001C91 RID: 7313
	private bool m_isTutorialEnd;

	// Token: 0x04001C92 RID: 7314
	private int m_freeItemCount;

	// Token: 0x04001C93 RID: 7315
	private ItemButton.CursorColor m_cursorColor = ItemButton.CursorColor.NONE;

	// Token: 0x0200041B RID: 1051
	public enum CursorColor
	{
		// Token: 0x04001C95 RID: 7317
		NONE = -1,
		// Token: 0x04001C96 RID: 7318
		BLUE,
		// Token: 0x04001C97 RID: 7319
		RED,
		// Token: 0x04001C98 RID: 7320
		GREEN,
		// Token: 0x04001C99 RID: 7321
		NUM
	}

	// Token: 0x02000A8C RID: 2700
	// (Invoke) Token: 0x0600486A RID: 18538
	public delegate void ClickCallback(ItemType itemType, bool isEquiped);
}
