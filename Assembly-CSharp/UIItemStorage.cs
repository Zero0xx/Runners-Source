using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000051 RID: 81
[AddComponentMenu("NGUI/Examples/UI Item Storage")]
public class UIItemStorage : MonoBehaviour
{
	// Token: 0x17000067 RID: 103
	// (get) Token: 0x0600029B RID: 667 RVA: 0x0000B54C File Offset: 0x0000974C
	public List<InvGameItem> items
	{
		get
		{
			while (this.mItems.Count < this.maxItemCount)
			{
				this.mItems.Add(null);
			}
			return this.mItems;
		}
	}

	// Token: 0x0600029C RID: 668 RVA: 0x0000B57C File Offset: 0x0000977C
	public InvGameItem GetItem(int slot)
	{
		return (slot >= this.items.Count) ? null : this.mItems[slot];
	}

	// Token: 0x0600029D RID: 669 RVA: 0x0000B5A4 File Offset: 0x000097A4
	public InvGameItem Replace(int slot, InvGameItem item)
	{
		if (slot < this.maxItemCount)
		{
			InvGameItem result = this.items[slot];
			this.mItems[slot] = item;
			return result;
		}
		return item;
	}

	// Token: 0x0600029E RID: 670 RVA: 0x0000B5DC File Offset: 0x000097DC
	private void Start()
	{
		if (this.template != null)
		{
			int num = 0;
			Bounds bounds = default(Bounds);
			for (int i = 0; i < this.maxRows; i++)
			{
				for (int j = 0; j < this.maxColumns; j++)
				{
					GameObject gameObject = NGUITools.AddChild(base.gameObject, this.template);
					Transform transform = gameObject.transform;
					transform.localPosition = new Vector3((float)this.padding + ((float)j + 0.5f) * (float)this.spacing, (float)(-(float)this.padding) - ((float)i + 0.5f) * (float)this.spacing, 0f);
					UIStorageSlot component = gameObject.GetComponent<UIStorageSlot>();
					if (component != null)
					{
						component.storage = this;
						component.slot = num;
					}
					bounds.Encapsulate(new Vector3((float)this.padding * 2f + (float)((j + 1) * this.spacing), (float)(-(float)this.padding) * 2f - (float)((i + 1) * this.spacing), 0f));
					if (++num >= this.maxItemCount)
					{
						if (this.background != null)
						{
							this.background.transform.localScale = bounds.size;
						}
						return;
					}
				}
			}
			if (this.background != null)
			{
				this.background.transform.localScale = bounds.size;
			}
		}
	}

	// Token: 0x0400012C RID: 300
	public int maxItemCount = 8;

	// Token: 0x0400012D RID: 301
	public int maxRows = 4;

	// Token: 0x0400012E RID: 302
	public int maxColumns = 4;

	// Token: 0x0400012F RID: 303
	public GameObject template;

	// Token: 0x04000130 RID: 304
	public UIWidget background;

	// Token: 0x04000131 RID: 305
	public int spacing = 128;

	// Token: 0x04000132 RID: 306
	public int padding = 10;

	// Token: 0x04000133 RID: 307
	private List<InvGameItem> mItems = new List<InvGameItem>();
}
