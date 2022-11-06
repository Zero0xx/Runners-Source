using System;

// Token: 0x02000852 RID: 2130
public class ObjBossZazz2 : ObjBossZazz1
{
	// Token: 0x06003A24 RID: 14884 RVA: 0x00132774 File Offset: 0x00130974
	protected override string GetModelName()
	{
		return "enm_zazz_r";
	}

	// Token: 0x06003A25 RID: 14885 RVA: 0x0013277C File Offset: 0x0013097C
	protected override ResourceCategory GetModelCategory()
	{
		return ResourceCategory.EVENT_RESOURCE;
	}

	// Token: 0x06003A26 RID: 14886 RVA: 0x00132780 File Offset: 0x00130980
	protected override BossType GetBossType()
	{
		return BossType.EVENT2;
	}

	// Token: 0x040030F8 RID: 12536
	private const string ModelName = "enm_zazz_r";
}
