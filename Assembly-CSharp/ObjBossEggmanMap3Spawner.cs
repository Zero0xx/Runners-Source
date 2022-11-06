using System;
using UnityEngine;

// Token: 0x0200086D RID: 2157
public class ObjBossEggmanMap3Spawner : SpawnableBehavior
{
	// Token: 0x06003A71 RID: 14961 RVA: 0x001354F8 File Offset: 0x001336F8
	public override void SetParameters(SpawnableParameter srcParameter)
	{
		ObjBossEggmanMap3Parameter objBossEggmanMap3Parameter = srcParameter as ObjBossEggmanMap3Parameter;
		if (objBossEggmanMap3Parameter != null)
		{
			ObjBossEggmanMap3 component = base.GetComponent<ObjBossEggmanMap3>();
			if (component)
			{
				component.SetObjBossEggmanMap3Parameter(objBossEggmanMap3Parameter);
			}
		}
	}

	// Token: 0x06003A72 RID: 14962 RVA: 0x0013552C File Offset: 0x0013372C
	public override SpawnableParameter GetParameter()
	{
		return this.m_parameter;
	}

	// Token: 0x04003251 RID: 12881
	[SerializeField]
	private ObjBossEggmanMap3Parameter m_parameter;
}
