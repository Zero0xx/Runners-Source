using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001D1 RID: 465
public class UIDebugMenuMigration : UIDebugMenuTask
{
	// Token: 0x06000CF7 RID: 3319 RVA: 0x0004AFAC File Offset: 0x000491AC
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

	// Token: 0x06000CF8 RID: 3320 RVA: 0x0004B080 File Offset: 0x00049280
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

	// Token: 0x06000CF9 RID: 3321 RVA: 0x0004B100 File Offset: 0x00049300
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

	// Token: 0x06000CFA RID: 3322 RVA: 0x0004B180 File Offset: 0x00049380
	private void OnClicked(string name)
	{
		if (name == "Back")
		{
			base.TransitionToParent();
		}
		else if (name == "Decide")
		{
			UIDebugMenuTextField uidebugMenuTextField = this.m_TextFields[0];
			if (uidebugMenuTextField == null)
			{
				return;
			}
			ServerInterface serverInterface = GameObjectUtil.FindGameObjectComponent<ServerInterface>("ServerInterface");
			if (serverInterface != null)
			{
				string text = uidebugMenuTextField.text;
				serverInterface.RequestServerMigration(string.Empty, text, base.gameObject);
			}
		}
	}

	// Token: 0x04000A6C RID: 2668
	private UIDebugMenuButton m_backButton;

	// Token: 0x04000A6D RID: 2669
	private UIDebugMenuButton m_decideButton;

	// Token: 0x04000A6E RID: 2670
	private UIDebugMenuTextField[] m_TextFields = new UIDebugMenuTextField[1];

	// Token: 0x04000A6F RID: 2671
	private string[] DefaultTextList = new string[]
	{
		"パスワード"
	};

	// Token: 0x04000A70 RID: 2672
	private List<Rect> RectList = new List<Rect>
	{
		new Rect(200f, 200f, 250f, 50f)
	};

	// Token: 0x04000A71 RID: 2673
	private NetDebugUpdPointData m_upPoint;

	// Token: 0x020001D2 RID: 466
	private enum TextType
	{
		// Token: 0x04000A73 RID: 2675
		PASSWORD,
		// Token: 0x04000A74 RID: 2676
		NUM
	}
}
