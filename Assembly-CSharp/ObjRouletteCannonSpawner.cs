using System;
using UnityEngine;

// Token: 0x020008CC RID: 2252
public class ObjRouletteCannonSpawner : SpawnableBehavior
{
	// Token: 0x06003BEF RID: 15343 RVA: 0x0013C5E4 File Offset: 0x0013A7E4
	public override void SetParameters(SpawnableParameter srcParameter)
	{
		ObjRouletteCannonParameter objRouletteCannonParameter = srcParameter as ObjRouletteCannonParameter;
		if (objRouletteCannonParameter != null)
		{
			ObjRouletteCannon component = base.GetComponent<ObjRouletteCannon>();
			if (component)
			{
				component.SetObjRouletteCannonParameter(objRouletteCannonParameter);
			}
		}
	}

	// Token: 0x06003BF0 RID: 15344 RVA: 0x0013C618 File Offset: 0x0013A818
	public override SpawnableParameter GetParameter()
	{
		return this.m_parameter;
	}

	// Token: 0x04003455 RID: 13397
	[SerializeField]
	private ObjRouletteCannonParameter m_parameter;
}
