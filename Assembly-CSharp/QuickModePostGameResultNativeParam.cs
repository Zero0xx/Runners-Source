using System;
using System.Runtime.InteropServices;

// Token: 0x0200010F RID: 271
public struct QuickModePostGameResultNativeParam
{
	// Token: 0x060007F4 RID: 2036 RVA: 0x0002F1CC File Offset: 0x0002D3CC
	public void Init(ServerQuickModeGameResults resultData)
	{
		if (resultData != null)
		{
			BindingLinkUtility.LongToIntArray(out this.score, resultData.m_score);
			BindingLinkUtility.LongToIntArray(out this.numRings, resultData.m_numRings);
			BindingLinkUtility.LongToIntArray(out this.numFailureRings, resultData.m_numFailureRings);
			BindingLinkUtility.LongToIntArray(out this.numRedStarRings, resultData.m_numRedStarRings);
			BindingLinkUtility.LongToIntArray(out this.distance, resultData.m_distance);
			BindingLinkUtility.LongToIntArray(out this.dailyChallengeValue, resultData.m_dailyMissionValue);
			BindingLinkUtility.LongToIntArray(out this.numAnimals, resultData.m_numAnimals);
			this.maxComboCount = resultData.m_maxComboCount;
			this.closed = resultData.m_isSuspended;
			this.dailyChallengeComplete = resultData.m_dailyMissionComplete;
		}
	}

	// Token: 0x0400061F RID: 1567
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
	public int[] score;

	// Token: 0x04000620 RID: 1568
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
	public int[] distance;

	// Token: 0x04000621 RID: 1569
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
	public int[] numRings;

	// Token: 0x04000622 RID: 1570
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
	public int[] numFailureRings;

	// Token: 0x04000623 RID: 1571
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
	public int[] numRedStarRings;

	// Token: 0x04000624 RID: 1572
	public bool dailyChallengeComplete;

	// Token: 0x04000625 RID: 1573
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
	public int[] dailyChallengeValue;

	// Token: 0x04000626 RID: 1574
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
	public int[] numAnimals;

	// Token: 0x04000627 RID: 1575
	public bool closed;

	// Token: 0x04000628 RID: 1576
	public int maxComboCount;
}
