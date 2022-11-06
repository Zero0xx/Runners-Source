using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x0200078C RID: 1932
public class NetServerSendApollo : NetBase
{
	// Token: 0x0600334A RID: 13130 RVA: 0x0011B2D8 File Offset: 0x001194D8
	public NetServerSendApollo() : this(-1, null)
	{
	}

	// Token: 0x0600334B RID: 13131 RVA: 0x0011B2E4 File Offset: 0x001194E4
	public NetServerSendApollo(int type, string[] value)
	{
		this.type = type;
		this.value = value;
	}

	// Token: 0x0600334C RID: 13132 RVA: 0x0011B2FC File Offset: 0x001194FC
	protected override void DoRequest()
	{
		base.SetAction("Sgn/sendApollo");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			List<string> list = new List<string>();
			if (this.value != null)
			{
				foreach (string item in this.value)
				{
					list.Add(item);
				}
			}
			string sendApolloString = instance.GetSendApolloString(this.type, list);
			Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(sendApolloString);
		}
	}

	// Token: 0x0600334D RID: 13133 RVA: 0x0011B384 File Offset: 0x00119584
	protected override void DoDidSuccess(JsonData jdata)
	{
	}

	// Token: 0x0600334E RID: 13134 RVA: 0x0011B388 File Offset: 0x00119588
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000701 RID: 1793
	// (get) Token: 0x0600334F RID: 13135 RVA: 0x0011B38C File Offset: 0x0011958C
	// (set) Token: 0x06003350 RID: 13136 RVA: 0x0011B394 File Offset: 0x00119594
	public int type { get; set; }

	// Token: 0x17000702 RID: 1794
	// (get) Token: 0x06003351 RID: 13137 RVA: 0x0011B3A0 File Offset: 0x001195A0
	// (set) Token: 0x06003352 RID: 13138 RVA: 0x0011B3A8 File Offset: 0x001195A8
	public string[] value { get; set; }

	// Token: 0x06003353 RID: 13139 RVA: 0x0011B3B4 File Offset: 0x001195B4
	private void SetParameter_Data()
	{
		base.WriteActionParamValue("type", this.type);
		if (this.value != null && this.value.Length != 0)
		{
			base.WriteActionParamArray("value", new List<object>(this.value));
		}
	}
}
