using System;
using GameScore;
using Message;
using UnityEngine;

// Token: 0x0200091A RID: 2330
public class ObjTrap : ObjTrapBase
{
	// Token: 0x06003D44 RID: 15684 RVA: 0x001414C8 File Offset: 0x0013F6C8
	protected override string GetModelName()
	{
		return "obj_cmn_trap";
	}

	// Token: 0x06003D45 RID: 15685 RVA: 0x001414D0 File Offset: 0x0013F6D0
	protected override ResourceCategory GetModelCategory()
	{
		return ResourceCategory.OBJECT_RESOURCE;
	}

	// Token: 0x06003D46 RID: 15686 RVA: 0x001414D4 File Offset: 0x0013F6D4
	protected override int GetScore()
	{
		return Data.Trap;
	}

	// Token: 0x06003D47 RID: 15687 RVA: 0x001414DC File Offset: 0x0013F6DC
	protected override void OnSpawned()
	{
		base.enabled = false;
		base.OnSpawned();
		if (StageComboManager.Instance != null && StageComboManager.Instance.IsChaoFlagStatus(StageComboManager.ChaoFlagStatus.DESTROY_TRAP))
		{
			base.SetBroken();
		}
	}

	// Token: 0x06003D48 RID: 15688 RVA: 0x0014151C File Offset: 0x0013F71C
	public void OnMsgObjectDeadChaoCombo(MsgObjectDead msg)
	{
		if (msg.m_chaoAbility == ChaoAbility.COMBO_DESTROY_TRAP)
		{
			base.SetBroken();
		}
	}

	// Token: 0x06003D49 RID: 15689 RVA: 0x00141534 File Offset: 0x0013F734
	protected override void PlayEffect()
	{
		if (StageEffectManager.Instance != null)
		{
			StageEffectManager.Instance.PlayEffect(EffectPlayType.TRAP, ObjUtil.GetCollisionCenterPosition(base.gameObject), Quaternion.identity);
		}
	}

	// Token: 0x06003D4A RID: 15690 RVA: 0x0014156C File Offset: 0x0013F76C
	public override void OnRevival()
	{
		BoxCollider component = base.GetComponent<BoxCollider>();
		if (component)
		{
			component.enabled = true;
		}
		base.enabled = true;
		this.m_end = false;
		this.OnSpawned();
	}

	// Token: 0x04003545 RID: 13637
	private const string ModelName = "obj_cmn_trap";
}
