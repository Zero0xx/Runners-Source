using System;
using UnityEngine;

// Token: 0x02000867 RID: 2151
public class ObjBossEggmanMap1Spawner : SpawnableBehavior
{
	// Token: 0x06003A65 RID: 14949 RVA: 0x0013457C File Offset: 0x0013277C
	public override void SetParameters(SpawnableParameter srcParameter)
	{
		ObjBossEggmanMap1Parameter objBossEggmanMap1Parameter = srcParameter as ObjBossEggmanMap1Parameter;
		if (objBossEggmanMap1Parameter != null)
		{
			ObjBossEggmanMap1 component = base.GetComponent<ObjBossEggmanMap1>();
			if (component)
			{
				component.SetObjBossEggmanMap1Parameter(objBossEggmanMap1Parameter);
			}
		}
	}

	// Token: 0x06003A66 RID: 14950 RVA: 0x001345B0 File Offset: 0x001327B0
	public override SpawnableParameter GetParameter()
	{
		return this.m_parameter;
	}

	// Token: 0x040031D9 RID: 12761
	[SerializeField]
	private ObjBossEggmanMap1Parameter m_parameter;
}
