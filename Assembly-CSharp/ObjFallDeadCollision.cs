using System;
using UnityEngine;

// Token: 0x020008B9 RID: 2233
[AddComponentMenu("Scripts/Runners/Object/Collision/ObjFallDeadCollision")]
public class ObjFallDeadCollision : ObjCollision
{
	// Token: 0x06003B95 RID: 15253 RVA: 0x0013AD20 File Offset: 0x00138F20
	protected override void OnSpawned()
	{
		base.OnSpawned();
	}

	// Token: 0x06003B96 RID: 15254 RVA: 0x0013AD28 File Offset: 0x00138F28
	private void OnTriggerExit(Collider other)
	{
		Vector3 position = other.transform.position;
		Vector3 position2 = base.transform.position;
		Vector3 up = Vector3.up;
		Vector3 lhs = position2 - position;
		if (Vector3.Dot(lhs, up) > 0f)
		{
			other.gameObject.SendMessage("OnFallingDead");
		}
	}

	// Token: 0x06003B97 RID: 15255 RVA: 0x0013AD7C File Offset: 0x00138F7C
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(base.transform.position, 0.5f);
	}
}
