using System;
using Message;
using UnityEngine;

// Token: 0x020008C0 RID: 2240
public class ObjTutorialEndCollision : ObjCollision
{
	// Token: 0x06003BB3 RID: 15283 RVA: 0x0013B410 File Offset: 0x00139610
	protected override void OnSpawned()
	{
		base.OnSpawned();
		BoxCollider component = base.gameObject.GetComponent<BoxCollider>();
		if (component != null && component.size.x < 2f)
		{
			component.size = new Vector3(2f, component.size.y, component.size.z);
		}
	}

	// Token: 0x06003BB4 RID: 15284 RVA: 0x0013B480 File Offset: 0x00139680
	private void OnTriggerEnter(Collider other)
	{
		GameObjectUtil.SendMessageFindGameObject("StageTutorialManager", "OnMsgTutorialEnd", new MsgTutorialEnd(), SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06003BB5 RID: 15285 RVA: 0x0013B498 File Offset: 0x00139698
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(base.transform.position, 0.5f);
	}

	// Token: 0x04003420 RID: 13344
	private const float COLLIDER_X_SIZE = 2f;
}
