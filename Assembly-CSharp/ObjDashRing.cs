using System;
using GameScore;
using Message;
using UnityEngine;

// Token: 0x020008DD RID: 2269
public class ObjDashRing : SpawnableObject
{
	// Token: 0x06003C2F RID: 15407 RVA: 0x0013D0D4 File Offset: 0x0013B2D4
	protected override string GetModelName()
	{
		return "obj_cmn_dashring";
	}

	// Token: 0x06003C30 RID: 15408 RVA: 0x0013D0DC File Offset: 0x0013B2DC
	protected virtual string GetEffectName()
	{
		return "ef_ob_pass_dashring01";
	}

	// Token: 0x06003C31 RID: 15409 RVA: 0x0013D0E4 File Offset: 0x0013B2E4
	protected virtual string GetSEName()
	{
		return "obj_dashring";
	}

	// Token: 0x06003C32 RID: 15410 RVA: 0x0013D0EC File Offset: 0x0013B2EC
	protected virtual int GetScore()
	{
		return Data.DashRing;
	}

	// Token: 0x06003C33 RID: 15411 RVA: 0x0013D0F4 File Offset: 0x0013B2F4
	protected override ResourceCategory GetModelCategory()
	{
		return ResourceCategory.OBJECT_RESOURCE;
	}

	// Token: 0x06003C34 RID: 15412 RVA: 0x0013D0F8 File Offset: 0x0013B2F8
	protected override void OnSpawned()
	{
	}

	// Token: 0x06003C35 RID: 15413 RVA: 0x0013D0FC File Offset: 0x0013B2FC
	public void SetObjDashRingParameter(ObjDashRingParameter param)
	{
		this.m_firstSpeed = param.firstSpeed;
		this.m_outOfcontrol = param.outOfcontrol;
	}

	// Token: 0x06003C36 RID: 15414 RVA: 0x0013D118 File Offset: 0x0013B318
	private void OnTriggerEnter(Collider other)
	{
		Quaternion rot = Quaternion.FromToRotation(base.transform.up, base.transform.right) * base.transform.rotation;
		MsgOnDashRingImpulse msgOnDashRingImpulse = new MsgOnDashRingImpulse(base.transform.position, rot, this.m_firstSpeed, this.m_outOfcontrol);
		other.gameObject.SendMessage("OnDashRingImpulse", msgOnDashRingImpulse, SendMessageOptions.DontRequireReceiver);
		if (msgOnDashRingImpulse.m_succeed)
		{
			ObjUtil.SendMessageAddScore(this.GetScore());
			ObjUtil.SendMessageScoreCheck(new StageScoreData(4, this.GetScore()));
			ObjUtil.PlayEffect(this.GetEffectName(), base.transform.position, base.transform.rotation, 1f, false);
			ObjUtil.PlaySE(this.GetSEName(), "SE");
		}
	}

	// Token: 0x04003472 RID: 13426
	private float m_firstSpeed = 8f;

	// Token: 0x04003473 RID: 13427
	private float m_outOfcontrol = 0.5f;
}
