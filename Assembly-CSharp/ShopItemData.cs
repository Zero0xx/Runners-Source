using System;

// Token: 0x02000427 RID: 1063
public class ShopItemData
{
	// Token: 0x17000474 RID: 1140
	// (get) Token: 0x06002040 RID: 8256 RVA: 0x000C231C File Offset: 0x000C051C
	// (set) Token: 0x06002041 RID: 8257 RVA: 0x000C2324 File Offset: 0x000C0524
	public int number { get; set; }

	// Token: 0x17000475 RID: 1141
	// (get) Token: 0x06002042 RID: 8258 RVA: 0x000C2330 File Offset: 0x000C0530
	// (set) Token: 0x06002043 RID: 8259 RVA: 0x000C2338 File Offset: 0x000C0538
	public string name { get; private set; }

	// Token: 0x17000476 RID: 1142
	// (get) Token: 0x06002044 RID: 8260 RVA: 0x000C2344 File Offset: 0x000C0544
	// (set) Token: 0x06002045 RID: 8261 RVA: 0x000C234C File Offset: 0x000C054C
	public int rings { get; set; }

	// Token: 0x17000477 RID: 1143
	// (get) Token: 0x06002046 RID: 8262 RVA: 0x000C2358 File Offset: 0x000C0558
	// (set) Token: 0x06002047 RID: 8263 RVA: 0x000C2360 File Offset: 0x000C0560
	public string details { get; private set; }

	// Token: 0x17000478 RID: 1144
	// (get) Token: 0x06002048 RID: 8264 RVA: 0x000C236C File Offset: 0x000C056C
	public int id
	{
		get
		{
			return this.number - 1;
		}
	}

	// Token: 0x17000479 RID: 1145
	// (get) Token: 0x06002049 RID: 8265 RVA: 0x000C2378 File Offset: 0x000C0578
	public int index
	{
		get
		{
			return this.id;
		}
	}

	// Token: 0x1700047A RID: 1146
	// (get) Token: 0x0600204A RID: 8266 RVA: 0x000C2380 File Offset: 0x000C0580
	public bool IsValidate
	{
		get
		{
			return this.id != -1;
		}
	}

	// Token: 0x0600204B RID: 8267 RVA: 0x000C2390 File Offset: 0x000C0590
	public void SetName(string name)
	{
		this.name = name;
	}

	// Token: 0x0600204C RID: 8268 RVA: 0x000C239C File Offset: 0x000C059C
	public void SetDetails(string details)
	{
		this.details = details;
	}

	// Token: 0x04001D07 RID: 7431
	public const int ID_NONE = -1;

	// Token: 0x04001D08 RID: 7432
	public const int ID_ORIGIN = 0;
}
