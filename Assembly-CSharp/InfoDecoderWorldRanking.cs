using System;
using Text;

// Token: 0x020004F5 RID: 1269
public class InfoDecoderWorldRanking : InfoDecoder
{
	// Token: 0x060025C8 RID: 9672 RVA: 0x000E5DD4 File Offset: 0x000E3FD4
	public InfoDecoderWorldRanking(string messageInfo)
	{
		this.m_messageInfo = messageInfo;
		this.m_converter = new RankingServerInfoConverter(this.m_messageInfo);
	}

	// Token: 0x060025C9 RID: 9673 RVA: 0x000E5E00 File Offset: 0x000E4000
	public override string GetCaption()
	{
		string commonText = TextUtility.GetCommonText("Ranking", "ranking_result_all_caption");
		if (!string.IsNullOrEmpty(commonText))
		{
			return commonText;
		}
		return string.Empty;
	}

	// Token: 0x060025CA RID: 9674 RVA: 0x000E5E30 File Offset: 0x000E4030
	public override string GetResultString()
	{
		return this.m_converter.rankingResultAllText;
	}

	// Token: 0x060025CB RID: 9675 RVA: 0x000E5E40 File Offset: 0x000E4040
	public override string GetMedalSpriteName()
	{
		return "ui_ranking_world_sonicmedal_blue";
	}

	// Token: 0x04002212 RID: 8722
	private string m_messageInfo = string.Empty;

	// Token: 0x04002213 RID: 8723
	private RankingServerInfoConverter m_converter;
}
