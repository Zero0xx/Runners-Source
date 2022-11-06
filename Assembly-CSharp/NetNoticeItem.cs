using System;

// Token: 0x02000810 RID: 2064
public class NetNoticeItem
{
	// Token: 0x1700081B RID: 2075
	// (get) Token: 0x06003747 RID: 14151 RVA: 0x00123BB8 File Offset: 0x00121DB8
	public long Id
	{
		get
		{
			return this.m_id;
		}
	}

	// Token: 0x1700081C RID: 2076
	// (get) Token: 0x06003748 RID: 14152 RVA: 0x00123BC0 File Offset: 0x00121DC0
	public int Priority
	{
		get
		{
			return this.m_priority;
		}
	}

	// Token: 0x1700081D RID: 2077
	// (get) Token: 0x06003749 RID: 14153 RVA: 0x00123BC8 File Offset: 0x00121DC8
	public long Start
	{
		get
		{
			return this.m_start;
		}
	}

	// Token: 0x1700081E RID: 2078
	// (get) Token: 0x0600374A RID: 14154 RVA: 0x00123BD0 File Offset: 0x00121DD0
	public long End
	{
		get
		{
			return this.m_end;
		}
	}

	// Token: 0x1700081F RID: 2079
	// (get) Token: 0x0600374B RID: 14155 RVA: 0x00123BD8 File Offset: 0x00121DD8
	public int AnnounceType
	{
		get
		{
			return this.m_announceType;
		}
	}

	// Token: 0x17000820 RID: 2080
	// (get) Token: 0x0600374C RID: 14156 RVA: 0x00123BE0 File Offset: 0x00121DE0
	public int WindowType
	{
		get
		{
			return this.m_windowType;
		}
	}

	// Token: 0x17000821 RID: 2081
	// (get) Token: 0x0600374D RID: 14157 RVA: 0x00123BE8 File Offset: 0x00121DE8
	public string ImageId
	{
		get
		{
			return this.m_imageId;
		}
	}

	// Token: 0x17000822 RID: 2082
	// (get) Token: 0x0600374E RID: 14158 RVA: 0x00123BF0 File Offset: 0x00121DF0
	public string Message
	{
		get
		{
			return this.m_message;
		}
	}

	// Token: 0x17000823 RID: 2083
	// (get) Token: 0x0600374F RID: 14159 RVA: 0x00123BF8 File Offset: 0x00121DF8
	public string Adress
	{
		get
		{
			return this.m_webAdress;
		}
	}

	// Token: 0x17000824 RID: 2084
	// (get) Token: 0x06003750 RID: 14160 RVA: 0x00123C00 File Offset: 0x00121E00
	public string SaveKey
	{
		get
		{
			return this.m_saveKey;
		}
	}

	// Token: 0x06003751 RID: 14161 RVA: 0x00123C08 File Offset: 0x00121E08
	public void Init(long id, int priority, long start, long end, string param, string saveKey)
	{
		this.m_id = id;
		this.m_priority = priority;
		this.m_start = start;
		this.m_end = end;
		this.m_saveKey = saveKey;
		string[] array = param.Split(new char[]
		{
			'_'
		});
		this.m_announceType = 0;
		this.m_imageId = "-1";
		this.m_message = string.Empty;
		this.m_windowType = 0;
		this.m_webAdress = string.Empty;
		this.m_designatedArea = string.Empty;
		if (array.Length > 0)
		{
			int.TryParse(array[0], out this.m_announceType);
		}
		if (array.Length > 1)
		{
			this.m_message = array[1];
		}
		if (array.Length > 2)
		{
			this.m_imageId = array[2];
		}
		if (array.Length > 3)
		{
			int.TryParse(array[3], out this.m_windowType);
		}
		if (array.Length == 5)
		{
			if (this.m_windowType == 16 || this.m_windowType == 17)
			{
				this.m_designatedArea = array[4];
			}
			else
			{
				this.m_webAdress = array[4];
			}
		}
		else if (array.Length > 5 && (this.m_windowType != 16 || this.m_windowType == 17))
		{
			for (int i = 4; i < array.Length; i++)
			{
				if (i == 4)
				{
					this.m_webAdress = array[i];
				}
				else
				{
					this.m_webAdress = this.m_webAdress + "_" + array[i];
				}
			}
		}
	}

