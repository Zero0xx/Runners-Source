using System;

// Token: 0x02000856 RID: 2134
public class ObjBossZomom1 : ObjBossZazz1
{
	// Token: 0x06003A31 RID: 14897 RVA: 0x001333E0 File Offset: 0x001315E0
	protected override string GetModelName()
	{
		return "enm_zazz";
	}

	// Token: 0x06003A32 RID: 14898 RVA: 0x001333E8 File Offset: 0x001315E8
	protected override ResourceCategory GetModelCategory()
	{
		return ResourceCategory.EVENT_RESOURCE;
	}

	// Token: 0x04003160 RID: 12640
	private const string ModelName = "enm_zazz";
}
