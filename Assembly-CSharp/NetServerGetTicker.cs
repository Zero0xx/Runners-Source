using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x02000752 RID: 1874
public class NetServerGetTicker : NetBase
{
	// Token: 0x060031E0 RID: 12768 RVA: 0x001183C8 File Offset: 0x001165C8
	public NetServerGetTicker()
	{
		this.m_tickerData = new List<ServerTickerData>();
	}

	// Token: 0x060031E1 RID: 12769 RVA: 0x001183DC File Offset: 0x001165DC
	protected override void DoRequest()
	{
		base.SetAction("login/getTicker");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string onlySendBaseParamString = instance.GetOnlySendBaseParamString();
			base.WriteJsonString(onlySendBaseParamString);
		}
	}

	// Token: 0x060031E2 RID: 12770 RVA: 0x00118414 File Offset: 0x00116614
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.m_tickerData.Clear();
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "tickerList");
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			JsonData jdata2 = jsonArray[i];
			long jsonLong = NetUtil.GetJsonLong(jdata2, "id");
			long jsonLong2 = NetUtil.GetJsonLong(jdata2, "start");
			long jsonLong3 = NetUtil.GetJsonLong(jdata2, "end");
			string jsonString = NetUtil.GetJsonString(jdata2, "param");
			this.AddInfo(jsonLong, jsonLong2, jsonLong3, jsonString);
		}
	}

	// Token: 0x060031E3 RID: 12771 RVA: 0x0011849C File Offset: 0x0011669C
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x060031E4 RID: 12772 RVA: 0x001184A0 File Offset: 0x001166A0
	public ServerTickerData GetInfo(int index)
	{
		if (index < this.m_tickerData.Count)
		{
			return this.m_tickerData[index];
		}
		return null;
	}

	// Token: 0x060031E5 RID: 12773 RVA: 0x001184C4 File Offset: 0x001166C4
	public int GetInfoCount()
	{
		return this.m_tickerData.Count;
	}

	// Token: 0x060031E6 RID: 12774 RVA: 0x001184D4 File Offset: 0x001166D4
	private void AddInfo(long id, long start, long end, string param)
	{
		ServerTickerData serverTickerData = new ServerTickerData();
		serverTickerData.Init(id, start, end, param);
		this.m_tickerData.Add(serverTickerData);
	}

	// Token: 0x04002B4C RID: 11084
	private List<ServerTickerData> m_tickerData;
}
