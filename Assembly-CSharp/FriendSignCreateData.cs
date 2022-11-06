using System;
using UnityEngine;

// Token: 0x020008EA RID: 2282
public class FriendSignCreateData
{
	// Token: 0x06003C62 RID: 15458 RVA: 0x0013DA58 File Offset: 0x0013BC58
	public FriendSignCreateData(Texture2D texture)
	{
		this.m_create = false;
		this.m_texture = texture;
	}

	// Token: 0x04003497 RID: 13463
	public bool m_create;

	// Token: 0x04003498 RID: 13464
	public Texture2D m_texture;
}
