using System;

// Token: 0x020008CD RID: 2253
public class ObjEventCrystal : ObjEventCrystalBase
{
	// Token: 0x06003BF2 RID: 15346 RVA: 0x0013C628 File Offset: 0x0013A828
	protected override string GetModelName()
	{
		return EventSPStageObjectTable.GetSPCrystalModel();
	}

	// Token: 0x06003BF3 RID: 15347 RVA: 0x0013C630 File Offset: 0x0013A830
	protected override int GetAddCount()
	{
		return 1;
	}

	// Token: 0x06003BF4 RID: 15348 RVA: 0x0013C634 File Offset: 0x0013A834
	protected override EventCtystalType GetOriginalType()
	{
		return EventCtystalType.SMALL;
	}
}
