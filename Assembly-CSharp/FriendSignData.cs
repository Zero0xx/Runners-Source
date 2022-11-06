using System;
using UnityEngine;

// Token: 0x02000265 RID: 613
public class FriendSignData
{
	// Token: 0x06001072 RID: 4210 RVA: 0x000602C4 File Offset: 0x0005E4C4
	public FriendSignData(int index, float distance, Texture2D texture, bool appear)
	{
		this.m_index = index;
		this.m_distance = distance;
		this.m_texture = texture;
		this.m_appear = appear;
	}

	// Token: 0x04000F07 RID: 3847
	public int m_index;

	// Token: 0x04000F08 RID: 3848
	public float m_distance;

	// Token: 0x04000F09 RID: 3849
	public Texture2D m_texture;

	// Token: 0x04000F0A RID: 3850
	public bool m_appear;
}
