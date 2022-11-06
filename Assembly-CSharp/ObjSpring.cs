using System;
using Message;
using UnityEngine;

// Token: 0x02000909 RID: 2313
[AddComponentMenu("Scripts/Runners/Object/Common/ObjSpring")]
public class ObjSpring : SpawnableObject
{
	// Token: 0x06003D03 RID: 15619 RVA: 0x00140BCC File Offset: 0x0013EDCC
	protected override string GetModelName()
	{
		return "obj_cmn_spring";
	}

	// Token: 0x06003D04 RID: 15620 RVA: 0x00140BD4 File Offset: 0x0013EDD4
	protected override ResourceCategory GetModelCategory()
	{
		return ResourceCategory.OBJECT_RESOURCE;
	}

	// Token: 0x06003D05 RID: 15621 RVA: 0x00140BD8 File Offset: 0x0013EDD8
	protected virtual string GetMotionName()
	{
		return "obj_spring_bounce";
	}

	// Token: 0x06003D06 RID: 15622 RVA: 0x00140BE0 File Offset: 0x0013EDE0
	protected override void OnSpawned()
	{
		ObjUtil.StopAnimation(base.gameObject);
		base.enabled = false;
	}

	// Token: 0x06003D07 RID: 15623 RVA: 0x00140BF4 File Offset: 0x0013EDF4
	public void SetObjSpringParameter(ObjSpringParameter param)
	{
		this.m_firstSpeed = param.firstSpeed;
		this.m_outOfcontrol = param.outOfcontrol;
	}

	// Token: 0x06003D08 RID: 15624 RVA: 0x00140C10 File Offset: 0x0013EE10
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
				componentInChildren.Play(this.GetMotionName());
			}
			ObjUtil.PlaySE("obj_spring", "SE");
		}
	}

	// Token: 0x0400351B RID: 13595
	private const string ModelName = "obj_cmn_spring";

	// Token: 0x0400351C RID: 13596
	private float m_firstSpeed = 5f;

	// Token: 0x0400351D RID: 13597
	private float m_outOfcontrol = 0.5f;
}
