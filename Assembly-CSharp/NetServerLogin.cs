using System;
using System.Collections.Generic;
using App;
using LitJson;
using SaveData;
using UnityEngine;

// Token: 0x02000755 RID: 1877
public class NetServerLogin : NetBase
{
	// Token: 0x060031FC RID: 12796 RVA: 0x00118684 File Offset: 0x00116884
	public NetServerLogin() : this(string.Empty, string.Empty)
	{
	}

	// Token: 0x060031FD RID: 12797 RVA: 0x00118698 File Offset: 0x00116898
	public NetServerLogin(string userId, string password)
	{
		this.paramUserId = userId;
		this.paramPassword = password;
		this.paramMigrationPassWord = string.Empty;
	}

	// Token: 0x060031FE RID: 12798 RVA: 0x001186CC File Offset: 0x001168CC
	protected override void DoRequest()
	{
		base.SetAction("Login/login");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string userId = (this.paramUserId == null) ? string.Empty : this.paramUserId;
			string password = (this.paramPassword == null) ? string.Empty : this.paramPassword;
			string migrationPassword = (this.paramMigrationPassWord == null) ? string.Empty : this.paramMigrationPassWord;
			int platform = 2;
			string device = string.Empty;
			string deviceModel = SystemInfo.deviceModel;
			device = deviceModel.Replace(" ", "_");
			int language = (int)Env.language;
			int salesLocale = 0;
			int storeId = 2;
			int apolloPlatform = this.m_apolloPlatform2;
			string loginString = instance.GetLoginString(userId, password, migrationPassword, platform, device, language, salesLocale, storeId, apolloPlatform);
			base.WriteJsonString(loginString);
		}
	}

	// Token: 0x060031FF RID: 12799 RVA: 0x001187A4 File Offset: 0x001169A4
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_SessionId(jdata);
		this.GetResponse_SettingData(jdata);
	}

	// Token: 0x06003200 RID: 12800 RVA: 0x001187B4 File Offset: 0x001169B4
	protected override void DoDidSuccessEmulation()
	{
		this.resultSessionId = "dummy";
	}

	// Token: 0x170006BE RID: 1726
	// (get) Token: 0x06003201 RID: 12801 RVA: 0x001187C4 File Offset: 0x001169C4
	// (set) Token: 0x06003202 RID: 12802 RVA: 0x001187CC File Offset: 0x001169CC
	public string paramUserId { get; set; }

	// Token: 0x170006BF RID: 1727
	// (get) Token: 0x06003203 RID: 12803 RVA: 0x001187D8 File Offset: 0x001169D8
	// (set) Token: 0x06003204 RID: 12804 RVA: 0x001187E0 File Offset: 0x001169E0
	public string paramPassword { get; set; }

	// Token: 0x170006C0 RID: 1728
	// (get) Token: 0x06003205 RID: 12805 RVA: 0x001187EC File Offset: 0x001169EC
	// (set) Token: 0x06003206 RID: 12806 RVA: 0x001187F4 File Offset: 0x001169F4
	public string paramMigrationPassWord { get; set; }

	// Token: 0x06003207 RID: 12807 RVA: 0x00118800 File Offset: 0x00116A00
	private void SetParameter_LineAuth()
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>(1);
		dictionary.Add("userId", (this.paramUserId == null) ? string.Empty : this.paramUserId);
		dictionary.Add("password", (this.paramPassword == null) ? string.Empty : this.paramPassword);
		dictionary.Add("migrationPassword", (this.paramMigrationPassWord == null) ? string.Empty : this.paramMigrationPassWord);
		base.WriteActionParamObject("lineAuth", dictionary);
		dictionary.Clear();
	}

	// Token: 0x06003208 RID: 12808 RVA: 0x0011889C File Offset: 0x00116A9C
	private void SetParameter_Platform()
	{
		int num = 2;
		base.WriteActionParamValue("platform", num);
	}

	// Token: 0x06003209 RID: 12809 RVA: 0x001188BC File Offset: 0x00116ABC
	private void SetParameter_Device()
	{
		string value = string.Empty;
		string deviceModel = SystemInfo.deviceModel;
		value = deviceModel.Replace(" ", "_");
		base.WriteActionParamValue("device", value);
	}

	// Token: 0x0600320A RID: 12810 RVA: 0x001188F4 File Offset: 0x00116AF4
	private void SetParameter_Language()
	{
		int language = (int)Env.language;
		base.WriteActionParamValue("language", language);
	}

	// Token: 0x0600320B RID: 12811 RVA: 0x00118918 File Offset: 0x00116B18
	private void SetParameter_Locate()
	{
		base.WriteActionParamValue("salesLocate", 0);
	}

	// Token: 0x0600320C RID: 12812 RVA: 0x0011892C File Offset: 0x00116B2C
	private void SetParameter_StoreId()
	{
		int num = 2;
		base.WriteActionParamValue("storeId", num);
	}

	// Token: 0x0600320D RID: 12813 RVA: 0x0011894C File Offset: 0x00116B4C
	private void SetParameter_SnsPlatform()
	{
		int apolloPlatform = this.m_apolloPlatform2;
		base.WriteActionParamValue("platform_sns", apolloPlatform);
	}

	// Token: 0x170006C1 RID: 1729
	// (get) Token: 0x0600320E RID: 12814 RVA: 0x00118974 File Offset: 0x00116B74
	// (set) Token: 0x0600320F RID: 12815 RVA: 0x0011897C File Offset: 0x00116B7C
	public string resultSessionId { get; private set; }

	// Token: 0x170006C2 RID: 1730
	// (get) Token: 0x06003210 RID: 12816 RVA: 0x00118988 File Offset: 0x00116B88
	// (set) Token: 0x06003211 RID: 12817 RVA: 0x00118990 File Offset: 0x00116B90
	public long resultEnergyRefreshTime { get; private set; }

	// Token: 0x170006C3 RID: 1731
	// (get) Token: 0x06003212 RID: 12818 RVA: 0x0011899C File Offset: 0x00116B9C
	// (set) Token: 0x06003213 RID: 12819 RVA: 0x001189A4 File Offset: 0x00116BA4
	public ServerItemState resultInviteBaseIncentive { get; private set; }

	// Token: 0x170006C4 RID: 1732
	// (get) Token: 0x06003214 RID: 12820 RVA: 0x001189B0 File Offset: 0x00116BB0
	// (set) Token: 0x06003215 RID: 12821 RVA: 0x001189B8 File Offset: 0x00116BB8
	public ServerItemState resultRentalBaseIncentive { get; private set; }

	// Token: 0x170006C5 RID: 1733
	// (get) Token: 0x06003216 RID: 12822 RVA: 0x001189C4 File Offset: 0x00116BC4
	// (set) Token: 0x06003217 RID: 12823 RVA: 0x001189CC File Offset: 0x00116BCC
	public string userName { get; private set; }

	// Token: 0x170006C6 RID: 1734
	// (get) Token: 0x06003218 RID: 12824 RVA: 0x001189D8 File Offset: 0x00116BD8
	// (set) Token: 0x06003219 RID: 12825 RVA: 0x001189E0 File Offset: 0x00116BE0
	public string userId { get; private set; }

	// Token: 0x170006C7 RID: 1735
	// (get) Token: 0x0600321A RID: 12826 RVA: 0x001189EC File Offset: 0x00116BEC
	// (set) Token: 0x0600321B RID: 12827 RVA: 0x001189F4 File Offset: 0x00116BF4
	public string password { get; private set; }

	// Token: 0x170006C8 RID: 1736
	// (get) Token: 0x0600321C RID: 12828 RVA: 0x00118A00 File Offset: 0x00116C00
	// (set) Token: 0x0600321D RID: 12829 RVA: 0x00118A08 File Offset: 0x00116C08
	public string key { get; private set; }

	// Token: 0x170006C9 RID: 1737
	// (get) Token: 0x0600321E RID: 12830 RVA: 0x00118A14 File Offset: 0x00116C14
	// (set) Token: 0x0600321F RID: 12831 RVA: 0x00118A1C File Offset: 0x00116C1C
	public int sessionTimeLimit { get; private set; }

	// Token: 0x170006CA RID: 1738
	// (get) Token: 0x06003220 RID: 12832 RVA: 0x00118A28 File Offset: 0x00116C28
	// (set) Token: 0x06003221 RID: 12833 RVA: 0x00118A30 File Offset: 0x00116C30
	public int energyRecoveryMax { get; private set; }

	// Token: 0x06003222 RID: 12834 RVA: 0x00118A3C File Offset: 0x00116C3C
	private void GetResponse_SessionId(JsonData jdata)
	{
		this.resultSessionId = NetUtil.GetJsonString(jdata, "sessionId");
	}

	// Token: 0x06003223 RID: 12835 RVA: 0x00118A50 File Offset: 0x00116C50
	private void GetResponse_SettingData(JsonData jdata)
	{
		this.resultEnergyRefreshTime = NetUtil.GetJsonLong(jdata, "energyRecoveryTime");
		this.resultInviteBaseIncentive = NetUtil.AnalyzeItemStateJson(jdata, "inviteBasicIncentiv");
		this.resultRentalBaseIncentive = NetUtil.AnalyzeItemStateJson(jdata, "chaoRentalBasicIncentiv");
		this.userName = NetUtil.GetJsonString(jdata, "userName");
		this.sessionTimeLimit = NetUtil.GetJsonInt(jdata, "sessionTimeLimit");
		this.userId = NetUtil.GetJsonString(jdata, "userId");
		this.password = NetUtil.GetJsonString(jdata, "password");
		this.energyRecoveryMax = NetUtil.GetJsonInt(jdata, "energyRecoveryMax");
	}

	// Token: 0x06003224 RID: 12836 RVA: 0x00118AE8 File Offset: 0x00116CE8
	protected override bool IsEscapeErrorMode()
	{
		return true;
	}

	// Token: 0x06003225 RID: 12837 RVA: 0x00118AEC File Offset: 0x00116CEC
	protected override void DoEscapeErrorMode(JsonData jdata)
	{
		this.userId = NetUtil.GetJsonString(jdata, "userId");
		this.password = NetUtil.GetJsonString(jdata, "password");
		this.key = NetUtil.GetJsonString(jdata, "key");
		string jsonString = NetUtil.GetJsonString(jdata, "countryCode");
		if (!string.IsNullOrEmpty(jsonString))
		{
			SystemSaveManager instance = SystemSaveManager.Instance;
			SystemData systemdata = instance.GetSystemdata();
			systemdata.country = jsonString;
			SystemSaveManager.CheckIAPMessage();
		}
	}

	// Token: 0x04002B53 RID: 11091
	private int m_apolloPlatform2 = 1;
}
