using System;

// Token: 0x0200091D RID: 2333
public class ObjEnmBeetonData
{
	// Token: 0x06003D5B RID: 15707 RVA: 0x001417F4 File Offset: 0x0013F9F4
	public static string[] GetModelFiles()
	{
		return ObjEnmBeetonData.MODEL_FILES;
	}

	// Token: 0x06003D5C RID: 15708 RVA: 0x001417FC File Offset: 0x0013F9FC
	public static ObjEnemyUtil.EnemyEffectSize GetEffectSize()
	{
		return ObjEnemyUtil.EnemyEffectSize.S;
	}

	// Token: 0x04003547 RID: 13639
	private static readonly string[] MODEL_FILES = new string[]
	{
		"enm_beeton",
		"enm_beeton_m",
		"enm_beeton_g"
	};
}
