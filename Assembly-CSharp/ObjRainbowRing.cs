using System;
using GameScore;

// Token: 0x020008E0 RID: 2272
public class ObjRainbowRing : ObjDashRing
{
	// Token: 0x06003C3C RID: 15420 RVA: 0x0013D250 File Offset: 0x0013B450
	protected override string GetModelName()
	{
		return "obj_cmn_rainbowring";
	}

	// Token: 0x06003C3D RID: 15421 RVA: 0x0013D258 File Offset: 0x0013B458
	protected override string GetEffectName()
	{
		return "ef_ob_pass_rainbowring01";
	}

	// Token: 0x06003C3E RID: 15422 RVA: 0x0013D260 File Offset: 0x0013B460
	protected override string GetSEName()
	{
		return "obj_rainbowring";
	}

	// Token: 0x06003C3F RID: 15423 RVA: 0x0013D268 File Offset: 0x0013B468
	protected override int GetScore()
	{
		return Data.RainbowRing;
	}

	// Token: 0x06003C40 RID: 15424 RVA: 0x0013D270 File Offset: 0x0013B470
	public void SetObjRainbowRingParameter(ObjRainbowRingParameter param)
	{
		base.SetObjDashRingParameter(new ObjDashRingParameter
		{
			firstSpeed = param.firstSpeed,
			outOfcontrol = param.outOfcontrol
		});
	}
}
