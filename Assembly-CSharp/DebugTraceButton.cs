using System;
using UnityEngine;

// Token: 0x020001B2 RID: 434
public class DebugTraceButton : MonoBehaviour
{
	// Token: 0x06000C78 RID: 3192 RVA: 0x000471E0 File Offset: 0x000453E0
	public void Setup(string name, DebugTraceButton.ButtonClickedCallback callback, Vector2 position)
	{
		this.Setup(name, callback, position, DebugTraceButton.s_DefaultSize);
	}

	// Token: 0x06000C79 RID: 3193 RVA: 0x000471F0 File Offset: 0x000453F0
	public void Setup(string name, DebugTraceButton.ButtonClickedCallback callback, Vector2 position, Vector2 size)
	{
		this.m_name = name;
		this.m_callback = callback;
		this.m_position = position;
		this.m_size = size;
		this.m_rect = new Rect(this.m_position.x, this.m_position.y, this.m_size.x, this.m_size.y);
	}

	// Token: 0x06000C7A RID: 3194 RVA: 0x00047254 File Offset: 0x00045454
	public void SetActive(bool isActive)
	{
		this.m_isActive = isActive;
	}

	// Token: 0x06000C7B RID: 3195 RVA: 0x00047260 File Offset: 0x00045460
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
		if (GUI.Button(this.m_rect, this.m_name))
		{
			if (this.m_callback == null)
			{
				return;
			}
			this.m_callback(this.m_name);
		}
	}

	// Token: 0x06000C7C RID: 3196 RVA: 0x000472B8 File Offset: 0x000454B8
	private void Start()
	{
	}

	// Token: 0x06000C7D RID: 3197 RVA: 0x000472BC File Offset: 0x000454BC
	private void Update()
	{
	}

	// Token: 0x040009B8 RID: 2488
	private DebugTraceButton.ButtonClickedCallback m_callback;

	// Token: 0x040009B9 RID: 2489
	private string m_name;

	// Token: 0x040009BA RID: 2490
	private static Vector2 s_DefaultSize = new Vector2(200f, 50f);

	// Token: 0x040009BB RID: 2491
	private Vector2 m_position;

	// Token: 0x040009BC RID: 2492
	private Vector2 m_size;

	// Token: 0x040009BD RID: 2493
	private Rect m_rect;

	// Token: 0x040009BE RID: 2494
	private bool m_isActive;

	// Token: 0x02000A75 RID: 2677
	// (Invoke) Token: 0x0600480E RID: 18446
	public delegate void ButtonClickedCallback(string name);
}
