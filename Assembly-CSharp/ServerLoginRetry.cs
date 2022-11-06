using System;
using UnityEngine;

// Token: 0x02000766 RID: 1894
public class ServerLoginRetry : ServerRetryProcess
{
	// Token: 0x06003296 RID: 12950 RVA: 0x00119908 File Offset: 0x00117B08
	public ServerLoginRetry(string userId, string password, GameObject callbackObject) : base(callbackObject)
	{
		this.m_userId = userId;
		this.m_password = password;
	}

	// Token: 0x06003297 RID: 12951 RVA: 0x00119920 File Offset: 0x00117B20
	public override void Retry()
	{
		ServerInterface serverInterface = GameObjectUtil.FindGameObjectComponent<ServerInterface>("ServerInterface");
		if (serverInterface != null)
		{
			serverInterface.RequestServerLogin(this.m_userId, this.m_password, this.m_callbackObject);
		}
	}

	// Token: 0x04002B80 RID: 11136
	public string m_userId;

	// Token: 0x04002B81 RID: 11137
	public string m_password;
}
