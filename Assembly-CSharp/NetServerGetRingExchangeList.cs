using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x020007AC RID: 1964
public class NetServerGetRingExchangeList : NetBase
{
	// Token: 0x060033FC RID: 13308 RVA: 0x0011C7E0 File Offset: 0x0011A9E0
	protected override void DoRequest()
	{
		base.SetAction("Store/getRingExchangeList");
	}

	// Token: 0x060033FD RID: 13309 RVA: 0x0011C7F0 File Offset: 0x0011A9F0
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_RingExchangeList(jdata);
	}

	// Token: 0x060033FE RID: 13310 RVA: 0x0011C7FC File Offset: 0x0011A9FC
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x060033FF RID: 13311 RVA: 0x0011C800 File Offset: 0x0011AA00
	private void GetResponse_RingExchangeList(JsonData jdata)
	{
		this.m_ringExchangeList = NetUtil.AnalyzeRingExchangeList(jdata);
		this.m_totalItems = NetUtil.AnalyzeRingExchangeListTotalItems(jdata);
	}

	// Token: 0x04002BFF RID: 11263
	public List<ServerRingExchangeList> m_ringExchangeList;

	// Token: 0x04002C00 RID: 11264
	public int m_totalItems;
}
