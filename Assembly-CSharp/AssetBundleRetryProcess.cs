using System;
using UnityEngine;

// Token: 0x020009D3 RID: 2515
public class AssetBundleRetryProcess : ServerRetryProcess
{
	// Token: 0x06004211 RID: 16913 RVA: 0x001586F0 File Offset: 0x001568F0
	public AssetBundleRetryProcess(GameObject callbackObject) : base(callbackObject)
	{
	}

	// Token: 0x06004212 RID: 16914 RVA: 0x001586FC File Offset: 0x001568FC
	public override void Retry()
	{
		if (this.m_callbackObject != null)
		{
			AssetBundleLoader component = this.m_callbackObject.GetComponent<AssetBundleLoader>();
			if (component != null)
			{
				component.RetryLoadScene(this);
			}
		}
	}
}
