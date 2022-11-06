using System;

// Token: 0x020007EC RID: 2028
public class ServerCampaignData
{
	// Token: 0x0600362C RID: 13868 RVA: 0x00120B5C File Offset: 0x0011ED5C
	public ServerCampaignData()
	{
		this.campaignType = Constants.Campaign.emType.BankedRingBonus;
		this.id = 0;
		this.beginDate = 0L;
		this.endDate = 0L;
		this.iContent = 0;
	}

	// Token: 0x170007BC RID: 1980
	// (get) Token: 0x0600362D RID: 13869 RVA: 0x00120B8C File Offset: 0x0011ED8C
	public float fContent
	{
		get
		{
			return (float)this.iContent / ServerCampaignData.fContentBasis;
		}
	}

	// Token: 0x170007BD RID: 1981
	// (get) Token: 0x0600362E RID: 13870 RVA: 0x00120B9C File Offset: 0x0011ED9C
	public static float fContentBasis
	{
		get
		{
			return 1000f;
		}
	}

	// Token: 0x170007BE RID: 1982
	// (get) Token: 0x0600362F RID: 13871 RVA: 0x00120BA4 File Offset: 0x0011EDA4
	public float fSubContent
	{
		get
		{
			return (float)this.iSubContent / 1000f;
		}
	}

	// Token: 0x06003630 RID: 13872 RVA: 0x00120BB4 File Offset: 0x0011EDB4
	public void CopyTo(ServerCampaignData to)
	{
		to.campaignType = this.campaignType;
		to.id = this.id;
		to.beginDate = this.beginDate;
		to.endDate = this.endDate;
		to.iContent = this.iContent;
	}

	// Token: 0x06003631 RID: 13873 RVA: 0x00120C00 File Offset: 0x0011EE00
	public bool InSession()
	{
		DateTime t = (this.beginDate == 0L) ? DateTime.MinValue : NetUtil.GetLocalDateTime(this.beginDate);
		DateTime t2 = (this.endDate == 0L) ? DateTime.MaxValue : NetUtil.GetLocalDateTime(this.endDate);
		DateTime currentTime = NetUtil.GetCurrentTime();
		return currentTime >= t && currentTime <= t2;
	}

	// Token: 0x04002DB3 RID: 11699
	public Constants.Campaign.emType campaignType;

	// Token: 0x04002DB4 RID: 11700
	public int id;

	// Token: 0x04002DB5 RID: 11701
	public long beginDate;

	// Token: 0x04002DB6 RID: 11702
	public long endDate;

	// Token: 0x04002DB7 RID: 11703
	public int iContent;

	// Token: 0x04002DB8 RID: 11704
	public int iSubContent;
}
