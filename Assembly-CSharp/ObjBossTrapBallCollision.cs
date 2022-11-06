using System;
using Message;
using UnityEngine;

// Token: 0x02000880 RID: 2176
public class ObjBossTrapBallCollision : MonoBehaviour
{
	// Token: 0x06003ACE RID: 15054 RVA: 0x001374E0 File Offset: 0x001356E0
	private void OnTriggerEnter(Collider other)
	{
		if (other)
		{
			GameObject gameObject = other.gameObject;
			if (gameObject)
			{
				MsgHitDamage value = new MsgHitDamage(base.gameObject, AttackPower.PlayerColorPower);
				gameObject.SendMessage("OnDamageHit", value, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Token: 0x06003ACF RID: 15055 RVA: 0x00137524 File Offset: 0x00135724
	private void OnDrawGizmos()
	{
		SphereCollider component = base.GetComponent<SphereCollider>();
		if (component)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(base.transform.position, component.radius);
		}
	}
}
