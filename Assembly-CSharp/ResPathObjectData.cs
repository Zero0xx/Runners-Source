using System;
using UnityEngine;

// Token: 0x0200029E RID: 670
public class ResPathObjectData
{
	// Token: 0x170002EC RID: 748
	// (get) Token: 0x06001261 RID: 4705 RVA: 0x00066A48 File Offset: 0x00064C48
	// (set) Token: 0x06001262 RID: 4706 RVA: 0x00066A50 File Offset: 0x00064C50
	public string name { get; set; }

	// Token: 0x170002ED RID: 749
	// (get) Token: 0x06001263 RID: 4707 RVA: 0x00066A5C File Offset: 0x00064C5C
	// (set) Token: 0x06001264 RID: 4708 RVA: 0x00066A64 File Offset: 0x00064C64
	public byte playbackType { get; set; }

	// Token: 0x170002EE RID: 750
	// (get) Token: 0x06001265 RID: 4709 RVA: 0x00066A70 File Offset: 0x00064C70
	// (set) Token: 0x06001266 RID: 4710 RVA: 0x00066A78 File Offset: 0x00064C78
	public byte flags { get; set; }

	// Token: 0x170002EF RID: 751
	// (get) Token: 0x06001267 RID: 4711 RVA: 0x00066A84 File Offset: 0x00064C84
	// (set) Token: 0x06001268 RID: 4712 RVA: 0x00066A8C File Offset: 0x00064C8C
	public ushort numKeys { get; set; }

	// Token: 0x170002F0 RID: 752
	// (get) Token: 0x06001269 RID: 4713 RVA: 0x00066A98 File Offset: 0x00064C98
	// (set) Token: 0x0600126A RID: 4714 RVA: 0x00066AA0 File Offset: 0x00064CA0
	public float length { get; set; }

	// Token: 0x170002F1 RID: 753
	// (get) Token: 0x0600126B RID: 4715 RVA: 0x00066AAC File Offset: 0x00064CAC
	// (set) Token: 0x0600126C RID: 4716 RVA: 0x00066AB4 File Offset: 0x00064CB4
	public ushort[] knotType { get; set; }

	// Token: 0x170002F2 RID: 754
	// (get) Token: 0x0600126D RID: 4717 RVA: 0x00066AC0 File Offset: 0x00064CC0
	// (set) Token: 0x0600126E RID: 4718 RVA: 0x00066AC8 File Offset: 0x00064CC8
	public float[] distance { get; set; }

	// Token: 0x170002F3 RID: 755
	// (get) Token: 0x0600126F RID: 4719 RVA: 0x00066AD4 File Offset: 0x00064CD4
	// (set) Token: 0x06001270 RID: 4720 RVA: 0x00066ADC File Offset: 0x00064CDC
	public Vector3[] position { get; set; }

	// Token: 0x170002F4 RID: 756
	// (get) Token: 0x06001271 RID: 4721 RVA: 0x00066AE8 File Offset: 0x00064CE8
	// (set) Token: 0x06001272 RID: 4722 RVA: 0x00066AF0 File Offset: 0x00064CF0
	public Vector3[] normal { get; set; }

	// Token: 0x170002F5 RID: 757
	// (get) Token: 0x06001273 RID: 4723 RVA: 0x00066AFC File Offset: 0x00064CFC
	// (set) Token: 0x06001274 RID: 4724 RVA: 0x00066B04 File Offset: 0x00064D04
	public Vector3[] tangent { get; set; }

	// Token: 0x170002F6 RID: 758
	// (get) Token: 0x06001275 RID: 4725 RVA: 0x00066B10 File Offset: 0x00064D10
	// (set) Token: 0x06001276 RID: 4726 RVA: 0x00066B18 File Offset: 0x00064D18
	public uint numVertices { get; set; }

	// Token: 0x170002F7 RID: 759
	// (get) Token: 0x06001277 RID: 4727 RVA: 0x00066B24 File Offset: 0x00064D24
	// (set) Token: 0x06001278 RID: 4728 RVA: 0x00066B2C File Offset: 0x00064D2C
	public Vector3[] vertices { get; set; }

	// Token: 0x170002F8 RID: 760
	// (get) Token: 0x06001279 RID: 4729 RVA: 0x00066B38 File Offset: 0x00064D38
	// (set) Token: 0x0600127A RID: 4730 RVA: 0x00066B40 File Offset: 0x00064D40
	public Vector3 min { get; set; }

	// Token: 0x170002F9 RID: 761
	// (get) Token: 0x0600127B RID: 4731 RVA: 0x00066B4C File Offset: 0x00064D4C
	// (set) Token: 0x0600127C RID: 4732 RVA: 0x00066B54 File Offset: 0x00064D54
	public Vector3 max { get; set; }

	// Token: 0x170002FA RID: 762
	// (get) Token: 0x0600127D RID: 4733 RVA: 0x00066B60 File Offset: 0x00064D60
	// (set) Token: 0x0600127E RID: 4734 RVA: 0x00066B68 File Offset: 0x00064D68
	public ulong uid { get; set; }
}
