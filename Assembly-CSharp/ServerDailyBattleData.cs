using System;
using App;

// Token: 0x020006BE RID: 1726
public class ServerDailyBattleData
{
	// Token: 0x06002E63 RID: 11875 RVA: 0x00111278 File Offset: 0x0010F478
	public ServerDailyBattleData()
	{
		this.maxScore = 0L;
		this.league = 0;
		this.userId = string.Empty;
		this.name = string.Empty;
		this.loginTime = 0L;
		this.mainChaoId = 0;
		this.mainChaoLevel = 0;
		this.subChaoId = 0;
		this.subChaoLevel = 0;
		this.numRank = 0;
		this.charaId = 0;
		this.charaLevel = 0;
		this.subCharaId = 0;
		this.subCharaLevel = 0;
		this.goOnWin = 0;
		this.isSentEnergy = false;
		this.language = Env.Language.JAPANESE;
	}

	// Token: 0x06002E64 RID: 11876 RVA: 0x00111324 File Offset: 0x0010F524
	public bool CheckFriend()
	{
		bool result = false;
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		if (socialInterface != null && socialInterface.IsLoggedIn)
		{
			result = (SocialInterface.GetSocialUserDataFromGameId(socialInterface.FriendList, this.userId) != null);
		}
		return result;
	}

	// Token: 0x06002E65 RID: 11877 RVA: 0x00111370 File Offset: 0x0010F570
	public void Dump()
	{
		if (!string.IsNullOrEmpty(this.userId))
		{
			Debug.Log(string.Format("ServerDailyBattleData  maxScore:{0} league:{1} userId:{2} name:{3} numRank:{4} goOnWin:{5}", new object[]
			{
				this.maxScore,
				this.league,
				this.userId,
				this.name,
				this.numRank,
				this.goOnWin
			}));
		}
		else
		{
			Debug.Log("ServerDailyBattleData  null");
		}
	}

	// Token: 0x06002E66 RID: 11878 RVA: 0x001113FC File Offset: 0x0010F5FC
	public void CopyTo(ServerDailyBattleData dest)
	{
		dest.maxScore = this.maxScore;
		dest.league = this.league;
		dest.userId = this.userId;
		dest.name = this.name;
		dest.loginTime = this.loginTime;
		dest.mainChaoId = this.mainChaoId;
		dest.mainChaoLevel = this.mainChaoLevel;
		dest.subChaoId = this.subChaoId;
		dest.subChaoLevel = this.subChaoLevel;
		dest.numRank = this.numRank;
		dest.charaId = this.charaId;
		dest.charaLevel = this.charaLevel;
		dest.subCharaId = this.subCharaId;
		dest.subCharaLevel = this.subCharaLevel;
		dest.goOnWin = this.goOnWin;
		dest.isSentEnergy = this.isSentEnergy;
		dest.language = this.language;
	}

	// Token: 0x04002A11 RID: 10769
	public long maxScore;

	// Token: 0x04002A12 RID: 10770
	public int league;

	// Token: 0x04002A13 RID: 10771
	public string userId = string.Empty;

	// Token: 0x04002A14 RID: 10772
	public string name = string.Empty;

	// Token: 0x04002A15 RID: 10773
	public long loginTime;

	// Token: 0x04002A16 RID: 10774
	public int mainChaoId;

	// Token: 0x04002A17 RID: 10775
	public int mainChaoLevel;

	// Token: 0x04002A18 RID: 10776
	public int subChaoId;

	// Token: 0x04002A19 RID: 10777
	public int subChaoLevel;

	// Token: 0x04002A1A RID: 10778
	public int numRank;

	// Token: 0x04002A1B RID: 10779
	public int charaId;

	// Token: 0x04002A1C RID: 10780
	public int charaLevel;

	// Token: 0x04002A1D RID: 10781
	public int subCharaId;

	// Token: 0x04002A1E RID: 10782
	public int subCharaLevel;

	// Token: 0x04002A1F RID: 10783
	public int goOnWin;

	// Token: 0x04002A20 RID: 10784
	public bool isSentEnergy;

	// Token: 0x04002A21 RID: 10785
	public Env.Language language;
}
