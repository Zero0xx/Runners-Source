using System;
using Message;
using UnityEngine;

// Token: 0x020008C1 RID: 2241
public class ObjTutorialStartCollision : ObjCollision
{
	// Token: 0x06003BB7 RID: 15287 RVA: 0x0013B4CC File Offset: 0x001396CC
	protected override void OnSpawned()
	{
		base.OnSpawned();
		BoxCollider component = base.gameObject.GetComponent<BoxCollider>();
		if (component != null && component.size.x < 2f)
		{
			component.size = new Vector3(2f, component.size.y, component.size.z);
		}
	}

	// Token: 0x06003BB8 RID: 15288 RVA: 0x0013B53C File Offset: 0x0013973C
	private void OnTriggerEnter(Collider other)
	{
		GameObjectUtil.SendMessageFindGameObject("StageTutorialManager", "OnMsgTutorialStart", new MsgTutorialStart(base.transform.position), SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06003BB9 RID: 15289 RVA: 0x0013B56C File Offset: 0x0013976C
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(base.transform.position, 0.5f);
	}

	// Token: 0x04003421 RID: 13345
	private const float COLLIDER_X_SIZE = 2f;
}
