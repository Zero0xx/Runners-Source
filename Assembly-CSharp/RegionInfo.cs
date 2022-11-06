using System;

// Token: 0x020009D0 RID: 2512
public class RegionInfo
{
	// Token: 0x060041F6 RID: 16886 RVA: 0x00156DFC File Offset: 0x00154FFC
	public RegionInfo(int countryId, string countryCode, string area, string limit)
	{
		this.m_countryId = countryId;
		this.m_countryCode = countryCode;
		this.m_area = area;
		this.m_limit = limit;
	}

	// Token: 0x170008F4 RID: 2292
	// (get) Token: 0x060041F7 RID: 16887 RVA: 0x00156E24 File Offset: 0x00155024
	// (set) Token: 0x060041F8 RID: 16888 RVA: 0x00156E2C File Offset: 0x0015502C
	public int CountryId
	{
		get
		{
			return this.m_countryId;
		}
		private set
		{
		}
	}

	// Token: 0x170008F5 RID: 2293
	// (get) Token: 0x060041F9 RID: 16889 RVA: 0x00156E30 File Offset: 0x00155030
	// (set) Token: 0x060041FA RID: 16890 RVA: 0x00156E38 File Offset: 0x00155038
	public string CountryCode
	{
		get
		{
			return this.m_countryCode;
		}
		private set
		{
		}
	}

	// Token: 0x170008F6 RID: 2294
	// (get) Token: 0x060041FB RID: 16891 RVA: 0x00156E3C File Offset: 0x0015503C
	// (set) Token: 0x060041FC RID: 16892 RVA: 0x00156E44 File Offset: 0x00155044
	public string Area
	{
		get
		{
			return this.m_area;
		}
		private set
		{
		}
	}

	// Token: 0x170008F7 RID: 2295
	// (get) Token: 0x060041FD RID: 16893 RVA: 0x00156E48 File Offset: 0x00155048
	// (set) Token: 0x060041FE RID: 16894 RVA: 0x00156E50 File Offset: 0x00155050
	public string Limit
	{
		get
		{
			return this.m_limit;
		}
		private set
		{
		}
	}

	// Token: 0x04003837 RID: 14391
	private int m_countryId;

	// Token: 0x04003838 RID: 14392
	private string m_countryCode;

	// Token: 0x04003839 RID: 14393
	private string m_area;

	// Token: 0x0400383A RID: 14394
	private string m_limit;
}
