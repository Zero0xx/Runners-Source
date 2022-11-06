using System;
using System.Collections.Generic;

// Token: 0x020004FE RID: 1278
public class RankingCallbackTemporarilySaved
{
	// Token: 0x06002630 RID: 9776 RVA: 0x000E8E90 File Offset: 0x000E7090
	public RankingCallbackTemporarilySaved(List<RankingUtil.Ranker> rankerList, RankingUtil.RankingScoreType score, RankingUtil.RankingRankerType type, int page, bool isNext, bool isPrev, bool isCashData, RankingManager.CallbackRankingData callback)
	{
		this.m_rankerList = rankerList;
		this.m_score = score;
		this.m_type = type;
		this.m_page = page;
		this.m_isNext = isNext;
		this.m_isPrev = isPrev;
		this.m_isCashData = isCashData;
		this.m_callback = callback;
	}

	// Token: 0x06002631 RID: 9777 RVA: 0x000E8EE8 File Offset: 0x000E70E8
	public void SendCallback()
	{
		if (this.m_callback != null)
		{
			this.m_callback(this.m_rankerList, this.m_score, this.m_type, this.m_page, this.m_isNext, this.m_isPrev, this.m_isCashData);
		}
	}

	// Token: 0x0400228E RID: 8846
	private List<RankingUtil.Ranker> m_rankerList;

	// Token: 0x0400228F RID: 8847
	private RankingUtil.RankingScoreType m_score;

	// Token: 0x04002290 RID: 8848
	private RankingUtil.RankingRankerType m_type = RankingUtil.RankingRankerType.RIVAL;

	// Token: 0x04002291 RID: 8849
	private int m_page;

	// Token: 0x04002292 RID: 8850
	private bool m_isNext;

	// Token: 0x04002293 RID: 8851
	private bool m_isPrev;

	// Token: 0x04002294 RID: 8852
	private bool m_isCashData;

	// Token: 0x04002295 RID: 8853
	private RankingManager.CallbackRankingData m_callback;
}
