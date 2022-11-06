using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001E4 RID: 484
public class UIDebugMenuServerUpgradeCharacter : UIDebugMenuTask
{
	// Token: 0x06000D28 RID: 3368 RVA: 0x0004CD78 File Offset: 0x0004AF78
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

	// Token: 0x06000D29 RID: 3369 RVA: 0x0004CE4C File Offset: 0x0004B04C
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

	// Token: 0x06000D2A RID: 3370 RVA: 0x0004CECC File Offset: 0x0004B0CC
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

	// Token: 0x06000D2B RID: 3371 RVA: 0x0004CF4C File Offset: 0x0004B14C
	private void OnClicked(string name)
	{
		if (name == "Back")
		{
			base.TransitionToParent();
		}
		else if (name == "Decide")
		{
			for (int i = 0; i < 2; i++)
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
			this.m_upgradeCharacter = new NetDebugUpgradeCharacter(int.Parse(this.m_TextFields[0].text), int.Parse(this.m_TextFields[1].text));
			this.m_upgradeCharacter.Request();
		}
	}

	// Token: 0x04000AD4 RID: 2772
	private UIDebugMenuButton m_backButton;

	// Token: 0x04000AD5 RID: 2773
	private UIDebugMenuButton m_decideButton;

	// Token: 0x04000AD6 RID: 2774
	private UIDebugMenuTextField[] m_TextFields = new UIDebugMenuTextField[2];

	// Token: 0x04000AD7 RID: 2775
	private string[] DefaultTextList = new string[]
	{
		"Character ID",
		"Level"
	};

	// Token: 0x04000AD8 RID: 2776
	private List<Rect> RectList = new List<Rect>
	{
		new Rect(200f, 200f, 250f, 50f),
		new Rect(200f, 275f, 250f, 50f)
	};

	// Token: 0x04000AD9 RID: 2777
	private NetDebugUpgradeCharacter m_upgradeCharacter;

	// Token: 0x020001E5 RID: 485
	private enum TextType
	{
		// Token: 0x04000ADB RID: 2779
		CHARA_ID,
		// Token: 0x04000ADC RID: 2780
		LEVEL,
		// Token: 0x04000ADD RID: 2781
		NUM
	}
}
