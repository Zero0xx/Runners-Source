using System;
using UnityEngine;

// Token: 0x020001F9 RID: 505
public class UIDebugMenuTextField : MonoBehaviour
{
	// Token: 0x06000D7D RID: 3453 RVA: 0x0004F340 File Offset: 0x0004D540
	public void Setup(Rect rect, string titleText)
	{
		this.Setup(rect, titleText, "0");
	}

	// Token: 0x06000D7E RID: 3454 RVA: 0x0004F350 File Offset: 0x0004D550
	public void Setup(Rect rect, string titleText, string fieldText)
	{
		this.m_rect = rect;
		this.m_title = titleText;
		this.m_text = fieldText;
		this.m_isActive = false;
	}

	// Token: 0x06000D7F RID: 3455 RVA: 0x0004F370 File Offset: 0x0004D570
	public void SetActive(bool flag)
	{
		this.m_isActive = flag;
	}

	// Token: 0x170001EE RID: 494
	// (get) Token: 0x06000D80 RID: 3456 RVA: 0x0004F37C File Offset: 0x0004D57C
	// (set) Token: 0x06000D81 RID: 3457 RVA: 0x0004F384 File Offset: 0x0004D584
	public string text
	{
		get
		{
			return this.m_text;
		}
		set
		{
			this.m_text = value;
		}
	}

	// Token: 0x06000D82 RID: 3458 RVA: 0x0004F390 File Offset: 0x0004D590
	private void OnGUI()
	{
		if (!this.m_isActive)
		{
			return;
		}
		GUI.TextArea(new Rect(this.m_rect.xMin, this.m_rect.yMin - 20f, this.m_rect.width, 20f), this.m_title);
		this.m_text = GUI.TextField(this.m_rect, this.m_text);
	}

	// Token: 0x04000B45 RID: 2885
	private Rect m_rect;

	// Token: 0x04000B46 RID: 2886
	private string m_title;

	// Token: 0x04000B47 RID: 2887
	private string m_text;

	// Token: 0x04000B48 RID: 2888
	private bool m_isActive;
}
