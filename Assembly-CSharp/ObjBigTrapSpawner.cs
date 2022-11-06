using System;
using UnityEngine;

// Token: 0x02000914 RID: 2324
public class ObjBigTrapSpawner : SpawnableBehavior
{
	// Token: 0x06003D2F RID: 15663 RVA: 0x00141174 File Offset: 0x0013F374
	public override void SetParameters(SpawnableParameter srcParameter)
	{
		ObjBigTrapParameter objBigTrapParameter = srcParameter as ObjBigTrapParameter;
		if (objBigTrapParameter != null)
		{
			ObjBigTrap component = base.GetComponent<ObjBigTrap>();
			if (component)
			{
				component.SetObjBigTrapParameter(objBigTrapParameter);
			}
		}
	}

	// Token: 0x06003D30 RID: 15664 RVA: 0x001411A8 File Offset: 0x0013F3A8
	public override SpawnableParameter GetParameter()
	{
		return this.m_parameter;
	}

	// Token: 0x04003534 RID: 13620
	[SerializeField]
	private ObjBigTrapParameter m_parameter;
}
