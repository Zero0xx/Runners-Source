using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001F6 RID: 502
public class UIDebugMenuButtonList : MonoBehaviour
{
	// Token: 0x06000D60 RID: 3424 RVA: 0x0004EF7C File Offset: 0x0004D17C
	public UIDebugMenuButtonList()
	{
		this.m_buttons = new List<UIDebugMenuButton>();
	}

	// Token: 0x06000D61 RID: 3425 RVA: 0x0004EF90 File Offset: 0x0004D190
	public void Add(List<Rect> rect, List<string> name, GameObject callbackObject)
	{
		if (rect.Count != name.Count)
		{
			return;
		}
		int count = rect.Count;
		for (int i = 0; i < count; i++)
		{
			UIDebugMenuButton uidebugMenuButton = base.gameObject.AddComponent<UIDebugMenuButton>();
			uidebugMenuButton.Setup(rect[i], name[i], callbackObject);
			this.m_buttons.Add(uidebugMenuButton);
		}
	}

	// Token: 0x06000D62 RID: 3426 RVA: 0x0004EFF8 File Offset: 0x0004D1F8
	public void SetActive(bool flag)
	{
		foreach (UIDebugMenuButton uidebugMenuButton in this.m_buttons)
		{
			if (!(uidebugMenuButton == null))
			{
				uidebugMenuButton.SetActive(flag);
			}
		}
	}

	// Token: 0x04000B3C RID: 2876
	private List<UIDebugMenuButton> m_buttons;
}
