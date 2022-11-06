using System;
using System.Collections.Generic;
using Message;
using Text;
using UnityEngine;

// Token: 0x0200068A RID: 1674
public class ErrorHandleUnExpectedError : ErrorHandleBase
{
	// Token: 0x06002CA1 RID: 11425 RVA: 0x0010D2A0 File Offset: 0x0010B4A0
	public override void Setup(GameObject callbackObject, string callbackFuncName, MessageBase msg)
	{
		this.m_msg = msg;
	}

	// Token: 0x06002CA2 RID: 11426 RVA: 0x0010D2AC File Offset: 0x0010B4AC
	public override void StartErrorHandle()
	{
		string caption = string.Empty;
		string text = string.Empty;
		MsgServerConnctFailed msgServerConnctFailed = this.m_msg as MsgServerConnctFailed;
		if (msgServerConnctFailed != null)
		{
			ErrorHandleUnExpectedError.TextInfo textInfo = null;
			if (ErrorHandleUnExpectedError.ErrorTextPair.TryGetValue(msgServerConnctFailed.m_status, out textInfo))
			{
				caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", textInfo.CaptionId).text;
				text = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", textInfo.MessageId).text;
			}
			else
			{
				caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_caption").text;
				text = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_reset").text;
				string str = text;
				int status = (int)msgServerConnctFailed.m_status;
				text = str + status.ToString();
			}
		}
		NetworkErrorWindow.Create(new NetworkErrorWindow.CInfo
		{
			buttonType = NetworkErrorWindow.ButtonType.Ok,
			anchor_path = string.Empty,
			caption = caption,
			message = text
		});
	}

	// Token: 0x06002CA3 RID: 11427 RVA: 0x0010D39C File Offset: 0x0010B59C
	public override void Update()
	{
		if (NetworkErrorWindow.IsOkButtonPressed)
		{
			NetworkErrorWindow.Close();
			HudMenuUtility.GoToTitleScene();
			this.m_isEnd = true;
		}
	}

	// Token: 0x06002CA4 RID: 11428 RVA: 0x0010D3BC File Offset: 0x0010B5BC
	public override bool IsEnd()
	{
		return this.m_isEnd;
	}

	// Token: 0x06002CA5 RID: 11429 RVA: 0x0010D3C4 File Offset: 0x0010B5C4
	public override void EndErrorHandle()
	{
	}

	// Token: 0x04002974 RID: 10612
	private bool m_isEnd;

	// Token: 0x04002975 RID: 10613
	private MessageBase m_msg;

	// Token: 0x04002976 RID: 10614
	private static readonly Dictionary<ServerInterface.StatusCode, ErrorHandleUnExpectedError.TextInfo> ErrorTextPair = new Dictionary<ServerInterface.StatusCode, ErrorHandleUnExpectedError.TextInfo>
	{
		{
			ServerInterface.StatusCode.PassWordError,
			new ErrorHandleUnExpectedError.TextInfo("ui_Lbl_caption_local", "ui_Lbl_password_error")
		}
	};

	// Token: 0x0200068B RID: 1675
	private class TextInfo
	{
		// Token: 0x06002CA6 RID: 11430 RVA: 0x0010D3C8 File Offset: 0x0010B5C8
		public TextInfo(string captionId, string messageId)
		{
			this.m_captionId = captionId;
			this.m_messageId = messageId;
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06002CA7 RID: 11431 RVA: 0x0010D3E0 File Offset: 0x0010B5E0
		// (set) Token: 0x06002CA8 RID: 11432 RVA: 0x0010D3E8 File Offset: 0x0010B5E8
		public string CaptionId
		{
			get
			{
				return this.m_captionId;
			}
			private set
			{
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06002CA9 RID: 11433 RVA: 0x0010D3EC File Offset: 0x0010B5EC
		// (set) Token: 0x06002CAA RID: 11434 RVA: 0x0010D3F4 File Offset: 0x0010B5F4
		public string MessageId
		{
			get
			{
				return this.m_messageId;
			}
			private set
			{
			}
		}

		// Token: 0x04002977 RID: 10615
		private string m_captionId;

		// Token: 0x04002978 RID: 10616
		private string m_messageId;
	}
}
