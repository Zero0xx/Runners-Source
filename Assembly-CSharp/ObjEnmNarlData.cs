using System;

// Token: 0x0200092E RID: 2350
public class ObjEnmNarlData
{
	// Token: 0x06003D95 RID: 15765 RVA: 0x00141A30 File Offset: 0x0013FC30
	public static string[] GetModelFiles()
	{
		return ObjEnmNarlData.MODEL_FILES;
	}

	// Token: 0x06003D96 RID: 15766 RVA: 0x00141A38 File Offset: 0x0013FC38
	public static ObjEnemyUtil.EnemyEffectSize GetEffectSize()
	{
		return ObjEnemyUtil.EnemyEffectSize.M;
	}

	// Token: 0x0400354C RID: 13644
	private static readonly string[] MODEL_FILES = new string[]
	{
		"enm_narl",
		"enm_narl_m",
		"enm_narl_g"
	};
}
