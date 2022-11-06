using System;

// Token: 0x020003D5 RID: 981
public class HudValueInterpolate
{
	// Token: 0x06001C75 RID: 7285 RVA: 0x000A9510 File Offset: 0x000A7710
	public HudValueInterpolate()
	{
		this.m_startValue = 0L;
		this.m_endValue = 0L;
		this.m_interpolateTime = 0f;
		this.m_currentTime = 0f;
		this.m_isEnd = false;
		this.m_pauseFlag = false;
	}

	// Token: 0x06001C76 RID: 7286 RVA: 0x000A9558 File Offset: 0x000A7758
	public void Setup(long startValue, long endValue, float interpolateTime)
	{
		this.m_startValue = startValue;
		this.m_currentTime = 0f;
		this.m_endValue = endValue;
		this.m_interpolateTime = interpolateTime;
		this.m_isEnd = false;
		this.m_pauseFlag = false;
	}

	// Token: 0x06001C77 RID: 7287 RVA: 0x000A9594 File Offset: 0x000A7794
	public void Reset()
	{
		this.m_isEnd = false;
	}

	// Token: 0x06001C78 RID: 7288 RVA: 0x000A95A0 File Offset: 0x000A77A0
	public long ForceEnd()
	{
		this.m_isEnd = true;
		this.m_currentTime = this.m_interpolateTime;
		return this.m_endValue;
	}

	// Token: 0x06001C79 RID: 7289 RVA: 0x000A95BC File Offset: 0x000A77BC
	public long Update(float deltaTime)
	{
		if (this.m_isEnd)
		{
			return this.m_endValue;
		}
		if (!this.m_pauseFlag)
		{
			this.m_currentTime += deltaTime;
		}
		if (this.m_currentTime >= this.m_interpolateTime)
		{
			this.m_isEnd = true;
			return this.m_endValue;
		}
		long num = this.m_endValue - this.m_startValue;
		float num2 = this.m_currentTime / this.m_interpolateTime;
		return this.m_startValue + (long)((float)num * num2);
	}

	// Token: 0x17000408 RID: 1032
	// (get) Token: 0x06001C7A RID: 7290 RVA: 0x000A9640 File Offset: 0x000A7840
	public bool IsEnd
	{
		get
		{
			return this.m_isEnd;
		}
	}

	// Token: 0x17000409 RID: 1033
	// (get) Token: 0x06001C7B RID: 7291 RVA: 0x000A9648 File Offset: 0x000A7848
	// (set) Token: 0x06001C7C RID: 7292 RVA: 0x000A9650 File Offset: 0x000A7850
	public bool IsPause
	{
		get
		{
			return this.m_pauseFlag;
		}
		set
		{
			this.m_pauseFlag = value;
		}
	}

	// Token: 0x1700040A RID: 1034
	// (get) Token: 0x06001C7D RID: 7293 RVA: 0x000A965C File Offset: 0x000A785C
	public float CurrentTime
	{
		get
		{
			return this.m_currentTime;
		}
	}

	// Token: 0x04001A62 RID: 6754
	private long m_startValue;

	// Token: 0x04001A63 RID: 6755
	private long m_endValue;

	// Token: 0x04001A64 RID: 6756
	private float m_interpolateTime;

	// Token: 0x04001A65 RID: 6757
	private float m_currentTime;

	// Token: 0x04001A66 RID: 6758
	private bool m_isEnd;

	// Token: 0x04001A67 RID: 6759
	private bool m_pauseFlag;
}
