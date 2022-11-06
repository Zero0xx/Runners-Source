using System;

// Token: 0x02000945 RID: 2373
public class ObjEnmValkyneData
{
	// Token: 0x06003DF4 RID: 15860 RVA: 0x001426B4 File Offset: 0x001408B4
	public static string[] GetModelFiles()
	{
		return ObjEnmValkyneData.MODEL_FILES;
	}

	// Token: 0x06003DF5 RID: 15861 RVA: 0x001426BC File Offset: 0x001408BC
	public static ObjEnemyUtil.EnemyEffectSize GetEffectSize()
	{
		return ObjEnemyUtil.EnemyEffectSize.S;
	}

	// Token: 0x0400356C RID: 13676
	private static readonly string[] MODEL_FILES = new string[]
	{
		"enm_valkyne",
		"enm_valkyne_m",
		"enm_valkyne_g"
	};
}
