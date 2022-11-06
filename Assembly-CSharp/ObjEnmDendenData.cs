using System;

// Token: 0x02000922 RID: 2338
public class ObjEnmDendenData
{
	// Token: 0x06003D6B RID: 15723 RVA: 0x00141880 File Offset: 0x0013FA80
	public static string[] GetModelFiles()
	{
		return ObjEnmDendenData.MODEL_FILES;
	}

	// Token: 0x06003D6C RID: 15724 RVA: 0x00141888 File Offset: 0x0013FA88
	public static ObjEnemyUtil.EnemyEffectSize GetEffectSize()
	{
		return ObjEnemyUtil.EnemyEffectSize.M;
	}

	// Token: 0x04003548 RID: 13640
	private static readonly string[] MODEL_FILES = new string[]
	{
		"enm_denden",
		"enm_denden_m",
		"enm_denden_g"
	};
}
