using System;

// Token: 0x020003D7 RID: 983
public class HudInterpolateConstant
{
	// Token: 0x06001C85 RID: 7301 RVA: 0x000A9820 File Offset: 0x000A7A20
	public HudInterpolateConstant()
	{
		this.m_startValue = 0;
		this.m_endValue = 0;
		this.m_addValuePerSec = 0f;
		this.m_currentValue = 0f;
		this.m_isEnd = false;
		this.m_pauseFlag = false;
	}

	// Token: 0x06001C86 RID: 7302 RVA: 0x000A9868 File Offset: 0x000A7A68
	public void Setup(int startValue, int endValue, float addValuePerSec)
	{
		this.m_startValue = startValue;
		this.m_currentValue = (float)this.m_startValue;
		this.m_prevValue = this.m_currentValue;
		this.m_endValue = endValue;
		this.m_addValuePerSec = addValuePerSec;
		this.m_isEnd = false;
		this.m_pauseFlag = false;
	}

	// Token: 0x06001C87 RID: 7303 RVA: 0x000A98B4 File Offset: 0x000A7AB4
	public void Reset()
	{
		this.m_isEnd = false;
	}

	// Token: 0x06001C88 RID: 7304 RVA: 0x000A98C0 File Offset: 0x000A7AC0
	public int ForceEnd()
	{
		this.m_isEnd = true;
		this.m_currentValue = (float)this.m_endValue;
		return this.m_endValue;
	}

	// Token: 0x06001C89 RID: 7305 RVA: 0x000A98DC File Offset: 0x000A7ADC
	public int SetForceValue(int value)
	{
		this.m_prevValue = this.m_currentValue;
		if (value < this.m_endValue)
		{
			this.m_currentValue = (float)value;
		}
		else
		{
			this.m_currentValue = (float)this.m_endValue;
			this.m_isEnd = true;
		}
		return (int)this.m_currentValue;
	}

	// Token: 0x06001C8A RID: 7306 RVA: 0x000A992C File Offset: 0x000A7B2C
	public int Update(float deltaTime)
	{
		if (this.m_isEnd)
		{
			return this.m_endValue;
		}
		if (!this.m_pauseFlag)
		{
			this.m_prevValue = this.m_currentValue;
			float num = this.m_addValuePerSec * deltaTime;
			this.m_currentValue += num;
		}
		if (this.m_currentValue >= (float)this.m_endValue)
		{
			this.m_currentValue = (float)this.m_endValue;
			this.m_isEnd = true;
			return this.m_endValue;
		}
		return (int)this.m_currentValue;
	}

	// Token: 0x1700040C RID: 1036
	// (get) Token: 0x06001C8B RID: 7307 RVA: 0x000A99B0 File Offset: 0x000A7BB0
	public bool IsEnd
	{
		get
		{
			return this.m_isEnd;
		}
	}

	// Token: 0x1700040D RID: 1037
	// (get) Token: 0x06001C8C RID: 7308 RVA: 0x000A99B8 File Offset: 0x000A7BB8
	// (set) Token: 0x06001C8D RID: 7309 RVA: 0x000A99C0 File Offset: 0x000A7BC0
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

	// Token: 0x1700040E RID: 1038
	// (get) Token: 0x06001C8E RID: 7310 RVA: 0x000A99CC File Offset: 0x000A7BCC
	public int CurrentValue
	{
		get
		{
			return (int)this.m_currentValue;
		}
	}

	// Token: 0x1700040F RID: 1039
	// (get) Token: 0x06001C8F RID: 7311 RVA: 0x000A99D8 File Offset: 0x000A7BD8
	public int PrevValue
	{
		get
		{
			return (int)this.m_prevValue;
		}
	}

	// Token: 0x04001A6B RID: 6763
	private int m_startValue;

	// Token: 0x04001A6C RID: 6764
	private int m_endValue;

	// Token: 0x04001A6D RID: 6765
	private float m_addValuePerSec;

	// Token: 0x04001A6E RID: 6766
	private float m_currentValue;

	// Token: 0x04001A6F RID: 6767
	private float m_prevValue;

	// Token: 0x04001A70 RID: 6768
	private bool m_isEnd;

	// Token: 0x04001A71 RID: 6769
	private bool m_pauseFlag;
}
