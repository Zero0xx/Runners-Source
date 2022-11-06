using System;
using UnityEngine;

// Token: 0x020006A1 RID: 1697
public class ServerEquipChaoRetry : ServerRetryProcess
{
	// Token: 0x06002DBD RID: 11709 RVA: 0x0011044C File Offset: 0x0010E64C
	public ServerEquipChaoRetry(int mainChaoId, int subChaoId, GameObject callbackObject) : base(callbackObject)
	{
		this.m_mainChaoId = mainChaoId;
		this.m_subChaoId = subChaoId;
	}

	// Token: 0x06002DBE RID: 11710 RVA: 0x00110464 File Offset: 0x0010E664
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerEquipChao(this.m_mainChaoId, this.m_subChaoId, this.m_callbackObject);
		}
	}

	// Token: 0x040029E5 RID: 10725
	public int m_mainChaoId;

	// Token: 0x040029E6 RID: 10726
	public int m_subChaoId;
}
