using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x02000740 RID: 1856
public class NetServerEquipItem : NetBase
{
	// Token: 0x06003150 RID: 12624 RVA: 0x00116FA4 File Offset: 0x001151A4
	public NetServerEquipItem() : this(null)
	{
	}

	// Token: 0x06003151 RID: 12625 RVA: 0x00116FB0 File Offset: 0x001151B0
	public NetServerEquipItem(List<ItemType> items)
	{
		this.items = items;
	}

	// Token: 0x06003152 RID: 12626 RVA: 0x00116FC0 File Offset: 0x001151C0
	protected override void DoRequest()
	{
		base.SetAction("Item/equipItem");
		this.SetParameter_EquipItem();
	}

	// Token: 0x06003153 RID: 12627 RVA: 0x00116FD4 File Offset: 0x001151D4
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_PlayerState(jdata);
	}

	// Token: 0x06003154 RID: 12628 RVA: 0x00116FE0 File Offset: 0x001151E0
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x1700069C RID: 1692
	// (get) Token: 0x06003155 RID: 12629 RVA: 0x00116FE4 File Offset: 0x001151E4
	// (set) Token: 0x06003156 RID: 12630 RVA: 0x00116FEC File Offset: 0x001151EC
	public List<ItemType> items { get; set; }

	// Token: 0x06003157 RID: 12631 RVA: 0x00116FF8 File Offset: 0x001151F8
	private void SetParameter_EquipItem()
	{
		if (this.items == null)
		{
			this.items = new List<ItemType>();
		}
		List<object> list = new List<object>();
		foreach (ItemType itemType in this.items)
		{
			ServerItem serverItem = new ServerItem(itemType);
			int id = (int)serverItem.id;
			list.Add(id);
		}
		base.WriteActionParamArray("equipItemList", list);
	}

	// Token: 0x1700069D RID: 1693
	// (get) Token: 0x06003158 RID: 12632 RVA: 0x001170A0 File Offset: 0x001152A0
	// (set) Token: 0x06003159 RID: 12633 RVA: 0x001170A8 File Offset: 0x001152A8
	public ServerPlayerState resultPlayerState { get; private set; }

	// Token: 0x0600315A RID: 12634 RVA: 0x001170B4 File Offset: 0x001152B4
	private void GetResponse_PlayerState(JsonData jdata)
	{
		this.resultPlayerState = NetUtil.AnalyzePlayerStateJson(jdata, "playerState");
	}
}
