using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x0200042E RID: 1070
public class ui_item_set_cell : MonoBehaviour
{
	// Token: 0x06002088 RID: 8328 RVA: 0x000C3258 File Offset: 0x000C1458
	private void Start()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06002089 RID: 8329 RVA: 0x000C3268 File Offset: 0x000C1468
	public void UpdateView(ItemType itemType, int count)
	{
		this.UpdateView((int)itemType, count);
	}

	// Token: 0x0600208A RID: 8330 RVA: 0x000C3274 File Offset: 0x000C1474
	public void UpdateView(int id, int count)
	{
		this.m_id = id;
		this.m_count = count;
		if (id == -1)
		{
			base.gameObject.SetActive(false);
		}
		else
		{
			base.gameObject.SetActive(true);
			UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_item_volume");
			if (uilabel != null)
			{
				uilabel.text = count.ToString();
			}
			bool flag = base.gameObject.transform.parent.name == "slot" || base.gameObject.transform.parent.name == "slot_equip";
			UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_item");
			if (uisprite != null)
			{
				uisprite.spriteName = "ui_cmn_icon_item_" + ((count != 0 || !flag) ? string.Empty : "g_") + id.ToString();
			}
		}
	}

	// Token: 0x0600208B RID: 8331 RVA: 0x000C3374 File Offset: 0x000C1574
	private void OnClick()
	{
		string name = base.gameObject.transform.parent.name;
		if (name != null)
		{
			if (ui_item_set_cell.<>f__switch$map6 == null)
			{
				ui_item_set_cell.<>f__switch$map6 = new Dictionary<string, int>(3)
				{
					{
						"slot",
						0
					},
					{
						"slot_equip",
						1
					},
					{
						"slot_item",
						1
					}
				};
			}
			int num;
			if (ui_item_set_cell.<>f__switch$map6.TryGetValue(name, out num))
			{
				if (num != 0)
				{
					if (num == 1)
					{
						ItemSetWindowEquipUI itemSetWindowEquipUI = GameObjectUtil.FindChildGameObjectComponent<ItemSetWindowEquipUI>(base.gameObject.transform.root.gameObject, "ItemSetWindowEquipUI");
						if (itemSetWindowEquipUI != null)
						{
							itemSetWindowEquipUI.gameObject.SetActive(true);
							itemSetWindowEquipUI.OpenWindow(this.m_id, this.m_count);
						}
					}
				}
				else
				{
					SoundManager.SePlay("sys_menu_decide", "SE");
					Animation animation = GameObjectUtil.FindGameObjectComponent<Animation>("menu_Anim");
					if (animation != null)
					{
						ActiveAnimation.Play(animation, "ui_menu_item_Anim", Direction.Forward, EnableCondition.DoNothing, DisableCondition.DoNotDisable);
					}
				}
			}
		}
	}

	// Token: 0x04001D19 RID: 7449
	private int m_id;

	// Token: 0x04001D1A RID: 7450
	private int m_count;
}
