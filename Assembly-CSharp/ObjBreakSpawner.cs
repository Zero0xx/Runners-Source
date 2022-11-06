using System;
using UnityEngine;

// Token: 0x020008C5 RID: 2245
public class ObjBreakSpawner : SpawnableBehavior
{
	// Token: 0x06003BCE RID: 15310 RVA: 0x0013BE2C File Offset: 0x0013A02C
	public override void SetParameters(SpawnableParameter srcParameter)
	{
		ObjBreakParameter objBreakParameter = srcParameter as ObjBreakParameter;
		if (objBreakParameter != null)
		{
			GameObject modelObject = objBreakParameter.m_modelObject;
			ObjBreak component = base.GetComponent<ObjBreak>();
			if (modelObject != null && component != null)
			{
				if (component.ModelObject != null)
				{
					return;
				}
				GameObject gameObject = UnityEngine.Object.Instantiate(modelObject, base.transform.position, base.transform.rotation) as GameObject;
				if (gameObject != null)
				{
					gameObject.transform.parent = base.transform;
					component.ModelObject = gameObject;
				}
				component.SetObjName(modelObject.name);
			}
		}
	}

	// Token: 0x06003BCF RID: 15311 RVA: 0x0013BED0 File Offset: 0x0013A0D0
	public override SpawnableParameter GetParameter()
	{
		return this.m_parameter;
	}

	// Token: 0x04003435 RID: 13365
	[SerializeField]
	private ObjBreakParameter m_parameter;
}
