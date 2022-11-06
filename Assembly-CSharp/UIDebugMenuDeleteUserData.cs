using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001CB RID: 459
public class UIDebugMenuDeleteUserData : UIDebugMenuTask
{
	// Token: 0x06000CE3 RID: 3299 RVA: 0x0004A35C File Offset: 0x0004855C
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

	// Token: 0x06000CE4 RID: 3300 RVA: 0x0004A430 File Offset: 0x00048630
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

	// Token: 0x06000CE5 RID: 3301 RVA: 0x0004A4B0 File Offset: 0x000486B0
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

	// Token: 0x06000CE6 RID: 3302 RVA: 0x0004A530 File Offset: 0x00048730
	private void OnClicked(string name)
	{
		if (name == "Back")
		{
			base.TransitionToParent();
		}
		else if (name == "Decide")
		{
			for (int i = 0; i < 1; i++)
			{
				UIDebugMenuTextField uidebugMenuTextField = this.m_TextFields[i];
				if (!(uidebugMenuTextField == null))
				{
					int num;
					if (!int.TryParse(uidebugMenuTextField.text, out num))
					{
						return;
					}
				}
			}
			this.m_DeleteUserData = new NetDebugDeleteUserData(int.Parse(this.m_TextFields[0].text));
			this.m_DeleteUserData.Request();
		}
	}

	// Token: 0x04000A4F RID: 2639
	private UIDebugMenuButton m_backButton;

	// Token: 0x04000A50 RID: 2640
	private UIDebugMenuButton m_decideButton;

	// Token: 0x04000A51 RID: 2641
	private UIDebugMenuTextField[] m_TextFields = new UIDebugMenuTextField[1];

	// Token: 0x04000A52 RID: 2642
	private string[] DefaultTextList = new string[]
	{
		"ユーザーID"
	};

	// Token: 0x04000A53 RID: 2643
	private List<Rect> RectList = new List<Rect>
	{
		new Rect(200f, 200f, 250f, 50f)
	};

	// Token: 0x04000A54 RID: 2644
	private NetDebugDeleteUserData m_DeleteUserData;

	// Token: 0x020001CC RID: 460
	private enum TextType
	{
		// Token: 0x04000A56 RID: 2646
		USER_ID,
		// Token: 0x04000A57 RID: 2647
		NUM
	}
}
