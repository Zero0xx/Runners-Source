using System;
using UnityEngine;

// Token: 0x0200090A RID: 2314
[AddComponentMenu("Scripts/Runners/Object/Common/ObjSpringAir")]
public class ObjSpringAir : ObjSpring
{
	// Token: 0x06003D0A RID: 15626 RVA: 0x00140CA0 File Offset: 0x0013EEA0
	protected override string GetModelName()
	{
		return "obj_cmn_springAir";
	}

	// Token: 0x06003D0B RID: 15627 RVA: 0x00140CA8 File Offset: 0x0013EEA8
	protected override string GetMotionName()
	{
		return "obj_springAir_bounce";
	}

	// Token: 0x06003D0C RID: 15628 RVA: 0x00140CB0 File Offset: 0x0013EEB0
	public void SetObjSpringAirParameter(ObjSpringAirParameter param)
	{
		base.SetObjSpringParameter(new ObjSpringParameter
		{
			firstSpeed = param.firstSpeed,
			outOfcontrol = param.outOfcontrol
		});
	}

	// Token: 0x0400351E RID: 13598
	private const string ModelName = "obj_cmn_springAir";
}
