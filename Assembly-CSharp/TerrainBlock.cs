using System;

// Token: 0x02000289 RID: 649
public class TerrainBlock
{
	// Token: 0x060011CD RID: 4557 RVA: 0x00064890 File Offset: 0x00062A90
	public TerrainBlock(string name, TransformParam transform)
	{
		this.m_name = name;
		this.m_transform = transform;
	}

	// Token: 0x170002DA RID: 730
	// (get) Token: 0x060011CF RID: 4559 RVA: 0x000648B4 File Offset: 0x00062AB4
	// (set) Token: 0x060011CE RID: 4558 RVA: 0x000648A8 File Offset: 0x00062AA8
	public string m_name { get; private set; }

	// Token: 0x170002DB RID: 731
	// (get) Token: 0x060011D1 RID: 4561 RVA: 0x000648C8 File Offset: 0x00062AC8
	// (set) Token: 0x060011D0 RID: 4560 RVA: 0x000648BC File Offset: 0x00062ABC
	public TransformParam m_transform { get; private set; }
}
