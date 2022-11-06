using System;

// Token: 0x020007EE RID: 2030
public class ServerChaoData
{
	// Token: 0x06003644 RID: 13892 RVA: 0x00121120 File Offset: 0x0011F320
	public ServerChaoData()
	{
		this.Id = 0;
		this.Level = 0;
		this.Rarity = -1;
	}

	// Token: 0x170007BF RID: 1983
	// (get) Token: 0x06003645 RID: 13893 RVA: 0x00121148 File Offset: 0x0011F348
	// (set) Token: 0x06003646 RID: 13894 RVA: 0x00121150 File Offset: 0x0011F350
	public int Id { get; set; }

	// Token: 0x170007C0 RID: 1984
	// (get) Token: 0x06003647 RID: 13895 RVA: 0x0012115C File Offset: 0x0011F35C
	// (set) Token: 0x06003648 RID: 13896 RVA: 0x00121164 File Offset: 0x0011F364
	public int Level { get; set; }

	// Token: 0x170007C1 RID: 1985
	// (get) Token: 0x06003649 RID: 13897 RVA: 0x00121170 File Offset: 0x0011F370
	// (set) Token: 0x0600364A RID: 13898 RVA: 0x00121178 File Offset: 0x0011F378
	public int Rarity { get; set; }

	// Token: 0x0600364B RID: 13899 RVA: 0x00121184 File Offset: 0x0011F384
	public void Dump()
	{
	}

	// Token: 0x020007EF RID: 2031
	public enum RarityType
	{
		// Token: 0x04002DBE RID: 11710
		NORMAL,
		// Token: 0x04002DBF RID: 11711
		RARE,
		// Token: 0x04002DC0 RID: 11712
		SRARE,
		// Token: 0x04002DC1 RID: 11713
		PLAYER = 100,
		// Token: 0x04002DC2 RID: 11714
		CAMPAIGN
	}
}
