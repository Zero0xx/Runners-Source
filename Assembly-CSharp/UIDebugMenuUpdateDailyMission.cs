using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001E8 RID: 488
public class UIDebugMenuUpdateDailyMission : UIDebugMenuTask
{
	// Token: 0x06000D32 RID: 3378 RVA: 0x0004D300 File Offset: 0x0004B500
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

	// Token: 0x06000D33 RID: 3379 RVA: 0x0004D3D4 File Offset: 0x0004B5D4
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

	// Token: 0x06000D34 RID: 3380 RVA: 0x0004D454 File Offset: 0x0004B654
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

	// Token: 0x06000D35 RID: 3381 RVA: 0x0004D4D4 File Offset: 0x0004B6D4
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
			this.m_updateDailyMission = new NetDebugUpdDailyMission(int.Parse(this.m_TextFields[0].text));
			this.m_updateDailyMission.Request();
		}
	}

	// Token: 0x04000AE7 RID: 2791
	private UIDebugMenuButton m_backButton;

	// Token: 0x04000AE8 RID: 2792
	private UIDebugMenuButton m_decideButton;

	// Token: 0x04000AE9 RID: 2793
	private UIDebugMenuTextField[] m_TextFields = new UIDebugMenuTextField[1];

	// Token: 0x04000AEA RID: 2794
	private string[] DefaultTextList = new string[]
	{
		"Mission ID"
	};

	// Token: 0x04000AEB RID: 2795
	private List<Rect> RectList = new List<Rect>
	{
		new Rect(200f, 200f, 250f, 50f)
	};

	// Token: 0x04000AEC RID: 2796
	private NetDebugUpdDailyMission m_updateDailyMission;

	// Token: 0x020001E9 RID: 489
	private enum TextType
	{
		// Token: 0x04000AEE RID: 2798
		MISSION_ID,
		// Token: 0x04000AEF RID: 2799
		NUM
	}
}
