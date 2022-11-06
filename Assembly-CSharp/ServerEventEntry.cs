using System;

// Token: 0x020007D9 RID: 2009
public class ServerEventEntry
{
	// Token: 0x0600357B RID: 13691 RVA: 0x0011FBDC File Offset: 0x0011DDDC
	public ServerEventEntry()
	{
		this.m_eventId = 0;
		this.m_eventType = 0;
		this.m_eventStartTime = NetBase.GetCurrentTime();
		this.m_eventEndTime = NetBase.GetCurrentTime();
		this.m_eventCloseTime = NetBase.GetCurrentTime();
	}

	// Token: 0x17000771 RID: 1905
	// (get) Token: 0x0600357C RID: 13692 RVA: 0x0011FC14 File Offset: 0x0011DE14
	// (set) Token: 0x0600357D RID: 13693 RVA: 0x0011FC1C File Offset: 0x0011DE1C
	public int EventId
	{
		get
		{
			return this.m_eventId;
		}
		set
		{
			this.m_eventId = value;
		}
	}

	// Token: 0x17000772 RID: 1906
	// (get) Token: 0x0600357E RID: 13694 RVA: 0x0011FC28 File Offset: 0x0011DE28
	// (set) Token: 0x0600357F RID: 13695 RVA: 0x0011FC30 File Offset: 0x0011DE30
	public int EventType
	{
		get
		{
			return this.m_eventType;
		}
		set
		{
			this.m_eventType = value;
		}
	}

	// Token: 0x17000773 RID: 1907
	// (get) Token: 0x06003580 RID: 13696 RVA: 0x0011FC3C File Offset: 0x0011DE3C
	// (set) Token: 0x06003581 RID: 13697 RVA: 0x0011FC44 File Offset: 0x0011DE44
	public DateTime EventStartTime
	{
		get
		{
			return this.m_eventStartTime;
		}
		set
		{
			this.m_eventStartTime = value;
		}
	}

	// Token: 0x17000774 RID: 1908
	// (get) Token: 0x06003582 RID: 13698 RVA: 0x0011FC50 File Offset: 0x0011DE50
	// (set) Token: 0x06003583 RID: 13699 RVA: 0x0011FC58 File Offset: 0x0011DE58
	public DateTime EventEndTime
	{
		get
		{
			return this.m_eventEndTime;
		}
		set
		{
			this.m_eventEndTime = value;
		}
	}

	// Token: 0x17000775 RID: 1909
	// (get) Token: 0x06003584 RID: 13700 RVA: 0x0011FC64 File Offset: 0x0011DE64
	// (set) Token: 0x06003585 RID: 13701 RVA: 0x0011FC6C File Offset: 0x0011DE6C
	public DateTime EventCloseTime
	{
		get
		{
			return this.m_eventCloseTime;
		}
		set
		{
			this.m_eventCloseTime = value;
		}
	}

	// Token: 0x06003586 RID: 13702 RVA: 0x0011FC78 File Offset: 0x0011DE78
	public void CopyTo(ServerEventEntry to)
	{
		to.m_eventId = this.m_eventId;
		to.m_eventType = this.m_eventType;
		to.m_eventStartTime = this.m_eventStartTime;
		to.m_eventEndTime = this.m_eventEndTime;
		to.m_eventCloseTime = this.m_eventCloseTime;
	}

	// Token: 0x04002D34 RID: 11572
	private int m_eventId;

	// Token: 0x04002D35 RID: 11573
	private int m_eventType;

	// Token: 0x04002D36 RID: 11574
	private DateTime m_eventStartTime;

	// Token: 0x04002D37 RID: 11575
	private DateTime m_eventEndTime;

	// Token: 0x04002D38 RID: 11576
	private DateTime m_eventCloseTime;
}
