using System;
using Message;
using UnityEngine;

// Token: 0x020008E6 RID: 2278
[AddComponentMenu("Scripts/Runners/Object/Common/ObjTrampolineFloor")]
public class ObjTrampolineFloor : SpawnableObject
{
	// Token: 0x06003C54 RID: 15444 RVA: 0x0013D73C File Offset: 0x0013B93C
	protected override string GetModelName()
	{
		return "obj_cmn_trampolinefloor";
	}

	// Token: 0x06003C55 RID: 15445 RVA: 0x0013D744 File Offset: 0x0013B944
	protected override ResourceCategory GetModelCategory()
	{
		return ResourceCategory.OBJECT_RESOURCE;
	}

	// Token: 0x06003C56 RID: 15446 RVA: 0x0013D748 File Offset: 0x0013B948
	protected override void OnSpawned()
	{
		ObjUtil.StopAnimation(base.gameObject);
		ObjUtil.PlayEffectCollisionCenter(base.gameObject, "ef_com_item_warp_s01", 1f, false);
		base.enabled = false;
	}

	// Token: 0x06003C57 RID: 15447 RVA: 0x0013D780 File Offset: 0x0013B980
	public void SetParam(float first_speed, float out_of_control)
	{
		this.m_firstSpeed = first_speed;
		this.m_outOfcontrol = out_of_control;
	}

	// Token: 0x06003C58 RID: 15448 RVA: 0x0013D790 File Offset: 0x0013B990
	private void OnTriggerEnter(Collider other)
	{
		MsgOnSpringImpulse msgOnSpringImpulse = new MsgOnSpringImpulse(base.transform.position, base.transform.rotation, this.m_firstSpeed, this.m_outOfcontrol);
		other.gameObject.SendMessage("OnSpringImpulse", msgOnSpringImpulse, SendMessageOptions.DontRequireReceiver);
		if (msgOnSpringImpulse.m_succeed)
		{
			Animation componentInChildren = base.GetComponentInChildren<Animation>();
			if (componentInChildren)
			{
				componentInChildren.wrapMode = WrapMode.Once;
				componentInChildren.Play("obj_trampolinefloor_bounce");
			}
			ObjUtil.PlaySE("obj_trampolinefloor", "SE");
		}
	}

	// Token: 0x04003484 RID: 13444
	private const string ModelName = "obj_cmn_trampolinefloor";

	// Token: 0x04003485 RID: 13445
	private float m_firstSpeed;

	// Token: 0x04003486 RID: 13446
	private float m_outOfcontrol;
}
