using System;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x020008D6 RID: 2262
public class ObjCrystalBase : SpawnableObject
{
	// Token: 0x06003C14 RID: 15380 RVA: 0x0013CA48 File Offset: 0x0013AC48
	protected override ResourceCategory GetModelCategory()
	{
		return ResourceCategory.OBJECT_RESOURCE;
	}

	// Token: 0x06003C15 RID: 15381 RVA: 0x0013CA4C File Offset: 0x0013AC4C
	protected override string GetModelName()
	{
		if (this.m_type == CtystalType.NONE)
		{
			this.m_type = this.GetCtystalModelType();
		}
		CtystalParam crystalParam = ObjCrystalData.GetCrystalParam(this.m_type);
		if (crystalParam != null)
		{
			return crystalParam.m_model;
		}
		return string.Empty;
	}

	// Token: 0x06003C16 RID: 15382 RVA: 0x0013CA90 File Offset: 0x0013AC90
	protected override void OnSpawned()
	{
		base.enabled = false;
		this.m_playerInfo = ObjUtil.GetPlayerInformation();
		if (this.m_type == CtystalType.NONE)
		{
			this.m_type = this.GetCtystalModelType();
		}
		bool flag = ObjCrystalData.IsBig(this.GetOriginalType());
		bool flag2 = ObjCrystalData.IsBig(this.m_type);
		if (flag2 && flag != flag2)
		{
			SphereCollider component = base.GetComponent<SphereCollider>();
			if (component != null)
			{
				component.center = new Vector3(component.center.x, component.center.y + 0.15f, component.center.z);
				component.radius += 0.1f;
			}
		}
		this.CheckActiveComboChaoAbility();
	}

	// Token: 0x06003C17 RID: 15383 RVA: 0x0013CB54 File Offset: 0x0013AD54
	private CtystalType GetCtystalModelType()
	{
		return ObjCrystalUtil.GetCrystalModelType(this.GetOriginalType());
	}

	// Token: 0x06003C18 RID: 15384 RVA: 0x0013CB64 File Offset: 0x0013AD64
	protected virtual CtystalType GetOriginalType()
	{
		return CtystalType.SMALL_A;
	}

	// Token: 0x06003C19 RID: 15385 RVA: 0x0013CB68 File Offset: 0x0013AD68
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

	// Token: 0x06003C1A RID: 15386 RVA: 0x0013CC08 File Offset: 0x0013AE08
	private void OnDrawingRings(MsgOnDrawingRings msg)
	{
		ObjUtil.StartMagnetControl(base.gameObject);
	}

	// Token: 0x06003C1B RID: 15387 RVA: 0x0013CC18 File Offset: 0x0013AE18
	private void OnDrawingRingsToChao(MsgOnDrawingRings msg)
	{
		if (msg != null)
		{
			ObjUtil.StartMagnetControl(base.gameObject, msg.m_target);
		}
	}

	// Token: 0x06003C1C RID: 15388 RVA: 0x0013CC34 File Offset: 0x0013AE34
	private void OnDrawingRingsChaoAbility(MsgOnDrawingRings msg)
	{
		if (msg.m_chaoAbility == ChaoAbility.COMBO_RECOVERY_ALL_OBJ || msg.m_chaoAbility == ChaoAbility.COMBO_DESTROY_AND_RECOVERY)
		{
			this.OnDrawingRings(new MsgOnDrawingRings());
		}
	}

	// Token: 0x06003C1D RID: 15389 RVA: 0x0013CC5C File Offset: 0x0013AE5C
	private EffectPlayType GetEffectType()
	{
		switch (this.m_type)
		{
		case CtystalType.SMALL_A:
			return EffectPlayType.CRYSTAL_A;
		case CtystalType.SMALL_B:
			return EffectPlayType.CRYSTAL_B;
		case CtystalType.SMALL_C:
			return EffectPlayType.CRYSTAL_C;
		case CtystalType.BIG_A:
			return EffectPlayType.CRYSTAL_BIG_A;
		case CtystalType.BIG_B:
			return EffectPlayType.CRYSTAL_BIG_B;
		case CtystalType.BIG_C:
			return EffectPlayType.CRYSTAL_BIG_C;
		default:
			return EffectPlayType.CRYSTAL_A;
		}
	}

	// Token: 0x06003C1E RID: 15390 RVA: 0x0013CCA8 File Offset: 0x0013AEA8
	private void TakeCrystal()
	{
		this.m_end = true;
		CtystalParam crystalParam = ObjCrystalData.GetCrystalParam(this.m_type);
		if (crystalParam != null)
		{
			ObjCrystalBase.SetChaoAbliltyScoreEffect(this.m_playerInfo, crystalParam, base.gameObject);
			if (StageEffectManager.Instance != null)
			{
				StageEffectManager.Instance.PlayEffect(this.GetEffectType(), ObjUtil.GetCollisionCenterPosition(base.gameObject), Quaternion.identity);
			}
			ObjUtil.LightPlaySE(crystalParam.m_se, "SE");
			HudTutorial.SendActionTutorial(HudTutorial.Id.ACTION_1);
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

	// Token: 0x06003C1F RID: 15391 RVA: 0x0013CD50 File Offset: 0x0013AF50
	public static void SetChaoAbliltyScoreEffect(PlayerInformation playerInfo, CtystalParam ctystalParam, GameObject obj)
	{
		if (ctystalParam != null)
		{
			List<ChaoAbility> abilityList = new List<ChaoAbility>();
			ObjUtil.GetChaoAbliltyPhantomFlag(playerInfo, ref abilityList);
			int chaoAbliltyScore = ObjUtil.GetChaoAbliltyScore(abilityList, ctystalParam.m_score);
			ObjUtil.SendMessageAddScore(chaoAbliltyScore);
			ObjUtil.SendMessageScoreCheck(new StageScoreData(2, chaoAbliltyScore));
			ObjUtil.AddCombo();
		}
	}

	// Token: 0x06003C20 RID: 15392 RVA: 0x0013CD98 File Offset: 0x0013AF98
	public void CheckActiveComboChaoAbility()
	{
		if (StageComboManager.Instance != null && (StageComboManager.Instance.IsActiveComboChaoAbility(ChaoAbility.COMBO_RECOVERY_ALL_OBJ) || StageComboManager.Instance.IsActiveComboChaoAbility(ChaoAbility.COMBO_DESTROY_AND_RECOVERY)))
		{
			this.OnDrawingRings(new MsgOnDrawingRings());
		}
	}

	// Token: 0x06003C21 RID: 15393 RVA: 0x0013CDE4 File Offset: 0x0013AFE4
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

	// Token: 0x0400345E RID: 13406
	private PlayerInformation m_playerInfo;

	// Token: 0x0400345F RID: 13407
	private CtystalType m_type = CtystalType.NONE;

	// Token: 0x04003460 RID: 13408
	private bool m_end;
}
