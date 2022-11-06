using System;

// Token: 0x02000858 RID: 2136
public class ObjBossZomom3 : ObjBossZazz3
{
	// Token: 0x06003A37 RID: 14903 RVA: 0x00133408 File Offset: 0x00131608
	protected override string GetModelName()
	{
		return "enm_zazz";
	}

	// Token: 0x06003A38 RID: 14904 RVA: 0x00133410 File Offset: 0x00131610
	protected override ResourceCategory GetModelCategory()
	{
		return ResourceCategory.EVENT_RESOURCE;
	}

	// Token: 0x04003162 RID: 12642
	private const string ModelName = "enm_zazz";
}
