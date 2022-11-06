using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x020007AB RID: 1963
public class NetServerGetRedStarExchangeList : NetBase
{
	// Token: 0x060033E9 RID: 13289 RVA: 0x0011C618 File Offset: 0x0011A818
	public NetServerGetRedStarExchangeList() : this(0)
	{
	}

	// Token: 0x060033EA RID: 13290 RVA: 0x0011C624 File Offset: 0x0011A824
	public NetServerGetRedStarExchangeList(int itemType)
	{
		this.paramItemType = itemType;
	}

	// Token: 0x060033EB RID: 13291 RVA: 0x0011C634 File Offset: 0x0011A834
	protected override void DoRequest()
	{
		base.SetAction("Store/getRedstarExchangeList");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string getRedStarExchangeListString = instance.GetGetRedStarExchangeListString(this.paramItemType);
			Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(getRedStarExchangeListString);
		}
	}

	// Token: 0x060033EC RID: 13292 RVA: 0x0011C67C File Offset: 0x0011A87C
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_RedStarItemStateList(jdata);
		this.GetResponse_TotalItems(jdata);
		this.GetResponse_BirthDay(jdata);
		this.GetResponse_MonthPurchase(jdata);
	}

	// Token: 0x060033ED RID: 13293 RVA: 0x0011C6A8 File Offset: 0x0011A8A8
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000717 RID: 1815
	// (get) Token: 0x060033EE RID: 13294 RVA: 0x0011C6AC File Offset: 0x0011A8AC
	// (set) Token: 0x060033EF RID: 13295 RVA: 0x0011C6B4 File Offset: 0x0011A8B4
	public int paramItemType
	{
		get
		{
			return this.mParamItemType;
		}
		set
		{
			this.mParamItemType = value;
		}
	}

	// Token: 0x060033F0 RID: 13296 RVA: 0x0011C6C0 File Offset: 0x0011A8C0
	private void SetParameter_ItemType()
	{
		base.WriteActionParamValue("itemType", this.paramItemType);
	}

	// Token: 0x17000718 RID: 1816
	// (get) Token: 0x060033F1 RID: 13297 RVA: 0x0011C6D8 File Offset: 0x0011A8D8
	// (set) Token: 0x060033F2 RID: 13298 RVA: 0x0011C6E0 File Offset: 0x0011A8E0
	public int resultTotalItems { get; private set; }

	// Token: 0x17000719 RID: 1817
	// (get) Token: 0x060033F3 RID: 13299 RVA: 0x0011C6EC File Offset: 0x0011A8EC
	public int resultItems
	{
		get
		{
			if (this.resultRedStarItemStateList != null)
			{
				return this.resultRedStarItemStateList.Count;
			}
			return 0;
		}
	}

	// Token: 0x1700071A RID: 1818
	// (get) Token: 0x060033F4 RID: 13300 RVA: 0x0011C708 File Offset: 0x0011A908
	// (set) Token: 0x060033F5 RID: 13301 RVA: 0x0011C710 File Offset: 0x0011A910
	private List<ServerRedStarItemState> resultRedStarItemStateList { get; set; }

	// Token: 0x060033F6 RID: 13302 RVA: 0x0011C71C File Offset: 0x0011A91C
	public ServerRedStarItemState GetResultRedStarItemState(int index)
	{
		if (0 <= index && this.resultItems > index)
		{
			return this.resultRedStarItemStateList[index];
		}
		return null;
	}

	// Token: 0x060033F7 RID: 13303 RVA: 0x0011C740 File Offset: 0x0011A940
	private void GetResponse_RedStarItemStateList(JsonData jdata)
	{
		this.resultRedStarItemStateList = new List<ServerRedStarItemState>();
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "itemList");
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			ServerRedStarItemState item = NetUtil.AnalyzeRedStarItemStateJson(jsonArray[i], string.Empty);
			this.resultRedStarItemStateList.Add(item);
		}
	}

	// Token: 0x060033F8 RID: 13304 RVA: 0x0011C79C File Offset: 0x0011A99C
	private void GetResponse_TotalItems(JsonData jdata)
	{
		this.resultTotalItems = NetUtil.GetJsonInt(jdata, "totalItems");
	}

	// Token: 0x060033F9 RID: 13305 RVA: 0x0011C7B0 File Offset: 0x0011A9B0
	private void GetResponse_BirthDay(JsonData jdata)
	{
		this.resultBirthDay = NetUtil.GetJsonString(jdata, "birthday");
	}

	// Token: 0x060033FA RID: 13306 RVA: 0x0011C7C4 File Offset: 0x0011A9C4
	private void GetResponse_MonthPurchase(JsonData jdata)
	{
		this.resultMonthPurchase = NetUtil.GetJsonInt(jdata, "monthPurchase");
	}

	// Token: 0x04002BFA RID: 11258
	private int mParamItemType;

	// Token: 0x04002BFB RID: 11259
	public string resultBirthDay;

	// Token: 0x04002BFC RID: 11260
	public int resultMonthPurchase;
}
