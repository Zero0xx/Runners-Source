using System;
using UnityEngine;

// Token: 0x020008EE RID: 2286
public class ObjInfomationSignSpawner : SpawnableBehavior
{
	// Token: 0x06003C7A RID: 15482 RVA: 0x0013E320 File Offset: 0x0013C520
	public override void SetParameters(SpawnableParameter srcParameter)
	{
		ObjInfomationSignParameter objInfomationSignParameter = srcParameter as ObjInfomationSignParameter;
		if (objInfomationSignParameter != null)
		{
			GameObject infomationObject = objInfomationSignParameter.m_infomationObject;
			ObjInfomationSign component = base.GetComponent<ObjInfomationSign>();
			if (infomationObject != null && component != null)
			{
				if (component.InfomationObject != null)
				{
					return;
				}
				GameObject gameObject = UnityEngine.Object.Instantiate(infomationObject, base.transform.position, base.transform.rotation) as GameObject;
				if (gameObject != null)
				{
					gameObject.transform.parent = base.transform;
					gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
					component.InfomationObject = gameObject;
				}
			}
		}
	}

	// Token: 0x06003C7B RID: 15483 RVA: 0x0013E3D0 File Offset: 0x0013C5D0
	public override SpawnableParameter GetParameter()
	{
		return this.m_parameter;
	}

	// Token: 0x040034AA RID: 13482
	[SerializeField]
	private ObjInfomationSignParameter m_parameter;
}
