using System;
using Message;
using Text;
using UnityEngine;

// Token: 0x02000683 RID: 1667
public class ErrorHandleInvalidReciept : ErrorHandleBase
{
	// Token: 0x06002C73 RID: 11379 RVA: 0x0010C9AC File Offset: 0x0010ABAC
	public override void Setup(GameObject callbackObject, string callbackFuncName, MessageBase msg)
	{
		this.m_callbackObject = callbackObject;
		this.m_callbackFuncName = callbackFuncName;
		this.m_msg = msg;
	}

	// Token: 0x06002C74 RID: 11380 RVA: 0x0010C9C4 File Offset: 0x0010ABC4
	public override void StartErrorHandle()
	{
		NetworkErrorWindow.Create(new NetworkErrorWindow.CInfo
		{
			buttonType = NetworkErrorWindow.ButtonType.Ok,
			anchor_path = string.Empty,
			caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_caption").text,
			message = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_invalid_receipt").text
		});
	}

	// Token: 0x06002C75 RID: 11381 RVA: 0x0010CA2C File Offset: 0x0010AC2C
	public override void Update()
	{
		if (NetworkErrorWindow.IsOkButtonPressed)
		{
			NetworkErrorWindow.Close();
			if (this.m_callbackObject != null)
			{
				this.m_callbackObject.SendMessage(this.m_callbackFuncName, this.m_msg, SendMessageOptions.DontRequireReceiver);
			}
			this.m_isEnd = true;
		}
	}

	// Token: 0x06002C76 RID: 11382 RVA: 0x0010CA7C File Offset: 0x0010AC7C
	public override bool IsEnd()
	{
		return this.m_isEnd;
	}

	// Token: 0x06002C77 RID: 11383 RVA: 0x0010CA84 File Offset: 0x0010AC84
	public override void EndErrorHandle()
	{
	}

	// Token: 0x04002960 RID: 10592
	private bool m_isEnd;

	// Token: 0x04002961 RID: 10593
	private GameObject m_callbackObject;

	// Token: 0x04002962 RID: 10594
	private string m_callbackFuncName;

	// Token: 0x04002963 RID: 10595
	private MessageBase m_msg;
}
