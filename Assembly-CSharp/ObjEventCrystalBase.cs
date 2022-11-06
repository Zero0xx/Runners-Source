using System;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x020008CF RID: 2255
public class ObjEventCrystalBase : SpawnableObject
{
	// Token: 0x06003BFA RID: 15354 RVA: 0x0013C660 File Offset: 0x0013A860
	protected override ResourceCategory GetModelCategory()
	{
		return ResourceCategory.EVENT_RESOURCE;
	}

	// Token: 0x06003BFB RID: 15355 RVA: 0x0013C664 File Offset: 0x0013A864
	protected override void OnSpawned()
	{
		base.enabled = false;
		this.m_playerInfo = ObjUtil.GetPlayerInformation();
		this.CheckActiveComboChaoAbility();
	}

	// Token: 0x06003BFC RID: 15356 RVA: 0x0013C680 File Offset: 0x0013A880
	private EventCtystalType GetCtystalType()
	{
		return this.GetOriginalType();
	}

	// Token: 0x06003BFD RID: 15357 RVA: 0x0013C688 File Offset: 0x0013A888
	protected virtual int GetAddCount()
	{
		return 0;
	}

	// Token: 0x06003BFE RID: 15358 RVA: 0x0013C68C File Offset: 0x0013A88C
	protected virtual EventCtystalType GetOriginalType()
	{
		return EventCtystalType.SMALL;
	}

	// Token: 0x06003BFF RID: 15359 RVA: 0x0013C690 File Offset: 0x0013A890
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
						this.TakeCrystal();
					}
				}
				else if (a == "HitCrystal" && gameObject.tag == "Chao")
				{
					this.TakeCrystal();
				}
			}
		}
	}

	// Token: 0x06003C00 RID: 15360 RVA: 0x0013C730 File Offset: 0x0013A930
	private void OnDrawingRings(MsgOnDrawingRings msg)
	{
		ObjUtil.StartMagnetControl(base.gameObject);
	}

	// Token: 0x06003C01 RID: 15361 RVA: 0x0013C740 File Offset: 0x0013A940
	private void OnDrawingRingsToChao(MsgOnDrawingRings msg)
	{
		if (msg != null)
		{
			ObjUtil.StartMagnetControl(base.gameObject, msg.m_target);
		}
	}

	// Token: 0x06003C02 RID: 15362 RVA: 0x0013C75C File Offset: 0x0013A95C
	private void OnDrawingRingsChaoAbility(MsgOnDrawingRings msg)
	{
		if (msg.m_chaoAbility == ChaoAbility.COMBO_RECOVERY_ALL_OBJ || msg.m_chaoAbility == ChaoAbility.COMBO_DESTROY_AND_RECOVERY)
		{
			this.OnDrawingRings(new MsgOnDrawingRings());
		}
	}

	// Token: 0x06003C03 RID: 15363 RVA: 0x0013C784 File Offset: 0x0013A984
	private void TakeCrystal()
	{
		this.m_end = true;
		CtystalParam ctystalParam = ObjEventCrystalData.GetCtystalParam(this.GetCtystalType());
		if (ctystalParam != null)
		{
			ObjUtil.SendMessageAddSpecialCrystal(this.GetSrytalCount());
			ObjEventCrystalBase.SetChaoAbliltyScoreEffect(this.m_playerInfo, ctystalParam, base.gameObject);
			if (StageEffectManager.Instance != null)
			{
				StageEffectManager.Instance.PlayEffect((!ctystalParam.m_big) ? EffectPlayType.CRYSTAL_C : EffectPlayType.CRYSTAL_BIG_C, ObjUtil.GetCollisionCenterPosition(base.gameObject), Quaternion.identity);
			}
			ObjUtil.LightPlaySE(ctystalParam.m_se, "SE");
			if (base.Share)
			{
				base.gameObject.SetActive(false);
			}
			else
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x06003C04 RID: 15364 RVA: 0x0013C840 File Offset: 0x0013AA40
	private int GetSrytalCount()
	{
		if (this.m_playerInfo != null && this.m_playerInfo.PhantomType == PhantomType.DRILL)
		{
			return this.GetAddCount() * this.DRILL_UP_COUNT;
		}
		return this.GetAddCount();
	}

	// Token: 0x06003C05 RID: 15365 RVA: 0x0013C884 File Offset: 0x0013AA84
	public static void SetChaoAbliltyScoreEffect(PlayerInformation playerInfo, CtystalParam ctystalParam, GameObject obj)
	{
		if (ctystalParam != null)
		{
			List<ChaoAbility> abilityList = new List<ChaoAbility>();
			ObjUtil.GetChaoAbliltyPhantomFlag(playerInfo, ref abilityList);
			int chaoAbliltyScore = ObjUtil.GetChaoAbliltyScore(abilityList, ctystalParam.m_score);
			ObjUtil.SendMessageAddScore(chaoAbliltyScore);
			ObjUtil.SendMessageScoreCheck(new StageScoreData(3, chaoAbliltyScore));
			ObjUtil.AddCombo();
		}
	}

	// Token: 0x06003C06 RID: 15366 RVA: 0x0013C8CC File Offset: 0x0013AACC
	public void CheckActiveComboChaoAbility()
	{
		if (StageComboManager.Instance != null && (StageComboManager.Instance.IsActiveComboChaoAbility(ChaoAbility.COMBO_RECOVERY_ALL_OBJ) || StageComboManager.Instance.IsActiveComboChaoAbility(ChaoAbility.COMBO_DESTROY_AND_RECOVERY)))
		{
			this.OnDrawingRings(new MsgOnDrawingRings());
		}
	}

	// Token: 0x06003C07 RID: 15367 RVA: 0x0013C918 File Offset: 0x0013AB18
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
		this.CheckActiveComboChaoAbility();
		this.m_end = false;
	}

	// Token: 0x04003456 RID: 13398
	private PlayerInformation m_playerInfo;

	// Token: 0x04003457 RID: 13399
	private bool m_end;

	// Token: 0x04003458 RID: 13400
	private int DRILL_UP_COUNT = 3;
}
