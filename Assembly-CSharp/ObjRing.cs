using System;
using Message;
using UnityEngine;

// Token: 0x02000907 RID: 2311
[AddComponentMenu("Scripts/Runners/Object/Common")]
public class ObjRing : SpawnableObject
{
	// Token: 0x06003CE3 RID: 15587 RVA: 0x001404D8 File Offset: 0x0013E6D8
	public static string GetRingModelName()
	{
		if (EventManager.Instance != null && EventManager.Instance.IsRaidBossStage())
		{
			return EventBossObjectTable.GetItemData(EventBossObjectTableItem.RingModel);
		}
		return "obj_cmn_ring";
	}

	// Token: 0x06003CE4 RID: 15588 RVA: 0x00140508 File Offset: 0x0013E708
	protected override string GetModelName()
	{
		return ObjRing.GetRingModelName();
	}

	// Token: 0x06003CE5 RID: 15589 RVA: 0x00140510 File Offset: 0x0013E710
	public static ResourceCategory GetRingModelCategory()
	{
		if (EventManager.Instance != null && EventManager.Instance.IsRaidBossStage())
		{
			return ResourceCategory.EVENT_RESOURCE;
		}
		return ResourceCategory.OBJECT_RESOURCE;
	}

	// Token: 0x06003CE6 RID: 15590 RVA: 0x00140538 File Offset: 0x0013E738
	protected override ResourceCategory GetModelCategory()
	{
		return ObjRing.GetRingModelCategory();
	}

	// Token: 0x06003CE7 RID: 15591 RVA: 0x00140540 File Offset: 0x0013E740
	public static string GetRingEffect()
	{
		if (EventManager.Instance != null && EventManager.Instance.IsRaidBossStage())
		{
			return EventBossObjectTable.GetItemData(EventBossObjectTableItem.RingEffect);
		}
		return "ef_ob_get_ring01";
	}

	// Token: 0x06003CE8 RID: 15592 RVA: 0x00140570 File Offset: 0x0013E770
	public static void SetPlayRingSE()
	{
		if (EventManager.Instance != null && EventManager.Instance.IsRaidBossStage())
		{
			ObjUtil.LightPlayEventSE(EventBossObjectTable.GetItemData(EventBossObjectTableItem.RingSE), EventManager.EventType.RAID_BOSS);
		}
		ObjUtil.LightPlaySE("obj_ring", "SE");
	}

	// Token: 0x06003CE9 RID: 15593 RVA: 0x001405BC File Offset: 0x0013E7BC
	public static int GetScore()
	{
		return 10;
	}

	// Token: 0x06003CEA RID: 15594 RVA: 0x001405C0 File Offset: 0x0013E7C0
	protected override void OnSpawned()
	{
		if (StageComboManager.Instance != null && (StageComboManager.Instance.IsActiveComboChaoAbility(ChaoAbility.COMBO_RECOVERY_ALL_OBJ) || StageComboManager.Instance.IsActiveComboChaoAbility(ChaoAbility.COMBO_DESTROY_AND_RECOVERY)))
		{
			this.OnDrawingRings(new MsgOnDrawingRings());
		}
		base.enabled = false;
	}

	// Token: 0x06003CEB RID: 15595 RVA: 0x00140614 File Offset: 0x0013E814
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

	// Token: 0x06003CEC RID: 15596 RVA: 0x0014067C File Offset: 0x0013E87C
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
						gameObject.SendMessage("OnAddRings", 1, SendMessageOptions.DontRequireReceiver);
						this.TakeRing();
					}
				}
				else if (a == "HitRing" && gameObject.tag == "Chao")
				{
					GameObjectUtil.SendMessageToTagObjects("Player", "OnAddRings", 1, SendMessageOptions.DontRequireReceiver);
					this.TakeRing();
				}
			}
		}
	}

	// Token: 0x06003CED RID: 15597 RVA: 0x00140744 File Offset: 0x0013E944
	private void OnDrawingRings(MsgOnDrawingRings msg)
	{
		ObjUtil.StartMagnetControl(base.gameObject);
	}

	// Token: 0x06003CEE RID: 15598 RVA: 0x00140754 File Offset: 0x0013E954
	private void OnDrawingRingsToChao(MsgOnDrawingRings msg)
	{
		if (msg != null)
		{
			ObjUtil.StartMagnetControl(base.gameObject, msg.m_target);
		}
	}

	// Token: 0x06003CEF RID: 15599 RVA: 0x00140770 File Offset: 0x0013E970
	private void OnDrawingRingsChaoAbility(MsgOnDrawingRings msg)
	{
		if (msg.m_chaoAbility == ChaoAbility.COMBO_RECOVERY_ALL_OBJ || msg.m_chaoAbility == ChaoAbility.COMBO_DESTROY_AND_RECOVERY)
		{
			this.OnDrawingRings(new MsgOnDrawingRings());
		}
	}

	// Token: 0x06003CF0 RID: 15600 RVA: 0x00140798 File Offset: 0x0013E998
	private void TakeRing()
	{
		this.m_end = true;
		if (StageEffectManager.Instance != null)
		{
			StageEffectManager.Instance.PlayEffect(EffectPlayType.RING, ObjUtil.GetCollisionCenterPosition(base.gameObject), Quaternion.identity);
		}
		ObjRing.SetPlayRingSE();
		if (base.Share)
		{
			base.gameObject.SetActive(false);
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04003516 RID: 13590
	private const int m_add_ring_value = 1;

	// Token: 0x04003517 RID: 13591
	private bool m_end;
}
