using System;

// Token: 0x020007FA RID: 2042
public class ServerConsumedCostData
{
	// Token: 0x060036C5 RID: 14021 RVA: 0x00122244 File Offset: 0x00120444
	public ServerConsumedCostData()
	{
		this.consumedItemId = 0;
		this.itemId = 0;
		this.numItem = 0;
	}

	// Token: 0x170007FC RID: 2044
	// (get) Token: 0x060036C6 RID: 14022 RVA: 0x0012226C File Offset: 0x0012046C
	// (set) Token: 0x060036C7 RID: 14023 RVA: 0x00122274 File Offset: 0x00120474
	public int consumedItemId { get; set; }

	// Token: 0x170007FD RID: 2045
	// (get) Token: 0x060036C8 RID: 14024 RVA: 0x00122280 File Offset: 0x00120480
	// (set) Token: 0x060036C9 RID: 14025 RVA: 0x00122288 File Offset: 0x00120488
	public int itemId { get; set; }

	// Token: 0x170007FE RID: 2046
	// (get) Token: 0x060036CA RID: 14026 RVA: 0x00122294 File Offset: 0x00120494
	// (set) Token: 0x060036CB RID: 14027 RVA: 0x0012229C File Offset: 0x0012049C
	public int numItem { get; set; }

	// Token: 0x060036CC RID: 14028 RVA: 0x001222A8 File Offset: 0x001204A8
	public void CopyTo(ServerConsumedCostData dest)
	{
		dest.consumedItemId = this.consumedItemId;
		dest.itemId = this.itemId;
		dest.numItem = this.numItem;
	}
}
