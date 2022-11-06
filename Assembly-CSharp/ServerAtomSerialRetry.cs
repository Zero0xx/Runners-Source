using System;
using UnityEngine;

// Token: 0x0200078E RID: 1934
public class ServerAtomSerialRetry : ServerRetryProcess
{
	// Token: 0x0600335C RID: 13148 RVA: 0x0011B4A0 File Offset: 0x001196A0
	public ServerAtomSerialRetry(string campaignId, string serial, bool new_user, GameObject callbackObject) : base(callbackObject)
	{
		this.m_campaignId = campaignId;
		this.m_serial = serial;
		this.m_new_user = new_user;
	}

	// Token: 0x0600335D RID: 13149 RVA: 0x0011B4C0 File Offset: 0x001196C0
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerAtomSerial(this.m_campaignId, this.m_serial, this.m_new_user, this.m_callbackObject);
		}
	}

	// Token: 0x04002BC2 RID: 11202
	public string m_campaignId;

	// Token: 0x04002BC3 RID: 11203
	public string m_serial;

	// Token: 0x04002BC4 RID: 11204
	public bool m_new_user;
}
