using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x020006D6 RID: 1750
public class NetDebugLogin : NetBase
{
	// Token: 0x06002EC7 RID: 11975 RVA: 0x001121C0 File Offset: 0x001103C0
	public NetDebugLogin() : this(string.Empty, string.Empty, string.Empty)
	{
	}

	// Token: 0x06002EC8 RID: 11976 RVA: 0x001121D8 File Offset: 0x001103D8
	public NetDebugLogin(string lineId, string altLineId, string lineAuth)
	{
		this.paramLineId = lineId;
		this.paramAltLineId = altLineId;
		this.paramLineAuth = lineAuth;
	}

	// Token: 0x06002EC9 RID: 11977 RVA: 0x00112200 File Offset: 0x00110400
	protected override void DoRequest()
	{
		base.SetAction("Debug/login");
		this.SetParameter_LineAuth();
	}

	// Token: 0x06002ECA RID: 11978 RVA: 0x00112214 File Offset: 0x00110414
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_SessionId(jdata);
		this.GetResponse_EnergyRefreshTime(jdata);
		this.GetResponse_RingItemStateList(jdata);
	}

	// Token: 0x06002ECB RID: 11979 RVA: 0x0011222C File Offset: 0x0011042C
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x1700061F RID: 1567
	// (get) Token: 0x06002ECC RID: 11980 RVA: 0x00112230 File Offset: 0x00110430
	// (set) Token: 0x06002ECD RID: 11981 RVA: 0x00112238 File Offset: 0x00110438
	public string paramLineId { get; set; }

	// Token: 0x17000620 RID: 1568
	// (get) Token: 0x06002ECE RID: 11982 RVA: 0x00112244 File Offset: 0x00110444
	// (set) Token: 0x06002ECF RID: 11983 RVA: 0x0011224C File Offset: 0x0011044C
	public string paramAltLineId { get; set; }

	// Token: 0x17000621 RID: 1569
	// (get) Token: 0x06002ED0 RID: 11984 RVA: 0x00112258 File Offset: 0x00110458
	// (set) Token: 0x06002ED1 RID: 11985 RVA: 0x00112260 File Offset: 0x00110460
	public string paramLineAuth { get; set; }

	// Token: 0x06002ED2 RID: 11986 RVA: 0x0011226C File Offset: 0x0011046C
	private void SetParameter_LineAuth()
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>(2);
		dictionary.Add("lineId", this.paramLineId);
		dictionary.Add("altLineId", this.paramAltLineId);
		dictionary.Add("lineAuthToken", this.paramLineAuth);
		base.WriteActionParamObject("lineAuth", dictionary);
		dictionary.Clear();
	}

	// Token: 0x17000622 RID: 1570
	// (get) Token: 0x06002ED3 RID: 11987 RVA: 0x001122C8 File Offset: 0x001104C8
	// (set) Token: 0x06002ED4 RID: 11988 RVA: 0x001122D0 File Offset: 0x001104D0
	public string resultSessionId { get; private set; }

	// Token: 0x17000623 RID: 1571
	// (get) Token: 0x06002ED5 RID: 11989 RVA: 0x001122DC File Offset: 0x001104DC
	// (set) Token: 0x06002ED6 RID: 11990 RVA: 0x001122E4 File Offset: 0x001104E4
	public long resultEnergyRefreshTime { get; private set; }

	// Token: 0x17000624 RID: 1572
	// (get) Token: 0x06002ED7 RID: 11991 RVA: 0x001122F0 File Offset: 0x001104F0
	public int resultRingItemStates
	{
		get
		{
			if (this.resultRingItemStateList != null)
			{
				return this.resultRingItemStateList.Count;
			}
			return 0;
		}
	}

	// Token: 0x17000625 RID: 1573
	// (get) Token: 0x06002ED8 RID: 11992 RVA: 0x0011230C File Offset: 0x0011050C
	// (set) Token: 0x06002ED9 RID: 11993 RVA: 0x00112314 File Offset: 0x00110514
	private List<ServerRingItemState> resultRingItemStateList { get; set; }

	// Token: 0x06002EDA RID: 11994 RVA: 0x00112320 File Offset: 0x00110520
	public ServerRingItemState GetResultRingItemState(int index)
	{
		if (0 <= index && this.resultRingItemStates > index)
		{
			return this.resultRingItemStateList[index];
		}
		return null;
	}

	// Token: 0x06002EDB RID: 11995 RVA: 0x00112344 File Offset: 0x00110544
	private void GetResponse_SessionId(JsonData jdata)
	{
		this.resultSessionId = NetUtil.GetJsonString(jdata, "sessionId");
	}

	// Token: 0x06002EDC RID: 11996 RVA: 0x00112358 File Offset: 0x00110558
	private void GetResponse_EnergyRefreshTime(JsonData jdata)
	{
		this.resultEnergyRefreshTime = NetUtil.GetJsonLong(jdata, "energyRecoveryTime");
	}

	// Token: 0x06002EDD RID: 11997 RVA: 0x0011236C File Offset: 0x0011056C
	private void GetResponse_RingItemStateList(JsonData jdata)
	{
		this.resultRingItemStateList = new List<ServerRingItemState>();
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "ringItemList");
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			JsonData jdata2 = jsonArray[i];
			ServerRingItemState item = NetUtil.AnalyzeRingItemStateJson(jdata2, string.Empty);
			this.resultRingItemStateList.Add(item);
		}
	}
}
