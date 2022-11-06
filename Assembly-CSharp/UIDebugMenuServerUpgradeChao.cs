using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001E2 RID: 482
public class UIDebugMenuServerUpgradeChao : UIDebugMenuTask
{
	// Token: 0x06000D23 RID: 3363 RVA: 0x0004CA68 File Offset: 0x0004AC68
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

	// Token: 0x06000D24 RID: 3364 RVA: 0x0004CB3C File Offset: 0x0004AD3C
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

	// Token: 0x06000D25 RID: 3365 RVA: 0x0004CBBC File Offset: 0x0004ADBC
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

	// Token: 0x06000D26 RID: 3366 RVA: 0x0004CC3C File Offset: 0x0004AE3C
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
			this.m_upgradeChao = new NetDebugUpgradeChao(int.Parse(this.m_TextFields[0].text), int.Parse(this.m_TextFields[1].text));
			this.m_upgradeChao.Request();
		}
	}

	// Token: 0x04000ACA RID: 2762
	private UIDebugMenuButton m_backButton;

	// Token: 0x04000ACB RID: 2763
	private UIDebugMenuButton m_decideButton;

	// Token: 0x04000ACC RID: 2764
	private UIDebugMenuTextField[] m_TextFields = new UIDebugMenuTextField[2];

	// Token: 0x04000ACD RID: 2765
	private string[] DefaultTextList = new string[]
	{
		"Chao ID",
		"Level"
	};

	// Token: 0x04000ACE RID: 2766
	private List<Rect> RectList = new List<Rect>
	{
		new Rect(200f, 200f, 250f, 50f),
		new Rect(200f, 275f, 250f, 50f)
	};

	// Token: 0x04000ACF RID: 2767
	private NetDebugUpgradeChao m_upgradeChao;

	// Token: 0x020001E3 RID: 483
	private enum TextType
	{
		// Token: 0x04000AD1 RID: 2769
		CHAO_ID,
		// Token: 0x04000AD2 RID: 2770
		LEVEL,
		// Token: 0x04000AD3 RID: 2771
		NUM
	}
}
