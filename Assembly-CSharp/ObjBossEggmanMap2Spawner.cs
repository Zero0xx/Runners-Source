using System;
using UnityEngine;

// Token: 0x0200086A RID: 2154
public class ObjBossEggmanMap2Spawner : SpawnableBehavior
{
	// Token: 0x06003A6B RID: 14955 RVA: 0x00134ED0 File Offset: 0x001330D0
	public override void SetParameters(SpawnableParameter srcParameter)
	{
		ObjBossEggmanMap2Parameter objBossEggmanMap2Parameter = srcParameter as ObjBossEggmanMap2Parameter;
		if (objBossEggmanMap2Parameter != null)
		{
			ObjBossEggmanMap2 component = base.GetComponent<ObjBossEggmanMap2>();
			if (component)
			{
				component.SetObjBossEggmanMap2Parameter(objBossEggmanMap2Parameter);
			}
		}
	}

	// Token: 0x06003A6C RID: 14956 RVA: 0x00134F04 File Offset: 0x00133104
	public override SpawnableParameter GetParameter()
	{
		return this.m_parameter;
	}

	// Token: 0x04003225 RID: 12837
	[SerializeField]
	private ObjBossEggmanMap2Parameter m_parameter;
}
