using System;

// Token: 0x020007DD RID: 2013
public class ServerEventRaidBossState
{
	// Token: 0x060035B4 RID: 13748 RVA: 0x001200FC File Offset: 0x0011E2FC
	public ServerEventRaidBossState()
	{
		this.m_raidBossId = 0L;
		this.m_level = 0;
		this.m_rarity = 0;
		this.m_hitPoint = 0;
		this.m_maxHitPoint = 0;
		this.m_status = 0;
		this.m_escapeAt = DateTime.MinValue;
		this.m_encounterName = string.Empty;
		this.m_encounterFlag = false;
		this.m_crowdedFlag = false;
		this.m_participationFlag = false;
	}

	// Token: 0x1700078A RID: 1930
	// (get) Token: 0x060035B5 RID: 13749 RVA: 0x00120168 File Offset: 0x0011E368
	// (set) Token: 0x060035B6 RID: 13750 RVA: 0x00120170 File Offset: 0x0011E370
	public long Id
	{
		get
		{
			return this.m_raidBossId;
		}
		set
		{
			this.m_raidBossId = value;
		}
	}

	// Token: 0x1700078B RID: 1931
	// (get) Token: 0x060035B7 RID: 13751 RVA: 0x0012017C File Offset: 0x0011E37C
	// (set) Token: 0x060035B8 RID: 13752 RVA: 0x00120184 File Offset: 0x0011E384
	public int Level
	{
		get
		{
			return this.m_level;
		}
		set
		{
			this.m_level = value;
		}
	}

	// Token: 0x1700078C RID: 1932
	// (get) Token: 0x060035B9 RID: 13753 RVA: 0x00120190 File Offset: 0x0011E390
	// (set) Token: 0x060035BA RID: 13754 RVA: 0x00120198 File Offset: 0x0011E398
	public int Rarity
	{
		get
		{
			return this.m_rarity;
		}
		set
		{
			this.m_rarity = value;
		}
	}

	// Token: 0x1700078D RID: 1933
	// (get) Token: 0x060035BB RID: 13755 RVA: 0x001201A4 File Offset: 0x0011E3A4
	// (set) Token: 0x060035BC RID: 13756 RVA: 0x001201AC File Offset: 0x0011E3AC
	public int HitPoint
	{
		get
		{
			return this.m_hitPoint;
		}
		set
		{
			this.m_hitPoint = value;
		}
	}

	// Token: 0x1700078E RID: 1934
	// (get) Token: 0x060035BD RID: 13757 RVA: 0x001201B8 File Offset: 0x0011E3B8
	// (set) Token: 0x060035BE RID: 13758 RVA: 0x001201C0 File Offset: 0x0011E3C0
	public int MaxHitPoint
	{
		get
		{
			return this.m_maxHitPoint;
		}
		set
		{
			this.m_maxHitPoint = value;
		}
	}

	// Token: 0x1700078F RID: 1935
	// (get) Token: 0x060035BF RID: 13759 RVA: 0x001201CC File Offset: 0x0011E3CC
	// (set) Token: 0x060035C0 RID: 13760 RVA: 0x001201D4 File Offset: 0x0011E3D4
	public int Status
	{
		get
		{
			return this.m_status;
		}
		set
		{
			this.m_status = value;
		}
	}

	// Token: 0x060035C1 RID: 13761 RVA: 0x001201E0 File Offset: 0x0011E3E0
	public ServerEventRaidBossState.StatusType GetStatusType()
	{
		ServerEventRaidBossState.StatusType result = ServerEventRaidBossState.StatusType.INIT;
		switch (this.m_status)
		{
		case 1:
			result = ServerEventRaidBossState.StatusType.BOSS_ALIVE;
			break;
		case 2:
			result = ServerEventRaidBossState.StatusType.BOSS_ESCAPE;
			break;
		case 3:
			result = ServerEventRaidBossState.StatusType.REWARD;
			break;
		case 4:
			result = ServerEventRaidBossState.StatusType.PROCESS_END;
			break;
		}
		return result;
	}

