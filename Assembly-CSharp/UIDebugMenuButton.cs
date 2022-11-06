using System;
using UnityEngine;

// Token: 0x020001F5 RID: 501
public class UIDebugMenuButton : MonoBehaviour
{
	// Token: 0x06000D5D RID: 3421 RVA: 0x0004EEEC File Offset: 0x0004D0EC
	public void Setup(Rect rect, string name, GameObject callbackObject)
	{
		this.m_rect = rect;
		this.m_name = name;
		this.m_callbackObject = callbackObject;
		this.m_isActive = false;
	}

	// Token: 0x06000D5E RID: 3422 RVA: 0x0004EF0C File Offset: 0x0004D10C
	public void SetActive(bool flag)
	{
		this.m_isActive = flag;
	}

	// Token: 0x06000D5F RID: 3423 RVA: 0x0004EF18 File Offset: 0x0004D118
	private void OnGUI()
	{
		if (!this.m_isActive)
		{
			return;
		}
		if (this.m_name == null)
		{
			return;
		}
		if (this.m_callbackObject == null)
		{
			return;
		}
		if (GUI.Button(this.m_rect, this.m_name))
		{
			this.m_callbackObject.SendMessage("OnClicked", this.m_name);
		}
	}

	// Token: 0x04000B38 RID: 2872
	private Rect m_rect;

	// Token: 0x04000B39 RID: 2873
	private string m_name;

	// Token: 0x04000B3A RID: 2874
	private GameObject m_callbackObject;

	// Token: 0x04000B3B RID: 2875
	private bool m_isActive;
}
