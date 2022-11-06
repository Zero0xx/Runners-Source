using System;
using Message;
using UnityEngine;

// Token: 0x02000688 RID: 1672
public class ErrorHandleSecurityError : ErrorHandleBase
{
	// Token: 0x06002C90 RID: 11408 RVA: 0x0010CEF0 File Offset: 0x0010B0F0
	public void SetRetryProcess(ServerRetryProcess retryProcess)
	{
		this.m_retryProcess = retryProcess;
	}

	// Token: 0x06002C91 RID: 11409 RVA: 0x0010CEFC File Offset: 0x0010B0FC
	public override void Setup(GameObject callbackObject, string callbackFuncName, MessageBase msg)
	{
	}

	// Token: 0x06002C92 RID: 11410 RVA: 0x0010CF00 File Offset: 0x0010B100
	public override void StartErrorHandle()
	{
		ServerSessionWatcher serverSessionWatcher = GameObjectUtil.FindGameObjectComponent<ServerSessionWatcher>("NetMonitor");
		if (serverSessionWatcher != null)
		{
			serverSessionWatcher.InvalidateSession();
			this.m_isEnd = true;
		}
	}

	// Token: 0x06002C93 RID: 11411 RVA: 0x0010CF34 File Offset: 0x0010B134
	public override void Update()
	{
	}

	// Token: 0x06002C94 RID: 11412 RVA: 0x0010CF38 File Offset: 0x0010B138
	public override bool IsEnd()
	{
		return this.m_isEnd;
	}

	// Token: 0x06002C95 RID: 11413 RVA: 0x0010CF40 File Offset: 0x0010B140
	public override void EndErrorHandle()
	{
		if (this.m_retryProcess != null)
		{
			this.m_retryProcess.Retry();
		}
	}

	// Token: 0x06002C96 RID: 11414 RVA: 0x0010CF58 File Offset: 0x0010B158
	private void ValidateSessionEndCallback(bool isSuccess)
	{
		this.m_isEnd = true;
	}

	// Token: 0x0400296C RID: 10604
	private bool m_isEnd;

	// Token: 0x0400296D RID: 10605
	private ServerRetryProcess m_retryProcess;
}
