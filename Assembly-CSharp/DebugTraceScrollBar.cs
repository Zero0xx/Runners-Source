using System;
using App;
using UnityEngine;

// Token: 0x020001B4 RID: 436
public class DebugTraceScrollBar : MonoBehaviour
{
	// Token: 0x06000C8C RID: 3212 RVA: 0x000474F8 File Offset: 0x000456F8
	public void SetActive(bool isActive)
	{
		this.m_isActive = isActive;
	}

	// Token: 0x06000C8D RID: 3213 RVA: 0x00047504 File Offset: 0x00045704
	public void SetUp(string name, DebugTraceScrollBar.ValueChangedCallback callback, Vector2 position)
	{
		this.m_name = name;
		this.m_callback = callback;
		this.m_rect = new Rect(position.x, position.y, DebugTraceScrollBar.s_Size.x, DebugTraceScrollBar.s_Size.y);
	}

	// Token: 0x06000C8E RID: 3214 RVA: 0x00047554 File Offset: 0x00045754
	private void OnGUI()
	{
		if (!this.m_isActive)
		{
			return;
		}
		GUI.Label(new Rect(this.m_rect.left, this.m_rect.top - 20f, this.m_rect.width, this.m_rect.height), this.m_name);
		this.m_scrollValue = GUI.HorizontalScrollbar(this.m_rect, this.m_scrollValue, 1f, DebugTraceScrollBar.MinValue, DebugTraceScrollBar.MaxValue);
		if (!App.Math.NearEqual(this.m_scrollValue, this.m_scrollValuePrev, 1E-06f))
		{
			this.m_scrollValuePrev = this.m_scrollValue;
			if (this.m_callback != null)
			{
				this.m_callback(this.m_name, this.m_scrollValue);
			}
		}
	}

	// Token: 0x040009C8 RID: 2504
	private DebugTraceScrollBar.ValueChangedCallback m_callback;

	// Token: 0x040009C9 RID: 2505
	public static readonly float MaxValue = 100f;

	// Token: 0x040009CA RID: 2506
	public static readonly float MinValue = 1f;

	// Token: 0x040009CB RID: 2507
	private string m_name;

	// Token: 0x040009CC RID: 2508
	private bool m_isActive;

	// Token: 0x040009CD RID: 2509
	private float m_scrollValue;

	// Token: 0x040009CE RID: 2510
	private float m_scrollValuePrev;

	// Token: 0x040009CF RID: 2511
	private static readonly Vector2 s_Size = new Vector2(200f, 50f);

	// Token: 0x040009D0 RID: 2512
	private Rect m_rect;

	// Token: 0x02000A76 RID: 2678
	// (Invoke) Token: 0x06004812 RID: 18450
	public delegate void ValueChangedCallback(string name, float value);
}
