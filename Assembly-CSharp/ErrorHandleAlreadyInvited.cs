using System;
using Message;
using Text;
using UnityEngine;

// Token: 0x0200067D RID: 1661
public class ErrorHandleAlreadyInvited : ErrorHandleBase
{
	// Token: 0x06002C56 RID: 11350 RVA: 0x0010C3D4 File Offset: 0x0010A5D4
	public override void Setup(GameObject callbackObject, string callbackFuncName, MessageBase msg)
	{
	}

	// Token: 0x06002C57 RID: 11351 RVA: 0x0010C3D8 File Offset: 0x0010A5D8
	public override void StartErrorHandle()
	{
		string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Option", "accepted_invite_caption").text;
		string text2 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Option", "accepted_invite_text").text;
		NetworkErrorWindow.Create(new NetworkErrorWindow.CInfo
		{
			buttonType = NetworkErrorWindow.ButtonType.Ok,
			anchor_path = string.Empty,
			caption = text,
			message = text2
		});
		this.m_isEnd = false;
	}

	// Token: 0x06002C58 RID: 11352 RVA: 0x0010C44C File Offset: 0x0010A64C
	public override void Update()
	{
		if (NetworkErrorWindow.IsOkButtonPressed)
		{
			NetworkErrorWindow.Close();
			this.m_isEnd = true;
		}
	}

	// Token: 0x06002C59 RID: 11353 RVA: 0x0010C468 File Offset: 0x0010A668
	public override bool IsEnd()
	{
		return this.m_isEnd;
	}

	// Token: 0x06002C5A RID: 11354 RVA: 0x0010C470 File Offset: 0x0010A670
	public override void EndErrorHandle()
	{
	}

	// Token: 0x04002947 RID: 10567
	private bool m_isEnd;
}
