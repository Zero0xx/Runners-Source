using System;

// Token: 0x020007F0 RID: 2032
public class ServerChaoRentalState
{
	// Token: 0x0600364C RID: 13900 RVA: 0x00121188 File Offset: 0x0011F388
	public ServerChaoRentalState()
	{
		this.FriendId = string.Empty;
		this.Name = string.Empty;
		this.Url = string.Empty;
		this.ChaoData = null;
		this.RentalState = 0;
		this.NextRentalAt = 0L;
		this.TimeSinceStartup = 0f;
	}

	// Token: 0x170007C2 RID: 1986
	// (get) Token: 0x0600364D RID: 13901 RVA: 0x001211E0 File Offset: 0x0011F3E0
	// (set) Token: 0x0600364E RID: 13902 RVA: 0x001211E8 File Offset: 0x0011F3E8
	public string FriendId { get; set; }

	// Token: 0x170007C3 RID: 1987
	// (get) Token: 0x0600364F RID: 13903 RVA: 0x001211F4 File Offset: 0x0011F3F4
	// (set) Token: 0x06003650 RID: 13904 RVA: 0x001211FC File Offset: 0x0011F3FC
	public string Name { get; set; }

	// Token: 0x170007C4 RID: 1988
	// (get) Token: 0x06003651 RID: 13905 RVA: 0x00121208 File Offset: 0x0011F408
	// (set) Token: 0x06003652 RID: 13906 RVA: 0x00121210 File Offset: 0x0011F410
	public string Url { get; set; }

	// Token: 0x170007C5 RID: 1989
	// (get) Token: 0x06003653 RID: 13907 RVA: 0x0012121C File Offset: 0x0011F41C
	// (set) Token: 0x06003654 RID: 13908 RVA: 0x00121224 File Offset: 0x0011F424
	public ServerChaoData ChaoData { get; set; }

	// Token: 0x170007C6 RID: 1990
	// (get) Token: 0x06003655 RID: 13909 RVA: 0x00121230 File Offset: 0x0011F430
	// (set) Token: 0x06003656 RID: 13910 RVA: 0x00121238 File Offset: 0x0011F438
	public int RentalState { get; set; }

	// Token: 0x170007C7 RID: 1991
	// (get) Token: 0x06003657 RID: 13911 RVA: 0x00121244 File Offset: 0x0011F444
	// (set) Token: 0x06003658 RID: 13912 RVA: 0x0012124C File Offset: 0x0011F44C
	public long NextRentalAt { get; set; }

	// Token: 0x170007C8 RID: 1992
	// (get) Token: 0x06003659 RID: 13913 RVA: 0x00121258 File Offset: 0x0011F458
	public bool IsRented
	{
		get
		{
			return 1 == this.RentalState;
		}
	}

	// Token: 0x170007C9 RID: 1993
	// (get) Token: 0x0600365A RID: 13914 RVA: 0x00121264 File Offset: 0x0011F464
	public bool IsRentalable
	{
		get
		{
			return 0L == this.NextRentalAt;
		}
	}

	// Token: 0x170007CA RID: 1994
	// (get) Token: 0x0600365B RID: 13915 RVA: 0x00121270 File Offset: 0x0011F470
	// (set) Token: 0x0600365C RID: 13916 RVA: 0x00121278 File Offset: 0x0011F478
	public float TimeSinceStartup { get; set; }

	// Token: 0x0600365D RID: 13917 RVA: 0x00121284 File Offset: 0x0011F484
	public void Dump()
	{
	}
}
