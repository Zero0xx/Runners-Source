using System;
using UnityEngine;

// Token: 0x020008F9 RID: 2297
public class ObjLoopTerrainSpawner : SpawnableBehavior
{
	// Token: 0x06003CA8 RID: 15528 RVA: 0x0013ED3C File Offset: 0x0013CF3C
	public override void SetParameters(SpawnableParameter srcParameter)
	{
		ObjLoopTerrainParameter objLoopTerrainParameter = srcParameter as ObjLoopTerrainParameter;
		if (objLoopTerrainParameter != null)
		{
			this.m_parameter = objLoopTerrainParameter;
			ObjLoopTerrain component = base.GetComponent<ObjLoopTerrain>();
			if (component != null)
			{
				if (objLoopTerrainParameter.m_pathName.Length > 0)
				{
					component.SetPathName(objLoopTerrainParameter.m_pathName);
				}
				component.SetZOffset(objLoopTerrainParameter.m_pathZOffset);
			}
			BoxCollider component2 = base.GetComponent<BoxCollider>();
			if (component2)
			{
				component2.size = objLoopTerrainParameter.Size;
				component2.center = objLoopTerrainParameter.Center;
			}
		}
	}

	// Token: 0x06003CA9 RID: 15529 RVA: 0x0013EDC4 File Offset: 0x0013CFC4
	public override SpawnableParameter GetParameter()
	{
		return this.m_parameter;
	}

	// Token: 0x06003CAA RID: 15530 RVA: 0x0013EDCC File Offset: 0x0013CFCC
	public override SpawnableParameter GetParameterForExport()
	{
		BoxCollider component = base.GetComponent<BoxCollider>();
		if (component)
		{
			this.m_parameter.Size = component.size;
			this.m_parameter.Center = component.center;
		}
		return this.m_parameter;
	}

	// Token: 0x040034CE RID: 13518
	[SerializeField]
	private ObjLoopTerrainParameter m_parameter;
}
