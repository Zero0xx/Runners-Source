using System;
using System.Collections.Generic;
using App;
using LitJson;
using UnityEngine;

// Token: 0x02000758 RID: 1880
public class NetServerMigration : NetBase
{
	// Token: 0x0600324E RID: 12878 RVA: 0x001190FC File Offset: 0x001172FC
	public NetServerMigration() : this(string.Empty, string.Empty)
	{
	}

	// Token: 0x0600324F RID: 12879 RVA: 0x00119110 File Offset: 0x00117310
	public NetServerMigration(string migrationId, string migrationPassword)
	{
		this.paramUserId = "0";
		this.paramPassword = "0";
		this.paramMigrationId = migrationId;
		this.paramMigrationPassword = migrationPassword;
	}

	// Token: 0x06003250 RID: 12880 RVA: 0x00119150 File Offset: 0x00117350
	protected override void DoRequest()
	{
		base.SetAction("Login/migration");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string userId = (this.paramUserId == null) ? string.Empty : this.paramUserId;
			string migrationPassword = (this.paramMigrationId == null) ? string.Empty : this.paramMigrationId;
			string migrationUserPassword = (this.paramMigrationPassword == null) ? string.Empty : this.paramMigrationPassword;
			int platform = 2;
			string device = string.Empty;
			string deviceModel = SystemInfo.deviceModel;
			device = deviceModel.Replace(" ", "_");
			int language = (int)Env.language;
			int salesLocale = 0;
			int storeId = 2;
			int apolloPlatform = this.m_apolloPlatform2;
			string migrationString = instance.GetMigrationString(userId, migrationPassword, migrationUserPassword, platform, device, language, salesLocale, storeId, apolloPlatform);
			base.WriteJsonString(migrationString);
		}
	}

	// Token: 0x06003251 RID: 12881 RVA: 0x00119228 File Offset: 0x00117428
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_SessionId(jdata);
		this.GetResponse_SettingData(jdata);
	}

	// Token: 0x06003252 RID: 12882 RVA: 0x00119238 File Offset: 0x00117438
	protected override void DoDidSuccessEmulation()
	{
		this.resultSessionId = "dummy";
	}

	// Token: 0x170006D6 RID: 1750
	// (get) Token: 0x06003253 RID: 12883 RVA: 0x00119248 File Offset: 0x00117448
	// (set) Token: 0x06003254 RID: 12884 RVA: 0x00119250 File Offset: 0x00117450
	public string paramUserId { get; set; }

	// Token: 0x170006D7 RID: 1751
	// (get) Token: 0x06003255 RID: 12885 RVA: 0x0011925C File Offset: 0x0011745C
	// (set) Token: 0x06003256 RID: 12886 RVA: 0x00119264 File Offset: 0x00117464
	public string paramPassword { get; set; }

	// Token: 0x170006D8 RID: 1752
	// (get) Token: 0x06003257 RID: 12887 RVA: 0x00119270 File Offset: 0x00117470
	// (set) Token: 0x06003258 RID: 12888 RVA: 0x00119278 File Offset: 0x00117478
	public string paramMigrationId { get; set; }

	// Token: 0x170006D9 RID: 1753
	// (get) Token: 0x06003259 RID: 12889 RVA: 0x00119284 File Offset: 0x00117484
	// (set) Token: 0x0600325A RID: 12890 RVA: 0x0011928C File Offset: 0x0011748C
	public string paramMigrationPassword { get; set; }

	// Token: 0x0600325B RID: 12891 RVA: 0x00119298 File Offset: 0x00117498
	private void SetParameter_LineAuth()
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>(1);
		dictionary.Add("userId", (this.paramUserId == null) ? string.Empty : this.paramUserId);
		dictionary.Add("password", (this.paramPassword == null) ? string.Empty : this.paramPassword);
		dictionary.Add("migrationPassword", (this.paramMigrationId == null) ? string.Empty : this.paramMigrationId);
		dictionary.Add("migrationUserPassword", (this.paramMigrationPassword == null) ? string.Empty : this.paramMigrationPassword);
		base.WriteActionParamObject("lineAuth", dictionary);
		dictionary.Clear();
	}

	// Token: 0x0600325C RID: 12892 RVA: 0x00119358 File Offset: 0x00117558
	private void SetParameter_Platform()
	{
		int num = 2;
		base.WriteActionParamValue("platform", num);
	}

	// Token: 0x0600325D RID: 12893 RVA: 0x00119378 File Offset: 0x00117578
	private void SetParameter_Device()
	{
		string value = string.Empty;
		string deviceModel = SystemInfo.deviceModel;
		value = deviceModel.Replace(" ", "_");
		base.WriteActionParamValue("device", value);
	}

	// Token: 0x0600325E RID: 12894 RVA: 0x001193B0 File Offset: 0x001175B0
	private void SetParameter_Language()
	{
		int language = (int)Env.language;
		base.WriteActionParamValue("language", language);
	}

	// Token: 0x0600325F RID: 12895 RVA: 0x001193D4 File Offset: 0x001175D4
	private void SetParameter_Locate()
	{
		base.WriteActionParamValue("salesLocate", 0);
	}

	// Token: 0x06003260 RID: 12896 RVA: 0x001193E8 File Offset: 0x001175E8
	private void SetParameter_StoreId()
	{
		int num = 2;
		base.WriteActionParamValue("storeId", num);
	}

	// Token: 0x06003261 RID: 12897 RVA: 0x00119408 File Offset: 0x00117608
	private void SetParameter_SnsPlatform()
	{
		int apolloPlatform = this.m_apolloPlatform2;
		base.WriteActionParamValue("platform_sns", apolloPlatform);
	}

	// Token: 0x170006DA RID: 1754
	// (get) Token: 0x06003262 RID: 12898 RVA: 0x00119430 File Offset: 0x00117630
	// (set) Token: 0x06003263 RID: 12899 RVA: 0x00119438 File Offset: 0x00117638
	public string resultSessionId { get; private set; }

	// Token: 0x170006DB RID: 1755
	// (get) Token: 0x06003264 RID: 12900 RVA: 0x00119444 File Offset: 0x00117644
	// (set) Token: 0x06003265 RID: 12901 RVA: 0x0011944C File Offset: 0x0011764C
	public long resultEnergyRefreshTime { get; private set; }

	// Token: 0x170006DC RID: 1756
	// (get) Token: 0x06003266 RID: 12902 RVA: 0x00119458 File Offset: 0x00117658
	// (set) Token: 0x06003267 RID: 12903 RVA: 0x00119460 File Offset: 0x00117660
	public ServerItemState resultInviteBaseIncentive { get; private set; }

	// Token: 0x170006DD RID: 1757
	// (get) Token: 0x06003268 RID: 12904 RVA: 0x0011946C File Offset: 0x0011766C
	// (set) Token: 0x06003269 RID: 12905 RVA: 0x00119474 File Offset: 0x00117674
	public ServerItemState resultRentalBaseIncentive { get; private set; }

	// Token: 0x170006DE RID: 1758
	// (get) Token: 0x0600326A RID: 12906 RVA: 0x00119480 File Offset: 0x00117680
	// (set) Token: 0x0600326B RID: 12907 RVA: 0x00119488 File Offset: 0x00117688
	public string userName { get; private set; }

	// Token: 0x170006DF RID: 1759
	// (get) Token: 0x0600326C RID: 12908 RVA: 0x00119494 File Offset: 0x00117694
	// (set) Token: 0x0600326D RID: 12909 RVA: 0x0011949C File Offset: 0x0011769C
	public string userId { get; private set; }

	// Token: 0x170006E0 RID: 1760
	// (get) Token: 0x0600326E RID: 12910 RVA: 0x001194A8 File Offset: 0x001176A8
	// (set) Token: 0x0600326F RID: 12911 RVA: 0x001194B0 File Offset: 0x001176B0
	public string password { get; private set; }

	// Token: 0x170006E1 RID: 1761
	// (get) Token: 0x06003270 RID: 12912 RVA: 0x001194BC File Offset: 0x001176BC
	// (set) Token: 0x06003271 RID: 12913 RVA: 0x001194C4 File Offset: 0x001176C4
	public string countryCode { get; private set; }

	// Token: 0x06003272 RID: 12914 RVA: 0x001194D0 File Offset: 0x001176D0
	private void GetResponse_SessionId(JsonData jdata)
	{
		this.resultSessionId = NetUtil.GetJsonString(jdata, "sessionId");
	}

	// Token: 0x06003273 RID: 12915 RVA: 0x001194E4 File Offset: 0x001176E4
	private void GetResponse_SettingData(JsonData jdata)
	{
		this.resultEnergyRefreshTime = NetUtil.GetJsonLong(jdata, "energyRecoveryTime");
		this.resultInviteBaseIncentive = NetUtil.AnalyzeItemStateJson(jdata, "inviteBasicIncentiv");
		this.resultRentalBaseIncentive = NetUtil.AnalyzeItemStateJson(jdata, "chaoRentalBasicIncentiv");
		this.userName = NetUtil.GetJsonString(jdata, "userName");
		this.userId = NetUtil.GetJsonString(jdata, "userId");
		this.password = NetUtil.GetJsonString(jdata, "password");
		this.countryCode = NetUtil.GetJsonString(jdata, "country_code");
	}

	// Token: 0x04002B70 RID: 11120
	private int m_apolloPlatform2 = 1;
}
