using System;

// Token: 0x020008D8 RID: 2264
public class CtystalParam
{
	// Token: 0x06003C22 RID: 15394 RVA: 0x0013CE4C File Offset: 0x0013B04C
	public CtystalParam(bool big, string objName, string model, string effect, string se, int score, bool scoreIcon)
	{
		this.m_big = big;
		this.m_objName = objName;
		this.m_model = model;
		this.m_effect = effect;
		this.m_se = se;
		this.m_score = score;
		this.m_scoreIcon = scoreIcon;
	}

	// Token: 0x0400346A RID: 13418
	public bool m_big;

	// Token: 0x0400346B RID: 13419
	public string m_objName;

	// Token: 0x0400346C RID: 13420
	public string m_model;

	// Token: 0x0400346D RID: 13421
	public string m_effect;

	// Token: 0x0400346E RID: 13422
	public string m_se;

	// Token: 0x0400346F RID: 13423
	public int m_score;

	// Token: 0x04003470 RID: 13424
	public bool m_scoreIcon;
}
