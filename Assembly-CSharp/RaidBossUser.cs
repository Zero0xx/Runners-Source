using System;
using App;
using UnityEngine;

// Token: 0x02000231 RID: 561
public class RaidBossUser
{
	// Token: 0x06000F3A RID: 3898 RVA: 0x00058E50 File Offset: 0x00057050
	public RaidBossUser(ServerEventRaidBossUserState state)
	{
		this.isDestroy = state.WrestleBeatFlg;
		this.damage = (long)state.WrestleDamage;
		this.destroyCount = (long)state.WrestleCount;
		this.id = state.WrestleId;
		this.userName = state.UserName;
		this.rankIndex = state.Grade - 1;
		this.mapRank = state.NumRank;
		this.loginTime = NetUtil.GetLocalDateTime((long)state.LoginTime);
		ServerItem serverItem = new ServerItem((ServerItem.Id)state.CharaId);
		this.charaType = serverItem.charaType;
		ServerItem serverItem2 = new ServerItem((ServerItem.Id)state.SubCharaId);
		this.charaSubType = serverItem2.charaType;
		this.charaLevel = state.CharaLevel;
		ServerItem serverItem3 = new ServerItem((ServerItem.Id)state.MainChaoId);
		this.mainChaoId = serverItem3.chaoId;
		ServerItem serverItem4 = new ServerItem((ServerItem.Id)state.SubChaoId);
		this.subChaoId = serverItem4.chaoId;
		this.mainChaoLevel = state.MainChaoLevel;
		this.subChaoLevel = state.SubChaoLevel;
		this.leagueIndex = state.League;
		this.language = (Env.Language)state.Language;
		if (RaidBossUser.s_socialInterface == null)
		{
			RaidBossUser.s_socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		}
		if (RaidBossUser.s_socialInterface != null)
		{
			this.isFriend = (SocialInterface.GetSocialUserDataFromGameId(RaidBossUser.s_socialInterface.FriendList, this.id) != null);
		}
		else
		{
			this.isFriend = (this.isSentEnergy || UnityEngine.Random.Range(0, 3) != 0);
		}
	}

	// Token: 0x06000F3B RID: 3899 RVA: 0x00058FE8 File Offset: 0x000571E8
	public RaidBossUser(ServerEventRaidBossDesiredState state)
	{
		this.isDestroy = false;
		this.damage = 0L;
		this.destroyCount = 0L;
		this.id = state.DesireId;
		this.userName = state.UserName;
		this.mapRank = state.NumRank;
		this.loginTime = NetUtil.GetLocalDateTime((long)state.LoginTime);
		ServerItem serverItem = new ServerItem((ServerItem.Id)state.CharaId);
		this.charaType = serverItem.charaType;
		ServerItem serverItem2 = new ServerItem((ServerItem.Id)state.SubCharaId);
		this.charaSubType = serverItem2.charaType;
		this.charaLevel = state.CharaLevel;
		ServerItem serverItem3 = new ServerItem((ServerItem.Id)state.MainChaoId);
		this.mainChaoId = serverItem3.chaoId;
		ServerItem serverItem4 = new ServerItem((ServerItem.Id)state.SubChaoId);
		this.subChaoId = serverItem4.chaoId;
		this.mainChaoLevel = state.MainChaoLevel;
		this.subChaoLevel = state.SubChaoLevel;
		this.leagueIndex = state.League;
		this.language = (Env.Language)state.Language;
		if (RaidBossUser.s_socialInterface == null)
		{
			RaidBossUser.s_socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		}
		if (RaidBossUser.s_socialInterface != null)
		{
			this.isFriend = (SocialInterface.GetSocialUserDataFromGameId(RaidBossUser.s_socialInterface.FriendList, this.id) != null);
		}
		else
		{
			this.isFriend = (this.isSentEnergy || UnityEngine.Random.Range(0, 3) != 0);
		}
	}

