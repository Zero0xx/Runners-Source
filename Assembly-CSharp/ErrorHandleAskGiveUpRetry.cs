using System;
using Message;
using Text;
using UnityEngine;

// Token: 0x0200067E RID: 1662
public class ErrorHandleAskGiveUpRetry : ErrorHandleBase
{
	// Token: 0x06002C5C RID: 11356 RVA: 0x0010C484 File Offset: 0x0010A684
	public void SetRetryProcess(ServerRetryProcess retryProcess)
	{
		this.m_retryProcess = retryProcess;
	}

	// Token: 0x06002C5D RID: 11357 RVA: 0x0010C490 File Offset: 0x0010A690
	public override void Setup(GameObject callbackObject, string callbackFuncName, MessageBase msg)
	{
		this.m_callbackObject = callbackObject;
		this.m_callbackFuncName = callbackFuncName;
		this.m_msg = msg;
	}

	// Token: 0x06002C5E RID: 11358 RVA: 0x0010C4A8 File Offset: 0x0010A6A8
	public override void StartErrorHandle()
	{
		if (this.m_msg == null)
		{
			this.m_state = ErrorHandleAskGiveUpRetry.State.END;
			return;
		}
		this.m_isRetry = false;
		this.m_state = ErrorHandleAskGiveUpRetry.State.INIT;
	}

	// Token: 0x06002C5F RID: 11359 RVA: 0x0010C4CC File Offset: 0x0010A6CC
	public override void Update()
	{
		switch (this.m_state)
		{
		case ErrorHandleAskGiveUpRetry.State.INIT:
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
						message = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_ask_to_giveup_retry").text;
						goto IL_9E;
					}
					message = string.Empty;
					IL_9E:
					NetworkErrorWindow.Create(new NetworkErrorWindow.CInfo
					{
						name = "NetworkErrorRetry",
						buttonType = NetworkErrorWindow.ButtonType.YesNo,
						anchor_path = string.Empty,
						caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_caption").text,
						finishedCloseDelegate = new NetworkErrorWindow.CInfo.FinishedCloseDelegate(this.OnFinishedCloseAnimCallback),
						message = message
					});
				}
				this.m_state = ErrorHandleAskGiveUpRetry.State.ASK_GIVEUP;
			}
			else if (this.m_msg.ID == 61520)
			{
				NetworkErrorWindow.Create(new NetworkErrorWindow.CInfo
				{
					name = "NetworkErrorRetry",
					buttonType = NetworkErrorWindow.ButtonType.YesNo,
					anchor_path = string.Empty,
					caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_caption").text,
					message = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_ask_to_giveup_retry").text,
					finishedCloseDelegate = new NetworkErrorWindow.CInfo.FinishedCloseDelegate(this.OnFinishedCloseAnimCallback)
				});
				this.m_state = ErrorHandleAskGiveUpRetry.State.ASK_GIVEUP;
			}
			else
			{
				this.m_state = ErrorHandleAskGiveUpRetry.State.END;
			}
			break;
		case ErrorHandleAskGiveUpRetry.State.ASK_GIVEUP:
			if (!NetworkErrorWindow.IsYesButtonPressed)
			{
				if (NetworkErrorWindow.IsNoButtonPressed)
				{
					NetworkErrorWindow.Close();
					NetworkErrorWindow.Create(new NetworkErrorWindow.CInfo
					{
						buttonType = NetworkErrorWindow.ButtonType.YesNo,
						anchor_path = string.Empty,
						caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_caption").text,
						message = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_giveup_confirmation").text
					});
					this.m_state = ErrorHandleAskGiveUpRetry.State.CONFIRMATION;
				}
			}
			break;
		case ErrorHandleAskGiveUpRetry.State.CONFIRMATION:
			if (NetworkErrorWindow.IsYesButtonPressed)
			{
				NetworkErrorWindow.Close();
				if (this.m_callbackObject != null)
				{
					this.m_callbackObject.SendMessage(this.m_callbackFuncName, this.m_msg, SendMessageOptions.DontRequireReceiver);
				}
				GameObjectUtil.SendMessageFindGameObject("NetMonitor", "OnResetConnectFailedCount", null, SendMessageOptions.DontRequireReceiver);
				HudMenuUtility.GoToTitleScene();
				this.m_state = ErrorHandleAskGiveUpRetry.State.END;
			}
			else if (NetworkErrorWindow.IsNoButtonPressed)
			{
				NetworkErrorWindow.Close();
				this.m_state = ErrorHandleAskGiveUpRetry.State.INIT;
			}
			break;
		}
	}

	// Token: 0x06002C60 RID: 11360 RVA: 0x0010C794 File Offset: 0x0010A994
	public override bool IsEnd()
	{
		return this.m_state == ErrorHandleAskGiveUpRetry.State.END;
	}

	// Token: 0x06002C61 RID: 11361 RVA: 0x0010C7A8 File Offset: 0x0010A9A8
	public override void EndErrorHandle()
	{
		NetworkErrorWindow.Close();
		if (this.m_isRetry && this.m_retryProcess != null)
		{
			this.m_retryProcess.Retry();
		}
	}

	// Token: 0x06002C62 RID: 11362 RVA: 0x0010C7D4 File Offset: 0x0010A9D4
	private void OnFinishedCloseAnimCallback()
	{
		if (this.m_state == ErrorHandleAskGiveUpRetry.State.ASK_GIVEUP && NetworkErrorWindow.IsYesButtonPressed)
		{
			if (this.m_callbackObject != null)
			{
				this.m_callbackObject.SendMessage(this.m_callbackFuncName, this.m_msg, SendMessageOptions.DontRequireReceiver);
			}
			this.m_isRetry = true;
			this.m_state = ErrorHandleAskGiveUpRetry.State.END;
		}
	}

	// Token: 0x04002948 RID: 10568
	private ErrorHandleAskGiveUpRetry.State m_state = ErrorHandleAskGiveUpRetry.State.NONE;

	// Token: 0x04002949 RID: 10569
	private ServerRetryProcess m_retryProcess;

	// Token: 0x0400294A RID: 10570
	private GameObject m_callbackObject;

	// Token: 0x0400294B RID: 10571
	private string m_callbackFuncName;

	// Token: 0x0400294C RID: 10572
	private MessageBase m_msg;

	// Token: 0x0400294D RID: 10573
	private bool m_isRetry;

	// Token: 0x0200067F RID: 1663
	private enum State
	{
		// Token: 0x0400294F RID: 10575
		NONE = -1,
		// Token: 0x04002950 RID: 10576
		INIT,
		// Token: 0x04002951 RID: 10577
		ASK_GIVEUP,
		// Token: 0x04002952 RID: 10578
		CONFIRMATION,
		// Token: 0x04002953 RID: 10579
		END,
		// Token: 0x04002954 RID: 10580
		COUNT
	}
}
