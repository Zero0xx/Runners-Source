using System;
using System.Collections.Generic;
using SaveData;
using UnityEngine;

// Token: 0x02000230 RID: 560
public class RaidBossData
{
	// Token: 0x06000F1E RID: 3870 RVA: 0x0005887C File Offset: 0x00056A7C
	public RaidBossData(ServerEventRaidBossState state)
	{
		this.m_myData = null;
		this.m_listData = null;
		this.m_callback = null;
		this.SetData(state);
	}

	// Token: 0x17000240 RID: 576
	// (get) Token: 0x06000F1F RID: 3871 RVA: 0x000588AC File Offset: 0x00056AAC
	// (set) Token: 0x06000F20 RID: 3872 RVA: 0x000588B4 File Offset: 0x00056AB4
	public RaidBossWindow parent
	{
		get
		{
			return this.m_parent;
		}
		set
		{
			this.m_parent = value;
		}
	}

	// Token: 0x06000F21 RID: 3873 RVA: 0x000588C0 File Offset: 0x00056AC0
	public void SetData(ServerEventRaidBossState state)
	{
		this.m_callback = null;
		this.m_id = state.Id;
		this.m_rarity = state.Rarity;
		this.m_lv = state.Level;
		this.m_encounter = state.Encounter;
		this.m_discoverer = state.EncounterName;
		this.m_name = EventUtility.GetRaidBossName(this.m_rarity);
		this.m_participation = state.Participation;
		this.m_end = false;
		this.m_clear = false;
		this.m_creowded = state.Crowded;
		switch (state.Status)
		{
		case 2:
			this.m_end = true;
			break;
		case 3:
			this.m_clear = true;
			this.m_end = true;
			break;
		case 4:
			this.m_clear = true;
			this.m_end = true;
			break;
		}
		this.m_limitTime = state.EscapeAt;
		this.m_bossHp = (long)state.HitPoint;
		this.m_bossHpMax = (long)state.MaxHitPoint;
		this.m_limitTime = this.m_limitTime.AddSeconds(1.0);
	}

	// Token: 0x06000F22 RID: 3874 RVA: 0x000589EC File Offset: 0x00056BEC
	public void SetReward(ServerEventRaidBossBonus bonus)
	{
		this.m_raidbossReward = bonus;
	}

	// Token: 0x06000F23 RID: 3875 RVA: 0x000589F8 File Offset: 0x00056BF8
	public float GetHpRate()
	{
		if (this.m_bossHpMax < 0L && this.m_bossHp < 0L)
		{
			return 0f;
		}
		if (this.m_bossHp >= this.m_bossHpMax)
		{
			return 1f;
		}
		return (float)this.m_bossHp / (float)this.m_bossHpMax;
	}

	// Token: 0x06000F24 RID: 3876 RVA: 0x00058A4C File Offset: 0x00056C4C
	public TimeSpan GetTimeLimit()
	{
		DateTime currentTime = NetBase.GetCurrentTime();
		return this.m_limitTime - currentTime;
	}

