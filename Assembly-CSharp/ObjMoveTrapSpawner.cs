using System;
using UnityEngine;

// Token: 0x02000919 RID: 2329
public class ObjMoveTrapSpawner : SpawnableBehavior
{
	// Token: 0x06003D41 RID: 15681 RVA: 0x00141484 File Offset: 0x0013F684
	public override void SetParameters(SpawnableParameter srcParameter)
	{
		ObjMoveTrapParameter objMoveTrapParameter = srcParameter as ObjMoveTrapParameter;
		if (objMoveTrapParameter != null)
		{
			ObjMoveTrap component = base.GetComponent<ObjMoveTrap>();
			if (component)
			{
				component.SetObjMoveTrapParameter(objMoveTrapParameter);
			}
		}
	}

	// Token: 0x06003D42 RID: 15682 RVA: 0x001414B8 File Offset: 0x0013F6B8
	public override SpawnableParameter GetParameter()
	{
		return this.m_parameter;
	}

	// Token: 0x04003544 RID: 13636
	[SerializeField]
	private ObjMoveTrapParameter m_parameter;
}
