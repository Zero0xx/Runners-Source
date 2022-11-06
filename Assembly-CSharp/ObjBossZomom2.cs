using System;

// Token: 0x02000857 RID: 2135
public class ObjBossZomom2 : ObjBossZazz1
{
	// Token: 0x06003A34 RID: 14900 RVA: 0x001333F4 File Offset: 0x001315F4
	protected override string GetModelName()
	{
		return "enm_zazz";
	}

	// Token: 0x06003A35 RID: 14901 RVA: 0x001333FC File Offset: 0x001315FC
	protected override ResourceCategory GetModelCategory()
	{
		return ResourceCategory.EVENT_RESOURCE;
	}

	// Token: 0x04003161 RID: 12641
	private const string ModelName = "enm_zazz";
}
