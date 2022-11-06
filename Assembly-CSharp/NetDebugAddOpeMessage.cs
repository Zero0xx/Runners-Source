using System;
using LitJson;

// Token: 0x020006D1 RID: 1745
public class NetDebugAddOpeMessage : NetBase
{
	// Token: 0x06002EA4 RID: 11940 RVA: 0x00111EB8 File Offset: 0x001100B8
	public NetDebugAddOpeMessage(NetDebugAddOpeMessage.OpeMsgInfo info)
	{
		this.paramOpeMsgInfo = info;
	}

	// Token: 0x06002EA5 RID: 11941 RVA: 0x00111EC8 File Offset: 0x001100C8
	protected override void DoRequest()
	{
		base.SetAction("Debug/addOpeMessage");
		this.SetParameter_Message();
	}

	// Token: 0x06002EA6 RID: 11942 RVA: 0x00111EDC File Offset: 0x001100DC
	protected override void DoDidSuccess(JsonData jdata)
	{
	}

	// Token: 0x06002EA7 RID: 11943 RVA: 0x00111EE0 File Offset: 0x001100E0
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x06002EA8 RID: 11944 RVA: 0x00111EE4 File Offset: 0x001100E4
	private void SetParameter_Message()
	{
		if (this.paramOpeMsgInfo != null)
		{
			base.WriteActionParamValue("messageKind", this.paramOpeMsgInfo.messageKind);
			base.WriteActionParamValue("infoid", this.paramOpeMsgInfo.infoId);
			base.WriteActionParamValue("item_id", this.paramOpeMsgInfo.itemId);
			base.WriteActionParamValue("num_item", this.paramOpeMsgInfo.numItem);
			base.WriteActionParamValue("additional_info_1", this.paramOpeMsgInfo.additionalInfo1);
			base.WriteActionParamValue("additional_info_2", this.paramOpeMsgInfo.additionalInfo2);
			base.WriteActionParamValue("msg_title", this.paramOpeMsgInfo.msgTitle);
			base.WriteActionParamValue("msg_content", this.paramOpeMsgInfo.msgContent);
			base.WriteActionParamValue("msg_image_id", this.paramOpeMsgInfo.msgImageId);
			base.WriteActionParamValue("hspToId", this.paramOpeMsgInfo.userID);
		}
	}

	// Token: 0x04002A43 RID: 10819
	private NetDebugAddOpeMessage.OpeMsgInfo paramOpeMsgInfo;

	// Token: 0x020006D2 RID: 1746
	public class OpeMsgInfo
	{
		// Token: 0x04002A44 RID: 10820
		public string userID;

		// Token: 0x04002A45 RID: 10821
		public int messageKind;

		// Token: 0x04002A46 RID: 10822
		public int infoId;

		// Token: 0x04002A47 RID: 10823
		public int itemId;

		// Token: 0x04002A48 RID: 10824
		public int numItem;

		// Token: 0x04002A49 RID: 10825
		public int additionalInfo1;

		// Token: 0x04002A4A RID: 10826
		public int additionalInfo2;

		// Token: 0x04002A4B RID: 10827
		public string msgTitle;

		// Token: 0x04002A4C RID: 10828
		public string msgContent;

		// Token: 0x04002A4D RID: 10829
		public string msgImageId;
	}
}
