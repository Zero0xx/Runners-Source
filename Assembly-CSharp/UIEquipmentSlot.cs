using System;
using UnityEngine;

// Token: 0x0200004F RID: 79
[AddComponentMenu("NGUI/Examples/UI Equipment Slot")]
public class UIEquipmentSlot : UIItemSlot
{
	// Token: 0x17000065 RID: 101
	// (get) Token: 0x0600028F RID: 655 RVA: 0x0000B05C File Offset: 0x0000925C
	protected override InvGameItem observedItem
	{
		get
		{
			return (!(this.equipment != null)) ? null : this.equipment.GetItem(this.slot);
		}
	}

	// Token: 0x06000290 RID: 656 RVA: 0x0000B094 File Offset: 0x00009294
	protected override InvGameItem Replace(InvGameItem item)
	{
		return (!(this.equipment != null)) ? item : this.equipment.Replace(this.slot, item);
	}

	// Token: 0x04000121 RID: 289
	public InvEquipment equipment;

	// Token: 0x04000122 RID: 290
	public InvBaseItem.Slot slot;
}
