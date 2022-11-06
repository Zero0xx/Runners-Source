using System;

// Token: 0x020008F1 RID: 2289
[Serializable]
public class ObjItemPointParameter : SpawnableParameter
{
	// Token: 0x06003C8C RID: 15500 RVA: 0x0013E8C0 File Offset: 0x0013CAC0
	public ObjItemPointParameter() : base("ObjItemPoint")
	{
		this.m_tblID = 0;
	}

	// Token: 0x040034B2 RID: 13490
	public int m_tblID;
}
