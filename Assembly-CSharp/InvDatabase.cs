using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000056 RID: 86
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Examples/Item Database")]
public class InvDatabase : MonoBehaviour
{
	// Token: 0x17000069 RID: 105
	// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000B8E8 File Offset: 0x00009AE8
	public static InvDatabase[] list
	{
		get
		{
			if (InvDatabase.mIsDirty)
			{
				InvDatabase.mIsDirty = false;
				InvDatabase.mList = NGUITools.FindActive<InvDatabase>();
			}
			return InvDatabase.mList;
		}
	}

	// Token: 0x060002A8 RID: 680 RVA: 0x0000B90C File Offset: 0x00009B0C
	private void OnEnable()
	{
		InvDatabase.mIsDirty = true;
	}

	// Token: 0x060002A9 RID: 681 RVA: 0x0000B914 File Offset: 0x00009B14
	private void OnDisable()
	{
		InvDatabase.mIsDirty = true;
	}

	// Token: 0x060002AA RID: 682 RVA: 0x0000B91C File Offset: 0x00009B1C
	private InvBaseItem GetItem(int id16)
	{
		int i = 0;
		int count = this.items.Count;
		while (i < count)
		{
			InvBaseItem invBaseItem = this.items[i];
			if (invBaseItem.id16 == id16)
			{
				return invBaseItem;
			}
			i++;
		}
		return null;
	}

	// Token: 0x060002AB RID: 683 RVA: 0x0000B964 File Offset: 0x00009B64
	private static InvDatabase GetDatabase(int dbID)
	{
		int i = 0;
		int num = InvDatabase.list.Length;
		while (i < num)
		{
			InvDatabase invDatabase = InvDatabase.list[i];
			if (invDatabase.databaseID == dbID)
			{
				return invDatabase;
			}
			i++;
		}
		return null;
	}

	// Token: 0x060002AC RID: 684 RVA: 0x0000B9A4 File Offset: 0x00009BA4
	public static InvBaseItem FindByID(int id32)
	{
		InvDatabase database = InvDatabase.GetDatabase(id32 >> 16);
		return (!(database != null)) ? null : database.GetItem(id32 & 65535);
	}

	// Token: 0x060002AD RID: 685 RVA: 0x0000B9DC File Offset: 0x00009BDC
	public static InvBaseItem FindByName(string exact)
	{
		int i = 0;
		int num = InvDatabase.list.Length;
		while (i < num)
		{
			InvDatabase invDatabase = InvDatabase.list[i];
			int j = 0;
			int count = invDatabase.items.Count;
			while (j < count)
			{
				InvBaseItem invBaseItem = invDatabase.items[j];
				if (invBaseItem.name == exact)
				{
					return invBaseItem;
				}
				j++;
			}
			i++;
		}
		return null;
	}

	// Token: 0x060002AE RID: 686 RVA: 0x0000BA50 File Offset: 0x00009C50
	public static int FindItemID(InvBaseItem item)
	{
		int i = 0;
		int num = InvDatabase.list.Length;
		while (i < num)
		{
			InvDatabase invDatabase = InvDatabase.list[i];
			if (invDatabase.items.Contains(item))
			{
				return invDatabase.databaseID << 16 | item.id16;
			}
			i++;
		}
		return -1;
	}

	// Token: 0x0400014E RID: 334
	private static InvDatabase[] mList;

	// Token: 0x0400014F RID: 335
	private static bool mIsDirty = true;

	// Token: 0x04000150 RID: 336
	public int databaseID;

	// Token: 0x04000151 RID: 337
	public List<InvBaseItem> items = new List<InvBaseItem>();

	// Token: 0x04000152 RID: 338
	public UIAtlas iconAtlas;
}
