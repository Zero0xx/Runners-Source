using System;
using UnityEngine;

// Token: 0x02000225 RID: 549
public class RaidBossBoostGagueColor : MonoBehaviour
{
	// Token: 0x1700022D RID: 557
	// (get) Token: 0x06000E9F RID: 3743 RVA: 0x00054E30 File Offset: 0x00053030
	public Color Level1
	{
		get
		{
			float r = this.m_level1R / 255f;
			float g = this.m_level1G / 255f;
			float b = this.m_level1B / 255f;
			return new Color(r, g, b);
		}
	}

	// Token: 0x1700022E RID: 558
	// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x00054E6C File Offset: 0x0005306C
	public Color Level2
	{
		get
		{
			float r = this.m_level2R / 255f;
			float g = this.m_level2G / 255f;
			float b = this.m_level2B / 255f;
			return new Color(r, g, b);
		}
	}

	// Token: 0x1700022F RID: 559
	// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x00054EA8 File Offset: 0x000530A8
	public Color Level3
	{
		get
		{
			float r = this.m_level3R / 255f;
			float g = this.m_level3G / 255f;
			float b = this.m_level3B / 255f;
			return new Color(r, g, b);
		}
	}

	// Token: 0x04000C81 RID: 3201
	[SerializeField]
	private float m_level1R;

	// Token: 0x04000C82 RID: 3202
	[SerializeField]
	private float m_level1G;

	// Token: 0x04000C83 RID: 3203
	[SerializeField]
	private float m_level1B;

	// Token: 0x04000C84 RID: 3204
	[SerializeField]
	private float m_level2R;

	// Token: 0x04000C85 RID: 3205
	[SerializeField]
	private float m_level2G;

	// Token: 0x04000C86 RID: 3206
	[SerializeField]
	private float m_level2B;

	// Token: 0x04000C87 RID: 3207
	[SerializeField]
	private float m_level3R;

	// Token: 0x04000C88 RID: 3208
	[SerializeField]
	private float m_level3G;

	// Token: 0x04000C89 RID: 3209
	[SerializeField]
	private float m_level3B;
}
