using System;
using UnityEngine;

// Token: 0x02000309 RID: 777
public class EffectShareState : MonoBehaviour
{
	// Token: 0x060016CD RID: 5837 RVA: 0x00083380 File Offset: 0x00081580
	public bool IsSleep()
	{
		return this.m_state == EffectShareState.State.Sleep;
	}

	// Token: 0x060016CE RID: 5838 RVA: 0x0008338C File Offset: 0x0008158C
	public void SetState(EffectShareState.State state)
	{
		this.m_state = state;
	}

	// Token: 0x060016CF RID: 5839 RVA: 0x00083398 File Offset: 0x00081598
	private void Start()
	{
		base.enabled = false;
	}

	// Token: 0x04001463 RID: 5219
	private EffectShareState.State m_state;

	// Token: 0x04001464 RID: 5220
	public EffectPlayType m_effectType;

	// Token: 0x0200030A RID: 778
	public enum State
	{
		// Token: 0x04001466 RID: 5222
		Sleep,
		// Token: 0x04001467 RID: 5223
		Active
	}
}
