using System;
using UnityEngine;

// Token: 0x0200053C RID: 1340
public abstract class SettingBase : MonoBehaviour
{
	// Token: 0x06002952 RID: 10578 RVA: 0x000FF494 File Offset: 0x000FD694
	public void Setup(string anthorPath)
	{
		this.OnSetup(anthorPath);
	}

	// Token: 0x06002953 RID: 10579 RVA: 0x000FF4A0 File Offset: 0x000FD6A0
	public void PlayStart()
	{
		this.OnPlayStart();
	}

	// Token: 0x06002954 RID: 10580 RVA: 0x000FF4A8 File Offset: 0x000FD6A8
	public bool IsEndPlay()
	{
		return this.OnIsEndPlay();
	}

	// Token: 0x06002955 RID: 10581 RVA: 0x000FF4B0 File Offset: 0x000FD6B0
	private void Update()
	{
		this.OnUpdate();
	}

	// Token: 0x06002956 RID: 10582
	protected abstract void OnSetup(string anthorPath);

	// Token: 0x06002957 RID: 10583
	protected abstract void OnPlayStart();

	// Token: 0x06002958 RID: 10584
	protected abstract bool OnIsEndPlay();

	// Token: 0x06002959 RID: 10585
	protected abstract void OnUpdate();
}
