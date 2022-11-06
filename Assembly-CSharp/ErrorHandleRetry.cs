using System;
using Message;
using Text;
using UnityEngine;

// Token: 0x02000687 RID: 1671
public class ErrorHandleRetry : ErrorHandleBase
{
	// Token: 0x06002C88 RID: 11400 RVA: 0x0010CC90 File Offset: 0x0010AE90
	public void SetRetryProcess(ServerRetryProcess retryProcess)
	{
		this.m_retryProcess = retryProcess;
	}

	// Token: 0x06002C89 RID: 11401 RVA: 0x0010CC9C File Offset: 0x0010AE9C
	public override void Setup(GameObject callbackObject, string callbackFuncName, MessageBase msg)
	{
		this.m_callbackObject = callbackObject;
		this.m_callbackFuncName = callbackFuncName;
		this.m_msg = msg;
	}

	// Token: 0x06002C8A RID: 11402 RVA: 0x0010CCB4 File Offset: 0x0010AEB4
	public override void StartErrorHandle()
	{
		if (this.m_msg == null)
		{
			this.m_isEnd = true;
			return;
		}
		if (this.m_msg.ID == 61517)
		{
			MsgServerConnctFailed msgServerConnctFailed = this.m_msg as MsgServerConnctFailed;
			if (msgServerConnctFailed != null)
			{
				string message = string.Empty;
				ServerInterface.StatusCode status = msgServerConnctFailed.m_status;
				switch (status + 10)
				{
				case ServerInterface.StatusCode.Ok:
				case (ServerInterface.StatusCode)3:
					message = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_reload").text;
					break;
				default:
					if (status != ServerInterface.StatusCode.ExpirationSession)
					{
						message = string.Empty;
					}
					else
					{
						message = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_timeout").text;
					}
					break;
				}
				NetworkErrorWindow.Create(new NetworkErrorWindow.CInfo
				{
					name = "NetworkErrorReload",
					buttonType = NetworkErrorWindow.ButtonType.Ok,
					anchor_path = string.Empty,
					caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_caption").text,
					message = message,
					finishedCloseDelegate = new NetworkErrorWindow.CInfo.FinishedCloseDelegate(this.OnFinishCloseAnimCallback)
				});
			}
		}
		else if (this.m_msg.ID == 61520)
		{
			NetworkErrorWindow.Create(new NetworkErrorWindow.CInfo
			{
				name = "NetworkErrorReload",
				buttonType = NetworkErrorWindow.ButtonType.Ok,
				anchor_path = string.Empty,
				caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_caption").text,
				message = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_reload").text,
				finishedCloseDelegate = new NetworkErrorWindow.CInfo.FinishedCloseDelegate(this.OnFinishCloseAnimCallback)
			});
		}
		else
		{
			this.m_isEnd = true;
		}
	}

	// Token: 0x06002C8B RID: 11403 RVA: 0x0010CE7C File Offset: 0x0010B07C
	public override void Update()
	{
	}

	// Token: 0x06002C8C RID: 11404 RVA: 0x0010CE80 File Offset: 0x0010B080
	public override bool IsEnd()
	{
		return this.m_isEnd;
	}

	// Token: 0x06002C8D RID: 11405 RVA: 0x0010CE88 File Offset: 0x0010B088
	public override void EndErrorHandle()
	{
		NetworkErrorWindow.Close();
		if (this.m_retryProcess != null)
		{
			this.m_retryProcess.Retry();
		}
	}

	// Token: 0x06002C8E RID: 11406 RVA: 0x0010CEA8 File Offset: 0x0010B0A8
	private void OnFinishCloseAnimCallback()
	{
		if (this.m_callbackObject != null)
		{
			this.m_callbackObject.SendMessage(this.m_callbackFuncName, this.m_msg, SendMessageOptions.DontRequireReceiver);
		}
		this.m_isEnd = true;
	}

	// Token: 0x04002967 RID: 10599
	private ServerRetryProcess m_retryProcess;

	// Token: 0x04002968 RID: 10600
	private GameObject m_callbackObject;

	// Token: 0x04002969 RID: 10601
	private string m_callbackFuncName;

	// Token: 0x0400296A RID: 10602
	private MessageBase m_msg;

	// Token: 0x0400296B RID: 10603
	private bool m_isEnd;
}
