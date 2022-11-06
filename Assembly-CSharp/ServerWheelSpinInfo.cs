using System;

// Token: 0x0200082A RID: 2090
public class ServerWheelSpinInfo
{
	// Token: 0x06003846 RID: 14406 RVA: 0x0012919C File Offset: 0x0012739C
	public ServerWheelSpinInfo()
	{
		this.id = 1;
		this.start = NetBase.GetCurrentTime();
		this.end = NetBase.GetCurrentTime();
		this.param = string.Empty;
	}

	// Token: 0x1700086D RID: 2157
	// (get) Token: 0x06003847 RID: 14407 RVA: 0x001291D8 File Offset: 0x001273D8
	// (set) Token: 0x06003848 RID: 14408 RVA: 0x001291E0 File Offset: 0x001273E0
	public int id { get; set; }

	// Token: 0x1700086E RID: 2158
	// (get) Token: 0x06003849 RID: 14409 RVA: 0x001291EC File Offset: 0x001273EC
	// (set) Token: 0x0600384A RID: 14410 RVA: 0x001291F4 File Offset: 0x001273F4
	public DateTime start { get; set; }

	// Token: 0x1700086F RID: 2159
	// (get) Token: 0x0600384B RID: 14411 RVA: 0x00129200 File Offset: 0x00127400
	// (set) Token: 0x0600384C RID: 14412 RVA: 0x00129208 File Offset: 0x00127408
	public DateTime end { get; set; }

	// Token: 0x17000870 RID: 2160
	// (get) Token: 0x0600384D RID: 14413 RVA: 0x00129214 File Offset: 0x00127414
	// (set) Token: 0x0600384E RID: 14414 RVA: 0x0012921C File Offset: 0x0012741C
	public string param { get; set; }

	// Token: 0x17000871 RID: 2161
	// (get) Token: 0x0600384F RID: 14415 RVA: 0x00129228 File Offset: 0x00127428
	public bool isEnabled
	{
		get
		{
			bool result = false;
			if (NetBase.GetCurrentTime() >= this.start && NetBase.GetCurrentTime() < this.end)
			{
				result = true;
			}
			return result;
		}
	}

	// Token: 0x06003850 RID: 14416 RVA: 0x00129264 File Offset: 0x00127464
	public void Dump()
	{
	}

	// Token: 0x06003851 RID: 14417 RVA: 0x00129268 File Offset: 0x00127468
	public void CopyTo(ServerWheelSpinInfo to)
	{
		to.id = this.id;
		to.start = this.start;
		to.end = this.end;
		to.param = this.param;
	}
}
