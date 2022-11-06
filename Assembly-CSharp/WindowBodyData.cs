using System;

// Token: 0x02000670 RID: 1648
public class WindowBodyData
{
	// Token: 0x06002C0D RID: 11277 RVA: 0x0010B6A0 File Offset: 0x001098A0
	public bool IsBgmStop()
	{
		return WindowBodyData.IsBgmStop(this.bgm);
	}

	// Token: 0x06002C0E RID: 11278 RVA: 0x0010B6B0 File Offset: 0x001098B0
	public static bool IsBgmStop(string bgm)
	{
		return bgm != null && bgm == "@stop";
	}

	// Token: 0x04002919 RID: 10521
	public int face_count;

	// Token: 0x0400291A RID: 10522
	public WindowProductData[] product;

	// Token: 0x0400291B RID: 10523
	public ArrowType arrow;

	// Token: 0x0400291C RID: 10524
	public string bgm;

	// Token: 0x0400291D RID: 10525
	public string se;

	// Token: 0x0400291E RID: 10526
	public string text_cell_id;
}
