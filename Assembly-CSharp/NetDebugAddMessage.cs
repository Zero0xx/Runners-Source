using System;
using LitJson;

// Token: 0x020006D0 RID: 1744
public class NetDebugAddMessage : NetBase
{
	// Token: 0x06002E98 RID: 11928 RVA: 0x00111DDC File Offset: 0x0010FFDC
	public NetDebugAddMessage() : this(string.Empty, string.Empty, 0)
	{
	}

	// Token: 0x06002E99 RID: 11929 RVA: 0x00111DF0 File Offset: 0x0010FFF0
	public NetDebugAddMessage(string fromHspId, string toHspId, int messageType)
	{
		this.paramFromHspId = fromHspId;
		this.paramToHspId = toHspId;
		this.paramMessageType = messageType;
	}

	// Token: 0x06002E9A RID: 11930 RVA: 0x00111E18 File Offset: 0x00110018
	protected override void DoRequest()
	{
		base.SetAction("Debug/addMessage");
		this.SetParameter_Message();
	}

	// Token: 0x06002E9B RID: 11931 RVA: 0x00111E2C File Offset: 0x0011002C
	protected override void DoDidSuccess(JsonData jdata)
	{
	}

	// Token: 0x06002E9C RID: 11932 RVA: 0x00111E30 File Offset: 0x00110030
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000616 RID: 1558
	// (get) Token: 0x06002E9D RID: 11933 RVA: 0x00111E34 File Offset: 0x00110034
	// (set) Token: 0x06002E9E RID: 11934 RVA: 0x00111E3C File Offset: 0x0011003C
	public string paramFromHspId { get; set; }

	// Token: 0x17000617 RID: 1559
	// (get) Token: 0x06002E9F RID: 11935 RVA: 0x00111E48 File Offset: 0x00110048
	// (set) Token: 0x06002EA0 RID: 11936 RVA: 0x00111E50 File Offset: 0x00110050
	public string paramToHspId { get; set; }

	// Token: 0x17000618 RID: 1560
	// (get) Token: 0x06002EA1 RID: 11937 RVA: 0x00111E5C File Offset: 0x0011005C
	// (set) Token: 0x06002EA2 RID: 11938 RVA: 0x00111E64 File Offset: 0x00110064
	public int paramMessageType { get; set; }

	// Token: 0x06002EA3 RID: 11939 RVA: 0x00111E70 File Offset: 0x00110070
	private void SetParameter_Message()
	{
		base.WriteActionParamValue("hspFromId", this.paramFromHspId);
		base.WriteActionParamValue("hspToId", this.paramToHspId);
		base.WriteActionParamValue("messageKind", this.paramMessageType);
	}
}
