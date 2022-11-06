using System;
using LitJson;

// Token: 0x02000702 RID: 1794
public class NetServerSetFacebookScopedId : NetBase
{
	// Token: 0x06002FEF RID: 12271 RVA: 0x00114020 File Offset: 0x00112220
	public NetServerSetFacebookScopedId() : this(string.Empty)
	{
	}

	// Token: 0x06002FF0 RID: 12272 RVA: 0x00114030 File Offset: 0x00112230
	public NetServerSetFacebookScopedId(string userId)
	{
		this.paramUserId = userId;
	}

	// Token: 0x06002FF1 RID: 12273 RVA: 0x00114040 File Offset: 0x00112240
	protected override void DoRequest()
	{
		base.SetAction("Friend/setFacebookScopedId");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string setFacebookScopedIdString = instance.GetSetFacebookScopedIdString(this.paramUserId);
			Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(setFacebookScopedIdString);
		}
	}

	// Token: 0x06002FF2 RID: 12274 RVA: 0x00114088 File Offset: 0x00112288
	protected override void DoDidSuccess(JsonData jdata)
	{
	}

	// Token: 0x06002FF3 RID: 12275 RVA: 0x0011408C File Offset: 0x0011228C
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x1700065B RID: 1627
	// (get) Token: 0x06002FF4 RID: 12276 RVA: 0x00114090 File Offset: 0x00112290
	// (set) Token: 0x06002FF5 RID: 12277 RVA: 0x00114098 File Offset: 0x00112298
	public string paramUserId { get; set; }

	// Token: 0x06002FF6 RID: 12278 RVA: 0x001140A4 File Offset: 0x001122A4
	private void SetParameter_UserId()
	{
		base.WriteActionParamValue("facebookId", this.paramUserId);
	}
}
