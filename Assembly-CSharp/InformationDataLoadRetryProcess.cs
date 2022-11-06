using System;
using UnityEngine;

// Token: 0x02000335 RID: 821
internal class InformationDataLoadRetryProcess : ServerRetryProcess
{
	// Token: 0x06001863 RID: 6243 RVA: 0x0008CEB8 File Offset: 0x0008B0B8
	public InformationDataLoadRetryProcess(GameObject returnObject, TitleDataLoader loader) : base(returnObject)
	{
		this.m_loader = loader;
	}

	// Token: 0x06001864 RID: 6244 RVA: 0x0008CEC8 File Offset: 0x0008B0C8
	public override void Retry()
	{
		if (this.m_loader != null)
		{
			this.m_loader.RetryInformationDataLoad();
		}
	}

	// Token: 0x040015F1 RID: 5617
	private TitleDataLoader m_loader;
}
