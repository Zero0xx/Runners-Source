using System;
using System.Collections.Generic;
using LitJson;
using SaveData;

// Token: 0x02000750 RID: 1872
public class NetServerGetNoticeInfo : NetBase
{
	// Token: 0x060031CD RID: 12749 RVA: 0x00117DC4 File Offset: 0x00115FC4
	public NetServerGetNoticeInfo()
	{
		this.m_noticeItems = new List<NetNoticeItem>();
		base.SetSecureFlag(false);
	}

	// Token: 0x060031CE RID: 12750 RVA: 0x00117DE0 File Offset: 0x00115FE0
	protected override void DoRequest()
	{
		base.SetAction("login/getInformation");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string onlySendBaseParamString = instance.GetOnlySendBaseParamString();
			base.WriteJsonString(onlySendBaseParamString);
		}
	}

	// Token: 0x060031CF RID: 12751 RVA: 0x00117E18 File Offset: 0x00116018
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.m_noticeItems.Clear();
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "informations");
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			JsonData jdata2 = jsonArray[i];
			long jsonLong = NetUtil.GetJsonLong(jdata2, "id");
			int jsonInt = NetUtil.GetJsonInt(jdata2, "priority");
			long jsonLong2 = NetUtil.GetJsonLong(jdata2, "start");
			long jsonLong3 = NetUtil.GetJsonLong(jdata2, "end");
			string jsonString = NetUtil.GetJsonString(jdata2, "param");
			this.AddInfo(jsonLong, jsonInt, jsonLong2, jsonLong3, jsonString, string.Empty);
		}
		JsonData jsonArray2 = NetUtil.GetJsonArray(jdata, "operatorEachInfos");
		int count2 = jsonArray2.Count;
		for (int j = 0; j < count2; j++)
		{
			JsonData jdata3 = jsonArray2[j];
			long num = (long)NetNoticeItem.OPERATORINFO_START_ID + NetUtil.GetJsonLong(jdata3, "id");
			int priority = -10000;
			long start = 0L;
			long end = 0L;
			string jsonString2 = NetUtil.GetJsonString(jdata3, "content");
			string saveKey = string.Empty;
			string param = "1_" + jsonString2 + "_0_0_url";
			if (num == (long)NetNoticeItem.OPERATORINFO_RANKINGRESULT_ID)
			{
				priority = -10001;
				string[] array = jsonString2.Split(new char[]
				{
					','
				});
				if (array.Length >= 10)
				{
					saveKey = array[5] + array[6];
				}
			}
			if (num == (long)NetNoticeItem.OPERATORINFO_QUICKRANKINGRESULT_ID)
			{
				priority = -10002;
				string[] array2 = jsonString2.Split(new char[]
				{
					','
				});
				if (array2.Length >= 10)
				{
					saveKey = array2[5] + array2[6] + "QUICK";
				}
			}
			else if (num == (long)NetNoticeItem.OPERATORINFO_EVENTRANKINGRESULT_ID)
			{
				priority = -10003;
				string text = "0";
				EventRankingServerInfoConverter eventRankingServerInfoConverter = new EventRankingServerInfoConverter(jsonString2);
				if (eventRankingServerInfoConverter != null)
				{
					if (eventRankingServerInfoConverter.eventId > 0)
					{
						text = "EventResult" + eventRankingServerInfoConverter.eventId.ToString();
					}
					saveKey = eventRankingServerInfoConverter.eventId.ToString();
				}
				param = string.Concat(new string[]
				{
					"1_",
					jsonString2,
					"_",
					text,
					"_0_url"
				});
			}
			this.AddInfo(num, priority, start, end, param, saveKey);
		}
		ServerInterface.PlayerState.m_numUnreadMessages += NetUtil.GetJsonInt(jdata, "numOperatorInfo");
		this.UpdateInformationData();
	}

	// Token: 0x060031D0 RID: 12752 RVA: 0x00118090 File Offset: 0x00116290
	protected override void DoDidSuccessEmulation()
	{
		long unixTime = NetUtil.GetUnixTime(new DateTime(2014, 9, 1, 14, 0, 0, 0));
		long unixTime2 = NetUtil.GetUnixTime(new DateTime(2016, 11, 11, 14, 0, 0, 0));
		this.AddInfo((long)NetNoticeItem.OPERATORINFO_RANKINGRESULT_ID, -10001, 0L, 0L, "0_1,2,3,4,1,100000,900000,1,1,9876,1,1_0_0", unixTime.ToString() + unixTime2.ToString());
	}

	// Token: 0x060031D1 RID: 12753 RVA: 0x001180FC File Offset: 0x001162FC
	public NetNoticeItem GetInfo(int index)
	{
		if (index < this.m_noticeItems.Count)
		{
			return this.m_noticeItems[index];
		}
		return null;
	}

	// Token: 0x060031D2 RID: 12754 RVA: 0x00118120 File Offset: 0x00116320
	public int GetInfoCount()
	{
		return this.m_noticeItems.Count;
	}

	// Token: 0x060031D3 RID: 12755 RVA: 0x00118130 File Offset: 0x00116330
	private void UpdateInformationData()
	{
		InformationSaveManager instance = InformationSaveManager.Instance;
		if (instance != null)
		{
			InformationData informationData = instance.GetInformationData();
			int num = informationData.DataCount();
			for (int i = 0; i < num; i++)
			{
				string data = informationData.GetData(i, InformationData.DataType.ID);
				if (data != InformationData.INVALID_PARAM)
				{
					bool flag = false;
					if (long.Parse(data) == (long)NetNoticeItem.OPERATORINFO_EVENTRANKINGRESULT_ID)
					{
						foreach (NetNoticeItem netNoticeItem in this.m_noticeItems)
						{
							if (netNoticeItem.SaveKey == informationData.GetData(i, InformationData.DataType.ADD_INFO))
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							InformationImageManager instance2 = InformationImageManager.Instance;
							if (instance2 != null)
							{
								string data2 = informationData.GetData(i, InformationData.DataType.IMAGE_ID);
								instance2.DeleteImageData(data2);
							}
							informationData.Reset(i);
						}
					}
					else
					{
						foreach (NetNoticeItem netNoticeItem2 in this.m_noticeItems)
						{
							if (netNoticeItem2.Id.ToString() == data)
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							informationData.Reset(i);
						}
					}
				}
			}
			instance.SaveInformationData();
		}
	}

	// Token: 0x060031D4 RID: 12756 RVA: 0x001182D8 File Offset: 0x001164D8
	private void AddInfo(long id, int priority, long start, long end, string param, string saveKey)
	{
		NetNoticeItem netNoticeItem = new NetNoticeItem();
		netNoticeItem.Init(id, priority, start, end, param, saveKey);
		this.m_noticeItems.Add(netNoticeItem);
	}

	// Token: 0x04002B49 RID: 11081
	private List<NetNoticeItem> m_noticeItems;
}
