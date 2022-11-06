using System;
using UnityEngine;

// Token: 0x020006B5 RID: 1717
public class ServerUseSubCharacterRetry : ServerRetryProcess
{
	// Token: 0x06002E15 RID: 11797 RVA: 0x00110CF4 File Offset: 0x0010EEF4
	public ServerUseSubCharacterRetry(bool useFlag, GameObject callbackObject) : base(callbackObject)
	{
		this.m_useFlag = useFlag;
	}

	// Token: 0x06002E16 RID: 11798 RVA: 0x00110D04 File Offset: 0x0010EF04
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerUseSubCharacter(this.m_useFlag, this.m_callbackObject);
		}
	}

	// Token: 0x040029FE RID: 10750
	public bool m_useFlag;
}
