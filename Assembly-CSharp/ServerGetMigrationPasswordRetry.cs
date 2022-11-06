using System;
using UnityEngine;

// Token: 0x0200075E RID: 1886
public class ServerGetMigrationPasswordRetry : ServerRetryProcess
{
	// Token: 0x06003286 RID: 12934 RVA: 0x00119748 File Offset: 0x00117948
	public ServerGetMigrationPasswordRetry(string userPassword, GameObject callbackObject) : base(callbackObject)
	{
		this.m_userPassword = userPassword;
	}

	// Token: 0x06003287 RID: 12935 RVA: 0x00119758 File Offset: 0x00117958
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetMigrationPassword(this.m_userPassword, this.m_callbackObject);
		}
	}

	// Token: 0x04002B7F RID: 11135
	public string m_userPassword;
}
