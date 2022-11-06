using System;
using Message;
using UnityEngine;

// Token: 0x02000161 RID: 353
public class ChaoPartsAttackEnemy : MonoBehaviour
{
	// Token: 0x06000A44 RID: 2628 RVA: 0x0003D774 File Offset: 0x0003B974
	private void Awake()
	{
		this.Setup();
	}

	// Token: 0x06000A45 RID: 2629 RVA: 0x0003D77C File Offset: 0x0003B97C
	private void Start()
	{
	}

	// Token: 0x06000A46 RID: 2630 RVA: 0x0003D780 File Offset: 0x0003B980
	private void Update()
	{
	}

	// Token: 0x06000A47 RID: 2631 RVA: 0x0003D784 File Offset: 0x0003B984
	private void Setup()
	{
		base.gameObject.layer = LayerMask.NameToLayer("Player");
		base.gameObject.tag = "ChaoAttack";
		CapsuleCollider capsuleCollider = base.gameObject.AddComponent<CapsuleCollider>();
		capsuleCollider.radius = 1f;
		capsuleCollider.height = 3f;
		capsuleCollider.isTrigger = true;
	}

	// Token: 0x06000A48 RID: 2632 RVA: 0x0003D7E0 File Offset: 0x0003B9E0
	public static GameObject Create(GameObject parent)
	{
		GameObject gameObject = new GameObject("ChaoPartsAttackEnemy");
		gameObject.transform.parent = parent.transform;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.identity;
		gameObject.AddComponent<ChaoPartsAttackEnemy>();
		return gameObject;
	}

	// Token: 0x06000A49 RID: 2633 RVA: 0x0003D834 File Offset: 0x0003BA34
	public void OnTriggerEnter(Collider other)
	{
		AttackPower attack = AttackPower.PlayerInvincible;
		MsgHitDamage msgHitDamage = new MsgHitDamage(base.gameObject, attack);
		msgHitDamage.m_attackAttribute = 32U;
		GameObjectUtil.SendDelayedMessageToGameObject(other.gameObject, "OnDamageHit", msgHitDamage);
	}

	// Token: 0x06000A4A RID: 2634 RVA: 0x0003D86C File Offset: 0x0003BA6C
	private void OnDamageSucceed(MsgHitDamageSucceed msg)
	{
		base.gameObject.transform.parent.SendMessage("OnDamageSucceed", msg, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x040007EE RID: 2030
	private const float ColliRadius = 1f;

	// Token: 0x040007EF RID: 2031
	private const float ColliHeight = 3f;
}
