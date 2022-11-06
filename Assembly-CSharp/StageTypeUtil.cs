using System;

// Token: 0x02000324 RID: 804
public class StageTypeUtil
{
	// Token: 0x060017C2 RID: 6082 RVA: 0x000881FC File Offset: 0x000863FC
	public static string GetName(StageType stageType)
	{
		return "w" + ((int)(stageType + 1)).ToString("D2");
	}

	// Token: 0x060017C3 RID: 6083 RVA: 0x00088224 File Offset: 0x00086424
	public static string GetCueSheetName(StageType stageType)
	{
		return "BGM_z" + ((int)(stageType + 1)).ToString("D2");
	}

	// Token: 0x060017C4 RID: 6084 RVA: 0x0008824C File Offset: 0x0008644C
	public static string GetBgmName(StageType stageType)
	{
		return "bgm_z_" + StageTypeUtil.GetName(stageType);
	}

	// Token: 0x060017C5 RID: 6085 RVA: 0x00088260 File Offset: 0x00086460
	public static string GetQuickModeCueSheetName(StageType stageType)
	{
		int num = 1;
		if (stageType != StageType.W01 && stageType != StageType.W02 && stageType != StageType.W03)
		{
			num = (int)(stageType + 1);
		}
		return "BGM_q" + num.ToString("D2");
	}

	// Token: 0x060017C6 RID: 6086 RVA: 0x000882A0 File Offset: 0x000864A0
	public static string GetQuickModeBgmName(StageType in_stageType)
	{
		StageType stageType = in_stageType;
		if (stageType == StageType.W02 || stageType == StageType.W03)
		{
			stageType = StageType.W01;
		}
		return "bgm_q_" + StageTypeUtil.GetName(stageType);
	}

	// Token: 0x060017C7 RID: 6087 RVA: 0x000882D0 File Offset: 0x000864D0
	public static StageType GetType(string stageName)
	{
		for (int i = 0; i < 30; i++)
		{
			if (stageName == StageTypeUtil.GetName((StageType)i))
			{
				return (StageType)i;
			}
		}
		return StageType.NONE;
	}

	// Token: 0x060017C8 RID: 6088 RVA: 0x00088304 File Offset: 0x00086504
	public static string GetBgmName(string stageName)
	{
		return StageTypeUtil.GetBgmName(StageTypeUtil.GetType(stageName));
	}

	// Token: 0x060017C9 RID: 6089 RVA: 0x00088314 File Offset: 0x00086514
	public static string GetCueSheetName(string stageName)
	{
		return StageTypeUtil.GetCueSheetName(StageTypeUtil.GetType(stageName));
	}

	// Token: 0x060017CA RID: 6090 RVA: 0x00088324 File Offset: 0x00086524
	public static string GetQuickModeBgmName(string stageName)
	{
		return StageTypeUtil.GetQuickModeBgmName(StageTypeUtil.GetType(stageName));
	}

	// Token: 0x060017CB RID: 6091 RVA: 0x00088334 File Offset: 0x00086534
	public static string GetQuickModeCueSheetName(string stageName)
	{
		return StageTypeUtil.GetQuickModeCueSheetName(StageTypeUtil.GetType(stageName));
	}

	// Token: 0x060017CC RID: 6092 RVA: 0x00088344 File Offset: 0x00086544
	public static string GetStageName(string nameStr)
	{
		for (int i = 0; i < 30; i++)
		{
			string name = StageTypeUtil.GetName((StageType)i);
			if (nameStr.Contains(name))
			{
				return name;
			}
		}
		return string.Empty;
	}

	// Token: 0x04001566 RID: 5478
	private const string stageBaseName = "w";

	// Token: 0x04001567 RID: 5479
	private const string bgmBaseName = "bgm_z_";

	// Token: 0x04001568 RID: 5480
	private const string bgmCueSheetName = "BGM_z";

	// Token: 0x04001569 RID: 5481
	private const string bgmQuickModeBaseName = "bgm_q_";

	// Token: 0x0400156A RID: 5482
	private const string bgmQuickModeCueSheetName = "BGM_q";
}
