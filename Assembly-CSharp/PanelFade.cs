using System;

// Token: 0x02000563 RID: 1379
public class PanelFade
{
	// Token: 0x06002A80 RID: 10880 RVA: 0x00107728 File Offset: 0x00105928
	public void Setup(UIPanel panel)
	{
		this.m_panel = panel;
		this.m_hudInterpolate = new HudValueInterpolate();
	}

	// Token: 0x06002A81 RID: 10881 RVA: 0x0010773C File Offset: 0x0010593C
	public void PlayStart(float fadeTime, bool isFadeIn)
	{
		if (isFadeIn)
		{
			this.m_hudInterpolate.Setup(0L, 10000L, fadeTime);
		}
		else
		{
			this.m_hudInterpolate.Setup(10000L, 0L, fadeTime);
		}
	}

	// Token: 0x06002A82 RID: 10882 RVA: 0x00107774 File Offset: 0x00105974
	public void Update(float deltaTime)
	{
		if (this.m_hudInterpolate == null)
		{
			return;
		}
		if (!this.m_hudInterpolate.IsEnd)
		{
			long num = this.m_hudInterpolate.Update(deltaTime);
			this.m_panel.alpha = (float)(num / 10000L);
		}
	}

	// Token: 0x06002A83 RID: 10883 RVA: 0x001077C0 File Offset: 0x001059C0
	public bool IsEndFade()
	{
		return this.m_hudInterpolate == null || this.m_hudInterpolate.IsEnd;
	}

	// Token: 0x040025EE RID: 9710
	private const int InterpolateEndValue = 10000;

	// Token: 0x040025EF RID: 9711
	private UIPanel m_panel;

	// Token: 0x040025F0 RID: 9712
	private HudValueInterpolate m_hudInterpolate;
}
