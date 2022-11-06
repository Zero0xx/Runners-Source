using System;
using LitJson;

// Token: 0x0200077B RID: 1915
public class NetServerGetOptionUserResult : NetBase
{
	// Token: 0x170006F8 RID: 1784
	// (get) Token: 0x06003306 RID: 13062 RVA: 0x0011AC68 File Offset: 0x00118E68
	public ServerOptionUserResult UserResult
	{
		get
		{
			return this.m_userResult;
		}
	}

	// Token: 0x06003307 RID: 13063 RVA: 0x0011AC70 File Offset: 0x00118E70
	protected override void DoRequest()
	{
		base.SetAction("Option/userResult");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string onlySendBaseParamString = instance.GetOnlySendBaseParamString();
			base.WriteJsonString(onlySendBaseParamString);
		}
	}

	// Token: 0x06003308 RID: 13064 RVA: 0x0011ACA8 File Offset: 0x00118EA8
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_OptionUserResult(jdata);
	}

	// Token: 0x06003309 RID: 13065 RVA: 0x0011ACB4 File Offset: 0x00118EB4
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x0600330A RID: 13066 RVA: 0x0011ACB8 File Offset: 0x00118EB8
	private void GetResponse_OptionUserResult(JsonData jdata)
	{
		JsonData jsonObject = NetUtil.GetJsonObject(jdata, "optionUserResult");
		if (jsonObject != null)
		{
			this.m_userResult.m_totalSumHightScore = NetUtil.GetJsonLong(jsonObject, "totalSumHightScore");
			this.m_userResult.m_quickTotalSumHightScore = NetUtil.GetJsonLong(jsonObject, "quickTotalSumHightScore");
			this.m_userResult.m_numTakeAllRings = NetUtil.GetJsonLong(jsonObject, "numTakeAllRings");
			this.m_userResult.m_numTakeAllRedRings = NetUtil.GetJsonLong(jsonObject, "numTakeAllRedRings");
			this.m_userResult.m_numChaoRoulette = NetUtil.GetJsonInt(jsonObject, "numChaoRoulette");
			this.m_userResult.m_numItemRoulette = NetUtil.GetJsonInt(jsonObject, "numItemRoulette");
			this.m_userResult.m_numJackPot = NetUtil.GetJsonInt(jsonObject, "numJackPot");
			this.m_userResult.m_numMaximumJackPotRings = NetUtil.GetJsonInt(jsonObject, "numMaximumJackPotRings");
			this.m_userResult.m_numSupport = NetUtil.GetJsonInt(jsonObject, "numSupport");
		}
	}

	// Token: 0x04002BA6 RID: 11174
	private ServerOptionUserResult m_userResult = new ServerOptionUserResult();
}
