using System;
using UnityEngine;

// Token: 0x020004D4 RID: 1236
public abstract class MenuPlayerSetPartsBase : MonoBehaviour
{
	// Token: 0x06002492 RID: 9362 RVA: 0x000DB580 File Offset: 0x000D9780
	public MenuPlayerSetPartsBase(string panelName)
	{
		this.m_isReady = false;
	}

	// Token: 0x06002493 RID: 9363 RVA: 0x000DB590 File Offset: 0x000D9790
	public void PlayStart()
	{
		this.OnPlayStart();
	}

	// Token: 0x06002494 RID: 9364 RVA: 0x000DB598 File Offset: 0x000D9798
	public void PlayEnd()
	{
		this.OnPlayEnd();
	}

	// Token: 0x06002495 RID: 9365 RVA: 0x000DB5A0 File Offset: 0x000D97A0
	public void Reset()
	{
		this.m_isReady = false;
	}

	// Token: 0x06002496 RID: 9366 RVA: 0x000DB5AC File Offset: 0x000D97AC
	public void LateUpdate()
	{
		float deltaTime = Time.deltaTime;
		if (!this.m_isReady)
		{
			this.OnSetup();
			this.PlayStart();
			this.m_isReady = true;
		}
		this.OnUpdate(deltaTime);
	}

	// Token: 0x06002497 RID: 9367
	protected abstract void OnSetup();

	// Token: 0x06002498 RID: 9368
	protected abstract void OnPlayStart();

	// Token: 0x06002499 RID: 9369
	protected abstract void OnPlayEnd();

	// Token: 0x0600249A RID: 9370
	protected abstract void OnUpdate(float deltaTime);

	// Token: 0x040020EE RID: 8430
	private UIPanel m_panel;

	// Token: 0x040020EF RID: 8431
	private bool m_isReady;
}
