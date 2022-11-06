using System;
using UnityEngine;

// Token: 0x020004C2 RID: 1218
public abstract class MenuPlayerSetLevelState : MonoBehaviour
{
	// Token: 0x0600240E RID: 9230 RVA: 0x000D8318 File Offset: 0x000D6518
	public MenuPlayerSetLevelState()
	{
	}

	// Token: 0x0600240F RID: 9231 RVA: 0x000D8320 File Offset: 0x000D6520
	public void Setup(AbilityButtonParams param)
	{
		this.m_params = param;
	}

	// Token: 0x06002410 RID: 9232
	public abstract void ChangeLabels();

	// Token: 0x040020AC RID: 8364
	protected AbilityButtonParams m_params;

	// Token: 0x020004C3 RID: 1219
	protected enum EventSignal
	{
		// Token: 0x040020AE RID: 8366
		BUTTON_PRESSED = 100,
		// Token: 0x040020AF RID: 8367
		SERVER_RESPONSE_END
	}
}
