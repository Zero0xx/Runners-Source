using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

// Token: 0x0200010B RID: 267
public struct ActStartNativeParam
{
	// Token: 0x060007F0 RID: 2032 RVA: 0x0002EF94 File Offset: 0x0002D194
	public void Init(List<int> itemList, bool tutorial, int eventId)
	{
		this.modifire = new int[6];
		for (int i = 0; i < 6; i++)
		{
			if (i < itemList.Count)
			{
				this.modifire[i] = itemList[i];
			}
			else
			{
				this.modifire[i] = -1;
			}
		}
		this.tutorial = tutorial;
		this.eventId = eventId;
	}

	// Token: 0x04000603 RID: 1539
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
	public int[] modifire;

	// Token: 0x04000604 RID: 1540
	public bool tutorial;

	// Token: 0x04000605 RID: 1541
	public int eventId;
}
