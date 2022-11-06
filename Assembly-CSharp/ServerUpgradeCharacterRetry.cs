using System;
using UnityEngine;

// Token: 0x020006B3 RID: 1715
public class ServerUpgradeCharacterRetry : ServerRetryProcess
{
	// Token: 0x06002E11 RID: 11793 RVA: 0x00110C5C File Offset: 0x0010EE5C
	public ServerUpgradeCharacterRetry(int characterId, int abilityId, GameObject callbackObject) : base(callbackObject)
	{
		this.m_characterId = characterId;
		this.m_abilityId = abilityId;
	}

	// Token: 0x06002E12 RID: 11794 RVA: 0x00110C74 File Offset: 0x0010EE74
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerUpgradeCharacter(this.m_characterId, this.m_abilityId, this.m_callbackObject);
		}
	}

	// Token: 0x040029FC RID: 10748
	public int m_characterId;

	// Token: 0x040029FD RID: 10749
	public int m_abilityId;
}
