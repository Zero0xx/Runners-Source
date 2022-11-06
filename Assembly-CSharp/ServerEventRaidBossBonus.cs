using System;

// Token: 0x020007DB RID: 2011
public class ServerEventRaidBossBonus
{
	// Token: 0x06003588 RID: 13704 RVA: 0x0011FDAC File Offset: 0x0011DFAC
	public ServerEventRaidBossBonus()
	{
		this.m_encounterBonus = 0;
		this.m_wrestleBonus = 0;
		this.m_damageRateBonus = 0;
		this.m_damageTopBonus = 0;
		this.m_beatBonus = 0;
	}

	// Token: 0x17000776 RID: 1910
	// (get) Token: 0x06003589 RID: 13705 RVA: 0x0011FDD8 File Offset: 0x0011DFD8
	// (set) Token: 0x0600358A RID: 13706 RVA: 0x0011FDE0 File Offset: 0x0011DFE0
	public int EncounterBonus
	{
		get
		{
			return this.m_encounterBonus;
		}
		set
		{
			this.m_encounterBonus = value;
		}
	}

	// Token: 0x17000777 RID: 1911
	// (get) Token: 0x0600358B RID: 13707 RVA: 0x0011FDEC File Offset: 0x0011DFEC
	// (set) Token: 0x0600358C RID: 13708 RVA: 0x0011FDF4 File Offset: 0x0011DFF4
	public int WrestleBonus
	{
		get
		{
			return this.m_wrestleBonus;
		}
		set
		{
			this.m_wrestleBonus = value;
		}
	}

	// Token: 0x17000778 RID: 1912
	// (get) Token: 0x0600358D RID: 13709 RVA: 0x0011FE00 File Offset: 0x0011E000
	// (set) Token: 0x0600358E RID: 13710 RVA: 0x0011FE08 File Offset: 0x0011E008
	public int DamageRateBonus
	{
		get
		{
			return this.m_damageRateBonus;
		}
		set
		{
			this.m_damageRateBonus = value;
		}
	}

	// Token: 0x17000779 RID: 1913
	// (get) Token: 0x0600358F RID: 13711 RVA: 0x0011FE14 File Offset: 0x0011E014
	// (set) Token: 0x06003590 RID: 13712 RVA: 0x0011FE1C File Offset: 0x0011E01C
	public int DamageTopBonus
	{
		get
		{
			return this.m_damageTopBonus;
		}
		set
		{
			this.m_damageTopBonus = value;
		}
	}

	// Token: 0x1700077A RID: 1914
	// (get) Token: 0x06003591 RID: 13713 RVA: 0x0011FE28 File Offset: 0x0011E028
	// (set) Token: 0x06003592 RID: 13714 RVA: 0x0011FE30 File Offset: 0x0011E030
	public int BeatBonus
	{
		get
		{
			return this.m_beatBonus;
		}
		set
		{
			this.m_beatBonus = value;
		}
	}

	// Token: 0x06003593 RID: 13715 RVA: 0x0011FE3C File Offset: 0x0011E03C
	public void CopyTo(ServerEventRaidBossBonus to)
	{
		to.m_encounterBonus = this.m_encounterBonus;
		to.m_wrestleBonus = this.m_wrestleBonus;
		to.m_damageRateBonus = this.m_damageRateBonus;
		to.m_damageTopBonus = this.m_damageTopBonus;
		to.m_beatBonus = this.m_beatBonus;
	}

	// Token: 0x04002D44 RID: 11588
	private int m_encounterBonus;

	// Token: 0x04002D45 RID: 11589
	private int m_wrestleBonus;

	// Token: 0x04002D46 RID: 11590
	private int m_damageRateBonus;

	// Token: 0x04002D47 RID: 11591
	private int m_damageTopBonus;

	// Token: 0x04002D48 RID: 11592
	private int m_beatBonus;
}
