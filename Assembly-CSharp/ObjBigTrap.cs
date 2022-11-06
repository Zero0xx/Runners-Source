using System;
using GameScore;
using UnityEngine;

// Token: 0x02000912 RID: 2322
[AddComponentMenu("Scripts/Runners/Object/Common/ObjBigTrap")]
public class ObjBigTrap : ObjTrapBase
{
	// Token: 0x06003D24 RID: 15652 RVA: 0x00140FA4 File Offset: 0x0013F1A4
	protected override string GetModelName()
	{
		return "obj_cmn_boomboo_L";
	}

	// Token: 0x06003D25 RID: 15653 RVA: 0x00140FAC File Offset: 0x0013F1AC
	protected override ResourceCategory GetModelCategory()
	{
		return ResourceCategory.OBJECT_RESOURCE;
	}

	// Token: 0x06003D26 RID: 15654 RVA: 0x00140FB0 File Offset: 0x0013F1B0
	protected override int GetScore()
	{
		return Data.BigTrap;
	}

	// Token: 0x06003D27 RID: 15655 RVA: 0x00140FB8 File Offset: 0x0013F1B8
	private void StopSE()
	{
		if (this.m_move_SEID != 0U)
		{
			ObjUtil.StopSE((SoundManager.PlayId)this.m_move_SEID);
			this.m_move_SEID = 0U;
		}
	}

	// Token: 0x06003D28 RID: 15656 RVA: 0x00140FD8 File Offset: 0x0013F1D8
	private void Update()
	{
		if (this.m_start)
		{
			return;
		}
		if (this.m_playerInfo == null)
		{
			this.m_playerInfo = ObjUtil.GetPlayerInformation();
		}
		if (this.m_playerInfo != null && this.m_param != null)
		{
			float playerDistance = this.GetPlayerDistance();
			if (playerDistance < this.m_param.startMoveDistance)
			{
				MotorAnimalFly component = base.GetComponent<MotorAnimalFly>();
				if (component)
				{
					component.SetupParam(this.m_param.moveSpeedY, this.m_param.moveDistanceY, this.m_param.moveSpeedX, base.transform.right, 0f, false);
					if (this.m_move_SEID == 0U)
					{
						this.m_move_SEID = (uint)ObjUtil.LightPlaySE("obj_ghost_l", "SE");
					}
					this.m_start = true;
				}
			}
		}
	}

	// Token: 0x06003D29 RID: 15657 RVA: 0x001410B4 File Offset: 0x0013F2B4
	private float GetPlayerDistance()
	{
		if (this.m_playerInfo)
		{
			Vector3 position = base.transform.position;
			return Mathf.Abs(Vector3.Distance(position, this.m_playerInfo.Position));
		}
		return 0f;
	}

	// Token: 0x06003D2A RID: 15658 RVA: 0x001410FC File Offset: 0x0013F2FC
	public void SetObjBigTrapParameter(ObjBigTrapParameter param)
	{
		this.m_param = param;
	}

	// Token: 0x06003D2B RID: 15659 RVA: 0x00141108 File Offset: 0x0013F308
	protected override void PlayEffect()
	{
		ObjUtil.PlayEffectCollisionCenter(base.gameObject, "ef_com_explosion_l01", 1f, false);
	}

	// Token: 0x06003D2C RID: 15660 RVA: 0x00141120 File Offset: 0x0013F320
	protected override void TrapDamageHit()
	{
		this.StopSE();
		base.SetBroken();
	}

	// Token: 0x0400352B RID: 13611
	private const string ModelName = "obj_cmn_boomboo_L";

	// Token: 0x0400352C RID: 13612
	private ObjBigTrapParameter m_param;

	// Token: 0x0400352D RID: 13613
	private PlayerInformation m_playerInfo;

	// Token: 0x0400352E RID: 13614
	private bool m_start;

	// Token: 0x0400352F RID: 13615
	private uint m_move_SEID;
}
