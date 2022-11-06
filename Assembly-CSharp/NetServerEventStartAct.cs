using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x020006E2 RID: 1762
public class NetServerEventStartAct : NetBase
{
	// Token: 0x06002F48 RID: 12104 RVA: 0x00112BB0 File Offset: 0x00110DB0
	public NetServerEventStartAct() : this(-1, -1, -1L, null, null)
	{
	}

	// Token: 0x06002F49 RID: 12105 RVA: 0x00112BC0 File Offset: 0x00110DC0
	public NetServerEventStartAct(int eventId, int energyExpend, long raidBossId, List<ItemType> modifiersItem, List<BoostItemType> modifiersBoostItem)
	{
		this.paramEventId = eventId;
		this.paramEnergyExpend = energyExpend;
		this.paramRaidBossId = raidBossId;
		this.paramModifiersItem = new List<ItemType>();
		if (modifiersItem != null)
		{
			for (int i = 0; i < modifiersItem.Count; i++)
			{
				this.paramModifiersItem.Add(modifiersItem[i]);
			}
		}
		this.paramModifiersBoostItem = new List<BoostItemType>();
		if (modifiersBoostItem != null)
		{
			for (int j = 0; j < modifiersBoostItem.Count; j++)
			{
				this.paramModifiersBoostItem.Add(modifiersBoostItem[j]);
			}
		}
	}

	// Token: 0x06002F4A RID: 12106 RVA: 0x00112C64 File Offset: 0x00110E64
	protected override void DoRequest()
	{
		base.SetAction("Event/eventActStart");
		this.SetParameter_EventId();
		this.SetParameter_EnergyExpend();
		this.SetParameter_RaidBossId();
		this.SetParameter_Modifiers();
	}

	// Token: 0x06002F4B RID: 12107 RVA: 0x00112C94 File Offset: 0x00110E94
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.resultPlayerState = NetUtil.AnalyzePlayerStateJson(jdata, "playerState");
		NetUtil.GetResponse_CampaignList(jdata);
		this.userRaidBossState = NetUtil.AnalyzeEventUserRaidBossState(jdata);
	}

	// Token: 0x06002F4C RID: 12108 RVA: 0x00112CBC File Offset: 0x00110EBC
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x1700063A RID: 1594
	// (get) Token: 0x06002F4D RID: 12109 RVA: 0x00112CC0 File Offset: 0x00110EC0
	// (set) Token: 0x06002F4E RID: 12110 RVA: 0x00112CC8 File Offset: 0x00110EC8
	public int paramEventId { get; set; }

	// Token: 0x1700063B RID: 1595
	// (get) Token: 0x06002F4F RID: 12111 RVA: 0x00112CD4 File Offset: 0x00110ED4
	// (set) Token: 0x06002F50 RID: 12112 RVA: 0x00112CDC File Offset: 0x00110EDC
	public int paramEnergyExpend { get; set; }

	// Token: 0x1700063C RID: 1596
	// (get) Token: 0x06002F51 RID: 12113 RVA: 0x00112CE8 File Offset: 0x00110EE8
	// (set) Token: 0x06002F52 RID: 12114 RVA: 0x00112CF0 File Offset: 0x00110EF0
	public long paramRaidBossId { get; set; }

	// Token: 0x1700063D RID: 1597
	// (get) Token: 0x06002F53 RID: 12115 RVA: 0x00112CFC File Offset: 0x00110EFC
	// (set) Token: 0x06002F54 RID: 12116 RVA: 0x00112D04 File Offset: 0x00110F04
	public List<ItemType> paramModifiersItem { get; set; }

	// Token: 0x1700063E RID: 1598
	// (get) Token: 0x06002F55 RID: 12117 RVA: 0x00112D10 File Offset: 0x00110F10
	// (set) Token: 0x06002F56 RID: 12118 RVA: 0x00112D18 File Offset: 0x00110F18
	public List<BoostItemType> paramModifiersBoostItem { get; set; }

	// Token: 0x06002F57 RID: 12119 RVA: 0x00112D24 File Offset: 0x00110F24
	private void SetParameter_EventId()
	{
		base.WriteActionParamValue("eventId", this.paramEventId);
	}

	// Token: 0x06002F58 RID: 12120 RVA: 0x00112D3C File Offset: 0x00110F3C
	private void SetParameter_EnergyExpend()
	{
		base.WriteActionParamValue("energyExpend", this.paramEnergyExpend);
	}

	// Token: 0x06002F59 RID: 12121 RVA: 0x00112D54 File Offset: 0x00110F54
	private void SetParameter_RaidBossId()
	{
		base.WriteActionParamValue("raidbossId", this.paramRaidBossId);
	}

	// Token: 0x06002F5A RID: 12122 RVA: 0x00112D6C File Offset: 0x00110F6C
	private void SetParameter_Modifiers()
	{
		List<object> list = new List<object>();
		for (int i = 0; i < this.paramModifiersItem.Count; i++)
		{
			ServerItem serverItem = new ServerItem(this.paramModifiersItem[i]);
			ServerItem.Id id = serverItem.id;
			list.Add((int)id);
		}
		for (int j = 0; j < this.paramModifiersBoostItem.Count; j++)
		{
			ServerItem serverItem2 = new ServerItem(this.paramModifiersBoostItem[j]);
			ServerItem.Id id2 = serverItem2.id;
			list.Add((int)id2);
		}
		base.WriteActionParamArray("modifire", list);
		list.Clear();
	}

	// Token: 0x1700063F RID: 1599
	// (get) Token: 0x06002F5B RID: 12123 RVA: 0x00112E1C File Offset: 0x0011101C
	// (set) Token: 0x06002F5C RID: 12124 RVA: 0x00112E24 File Offset: 0x00111024
	public ServerPlayerState resultPlayerState { get; private set; }

	// Token: 0x17000640 RID: 1600
	// (get) Token: 0x06002F5D RID: 12125 RVA: 0x00112E30 File Offset: 0x00111030
	// (set) Token: 0x06002F5E RID: 12126 RVA: 0x00112E38 File Offset: 0x00111038
	public ServerEventUserRaidBossState userRaidBossState { get; private set; }
}
