using System;
using UnityEngine;

// Token: 0x020008CA RID: 2250
[AddComponentMenu("Scripts/Runners/Object/Common/ObjRouletteCannon")]
public class ObjRouletteCannon : ObjCannon
{
	// Token: 0x06003BE7 RID: 15335 RVA: 0x0013C4F8 File Offset: 0x0013A6F8
	protected override string GetModelName()
	{
		return "obj_cmn_roulettecannon";
	}

	// Token: 0x06003BE8 RID: 15336 RVA: 0x0013C500 File Offset: 0x0013A700
	protected override string GetShotEffectName()
	{
		return "ef_ob_roulette_canon_st01";
	}

	// Token: 0x06003BE9 RID: 15337 RVA: 0x0013C508 File Offset: 0x0013A708
	protected override string GetShotAnimName()
	{
		return "roulettecannon_shot";
	}

	// Token: 0x06003BEA RID: 15338 RVA: 0x0013C510 File Offset: 0x0013A710
	protected override bool IsRoulette()
	{
		return true;
	}

	// Token: 0x06003BEB RID: 15339 RVA: 0x0013C514 File Offset: 0x0013A714
	public void SetObjRouletteCannonParameter(ObjRouletteCannonParameter param)
	{
		this.m_angle = param.angle;
		base.SetObjCannonParameter(new ObjCannonParameter
		{
			firstSpeed = param.firstSpeed,
			outOfcontrol = param.outOfcontrol,
			moveSpeed = param.moveSpeed,
			moveArea = 0f
		});
	}

	// Token: 0x06003BEC RID: 15340 RVA: 0x0013C56C File Offset: 0x0013A76C
	protected override Quaternion GetStartRot()
	{
		return Quaternion.Euler(0f, 0f, -this.m_angle) * base.transform.rotation;
	}

	// Token: 0x0400344F RID: 13391
	private const string ModelName = "obj_cmn_roulettecannon";

	// Token: 0x04003450 RID: 13392
	private float m_angle;
}
