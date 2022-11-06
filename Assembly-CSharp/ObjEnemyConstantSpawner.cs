using System;
using UnityEngine;

// Token: 0x02000933 RID: 2355
public class ObjEnemyConstantSpawner : SpawnableBehavior
{
	// Token: 0x06003DBC RID: 15804 RVA: 0x00142284 File Offset: 0x00140484
	public override void SetParameters(SpawnableParameter srcParameter)
	{
		ObjEnemyConstantParameter objEnemyConstantParameter = srcParameter as ObjEnemyConstantParameter;
		if (objEnemyConstantParameter != null)
		{
			ObjEnemyConstant component = base.GetComponent<ObjEnemyConstant>();
			if (component)
			{
				component.SetObjEnemyConstantParameter(objEnemyConstantParameter);
			}
		}
	}

	// Token: 0x06003DBD RID: 15805 RVA: 0x001422B8 File Offset: 0x001404B8
	public override SpawnableParameter GetParameter()
	{
		return this.m_parameter;
	}

	// Token: 0x06003DBE RID: 15806 RVA: 0x001422C0 File Offset: 0x001404C0
	private void OnDrawGizmos()
	{
		if (this.m_parameter.moveDistance > 0f && !base.transform.name.Contains("ObjEnmValkyne"))
		{
			Vector3 position = base.transform.position;
			Vector3 a = base.transform.forward;
			Gizmos.color = Color.green;
			if (base.transform.name.Contains("ObjEnmGanigani"))
			{
				a = base.transform.right;
			}
			Gizmos.DrawLine(position, position + a * this.m_parameter.moveDistance);
		}
	}

	// Token: 0x04003557 RID: 13655
	[SerializeField]
	private ObjEnemyConstantParameter m_parameter;
}
