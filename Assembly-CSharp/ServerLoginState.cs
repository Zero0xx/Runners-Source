using System;

// Token: 0x0200080B RID: 2059
public class ServerLoginState
{
	// Token: 0x0600372A RID: 14122 RVA: 0x0012388C File Offset: 0x00121A8C
	public ServerLoginState()
	{
		this.m_lineId = null;
		this.m_altLineId = null;
		this.m_lineAuthToken = null;
		this.m_segaAuthToken = null;
		this.sessionId = null;
		this.seqNum = 0UL;
		this.m_isChangeDataVersion = false;
		this.m_dataVersion = -1;
		this.m_isChangeAssetsVersion = false;
		this.m_assetsVersion = -1;
	}

	// Token: 0x1700080F RID: 2063
	// (get) Token: 0x0600372B RID: 14123 RVA: 0x001238E8 File Offset: 0x00121AE8
	public bool IsLoggedIn
	{
		get
		{
			return this.sessionId != null && string.Empty != this.sessionId;
		}
	}

	// Token: 0x17000810 RID: 2064
	// (get) Token: 0x0600372C RID: 14124 RVA: 0x00123908 File Offset: 0x00121B08
	// (set) Token: 0x0600372D RID: 14125 RVA: 0x00123910 File Offset: 0x00121B10
	public string sessionId
	{
		get
		{
			return this.m_sessionId;
		}
		set
		{
			this.m_sessionId = value;
		}
	}

	// Token: 0x17000811 RID: 2065
	// (get) Token: 0x0600372E RID: 14126 RVA: 0x0012391C File Offset: 0x00121B1C
	// (set) Token: 0x0600372F RID: 14127 RVA: 0x00123924 File Offset: 0x00121B24
	public ulong seqNum
	{
		get
		{
			return this.m_seqNum;
		}
		set
		{
			this.m_seqNum = value;
		}
	}

	// Token: 0x17000812 RID: 2066
	// (get) Token: 0x06003730 RID: 14128 RVA: 0x00123930 File Offset: 0x00121B30
	// (set) Token: 0x06003731 RID: 14129 RVA: 0x00123938 File Offset: 0x00121B38
	public string serverVersion { get; set; }

	// Token: 0x17000813 RID: 2067
	// (get) Token: 0x06003732 RID: 14130 RVA: 0x00123944 File Offset: 0x00121B44
	// (set) Token: 0x06003733 RID: 14131 RVA: 0x0012394C File Offset: 0x00121B4C
	public int serverVersionValue { get; set; }

	// Token: 0x17000814 RID: 2068
	// (get) Token: 0x06003734 RID: 14132 RVA: 0x00123958 File Offset: 0x00121B58
	// (set) Token: 0x06003735 RID: 14133 RVA: 0x00123960 File Offset: 0x00121B60
	public long serverLastTime { get; set; }

	// Token: 0x17000815 RID: 2069
	// (get) Token: 0x06003736 RID: 14134 RVA: 0x0012396C File Offset: 0x00121B6C
	// (set) Token: 0x06003737 RID: 14135 RVA: 0x00123974 File Offset: 0x00121B74
	public bool IsChangeDataVersion
	{
		get
		{
			return this.m_isChangeDataVersion;
		}
		set
		{
			this.m_isChangeDataVersion = value;
		}
	}

	// Token: 0x17000816 RID: 2070
	// (get) Token: 0x06003738 RID: 14136 RVA: 0x00123980 File Offset: 0x00121B80
	// (set) Token: 0x06003739 RID: 14137 RVA: 0x00123988 File Offset: 0x00121B88
	public int DataVersion
	{
		get
		{
			return this.m_dataVersion;
		}
		set
		{
			if (value != this.m_dataVersion)
			{
				this.m_dataVersion = value;
				this.m_isChangeDataVersion = true;
			}
		}
	}

	// Token: 0x17000817 RID: 2071
	// (get) Token: 0x0600373A RID: 14138 RVA: 0x001239B4 File Offset: 0x00121BB4
	// (set) Token: 0x0600373B RID: 14139 RVA: 0x001239BC File Offset: 0x00121BBC
	public bool IsChangeAssetsVersion
	{
		get
		{
			return this.m_isChangeAssetsVersion;
		}
		set
		{
			this.m_isChangeAssetsVersion = value;
		}
	}

	// Token: 0x17000818 RID: 2072
	// (get) Token: 0x0600373C RID: 14140 RVA: 0x001239C8 File Offset: 0x00121BC8
	// (set) Token: 0x0600373D RID: 14141 RVA: 0x001239D0 File Offset: 0x00121BD0
	public int AssetsVersion
	{
		get
		{
			return this.m_assetsVersion;
		}
		set
		{
			if (value != this.m_assetsVersion)
			{
				this.m_assetsVersion = value;
				this.m_isChangeAssetsVersion = true;
			}
		}
	}

	// Token: 0x17000819 RID: 2073
	// (get) Token: 0x0600373E RID: 14142 RVA: 0x001239FC File Offset: 0x00121BFC
	// (set) Token: 0x0600373F RID: 14143 RVA: 0x00123A04 File Offset: 0x00121C04
	public string AssetsVersionString
	{
		get
		{
			return this.m_assetsVersionString;
		}
		set
		{
			if (value != this.m_assetsVersionString)
			{
				this.m_assetsVersionString = value;
				this.m_isChangeAssetsVersion = true;
			}
		}
	}

	// Token: 0x1700081A RID: 2074
	// (get) Token: 0x06003740 RID: 14144 RVA: 0x00123A34 File Offset: 0x00121C34
	// (set) Token: 0x06003741 RID: 14145 RVA: 0x00123A3C File Offset: 0x00121C3C
	public string InfoVersionString
	{
		get
		{
			return this.m_infoVersionString;
		}
		set
		{
			this.m_infoVersionString = value;
		}
	}

	// Token: 0x04002E75 RID: 11893
	public string m_lineId;

	// Token: 0x04002E76 RID: 11894
	public string m_altLineId;

	// Token: 0x04002E77 RID: 11895
	public string m_lineAuthToken;

	// Token: 0x04002E78 RID: 11896
	public string m_segaAuthToken;

	// Token: 0x04002E79 RID: 11897
	private string m_sessionId;

	// Token: 0x04002E7A RID: 11898
	private ulong m_seqNum;

	// Token: 0x04002E7B RID: 11899
	public int sessionTimeLimit;

	// Token: 0x04002E7C RID: 11900
	private bool m_isChangeDataVersion;

	// Token: 0x04002E7D RID: 11901
	private int m_dataVersion;

	// Token: 0x04002E7E RID: 11902
	private bool m_isChangeAssetsVersion;

	// Token: 0x04002E7F RID: 11903
	private int m_assetsVersion;

	// Token: 0x04002E80 RID: 11904
	private string m_assetsVersionString;

	// Token: 0x04002E81 RID: 11905
	private string m_infoVersionString;
}
