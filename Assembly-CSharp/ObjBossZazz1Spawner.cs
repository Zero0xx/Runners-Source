using System;
using UnityEngine;

// Token: 0x02000851 RID: 2129
public class ObjBossZazz1Spawner : SpawnableBehavior
{
	// Token: 0x06003A21 RID: 14881 RVA: 0x00132730 File Offset: 0x00130930
	public override void SetParameters(SpawnableParameter srcParameter)
	{
		ObjBossZazz1Parameter objBossZazz1Parameter = srcParameter as ObjBossZazz1Parameter;
		if (objBossZazz1Parameter != null)
		{
			ObjBossZazz1 component = base.GetComponent<ObjBossZazz1>();
			if (component)
			{
				component.SetObjBossZazz1Parameter(objBossZazz1Parameter);
			}
		}
	}

	// Token: 0x06003A22 RID: 14882 RVA: 0x00132764 File Offset: 0x00130964
	public override SpawnableParameter GetParameter()
	{
		return this.m_parameter;
	}

	// Token: 0x040030F7 RID: 12535
	[SerializeField]
	private ObjBossZazz1Parameter m_parameter;
}
