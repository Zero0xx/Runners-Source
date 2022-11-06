using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x02000700 RID: 1792
public class NetServerGetFriendUserIdList : NetBase
{
	// Token: 0x06002FD8 RID: 12248 RVA: 0x00113E2C File Offset: 0x0011202C
	public NetServerGetFriendUserIdList() : this(null)
	{
	}

	// Token: 0x06002FD9 RID: 12249 RVA: 0x00113E38 File Offset: 0x00112038
	public NetServerGetFriendUserIdList(List<string> friendFBIdList)
	{
		this.paramFriendFBIdList = friendFBIdList;
	}

	// Token: 0x06002FDA RID: 12250 RVA: 0x00113E48 File Offset: 0x00112048
	protected override void DoRequest()
	{
		base.SetAction("Friend/getFriendUserIdList");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string getFacebookFriendUserIdList = instance.GetGetFacebookFriendUserIdList(this.paramFriendFBIdList);
			Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(getFacebookFriendUserIdList);
		}
	}

	// Token: 0x06002FDB RID: 12251 RVA: 0x00113E90 File Offset: 0x00112090
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_TransformDataList(jdata);
	}

	// Token: 0x06002FDC RID: 12252 RVA: 0x00113E9C File Offset: 0x0011209C
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000657 RID: 1623
	// (get) Token: 0x06002FDD RID: 12253 RVA: 0x00113EA0 File Offset: 0x001120A0
	// (set) Token: 0x06002FDE RID: 12254 RVA: 0x00113EA8 File Offset: 0x001120A8
	public List<string> paramFriendFBIdList { get; set; }

	// Token: 0x06002FDF RID: 12255 RVA: 0x00113EB4 File Offset: 0x001120B4
	private void SetParameter_FriendIdList()
	{
		List<object> list = new List<object>();
		foreach (string text in this.paramFriendFBIdList)
		{
			if (!string.IsNullOrEmpty(text))
			{
				list.Add(text);
			}
		}
		base.WriteActionParamArray("facebookIdList", list);
	}

	// Token: 0x06002FE0 RID: 12256 RVA: 0x00113F3C File Offset: 0x0011213C
	private void GetResponse_TransformDataList(JsonData jdata)
	{
		this.resultTransformDataList = NetUtil.AnalyzeUserTransformData(jdata);
	}

	// Token: 0x04002AB1 RID: 10929
	public List<ServerUserTransformData> resultTransformDataList;
}
