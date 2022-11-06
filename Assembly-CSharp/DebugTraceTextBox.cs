using System;
using UnityEngine;

// Token: 0x020001B3 RID: 435
public class DebugTraceTextBox : MonoBehaviour
{
	// Token: 0x06000C80 RID: 3200 RVA: 0x0004730C File Offset: 0x0004550C
	public void Setup(Vector2 position)
	{
		this.Setup(position, DebugTraceTextBox.s_DefaultSize);
	}

	// Token: 0x06000C81 RID: 3201 RVA: 0x0004731C File Offset: 0x0004551C
	public void Setup(Vector2 position, Vector2 size)
	{
		this.m_position = position;
		this.SetSize(size);
	}

	// Token: 0x06000C82 RID: 3202 RVA: 0x0004732C File Offset: 0x0004552C
	public void SetActive(bool isActive)
	{
		this.m_isActive = isActive;
	}

	// Token: 0x06000C83 RID: 3203 RVA: 0x00047338 File Offset: 0x00045538
	public void SetText(string text)
	{
		this.m_text = text;
	}

	// Token: 0x06000C84 RID: 3204 RVA: 0x00047344 File Offset: 0x00045544
	public void SetSize(Vector2 size)
	{
		this.m_size = size;
		this.m_rect = new Rect(this.m_position.x, this.m_position.y, this.m_size.x * this.m_sizeScale, this.m_size.y * this.m_sizeScale);
	}

	// Token: 0x06000C85 RID: 3205 RVA: 0x000473A0 File Offset: 0x000455A0
	public void SetSizeScale(float sizeScale)
	{
		this.m_sizeScale = sizeScale;
		this.m_rect = new Rect(this.m_position.x, this.m_position.y, this.m_size.x * this.m_sizeScale, this.m_size.y * this.m_sizeScale);
	}

	// Token: 0x06000C86 RID: 3206 RVA: 0x000473FC File Offset: 0x000455FC
	public void SetScrollScale(Vector2 scale)
	{
		this.m_scrollViewVector = Vector2.zero;
		this.m_scrollScale = scale;
	}

	// Token: 0x06000C87 RID: 3207 RVA: 0x00047410 File Offset: 0x00045610
	private void OnGUI()
	{
		if (!this.m_isActive)
		{
			return;
		}
		Rect rect = new Rect(this.m_rect.left, this.m_rect.top, this.m_rect.width, this.m_rect.height * this.m_scrollScale.y);
		this.m_scrollViewVector = GUI.BeginScrollView(this.m_rect, this.m_scrollViewVector, rect);
		int startIndex = Mathf.Max(this.m_text.Length - 10000, 0);
		GUI.TextArea(rect, this.m_text.Substring(startIndex));
		GUI.EndScrollView();
	}

	// Token: 0x06000C88 RID: 3208 RVA: 0x000474B0 File Offset: 0x000456B0
	private void Start()
	{
	}

	// Token: 0x06000C89 RID: 3209 RVA: 0x000474B4 File Offset: 0x000456B4
	private void Update()
	{
	}

	// Token: 0x040009BF RID: 2495
	private string m_text;

	// Token: 0x040009C0 RID: 2496
	private Vector2 m_scrollViewVector = Vector2.zero;

	// Token: 0x040009C1 RID: 2497
	private Vector2 m_scrollScale = new Vector2(1f, 1f);

	// Token: 0x040009C2 RID: 2498
	private bool m_isActive;

	// Token: 0x040009C3 RID: 2499
	private Vector2 m_position;

	// Token: 0x040009C4 RID: 2500
	private static Vector2 s_DefaultSize = new Vector2(300f, 300f);

	// Token: 0x040009C5 RID: 2501
	private Vector2 m_size;

	// Token: 0x040009C6 RID: 2502
	private float m_sizeScale = 1f;

	// Token: 0x040009C7 RID: 2503
	private Rect m_rect;
}
