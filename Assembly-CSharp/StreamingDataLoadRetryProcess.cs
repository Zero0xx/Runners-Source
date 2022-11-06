using System;
using UnityEngine;

// Token: 0x02000334 RID: 820
internal class StreamingDataLoadRetryProcess : ServerRetryProcess
{
	// Token: 0x06001861 RID: 6241 RVA: 0x0008CE60 File Offset: 0x0008B060
	public StreamingDataLoadRetryProcess(int retriedCount, GameObject returnObject, TitleDataLoader loader) : base(returnObject)
	{
		this.m_retryCount = retriedCount;
		this.m_loader = loader;
	}

	// Token: 0x06001862 RID: 6242 RVA: 0x0008CE78 File Offset: 0x0008B078
	public override void Retry()
	{
		this.m_retryCount++;
		if (this.m_loader != null)
		{
			this.m_loader.RetryStreamingDataLoad(this.m_retryCount);
		}
	}

	// Token: 0x040015EF RID: 5615
	private TitleDataLoader m_loader;

	// Token: 0x040015F0 RID: 5616
	private int m_retryCount;
}
