using System;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x020001CF RID: 463
public class UIDebugMenuGetMigrationPassword : UIDebugMenuTask
{
	// Token: 0x06000CF1 RID: 3313 RVA: 0x0004ACFC File Offset: 0x00048EFC
	protected override void OnStartFromTask()
	{
		this.m_backButton = base.gameObject.AddComponent<UIDebugMenuButton>();
		this.m_backButton.Setup(new Rect(200f, 100f, 150f, 50f), "Back", base.gameObject);
		this.m_decideButton = base.gameObject.AddComponent<UIDebugMenuButton>();
		this.m_decideButton.Setup(new Rect(200f, 450f, 150f, 50f), "Decide", base.gameObject);
		for (int i = 0; i < 1; i++)
		{
			this.m_TextFields[i] = base.gameObject.AddComponent<UIDebugMenuTextField>();
			this.m_TextFields[i].Setup(this.RectList[i], this.DefaultTextList[i]);
		}
	}

	// Token: 0x06000CF2 RID: 3314 RVA: 0x0004ADD0 File Offset: 0x00048FD0
	protected override void OnTransitionTo()
	{
		if (this.m_backButton != null)
		{
			this.m_backButton.SetActive(false);
		}
		if (this.m_decideButton != null)
		{
			this.m_decideButton.SetActive(false);
		}
		for (int i = 0; i < 1; i++)
		{
			if (!(this.m_TextFields[i] == null))
			{
				this.m_TextFields[i].SetActive(false);
			}
		}
	}

	// Token: 0x06000CF3 RID: 3315 RVA: 0x0004AE50 File Offset: 0x00049050
	protected override void OnTransitionFrom()
	{
		if (this.m_backButton != null)
		{
			this.m_backButton.SetActive(true);
		}
		if (this.m_decideButton != null)
		{
			this.m_decideButton.SetActive(true);
		}
		for (int i = 0; i < 1; i++)
		{
			if (!(this.m_TextFields[i] == null))
			{
				this.m_TextFields[i].SetActive(true);
			}
		}
	}

	// Token: 0x06000CF4 RID: 3316 RVA: 0x0004AED0 File Offset: 0x000490D0
	private void OnClicked(string name)
	{
		if (name == "Back")
		{
			base.TransitionToParent();
		}
		else if (name == "Decide")
		{
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				loggedInServerInterface.RequestServerGetMigrationPassword(null, base.gameObject);
			}
		}
	}

	// Token: 0x06000CF5 RID: 3317 RVA: 0x0004AF28 File Offset: 0x00049128
	private void ServerGetMigrationPassword_Succeeded(MsgGetMigrationPasswordSucceed msg)
	{
		UIDebugMenuTextField uidebugMenuTextField = this.m_TextFields[0];
		uidebugMenuTextField.text = msg.m_migrationPassword;
	}

	// Token: 0x04000A63 RID: 2659
	private UIDebugMenuButton m_backButton;

	// Token: 0x04000A64 RID: 2660
	private UIDebugMenuButton m_decideButton;

	// Token: 0x04000A65 RID: 2661
	private UIDebugMenuTextField[] m_TextFields = new UIDebugMenuTextField[1];

	// Token: 0x04000A66 RID: 2662
	private string[] DefaultTextList = new string[]
	{
		"Decideを押すと、ここにパスワードが入ります。"
	};

	// Token: 0x04000A67 RID: 2663
	private List<Rect> RectList = new List<Rect>
	{
		new Rect(200f, 200f, 450f, 50f)
	};

	// Token: 0x04000A68 RID: 2664
	private NetDebugUpdPointData m_upPoint;

	// Token: 0x020001D0 RID: 464
	private enum TextType
	{
		// Token: 0x04000A6A RID: 2666
		PASSWORD,
		// Token: 0x04000A6B RID: 2667
		NUM
	}
}
