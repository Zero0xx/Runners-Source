using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003B5 RID: 949
[AddComponentMenu("NGUI/Item/UI Rect Item Storage")]
public class UIRectItemStorage : MonoBehaviour
{
	// Token: 0x170003FC RID: 1020
	// (get) Token: 0x06001BA6 RID: 7078 RVA: 0x000A365C File Offset: 0x000A185C
	public List<UIInvGameItem> items
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

	// Token: 0x06001BA7 RID: 7079 RVA: 0x000A368C File Offset: 0x000A188C
	public UIInvGameItem GetItem(int slot)
	{
		return (slot >= this.items.Count) ? null : this.mItems[slot];
	}

	// Token: 0x06001BA8 RID: 7080 RVA: 0x000A36B4 File Offset: 0x000A18B4
	public UIInvGameItem Replace(int slot, UIInvGameItem item)
	{
		if (slot < this.maxItemCount)
		{
			UIInvGameItem result = this.items[slot];
			this.mItems[slot] = item;
			return result;
		}
		return item;
	}

	// Token: 0x06001BA9 RID: 7081 RVA: 0x000A36EC File Offset: 0x000A18EC
	private void Start()
	{
		if (!this.m_initCountainer)
		{
			this.InitContainer();
		}
	}

	// Token: 0x06001BAA RID: 7082 RVA: 0x000A3700 File Offset: 0x000A1900
	private void Place(int x, int y, int count, Bounds b)
	{
		GameObject gameObject = NGUITools.AddChild(base.gameObject, this.template);
		if (gameObject != null)
		{
			Transform transform = gameObject.transform;
			transform.localPosition = new Vector3((float)this.padding + ((float)x + 0.5f) * (float)this.spacing_x, (float)(-(float)this.padding) - ((float)y + 0.5f) * (float)this.spacing_y, 0f);
			UIRectItemStorageSlot component = gameObject.GetComponent<UIRectItemStorageSlot>();
			if (component != null)
			{
				component.storage = this;
				component.slot = count;
			}
			UIRectItemStorage.ActiveType activeType = this.m_activeType;
			if (activeType != UIRectItemStorage.ActiveType.ACTIVE)
			{
				if (activeType == UIRectItemStorage.ActiveType.NOT_ACTTIVE)
				{
					gameObject.SetActive(false);
				}
			}
			else
			{
				gameObject.SetActive(true);
			}
		}
		b.Encapsulate(new Vector3((float)this.padding * 2f + (float)((x + 1) * this.spacing_x), (float)(-(float)this.padding) * 2f - (float)((y + 1) * this.spacing_y), 0f));
		if (++count >= this.maxItemCount && this.background != null)
		{
			this.background.transform.localScale = b.size;
		}
	}

	// Token: 0x06001BAB RID: 7083 RVA: 0x000A3844 File Offset: 0x000A1A44
	public void Restart()
	{
		GameObject gameObject = base.gameObject;
		GameObject[] array = new GameObject[gameObject.transform.childCount];
		for (int i = 0; i < gameObject.transform.childCount; i++)
		{
			array[i] = gameObject.transform.GetChild(i).gameObject;
		}
		foreach (GameObject gameObject2 in array)
		{
			gameObject2.transform.parent = null;
			gameObject2.SetActive(false);
			UnityEngine.Object.Destroy(gameObject2);
		}
		this.InitContainer();
	}

	// Token: 0x06001BAC RID: 7084 RVA: 0x000A38DC File Offset: 0x000A1ADC
	private void InitContainer()
	{
		if (this.template != null)
		{
			int count = 0;
			Bounds b = default(Bounds);
			if (this.isPlaceVertical)
			{
				for (int i = 0; i < this.maxColumns; i++)
				{
					for (int j = 0; j < this.maxRows; j++)
					{
						this.Place(i, j, count, b);
					}
				}
			}
			else
			{
				for (int k = 0; k < this.maxRows; k++)
				{
					for (int l = 0; l < this.maxColumns; l++)
					{
						this.Place(l, k, count, b);
					}
				}
			}
			if (this.background != null)
			{
				this.background.transform.localScale = b.size;
			}
			this.m_initCountainer = true;
		}
	}

	// Token: 0x06001BAD RID: 7085 RVA: 0x000A39C0 File Offset: 0x000A1BC0
	public void Strip()
	{
		while (base.transform.childCount > this.maxItemCount)
		{
			GameObject gameObject = base.transform.GetChild(base.transform.childCount - 1).gameObject;
			gameObject.transform.parent = null;
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	// Token: 0x04001949 RID: 6473
	public bool isPlaceVertical;

	// Token: 0x0400194A RID: 6474
	public int maxItemCount = 8;

	// Token: 0x0400194B RID: 6475
	public int maxRows = 4;

	// Token: 0x0400194C RID: 6476
	public int maxColumns = 4;

	// Token: 0x0400194D RID: 6477
	public GameObject template;

	// Token: 0x0400194E RID: 6478
	public UIWidget background;

	// Token: 0x0400194F RID: 6479
	public int spacing_x = 128;

	// Token: 0x04001950 RID: 6480
	public int spacing_y = 128;

	// Token: 0x04001951 RID: 6481
	public int padding = 10;

	// Token: 0x04001952 RID: 6482
	private List<UIInvGameItem> mItems = new List<UIInvGameItem>();

	// Token: 0x04001953 RID: 6483
	public UIRectItemStorage.ActiveType m_activeType = UIRectItemStorage.ActiveType.DEFAULT;

	// Token: 0x04001954 RID: 6484
	private bool m_initCountainer;

	// Token: 0x020003B6 RID: 950
	public enum ActiveType
	{
		// Token: 0x04001956 RID: 6486
		ACTIVE,
		// Token: 0x04001957 RID: 6487
		NOT_ACTTIVE,
		// Token: 0x04001958 RID: 6488
		DEFAULT
	}
}
