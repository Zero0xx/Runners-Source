using System;
using System.Collections.Generic;

// Token: 0x0200080A RID: 2058
public class ServerLoginBonusData
{
	// Token: 0x06003722 RID: 14114 RVA: 0x001234FC File Offset: 0x001216FC
	public ServerLoginBonusData()
	{
		this.m_loginBonusState = new ServerLoginBonusState();
		this.m_startTime = DateTime.Now;
		this.m_endTime = DateTime.Now;
		this.m_loginBonusRewardList = new List<ServerLoginBonusReward>();
		this.m_firstLoginBonusRewardList = new List<ServerLoginBonusReward>();
		this.m_rewardId = 0;
		this.m_rewardDays = 0;
		this.m_firstRewardDays = 0;
		this.m_lastBonusReward = null;
		this.m_firstLastBonusReward = null;
	}

	// Token: 0x06003723 RID: 14115 RVA: 0x0012356C File Offset: 0x0012176C
	public void CopyTo(ServerLoginBonusData dest)
	{
		this.m_loginBonusState.CopyTo(dest.m_loginBonusState);
		dest.m_startTime = this.m_startTime;
		dest.m_endTime = this.m_endTime;
		foreach (ServerLoginBonusReward item in this.m_loginBonusRewardList)
		{
			dest.m_loginBonusRewardList.Add(item);
		}
		foreach (ServerLoginBonusReward item2 in this.m_firstLoginBonusRewardList)
		{
			dest.m_firstLoginBonusRewardList.Add(item2);
		}
		dest.m_rewardId = this.m_rewardId;
		dest.m_rewardDays = this.m_rewardDays;
		dest.m_firstRewardDays = this.m_firstRewardDays;
	}

	// Token: 0x06003724 RID: 14116 RVA: 0x00123680 File Offset: 0x00121880
	public int CalcTodayCount()
	{
		int result = 0;
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			DateTime startTime = this.m_startTime;
			DateTime endTime = this.m_endTime;
			DateTime currentTime = NetUtil.GetCurrentTime();
			if (endTime < currentTime)
			{
				return -1;
			}
			if (startTime < currentTime)
			{
				result = (currentTime - startTime).Days;
			}
			else if (currentTime < startTime)
			{
				return -1;
			}
		}
		return result;
	}

	// Token: 0x06003725 RID: 14117 RVA: 0x001236FC File Offset: 0x001218FC
	public int getTotalDays()
	{
		int result = 0;
		DateTime startTime = this.m_startTime;
		DateTime endTime = this.m_endTime;
		if (this.m_startTime < this.m_endTime)
		{
			result = (this.m_endTime - this.m_startTime).Days + 1;
		}
		return result;
	}

	// Token: 0x06003726 RID: 14118 RVA: 0x0012374C File Offset: 0x0012194C
	public bool isGetLoginBonusToday()
	{
		DateTime lastBonusTime = this.m_loginBonusState.m_lastBonusTime;
		DateTime currentTime = NetUtil.GetCurrentTime();
		return currentTime < lastBonusTime || (this.m_rewardId == -1 || this.m_rewardDays == -1);
	}

	// Token: 0x06003727 RID: 14119 RVA: 0x00123794 File Offset: 0x00121994
	public void setLoginBonusList(ServerLoginBonusReward reward, ServerLoginBonusReward firstReward)
	{
		this.clearLoginBonusList();
		if (reward != null)
		{
			this.m_lastBonusReward = new ServerLoginBonusReward();
			reward.CopyTo(this.m_lastBonusReward);
		}
		if (firstReward != null)
		{
			this.m_firstLastBonusReward = new ServerLoginBonusReward();
			firstReward.CopyTo(this.m_firstLastBonusReward);
		}
	}

	// Token: 0x06003728 RID: 14120 RVA: 0x001237E4 File Offset: 0x001219E4
	public void clearLoginBonusList()
	{
		this.m_lastBonusReward = null;
		this.m_firstLastBonusReward = null;
	}

	// Token: 0x06003729 RID: 14121 RVA: 0x001237F4 File Offset: 0x001219F4
	public void replayTodayBonus()
	{
		int numLogin = this.m_loginBonusState.m_numLogin;
		int numBonus = this.m_loginBonusState.m_numBonus;
		ServerLoginBonusReward reward = null;
		if (numBonus > 0 && this.m_loginBonusRewardList != null && this.m_loginBonusRewardList.Count > 0)
		{
			reward = this.m_loginBonusRewardList[numBonus - 1];
		}
		ServerLoginBonusReward firstReward = null;
		if (numLogin > 0 && this.m_firstLoginBonusRewardList != null && this.m_firstLoginBonusRewardList.Count > 0)
		{
			firstReward = this.m_firstLoginBonusRewardList[numLogin - 1];
		}
		this.setLoginBonusList(reward, firstReward);
	}

	// Token: 0x04002E6B RID: 11883
	public ServerLoginBonusState m_loginBonusState;

	// Token: 0x04002E6C RID: 11884
	public DateTime m_startTime;

	// Token: 0x04002E6D RID: 11885
	public DateTime m_endTime;

	// Token: 0x04002E6E RID: 11886
	public List<ServerLoginBonusReward> m_loginBonusRewardList;

	// Token: 0x04002E6F RID: 11887
	public List<ServerLoginBonusReward> m_firstLoginBonusRewardList;

	// Token: 0x04002E70 RID: 11888
	public int m_rewardId;

	// Token: 0x04002E71 RID: 11889
	public int m_rewardDays;

	// Token: 0x04002E72 RID: 11890
	public int m_firstRewardDays;

	// Token: 0x04002E73 RID: 11891
	public ServerLoginBonusReward m_lastBonusReward;

	// Token: 0x04002E74 RID: 11892
	public ServerLoginBonusReward m_firstLastBonusReward;
}
