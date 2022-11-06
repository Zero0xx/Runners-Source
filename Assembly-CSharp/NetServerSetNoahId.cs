using System;
using LitJson;

// Token: 0x0200078D RID: 1933
public class NetServerSetNoahId : NetBase
{
	// Token: 0x06003354 RID: 13140 RVA: 0x0011B408 File Offset: 0x00119608
	public NetServerSetNoahId() : this(string.Empty)
	{
	}

	// Token: 0x06003355 RID: 13141 RVA: 0x0011B418 File Offset: 0x00119618
	public NetServerSetNoahId(string noahId)
	{
		this.noahId = noahId;
	}

	// Token: 0x06003356 RID: 13142 RVA: 0x0011B428 File Offset: 0x00119628
	protected override void DoRequest()
	{
		base.SetAction("Sgn/setNoahId");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string setNoahIdString = instance.GetSetNoahIdString(this.noahId);
			Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(setNoahIdString);
		}
	}

	// Token: 0x06003357 RID: 13143 RVA: 0x0011B470 File Offset: 0x00119670
	protected override void DoDidSuccess(JsonData jdata)
	{
	}

	// Token: 0x06003358 RID: 13144 RVA: 0x0011B474 File Offset: 0x00119674
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000703 RID: 1795
	// (get) Token: 0x06003359 RID: 13145 RVA: 0x0011B478 File Offset: 0x00119678
	// (set) Token: 0x0600335A RID: 13146 RVA: 0x0011B480 File Offset: 0x00119680
	public string noahId { get; set; }

	// Token: 0x0600335B RID: 13147 RVA: 0x0011B48C File Offset: 0x0011968C
	private void SetParameter_NoahId()
	{
		base.WriteActionParamValue("noahId", this.noahId);
	}
}
