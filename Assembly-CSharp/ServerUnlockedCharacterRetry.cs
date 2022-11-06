using System;
using UnityEngine;

// Token: 0x020006B1 RID: 1713
public class ServerUnlockedCharacterRetry : ServerRetryProcess
{
	// Token: 0x06002E0D RID: 11789 RVA: 0x00110BC4 File Offset: 0x0010EDC4
	public ServerUnlockedCharacterRetry(CharaType charaType, ServerItem serverItem, GameObject callbackObject) : base(callbackObject)
	{
		this.m_charaType = charaType;
		this.m_item = serverItem;
	}

	// Token: 0x06002E0E RID: 11790 RVA: 0x00110BDC File Offset: 0x0010EDDC
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerUnlockedCharacter(this.m_charaType, this.m_item, this.m_callbackObject);
		}
	}

	// Token: 0x040029FA RID: 10746
	public CharaType m_charaType;

	// Token: 0x040029FB RID: 10747
	public ServerItem m_item;
}
