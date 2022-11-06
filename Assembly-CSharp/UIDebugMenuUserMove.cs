using System;
using System.Collections.Generic;
using SaveData;
using UnityEngine;

// Token: 0x020001F0 RID: 496
public class UIDebugMenuUserMove : UIDebugMenuTask
{
	// Token: 0x06000D4B RID: 3403 RVA: 0x0004E43C File Offset: 0x0004C63C
	protected override void OnStartFromTask()
	{
		this.m_backButton = base.gameObject.AddComponent<UIDebugMenuButton>();
		this.m_backButton.Setup(new Rect(200f, 100f, 150f, 50f), "Back", base.gameObject);
		this.m_decideButton = base.gameObject.AddComponent<UIDebugMenuButton>();
		this.m_decideButton.Setup(new Rect(200f, 450f, 150f, 50f), "Decide", base.gameObject);
		for (int i = 0; i < 2; i++)
		{
			this.m_TextFields[i] = base.gameObject.AddComponent<UIDebugMenuTextField>();
			this.m_TextFields[i].Setup(this.RectList[i], this.DefaultTextList[i]);
		}
	}

	// Token: 0x06000D4C RID: 3404 RVA: 0x0004E510 File Offset: 0x0004C710
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
		for (int i = 0; i < 2; i++)
		{
			if (!(this.m_TextFields[i] == null))
			{
				this.m_TextFields[i].SetActive(false);
			}
		}
	}

	// Token: 0x06000D4D RID: 3405 RVA: 0x0004E590 File Offset: 0x0004C790
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
		for (int i = 0; i < 2; i++)
		{
			if (!(this.m_TextFields[i] == null))
			{
				this.m_TextFields[i].SetActive(true);
			}
		}
	}

	// Token: 0x06000D4E RID: 3406 RVA: 0x0004E610 File Offset: 0x0004C810
	private void OnClicked(string name)
	{
		if (name == "Back")
		{
			base.TransitionToParent();
		}
		else if (name == "Decide")
		{
			UIDebugMenuTextField uidebugMenuTextField = this.m_TextFields[0];
			UIDebugMenuTextField uidebugMenuTextField2 = this.m_TextFields[1];
			if (uidebugMenuTextField == null || uidebugMenuTextField2 == null)
			{
				return;
			}
			SystemSaveManager.SetGameID(uidebugMenuTextField.text);
			SystemSaveManager.SetGamePassword(uidebugMenuTextField2.text);
			SystemSaveManager.Save();
		}
	}

	// Token: 0x04000B15 RID: 2837
	private UIDebugMenuButton m_backButton;

	// Token: 0x04000B16 RID: 2838
	private UIDebugMenuButton m_decideButton;

	// Token: 0x04000B17 RID: 2839
	private UIDebugMenuTextField[] m_TextFields = new UIDebugMenuTextField[2];

	// Token: 0x04000B18 RID: 2840
	private string[] DefaultTextList = new string[]
	{
		"Login ID",
		"パスワード"
	};

	// Token: 0x04000B19 RID: 2841
	private List<Rect> RectList = new List<Rect>
	{
		new Rect(200f, 200f, 250f, 50f),
		new Rect(200f, 270f, 250f, 50f)
	};

	// Token: 0x04000B1A RID: 2842
	private NetDebugUpdPointData m_upPoint;

	// Token: 0x020001F1 RID: 497
	private enum TextType
	{
		// Token: 0x04000B1C RID: 2844
		ID,
		// Token: 0x04000B1D RID: 2845
		PASSWORD,
		// Token: 0x04000B1E RID: 2846
		NUM
	}
}
