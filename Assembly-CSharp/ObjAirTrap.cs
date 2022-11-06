using System;
using GameScore;
using Message;
using UnityEngine;

// Token: 0x0200090F RID: 2319
[AddComponentMenu("Scripts/Runners/Object/Common/ObjAirTrap")]
public class ObjAirTrap : ObjTrapBase
{
	// Token: 0x06003D16 RID: 15638 RVA: 0x00140DBC File Offset: 0x0013EFBC
	protected override string GetModelName()
	{
		return "obj_cmn_airtrap";
	}

	// Token: 0x06003D17 RID: 15639 RVA: 0x00140DC4 File Offset: 0x0013EFC4
	protected override ResourceCategory GetModelCategory()
	{
		return ResourceCategory.OBJECT_RESOURCE;
	}

	// Token: 0x06003D18 RID: 15640 RVA: 0x00140DC8 File Offset: 0x0013EFC8
	protected override int GetScore()
	{
		return Data.AirTrap;
	}

	// Token: 0x06003D19 RID: 15641 RVA: 0x00140DD0 File Offset: 0x0013EFD0
	public override bool IsValid()
	{
		return !(StageModeManager.Instance != null) || !StageModeManager.Instance.IsQuickMode();
	}

	// Token: 0x06003D1A RID: 15642 RVA: 0x00140DF4 File Offset: 0x0013EFF4
	protected override void OnSpawned()
	{
		base.OnSpawned();
		if (StageComboManager.Instance != null && StageComboManager.Instance.IsChaoFlagStatus(StageComboManager.ChaoFlagStatus.DESTROY_AIRTRAP))
		{
			base.SetBroken();
		}
		base.enabled = false;
	}

	// Token: 0x06003D1B RID: 15643 RVA: 0x00140E34 File Offset: 0x0013F034
	public void OnMsgObjectDeadChaoCombo(MsgObjectDead msg)
	{
		if (msg.m_chaoAbility == ChaoAbility.COMBO_DESTROY_AIR_TRAP)
		{
			base.SetBroken();
		}
	}

	// Token: 0x06003D1C RID: 15644 RVA: 0x00140E4C File Offset: 0x0013F04C
	public void SetObjAirTrapParameter(ObjAirTrapParameter param)
	{
		this.m_param = param;
		MotorSwing component = base.GetComponent<MotorSwing>();
		if (component)
		{
			component.SetParam(this.m_param.moveSpeed, this.m_param.moveDistanceX, this.m_param.moveDistanceY, base.transform.right);
		}
	}

	// Token: 0x06003D1D RID: 15645 RVA: 0x00140EA4 File Offset: 0x0013F0A4
	protected override void PlayEffect()
	{
		if (StageEffectManager.Instance != null)
		{
			StageEffectManager.Instance.PlayEffect(EffectPlayType.AIR_TRAP, ObjUtil.GetCollisionCenterPosition(base.gameObject), Quaternion.identity);
		}
	}

	// Token: 0x06003D1E RID: 15646 RVA: 0x00140EE0 File Offset: 0x0013F0E0
	public override void OnRevival()
	{
		SphereCollider component = base.GetComponent<SphereCollider>();
		if (component)
		{
			component.enabled = true;
		}
		base.enabled = true;
		this.m_end = false;
		this.OnSpawned();
	}

	// Token: 0x04003525 RID: 13605
	private const string ModelName = "obj_cmn_airtrap";

	// Token: 0x04003526 RID: 13606
	private ObjAirTrapParameter m_param;
}
