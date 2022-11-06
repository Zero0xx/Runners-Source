using System;
using UnityEngine;

// Token: 0x020009E2 RID: 2530
public class ResourceInfo
{
	// Token: 0x06004297 RID: 17047 RVA: 0x0015A70C File Offset: 0x0015890C
	public ResourceInfo(ResourceCategory category)
	{
		this.Category = category;
	}

	// Token: 0x17000918 RID: 2328
	// (get) Token: 0x06004298 RID: 17048 RVA: 0x0015A71C File Offset: 0x0015891C
	// (set) Token: 0x06004299 RID: 17049 RVA: 0x0015A724 File Offset: 0x00158924
	public GameObject ResObject { get; set; }

	// Token: 0x17000919 RID: 2329
	// (get) Token: 0x0600429A RID: 17050 RVA: 0x0015A730 File Offset: 0x00158930
	// (set) Token: 0x0600429B RID: 17051 RVA: 0x0015A738 File Offset: 0x00158938
	public bool DontDestroyOnChangeScene { get; set; }

	// Token: 0x1700091A RID: 2330
	// (get) Token: 0x0600429C RID: 17052 RVA: 0x0015A744 File Offset: 0x00158944
	// (set) Token: 0x0600429D RID: 17053 RVA: 0x0015A74C File Offset: 0x0015894C
	public ResourceCategory Category { get; set; }

	// Token: 0x1700091B RID: 2331
	// (get) Token: 0x0600429E RID: 17054 RVA: 0x0015A758 File Offset: 0x00158958
	// (set) Token: 0x0600429F RID: 17055 RVA: 0x0015A760 File Offset: 0x00158960
	public string PathName { get; set; }

	// Token: 0x1700091C RID: 2332
	// (get) Token: 0x060042A0 RID: 17056 RVA: 0x0015A76C File Offset: 0x0015896C
	// (set) Token: 0x060042A1 RID: 17057 RVA: 0x0015A774 File Offset: 0x00158974
	public bool AssetBundle { get; set; }

	// Token: 0x1700091D RID: 2333
	// (get) Token: 0x060042A2 RID: 17058 RVA: 0x0015A780 File Offset: 0x00158980
	// (set) Token: 0x060042A3 RID: 17059 RVA: 0x0015A788 File Offset: 0x00158988
	public bool Cashed { get; set; }

	// Token: 0x060042A4 RID: 17060 RVA: 0x0015A794 File Offset: 0x00158994
	public void CopyTo(ResourceInfo dest)
	{
		dest.ResObject = this.ResObject;
		dest.DontDestroyOnChangeScene = this.DontDestroyOnChangeScene;
		dest.Category = this.Category;
		dest.PathName = this.PathName;
		dest.AssetBundle = this.AssetBundle;
		dest.Cashed = this.Cashed;
	}
}
