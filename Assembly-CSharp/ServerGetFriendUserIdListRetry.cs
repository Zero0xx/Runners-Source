using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000708 RID: 1800
public class ServerGetFriendUserIdListRetry : ServerRetryProcess
{
	// Token: 0x0600300F RID: 12303 RVA: 0x001142E4 File Offset: 0x001124E4
	public ServerGetFriendUserIdListRetry(List<string> friendFBIdList, GameObject callbackObject) : base(callbackObject)
	{
		this.m_friendFBIdList = friendFBIdList;
	}

	// Token: 0x06003010 RID: 12304 RVA: 0x001142F4 File Offset: 0x001124F4
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetFriendUserIdList(this.m_friendFBIdList, this.m_callbackObject);
		}
	}

	// Token: 0x04002AC3 RID: 10947
	public List<string> m_friendFBIdList;
}
