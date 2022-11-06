using System;

// Token: 0x0200092B RID: 2347
public class ObjEnmMotoraData
{
	// Token: 0x06003D8B RID: 15755 RVA: 0x001419CC File Offset: 0x0013FBCC
	public static string[] GetModelFiles()
	{
		return ObjEnmMotoraData.MODEL_FILES;
	}

	// Token: 0x06003D8C RID: 15756 RVA: 0x001419D4 File Offset: 0x0013FBD4
	public static ObjEnemyUtil.EnemyEffectSize GetEffectSize()
	{
		return ObjEnemyUtil.EnemyEffectSize.S;
	}

	// Token: 0x0400354B RID: 13643
	private static readonly string[] MODEL_FILES = new string[]
	{
		"enm_motora",
		"enm_motora_m",
		"enm_motora_g"
	};
}
