using System;
using LitJson;

// Token: 0x02000751 RID: 1873
public class NetServerGetMigrationPassword : NetBase
{
	// Token: 0x060031D5 RID: 12757 RVA: 0x00118308 File Offset: 0x00116508
	public NetServerGetMigrationPassword() : this(string.Empty)
	{
	}

	// Token: 0x060031D6 RID: 12758 RVA: 0x00118318 File Offset: 0x00116518
	public NetServerGetMigrationPassword(string userPassword)
	{
		this.paramUserPassword = userPassword;
	}

	// Token: 0x060031D7 RID: 12759 RVA: 0x00118328 File Offset: 0x00116528
	protected override void DoRequest()
	{
		base.SetAction("Login/getMigrationPassword");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string getMigrationPasswordString = instance.GetGetMigrationPasswordString(this.paramUserPassword);
			base.WriteJsonString(getMigrationPasswordString);
		}
	}

	// Token: 0x060031D8 RID: 12760 RVA: 0x00118368 File Offset: 0x00116568
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_MigrationPassword(jdata);
	}

	// Token: 0x060031D9 RID: 12761 RVA: 0x00118374 File Offset: 0x00116574
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x170006B6 RID: 1718
	// (get) Token: 0x060031DA RID: 12762 RVA: 0x00118378 File Offset: 0x00116578
	// (set) Token: 0x060031DB RID: 12763 RVA: 0x00118380 File Offset: 0x00116580
	public string paramUserPassword { private get; set; }

	// Token: 0x060031DC RID: 12764 RVA: 0x0011838C File Offset: 0x0011658C
	private void SetParameter_UserPassword()
	{
		base.WriteActionParamValue("userPassword", this.paramUserPassword);
	}

	// Token: 0x170006B7 RID: 1719
	// (get) Token: 0x060031DD RID: 12765 RVA: 0x001183A0 File Offset: 0x001165A0
	// (set) Token: 0x060031DE RID: 12766 RVA: 0x001183A8 File Offset: 0x001165A8
	public string paramMigrationPassword { get; set; }

	// Token: 0x060031DF RID: 12767 RVA: 0x001183B4 File Offset: 0x001165B4
	private void GetResponse_MigrationPassword(JsonData jdata)
	{
		this.paramMigrationPassword = NetUtil.GetJsonString(jdata, "password");
	}
}
