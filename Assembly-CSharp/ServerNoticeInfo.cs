using System;
using System.Collections.Generic;
using SaveData;

// Token: 0x02000811 RID: 2065
public class ServerNoticeInfo
{
	// Token: 0x06003757 RID: 14167 RVA: 0x00123E38 File Offset: 0x00122038
	public ServerNoticeInfo()
	{
		this.m_noticeItems = new List<NetNoticeItem>();
		this.m_rouletteItems = new List<NetNoticeItem>();
		this.m_eventItems = new List<NetNoticeItem>();
		this.m_isGetNoticeInfo = false;
		this.m_isShowedNoticeInfo = false;
	}

	// Token: 0x17000825 RID: 2085
	// (get) Token: 0x06003758 RID: 14168 RVA: 0x00123E88 File Offset: 0x00122088
	// (set) Token: 0x06003759 RID: 14169 RVA: 0x00123E90 File Offset: 0x00122090
	public bool m_isGetNoticeInfo { get; set; }

	// Token: 0x17000826 RID: 2086
	// (get) Token: 0x0600375A RID: 14170 RVA: 0x00123E9C File Offset: 0x0012209C
	// (set) Token: 0x0600375B RID: 14171 RVA: 0x00123EA4 File Offset: 0x001220A4
	public bool m_isShowedNoticeInfo { get; set; }

	// Token: 0x17000827 RID: 2087
	// (get) Token: 0x0600375C RID: 14172 RVA: 0x00123EB0 File Offset: 0x001220B0
	// (set) Token: 0x0600375D RID: 14173 RVA: 0x00123EB8 File Offset: 0x001220B8
	public List<NetNoticeItem> m_noticeItems { get; set; }

	// Token: 0x17000828 RID: 2088
	// (get) Token: 0x0600375E RID: 14174 RVA: 0x00123EC4 File Offset: 0x001220C4
	// (set) Token: 0x0600375F RID: 14175 RVA: 0x00123ECC File Offset: 0x001220CC
	public List<NetNoticeItem> m_rouletteItems { get; set; }

	// Token: 0x17000829 RID: 2089
	// (get) Token: 0x06003760 RID: 14176 RVA: 0x00123ED8 File Offset: 0x001220D8
	// (set) Token: 0x06003761 RID: 14177 RVA: 0x00123EE0 File Offset: 0x001220E0
	public List<NetNoticeItem> m_eventItems { get; set; }

	// Token: 0x1700082A RID: 2090
	// (get) Token: 0x06003762 RID: 14178 RVA: 0x00123EEC File Offset: 0x001220EC
	// (set) Token: 0x06003763 RID: 14179 RVA: 0x00123EF4 File Offset: 0x001220F4
	public DateTime LastUpdateInfoTime
	{
		get
		{
			return this.m_lastUpdateInfoTime;
		}
		set
		{
			this.m_lastUpdateInfoTime = value;
		}
	}

	// Token: 0x06003764 RID: 14180 RVA: 0x00123F00 File Offset: 0x00122100
	public bool IsNeedUpdateInfo()
	{
		DateTime currentTime = NetUtil.GetCurrentTime();
		TimeSpan t = currentTime - this.m_lastUpdateInfoTime;
		TimeSpan t2 = new TimeSpan(1, 0, 0);
		return t >= t2;
	}

	// Token: 0x06003765 RID: 14181 RVA: 0x00123F3C File Offset: 0x0012213C
	public bool IsAllChecked()
	{
		if (!this.m_isGetNoticeInfo)
		{
			return true;
		}
		if (this.m_isShowedNoticeInfo)
		{
			return true;
		}
		bool flag = true;
		int count = this.m_noticeItems.Count;
		for (int i = 0; i < count; i++)
		{
			flag &= this.IsChecked(this.m_noticeItems[i]);
		}
		return flag;
	}

	// Token: 0x06003766 RID: 14182 RVA: 0x00123F9C File Offset: 0x0012219C
	public NetNoticeItem GetInfo(int index)
	{
		NetNoticeItem result = null;
		if (this.m_noticeItems.Count > index)
		{
			result = this.m_noticeItems[index];
		}
		return result;
	}

	// Token: 0x06003767 RID: 14183 RVA: 0x00123FCC File Offset: 0x001221CC
	public void Clear()
	{
		this.m_noticeItems.Clear();
		this.m_rouletteItems.Clear();
		this.m_eventItems.Clear();
		this.m_isGetNoticeInfo = false;
	}

