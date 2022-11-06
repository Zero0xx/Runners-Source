using System;
using System.Collections.Generic;
using SaveData;
using UnityEngine;

// Token: 0x020001D7 RID: 471
public class UIDebugMenuServerAddMessage : UIDebugMenuTask
{
	// Token: 0x06000D06 RID: 3334 RVA: 0x0004B93C File Offset: 0x00049B3C
	private string GetGameId()
	{
		return SystemSaveManager.GetGameID();
	}

	// Token: 0x06000D07 RID: 3335 RVA: 0x0004B950 File Offset: 0x00049B50
	protected override void OnStartFromTask()
	{
		this.m_backButton = base.gameObject.AddComponent<UIDebugMenuButton>();
		this.m_backButton.Setup(new Rect(200f, 100f, 150f, 50f), "Back", base.gameObject);
		this.m_decideButton = base.gameObject.AddComponent<UIDebugMenuButton>();
		this.m_decideButton.Setup(new Rect(200f, 450f, 150f, 50f), "Decide", base.gameObject);
		for (int i = 0; i < 2; i++)
		{
			this.m_TextFields[i] = base.gameObject.AddComponent<UIDebugMenuTextField>();
			if (i == 0)
			{
				this.m_TextFields[i].Setup(this.RectList[i], this.DefaultTextList[i], "0");
			}
			else
			{
				this.m_TextFields[i].Setup(this.RectList[i], this.DefaultTextList[i], this.GetGameId());
			}
		}
	}

	// Token: 0x06000D08 RID: 3336 RVA: 0x0004BA5C File Offset: 0x00049C5C
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

	// Token: 0x06000D09 RID: 3337 RVA: 0x0004BADC File Offset: 0x00049CDC
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

	// Token: 0x06000D0A RID: 3338 RVA: 0x0004BB5C File Offset: 0x00049D5C
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
				UIDebugMenuTextField x = this.m_TextFields[i];
				if (x == null)
				{
				}
			}
			this.m_addMsg = new NetDebugAddMessage(this.m_TextFields[0].text, this.m_TextFields[1].text, 2);
			this.m_addMsg.Request();
		}
	}

	// Token: 0x04000A90 RID: 2704
	private UIDebugMenuButton m_backButton;

	// Token: 0x04000A91 RID: 2705
	private UIDebugMenuButton m_decideButton;

	// Token: 0x04000A92 RID: 2706
	private UIDebugMenuTextField[] m_TextFields = new UIDebugMenuTextField[2];

	// Token: 0x04000A93 RID: 2707
	private string[] DefaultTextList = new string[]
	{
		"送信元のGameID",
		"送信先のGameID(エナジーをもらう側)"
	};

	// Token: 0x04000A94 RID: 2708
	private List<Rect> RectList = new List<Rect>
	{
		new Rect(200f, 200f, 250f, 50f),
		new Rect(200f, 275f, 250f, 50f)
	};

	// Token: 0x04000A95 RID: 2709
	private NetDebugAddMessage m_addMsg;

	// Token: 0x020001D8 RID: 472
	private enum TextType
	{
		// Token: 0x04000A97 RID: 2711
		FROM_ID,
		// Token: 0x04000A98 RID: 2712
		TO_ID,
		// Token: 0x04000A99 RID: 2713
		NUM
	}
}
