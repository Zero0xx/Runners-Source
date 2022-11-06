using System;
using LitJson;

// Token: 0x020007B1 RID: 1969
public class NetServerSetBirthday : NetBase
{
	// Token: 0x06003427 RID: 13351 RVA: 0x0011CAC8 File Offset: 0x0011ACC8
	public NetServerSetBirthday() : this(string.Empty)
	{
	}

	// Token: 0x06003428 RID: 13352 RVA: 0x0011CAD8 File Offset: 0x0011ACD8
	public NetServerSetBirthday(string birthday)
	{
		this.birthday = birthday;
	}

	// Token: 0x06003429 RID: 13353 RVA: 0x0011CAE8 File Offset: 0x0011ACE8
	protected override void DoRequest()
	{
		base.SetAction("Store/setBirthday");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string setBirthdayString = instance.GetSetBirthdayString(this.birthday);
			Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(setBirthdayString);
		}
	}

	// Token: 0x0600342A RID: 13354 RVA: 0x0011CB30 File Offset: 0x0011AD30
	protected override void DoDidSuccess(JsonData jdata)
	{
		ServerInterface.SettingState.m_birthday = NetUtil.GetJsonString(jdata, "birthday");
	}

	// Token: 0x0600342B RID: 13355 RVA: 0x0011CB48 File Offset: 0x0011AD48
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000721 RID: 1825
	// (get) Token: 0x0600342C RID: 13356 RVA: 0x0011CB4C File Offset: 0x0011AD4C
	// (set) Token: 0x0600342D RID: 13357 RVA: 0x0011CB54 File Offset: 0x0011AD54
	public string birthday { get; set; }

	// Token: 0x0600342E RID: 13358 RVA: 0x0011CB60 File Offset: 0x0011AD60
	private void SetParameter_Birthday()
	{
		base.WriteActionParamValue("birthday", this.birthday);
	}
}
