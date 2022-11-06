using System;
using UnityEngine;

// Token: 0x02000201 RID: 513
public class ykTextureSheetSharedMaterialAnimation : ykTextureSheetAnimation
{
	// Token: 0x06000DA1 RID: 3489 RVA: 0x0005012C File Offset: 0x0004E32C
	protected override Material GetMaterial()
	{
		return base.renderer.sharedMaterial;
	}

	// Token: 0x06000DA2 RID: 3490 RVA: 0x0005013C File Offset: 0x0004E33C
	protected override bool IsValidChange()
	{
		return false;
	}
}
