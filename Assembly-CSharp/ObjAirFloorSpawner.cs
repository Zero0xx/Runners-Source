using System;
using UnityEngine;

// Token: 0x020008E5 RID: 2277
public class ObjAirFloorSpawner : SpawnableBehavior
{
	// Token: 0x06003C51 RID: 15441 RVA: 0x0013D678 File Offset: 0x0013B878
	public override void SetParameters(SpawnableParameter srcParameter)
	{
		ObjFloorParameter objFloorParameter = srcParameter as ObjFloorParameter;
		if (objFloorParameter != null)
		{
			GameObject modelObject = objFloorParameter.m_modelObject;
			ObjAirFloor component = base.GetComponent<ObjAirFloor>();
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
					gameObject.layer = LayerMask.NameToLayer("Plane");
				}
				component.Setup(modelObject.name);
			}
		}
	}

	// Token: 0x06003C52 RID: 15442 RVA: 0x0013D72C File Offset: 0x0013B92C
	public override SpawnableParameter GetParameter()
	{
		return this.m_parameter;
	}

	// Token: 0x04003483 RID: 13443
	[SerializeField]
	private ObjFloorParameter m_parameter;
}
