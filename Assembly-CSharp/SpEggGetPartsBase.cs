using System;
using UnityEngine;

// Token: 0x02000362 RID: 866
public abstract class SpEggGetPartsBase
{
	// Token: 0x170003CE RID: 974
	// (get) Token: 0x060019A3 RID: 6563 RVA: 0x000954B0 File Offset: 0x000936B0
	public int ChaoId
	{
		get
		{
			return this.m_chaoId;
		}
	}

	// Token: 0x060019A4 RID: 6564
	public abstract void Setup(GameObject spEggGetObjectRoot);

	// Token: 0x060019A5 RID: 6565
	public abstract void PlaySE(string seType);

	// Token: 0x040016EF RID: 5871
	protected int m_chaoId = -1;
}
