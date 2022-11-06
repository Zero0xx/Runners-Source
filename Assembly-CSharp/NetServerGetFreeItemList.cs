using System;
using LitJson;

// Token: 0x02000719 RID: 1817
public class NetServerGetFreeItemList : NetBase
{
	// Token: 0x06003055 RID: 12373 RVA: 0x00114954 File Offset: 0x00112B54
	protected override void DoRequest()
	{
		base.SetAction("Game/getFreeItemList");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string onlySendBaseParamString = instance.GetOnlySendBaseParamString();
			base.WriteJsonString(onlySendBaseParamString);
		}
	}

	// Token: 0x06003056 RID: 12374 RVA: 0x0011498C File Offset: 0x00112B8C
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_FreeItemList(jdata);
	}

	// Token: 0x06003057 RID: 12375 RVA: 0x00114998 File Offset: 0x00112B98
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x06003058 RID: 12376 RVA: 0x0011499C File Offset: 0x00112B9C
	private void GetResponse_FreeItemList(JsonData jdata)
	{
		this.resultFreeItemState = NetUtil.AnalyzeFreeItemList(jdata);
	}

	// Token: 0x04002AD8 RID: 10968
	public ServerFreeItemState resultFreeItemState;
}
