using System;
using UnityEngine;

// Token: 0x020008BF RID: 2239
public class ObjTrampolineFloorCollisionSpawner : SpawnableBehavior
{
	// Token: 0x06003BAF RID: 15279 RVA: 0x0013B36C File Offset: 0x0013956C
	public override void SetParameters(SpawnableParameter srcParameter)
	{
		ObjTrampolineFloorCollisionParameter objTrampolineFloorCollisionParameter = srcParameter as ObjTrampolineFloorCollisionParameter;
		if (objTrampolineFloorCollisionParameter != null)
		{
			if (!ObjUtil.IsUseTemporarySet())
			{
				BoxCollider component = base.GetComponent<BoxCollider>();
				if (component)
				{
					component.size = objTrampolineFloorCollisionParameter.GetSize();
				}
			}
			ObjTrampolineFloorCollision component2 = base.GetComponent<ObjTrampolineFloorCollision>();
			if (component2)
			{
				component2.SetObjCollisionParameter(objTrampolineFloorCollisionParameter);
			}
		}
	}

	// Token: 0x06003BB0 RID: 15280 RVA: 0x0013B3C8 File Offset: 0x001395C8
	public override SpawnableParameter GetParameter()
	{
		return this.m_parameter;
	}

	// Token: 0x06003BB1 RID: 15281 RVA: 0x0013B3D0 File Offset: 0x001395D0
	public override SpawnableParameter GetParameterForExport()
	{
		BoxCollider component = base.GetComponent<BoxCollider>();
		if (component)
		{
			this.m_parameter.SetSize(component.size);
		}
		return this.m_parameter;
	}

	// Token: 0x0400341F RID: 13343
	[SerializeField]
	private ObjTrampolineFloorCollisionParameter m_parameter;
}