	// Token: 0x06003752 RID: 14162 RVA: 0x00123D80 File Offset: 0x00121F80
	public bool IsEveryDay()
	{
		return 0 == this.m_announceType;
	}

	// Token: 0x06003753 RID: 14163 RVA: 0x00123D8C File Offset: 0x00121F8C
	public bool IsOnce()
	{
		return 1 == this.m_announceType;
	}

	// Token: 0x06003754 RID: 14164 RVA: 0x00123D98 File Offset: 0x00121F98
	public bool IsFullTime()
	{
		return 2 == this.m_announceType;
	}

	// Token: 0x06003755 RID: 14165 RVA: 0x00123DA4 File Offset: 0x00121FA4
	public bool IsOnlyInformationPage()
	{
		return 3 == this.m_announceType;
	}

	// Token: 0x06003756 RID: 14166 RVA: 0x00123DB0 File Offset: 0x00121FB0
	public bool IsOutsideDesignatedArea()
	{
		bool result = false;
		if (this.m_windowType == 16 || this.m_windowType == 17)
		{
			result = true;
			if (RegionManager.Instance != null)
			{
				RegionInfo regionInfo = RegionManager.Instance.GetRegionInfo();
				if (regionInfo != null && !string.IsNullOrEmpty(this.m_designatedArea) && !string.IsNullOrEmpty(regionInfo.CountryCode) && this.m_designatedArea == regionInfo.CountryCode)
				{
					result = false;
				}
			}
		}
		return result;
	}

	// Token: 0x04002EA1 RID: 11937
	private const int ANNOUNCE_TYPE_EVERYDAY = 0;

	// Token: 0x04002EA2 RID: 11938
	private const int ANNOUNCE_TYPE_ONCE = 1;

	// Token: 0x04002EA3 RID: 11939
	private const int ANNOUNCE_TYPE_FULLTIME = 2;

	// Token: 0x04002EA4 RID: 11940
	private const int ANNOUNCE_TYPE_NOT_POP_UP = 3;

	// Token: 0x04002EA5 RID: 11941
	public static int OPERATORINFO_START_ID = 1000000000;

	// Token: 0x04002EA6 RID: 11942
	public static int OPERATORINFO_RANKINGRESULT_ID = NetNoticeItem.OPERATORINFO_START_ID;

	// Token: 0x04002EA7 RID: 11943
	public static int OPERATORINFO_EVENTRANKINGRESULT_ID = NetNoticeItem.OPERATORINFO_START_ID + 1;

	// Token: 0x04002EA8 RID: 11944
	public static int OPERATORINFO_QUICKRANKINGRESULT_ID = NetNoticeItem.OPERATORINFO_START_ID + 2;

	// Token: 0x04002EA9 RID: 11945
	private long m_id;

	// Token: 0x04002EAA RID: 11946
	private int m_priority;

	// Token: 0x04002EAB RID: 11947
	private long m_start;

	// Token: 0x04002EAC RID: 11948
	private long m_end;

	// Token: 0x04002EAD RID: 11949
	private int m_announceType;

	// Token: 0x04002EAE RID: 11950
	private int m_windowType;

	// Token: 0x04002EAF RID: 11951
	private string m_imageId;

	// Token: 0x04002EB0 RID: 11952
	private string m_message;

	// Token: 0x04002EB1 RID: 11953
	private string m_webAdress;

	// Token: 0x04002EB2 RID: 11954
	private string m_saveKey;

	// Token: 0x04002EB3 RID: 11955
	private string m_designatedArea;
}
