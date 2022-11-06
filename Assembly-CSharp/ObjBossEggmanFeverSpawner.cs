using System;
using UnityEngine;

// Token: 0x02000864 RID: 2148
public class ObjBossEggmanFeverSpawner : SpawnableBehavior
{
	// Token: 0x06003A5F RID: 14943 RVA: 0x00133DF8 File Offset: 0x00131FF8
	public override void SetParameters(SpawnableParameter srcParameter)
	{
		ObjBossEggmanFeverParameter objBossEggmanFeverParameter = srcParameter as ObjBossEggmanFeverParameter;
		if (objBossEggmanFeverParameter != null)
		{
			ObjBossEggmanFever component = base.GetComponent<ObjBossEggmanFever>();
			if (component)
			{
				component.SetObjBossEggmanFeverParameter(objBossEggmanFeverParameter);
			}
		}
	}

	// Token: 0x06003A60 RID: 14944 RVA: 0x00133E2C File Offset: 0x0013202C
	public override SpawnableParameter GetParameter()
	{
		return this.m_parameter;
	}

	// Token: 0x0400319D RID: 12701
	[SerializeField]
	private ObjBossEggmanFeverParameter m_parameter;
}
