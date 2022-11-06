using System;
using UnityEngine;

// Token: 0x02000855 RID: 2133
public class ObjBossZazz3Spawner : SpawnableBehavior
{
	// Token: 0x06003A2E RID: 14894 RVA: 0x0013339C File Offset: 0x0013159C
	public override void SetParameters(SpawnableParameter srcParameter)
	{
		ObjBossZazz3Parameter objBossZazz3Parameter = srcParameter as ObjBossZazz3Parameter;
		if (objBossZazz3Parameter != null)
		{
			ObjBossZazz3 component = base.GetComponent<ObjBossZazz3>();
			if (component)
			{
				component.SetObjBossZazz3Parameter(objBossZazz3Parameter);
			}
		}
	}

	// Token: 0x06003A2F RID: 14895 RVA: 0x001333D0 File Offset: 0x001315D0
	public override SpawnableParameter GetParameter()
	{
		return this.m_parameter;
	}

	// Token: 0x0400315F RID: 12639
	[SerializeField]
	private ObjBossZazz3Parameter m_parameter;
}
