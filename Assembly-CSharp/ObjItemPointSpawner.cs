using System;
using UnityEngine;

// Token: 0x020008F2 RID: 2290
public class ObjItemPointSpawner : SpawnableBehavior
{
	// Token: 0x06003C8E RID: 15502 RVA: 0x0013E8DC File Offset: 0x0013CADC
	public override void SetParameters(SpawnableParameter srcParameter)
	{
		ObjItemPointParameter objItemPointParameter = srcParameter as ObjItemPointParameter;
		if (objItemPointParameter != null)
		{
			ObjItemPoint component = base.GetComponent<ObjItemPoint>();
			if (component != null)
			{
				component.SetID(objItemPointParameter.m_tblID);
			}
		}
	}

	// Token: 0x06003C8F RID: 15503 RVA: 0x0013E918 File Offset: 0x0013CB18
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(base.transform.position, 0.5f);
	}

	// Token: 0x06003C90 RID: 15504 RVA: 0x0013E944 File Offset: 0x0013CB44
	public override SpawnableParameter GetParameter()
	{
		return this.m_parameter;
	}

	// Token: 0x040034B3 RID: 13491
	[SerializeField]
	private ObjItemPointParameter m_parameter;
}
