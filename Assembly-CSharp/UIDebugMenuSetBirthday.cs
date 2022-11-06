using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001E6 RID: 486
public class UIDebugMenuSetBirthday : UIDebugMenuTask
{
	// Token: 0x06000D2D RID: 3373 RVA: 0x0004D060 File Offset: 0x0004B260
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

	// Token: 0x06000D2E RID: 3374 RVA: 0x0004D134 File Offset: 0x0004B334
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

	// Token: 0x06000D2F RID: 3375 RVA: 0x0004D1B4 File Offset: 0x0004B3B4
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

	// Token: 0x06000D30 RID: 3376 RVA: 0x0004D234 File Offset: 0x0004B434
	private void OnClicked(string name)
	{
		if (name == "Back")
		{
			base.TransitionToParent();
		}
		else if (name == "Decide")
		{
			string birthday = string.Empty;
			birthday = this.m_TextFields[0].text;
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				loggedInServerInterface.RequestServerSetBirthday(birthday, base.gameObject);
			}
		}
	}

	// Token: 0x04000ADE RID: 2782
	private UIDebugMenuButton m_backButton;

	// Token: 0x04000ADF RID: 2783
	private UIDebugMenuButton m_decideButton;

	// Token: 0x04000AE0 RID: 2784
	private UIDebugMenuTextField[] m_TextFields = new UIDebugMenuTextField[1];

	// Token: 0x04000AE1 RID: 2785
	private string[] DefaultTextList = new string[]
	{
		"誕生日(yyyy-mm-dd)"
	};

	// Token: 0x04000AE2 RID: 2786
	private List<Rect> RectList = new List<Rect>
	{
		new Rect(200f, 200f, 250f, 50f)
	};

	// Token: 0x04000AE3 RID: 2787
	private NetDebugUpdPointData m_upPoint;

	// Token: 0x020001E7 RID: 487
	private enum TextType
	{
		// Token: 0x04000AE5 RID: 2789
		BIRTHDAY,
		// Token: 0x04000AE6 RID: 2790
		NUM
	}
}
