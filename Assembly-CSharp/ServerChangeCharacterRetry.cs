using System;
using UnityEngine;

// Token: 0x020006AF RID: 1711
public class ServerChangeCharacterRetry : ServerRetryProcess
{
	// Token: 0x06002E09 RID: 11785 RVA: 0x00110B2C File Offset: 0x0010ED2C
	public ServerChangeCharacterRetry(int mainCharaId, int subCharaId, GameObject callbackObject) : base(callbackObject)
	{
		this.m_mainCharaId = mainCharaId;
		this.m_subCharaId = subCharaId;
	}

	// Token: 0x06002E0A RID: 11786 RVA: 0x00110B44 File Offset: 0x0010ED44
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerChangeCharacter(this.m_mainCharaId, this.m_subCharaId, this.m_callbackObject);
		}
	}

	// Token: 0x040029F8 RID: 10744
	public int m_mainCharaId;

	// Token: 0x040029F9 RID: 10745
	public int m_subCharaId;
}
