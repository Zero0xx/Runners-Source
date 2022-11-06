using System;
using Boss;
using UnityEngine;

// Token: 0x02000860 RID: 2144
public class ObjBossEggmanEffect : ObjBossEffect
{
	// Token: 0x06003A50 RID: 14928 RVA: 0x00133858 File Offset: 0x00131A58
	private void OnDestroy()
	{
		this.PlaySweatEffectEnd();
		this.DestroyBoostEffect();
	}

	// Token: 0x06003A51 RID: 14929 RVA: 0x00133868 File Offset: 0x00131A68
	public void PlayHitEffect()
	{
		if (this.m_hit_offset.y > 0.5f)
		{
			this.m_hit_offset.y = Mathf.Min(this.m_hit_offset.y, 0.5f);
		}
		else
		{
			this.m_hit_offset.y = Mathf.Max(this.m_hit_offset.y, -0.5f);
		}
		ObjUtil.PlayEffect("ef_bo_em_damage01", base.transform.position + new Vector3(0f, this.m_hit_offset.y, 0f), Quaternion.identity, 1f, false);
	}

	// Token: 0x06003A52 RID: 14930 RVA: 0x00133910 File Offset: 0x00131B10
	public void PlayFoundEffect()
	{
		ObjUtil.PlayEffectChild(base.gameObject, "ef_bo_em_found01", ObjBossEggmanEffect.APPEAR_EFFECT_OFFSET, Quaternion.identity, 2f, true);
	}

	// Token: 0x06003A53 RID: 14931 RVA: 0x00133940 File Offset: 0x00131B40
	public void PlaySweatEffectStart()
	{
		this.PlaySweatEffectEnd();
		this.m_sweat_effect = ObjUtil.PlayEffectChild(base.gameObject, "ef_bo_em_sweat01", Vector3.zero, Quaternion.identity, false);
	}

	// Token: 0x06003A54 RID: 14932 RVA: 0x00133974 File Offset: 0x00131B74
	public void PlaySweatEffectEnd()
	{
		if (this.m_sweat_effect)
		{
			UnityEngine.Object.Destroy(this.m_sweat_effect);
			this.m_sweat_effect = null;
		}
	}

	// Token: 0x06003A55 RID: 14933 RVA: 0x001339A4 File Offset: 0x00131BA4
	public void PlayEscapeEffect(ObjBossEggmanState context)
	{
		ObjUtil.PlayEffectChild(base.gameObject, "ef_bo_em_blackfog01", Vector3.zero, Quaternion.identity, 5f, true);
	}

	// Token: 0x06003A56 RID: 14934 RVA: 0x001339D4 File Offset: 0x00131BD4
	public void PlayBoostEffect(ObjBossEggmanEffect.BoostType type)
	{
		if (this.m_boostType != (int)type)
		{
			this.DestroyBoostEffect();
			if (type != ObjBossEggmanEffect.BoostType.Normal)
			{
				if (type == ObjBossEggmanEffect.BoostType.Attack)
				{
					this.PlayBoostEffect("ef_bo_em_vernier_l01");
				}
			}
			else
			{
				this.PlayBoostEffect("ef_bo_em_vernier_s01");
			}
			this.m_boostType = (int)type;
		}
	}

	// Token: 0x06003A57 RID: 14935 RVA: 0x00133A30 File Offset: 0x00131C30
	public void DestroyBoostEffect()
	{
		if (this.m_boost_effectL)
		{
			UnityEngine.Object.Destroy(this.m_boost_effectL);
			this.m_boost_effectL = null;
		}
		if (this.m_boost_effectR)
		{
			UnityEngine.Object.Destroy(this.m_boost_effectR);
			this.m_boost_effectR = null;
		}
	}

	// Token: 0x06003A58 RID: 14936 RVA: 0x00133A84 File Offset: 0x00131C84
	private void PlayBoostEffect(string effectName)
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Booster_L");
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, "Booster_R");
		if (gameObject && gameObject2)
		{
			Quaternion local_rot = Quaternion.Euler(ObjBossEggmanEffect.BOOST_EFFECT_ROT);
			this.m_boost_effectL = ObjUtil.PlayEffectChild(gameObject, effectName, Vector3.zero, local_rot, true);
			this.m_boost_effectR = ObjUtil.PlayEffectChild(gameObject2, effectName, Vector3.zero, local_rot, true);
		}
	}

	// Token: 0x06003A59 RID: 14937 RVA: 0x00133AFC File Offset: 0x00131CFC
	protected override void OnPlayChaoEffect()
	{
		StageAbilityManager instance = StageAbilityManager.Instance;
		if (instance == null)
		{
			return;
		}
		if (instance.HasChaoAbility(ChaoAbility.BOSS_SUPER_RING_RATE))
		{
			base.PlayChaoEffectSE();
			ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_beam_atk_ht_sr02", Vector3.zero, -1f, false);
			ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_beam_atk_lp_sr02", Vector3.zero, -1f, false);
		}
		if (instance.HasChaoAbility(ChaoAbility.BOSS_RED_RING_RATE))
		{
			base.PlayChaoEffectSE();
			ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_beam_atk_ht_sr01", Vector3.zero, -1f, false);
			ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_beam_atk_lp_sr01", Vector3.zero, -1f, false);
		}
		if (instance.HasChaoAbility(ChaoAbility.BOSS_STAGE_TIME))
		{
			base.PlayChaoEffectSE();
			ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_magic_atk_ht_sr01", Vector3.zero, -1f, false);
			ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_magic_atk_lp_sr01", Vector3.zero, -1f, false);
		}
	}

	// Token: 0x04003184 RID: 12676
	private const float HITEFFECT_AREA = 0.5f;

	// Token: 0x04003185 RID: 12677
	private static Vector3 APPEAR_EFFECT_OFFSET = new Vector3(-1f, 0.5f, 0f);

	// Token: 0x04003186 RID: 12678
	private static Vector3 BOOST_EFFECT_ROT = new Vector3(-90f, 0f, 0f);

	// Token: 0x04003187 RID: 12679
	private GameObject m_sweat_effect;

	// Token: 0x04003188 RID: 12680
	private GameObject m_boost_effectL;

	// Token: 0x04003189 RID: 12681
	private GameObject m_boost_effectR;

	// Token: 0x0400318A RID: 12682
	private int m_boostType = -1;

	// Token: 0x02000861 RID: 2145
	public enum BoostType
	{
		// Token: 0x0400318C RID: 12684
		Normal,
		// Token: 0x0400318D RID: 12685
		Attack
	}
}
