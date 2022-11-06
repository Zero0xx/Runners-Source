using System;
using Message;
using UnityEngine;

// Token: 0x02000906 RID: 2310
[AddComponentMenu("Scripts/Runners/Object/Common/ObjRedStarRing")]
public class ObjRedStarRing : SpawnableObject
{
	// Token: 0x06003CD9 RID: 15577 RVA: 0x0014032C File Offset: 0x0013E52C
	protected override string GetModelName()
	{
		return ObjRedStarRing.ModelName;
	}

	// Token: 0x06003CDA RID: 15578 RVA: 0x00140334 File Offset: 0x0013E534
	protected override ResourceCategory GetModelCategory()
	{
		return ResourceCategory.OBJECT_RESOURCE;
	}

	// Token: 0x06003CDB RID: 15579 RVA: 0x00140338 File Offset: 0x0013E538
	protected override void OnSpawned()
	{
		if (StageComboManager.Instance != null && (StageComboManager.Instance.IsActiveComboChaoAbility(ChaoAbility.COMBO_RECOVERY_ALL_OBJ) || StageComboManager.Instance.IsActiveComboChaoAbility(ChaoAbility.COMBO_DESTROY_AND_RECOVERY)))
		{
			this.OnDrawingRings(new MsgOnDrawingRings());
		}
	}

	// Token: 0x06003CDC RID: 15580 RVA: 0x00140384 File Offset: 0x0013E584
	private void Update()
	{
		this.UpdateModelPose();
	}

	// Token: 0x06003CDD RID: 15581 RVA: 0x0014038C File Offset: 0x0013E58C
	private void UpdateModelPose()
	{
		float y = 60f * Time.deltaTime;
		base.transform.rotation = Quaternion.Euler(0f, y, 0f) * base.transform.rotation;
	}

	// Token: 0x06003CDE RID: 15582 RVA: 0x001403D0 File Offset: 0x0013E5D0
	private void OnTriggerEnter(Collider other)
	{
		if (this.m_end)
		{
			return;
		}
		if (other)
		{
			GameObject gameObject = other.gameObject;
			if (gameObject)
			{
				string a = LayerMask.LayerToName(gameObject.layer);
				if (a == "Player" && gameObject.tag != "ChaoAttack")
				{
					ObjUtil.SendMessageAddRedRing();
					ObjUtil.SendMessageScoreCheck(new StageScoreData(8, 1));
					this.TakeRing();
				}
			}
		}
	}

	// Token: 0x06003CDF RID: 15583 RVA: 0x00140450 File Offset: 0x0013E650
	private void OnDrawingRings(MsgOnDrawingRings msg)
	{
		ObjUtil.StartMagnetControl(base.gameObject);
	}

	// Token: 0x06003CE0 RID: 15584 RVA: 0x00140460 File Offset: 0x0013E660
	private void OnDrawingRingsChaoAbility(MsgOnDrawingRings msg)
	{
		if (msg.m_chaoAbility == ChaoAbility.COMBO_RECOVERY_ALL_OBJ || msg.m_chaoAbility == ChaoAbility.COMBO_DESTROY_AND_RECOVERY)
		{
			this.OnDrawingRings(new MsgOnDrawingRings());
		}
	}

	// Token: 0x06003CE1 RID: 15585 RVA: 0x00140488 File Offset: 0x0013E688
	private void TakeRing()
	{
		this.m_end = true;
		ObjUtil.PlayEffectCollisionCenter(base.gameObject, ObjRedStarRing.EffectName, 1f, false);
		ObjUtil.PlaySE(ObjRedStarRing.SEName, "SE");
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04003511 RID: 13585
	private const float m_rotate_ratio = 60f;

	// Token: 0x04003512 RID: 13586
	public static string ModelName = "obj_cmn_redsterring";

	// Token: 0x04003513 RID: 13587
	public static string EffectName = "ef_ob_get_redring01";

	// Token: 0x04003514 RID: 13588
	public static string SEName = "obj_redring";

	// Token: 0x04003515 RID: 13589
	private bool m_end;
}
