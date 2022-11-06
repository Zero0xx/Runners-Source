using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x0200071D RID: 1821
public class NetServerGetRingItemList : NetBase
{
	// Token: 0x06003094 RID: 12436 RVA: 0x001151B8 File Offset: 0x001133B8
	protected override void DoRequest()
	{
		base.SetAction("Game/getRingItemList");
	}

	// Token: 0x06003095 RID: 12437 RVA: 0x001151C8 File Offset: 0x001133C8
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_RingItemStateList(jdata);
	}

	// Token: 0x06003096 RID: 12438 RVA: 0x001151D4 File Offset: 0x001133D4
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x1700067F RID: 1663
	// (get) Token: 0x06003097 RID: 12439 RVA: 0x001151D8 File Offset: 0x001133D8
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

	// Token: 0x17000680 RID: 1664
	// (get) Token: 0x06003098 RID: 12440 RVA: 0x001151F4 File Offset: 0x001133F4
	// (set) Token: 0x06003099 RID: 12441 RVA: 0x001151FC File Offset: 0x001133FC
	private List<ServerRingItemState> resultRingItemStateList { get; set; }

	// Token: 0x0600309A RID: 12442 RVA: 0x00115208 File Offset: 0x00113408
	public ServerRingItemState GetResultRingItemState(int index)
	{
		if (0 <= index && this.resultRingItemStates > index)
		{
			return this.resultRingItemStateList[index];
		}
		return null;
	}

	// Token: 0x0600309B RID: 12443 RVA: 0x0011522C File Offset: 0x0011342C
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
