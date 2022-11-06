using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001DE RID: 478
public class UIDebugMenuServerUpPoint : UIDebugMenuTask
{
	// Token: 0x06000D19 RID: 3353 RVA: 0x0004C470 File Offset: 0x0004A670
	protected override void OnStartFromTask()
	{
		this.m_backButton = base.gameObject.AddComponent<UIDebugMenuButton>();
		this.m_backButton.Setup(new Rect(200f, 100f, 150f, 50f), "Back", base.gameObject);
		this.m_decideButton = base.gameObject.AddComponent<UIDebugMenuButton>();
		this.m_decideButton.Setup(new Rect(200f, 450f, 150f, 50f), "Decide", base.gameObject);
		for (int i = 0; i < 3; i++)
		{
			this.m_TextFields[i] = base.gameObject.AddComponent<UIDebugMenuTextField>();
			this.m_TextFields[i].Setup(this.RectList[i], this.DefaultTextList[i]);
		}
	}

	// Token: 0x06000D1A RID: 3354 RVA: 0x0004C544 File Offset: 0x0004A744
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
		for (int i = 0; i < 3; i++)
		{
			if (!(this.m_TextFields[i] == null))
			{
				this.m_TextFields[i].SetActive(false);
			}
		}
	}

	// Token: 0x06000D1B RID: 3355 RVA: 0x0004C5C4 File Offset: 0x0004A7C4
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
		for (int i = 0; i < 3; i++)
		{
			if (!(this.m_TextFields[i] == null))
			{
				this.m_TextFields[i].SetActive(true);
			}
		}
	}

	// Token: 0x06000D1C RID: 3356 RVA: 0x0004C644 File Offset: 0x0004A844
	private void OnClicked(string name)
	{
		if (name == "Back")
		{
			base.TransitionToParent();
		}
		else if (name == "Decide")
		{
			for (int i = 0; i < 3; i++)
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
			this.m_upPoint = new NetDebugUpdPointData(int.Parse(this.m_TextFields[0].text), 0, int.Parse(this.m_TextFields[1].text), 0, int.Parse(this.m_TextFields[2].text), 0);
			this.m_upPoint.Request();
		}
	}

	// Token: 0x04000AB6 RID: 2742
	private UIDebugMenuButton m_backButton;

	// Token: 0x04000AB7 RID: 2743
	private UIDebugMenuButton m_decideButton;

	// Token: 0x04000AB8 RID: 2744
	private UIDebugMenuTextField[] m_TextFields = new UIDebugMenuTextField[3];

	// Token: 0x04000AB9 RID: 2745
	private string[] DefaultTextList = new string[]
	{
		"No. of Additional Challenge",
		"No. of Additional Ring",
		"No. of Additional Red Ring"
	};

	// Token: 0x04000ABA RID: 2746
	private List<Rect> RectList = new List<Rect>
	{
		new Rect(200f, 200f, 250f, 50f),
		new Rect(200f, 275f, 250f, 50f),
		new Rect(200f, 350f, 250f, 50f)
	};

	// Token: 0x04000ABB RID: 2747
	private NetDebugUpdPointData m_upPoint;

	// Token: 0x020001DF RID: 479
	private enum TextType
	{
		// Token: 0x04000ABD RID: 2749
		ENERGY,
		// Token: 0x04000ABE RID: 2750
		RING,
		// Token: 0x04000ABF RID: 2751
		RED_STAR_RING,
		// Token: 0x04000AC0 RID: 2752
		NUM
	}
}
