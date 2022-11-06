using System;
using UnityEngine;

// Token: 0x02000936 RID: 2358
public class ObjEnemySwingSpawner : SpawnableBehavior
{
	// Token: 0x06003DC4 RID: 15812 RVA: 0x00142410 File Offset: 0x00140610
	public override void SetParameters(SpawnableParameter srcParameter)
	{
		ObjEnemySwingParameter objEnemySwingParameter = srcParameter as ObjEnemySwingParameter;
		if (objEnemySwingParameter != null)
		{
			ObjEnemySwing component = base.GetComponent<ObjEnemySwing>();
			if (component)
			{
				component.SetObjEnemySwingParameter(objEnemySwingParameter);
			}
		}
	}

	// Token: 0x06003DC5 RID: 15813 RVA: 0x00142444 File Offset: 0x00140644
	public override SpawnableParameter GetParameter()
	{
		return this.m_parameter;
	}

	// Token: 0x0400355C RID: 13660
	[SerializeField]
	private ObjEnemySwingParameter m_parameter;
}
