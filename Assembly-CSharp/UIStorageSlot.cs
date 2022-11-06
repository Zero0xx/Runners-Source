using System;
using UnityEngine;

// Token: 0x02000052 RID: 82
[AddComponentMenu("NGUI/Examples/UI Storage Slot")]
public class UIStorageSlot : UIItemSlot
{
	// Token: 0x17000068 RID: 104
	// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000B764 File Offset: 0x00009964
	protected override InvGameItem observedItem
	{
		get
		{
			return (!(this.storage != null)) ? null : this.storage.GetItem(this.slot);
		}
	}

	// Token: 0x060002A1 RID: 673 RVA: 0x0000B79C File Offset: 0x0000999C
	protected override InvGameItem Replace(InvGameItem item)
	{
		return (!(this.storage != null)) ? item : this.storage.Replace(this.slot, item);
	}

	// Token: 0x04000134 RID: 308
	public UIItemStorage storage;

	// Token: 0x04000135 RID: 309
	public int slot;
}
