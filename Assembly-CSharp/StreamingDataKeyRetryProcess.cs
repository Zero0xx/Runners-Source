using System;
using UnityEngine;

// Token: 0x02000329 RID: 809
internal class StreamingDataKeyRetryProcess : ServerRetryProcess
{
	// Token: 0x060017F7 RID: 6135 RVA: 0x0008868C File Offset: 0x0008688C
	public StreamingDataKeyRetryProcess(GameObject returnObject, GameModeTitle title) : base(returnObject)
	{
		this.m_title = title;
	}

	// Token: 0x060017F8 RID: 6136 RVA: 0x0008869C File Offset: 0x0008689C
	public override void Retry()
	{
		if (this.m_title != null)
		{
			this.m_title.StreamingKeyDataRetry();
		}
	}

	// Token: 0x04001583 RID: 5507
	private GameModeTitle m_title;
}
