using System;
using GameScore;

// Token: 0x02000937 RID: 2359
public class ObjEnemyUtil
{
	// Token: 0x06003DC8 RID: 15816 RVA: 0x001424CC File Offset: 0x001406CC
	public static int[] GetDefaultScoreTable()
	{
		return ObjEnemyUtil.SOCRE_TABLE;
	}

	// Token: 0x06003DC9 RID: 15817 RVA: 0x001424D4 File Offset: 0x001406D4
	public static string GetEffectName(ObjEnemyUtil.EnemyEffectSize size)
	{
		if ((ulong)size < (ulong)((long)ObjEnemyUtil.EFFECT_FILES.Length))
		{
			return ObjEnemyUtil.EFFECT_FILES[(int)((UIntPtr)size)];
		}
		return string.Empty;
	}

	// Token: 0x06003DCA RID: 15818 RVA: 0x001424F4 File Offset: 0x001406F4
	public static string GetSEName(ObjEnemyUtil.EnemyType type)
	{
		if ((ulong)type < (ulong)((long)ObjEnemyUtil.SE_NAMETBL.Length))
		{
			return ObjEnemyUtil.SE_NAMETBL[(int)((UIntPtr)type)];
		}
		return string.Empty;
	}

	// Token: 0x06003DCB RID: 15819 RVA: 0x00142514 File Offset: 0x00140714
	public static string GetModelName(ObjEnemyUtil.EnemyType type, string[] model_files)
	{
		if (model_files != null && (ulong)type < (ulong)((long)model_files.Length))
		{
			return model_files[(int)((UIntPtr)type)];
		}
		return string.Empty;
	}

	// Token: 0x06003DCC RID: 15820 RVA: 0x00142534 File Offset: 0x00140734
	public static int GetScore(ObjEnemyUtil.EnemyType type, int[] score_table)
	{
		if ((ulong)type < (ulong)((long)score_table.Length))
		{
			return score_table[(int)((UIntPtr)type)];
		}
		return 0;
	}

	// Token: 0x0400355D RID: 13661
	private static readonly string[] EFFECT_FILES = new string[]
	{
		"ef_en_dead_s01",
		"ef_en_dead_m01",
		"ef_en_dead_l01"
	};

	// Token: 0x0400355E RID: 13662
	private static readonly string[] SE_NAMETBL = new string[]
	{
		"enm_dead",
		"enm_metal_dead",
		"enm_gold_dead"
	};

	// Token: 0x0400355F RID: 13663
	private static readonly int[] SOCRE_TABLE = new int[]
	{
		Data.EnemyNormal,
		Data.EnemyMetal,
		Data.EnemyRare
	};

	// Token: 0x02000938 RID: 2360
	public enum EnemyEffectSize : uint
	{
		// Token: 0x04003561 RID: 13665
		S,
		// Token: 0x04003562 RID: 13666
		M,
		// Token: 0x04003563 RID: 13667
		L,
		// Token: 0x04003564 RID: 13668
		NUM
	}

	// Token: 0x02000939 RID: 2361
	public enum EnemyType : uint
	{
		// Token: 0x04003566 RID: 13670
		NORMAL,
		// Token: 0x04003567 RID: 13671
		METAL,
		// Token: 0x04003568 RID: 13672
		RARE,
		// Token: 0x04003569 RID: 13673
		NUM
	}
}
