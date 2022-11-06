using System;

// Token: 0x020008CE RID: 2254
public class ObjEventCrystal10 : ObjEventCrystalBase
{
	// Token: 0x06003BF6 RID: 15350 RVA: 0x0013C640 File Offset: 0x0013A840
	protected override string GetModelName()
	{
		return EventSPStageObjectTable.GetItemData(EventSPStageObjectTableItem.SPCrystal10Model);
	}

	// Token: 0x06003BF7 RID: 15351 RVA: 0x0013C648 File Offset: 0x0013A848
	protected override int GetAddCount()
	{
		return 10;
	}

	// Token: 0x06003BF8 RID: 15352 RVA: 0x0013C64C File Offset: 0x0013A84C
	protected override EventCtystalType GetOriginalType()
	{
		return EventCtystalType.BIG;
	}
}
