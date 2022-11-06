using System;
using UnityEngine;

// Token: 0x020008FD RID: 2301
public class ObjPointMarkerSpawner : SpawnableBehavior
{
	// Token: 0x06003CB9 RID: 15545 RVA: 0x0013F130 File Offset: 0x0013D330
	public override void SetParameters(SpawnableParameter srcParameter)
	{
		ObjPointMarkerParameter objPointMarkerParameter = srcParameter as ObjPointMarkerParameter;
		if (objPointMarkerParameter != null)
		{
			ObjPointMarker component = base.GetComponent<ObjPointMarker>();
			if (component)
			{
				component.SetType((PointMarkerType)objPointMarkerParameter.m_type);
			}
		}
	}

	// Token: 0x06003CBA RID: 15546 RVA: 0x0013F168 File Offset: 0x0013D368
	public override SpawnableParameter GetParameter()
	{
		return this.m_parameter;
	}

	// Token: 0x040034DA RID: 13530
	[SerializeField]
	private ObjPointMarkerParameter m_parameter;
}
