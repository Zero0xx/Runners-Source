using System;

// Token: 0x02000389 RID: 905
public class GameResultScoreInterporate
{
	// Token: 0x06001ACC RID: 6860 RVA: 0x0009E448 File Offset: 0x0009C648
	public GameResultScoreInterporate()
	{
		this.m_label = null;
		this.m_interpolate = null;
	}

	// Token: 0x170003DD RID: 989
	// (get) Token: 0x06001ACD RID: 6861 RVA: 0x0009E460 File Offset: 0x0009C660
	public bool IsEnd
	{
		get
		{
			return this.m_interpolate.IsEnd;
		}
	}

	// Token: 0x170003DE RID: 990
	// (get) Token: 0x06001ACE RID: 6862 RVA: 0x0009E470 File Offset: 0x0009C670
	// (set) Token: 0x06001ACF RID: 6863 RVA: 0x0009E480 File Offset: 0x0009C680
	public bool IsPause
	{
		get
		{
			return this.m_interpolate.IsPause;
		}
		set
		{
			this.m_interpolate.IsPause = value;
		}
	}

	// Token: 0x170003DF RID: 991
	// (get) Token: 0x06001AD0 RID: 6864 RVA: 0x0009E490 File Offset: 0x0009C690
	public float CurrentTime
	{
		get
		{
			return this.m_interpolate.CurrentTime;
		}
	}

	// Token: 0x06001AD1 RID: 6865 RVA: 0x0009E4A0 File Offset: 0x0009C6A0
	public void Setup(UILabel label)
	{
		this.m_label = label;
		this.m_interpolate = new HudValueInterpolate();
		this.m_score = 0L;
	}

	// Token: 0x06001AD2 RID: 6866 RVA: 0x0009E4BC File Offset: 0x0009C6BC
	public void AddScore(long addScore)
	{
		this.m_score += addScore;
		this.SetLabelStartValue(this.m_score);
	}

	// Token: 0x06001AD3 RID: 6867 RVA: 0x0009E4D8 File Offset: 0x0009C6D8
	public void SetLabelStartValue(long startValue)
	{
		if (this.m_label == null)
		{
			return;
		}
		this.m_label.text = GameResultUtility.GetScoreFormat(startValue);
	}

	// Token: 0x06001AD4 RID: 6868 RVA: 0x0009E500 File Offset: 0x0009C700
	public void PlayStart(long startValue, long endValue, float interpolateTime)
	{
		if (this.m_interpolate == null)
		{
			return;
		}
		this.m_interpolate.Setup(startValue, endValue, interpolateTime);
		this.m_interpolate.Reset();
		if (startValue == endValue)
		{
			this.PlaySkip();
		}
	}

	// Token: 0x06001AD5 RID: 6869 RVA: 0x0009E540 File Offset: 0x0009C740
	public void PlaySkip()
	{
		if (this.m_interpolate == null)
		{
			return;
		}
		long val = this.m_interpolate.ForceEnd();
		this.m_label.text = GameResultUtility.GetScoreFormat(val);
	}

	// Token: 0x06001AD6 RID: 6870 RVA: 0x0009E578 File Offset: 0x0009C778
	public long Update(float deltaTime)
	{
		if (this.m_interpolate == null)
		{
			return 0L;
		}
		if (this.m_label == null)
		{
			return 0L;
		}
		long num = this.m_interpolate.Update(deltaTime);
		this.m_label.text = GameResultUtility.GetScoreFormat(num);
		return num;
	}

	// Token: 0x0400182D RID: 6189
	private UILabel m_label;

	// Token: 0x0400182E RID: 6190
	private HudValueInterpolate m_interpolate;

	// Token: 0x0400182F RID: 6191
	private long m_score;
}
