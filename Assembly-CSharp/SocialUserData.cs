using System;

// Token: 0x02000A13 RID: 2579
public class SocialUserData
{
	// Token: 0x0600442B RID: 17451 RVA: 0x00160078 File Offset: 0x0015E278
	public SocialUserData()
	{
		this.m_id = string.Empty;
		this.m_name = string.Empty;
		this.m_url = string.Empty;
		this.m_customData = new SocialUserCustomData();
	}

	// Token: 0x1700093C RID: 2364
	// (get) Token: 0x0600442C RID: 17452 RVA: 0x001600B8 File Offset: 0x0015E2B8
	// (set) Token: 0x0600442D RID: 17453 RVA: 0x001600C0 File Offset: 0x0015E2C0
	public string Id
	{
		get
		{
			return this.m_id;
		}
		set
		{
			this.m_id = value;
		}
	}

	// Token: 0x1700093D RID: 2365
	// (get) Token: 0x0600442E RID: 17454 RVA: 0x001600CC File Offset: 0x0015E2CC
	// (set) Token: 0x0600442F RID: 17455 RVA: 0x001600D4 File Offset: 0x0015E2D4
	public string Name
	{
		get
		{
			return this.m_name;
		}
		set
		{
			this.m_name = value;
		}
	}

	// Token: 0x1700093E RID: 2366
	// (get) Token: 0x06004430 RID: 17456 RVA: 0x001600E0 File Offset: 0x0015E2E0
	// (set) Token: 0x06004431 RID: 17457 RVA: 0x001600E8 File Offset: 0x0015E2E8
	public string Url
	{
		get
		{
			return this.m_url;
		}
		set
		{
			this.m_url = value;
		}
	}

	// Token: 0x1700093F RID: 2367
	// (get) Token: 0x06004432 RID: 17458 RVA: 0x001600F4 File Offset: 0x0015E2F4
	// (set) Token: 0x06004433 RID: 17459 RVA: 0x001600FC File Offset: 0x0015E2FC
	public bool IsSilhouette
	{
		get
		{
			return this.m_isSilhouette;
		}
		set
		{
			this.m_isSilhouette = value;
		}
	}

	// Token: 0x17000940 RID: 2368
	// (get) Token: 0x06004434 RID: 17460 RVA: 0x00160108 File Offset: 0x0015E308
	// (set) Token: 0x06004435 RID: 17461 RVA: 0x00160110 File Offset: 0x0015E310
	public SocialUserCustomData CustomData
	{
		get
		{
			return this.m_customData;
		}
		set
		{
			this.m_customData = value;
		}
	}

	// Token: 0x0400397D RID: 14717
	private string m_id;

	// Token: 0x0400397E RID: 14718
	private string m_name;

	// Token: 0x0400397F RID: 14719
	private string m_url;

	// Token: 0x04003980 RID: 14720
	private bool m_isSilhouette;

	// Token: 0x04003981 RID: 14721
	private SocialUserCustomData m_customData;
}
