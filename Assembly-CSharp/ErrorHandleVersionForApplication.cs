using System;
using App;
using Message;
using UnityEngine;

// Token: 0x0200068D RID: 1677
public class ErrorHandleVersionForApplication : ErrorHandleBase
{
	// Token: 0x06002CB2 RID: 11442 RVA: 0x0010D4C0 File Offset: 0x0010B6C0
	public void SetRetryProcess(ServerRetryProcess retryProcess)
	{
		this.m_retryProcess = retryProcess;
	}

	// Token: 0x06002CB3 RID: 11443 RVA: 0x0010D4CC File Offset: 0x0010B6CC
	public override void Setup(GameObject callbackObject, string callbackFuncName, MessageBase msg)
	{
	}

	// Token: 0x06002CB4 RID: 11444 RVA: 0x0010D4D0 File Offset: 0x0010B6D0
	public override void StartErrorHandle()
	{
		if (Env.actionServerType == Env.ActionServerType.LOCAL1)
		{
			Env.actionServerType = Env.ActionServerType.LOCAL4;
		}
		else
		{
			Env.actionServerType = Env.ActionServerType.APPLICATION;
		}
		NetBaseUtil.DebugServerUrl = null;
		this.m_isEnd = true;
	}

	// Token: 0x06002CB5 RID: 11445 RVA: 0x0010D4FC File Offset: 0x0010B6FC
	public override void Update()
	{
	}

	// Token: 0x06002CB6 RID: 11446 RVA: 0x0010D500 File Offset: 0x0010B700
	public override bool IsEnd()
	{
		return this.m_isEnd;
	}

	// Token: 0x06002CB7 RID: 11447 RVA: 0x0010D508 File Offset: 0x0010B708
	public override void EndErrorHandle()
	{
		if (this.m_retryProcess != null)
		{
			this.m_retryProcess.Retry();
		}
	}

	// Token: 0x06002CB8 RID: 11448 RVA: 0x0010D520 File Offset: 0x0010B720
	private void ValidateSessionEndCallback(bool isSuccess)
	{
		this.m_isEnd = true;
	}

	// Token: 0x0400297A RID: 10618
	private bool m_isEnd;

	// Token: 0x0400297B RID: 10619
	private ServerRetryProcess m_retryProcess;
}
