using System;
using UnityEngine;

// Token: 0x02000417 RID: 1047
public class InstantItemButton : MonoBehaviour
{
	// Token: 0x06001F73 RID: 8051 RVA: 0x000BA7F0 File Offset: 0x000B89F0
	public bool IsChecked()
	{
		return this.m_isChecked;
	}

	// Token: 0x1700046D RID: 1133
	// (get) Token: 0x06001F74 RID: 8052 RVA: 0x000BA7F8 File Offset: 0x000B89F8
	public BoostItemType boostItemType
	{
		get
		{
			return this.m_itemType;
		}
	}

	// Token: 0x06001F75 RID: 8053 RVA: 0x000BA800 File Offset: 0x000B8A00
	public void Setup(BoostItemType itemType, InstantItemButton.ClickCallback callback)
	{
		this.m_itemType = itemType;
		this.m_callback = callback;
		string name = "Btn_toggle_" + ((int)(itemType + 1)).ToString();
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, name);
		if (gameObject == null)
		{
			return;
		}
		this.m_uiToggle = gameObject.GetComponent<UIToggle>();
		UIButtonMessage uibuttonMessage = gameObject.GetComponent<UIButtonMessage>();
		if (uibuttonMessage == null)
		{
			uibuttonMessage = gameObject.AddComponent<UIButtonMessage>();
		}
		uibuttonMessage.target = base.gameObject;
		uibuttonMessage.functionName = "OnClickButton";
		this.m_ringManagement = ItemSetUtility.GetItemSetRingManagement();
		this.OnEnable();
	}

	// Token: 0x06001F76 RID: 8054 RVA: 0x000BA89C File Offset: 0x000B8A9C
	public void ResetCheckMark()
	{
		if (this.m_isChecked)
		{
			if (!this.IsFreeItem() && this.m_ringManagement != null)
			{
				int instantItemCost = ItemSetUtility.GetInstantItemCost(this.m_itemType);
				this.m_ringManagement.AddOffset(instantItemCost);
			}
			if (this.m_uiToggle != null)
			{
				this.m_uiToggle.value = false;
			}
			this.m_isChecked = false;
		}
	}

	// Token: 0x06001F77 RID: 8055 RVA: 0x000BA910 File Offset: 0x000B8B10
	private void Start()
	{
	}

	// Token: 0x06001F78 RID: 8056 RVA: 0x000BA914 File Offset: 0x000B8B14
	private void Update()
	{
		this.UpdateCampaignView();
	}

	// Token: 0x06001F79 RID: 8057 RVA: 0x000BA91C File Offset: 0x000B8B1C
	private void OnEnable()
	{
		this.m_isEnableCheck = !this.itemLock;
		this.m_isTutorialEnd = true;
		string name = "Btn_toggle_" + ((int)(this.m_itemType + 1)).ToString();
		UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(base.gameObject, name);
		if (uiimageButton)
		{
			if (this.m_isEnableCheck)
			{
				uiimageButton.isEnabled = true;
			}
			else
			{
				uiimageButton.isEnabled = false;
			}
		}
		this.UpdateCampaignView();
	}

	// Token: 0x1700046E RID: 1134
	// (get) Token: 0x06001F7A RID: 8058 RVA: 0x000BA998 File Offset: 0x000B8B98
	public bool itemLock
	{
		get
		{
			bool flag = false;
			SaveDataManager instance = SaveDataManager.Instance;
			if (instance != null)
			{
				flag = (this.m_itemType == BoostItemType.SUB_CHARACTER && instance.PlayerData.SubChara == CharaType.UNKNOWN);
			}
			bool flag2 = false;
			if (StageModeManager.Instance != null)
			{
				flag2 = StageModeManager.Instance.IsQuickMode();
			}
			if (!flag && this.m_itemType != BoostItemType.SUB_CHARACTER)
			{
				switch (HudMenuUtility.itemSelectMode)
				{
				case HudMenuUtility.ITEM_SELECT_MODE.NORMAL:
					if (MileageMapUtility.IsBossStage() && !flag2 && (this.m_itemType == BoostItemType.SCORE_BONUS || this.m_itemType == BoostItemType.ASSIST_TRAMPOLINE))
					{
						flag = true;
					}
					break;
				case HudMenuUtility.ITEM_SELECT_MODE.EVENT_STAGE:
					if (this.m_itemType == BoostItemType.SCORE_BONUS)
					{
						flag = true;
					}
					break;
				case HudMenuUtility.ITEM_SELECT_MODE.EVENT_BOSS:
					if (!flag2 && (this.m_itemType == BoostItemType.SCORE_BONUS || this.m_itemType == BoostItemType.ASSIST_TRAMPOLINE))
					{
						flag = true;
					}
					break;
				}
			}
			return flag;
		}
	}

	// Token: 0x06001F7B RID: 8059 RVA: 0x000BAA90 File Offset: 0x000B8C90
	private bool IsFreeItem()
	{
		bool result = false;
		if (this.m_isTutorialEnd && this.m_freeItemCount > 0)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x06001F7C RID: 8060 RVA: 0x000BAABC File Offset: 0x000B8CBC
	public void UpdateFreeItemCount(int count)
	{
		this.m_freeItemCount = count;
		this.UpdateCampaignView();
	}

	// Token: 0x06001F7D RID: 8061 RVA: 0x000BAACC File Offset: 0x000B8CCC
	private void UpdateCampaignView()
	{
		int num = (int)(this.m_itemType + 1);
		string name = "Lbl_ring_number_" + num.ToString();
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, name);
		if (uilabel != null)
		{
			uilabel.text = ItemSetUtility.GetInstantItemCostString(this.m_itemType);
		}
		bool active = false;
		bool flag = this.IsFreeItem();
		ServerItem[] serverItemTable = ServerItem.GetServerItemTable(ServerItem.IdType.BOOST_ITEM);
		int id = (int)serverItemTable[(int)this.m_itemType].id;
		ServerCampaignData campaignDataInSession = ItemSetUtility.GetCampaignDataInSession(id);
		if (campaignDataInSession != null)
		{
			active = true;
		}
		string name2 = "img_free_icon_" + num.ToString();
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, name2);
		if (gameObject != null)
		{
			gameObject.SetActive(flag);
		}
		if (flag)
		{
			active = false;
		}
		string name3 = "img_sale_icon_" + num.ToString();
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, name3);
		if (gameObject2 != null)
		{
			gameObject2.SetActive(active);
		}
	}

	// Token: 0x06001F7E RID: 8062 RVA: 0x000BABD0 File Offset: 0x000B8DD0
	private void OnClickButton()
	{
		this.m_isChecked = !this.m_isChecked;
		global::Debug.Log("InstantItemButton:OnClickButton   this button >>" + this.m_itemType.ToString());
		bool flag = this.IsFreeItem();
		bool flag2 = true;
		int instantItemCost = ItemSetUtility.GetInstantItemCost(this.m_itemType);
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance == null)
		{
			return;
		}
		if (!this.m_isEnableCheck)
		{
			this.m_isChecked = false;
			if (this.m_uiToggle)
			{
				this.m_uiToggle.value = false;
				flag2 = false;
			}
		}
		else if (!flag)
		{
			if (this.m_isChecked)
			{
				if (this.m_ringManagement != null)
				{
					this.m_ringManagement.AddOffset(-instantItemCost);
				}
			}
			else if (this.m_ringManagement != null)
			{
				this.m_ringManagement.AddOffset(instantItemCost);
			}
		}
		string cueName = string.Empty;
		string spriteName = string.Empty;
		if (this.m_isChecked)
		{
			cueName = "sys_menu_decide";
			spriteName = "ui_itemset_3_bost_1";
		}
		else if (flag2)
		{
			cueName = "sys_cancel";
			spriteName = "ui_itemset_3_bost_0";
		}
		else if (!this.m_isEnableCheck)
		{
			cueName = "sys_error";
			spriteName = "ui_itemset_3_bost_4";
		}
		else
		{
			cueName = "sys_error";
			spriteName = "ui_itemset_3_bost_0";
		}
		SoundManager.SePlay(cueName, "SE");
		UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_bg");
		if (uisprite != null)
		{
			uisprite.spriteName = spriteName;
		}
		if (this.m_callback != null)
		{
			this.m_callback(this.m_itemType, this.m_isChecked);
		}
	}

	// Token: 0x06001F7F RID: 8063 RVA: 0x000BAD84 File Offset: 0x000B8F84
	public void SetupBoostedItemButton(bool isChecked)
	{
		if (!this.m_isEnableCheck)
		{
			return;
		}
		this.m_isChecked = isChecked;
		bool flag = this.IsFreeItem();
		string spriteName = string.Empty;
		this.m_uiToggle.value = isChecked;
		if (this.m_isChecked)
		{
			if (this.m_isEnableCheck)
			{
				if (!flag)
				{
					int instantItemCost = ItemSetUtility.GetInstantItemCost(this.m_itemType);
					this.m_ringManagement.AddOffset(-instantItemCost);
				}
				spriteName = "ui_itemset_3_bost_1";
			}
			else
			{
				this.m_isChecked = false;
				this.m_uiToggle.value = false;
				spriteName = "ui_itemset_3_bost_4";
			}
		}
		else if (!this.m_isEnableCheck)
		{
			spriteName = "ui_itemset_3_bost_4";
		}
		else
		{
			spriteName = "ui_itemset_3_bost_0";
		}
		UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_bg");
		if (uisprite != null)
		{
			uisprite.spriteName = spriteName;
		}
		if (this.m_callback != null)
		{
			this.m_callback(this.m_itemType, this.m_isChecked);
		}
	}

	// Token: 0x04001C7C RID: 7292
	private bool m_isChecked;

	// Token: 0x04001C7D RID: 7293
	private UIToggle m_uiToggle;

	// Token: 0x04001C7E RID: 7294
	private BoostItemType m_itemType;

	// Token: 0x04001C7F RID: 7295
	private ItemSetRingManagement m_ringManagement;

	// Token: 0x04001C80 RID: 7296
	private InstantItemButton.ClickCallback m_callback;

	// Token: 0x04001C81 RID: 7297
	private bool m_isEnableCheck = true;

	// Token: 0x04001C82 RID: 7298
	private bool m_isTutorialEnd;

	// Token: 0x04001C83 RID: 7299
	private int m_freeItemCount;

	// Token: 0x02000A8B RID: 2699
	// (Invoke) Token: 0x06004866 RID: 18534
	public delegate void ClickCallback(BoostItemType itemType, bool isChecked);
}
