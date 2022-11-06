using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000457 RID: 1111
public class HudItemPanel : MonoBehaviour
{
	// Token: 0x06002164 RID: 8548 RVA: 0x000C8E98 File Offset: 0x000C7098
	private void Start()
	{
		HudMenuUtility.SetTagHudSaveItem(base.gameObject);
		base.enabled = false;
	}

	// Token: 0x06002165 RID: 8549 RVA: 0x000C8EAC File Offset: 0x000C70AC
	private void Initialize()
	{
		GameObject mainMenuUIObject = HudMenuUtility.GetMainMenuUIObject();
		if (mainMenuUIObject != null)
		{
			Transform transform = mainMenuUIObject.transform.FindChild("Anchor_5_MC/mainmenu_contents/grid/page_1/slot/ui_mm_main2_page(Clone)/item_set/slot");
			if (transform != null)
			{
				this.m_slot_obj = transform.gameObject;
				if (this.m_slot_obj != null)
				{
					this.m_ui_rect_item_storage = this.m_slot_obj.GetComponent<UIRectItemStorage>();
				}
			}
		}
		if (this.m_ui_rect_item_storage != null)
		{
			this.m_init_flag = true;
			List<GameObject> list = GameObjectUtil.FindChildGameObjects(this.m_slot_obj, "ui_item_set_cell(Clone)");
			if (list.Count == this.m_ui_rect_item_storage.maxItemCount)
			{
				for (int i = 0; i < list.Count; i++)
				{
					BoxCollider component = list[i].GetComponent<BoxCollider>();
					if (component != null)
					{
						component.enabled = false;
					}
					this.m_item_cells.Add(list[i].GetComponent<ui_item_set_cell>());
				}
			}
		}
	}

	// Token: 0x06002166 RID: 8550 RVA: 0x000C8FA8 File Offset: 0x000C71A8
	public void OnUpdateSaveDataDisplay()
	{
		if (!this.m_init_flag)
		{
			this.Initialize();
		}
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null && this.m_item_cells != null && this.m_item_cells.Count >= 3)
		{
			if (this.m_item_cells[0])
			{
				ItemType itemType = instance.PlayerData.EquippedItem[0];
				uint itemCount = instance.ItemData.GetItemCount(itemType);
				this.m_item_cells[0].UpdateView((int)itemType, (int)itemCount);
			}
			if (this.m_item_cells[1])
			{
				ItemType itemType2 = instance.PlayerData.EquippedItem[1];
				uint itemCount2 = instance.ItemData.GetItemCount(itemType2);
				this.m_item_cells[1].UpdateView((int)itemType2, (int)itemCount2);
			}
			if (this.m_item_cells[2])
			{
				ItemType itemType3 = instance.PlayerData.EquippedItem[2];
				uint itemCount3 = instance.ItemData.GetItemCount(itemType3);
				this.m_item_cells[2].UpdateView((int)itemType3, (int)itemCount3);
			}
		}
	}

	// Token: 0x04001E31 RID: 7729
	private const string m_slot_obj_path = "Anchor_5_MC/mainmenu_contents/grid/page_1/slot/ui_mm_main2_page(Clone)/item_set/slot";

	// Token: 0x04001E32 RID: 7730
	private GameObject m_slot_obj;

	// Token: 0x04001E33 RID: 7731
	private List<ui_item_set_cell> m_item_cells = new List<ui_item_set_cell>();

	// Token: 0x04001E34 RID: 7732
	private UIRectItemStorage m_ui_rect_item_storage;

	// Token: 0x04001E35 RID: 7733
	private bool m_init_flag;
}
