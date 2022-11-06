using System;

// Token: 0x02000928 RID: 2344
public class ObjEnmGaniganiData
{
	// Token: 0x06003D81 RID: 15745 RVA: 0x00141968 File Offset: 0x0013FB68
	public static string[] GetModelFiles()
	{
		return ObjEnmGaniganiData.MODEL_FILES;
	}

	// Token: 0x06003D82 RID: 15746 RVA: 0x00141970 File Offset: 0x0013FB70
	public static ObjEnemyUtil.EnemyEffectSize GetEffectSize()
	{
		return ObjEnemyUtil.EnemyEffectSize.M;
	}

	// Token: 0x0400354A RID: 13642
	private static readonly string[] MODEL_FILES = new string[]
	{
		"enm_ganigani",
		"enm_ganigani_m",
		"enm_ganigani_g"
	};
}
