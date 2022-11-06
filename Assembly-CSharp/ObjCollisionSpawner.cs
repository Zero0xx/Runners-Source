using System;
using UnityEngine;

// Token: 0x020008BC RID: 2236
public class ObjCollisionSpawner : SpawnableBehavior
{
	// Token: 0x06003B9E RID: 15262 RVA: 0x0013AE40 File Offset: 0x00139040
	public override void SetParameters(SpawnableParameter srcParameter)
	{
		ObjCollisionParameter objCollisionParameter = srcParameter as ObjCollisionParameter;
		if (objCollisionParameter != null && !ObjUtil.IsUseTemporarySet())
		{
			BoxCollider component = base.GetComponent<BoxCollider>();
			if (component)
			{
				component.size = objCollisionParameter.GetSize();
			}
		}
	}

	// Token: 0x06003B9F RID: 15263 RVA: 0x0013AE84 File Offset: 0x00139084
	public override SpawnableParameter GetParameter()
	{
		return this.m_parameter;
	}

	// Token: 0x06003BA0 RID: 15264 RVA: 0x0013AE8C File Offset: 0x0013908C
	public override SpawnableParameter GetParameterForExport()
	{
		BoxCollider component = base.GetComponent<BoxCollider>();
		if (component)
		{
			this.m_parameter.SetSize(component.size);
		}
		return this.m_parameter;
	}

	// Token: 0x04003414 RID: 13332
	[SerializeField]
	private ObjCollisionParameter m_parameter;
}
