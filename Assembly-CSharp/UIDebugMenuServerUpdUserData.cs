using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001E0 RID: 480
public class UIDebugMenuServerUpdUserData : UIDebugMenuTask
{
	// Token: 0x06000D1E RID: 3358 RVA: 0x0004C76C File Offset: 0x0004A96C
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

	// Token: 0x06000D1F RID: 3359 RVA: 0x0004C840 File Offset: 0x0004AA40
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

	// Token: 0x06000D20 RID: 3360 RVA: 0x0004C8C0 File Offset: 0x0004AAC0
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

	// Token: 0x06000D21 RID: 3361 RVA: 0x0004C940 File Offset: 0x0004AB40
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
			this.m_upUserData = new NetDebugUpdUserData(int.Parse(this.m_TextFields[0].text));
			this.m_upUserData.Request();
		}
	}

	// Token: 0x04000AC1 RID: 2753
	private UIDebugMenuButton m_backButton;

	// Token: 0x04000AC2 RID: 2754
	private UIDebugMenuButton m_decideButton;

	// Token: 0x04000AC3 RID: 2755
	private UIDebugMenuTextField[] m_TextFields = new UIDebugMenuTextField[1];

	// Token: 0x04000AC4 RID: 2756
	private string[] DefaultTextList = new string[]
	{
		"追加するランク(マイナス可)"
	};

	// Token: 0x04000AC5 RID: 2757
	private List<Rect> RectList = new List<Rect>
	{
		new Rect(200f, 200f, 250f, 50f)
	};

	// Token: 0x04000AC6 RID: 2758
	private NetDebugUpdUserData m_upUserData;

	// Token: 0x020001E1 RID: 481
	private enum TextType
	{
		// Token: 0x04000AC8 RID: 2760
		RANK,
		// Token: 0x04000AC9 RID: 2761
		NUM
	}
}
