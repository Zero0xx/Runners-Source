using System;
using UnityEngine;

// Token: 0x02000931 RID: 2353
public class ObjEnemyConstant : ObjEnemyBase
{
	// Token: 0x06003DB8 RID: 15800 RVA: 0x001421D4 File Offset: 0x001403D4
	public void SetObjEnemyConstantParameter(ObjEnemyConstantParameter param)
	{
		if (param != null)
		{
			base.SetupEnemy((uint)param.tblID, param.moveSpeed);
			MotorConstant component = base.GetComponent<MotorConstant>();
			if (component)
			{
				component.SetParam(param.moveSpeed, param.moveDistance, param.startMoveDistance, this.GetConstantAngle(), string.Empty, string.Empty);
			}
		}
	}

	// Token: 0x06003DB9 RID: 15801 RVA: 0x00142234 File Offset: 0x00140434
	protected virtual Vector3 GetConstantAngle()
	{
		return base.transform.forward;
	}
}
