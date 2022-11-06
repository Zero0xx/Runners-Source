using System;
using Message;
using Text;
using UnityEngine;

// Token: 0x02000686 RID: 1670
public class ErrorHandleMissingPlayer : ErrorHandleBase
{
	// Token: 0x06002C82 RID: 11394 RVA: 0x0010CBF0 File Offset: 0x0010ADF0
	public override void Setup(GameObject callbackObject, string callbackFuncName, MessageBase msg)
	{
	}

	// Token: 0x06002C83 RID: 11395 RVA: 0x0010CBF4 File Offset: 0x0010ADF4
	public override void StartErrorHandle()
	{
		NetworkErrorWindow.Create(new NetworkErrorWindow.CInfo
		{
			buttonType = NetworkErrorWindow.ButtonType.Ok,
			anchor_path = string.Empty,
			caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_caption").text,
			message = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_missing_player").text
		});
	}

	// Token: 0x06002C84 RID: 11396 RVA: 0x0010CC5C File Offset: 0x0010AE5C
	public override void Update()
	{
		if (NetworkErrorWindow.IsOkButtonPressed)
		{
			NetworkErrorWindow.Close();
			HudMenuUtility.GoToTitleScene();
			this.m_isEnd = true;
		}
	}

	// Token: 0x06002C85 RID: 11397 RVA: 0x0010CC7C File Offset: 0x0010AE7C
	public override bool IsEnd()
	{
		return this.m_isEnd;
	}

	// Token: 0x06002C86 RID: 11398 RVA: 0x0010CC84 File Offset: 0x0010AE84
	public override void EndErrorHandle()
	{
	}

	// Token: 0x04002966 RID: 10598
	private bool m_isEnd;
}