	// Token: 0x06003768 RID: 14184 RVA: 0x00124004 File Offset: 0x00122204
	public bool IsChecked(NetNoticeItem item)
	{
		bool result = false;
		InformationSaveManager instance = InformationSaveManager.Instance;
		if (instance != null)
		{
			InformationData informationData = instance.GetInformationData();
			if (informationData != null && item != null)
			{
				string data = informationData.GetData(item.Id.ToString(), InformationData.DataType.SHOWED_TIME);
				if (data != InformationData.INVALID_PARAM)
				{
					if (item.IsEveryDay())
					{
						result = true;
						DateTime localDateTime = NetUtil.GetLocalDateTime(long.Parse(data));
						DateTime localDateTime2 = NetUtil.GetLocalDateTime((long)NetUtil.GetCurrentUnixTime());
						if (localDateTime.Day != localDateTime2.Day)
						{
							result = false;
						}
						if (localDateTime.Month != localDateTime2.Month)
						{
							result = false;
						}
						if (localDateTime.Year != localDateTime2.Year)
						{
							result = false;
						}
					}
					else if (item.IsOnce())
					{
						if (item.Id == (long)NetNoticeItem.OPERATORINFO_RANKINGRESULT_ID)
						{
							string data2 = informationData.GetData(item.Id.ToString(), InformationData.DataType.ADD_INFO);
							result = (item.SaveKey == data2);
						}
						else if (item.Id == (long)NetNoticeItem.OPERATORINFO_EVENTRANKINGRESULT_ID)
						{
							string eventRankingData = informationData.GetEventRankingData(item.Id.ToString(), item.SaveKey, InformationData.DataType.ADD_INFO);
							result = (item.SaveKey == eventRankingData);
						}
						else if (item.Id == (long)NetNoticeItem.OPERATORINFO_QUICKRANKINGRESULT_ID)
						{
							string data3 = informationData.GetData(item.Id.ToString(), InformationData.DataType.ADD_INFO);
							result = (item.SaveKey == data3);
						}
						else
						{
							result = true;
						}
					}
					else if (item.IsOnlyInformationPage())
					{
						result = true;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06003769 RID: 14185 RVA: 0x001241A4 File Offset: 0x001223A4
	public bool IsCheckedForMenuIcon(NetNoticeItem item)
	{
		InformationSaveManager instance = InformationSaveManager.Instance;
		if (instance != null)
		{
			InformationData informationData = instance.GetInformationData();
			if (informationData != null && item != null)
			{
				string data = informationData.GetData(item.Id.ToString(), InformationData.DataType.SHOWED_TIME);
				if (data != InformationData.INVALID_PARAM)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600376A RID: 14186 RVA: 0x00124200 File Offset: 0x00122400
	public bool IsOnTime(NetNoticeItem item)
	{
		if (item != null)
		{
			long num = (long)NetUtil.GetCurrentUnixTime();
			if (num >= item.Start && item.End > num)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600376B RID: 14187 RVA: 0x00124238 File Offset: 0x00122438
	public void UpdateChecked(NetNoticeItem item)
	{
		if (item != null)
		{
			InformationSaveManager instance = InformationSaveManager.Instance;
			if (instance != null)
			{
				InformationData informationData = instance.GetInformationData();
				if (informationData != null)
				{
					long num = (long)NetUtil.GetCurrentUnixTime();
					if (item.Id == (long)NetNoticeItem.OPERATORINFO_RANKINGRESULT_ID)
					{
						informationData.UpdateShowedTime(item.Id.ToString(), num.ToString(), item.SaveKey, "-1");
					}
					else if (item.Id == (long)NetNoticeItem.OPERATORINFO_EVENTRANKINGRESULT_ID)
					{
						informationData.UpdateEventRankingShowedTime(item.Id.ToString(), num.ToString(), item.SaveKey, item.ImageId);
					}
					else if (item.Id == (long)NetNoticeItem.OPERATORINFO_QUICKRANKINGRESULT_ID)
					{
						informationData.UpdateShowedTime(item.Id.ToString(), num.ToString(), item.SaveKey, "-1");
					}
					else
					{
						informationData.UpdateShowedTime(item.Id.ToString(), num.ToString(), item.End.ToString(), item.ImageId);
					}
				}
			}
		}
	}

	// Token: 0x0600376C RID: 14188 RVA: 0x0012435C File Offset: 0x0012255C
	public void SaveInformation()
	{
		InformationSaveManager instance = InformationSaveManager.Instance;
		if (instance != null)
		{
			instance.SaveInformationData();
		}
	}

	// Token: 0x04002EB4 RID: 11956
	private DateTime m_lastUpdateInfoTime = DateTime.MinValue;
}
