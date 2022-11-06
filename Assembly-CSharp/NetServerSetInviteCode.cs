using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x02000703 RID: 1795
public class NetServerSetInviteCode : NetBase
{
	// Token: 0x06002FF7 RID: 12279 RVA: 0x001140B8 File Offset: 0x001122B8
	public NetServerSetInviteCode() : this(string.Empty)
	{
	}

	// Token: 0x06002FF8 RID: 12280 RVA: 0x001140C8 File Offset: 0x001122C8
	public NetServerSetInviteCode(string friendId)
	{
		this.friendId = friendId;
	}

	// Token: 0x06002FF9 RID: 12281 RVA: 0x001140D8 File Offset: 0x001122D8
	protected override void DoRequest()
	{
		base.SetAction("Friend/setInviteCode");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string setInviteCodeString = instance.GetSetInviteCodeString(this.friendId);
			Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(setInviteCodeString);
		}
	}

	// Token: 0x06002FFA RID: 12282 RVA: 0x00114120 File Offset: 0x00112320
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_PlayerState(jdata);
		this.GetResponse_Incentive(jdata);
	}

	// Token: 0x06002FFB RID: 12283 RVA: 0x00114130 File Offset: 0x00112330
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x1700065C RID: 1628
	// (get) Token: 0x06002FFC RID: 12284 RVA: 0x00114134 File Offset: 0x00112334
	// (set) Token: 0x06002FFD RID: 12285 RVA: 0x0011413C File Offset: 0x0011233C
	public string friendId { get; set; }

	// Token: 0x06002FFE RID: 12286 RVA: 0x00114148 File Offset: 0x00112348
	private void SetParameter_FriendId()
	{
		base.WriteActionParamValue("friendId", this.friendId);
	}

	// Token: 0x1700065D RID: 1629
	// (get) Token: 0x06002FFF RID: 12287 RVA: 0x0011415C File Offset: 0x0011235C
	// (set) Token: 0x06003000 RID: 12288 RVA: 0x00114164 File Offset: 0x00112364
	public ServerPlayerState playerState { get; set; }

	// Token: 0x1700065E RID: 1630
	// (get) Token: 0x06003001 RID: 12289 RVA: 0x00114170 File Offset: 0x00112370
	// (set) Token: 0x06003002 RID: 12290 RVA: 0x00114178 File Offset: 0x00112378
	public List<ServerPresentState> incentive { get; set; }

	// Token: 0x06003003 RID: 12291 RVA: 0x00114184 File Offset: 0x00112384
	private void GetResponse_PlayerState(JsonData jdata)
	{
		this.playerState = NetUtil.AnalyzePlayerStateJson(jdata, "playerState");
	}

	// Token: 0x06003004 RID: 12292 RVA: 0x00114198 File Offset: 0x00112398
	private void GetResponse_Incentive(JsonData jdata)
	{
		this.incentive = new List<ServerPresentState>();
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "incentive");
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			ServerPresentState serverPresentState = NetUtil.AnalyzePresentStateJson(jsonArray[i], string.Empty);
			if (serverPresentState != null)
			{
				this.incentive.Add(serverPresentState);
			}
		}
	}
}
