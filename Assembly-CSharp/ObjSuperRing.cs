using System;
using Message;
using UnityEngine;

// Token: 0x02000908 RID: 2312
[AddComponentMenu("Scripts/Runners/Object/Common/ObjSuperRing")]
public class ObjSuperRing : SpawnableObject
{
	// Token: 0x06003CF3 RID: 15603 RVA: 0x00140824 File Offset: 0x0013EA24
	public static string GetSuperRingModelName()
	{
		if (EventManager.Instance != null && EventManager.Instance.IsRaidBossStage())
		{
			return EventBossObjectTable.GetItemData(EventBossObjectTableItem.Ring10Model);
		}
		return ObjSuperRing.ModelName;
	}

	// Token: 0x06003CF4 RID: 15604 RVA: 0x00140854 File Offset: 0x0013EA54
	protected override string GetModelName()
	{
		return ObjSuperRing.GetSuperRingModelName();
	}

	// Token: 0x06003CF5 RID: 15605 RVA: 0x0014085C File Offset: 0x0013EA5C
	public static ResourceCategory GetSuperRingModelCategory()
	{
		if (EventManager.Instance != null && EventManager.Instance.IsRaidBossStage())
		{
			return ResourceCategory.EVENT_RESOURCE;
		}
		return ResourceCategory.OBJECT_RESOURCE;
	}

	// Token: 0x06003CF6 RID: 15606 RVA: 0x00140884 File Offset: 0x0013EA84
	protected override ResourceCategory GetModelCategory()
	{
		return ObjSuperRing.GetSuperRingModelCategory();
	}

	// Token: 0x06003CF7 RID: 15607 RVA: 0x0014088C File Offset: 0x0013EA8C
	public static string GetSuperRingEffect()
	{
		if (EventManager.Instance != null && EventManager.Instance.IsRaidBossStage())
		{
			return EventBossObjectTable.GetItemData(EventBossObjectTableItem.Ring10Effect);
		}
		return "ef_ob_get_superring01";
	}

	// Token: 0x06003CF8 RID: 15608 RVA: 0x001408BC File Offset: 0x0013EABC
	public static void SetPlaySuperRingSE()
	{
		if (EventManager.Instance != null && EventManager.Instance.IsRaidBossStage())
		{
			ObjUtil.PlayEventSE(EventBossObjectTable.GetItemData(EventBossObjectTableItem.Ring10SE), EventManager.EventType.RAID_BOSS);
		}
		ObjUtil.PlaySE(ObjSuperRing.SEName, "SE");
	}

	// Token: 0x06003CF9 RID: 15609 RVA: 0x00140908 File Offset: 0x0013EB08
	protected override void OnSpawned()
	{
		if (StageComboManager.Instance != null && (StageComboManager.Instance.IsActiveComboChaoAbility(ChaoAbility.COMBO_RECOVERY_ALL_OBJ) || StageComboManager.Instance.IsActiveComboChaoAbility(ChaoAbility.COMBO_DESTROY_AND_RECOVERY)))
		{
			this.OnDrawingRings(new MsgOnDrawingRings());
		}
		base.enabled = true;
	}

	// Token: 0x06003CFA RID: 15610 RVA: 0x0014095C File Offset: 0x0013EB5C
	public override void OnRevival()
	{
		MagnetControl component = base.GetComponent<MagnetControl>();
		if (component != null)
		{
			component.Reset();
		}
		SphereCollider component2 = base.GetComponent<SphereCollider>();
		if (component2)
		{
			component2.enabled = true;
		}
		BoxCollider component3 = base.GetComponent<BoxCollider>();
		if (component3)
		{
			component3.enabled = true;
		}
		this.m_end = false;
		this.OnSpawned();
	}

	// Token: 0x06003CFB RID: 15611 RVA: 0x001409C4 File Offset: 0x0013EBC4
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
				if (a == "Player")
				{
					if (gameObject.tag != "ChaoAttack")
					{
						gameObject.SendMessage("OnAddRings", ObjSuperRing.GetRingCount(), SendMessageOptions.DontRequireReceiver);
						this.TakeRing();
					}
				}
				else if (a == "HitRing" && gameObject.tag == "Chao")
				{
					GameObjectUtil.SendMessageToTagObjects("Player", "OnAddRings", ObjSuperRing.GetRingCount(), SendMessageOptions.DontRequireReceiver);
					this.TakeRing();
				}
			}
		}
	}

	// Token: 0x06003CFC RID: 15612 RVA: 0x00140A94 File Offset: 0x0013EC94
	private void OnDrawingRings(MsgOnDrawingRings msg)
	{
		ObjUtil.StartMagnetControl(base.gameObject);
	}

	// Token: 0x06003CFD RID: 15613 RVA: 0x00140AA4 File Offset: 0x0013ECA4
	private void OnDrawingRingsToChao(MsgOnDrawingRings msg)
	{
		if (msg != null)
		{
			ObjUtil.StartMagnetControl(base.gameObject, msg.m_target);
		}
	}

	// Token: 0x06003CFE RID: 15614 RVA: 0x00140AC0 File Offset: 0x0013ECC0
	private void OnDrawingRingsChaoAbility(MsgOnDrawingRings msg)
	{
		if (msg.m_chaoAbility == ChaoAbility.COMBO_RECOVERY_ALL_OBJ || msg.m_chaoAbility == ChaoAbility.COMBO_DESTROY_AND_RECOVERY)
		{
			this.OnDrawingRings(new MsgOnDrawingRings());
		}
	}

	// Token: 0x06003CFF RID: 15615 RVA: 0x00140AE8 File Offset: 0x0013ECE8
	private void TakeRing()
	{
		this.m_end = true;
		if (StageEffectManager.Instance != null)
		{
			StageEffectManager.Instance.PlayEffect(EffectPlayType.SUPER_RING, ObjUtil.GetCollisionCenterPosition(base.gameObject), Quaternion.identity);
		}
		ObjSuperRing.SetPlaySuperRingSE();
		if (base.Share)
		{
			base.gameObject.SetActive(false);
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06003D00 RID: 15616 RVA: 0x00140B54 File Offset: 0x0013ED54
	public static int GetRingCount()
	{
		int num = 10;
		if (StageAbilityManager.Instance != null)
		{
			num += (int)StageAbilityManager.Instance.GetChaoAbilityValue(ChaoAbility.SUPER_RING_UP);
		}
		return num;
	}

	// Token: 0x06003D01 RID: 15617 RVA: 0x00140B88 File Offset: 0x0013ED88
	public static void AddSuperRing(GameObject obj)
	{
		if (obj)
		{
			obj.SendMessage("OnAddRings", ObjSuperRing.GetRingCount(), SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x04003518 RID: 13592
	private bool m_end;

	// Token: 0x04003519 RID: 13593
	public static string ModelName = "obj_cmn_superring10";

	// Token: 0x0400351A RID: 13594
	public static string SEName = "obj_superring";
}
