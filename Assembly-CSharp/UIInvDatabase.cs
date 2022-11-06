using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003AE RID: 942
[ExecuteInEditMode]
public class UIInvDatabase : MonoBehaviour
{
	// Token: 0x170003F5 RID: 1013
	// (get) Token: 0x06001B87 RID: 7047 RVA: 0x000A2990 File Offset: 0x000A0B90
	public static UIInvDatabase[] list
	{
		get
		{
			if (UIInvDatabase.mIsDirty)
			{
				UIInvDatabase.mIsDirty = false;
				UIInvDatabase.mList = NGUITools.FindActive<UIInvDatabase>();
			}
			return UIInvDatabase.mList;
		}
	}

	// Token: 0x06001B88 RID: 7048 RVA: 0x000A29B4 File Offset: 0x000A0BB4
	private void OnEnable()
	{
		UIInvDatabase.mIsDirty = true;
	}

	// Token: 0x06001B89 RID: 7049 RVA: 0x000A29BC File Offset: 0x000A0BBC
	private void OnDisable()
	{
		UIInvDatabase.mIsDirty = true;
	}

	// Token: 0x06001B8A RID: 7050 RVA: 0x000A29C4 File Offset: 0x000A0BC4
	private UIInvBaseItem GetItem(int id16)
	{
		int i = 0;
		int count = this.items.Count;
		while (i < count)
		{
			UIInvBaseItem uiinvBaseItem = this.items[i];
			if (uiinvBaseItem.id16 == id16)
			{
				return uiinvBaseItem;
			}
			i++;
		}
		return null;
	}

	// Token: 0x06001B8B RID: 7051 RVA: 0x000A2A0C File Offset: 0x000A0C0C
	private static UIInvDatabase GetDatabase(int dbID)
	{
		int i = 0;
		int num = UIInvDatabase.list.Length;
		while (i < num)
		{
			UIInvDatabase uiinvDatabase = UIInvDatabase.list[i];
			if (uiinvDatabase.databaseID == dbID)
			{
				return uiinvDatabase;
			}
			i++;
		}
		return null;
	}

	// Token: 0x06001B8C RID: 7052 RVA: 0x000A2A4C File Offset: 0x000A0C4C
	public static UIInvBaseItem FindByID(int id32)
	{
		UIInvDatabase database = UIInvDatabase.GetDatabase(id32 >> 16);
		return (!(database != null)) ? null : database.GetItem(id32 & 65535);
	}

	// Token: 0x06001B8D RID: 7053 RVA: 0x000A2A84 File Offset: 0x000A0C84
	public static UIInvBaseItem FindByName(string exact)
	{
		int i = 0;
		int num = UIInvDatabase.list.Length;
		while (i < num)
		{
			UIInvDatabase uiinvDatabase = UIInvDatabase.list[i];
			int j = 0;
			int count = uiinvDatabase.items.Count;
			while (j < count)
			{
				UIInvBaseItem uiinvBaseItem = uiinvDatabase.items[j];
				if (uiinvBaseItem.name == exact)
				{
					return uiinvBaseItem;
				}
				j++;
			}
			i++;
		}
		return null;
	}

	// Token: 0x06001B8E RID: 7054 RVA: 0x000A2AF8 File Offset: 0x000A0CF8
	public static int FindItemID(UIInvBaseItem item)
	{
		int i = 0;
		int num = UIInvDatabase.list.Length;
		while (i < num)
		{
			UIInvDatabase uiinvDatabase = UIInvDatabase.list[i];
			if (uiinvDatabase.items.Contains(item))
			{
				return uiinvDatabase.databaseID << 16 | item.id16;
			}
			i++;
		}
		return -1;
	}

	// Token: 0x04001918 RID: 6424
	private static UIInvDatabase[] mList;

	// Token: 0x04001919 RID: 6425
	private static bool mIsDirty = true;

	// Token: 0x0400191A RID: 6426
	public int databaseID;

	// Token: 0x0400191B RID: 6427
	public List<UIInvBaseItem> items = new List<UIInvBaseItem>();

	// Token: 0x0400191C RID: 6428
	public UIAtlas iconAtlas;
}
