using System;

// Token: 0x020006BF RID: 1727
public class ServerDailyBattleDataPair
{
	// Token: 0x06002E67 RID: 11879 RVA: 0x001114D8 File Offset: 0x0010F6D8
	public ServerDailyBattleDataPair()
	{
		this.isDummyData = false;
		this.starTime = NetBase.GetCurrentTime();
		this.endTime = NetBase.GetCurrentTime();
		this.myBattleData = new ServerDailyBattleData();
		this.rivalBattleData = new ServerDailyBattleData();
	}

	// Token: 0x06002E68 RID: 11880 RVA: 0x00111514 File Offset: 0x0010F714
	public ServerDailyBattleDataPair(ServerDailyBattleDataPair data)
	{
		this.isDummyData = data.isDummyData;
		this.starTime = data.starTime;
		this.endTime = data.endTime;
		this.myBattleData = new ServerDailyBattleData();
		this.rivalBattleData = new ServerDailyBattleData();
		data.myBattleData.CopyTo(this.myBattleData);
		data.rivalBattleData.CopyTo(this.rivalBattleData);
	}

	// Token: 0x06002E69 RID: 11881 RVA: 0x00111584 File Offset: 0x0010F784
	public ServerDailyBattleDataPair(DateTime start, DateTime end)
	{
		this.isDummyData = true;
		this.starTime = start;
		this.endTime = end;
		this.myBattleData = new ServerDailyBattleData();
		this.rivalBattleData = new ServerDailyBattleData();
	}

	// Token: 0x17000610 RID: 1552
	// (get) Token: 0x06002E6A RID: 11882 RVA: 0x001115B8 File Offset: 0x0010F7B8
	public string starDateString
	{
		get
		{
			return GeneralUtil.GetDateString(this.starTime, 0);
		}
	}

	// Token: 0x17000611 RID: 1553
	// (get) Token: 0x06002E6B RID: 11883 RVA: 0x001115C8 File Offset: 0x0010F7C8
	public string endDateString
	{
		get
		{
			return GeneralUtil.GetDateString(this.endTime, 0);
		}
	}

	// Token: 0x17000612 RID: 1554
	// (get) Token: 0x06002E6C RID: 11884 RVA: 0x001115D8 File Offset: 0x0010F7D8
	public bool isToday
	{
		get
		{
			bool result = false;
			if (this.starTime.Ticks != this.endTime.Ticks && !this.isDummyData)
			{
				DateTime currentTime = NetBase.GetCurrentTime();
				if (this.endTime.Ticks >= currentTime.Ticks)
				{
					result = true;
				}
			}
			return result;
		}
	}

	// Token: 0x17000613 RID: 1555
	// (get) Token: 0x06002E6D RID: 11885 RVA: 0x00111630 File Offset: 0x0010F830
	public int goOnWin
	{
		get
		{
			int result = 0;
			if (this.myBattleData != null && !string.IsNullOrEmpty(this.myBattleData.userId))
			{
				result = this.myBattleData.goOnWin;
			}
			return result;
		}
	}

	// Token: 0x17000614 RID: 1556
	// (get) Token: 0x06002E6E RID: 11886 RVA: 0x0011166C File Offset: 0x0010F86C
	public int winFlag
	{
		get
		{
			int result = 0;
			if (this.myBattleData != null && !string.IsNullOrEmpty(this.myBattleData.userId))
			{
				if (this.rivalBattleData != null && !string.IsNullOrEmpty(this.rivalBattleData.userId))
				{
					if (this.myBattleData.maxScore > this.rivalBattleData.maxScore)
					{
						result = 3;
					}
					else if (this.myBattleData.maxScore == this.rivalBattleData.maxScore)
					{
						result = 2;
					}
					else
					{
						result = 1;
					}
				}
				else
				{
					result = 4;
				}
			}
			return result;
		}
	}

	// Token: 0x06002E6F RID: 11887 RVA: 0x0011170C File Offset: 0x0010F90C
	public void Dump()
	{
		this.myBattleData.Dump();
		this.rivalBattleData.Dump();
	}

	// Token: 0x06002E70 RID: 11888 RVA: 0x00111724 File Offset: 0x0010F924
	public void CopyTo(ServerDailyBattleDataPair dest)
	{
		dest.isDummyData = this.isDummyData;
		dest.starTime = this.starTime;
		dest.endTime = this.endTime;
		this.myBattleData.CopyTo(dest.myBattleData);
		this.rivalBattleData.CopyTo(dest.rivalBattleData);
	}

	// Token: 0x04002A22 RID: 10786
	public bool isDummyData;

	// Token: 0x04002A23 RID: 10787
	public DateTime starTime;

	// Token: 0x04002A24 RID: 10788
	public DateTime endTime;

	// Token: 0x04002A25 RID: 10789
	public ServerDailyBattleData myBattleData;

	// Token: 0x04002A26 RID: 10790
	public ServerDailyBattleData rivalBattleData;
}
