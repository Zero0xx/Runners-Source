using System;

// Token: 0x02000925 RID: 2341
public class ObjEnmEggpawnData
{
	// Token: 0x06003D75 RID: 15733 RVA: 0x001418E4 File Offset: 0x0013FAE4
	public static string[] GetModelFiles()
	{
		return ObjEnmEggpawnData.MODEL_FILES;
	}

	// Token: 0x06003D76 RID: 15734 RVA: 0x001418EC File Offset: 0x0013FAEC
	public static ObjEnemyUtil.EnemyEffectSize GetEffectSize()
	{
		return ObjEnemyUtil.EnemyEffectSize.M;
	}

	// Token: 0x04003549 RID: 13641
	private static readonly string[] MODEL_FILES = new string[]
	{
		"enm_eggpawn",
		"enm_eggpawn_m",
		"enm_eggpawn_g"
	};
}
