using System;
using UnityEngine;

// Token: 0x020001F8 RID: 504
public class UIDebugMenuTextBox : MonoBehaviour
{
	// Token: 0x170001EB RID: 491
	// (get) Token: 0x06000D73 RID: 3443 RVA: 0x0004F248 File Offset: 0x0004D448
	// (set) Token: 0x06000D74 RID: 3444 RVA: 0x0004F250 File Offset: 0x0004D450
	public string Text
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

	// Token: 0x170001EC RID: 492
	// (get) Token: 0x06000D75 RID: 3445 RVA: 0x0004F25C File Offset: 0x0004D45C
	// (set) Token: 0x06000D76 RID: 3446 RVA: 0x0004F264 File Offset: 0x0004D464
	public Vector2 ScrollScale
	{
		get
		{
			return this.m_scrollScale;
		}
		set
		{
			this.m_scrollScale = value;
		}
	}

	// Token: 0x06000D77 RID: 3447 RVA: 0x0004F270 File Offset: 0x0004D470
	public void Setup(Rect rect, string text)
	{
		this.m_rect = rect;
		this.m_text = text;
		this.m_isActive = false;
	}

	// Token: 0x06000D78 RID: 3448 RVA: 0x0004F288 File Offset: 0x0004D488
	public void SetActive(bool flag)
	{
		this.m_isActive = flag;
	}

	// Token: 0x170001ED RID: 493
	// (get) Token: 0x06000D79 RID: 3449 RVA: 0x0004F294 File Offset: 0x0004D494
	// (set) Token: 0x06000D7A RID: 3450 RVA: 0x0004F29C File Offset: 0x0004D49C
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

	// Token: 0x06000D7B RID: 3451 RVA: 0x0004F2A8 File Offset: 0x0004D4A8
	private void OnGUI()
	{
		if (!this.m_isActive)
		{
			return;
		}
		Rect rect = new Rect(this.m_rect.left, this.m_rect.top, this.m_rect.width * this.m_scrollScale.x, this.m_rect.height * this.m_scrollScale.y);
		this.m_scrollViewVector = GUI.BeginScrollView(this.m_rect, this.m_scrollViewVector, rect);
		GUI.TextArea(rect, this.m_text);
		GUI.EndScrollView();
	}

	// Token: 0x04000B40 RID: 2880
	private Rect m_rect;

	// Token: 0x04000B41 RID: 2881
	private string m_text;

	// Token: 0x04000B42 RID: 2882
	private bool m_isActive;

	// Token: 0x04000B43 RID: 2883
	private Vector2 m_scrollViewVector = Vector2.zero;

	// Token: 0x04000B44 RID: 2884
	private Vector2 m_scrollScale = new Vector2(1f, 2f);
}
