using System;

// Token: 0x0200011E RID: 286
public class CameraTypeData
{
	// Token: 0x060008DA RID: 2266 RVA: 0x00031DD4 File Offset: 0x0002FFD4
	public static bool IsGimmickCamera(CameraType type)
	{
		return type < CameraType.NUM && CameraTypeData.GIMMICK_CMAERA_TBL[(int)type] == 1;
	}

	// Token: 0x0400067A RID: 1658
	private static readonly int[] GIMMICK_CMAERA_TBL = new int[]
	{
		0,
		1,
		1,
		1,
		1,
		1
	};
}
