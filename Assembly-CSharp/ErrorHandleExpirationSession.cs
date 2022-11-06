using System;
using Message;
using Text;
using UnityEngine;

// Token: 0x02000681 RID: 1665
public class ErrorHandleExpirationSession : ErrorHandleBase
{
	// Token: 0x06002C6A RID: 11370 RVA: 0x0010C840 File Offset: 0x0010AA40
	public override void Setup(GameObject callbackObject, string callbackFuncName, MessageBase msg)
	{
	}

	// Token: 0x06002C6B RID: 11371 RVA: 0x0010C844 File Offset: 0x0010AA44
	public void SetRetryProcess(ServerRetryProcess retryProcess)
	{
		this.m_retryProcess = retryProcess;
	}

	// Token: 0x06002C6C RID: 11372 RVA: 0x0010C850 File Offset: 0x0010AA50
	public void SetSessionValidateType(ServerSessionWatcher.ValidateType validateType)
	{
		this.m_validateType = validateType;
	}

	// Token: 0x06002C6D RID: 11373 RVA: 0x0010C85C File Offset: 0x0010AA5C
	public override void StartErrorHandle()
	{
		if (this.m_state != ErrorHandleExpirationSession.State.IDLE)
		{
			return;
		}
		this.m_state = ErrorHandleExpirationSession.State.VALIDATING;
		ServerSessionWatcher serverSessionWatcher = GameObjectUtil.FindGameObjectComponent<ServerSessionWatcher>("NetMonitor");
		if (serverSessionWatcher != null)
		{
			serverSessionWatcher.ValidateSession(this.m_validateType, new ServerSessionWatcher.ValidateSessionEndCallback(this.ValidateSessionCallback));
		}
	}

	// Token: 0x06002C6E RID: 11374 RVA: 0x0010C8AC File Offset: 0x0010AAAC
	public override void Update()
	{
		if (this.m_state == ErrorHandleExpirationSession.State.FAILED && NetworkErrorWindow.IsOkButtonPressed)
		{
			NetworkErrorWindow.Close();
			HudMenuUtility.GoToTitleScene();
			this.m_isEnd = true;
		}
	}

	// Token: 0x06002C6F RID: 11375 RVA: 0x0010C8E4 File Offset: 0x0010AAE4
	public override bool IsEnd()
	{
		return this.m_isEnd;
	}

	// Token: 0x06002C70 RID: 11376 RVA: 0x0010C8EC File Offset: 0x0010AAEC
	public override void EndErrorHandle()
	{
		if (this.m_state == ErrorHandleExpirationSession.State.SUCCESS && this.m_retryProcess != null)
		{
			this.m_retryProcess.Retry();
		}
	}

	// Token: 0x06002C71 RID: 11377 RVA: 0x0010C91C File Offset: 0x0010AB1C
	private void ValidateSessionCallback(bool isSuccess)
	{
		if (isSuccess)
		{
			this.m_state = ErrorHandleExpirationSession.State.SUCCESS;
			this.m_isEnd = true;
		}
		else
		{
			NetworkErrorWindow.Create(new NetworkErrorWindow.CInfo
			{
				buttonType = NetworkErrorWindow.ButtonType.Ok,
				anchor_path = string.Empty,
				caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_session_timeout_caption").text,
				message = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_session_timeout_text").text
			});
			this.m_state = ErrorHandleExpirationSession.State.FAILED;
		}
	}

	// Token: 0x04002955 RID: 10581
	private bool m_isEnd;

	// Token: 0x04002956 RID: 10582
	private ServerRetryProcess m_retryProcess;

	// Token: 0x04002957 RID: 10583
	private ServerSessionWatcher.ValidateType m_validateType;

	// Token: 0x04002958 RID: 10584
	private ErrorHandleExpirationSession.State m_state;

	// Token: 0x02000682 RID: 1666
	private enum State
	{
		// Token: 0x0400295A RID: 10586
		NONE = -1,
		// Token: 0x0400295B RID: 10587
		IDLE,
		// Token: 0x0400295C RID: 10588
		VALIDATING,
		// Token: 0x0400295D RID: 10589
		SUCCESS,
		// Token: 0x0400295E RID: 10590
		FAILED,
		// Token: 0x0400295F RID: 10591
		COUNT
	}
}
