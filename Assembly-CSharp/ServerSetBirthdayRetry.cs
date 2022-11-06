using System;
using UnityEngine;

// Token: 0x020007C0 RID: 1984
public class ServerSetBirthdayRetry : ServerRetryProcess
{
	// Token: 0x0600344B RID: 13387 RVA: 0x0011CEEC File Offset: 0x0011B0EC
	public ServerSetBirthdayRetry(string birthday, GameObject callbackObject) : base(callbackObject)
	{
		this.m_birthday = birthday;
	}

	// Token: 0x0600344C RID: 13388 RVA: 0x0011CEFC File Offset: 0x0011B0FC
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerSetBirthday(this.m_birthday, this.m_callbackObject);
		}
	}

	// Token: 0x04002C11 RID: 11281
	public string m_birthday;
}
