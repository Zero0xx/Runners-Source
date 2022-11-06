using System;

// Token: 0x02000940 RID: 2368
public class ObjEnmSpinaData
{
	// Token: 0x06003DE3 RID: 15843 RVA: 0x00142620 File Offset: 0x00140820
	public static string[] GetModelFiles()
	{
		return ObjEnmSpinaData.MODEL_FILES;
	}

	// Token: 0x06003DE4 RID: 15844 RVA: 0x00142628 File Offset: 0x00140828
	public static ObjEnemyUtil.EnemyEffectSize GetEffectSize()
	{
		return ObjEnemyUtil.EnemyEffectSize.S;
	}

	// Token: 0x0400356B RID: 13675
	private static readonly string[] MODEL_FILES = new string[]
	{
		"enm_spina",
		"enm_spina_m",
		"enm_spina_g"
	};
}
