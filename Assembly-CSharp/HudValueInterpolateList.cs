using System;
using System.Collections.Generic;

// Token: 0x020003D6 RID: 982
public class HudValueInterpolateList
{
	// Token: 0x06001C7E RID: 7294 RVA: 0x000A9664 File Offset: 0x000A7864
	public HudValueInterpolateList()
	{
		this.m_interpolateList = new List<HudValueInterpolate>();
		this.m_lastValue = 0L;
	}

	// Token: 0x06001C7F RID: 7295 RVA: 0x000A9680 File Offset: 0x000A7880
	public void Add(int startValue, int endValue, float interpolateTime)
	{
		HudValueInterpolate hudValueInterpolate = new HudValueInterpolate();
		hudValueInterpolate.Setup((long)startValue, (long)endValue, interpolateTime);
		this.m_interpolateList.Add(hudValueInterpolate);
		this.m_isEnd = false;
	}

	// Token: 0x06001C80 RID: 7296 RVA: 0x000A96B4 File Offset: 0x000A78B4
	public void Reset()
	{
		this.m_isEnd = false;
		if (this.m_interpolateList == null)
		{
			return;
		}
		foreach (HudValueInterpolate hudValueInterpolate in this.m_interpolateList)
		{
			if (hudValueInterpolate != null)
			{
				hudValueInterpolate.Reset();
			}
		}
	}

	// Token: 0x06001C81 RID: 7297 RVA: 0x000A9738 File Offset: 0x000A7938
	public void Clear()
	{
		if (this.m_interpolateList == null)
		{
			return;
		}
		this.m_interpolateList.Clear();
	}

	// Token: 0x06001C82 RID: 7298 RVA: 0x000A9754 File Offset: 0x000A7954
	public void ForceEnd()
	{
		if (this.m_interpolateList == null)
		{
			return;
		}
		int count = this.m_interpolateList.Count;
		if (count == 0)
		{
			return;
		}
		HudValueInterpolate hudValueInterpolate = this.m_interpolateList[count - 1];
		this.m_lastValue = hudValueInterpolate.ForceEnd();
		this.m_interpolateList.Clear();
	}

	// Token: 0x06001C83 RID: 7299 RVA: 0x000A97A8 File Offset: 0x000A79A8
	public long Update(float deltaTime)
	{
		if (this.m_isEnd)
		{
			return this.m_lastValue;
		}
		HudValueInterpolate hudValueInterpolate = this.m_interpolateList[0];
		long lastValue = hudValueInterpolate.Update(deltaTime);
		if (hudValueInterpolate.IsEnd)
		{
			this.m_interpolateList.Remove(hudValueInterpolate);
			if (this.m_interpolateList.Count == 0)
			{
				this.m_isEnd = true;
			}
		}
		this.m_lastValue = lastValue;
		return this.m_lastValue;
	}

	// Token: 0x1700040B RID: 1035
	// (get) Token: 0x06001C84 RID: 7300 RVA: 0x000A9818 File Offset: 0x000A7A18
	public bool IsEnd
	{
		get
		{
			return this.m_isEnd;
		}
	}

	// Token: 0x04001A68 RID: 6760
	private List<HudValueInterpolate> m_interpolateList;

	// Token: 0x04001A69 RID: 6761
	private bool m_isEnd;

	// Token: 0x04001A6A RID: 6762
	private long m_lastValue;
}
