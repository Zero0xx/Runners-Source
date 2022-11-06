using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001D3 RID: 467
public class UIDebugMenuRingExchange : UIDebugMenuTask
{
	// Token: 0x06000CFC RID: 3324 RVA: 0x0004B260 File Offset: 0x00049460
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

	// Token: 0x06000CFD RID: 3325 RVA: 0x0004B334 File Offset: 0x00049534
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

	// Token: 0x06000CFE RID: 3326 RVA: 0x0004B3B4 File Offset: 0x000495B4
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

	// Token: 0x06000CFF RID: 3327 RVA: 0x0004B434 File Offset: 0x00049634
	private void OnClicked(string name)
	{
		if (name == "Back")
		{
			base.TransitionToParent();
		}
		else if (name == "Decide")
		{
			string s = string.Empty;
			s = this.m_TextFields[0].text;
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				int itemId = int.Parse(s);
				loggedInServerInterface.RequestServerRingExchange(itemId, 1, base.gameObject);
			}
		}
	}

	// Token: 0x04000A75 RID: 2677
	private UIDebugMenuButton m_backButton;

	// Token: 0x04000A76 RID: 2678
	private UIDebugMenuButton m_decideButton;

	// Token: 0x04000A77 RID: 2679
	private UIDebugMenuTextField[] m_TextFields = new UIDebugMenuTextField[1];

	// Token: 0x04000A78 RID: 2680
	private string[] DefaultTextList = new string[]
	{
		"ItemId"
	};

	// Token: 0x04000A79 RID: 2681
	private List<Rect> RectList = new List<Rect>
	{
		new Rect(200f, 200f, 250f, 50f)
	};

	// Token: 0x04000A7A RID: 2682
	private NetDebugUpdPointData m_upPoint;

	// Token: 0x020001D4 RID: 468
	private enum TextType
	{
		// Token: 0x04000A7C RID: 2684
		ITEM_ID,
		// Token: 0x04000A7D RID: 2685
		NUM
	}
}
