using System;
using UnityEngine;

// Token: 0x02000736 RID: 1846
public class ServerGetMileageDataRetry : ServerRetryProcess
{
	// Token: 0x0600313B RID: 12603 RVA: 0x00116CA0 File Offset: 0x00114EA0
	public ServerGetMileageDataRetry(string[] distanceFriendList, GameObject callbackObject) : base(callbackObject)
	{
		this.m_distanceFriendList = distanceFriendList;
	}

	// Token: 0x0600313C RID: 12604 RVA: 0x00116CB0 File Offset: 0x00114EB0
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetMileageData(this.m_distanceFriendList, this.m_callbackObject);
		}
	}

	// Token: 0x04002B1C RID: 11036
	public string[] m_distanceFriendList;
}
