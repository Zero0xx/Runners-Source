using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001DC RID: 476
public class UIDebugMenuServerGetSpecialItem : UIDebugMenuTask
{
	// Token: 0x06000D14 RID: 3348 RVA: 0x0004C138 File Offset: 0x0004A338
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

	// Token: 0x06000D15 RID: 3349 RVA: 0x0004C20C File Offset: 0x0004A40C
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

	// Token: 0x06000D16 RID: 3350 RVA: 0x0004C28C File Offset: 0x0004A48C
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

	// Token: 0x06000D17 RID: 3351 RVA: 0x0004C30C File Offset: 0x0004A50C
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
			this.m_getSpecialItem = new NetDebugGetSpecialItem(int.Parse(this.m_TextFields[0].text), int.Parse(this.m_TextFields[1].text));
			this.m_getSpecialItem.Request();
		}
	}

	// Token: 0x04000AAC RID: 2732
	private UIDebugMenuButton m_backButton;

	// Token: 0x04000AAD RID: 2733
	private UIDebugMenuButton m_decideButton;

	// Token: 0x04000AAE RID: 2734
	private UIDebugMenuTextField[] m_TextFields = new UIDebugMenuTextField[2];

	// Token: 0x04000AAF RID: 2735
	private string[] DefaultTextList = new string[]
	{
		"Item ID",
		"Quantity"
	};

	// Token: 0x04000AB0 RID: 2736
	private List<Rect> RectList = new List<Rect>
	{
		new Rect(200f, 200f, 250f, 50f),
		new Rect(200f, 275f, 250f, 50f)
	};

	// Token: 0x04000AB1 RID: 2737
	private NetDebugGetSpecialItem m_getSpecialItem;

	// Token: 0x020001DD RID: 477
	private enum TextType
	{
		// Token: 0x04000AB3 RID: 2739
		ITEM_ID,
		// Token: 0x04000AB4 RID: 2740
		ITEM_COUNT,
		// Token: 0x04000AB5 RID: 2741
		NUM
	}
}