	// Token: 0x17000790 RID: 1936
	// (get) Token: 0x060035C2 RID: 13762 RVA: 0x00120230 File Offset: 0x0011E430
	// (set) Token: 0x060035C3 RID: 13763 RVA: 0x00120238 File Offset: 0x0011E438
	public DateTime EscapeAt
	{
		get
		{
			return this.m_escapeAt;
		}
		set
		{
			this.m_escapeAt = value;
		}
	}

	// Token: 0x17000791 RID: 1937
	// (get) Token: 0x060035C4 RID: 13764 RVA: 0x00120244 File Offset: 0x0011E444
	// (set) Token: 0x060035C5 RID: 13765 RVA: 0x0012024C File Offset: 0x0011E44C
	public string EncounterName
	{
		get
		{
			return this.m_encounterName;
		}
		set
		{
			this.m_encounterName = value;
		}
	}

	// Token: 0x17000792 RID: 1938
	// (get) Token: 0x060035C6 RID: 13766 RVA: 0x00120258 File Offset: 0x0011E458
	// (set) Token: 0x060035C7 RID: 13767 RVA: 0x00120260 File Offset: 0x0011E460
	public bool Encounter
	{
		get
		{
			return this.m_encounterFlag;
		}
		set
		{
			this.m_encounterFlag = value;
		}
	}

	// Token: 0x17000793 RID: 1939
	// (get) Token: 0x060035C8 RID: 13768 RVA: 0x0012026C File Offset: 0x0011E46C
	// (set) Token: 0x060035C9 RID: 13769 RVA: 0x00120274 File Offset: 0x0011E474
	public bool Crowded
	{
		get
		{
			return this.m_crowdedFlag;
		}
		set
		{
			this.m_crowdedFlag = value;
		}
	}

	// Token: 0x17000794 RID: 1940
	// (get) Token: 0x060035CA RID: 13770 RVA: 0x00120280 File Offset: 0x0011E480
	// (set) Token: 0x060035CB RID: 13771 RVA: 0x00120288 File Offset: 0x0011E488
	public bool Participation
	{
		get
		{
			return this.m_participationFlag;
		}
		set
		{
			this.m_participationFlag = value;
		}
	}

	// Token: 0x060035CC RID: 13772 RVA: 0x00120294 File Offset: 0x0011E494
	public void CopyTo(ServerEventRaidBossState to)
	{
		to.m_raidBossId = this.m_raidBossId;
		to.m_level = this.m_level;
		to.m_rarity = this.m_rarity;
		to.m_hitPoint = this.m_hitPoint;
		to.m_maxHitPoint = this.m_maxHitPoint;
		to.m_status = this.m_status;
		to.m_escapeAt = this.m_escapeAt;
		to.m_encounterName = this.m_encounterName;
		to.m_encounterFlag = this.m_encounterFlag;
		to.m_crowdedFlag = this.m_crowdedFlag;
		to.m_participationFlag = this.m_participationFlag;
	}

	// Token: 0x04002D58 RID: 11608
	private long m_raidBossId;

	// Token: 0x04002D59 RID: 11609
	private int m_level;

	// Token: 0x04002D5A RID: 11610
	private int m_rarity;

	// Token: 0x04002D5B RID: 11611
	private int m_hitPoint;

	// Token: 0x04002D5C RID: 11612
	private int m_maxHitPoint;

	// Token: 0x04002D5D RID: 11613
	private int m_status;

	// Token: 0x04002D5E RID: 11614
	private DateTime m_escapeAt;

	// Token: 0x04002D5F RID: 11615
	private string m_encounterName;

	// Token: 0x04002D60 RID: 11616
	private bool m_encounterFlag;

	// Token: 0x04002D61 RID: 11617
	private bool m_crowdedFlag;

	// Token: 0x04002D62 RID: 11618
	private bool m_participationFlag;

	// Token: 0x020007DE RID: 2014
	public enum StatusType
	{
		// Token: 0x04002D64 RID: 11620
		INIT,
		// Token: 0x04002D65 RID: 11621
		BOSS_ALIVE,
		// Token: 0x04002D66 RID: 11622
		BOSS_ESCAPE,
		// Token: 0x04002D67 RID: 11623
		REWARD,
		// Token: 0x04002D68 RID: 11624
		PROCESS_END
	}
}