	// Token: 0x06000F25 RID: 3877 RVA: 0x00058A70 File Offset: 0x00056C70
	public string GetTimeLimitString(bool slightlyChangeColor = false)
	{
		string result;
		if (!this.end)
		{
			DateTime currentTime = NetBase.GetCurrentTime();
			TimeSpan timeSpan = this.m_limitTime - currentTime;
			if (timeSpan.Ticks > 0L)
			{
				if (timeSpan.TotalSeconds > 60.0 || !slightlyChangeColor)
				{
					result = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
				}
				else
				{
					result = string.Format("[ff0000]{0:D2}:{1:D2}:{2:D2}[-]", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
				}
			}
			else if (slightlyChangeColor)
			{
				result = "[ff0000]00:00:00[-]";
			}
			else
			{
				result = "00:00:00";
			}
		}
		else if (slightlyChangeColor)
		{
			result = "[ff0000]00:00:00[-]";
		}
		else
		{
			result = "00:00:00";
		}
		return result;
	}

	// Token: 0x06000F26 RID: 3878 RVA: 0x00058B68 File Offset: 0x00056D68
	public bool IsLimit()
	{
		return this.GetTimeLimit().Ticks <= 0L;
	}

	// Token: 0x06000F27 RID: 3879 RVA: 0x00058B8C File Offset: 0x00056D8C
	public bool IsDiscoverer()
	{
		return this.m_encounter;
	}

	// Token: 0x06000F28 RID: 3880 RVA: 0x00058B94 File Offset: 0x00056D94
	public bool IsList()
	{
		return this.m_listData != null || this.m_myData != null;
	}

	// Token: 0x06000F29 RID: 3881 RVA: 0x00058BB0 File Offset: 0x00056DB0
	public bool GetListData(RaidBossData.CallbackRaidBossDataUpdate callback, MonoBehaviour obj = null)
	{
		global::Debug.Log("GetListData:" + this.IsList());
		this.m_callback = callback;
		this.m_callback(this);
		return true;
	}

	// Token: 0x06000F2A RID: 3882 RVA: 0x00058BEC File Offset: 0x00056DEC
	public void SetUserList(List<ServerEventRaidBossUserState> stateList)
	{
		if (this.m_listData == null)
		{
			this.m_listData = new List<RaidBossUser>();
		}
		else
		{
			this.m_listData.Clear();
		}
		if (this.m_listData != null && stateList != null)
		{
			string gameID = SystemSaveManager.GetGameID();
			foreach (ServerEventRaidBossUserState state in stateList)
			{
				RaidBossUser raidBossUser = new RaidBossUser(state);
				if (!string.IsNullOrEmpty(raidBossUser.id) && raidBossUser.id != "0000000000" && raidBossUser.destroyCount > 0L)
				{
					this.m_listData.Add(raidBossUser);
					if (!string.IsNullOrEmpty(gameID) && gameID == raidBossUser.id)
					{
						this.m_myData = raidBossUser;
					}
				}
			}
			if (this.m_listData.Count > 0)
			{
				this.m_listData.Sort((RaidBossUser userA, RaidBossUser userB) => (int)(userB.damage - userA.damage));
			}
		}
	}

	// Token: 0x06000F2B RID: 3883 RVA: 0x00058D24 File Offset: 0x00056F24
	public string GetRewardText()
	{
		string text = null;
		if (this.end && this.clear && this.m_raidbossReward != null)
		{
			ServerItem serverItem = new ServerItem(ServerItem.Id.RAIDRING);
			text = serverItem.serverItemName;
			int num = 0;
			num += this.m_raidbossReward.BeatBonus;
			num += this.m_raidbossReward.DamageRateBonus;
			num += this.m_raidbossReward.DamageTopBonus;
			num += this.m_raidbossReward.EncounterBonus;
			num += this.m_raidbossReward.WrestleBonus;
			if (num > 1)
			{
				text = text + " × " + num;
			}
			else if (num <= 0)
			{
				text = null;
			}
		}
		return text;
	}

	// Token: 0x17000241 RID: 577
	// (get) Token: 0x06000F2C RID: 3884 RVA: 0x00058DD8 File Offset: 0x00056FD8
	public long id
	{
		get
		{
			return this.m_id;
		}
	}

	// Token: 0x17000242 RID: 578
	// (get) Token: 0x06000F2D RID: 3885 RVA: 0x00058DE0 File Offset: 0x00056FE0
	public int rarity
	{
		get
		{
			return this.m_rarity;
		}
	}

	// Token: 0x17000243 RID: 579
	// (get) Token: 0x06000F2E RID: 3886 RVA: 0x00058DE8 File Offset: 0x00056FE8
	public int lv
	{
		get
		{
			return this.m_lv;
		}
	}

	// Token: 0x17000244 RID: 580
	// (get) Token: 0x06000F2F RID: 3887 RVA: 0x00058DF0 File Offset: 0x00056FF0
	public string discoverer
	{
		get
		{
			return this.m_discoverer;
		}
	}

	// Token: 0x17000245 RID: 581
	// (get) Token: 0x06000F30 RID: 3888 RVA: 0x00058DF8 File Offset: 0x00056FF8
	public string name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x17000246 RID: 582
	// (get) Token: 0x06000F31 RID: 3889 RVA: 0x00058E00 File Offset: 0x00057000
	public bool participation
	{
		get
		{
			return this.m_participation;
		}
	}

	// Token: 0x17000247 RID: 583
	// (get) Token: 0x06000F32 RID: 3890 RVA: 0x00058E08 File Offset: 0x00057008
	public bool end
	{
		get
		{
			return this.m_end;
		}
	}

	// Token: 0x17000248 RID: 584
	// (get) Token: 0x06000F33 RID: 3891 RVA: 0x00058E10 File Offset: 0x00057010
	public bool clear
	{
		get
		{
			return this.m_clear;
		}
	}

	// Token: 0x17000249 RID: 585
	// (get) Token: 0x06000F34 RID: 3892 RVA: 0x00058E18 File Offset: 0x00057018
	public bool crowded
	{
		get
		{
			return this.m_creowded;
		}
	}

	// Token: 0x1700024A RID: 586
	// (get) Token: 0x06000F35 RID: 3893 RVA: 0x00058E20 File Offset: 0x00057020
	public long hp
	{
		get
		{
			return this.m_bossHp;
		}
	}

	// Token: 0x1700024B RID: 587
	// (get) Token: 0x06000F36 RID: 3894 RVA: 0x00058E28 File Offset: 0x00057028
	public long hpMax
	{
		get
		{
			return this.m_bossHpMax;
		}
	}

	// Token: 0x1700024C RID: 588
	// (get) Token: 0x06000F37 RID: 3895 RVA: 0x00058E30 File Offset: 0x00057030
	public RaidBossUser myData
	{
		get
		{
			return this.m_myData;
		}
	}

	// Token: 0x1700024D RID: 589
	// (get) Token: 0x06000F38 RID: 3896 RVA: 0x00058E38 File Offset: 0x00057038
	public List<RaidBossUser> listData
	{
		get
		{
			return this.m_listData;
		}
	}

	// Token: 0x04000D03 RID: 3331
	private RaidBossUser m_myData;

	// Token: 0x04000D04 RID: 3332
	private List<RaidBossUser> m_listData;

	// Token: 0x04000D05 RID: 3333
	private ServerEventRaidBossBonus m_raidbossReward;

	// Token: 0x04000D06 RID: 3334
	private long m_id;

	// Token: 0x04000D07 RID: 3335
	private int m_lv;

	// Token: 0x04000D08 RID: 3336
	private int m_rarity;

	// Token: 0x04000D09 RID: 3337
	private string m_discoverer;

	// Token: 0x04000D0A RID: 3338
	private string m_name;

	// Token: 0x04000D0B RID: 3339
	private bool m_participation;

	// Token: 0x04000D0C RID: 3340
	private bool m_end;

	// Token: 0x04000D0D RID: 3341
	private bool m_clear;

	// Token: 0x04000D0E RID: 3342
	private bool m_encounter;

	// Token: 0x04000D0F RID: 3343
	private bool m_creowded;

	// Token: 0x04000D10 RID: 3344
	private DateTime m_limitTime;

	// Token: 0x04000D11 RID: 3345
	private long m_bossHp;

	// Token: 0x04000D12 RID: 3346
	private long m_bossHpMax;

	// Token: 0x04000D13 RID: 3347
	private RaidBossWindow m_parent;

	// Token: 0x04000D14 RID: 3348
	private RaidBossData.CallbackRaidBossDataUpdate m_callback;

	// Token: 0x02000A7D RID: 2685
	// (Invoke) Token: 0x0600482E RID: 18478
	public delegate void CallbackRaidBossDataUpdate(RaidBossData data);
}
