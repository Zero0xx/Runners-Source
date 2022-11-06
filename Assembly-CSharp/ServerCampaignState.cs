using System;
using System.Collections.Generic;

// Token: 0x020007ED RID: 2029
public class ServerCampaignState
{
	// Token: 0x06003632 RID: 13874 RVA: 0x00120C6C File Offset: 0x0011EE6C
	public ServerCampaignState()
	{
		this.m_dic = new Dictionary<Constants.Campaign.emType, List<ServerCampaignData>>();
	}

	// Token: 0x06003633 RID: 13875 RVA: 0x00120C80 File Offset: 0x0011EE80
	public bool InSession(int id)
	{
		return this.InSession(id, 0);
	}

	// Token: 0x06003634 RID: 13876 RVA: 0x00120C8C File Offset: 0x0011EE8C
	public bool InSession(int id, int index)
	{
		ServerCampaignData campaign = this.GetCampaign(id, index);
		return campaign != null && campaign.InSession();
	}

	// Token: 0x06003635 RID: 13877 RVA: 0x00120CB0 File Offset: 0x0011EEB0
	public bool InAnyIdSession(Constants.Campaign.emType campaignType)
	{
		ServerCampaignData anyIdCampaign = this.GetAnyIdCampaign(campaignType);
		return anyIdCampaign != null && anyIdCampaign.InSession();
	}

	// Token: 0x06003636 RID: 13878 RVA: 0x00120CD4 File Offset: 0x0011EED4
	public bool InSession(Constants.Campaign.emType campaignType)
	{
		return this.InSession(campaignType, -1);
	}

	// Token: 0x06003637 RID: 13879 RVA: 0x00120CE0 File Offset: 0x0011EEE0
	public bool InSession(Constants.Campaign.emType campaignType, int id)
	{
		ServerCampaignData campaign = this.GetCampaign(campaignType, id);
		return campaign != null && campaign.InSession();
	}

	// Token: 0x06003638 RID: 13880 RVA: 0x00120D04 File Offset: 0x0011EF04
	public ServerCampaignData GetCampaign(int id)
	{
		return this.GetCampaign(id, 0);
	}

	// Token: 0x06003639 RID: 13881 RVA: 0x00120D10 File Offset: 0x0011EF10
	public ServerCampaignData GetCampaign(int id, int index)
	{
		int num = this.CampaignCount(id);
		if (0 > index || num <= index)
		{
			return null;
		}
		int num2 = 0;
		foreach (KeyValuePair<Constants.Campaign.emType, List<ServerCampaignData>> keyValuePair in this.m_dic)
		{
			int count = keyValuePair.Value.Count;
			int i = 0;
			while (i < count)
			{
				ServerCampaignData serverCampaignData = keyValuePair.Value[i];
				if (serverCampaignData.id == id)
				{
					if (num2 == index)
					{
						return serverCampaignData;
					}
					num2++;
					break;
				}
				else
				{
					i++;
				}
			}
		}
		return null;
	}

	// Token: 0x0600363A RID: 13882 RVA: 0x00120DE8 File Offset: 0x0011EFE8
	public ServerCampaignData GetAnyIdCampaign(Constants.Campaign.emType campaignType)
	{
		return this.GetCampaign(campaignType, -1);
	}

	// Token: 0x0600363B RID: 13883 RVA: 0x00120DF4 File Offset: 0x0011EFF4
	public ServerCampaignData GetCampaign(Constants.Campaign.emType campaignType)
	{
		return this.GetCampaign(campaignType, -1);
	}

	// Token: 0x0600363C RID: 13884 RVA: 0x00120E00 File Offset: 0x0011F000
	public ServerCampaignData GetCampaign(Constants.Campaign.emType campaignType, int id)
	{
		List<ServerCampaignData> list = null;
		if (this.m_dic.TryGetValue(campaignType, out list))
		{
			int count = list.Count;
			if (0 < count)
			{
				for (int i = 0; i < count; i++)
				{
					ServerCampaignData serverCampaignData = list[i];
					if (serverCampaignData.id == id || id == -1)
					{
						return serverCampaignData;
					}
				}
			}
		}
		return null;
	}

