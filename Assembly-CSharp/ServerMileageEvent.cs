using System;

// Token: 0x020007E5 RID: 2021
public class ServerMileageEvent
{
	// Token: 0x06003617 RID: 13847 RVA: 0x00120878 File Offset: 0x0011EA78
	public ServerMileageEvent()
	{
		this.Distance = 0;
		this.EventType = ServerMileageEvent.emEventType.Incentive;
		this.Content = 0;
		this.NumType = ServerConstants.NumType.Number;
		this.Num = 0;
		this.Level = 0;
	}

	// Token: 0x170007B6 RID: 1974
	// (get) Token: 0x06003618 RID: 13848 RVA: 0x001208B8 File Offset: 0x0011EAB8
	// (set) Token: 0x06003619 RID: 13849 RVA: 0x001208C0 File Offset: 0x0011EAC0
	public int Distance { get; set; }

	// Token: 0x170007B7 RID: 1975
	// (get) Token: 0x0600361A RID: 13850 RVA: 0x001208CC File Offset: 0x0011EACC
	// (set) Token: 0x0600361B RID: 13851 RVA: 0x001208D4 File Offset: 0x0011EAD4
	public ServerMileageEvent.emEventType EventType { get; set; }

	// Token: 0x170007B8 RID: 1976
	// (get) Token: 0x0600361C RID: 13852 RVA: 0x001208E0 File Offset: 0x0011EAE0
	// (set) Token: 0x0600361D RID: 13853 RVA: 0x001208E8 File Offset: 0x0011EAE8
	public int Content { get; set; }

	// Token: 0x170007B9 RID: 1977
	// (get) Token: 0x0600361E RID: 13854 RVA: 0x001208F4 File Offset: 0x0011EAF4
	// (set) Token: 0x0600361F RID: 13855 RVA: 0x001208FC File Offset: 0x0011EAFC
	public ServerConstants.NumType NumType { get; set; }

	// Token: 0x170007BA RID: 1978
	// (get) Token: 0x06003620 RID: 13856 RVA: 0x00120908 File Offset: 0x0011EB08
	// (set) Token: 0x06003621 RID: 13857 RVA: 0x00120910 File Offset: 0x0011EB10
	public int Num { get; set; }

	// Token: 0x170007BB RID: 1979
	// (get) Token: 0x06003622 RID: 13858 RVA: 0x0012091C File Offset: 0x0011EB1C
	// (set) Token: 0x06003623 RID: 13859 RVA: 0x00120924 File Offset: 0x0011EB24
	public int Level { get; set; }

	// Token: 0x020007E6 RID: 2022
	public enum emEventType
	{
		// Token: 0x04002D93 RID: 11667
		Incentive,
		// Token: 0x04002D94 RID: 11668
		BeginBonus,
		// Token: 0x04002D95 RID: 11669
		EndBonus,
		// Token: 0x04002D96 RID: 11670
		Goal
	}
}
