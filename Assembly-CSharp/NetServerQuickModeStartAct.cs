using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x02000721 RID: 1825
public class NetServerQuickModeStartAct : NetBase
{
	// Token: 0x060030FE RID: 12542 RVA: 0x00116298 File Offset: 0x00114498
	public NetServerQuickModeStartAct(List<ItemType> modifiersItem, List<BoostItemType> modifiersBoostItem, bool tutorial)
	{
		if (modifiersItem != null)
		{
			for (int i = 0; i < modifiersItem.Count; i++)
			{
				this.m_paramModifiersItem.Add(modifiersItem[i]);
			}
		}
		if (modifiersBoostItem != null)
		{
			for (int j = 0; j < modifiersBoostItem.Count; j++)
			{
				this.m_paramModifiersBoostItem.Add(modifiersBoostItem[j]);
			}
		}
		if (tutorial)
		{
			this.m_tutorial = 1;
		}
		else
		{
			this.m_tutorial = 0;
		}
	}

	// Token: 0x060030FF RID: 12543 RVA: 0x00116338 File Offset: 0x00114538
	protected override void DoRequest()
	{
		base.SetAction("Game/quickActStart");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			List<int> list = new List<int>();
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
			string quickModeActStartString = instance.GetQuickModeActStartString(list, this.m_tutorial);
			Debug.Log("NetServerQuickModeStartAct.json = " + quickModeActStartString);
			base.WriteJsonString(quickModeActStartString);
		}
	}

	// Token: 0x06003100 RID: 12544 RVA: 0x00116414 File Offset: 0x00114614
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.m_resultPlayerState = NetUtil.AnalyzePlayerStateJson(jdata, "playerState");
		NetUtil.GetResponse_CampaignList(jdata);
	}

	// Token: 0x06003101 RID: 12545 RVA: 0x00116430 File Offset: 0x00114630
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x06003102 RID: 12546 RVA: 0x00116434 File Offset: 0x00114634
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

	// Token: 0x06003103 RID: 12547 RVA: 0x001164E4 File Offset: 0x001146E4
	private void SetParameter_Tutorial()
	{
		base.WriteActionParamValue("tutorial", this.m_tutorial);
	}

	// Token: 0x17000699 RID: 1689
	// (get) Token: 0x06003104 RID: 12548 RVA: 0x001164FC File Offset: 0x001146FC
	public ServerPlayerState resultPlayerState
	{
		get
		{
			return this.m_resultPlayerState;
		}
	}

	// Token: 0x1700069A RID: 1690
	// (get) Token: 0x06003105 RID: 12549 RVA: 0x00116504 File Offset: 0x00114704
	public List<ItemType> paramModifiersItem
	{
		get
		{
			return this.m_paramModifiersItem;
		}
	}

	// Token: 0x1700069B RID: 1691
	// (get) Token: 0x06003106 RID: 12550 RVA: 0x0011650C File Offset: 0x0011470C
	public List<BoostItemType> paramModifiersBoostItem
	{
		get
		{
			return this.m_paramModifiersBoostItem;
		}
	}

	// Token: 0x04002B11 RID: 11025
	private ServerPlayerState m_resultPlayerState;

	// Token: 0x04002B12 RID: 11026
	private List<ItemType> m_paramModifiersItem = new List<ItemType>();

	// Token: 0x04002B13 RID: 11027
	private List<BoostItemType> m_paramModifiersBoostItem = new List<BoostItemType>();

	// Token: 0x04002B14 RID: 11028
	private int m_tutorial;
}
