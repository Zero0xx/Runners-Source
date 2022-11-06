using System;
using Boss;
using UnityEngine;

// Token: 0x02000848 RID: 2120
public class ObjBossEventBossEffect : ObjBossEffect
{
	// Token: 0x060039CD RID: 14797 RVA: 0x00130D70 File Offset: 0x0012EF70
	private void OnDestroy()
	{
	}

	// Token: 0x060039CE RID: 14798 RVA: 0x00130D74 File Offset: 0x0012EF74
	public static string GetBoostEffect(WispBoostLevel level)
	{
		string result = string.Empty;
		switch (level)
		{
		case WispBoostLevel.LEVEL1:
		case WispBoostLevel.LEVEL2:
		case WispBoostLevel.LEVEL3:
			result = ObjBossEventBossEffect.BOOST_EFFECT_NAME[(int)level];
			break;
		}
		return result;
	}

	// Token: 0x060039CF RID: 14799 RVA: 0x00130DB0 File Offset: 0x0012EFB0
	public void PlayHitEffect(WispBoostLevel level)
	{
		if (this.m_hit_offset.y > 0.5f)
		{
			this.m_hit_offset.y = Mathf.Min(this.m_hit_offset.y, 0.5f);
		}
		else
		{
			this.m_hit_offset.y = Mathf.Max(this.m_hit_offset.y, -0.5f);
		}
		Vector3 pos = base.transform.position + new Vector3(0f, this.m_hit_offset.y, 0f);
		string text = string.Empty;
		switch (level)
		{
		case WispBoostLevel.LEVEL1:
			text = "ef_raid_speedup_lv1_hit01";
			break;
		case WispBoostLevel.LEVEL2:
			text = "ef_raid_speedup_lv2_hit01";
			break;
		case WispBoostLevel.LEVEL3:
			text = "ef_raid_speedup_lv3_hit01";
			break;
		}
		if (text == string.Empty)
		{
			ObjUtil.PlayEffect("ef_bo_em_damage01", pos, Quaternion.identity, 1f, false);
		}
		else
		{
			ObjUtil.PlayEffect(text, pos, Quaternion.identity, 1f, false);
		}
	}

	// Token: 0x060039D0 RID: 14800 RVA: 0x00130EC0 File Offset: 0x0012F0C0
	public void PlayEscapeEffect(ObjBossEventBossState context)
	{
		ObjUtil.PlayEffectChild(base.gameObject, EventBossObjectTable.GetItemData(EventBossObjectTableItem.EscapeEffect), Vector3.zero, Quaternion.identity, 5f, true);
	}

	// Token: 0x060039D1 RID: 14801 RVA: 0x00130EF0 File Offset: 0x0012F0F0
	protected override void OnPlayChaoEffect()
	{
	}

	// Token: 0x0400304B RID: 12363
	private const float HITEFFECT_AREA = 0.5f;

	// Token: 0x0400304C RID: 12364
	private static readonly string[] BOOST_EFFECT_NAME = new string[]
	{
		"ef_raid_speedup_lv1_fog01",
		"ef_raid_speedup_lv2_fog01",
		"ef_raid_speedup_lv3_fog01"
	};
}
