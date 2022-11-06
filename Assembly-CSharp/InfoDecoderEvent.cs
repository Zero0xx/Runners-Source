using System;
using Text;

// Token: 0x020004F6 RID: 1270
public class InfoDecoderEvent : InfoDecoder
{
	// Token: 0x060025CC RID: 9676 RVA: 0x000E5E48 File Offset: 0x000E4048
	public InfoDecoderEvent(string messageInfo)
	{
		this.m_messageInfo = messageInfo;
		this.m_converter = new EventRankingServerInfoConverter(this.m_messageInfo);
	}

	// Token: 0x060025CD RID: 9677 RVA: 0x000E5E74 File Offset: 0x000E4074
	public override string GetCaption()
	{
		string commonText = TextUtility.GetCommonText("Ranking", "ranking_result_event_caption");
		if (!string.IsNullOrEmpty(commonText))
		{
			return commonText;
		}
		return string.Empty;
	}

	// Token: 0x060025CE RID: 9678 RVA: 0x000E5EA4 File Offset: 0x000E40A4
	public override string GetResultString()
	{
		return this.m_converter.Result;
	}

	// Token: 0x060025CF RID: 9679 RVA: 0x000E5EB4 File Offset: 0x000E40B4
	public override string GetMedalSpriteName()
	{
		return "ui_ranking_world_sonicmedal_blue";
	}

	// Token: 0x04002214 RID: 8724
	private string m_messageInfo = string.Empty;

	// Token: 0x04002215 RID: 8725
	private EventRankingServerInfoConverter m_converter;
}
