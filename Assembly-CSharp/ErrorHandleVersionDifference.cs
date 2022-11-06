using System;
using Message;
using Text;
using UnityEngine;

// Token: 0x0200068C RID: 1676
public class ErrorHandleVersionDifference : ErrorHandleBase
{
	// Token: 0x06002CAC RID: 11436 RVA: 0x0010D400 File Offset: 0x0010B600
	public override void Setup(GameObject callbackObject, string callbackFuncName, MessageBase msg)
	{
	}

	// Token: 0x06002CAD RID: 11437 RVA: 0x0010D404 File Offset: 0x0010B604
	public override void StartErrorHandle()
	{
		NetworkErrorWindow.Create(new NetworkErrorWindow.CInfo
		{
			buttonType = NetworkErrorWindow.ButtonType.Ok,
			anchor_path = string.Empty,
			caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_caption").text,
			message = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_update_version_android").text
		});
	}

	// Token: 0x06002CAE RID: 11438 RVA: 0x0010D46C File Offset: 0x0010B66C
	public override void Update()
	{
		if (NetworkErrorWindow.IsOkButtonPressed)
		{
			NetworkErrorWindow.Close();
			HudMenuUtility.GoToTitleScene();
			string redirectInstallPageUrl = NetBaseUtil.RedirectInstallPageUrl;
			if (!string.IsNullOrEmpty(redirectInstallPageUrl))
			{
				Application.OpenURL(redirectInstallPageUrl);
			}
			this.m_isEnd = true;
		}
	}

	// Token: 0x06002CAF RID: 11439 RVA: 0x0010D4AC File Offset: 0x0010B6AC
	public override bool IsEnd()
	{
		return this.m_isEnd;
	}

	// Token: 0x06002CB0 RID: 11440 RVA: 0x0010D4B4 File Offset: 0x0010B6B4
	public override void EndErrorHandle()
	{
	}

	// Token: 0x04002979 RID: 10617
	private bool m_isEnd;
}
