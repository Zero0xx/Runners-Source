using System;
using LitJson;

// Token: 0x02000773 RID: 1907
public class NetServerSendEnergy : NetBase
{
	// Token: 0x060032C7 RID: 12999 RVA: 0x0011A0FC File Offset: 0x001182FC
	public NetServerSendEnergy()
	{
	}

	// Token: 0x060032C8 RID: 13000 RVA: 0x0011A104 File Offset: 0x00118304
	public NetServerSendEnergy(string friendId)
	{
		this.paramFriendId = friendId;
	}

	// Token: 0x060032C9 RID: 13001 RVA: 0x0011A114 File Offset: 0x00118314
	protected override void DoRequest()
	{
		base.SetAction("Message/sendEnergy");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string sendEnergyString = instance.GetSendEnergyString(this.paramFriendId);
			base.WriteJsonString(sendEnergyString);
		}
	}

	// Token: 0x060032CA RID: 13002 RVA: 0x0011A154 File Offset: 0x00118354
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_ExpireTime(jdata);
	}

	// Token: 0x060032CB RID: 13003 RVA: 0x0011A160 File Offset: 0x00118360
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x170006EA RID: 1770
	// (get) Token: 0x060032CC RID: 13004 RVA: 0x0011A164 File Offset: 0x00118364
	// (set) Token: 0x060032CD RID: 13005 RVA: 0x0011A16C File Offset: 0x0011836C
	public string paramFriendId { get; set; }

	// Token: 0x060032CE RID: 13006 RVA: 0x0011A178 File Offset: 0x00118378
	private void SetParameter_FriendId()
	{
		base.WriteActionParamValue("friendId", this.paramFriendId);
	}

	// Token: 0x170006EB RID: 1771
	// (get) Token: 0x060032CF RID: 13007 RVA: 0x0011A18C File Offset: 0x0011838C
	// (set) Token: 0x060032D0 RID: 13008 RVA: 0x0011A194 File Offset: 0x00118394
	public int resultExpireTime { get; private set; }

	// Token: 0x060032D1 RID: 13009 RVA: 0x0011A1A0 File Offset: 0x001183A0
	private void GetResponse_ExpireTime(JsonData jdata)
	{
		this.resultExpireTime = NetUtil.GetJsonInt(jdata, "expireTime");
	}
}
