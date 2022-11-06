using System;
using UnityEngine;

// Token: 0x020003BA RID: 954
[AddComponentMenu("NGUI/Item/UI Rect Item Storage Slot")]
public class UIRectItemStorageSlot : UIRectItemSlot
{
	// Token: 0x170003FF RID: 1023
	// (get) Token: 0x06001BCC RID: 7116 RVA: 0x000A5368 File Offset: 0x000A3568
	protected override UIInvGameItem observedItem
	{
		get
		{
			UIInvGameItem uiinvGameItem = (!(this.storage != null)) ? null : this.storage.GetItem(this.slot);
			if (uiinvGameItem == null)
			{
				uiinvGameItem = ((!(this.storageRanking != null)) ? null : this.storageRanking.GetItem(this.slot));
			}
			return uiinvGameItem;
		}
	}

	// Token: 0x06001BCD RID: 7117 RVA: 0x000A53D0 File Offset: 0x000A35D0
	protected override UIInvGameItem Replace(UIInvGameItem item)
	{
		UIInvGameItem result;
		if (this.storage != null)
		{
			result = this.storage.Replace(this.slot, item);
		}
		else if (this.storageRanking != null)
		{
			result = this.storageRanking.Replace(this.slot, item);
		}
		else
		{
			result = item;
		}
		return result;
	}

	// Token: 0x0400197F RID: 6527
	public UIRectItemStorage storage;

	// Token: 0x04001980 RID: 6528
	public UIRectItemStorageRanking storageRanking;

	// Token: 0x04001981 RID: 6529
	public int slot;
}
