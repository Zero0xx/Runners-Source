using System;
using SaveData;
using UnityEngine;

// Token: 0x0200048E RID: 1166
public class OptionUI : MonoBehaviour
{
	// Token: 0x170004BB RID: 1211
	// (set) Token: 0x060022DA RID: 8922 RVA: 0x000D14C4 File Offset: 0x000CF6C4
	public bool SystemSaveFlag
	{
		set
		{
			this.m_systemSaveFlag = value;
		}
	}

	// Token: 0x060022DB RID: 8923 RVA: 0x000D14D0 File Offset: 0x000CF6D0
	private void Start()
	{
		base.enabled = false;
		this.m_backButtonObj = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_cmn_back");
	}

	// Token: 0x060022DC RID: 8924 RVA: 0x000D14F0 File Offset: 0x000CF6F0
	private void Initialize()
	{
		if (!this.m_inited)
		{
			this.UpdateRectItemStorage();
			this.m_inited = true;
		}
		this.m_scrollBar.value = 0f;
		this.m_systemSaveFlag = false;
		this.CheckESRBButtonView();
	}

	// Token: 0x060022DD RID: 8925 RVA: 0x000D1528 File Offset: 0x000CF728
	private void UpdateRectItemStorage()
	{
		if (this.m_itemStorage != null)
		{
			int num = this.m_optionInfos.Length;
			this.m_itemStorage.maxItemCount = num;
			int num2 = num % this.m_itemStorage.maxColumns;
			this.m_itemStorage.maxRows = num / this.m_itemStorage.maxColumns + num2;
			this.m_itemStorage.Restart();
			ui_option_scroll[] componentsInChildren = this.m_itemStorage.GetComponentsInChildren<ui_option_scroll>(true);
			int num3 = componentsInChildren.Length;
			int num4 = this.m_itemStorage.maxRows * this.m_itemStorage.maxColumns;
			for (int i = 0; i < num4; i++)
			{
				if (i < num3 && i < this.m_itemStorage.maxItemCount)
				{
					componentsInChildren[i].gameObject.SetActive(true);
					componentsInChildren[i].UpdateView(this.m_optionInfos[i], this);
				}
				else
				{
					componentsInChildren[i].gameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x060022DE RID: 8926 RVA: 0x000D1620 File Offset: 0x000CF820
	public void SetButtonTrigger(bool flag)
	{
		ui_option_scroll[] componentsInChildren = this.m_itemStorage.GetComponentsInChildren<ui_option_scroll>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(componentsInChildren[i].gameObject, "Btn_option_top");
			if (gameObject != null)
			{
				BoxCollider component = gameObject.GetComponent<BoxCollider>();
				if (component != null)
				{
					component.isTrigger = flag;
				}
			}
		}
		if (this.m_backButtonObj != null)
		{
			BoxCollider component2 = this.m_backButtonObj.GetComponent<BoxCollider>();
			if (component2 != null)
			{
				component2.isTrigger = flag;
			}
		}
	}

	// Token: 0x060022DF RID: 8927 RVA: 0x000D16BC File Offset: 0x000CF8BC
	private void OnGUI()
	{
	}

	// Token: 0x060022E0 RID: 8928 RVA: 0x000D16C0 File Offset: 0x000CF8C0
	private void CheckESRBButtonView()
	{
		RegionManager instance = RegionManager.Instance;
		ui_option_scroll[] componentsInChildren = this.m_itemStorage.GetComponentsInChildren<ui_option_scroll>(true);
		int num = componentsInChildren.Length;
		for (int i = 0; i < num; i++)
		{
			if (componentsInChildren[i].OptionInfo != null)
			{
				if (componentsInChildren[i].OptionInfo.type == OptionType.FACEBOOK_ACCESS || componentsInChildren[i].OptionInfo.type == OptionType.PUSH_NOTIFICATION)
				{
					bool enableImageButton = false;
					if (instance != null && instance.IsUseSNS())
					{
						enableImageButton = true;
					}
					componentsInChildren[i].SetEnableImageButton(enableImageButton);
				}
			}
		}
	}

	// Token: 0x060022E1 RID: 8929 RVA: 0x000D1758 File Offset: 0x000CF958
	private void OnStartOptionUI()
	{
		this.Initialize();
	}

	// Token: 0x060022E2 RID: 8930 RVA: 0x000D1760 File Offset: 0x000CF960
	private void OnEndOptionUI()
	{
		if (this.m_systemSaveFlag)
		{
			SystemSaveManager instance = SystemSaveManager.Instance;
			if (instance != null)
			{
				instance.SaveSystemData();
			}
			this.m_systemSaveFlag = false;
		}
	}

	// Token: 0x170004BC RID: 1212
	// (get) Token: 0x060022E3 RID: 8931 RVA: 0x000D1798 File Offset: 0x000CF998
	public bool IsEndSetup
	{
		get
		{
			return this.m_inited;
		}
	}

	// Token: 0x04001F7D RID: 8061
	[SerializeField]
	private UIRectItemStorage m_itemStorage;

	// Token: 0x04001F7E RID: 8062
	[SerializeField]
	private UIScrollBar m_scrollBar;

	// Token: 0x04001F7F RID: 8063
	private GameObject m_backButtonObj;

	// Token: 0x04001F80 RID: 8064
	private bool m_inited;

	// Token: 0x04001F81 RID: 8065
	private bool m_systemSaveFlag;

	// Token: 0x04001F82 RID: 8066
	public readonly OptionUI.OptionInfo[] m_optionInfos = new OptionUI.OptionInfo[]
	{
		new OptionUI.OptionInfo
		{
			label = "users_results",
			icon = "ui_option_icon_note"
		},
		new OptionUI.OptionInfo
		{
			label = "buying_info",
			icon = "ui_option_icon_note",
			type = OptionType.BUYING_HISTORY
		},
		new OptionUI.OptionInfo
		{
			label = "push_notification",
			icon = "ui_option_icon_gear",
			type = OptionType.PUSH_NOTIFICATION
		},
		new OptionUI.OptionInfo
		{
			label = "weight_saving",
			icon = "ui_option_icon_gear",
			type = OptionType.WEIGHT_SAVING
		},
		new OptionUI.OptionInfo
		{
			label = "id_check",
			icon = "ui_option_icon_note",
			type = OptionType.ID_CHECK
		},
		new OptionUI.OptionInfo
		{
			label = "tutorial",
			icon = "ui_option_icon_note",
			type = OptionType.TUTORIAL
		},
		new OptionUI.OptionInfo
		{
			label = "past_results",
			icon = "ui_option_icon_arrow",
			type = OptionType.PAST_RESULTS
		},
		new OptionUI.OptionInfo
		{
			label = "staff_credit",
			icon = "ui_option_icon_note",
			type = OptionType.STAFF_CREDIT
		},
		new OptionUI.OptionInfo
		{
			label = "terms_of_service",
			icon = "ui_option_icon_arrow",
			type = OptionType.TERMS_OF_SERVICE
		},
		new OptionUI.OptionInfo
		{
			label = "user_name",
			icon = "ui_option_icon_gear",
			type = OptionType.USER_NAME
		},
		new OptionUI.OptionInfo
		{
			label = "sound_config",
			icon = "ui_option_icon_gear",
			type = OptionType.SOUND_CONFIG
		},
		new OptionUI.OptionInfo
		{
			label = "invite_friend",
			icon = "ui_option_icon_gear",
			type = OptionType.INVITE_FRIEND
		},
		new OptionUI.OptionInfo
		{
			label = "acceptance_of_invite",
			icon = "ui_option_icon_gear",
			type = OptionType.ACCEPT_INVITE
		},
		new OptionUI.OptionInfo
		{
			label = "facebook_access",
			icon = "ui_option_icon_gear",
			type = OptionType.FACEBOOK_ACCESS
		},
		new OptionUI.OptionInfo
		{
			label = "help",
			icon = "ui_option_icon_arrow",
			type = OptionType.HELP
		},
		new OptionUI.OptionInfo
		{
			label = "cash_clear",
			icon = "ui_option_icon_gear",
			type = OptionType.CACHE_CLEAR
		},
		new OptionUI.OptionInfo
		{
			label = "copyright",
			icon = "ui_option_icon_note",
			type = OptionType.COPYRIGHT
		},
		new OptionUI.OptionInfo
		{
			label = "back_title",
			icon = "ui_option_icon_arrow",
			type = OptionType.BACK_TITLE
		}
	};

	// Token: 0x0200048F RID: 1167
	public class OptionInfo
	{
		// Token: 0x04001F83 RID: 8067
		public string label;

		// Token: 0x04001F84 RID: 8068
		public string icon;

		// Token: 0x04001F85 RID: 8069
		public OptionType type;
	}
}