	// Token: 0x0600363D RID: 13885 RVA: 0x00120E64 File Offset: 0x0011F064
	public ServerCampaignData GetCampaignInSession(int id)
	{
		int num = this.CampaignCount(id);
		for (int i = 0; i < num; i++)
		{
			ServerCampaignData campaignInSession = this.GetCampaignInSession(id, i);
			if (campaignInSession != null)
			{
				return campaignInSession;
			}
		}
		return null;
	}

	// Token: 0x0600363E RID: 13886 RVA: 0x00120EA0 File Offset: 0x0011F0A0
	public ServerCampaignData GetCampaignInSession(int id, int index)
	{
		if (this.InSession(id, index))
		{
			return this.GetCampaign(id, index);
		}
		return null;
	}

	// Token: 0x0600363F RID: 13887 RVA: 0x00120EBC File Offset: 0x0011F0BC
	public ServerCampaignData GetCampaignInSession(Constants.Campaign.emType campaignType)
	{
		return this.GetCampaignInSession(campaignType, -1);
	}

	// Token: 0x06003640 RID: 13888 RVA: 0x00120EC8 File Offset: 0x0011F0C8
	public ServerCampaignData GetCampaignInSession(Constants.Campaign.emType campaignType, int id)
	{
		if (this.InSession(campaignType, id))
		{
			return this.GetCampaign(campaignType, id);
		}
		return null;
	}

	// Token: 0x06003641 RID: 13889 RVA: 0x00120EE4 File Offset: 0x0011F0E4
	public int CampaignCount(int id)
	{
		int num = 0;
		foreach (KeyValuePair<Constants.Campaign.emType, List<ServerCampaignData>> keyValuePair in this.m_dic)
		{
			int count = keyValuePair.Value.Count;
			for (int i = 0; i < count; i++)
			{
				ServerCampaignData serverCampaignData = keyValuePair.Value[i];
				if (serverCampaignData.id == id)
				{
					num++;
					break;
				}
			}
		}
		return num;
	}

	// Token: 0x06003642 RID: 13890 RVA: 0x00120F90 File Offset: 0x0011F190
	public bool RegistCampaign(ServerCampaignData registData)
	{
		List<ServerCampaignData> list = null;
		if (!this.m_dic.TryGetValue(registData.campaignType, out list))
		{
			list = new List<ServerCampaignData>();
			list.Add(registData);
			this.m_dic.Add(registData.campaignType, list);
		}
		else
		{
			int count = list.Count;
			bool flag = false;
			for (int i = 0; i < count; i++)
			{
				ServerCampaignData serverCampaignData = list[i];
				if (serverCampaignData.id == registData.id && serverCampaignData.beginDate == registData.beginDate && serverCampaignData.endDate == registData.endDate)
				{
					registData.CopyTo(serverCampaignData);
					flag = true;
				}
			}
			if (!flag)
			{
				list.Add(registData);
			}
		}
		return true;
	}

	// Token: 0x06003643 RID: 13891 RVA: 0x00121050 File Offset: 0x0011F250
	public void RemoveCampaign(ServerCampaignData registData)
	{
		List<ServerCampaignData> list = null;
		if (!this.m_dic.TryGetValue(registData.campaignType, out list))
		{
			return;
		}
		List<ServerCampaignData> list2 = new List<ServerCampaignData>();
		int count = list.Count;
		for (int i = 0; i < count; i++)
		{
			ServerCampaignData serverCampaignData = list[i];
			if (serverCampaignData.id == registData.id)
			{
				list2.Add(serverCampaignData);
			}
		}
		foreach (ServerCampaignData serverCampaignData2 in list2)
		{
			if (serverCampaignData2 != null)
			{
				list.Remove(serverCampaignData2);
			}
		}
	}

	// Token: 0x04002DB9 RID: 11705
	private Dictionary<Constants.Campaign.emType, List<ServerCampaignData>> m_dic;
}
