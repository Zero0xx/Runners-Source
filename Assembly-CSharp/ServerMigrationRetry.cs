using System;
using UnityEngine;

// Token: 0x0200076C RID: 1900
public class ServerMigrationRetry : ServerRetryProcess
{
	// Token: 0x060032A3 RID: 12963 RVA: 0x00119B00 File Offset: 0x00117D00
	public ServerMigrationRetry(string migrationId, string migrationPassword, GameObject callbackObject) : base(callbackObject)
	{
		this.m_migrationId = migrationId;
		this.m_migrationPassword = migrationPassword;
	}

	// Token: 0x060032A4 RID: 12964 RVA: 0x00119B18 File Offset: 0x00117D18
	public override void Retry()
	{
		ServerInterface serverInterface = GameObjectUtil.FindGameObjectComponent<ServerInterface>("ServerInterface");
		if (serverInterface != null)
		{
			serverInterface.RequestServerMigration(this.m_migrationId, this.m_migrationPassword, this.m_callbackObject);
		}
	}

	// Token: 0x04002B89 RID: 11145
	public string m_migrationId;

	// Token: 0x04002B8A RID: 11146
	public string m_migrationPassword;
}
