using System;
using LitJson;

// Token: 0x02000701 RID: 1793
public class NetServerRequestEnergy : NetBase
{
	// Token: 0x06002FE1 RID: 12257 RVA: 0x00113F4C File Offset: 0x0011214C
	public NetServerRequestEnergy() : this(string.Empty)
	{
	}

	// Token: 0x06002FE2 RID: 12258 RVA: 0x00113F5C File Offset: 0x0011215C
	public NetServerRequestEnergy(string friendId)
	{
		this.paramFriendId = friendId;
	}

	// Token: 0x06002FE3 RID: 12259 RVA: 0x00113F6C File Offset: 0x0011216C
	protected override void DoRequest()
	{
		base.SetAction("Friend/requestEnergy");
		this.SetParameter_FriendId();
	}

	// Token: 0x06002FE4 RID: 12260 RVA: 0x00113F80 File Offset: 0x00112180
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_PlayerState(jdata);
		this.GetResponse_ExpireTime(jdata);
	}

	// Token: 0x06002FE5 RID: 12261 RVA: 0x00113F90 File Offset: 0x00112190
	protected override void DoDidSuccessEmulation()
	{
		this.resultPlayerState = ServerInterface.PlayerState;
		this.resultPlayerState.RefreshFakeState();
	}

	// Token: 0x17000658 RID: 1624
	// (get) Token: 0x06002FE6 RID: 12262 RVA: 0x00113FA8 File Offset: 0x001121A8
	// (set) Token: 0x06002FE7 RID: 12263 RVA: 0x00113FB0 File Offset: 0x001121B0
	public string paramFriendId { get; set; }

	// Token: 0x06002FE8 RID: 12264 RVA: 0x00113FBC File Offset: 0x001121BC
	private void SetParameter_FriendId()
	{
		base.WriteActionParamValue("friendId", this.paramFriendId);
	}

	// Token: 0x17000659 RID: 1625
	// (get) Token: 0x06002FE9 RID: 12265 RVA: 0x00113FD0 File Offset: 0x001121D0
	// (set) Token: 0x06002FEA RID: 12266 RVA: 0x00113FD8 File Offset: 0x001121D8
	public ServerPlayerState resultPlayerState { get; private set; }

	// Token: 0x1700065A RID: 1626
	// (get) Token: 0x06002FEB RID: 12267 RVA: 0x00113FE4 File Offset: 0x001121E4
	// (set) Token: 0x06002FEC RID: 12268 RVA: 0x00113FEC File Offset: 0x001121EC
	public long resultExpireTime { get; private set; }

	// Token: 0x06002FED RID: 12269 RVA: 0x00113FF8 File Offset: 0x001121F8
	private void GetResponse_PlayerState(JsonData jdata)
	{
		this.resultPlayerState = NetUtil.AnalyzePlayerStateJson(jdata, "playerState");
	}

	// Token: 0x06002FEE RID: 12270 RVA: 0x0011400C File Offset: 0x0011220C
	private void GetResponse_ExpireTime(JsonData jdata)
	{
		this.resultExpireTime = NetUtil.GetJsonLong(jdata, "expireTime");
	}
}
