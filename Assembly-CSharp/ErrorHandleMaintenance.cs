using System;
using Message;
using Text;
using UnityEngine;

// Token: 0x02000685 RID: 1669
public class ErrorHandleMaintenance : ErrorHandleBase
{
	// Token: 0x06002C7C RID: 11388 RVA: 0x0010CAD4 File Offset: 0x0010ACD4
	public override void Setup(GameObject callbackObject, string callbackFuncName, MessageBase msg)
	{
	}

	// Token: 0x06002C7D RID: 11389 RVA: 0x0010CAD8 File Offset: 0x0010ACD8
	public override void StartErrorHandle()
	{
		this.m_isExistMaintenancePage = false;
		NetworkErrorWindow.Create(new NetworkErrorWindow.CInfo
		{
			buttonType = ((!this.m_isExistMaintenancePage) ? NetworkErrorWindow.ButtonType.Ok : NetworkErrorWindow.ButtonType.HomePage),
			anchor_path = string.Empty,
			caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_caption").text,
			message = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_maintenance").text
		});
	}

	// Token: 0x06002C7E RID: 11390 RVA: 0x0010CB58 File Offset: 0x0010AD58
	public override void Update()
	{
		if (this.m_isExistMaintenancePage)
		{
			if (NetworkErrorWindow.IsYesButtonPressed)
			{
				NetworkErrorWindow.Close();
				HudMenuUtility.GoToTitleScene();
				string maintenancePageURL = ErrorHandleMaintenanceUtil.GetMaintenancePageURL();
				Application.OpenURL(maintenancePageURL);
				this.m_isEnd = true;
			}
			else if (NetworkErrorWindow.IsNoButtonPressed)
			{
				NetworkErrorWindow.Close();
				HudMenuUtility.GoToTitleScene();
				this.m_isEnd = true;
			}
		}
		else if (NetworkErrorWindow.IsOkButtonPressed)
		{
			NetworkErrorWindow.Close();
			HudMenuUtility.GoToTitleScene();
			this.m_isEnd = true;
		}
	}

	// Token: 0x06002C7F RID: 11391 RVA: 0x0010CBDC File Offset: 0x0010ADDC
	public override bool IsEnd()
	{
		return this.m_isEnd;
	}

	// Token: 0x06002C80 RID: 11392 RVA: 0x0010CBE4 File Offset: 0x0010ADE4
	public override void EndErrorHandle()
	{
	}

	// Token: 0x04002964 RID: 10596
	private bool m_isEnd;

	// Token: 0x04002965 RID: 10597
	private bool m_isExistMaintenancePage;
}
