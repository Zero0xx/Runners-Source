using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x02000796 RID: 1942
public class NetServerGetItemStockNum : NetBase
{
	// Token: 0x06003391 RID: 13201 RVA: 0x0011BC30 File Offset: 0x00119E30
	public NetServerGetItemStockNum(int eventId, List<int> itemId)
	{
		this.paramEventId = eventId;
		this.paramItemId = itemId;
	}

	// Token: 0x06003392 RID: 13202 RVA: 0x0011BC48 File Offset: 0x00119E48
	protected override void DoRequest()
	{
		base.SetAction("RaidbossSpin/getItemStockNum");
		int eventId = this.paramEventId;
		int[] itemIdList = this.paramItemId.ToArray();
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string getItemStockNumString = instance.GetGetItemStockNumString(eventId, itemIdList);
			Debug.Log("NetServerGetItemStockNum.json = " + getItemStockNumString);
			base.WriteJsonString(getItemStockNumString);
		}
	}

	// Token: 0x06003393 RID: 13203 RVA: 0x0011BCA8 File Offset: 0x00119EA8
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_itemStock(jdata);
	}

	// Token: 0x06003394 RID: 13204 RVA: 0x0011BCB4 File Offset: 0x00119EB4
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x06003395 RID: 13205 RVA: 0x0011BCB8 File Offset: 0x00119EB8
	private void SetParameter()
	{
		base.WriteActionParamValue("eventId", this.paramEventId);
		List<object> list = new List<object>();
		foreach (int num in this.paramItemId)
		{
			object item = num;
			list.Add(item);
		}
		base.WriteActionParamArray("itemIdList", list);
	}

	// Token: 0x1700070E RID: 1806
	// (get) Token: 0x06003396 RID: 13206 RVA: 0x0011BD4C File Offset: 0x00119F4C
	// (set) Token: 0x06003397 RID: 13207 RVA: 0x0011BD54 File Offset: 0x00119F54
	public List<ServerItemState> m_itemStockList { get; set; }

	// Token: 0x06003398 RID: 13208 RVA: 0x0011BD60 File Offset: 0x00119F60
	private void GetResponse_itemStock(JsonData jdata)
	{
		this.m_itemStockList = new List<ServerItemState>();
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "itemStockList");
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			ServerItemState item = NetUtil.AnalyzeItemStateJson(jsonArray[i], string.Empty);
			this.m_itemStockList.Add(item);
		}
	}

	// Token: 0x04002BD7 RID: 11223
	public int paramEventId;

	// Token: 0x04002BD8 RID: 11224
	public List<int> paramItemId;
}