	// Token: 0x1700024E RID: 590
	// (get) Token: 0x06000F3C RID: 3900 RVA: 0x00059164 File Offset: 0x00057364
	// (set) Token: 0x06000F3D RID: 3901 RVA: 0x0005916C File Offset: 0x0005736C
	public long damage { get; set; }

	// Token: 0x1700024F RID: 591
	// (get) Token: 0x06000F3E RID: 3902 RVA: 0x00059178 File Offset: 0x00057378
	// (set) Token: 0x06000F3F RID: 3903 RVA: 0x00059180 File Offset: 0x00057380
	public long destroyCount { get; set; }

	// Token: 0x17000250 RID: 592
	// (get) Token: 0x06000F40 RID: 3904 RVA: 0x0005918C File Offset: 0x0005738C
	// (set) Token: 0x06000F41 RID: 3905 RVA: 0x00059194 File Offset: 0x00057394
	public bool isDestroy { get; set; }

	// Token: 0x17000251 RID: 593
	// (get) Token: 0x06000F42 RID: 3906 RVA: 0x000591A0 File Offset: 0x000573A0
	// (set) Token: 0x06000F43 RID: 3907 RVA: 0x000591A8 File Offset: 0x000573A8
	public int mapRank { get; set; }

	// Token: 0x17000252 RID: 594
	// (get) Token: 0x06000F44 RID: 3908 RVA: 0x000591B4 File Offset: 0x000573B4
	public string dispMapRank
	{
		get
		{
			return (this.mapRank + 1).ToString("D3");
		}
	}

	// Token: 0x17000253 RID: 595
	// (get) Token: 0x06000F45 RID: 3909 RVA: 0x000591D8 File Offset: 0x000573D8
	// (set) Token: 0x06000F46 RID: 3910 RVA: 0x000591E0 File Offset: 0x000573E0
	public int rankIndex { get; set; }

	// Token: 0x17000254 RID: 596
	// (get) Token: 0x06000F47 RID: 3911 RVA: 0x000591EC File Offset: 0x000573EC
	// (set) Token: 0x06000F48 RID: 3912 RVA: 0x000591F4 File Offset: 0x000573F4
	public int rankIndexChanged { get; set; }

	// Token: 0x17000255 RID: 597
	// (get) Token: 0x06000F49 RID: 3913 RVA: 0x00059200 File Offset: 0x00057400
	// (set) Token: 0x06000F4A RID: 3914 RVA: 0x00059208 File Offset: 0x00057408
	public string userName { get; set; }

	// Token: 0x17000256 RID: 598
	// (get) Token: 0x06000F4B RID: 3915 RVA: 0x00059214 File Offset: 0x00057414
	// (set) Token: 0x06000F4C RID: 3916 RVA: 0x0005921C File Offset: 0x0005741C
	public bool isFriend { get; set; }

	// Token: 0x17000257 RID: 599
	// (get) Token: 0x06000F4D RID: 3917 RVA: 0x00059228 File Offset: 0x00057428
	// (set) Token: 0x06000F4E RID: 3918 RVA: 0x00059230 File Offset: 0x00057430
	public bool isSentEnergy { get; set; }

	// Token: 0x17000258 RID: 600
	// (get) Token: 0x06000F4F RID: 3919 RVA: 0x0005923C File Offset: 0x0005743C
	// (set) Token: 0x06000F50 RID: 3920 RVA: 0x00059244 File Offset: 0x00057444
	public CharaType charaType { get; set; }

	// Token: 0x17000259 RID: 601
	// (get) Token: 0x06000F51 RID: 3921 RVA: 0x00059250 File Offset: 0x00057450
	// (set) Token: 0x06000F52 RID: 3922 RVA: 0x00059258 File Offset: 0x00057458
	public CharaType charaSubType { get; set; }

