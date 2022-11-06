using System;
using UnityEngine;

// Token: 0x0200090E RID: 2318
public class ObjSpringSpawner : SpawnableBehavior
{
	// Token: 0x06003D13 RID: 15635 RVA: 0x00140D78 File Offset: 0x0013EF78
	public override void SetParameters(SpawnableParameter srcParameter)
	{
		ObjSpringParameter objSpringParameter = srcParameter as ObjSpringParameter;
		if (objSpringParameter != null)
		{
			ObjSpring component = base.GetComponent<ObjSpring>();
			if (component)
			{
				component.SetObjSpringParameter(objSpringParameter);
			}
		}
	}

	// Token: 0x06003D14 RID: 15636 RVA: 0x00140DAC File Offset: 0x0013EFAC
	public override SpawnableParameter GetParameter()
	{
		return this.m_parameter;
	}

	// Token: 0x04003524 RID: 13604
	[SerializeField]
	private ObjSpringParameter m_parameter;
}
