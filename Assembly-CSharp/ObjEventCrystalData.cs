using System;
using GameScore;

// Token: 0x020008D1 RID: 2257
public class ObjEventCrystalData
{
	// Token: 0x06003C0A RID: 15370 RVA: 0x0013C9E8 File Offset: 0x0013ABE8
	public static CtystalParam GetCtystalParam(EventCtystalType type)
	{
		if (type < (EventCtystalType)ObjEventCrystalData.PARAM_TBL.Length)
		{
			return ObjEventCrystalData.PARAM_TBL[(int)type];
		}
		return ObjEventCrystalData.PARAM_TBL[0];
	}

	// Token: 0x0400345D RID: 13405
	private static readonly CtystalParam[] PARAM_TBL = new CtystalParam[]
	{
		new CtystalParam(false, "ObjEventCrystal", string.Empty, "ef_ob_get_crystal_rd_s01", "obj_crystal_red", Data.EventCrystal, false),
		new CtystalParam(true, "ObjEventCrystal10", string.Empty, "ef_ob_get_crystal_rd_l01", "obj_big_crystal", Data.EventCrystal10, false)
	};
}
