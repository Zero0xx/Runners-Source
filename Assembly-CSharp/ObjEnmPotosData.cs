using System;

// Token: 0x0200093B RID: 2363
public class ObjEnmPotosData
{
	// Token: 0x06003DD3 RID: 15827 RVA: 0x00142594 File Offset: 0x00140794
	public static string[] GetModelFiles()
	{
		return ObjEnmPotosData.MODEL_FILES;
	}

	// Token: 0x06003DD4 RID: 15828 RVA: 0x0014259C File Offset: 0x0014079C
	public static ObjEnemyUtil.EnemyEffectSize GetEffectSize()
	{
		return ObjEnemyUtil.EnemyEffectSize.S;
	}

	// Token: 0x0400356A RID: 13674
	private static readonly string[] MODEL_FILES = new string[]
	{
		"enm_potos",
		"enm_potos_m",
		"enm_potos_g"
	};
}
