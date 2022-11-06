using System;

// Token: 0x020007E2 RID: 2018
public class ServerEventUserRaidBossState
{
	// Token: 0x060035FF RID: 13823 RVA: 0x001206D4 File Offset: 0x0011E8D4
	public ServerEventUserRaidBossState()
	{
		this.m_numRaidBossRings = 0;
		this.m_raidBossEnergy = 0;
		this.m_raidBossEnergyBuy = 0;
		this.m_numBeatedEncounter = 0;
		this.m_numBeatedEnterprise = 0;
		this.m_numRaidBossEncountered = 0;
		this.m_energyRenewsAt = DateTime.MinValue;
	}

	// Token: 0x170007AB RID: 1963
	// (get) Token: 0x06003600 RID: 13824 RVA: 0x00120714 File Offset: 0x0011E914
	// (set) Token: 0x06003601 RID: 13825 RVA: 0x0012071C File Offset: 0x0011E91C
	public int NumRaidbossRings
	{
		get
		{
			return this.m_numRaidBossRings;
		}
		set
		{
			this.m_numRaidBossRings = value;
		}
	}

	// Token: 0x170007AC RID: 1964
	// (get) Token: 0x06003602 RID: 13826 RVA: 0x00120728 File Offset: 0x0011E928
	// (set) Token: 0x06003603 RID: 13827 RVA: 0x00120730 File Offset: 0x0011E930
	public int RaidBossEnergy
	{
		get
		{
			return this.m_raidBossEnergy;
		}
		set
		{
			this.m_raidBossEnergy = value;
		}
	}

	// Token: 0x170007AD RID: 1965
	// (get) Token: 0x06003604 RID: 13828 RVA: 0x0012073C File Offset: 0x0011E93C
	// (set) Token: 0x06003605 RID: 13829 RVA: 0x00120744 File Offset: 0x0011E944
	public int RaidbossEnergyBuy
	{
		get
		{
			return this.m_raidBossEnergyBuy;
		}
		set
		{
			this.m_raidBossEnergyBuy = value;
		}
	}

	// Token: 0x170007AE RID: 1966
	// (get) Token: 0x06003606 RID: 13830 RVA: 0x00120750 File Offset: 0x0011E950
	public int RaidBossEnergyCount
	{
		get
		{
			return this.m_raidBossEnergy + this.m_raidBossEnergyBuy;
		}
	}

	// Token: 0x170007AF RID: 1967
	// (get) Token: 0x06003607 RID: 13831 RVA: 0x00120760 File Offset: 0x0011E960
	// (set) Token: 0x06003608 RID: 13832 RVA: 0x00120768 File Offset: 0x0011E968
	public int NumBeatedEncounter
	{
		get
		{
			return this.m_numBeatedEncounter;
		}
		set
		{
			this.m_numBeatedEncounter = value;
		}
	}

	// Token: 0x170007B0 RID: 1968
	// (get) Token: 0x06003609 RID: 13833 RVA: 0x00120774 File Offset: 0x0011E974
	// (set) Token: 0x0600360A RID: 13834 RVA: 0x0012077C File Offset: 0x0011E97C
	public int NumBeatedEnterprise
	{
		get
		{
			return this.m_numBeatedEnterprise;
		}
		set
		{
			this.m_numBeatedEnterprise = value;
		}
	}

	// Token: 0x170007B1 RID: 1969
	// (get) Token: 0x0600360B RID: 13835 RVA: 0x00120788 File Offset: 0x0011E988
	// (set) Token: 0x0600360C RID: 13836 RVA: 0x00120790 File Offset: 0x0011E990
	public int NumRaidBossEncountered
	{
		get
		{
			return this.m_numRaidBossEncountered;
		}
		set
		{
			this.m_numRaidBossEncountered = value;
		}
	}

	// Token: 0x170007B2 RID: 1970
	// (get) Token: 0x0600360D RID: 13837 RVA: 0x0012079C File Offset: 0x0011E99C
	// (set) Token: 0x0600360E RID: 13838 RVA: 0x001207A4 File Offset: 0x0011E9A4
	public DateTime EnergyRenewsAt
	{
		get
		{
			return this.m_energyRenewsAt;
		}
		set
		{
			this.m_energyRenewsAt = value;
		}
	}

	// Token: 0x0600360F RID: 13839 RVA: 0x001207B0 File Offset: 0x0011E9B0
	public void CopyTo(ServerEventUserRaidBossState to)
	{
		to.m_numRaidBossRings = this.m_numRaidBossRings;
		to.m_raidBossEnergy = this.m_raidBossEnergy;
		to.m_raidBossEnergyBuy = this.m_raidBossEnergyBuy;
		to.m_numBeatedEncounter = this.m_numBeatedEncounter;
		to.m_numBeatedEnterprise = this.m_numBeatedEnterprise;
		to.m_numRaidBossEncountered = this.m_numRaidBossEncountered;
		to.m_energyRenewsAt = this.m_energyRenewsAt;
	}

	// Token: 0x04002D7F RID: 11647
	private int m_numRaidBossRings;

	// Token: 0x04002D80 RID: 11648
	private int m_raidBossEnergy;

	// Token: 0x04002D81 RID: 11649
	private int m_raidBossEnergyBuy;

	// Token: 0x04002D82 RID: 11650
	private int m_numBeatedEncounter;

	// Token: 0x04002D83 RID: 11651
	private int m_numBeatedEnterprise;

	// Token: 0x04002D84 RID: 11652
	private int m_numRaidBossEncountered;

	// Token: 0x04002D85 RID: 11653
	private DateTime m_energyRenewsAt;
}
