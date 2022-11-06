using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

// Token: 0x0200010C RID: 268
public struct QuickModeActStartNativeParam
{
	// Token: 0x060007F1 RID: 2033 RVA: 0x0002EFF8 File Offset: 0x0002D1F8
	public void Init(List<int> itemList, int tutorial)
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
		this.m_tutorial = tutorial;
	}

	// Token: 0x04000606 RID: 1542
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
	public int[] modifire;

	// Token: 0x04000607 RID: 1543
	public int m_tutorial;
}
