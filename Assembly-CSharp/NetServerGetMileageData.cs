using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x0200071B RID: 1819
public class NetServerGetMileageData : NetBase
{
	// Token: 0x0600307F RID: 12415 RVA: 0x00114F00 File Offset: 0x00113100
	public NetServerGetMileageData(string[] distanceFriendList)
	{
		this.m_distanceFriendList = distanceFriendList;
	}

	// Token: 0x06003080 RID: 12416 RVA: 0x00114F10 File Offset: 0x00113110
	protected override void DoRequest()
	{
		base.SetAction("Game/getMileageData");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string onlySendBaseParamString = instance.GetOnlySendBaseParamString();
			base.WriteJsonString(onlySendBaseParamString);
		}
	}

	// Token: 0x06003081 RID: 12417 RVA: 0x00114F48 File Offset: 0x00113148
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_MileageState(jdata);
		this.GetResponse_MileageFriendList(jdata);
	}

	// Token: 0x06003082 RID: 12418 RVA: 0x00114F58 File Offset: 0x00113158
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x06003083 RID: 12419 RVA: 0x00114F5C File Offset: 0x0011315C
	private void SetParameter_DistanceFriendList()
	{
		if (this.m_distanceFriendList != null && this.m_distanceFriendList.Length != 0)
		{
			base.WriteActionParamArray("distanceFriendList", new List<object>(this.m_distanceFriendList));
		}
	}

	// Token: 0x1700067C RID: 1660
	// (get) Token: 0x06003084 RID: 12420 RVA: 0x00114F98 File Offset: 0x00113198
	// (set) Token: 0x06003085 RID: 12421 RVA: 0x00114FA0 File Offset: 0x001131A0
	public ServerMileageMapState resultMileageMapState { get; private set; }

	// Token: 0x1700067D RID: 1661
	// (get) Token: 0x06003086 RID: 12422 RVA: 0x00114FAC File Offset: 0x001131AC
	// (set) Token: 0x06003087 RID: 12423 RVA: 0x00114FB4 File Offset: 0x001131B4
	public List<ServerMileageFriendEntry> m_resultMileageFriendList { get; private set; }

	// Token: 0x06003088 RID: 12424 RVA: 0x00114FC0 File Offset: 0x001131C0
	private void GetResponse_MileageState(JsonData jdata)
	{
		this.resultMileageMapState = NetUtil.AnalyzeMileageMapStateJson(jdata, "mileageMapState");
	}

	// Token: 0x06003089 RID: 12425 RVA: 0x00114FD4 File Offset: 0x001131D4
	private void GetResponse_MileageFriendList(JsonData jdata)
	{
		this.m_resultMileageFriendList = NetUtil.AnalyzeMileageFriendListJson(jdata, "mileageFriendList");
	}

	// Token: 0x04002AED RID: 10989
	private string[] m_distanceFriendList;
}