	// Token: 0x1700025A RID: 602
	// (get) Token: 0x06000F53 RID: 3923 RVA: 0x00059264 File Offset: 0x00057464
	// (set) Token: 0x06000F54 RID: 3924 RVA: 0x0005926C File Offset: 0x0005746C
	public int charaLevel { get; set; }

	// Token: 0x1700025B RID: 603
	// (get) Token: 0x06000F55 RID: 3925 RVA: 0x00059278 File Offset: 0x00057478
	// (set) Token: 0x06000F56 RID: 3926 RVA: 0x00059280 File Offset: 0x00057480
	public int mainChaoId { get; set; }

	// Token: 0x1700025C RID: 604
	// (get) Token: 0x06000F57 RID: 3927 RVA: 0x0005928C File Offset: 0x0005748C
	// (set) Token: 0x06000F58 RID: 3928 RVA: 0x00059294 File Offset: 0x00057494
	public int subChaoId { get; set; }

	// Token: 0x1700025D RID: 605
	// (get) Token: 0x06000F59 RID: 3929 RVA: 0x000592A0 File Offset: 0x000574A0
	// (set) Token: 0x06000F5A RID: 3930 RVA: 0x000592A8 File Offset: 0x000574A8
	public int mainChaoLevel { get; set; }

	// Token: 0x1700025E RID: 606
	// (get) Token: 0x06000F5B RID: 3931 RVA: 0x000592B4 File Offset: 0x000574B4
	// (set) Token: 0x06000F5C RID: 3932 RVA: 0x000592BC File Offset: 0x000574BC
	public int subChaoLevel { get; set; }

	// Token: 0x1700025F RID: 607
	// (get) Token: 0x06000F5D RID: 3933 RVA: 0x000592C8 File Offset: 0x000574C8
	// (set) Token: 0x06000F5E RID: 3934 RVA: 0x000592D0 File Offset: 0x000574D0
	public Env.Language language { get; set; }

	// Token: 0x17000260 RID: 608
	// (get) Token: 0x06000F5F RID: 3935 RVA: 0x000592DC File Offset: 0x000574DC
	// (set) Token: 0x06000F60 RID: 3936 RVA: 0x000592E4 File Offset: 0x000574E4
	public int leagueIndex { get; set; }

	// Token: 0x17000261 RID: 609
	// (get) Token: 0x06000F61 RID: 3937 RVA: 0x000592F0 File Offset: 0x000574F0
	// (set) Token: 0x06000F62 RID: 3938 RVA: 0x000592F8 File Offset: 0x000574F8
	public string id { get; set; }

	// Token: 0x17000262 RID: 610
	// (get) Token: 0x06000F63 RID: 3939 RVA: 0x00059304 File Offset: 0x00057504
	// (set) Token: 0x06000F64 RID: 3940 RVA: 0x0005930C File Offset: 0x0005750C
	public DateTime loginTime { get; set; }

	// Token: 0x17000263 RID: 611
	// (get) Token: 0x06000F65 RID: 3941 RVA: 0x00059318 File Offset: 0x00057518
	public bool isRankTop
	{
		get
		{
			return this.rankIndex < 1;
		}
	}

	// Token: 0x17000264 RID: 612
	// (get) Token: 0x06000F66 RID: 3942 RVA: 0x00059330 File Offset: 0x00057530
	public int mainChaoRarity
	{
		get
		{
			return this.mainChaoId / 1000;
		}
	}

	// Token: 0x17000265 RID: 613
	// (get) Token: 0x06000F67 RID: 3943 RVA: 0x00059340 File Offset: 0x00057540
	public int subChaoRarity
	{
		get
		{
			return this.subChaoId / 1000;
		}
	}

	// Token: 0x06000F68 RID: 3944 RVA: 0x00059350 File Offset: 0x00057550
	public RankingUtil.Ranker ConvertRankerData()
	{
		return new RankingUtil.Ranker(this);
	}

	// Token: 0x04000D16 RID: 3350
	public static SocialInterface s_socialInterface;
}
