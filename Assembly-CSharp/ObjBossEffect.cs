using System;
using UnityEngine;

// Token: 0x02000842 RID: 2114
public class ObjBossEffect : MonoBehaviour
{
	// Token: 0x0600391A RID: 14618 RVA: 0x0012ED9C File Offset: 0x0012CF9C
	private void OnDestroy()
	{
	}

	// Token: 0x0600391B RID: 14619 RVA: 0x0012EDA0 File Offset: 0x0012CFA0
	public void SetHitOffset(Vector3 hit_offset)
	{
		this.m_hit_offset = hit_offset;
	}

	// Token: 0x0600391C RID: 14620 RVA: 0x0012EDAC File Offset: 0x0012CFAC
	public void PlayChaoEffect()
	{
		this.OnPlayChaoEffect();
	}

	// Token: 0x0600391D RID: 14621 RVA: 0x0012EDB4 File Offset: 0x0012CFB4
	protected virtual void OnPlayChaoEffect()
	{
	}

	// Token: 0x0600391E RID: 14622 RVA: 0x0012EDB8 File Offset: 0x0012CFB8
	protected void PlayChaoEffectSE()
	{
		if (this.m_chaoSEID == 0U)
		{
			this.m_chaoSEID = (uint)ObjUtil.PlaySE("act_boss_abnormal", "SE");
		}
	}

	// Token: 0x04002FFA RID: 12282
	private const string CHAO_SENAME = "act_boss_abnormal";

	// Token: 0x04002FFB RID: 12283
	protected Vector3 m_hit_offset = Vector3.zero;

	// Token: 0x04002FFC RID: 12284
	private uint m_chaoSEID;
}
