using System;
using App;
using UnityEngine;

// Token: 0x020003D0 RID: 976
public class HudScrollBar : MonoBehaviour
{
	// Token: 0x06001C4E RID: 7246 RVA: 0x000A8A8C File Offset: 0x000A6C8C
	private void Start()
	{
	}

	// Token: 0x06001C4F RID: 7247 RVA: 0x000A8A90 File Offset: 0x000A6C90
	public void Setup(UIScrollBar scrollBar, int pageCount)
	{
		if (scrollBar == null)
		{
			return;
		}
		if (pageCount <= 1)
		{
			return;
		}
		this.m_scrollBar = scrollBar;
		this.m_stepValue = 1f / ((float)pageCount - 1f);
		EventDelegate.Add(this.m_scrollBar.onChange, new EventDelegate.Callback(this.OnChangeScrollBarValue));
	}

	// Token: 0x06001C50 RID: 7248 RVA: 0x000A8AEC File Offset: 0x000A6CEC
	public void SetPageChangeCallback(HudScrollBar.PageChangeCallback callback)
	{
		this.m_callback = callback;
	}

	// Token: 0x06001C51 RID: 7249 RVA: 0x000A8AF8 File Offset: 0x000A6CF8
	public void LeftScroll(int pageCount)
	{
		float num = this.m_stepValue * (float)pageCount;
		float value = this.m_scrollBar.value;
		if (App.Math.NearZero(value - 1f, 1E-06f))
		{
			this.m_scrollBar.value = 1f - num;
		}
		else if (App.Math.NearZero(value - num, 1E-06f))
		{
			this.m_scrollBar.value = 0f;
			if (this.m_scrollBar.onDragFinished != null)
			{
				this.m_scrollBar.onDragFinished();
			}
		}
		else
		{
			this.m_scrollBar.value -= num;
		}
	}

	// Token: 0x06001C52 RID: 7250 RVA: 0x000A8BA4 File Offset: 0x000A6DA4
	public void RightScroll(int pageCount)
	{
		float num = this.m_stepValue * (float)pageCount;
		float value = this.m_scrollBar.value;
		if (App.Math.NearZero(value, 1E-06f))
		{
			this.m_scrollBar.value = num;
		}
		else if (App.Math.NearZero(value + num - 1f, 1E-06f))
		{
			this.m_scrollBar.value = 1f;
			if (this.m_scrollBar.onDragFinished != null)
			{
				this.m_scrollBar.onDragFinished();
			}
		}
		else
		{
			this.m_scrollBar.value += num;
		}
	}

	// Token: 0x06001C53 RID: 7251 RVA: 0x000A8C48 File Offset: 0x000A6E48
	public void PageJump(int pageIndex, bool isInit)
	{
		this.m_scrollBar.value = this.m_stepValue * (float)pageIndex;
		if (this.m_scrollBar.onDragFinished != null)
		{
			this.m_scrollBar.onDragFinished();
		}
		this.m_isInit = isInit;
	}

	// Token: 0x06001C54 RID: 7252 RVA: 0x000A8C88 File Offset: 0x000A6E88
	private void LateUpdate()
	{
		int currentPage = this.GetCurrentPage();
		if (this.m_currentPage != currentPage || this.m_isInit)
		{
			bool flag = App.Math.NearEqual(this.m_lastScrollValue, this.m_scrollBar.value, 1E-06f);
			if (flag)
			{
				if (this.m_callback != null)
				{
					this.m_callback(this.m_currentPage, currentPage);
				}
				this.m_currentPage = currentPage;
				this.m_isInit = false;
			}
		}
		this.m_lastScrollValue = this.m_scrollBar.value;
	}

	// Token: 0x06001C55 RID: 7253 RVA: 0x000A8D14 File Offset: 0x000A6F14
	private void OnDestroy()
	{
		EventDelegate.Remove(this.m_scrollBar.onChange, new EventDelegate.Callback(this.OnChangeScrollBarValue));
	}

	// Token: 0x06001C56 RID: 7254 RVA: 0x000A8D34 File Offset: 0x000A6F34
	private void OnChangeScrollBarValue()
	{
	}

	// Token: 0x06001C57 RID: 7255 RVA: 0x000A8D38 File Offset: 0x000A6F38
	private int GetCurrentPage()
	{
		float num = -this.m_stepValue * 0.5f;
		float num2 = this.m_stepValue * 0.5f;
		int num3 = 0;
		while (num > this.m_scrollBar.value || this.m_scrollBar.value > num2)
		{
			num += this.m_stepValue;
			num2 += this.m_stepValue;
			num3++;
		}
		return num3;
	}

	// Token: 0x04001A51 RID: 6737
	private UIScrollBar m_scrollBar;

	// Token: 0x04001A52 RID: 6738
	private float m_stepValue;

	// Token: 0x04001A53 RID: 6739
	private HudScrollBar.PageChangeCallback m_callback;

	// Token: 0x04001A54 RID: 6740
	private int m_currentPage;

	// Token: 0x04001A55 RID: 6741
	private bool m_isInit;

	// Token: 0x04001A56 RID: 6742
	private float m_lastScrollValue;

	// Token: 0x02000A89 RID: 2697
	// (Invoke) Token: 0x0600485E RID: 18526
	public delegate void PageChangeCallback(int prevPage, int currentPage);
}
