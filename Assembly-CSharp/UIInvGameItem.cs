using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003AF RID: 943
[Serializable]
public class UIInvGameItem
{
	// Token: 0x06001B8F RID: 7055 RVA: 0x000A2B4C File Offset: 0x000A0D4C
	public UIInvGameItem(int id)
	{
		this.mBaseItemID = id;
	}

	// Token: 0x06001B90 RID: 7056 RVA: 0x000A2B6C File Offset: 0x000A0D6C
	public UIInvGameItem(int id, UIInvBaseItem bi)
	{
		this.mBaseItemID = id;
		this.mBaseItem = bi;
	}

	// Token: 0x170003F6 RID: 1014
	// (get) Token: 0x06001B91 RID: 7057 RVA: 0x000A2B9C File Offset: 0x000A0D9C
	public int baseItemID
	{
		get
		{
			return this.mBaseItemID;
		}
	}

	// Token: 0x170003F7 RID: 1015
	// (get) Token: 0x06001B92 RID: 7058 RVA: 0x000A2BA4 File Offset: 0x000A0DA4
	public UIInvBaseItem baseItem
	{
		get
		{
			if (this.mBaseItem == null)
			{
				this.mBaseItem = UIInvDatabase.FindByID(this.baseItemID);
			}
			return this.mBaseItem;
		}
	}

	// Token: 0x170003F8 RID: 1016
	// (get) Token: 0x06001B93 RID: 7059 RVA: 0x000A2BD4 File Offset: 0x000A0DD4
	public string name
	{
		get
		{
			if (this.baseItem == null)
			{
				return null;
			}
			return this.quality.ToString() + " " + this.baseItem.name;
		}
	}

	// Token: 0x170003F9 RID: 1017
	// (get) Token: 0x06001B94 RID: 7060 RVA: 0x000A2C14 File Offset: 0x000A0E14
	public float statMultiplier
	{
		get
		{
			float num = 0f;
			switch (this.quality)
			{
			case UIInvGameItem.Quality.Broken:
				num = 0f;
				break;
			case UIInvGameItem.Quality.Cursed:
				num = -1f;
				break;
			case UIInvGameItem.Quality.Damaged:
				num = 0.25f;
				break;
			case UIInvGameItem.Quality.Worn:
				num = 0.9f;
				break;
			case UIInvGameItem.Quality.Sturdy:
				num = 1f;
				break;
			case UIInvGameItem.Quality.Polished:
				num = 1.1f;
				break;
			case UIInvGameItem.Quality.Improved:
				num = 1.25f;
				break;
			case UIInvGameItem.Quality.Crafted:
				num = 1.5f;
				break;
			case UIInvGameItem.Quality.Superior:
				num = 1.75f;
				break;
			case UIInvGameItem.Quality.Enchanted:
				num = 2f;
				break;
			case UIInvGameItem.Quality.Epic:
				num = 2.5f;
				break;
			case UIInvGameItem.Quality.Legendary:
				num = 3f;
				break;
			}
			float num2 = (float)this.itemLevel / 50f;
			return num * Mathf.Lerp(num2, num2 * num2, 0.5f);
		}
	}

	// Token: 0x170003FA RID: 1018
	// (get) Token: 0x06001B95 RID: 7061 RVA: 0x000A2D10 File Offset: 0x000A0F10
	public Color color
	{
		get
		{
			Color result = Color.white;
			switch (this.quality)
			{
			case UIInvGameItem.Quality.Broken:
				result = new Color(0.4f, 0.2f, 0.2f);
				break;
			case UIInvGameItem.Quality.Cursed:
				result = Color.red;
				break;
			case UIInvGameItem.Quality.Damaged:
				result = new Color(0.4f, 0.4f, 0.4f);
				break;
			case UIInvGameItem.Quality.Worn:
				result = new Color(0.7f, 0.7f, 0.7f);
				break;
			case UIInvGameItem.Quality.Sturdy:
				result = new Color(1f, 1f, 1f);
				break;
			case UIInvGameItem.Quality.Polished:
				result = NGUIMath.HexToColor(3774856959U);
				break;
			case UIInvGameItem.Quality.Improved:
				result = NGUIMath.HexToColor(2480359935U);
				break;
			case UIInvGameItem.Quality.Crafted:
				result = NGUIMath.HexToColor(1325334783U);
				break;
			case UIInvGameItem.Quality.Superior:
				result = NGUIMath.HexToColor(12255231U);
				break;
			case UIInvGameItem.Quality.Enchanted:
				result = NGUIMath.HexToColor(1937178111U);
				break;
			case UIInvGameItem.Quality.Epic:
				result = NGUIMath.HexToColor(2516647935U);
				break;
			case UIInvGameItem.Quality.Legendary:
				result = NGUIMath.HexToColor(4287627519U);
				break;
			}
			return result;
		}
	}

	// Token: 0x06001B96 RID: 7062 RVA: 0x000A2E50 File Offset: 0x000A1050
	public List<UIInvStat> CalculateStats()
	{
		List<UIInvStat> list = new List<UIInvStat>();
		if (this.baseItem != null)
		{
			float statMultiplier = this.statMultiplier;
			List<UIInvStat> stats = this.baseItem.stats;
			int i = 0;
			int count = stats.Count;
			while (i < count)
			{
				UIInvStat uiinvStat = stats[i];
				int num = Mathf.RoundToInt(statMultiplier * (float)uiinvStat.amount);
				if (num != 0)
				{
					bool flag = false;
					int j = 0;
					int count2 = list.Count;
					while (j < count2)
					{
						UIInvStat uiinvStat2 = list[j];
						if (uiinvStat2.id == uiinvStat.id && uiinvStat2.modifier == uiinvStat.modifier)
						{
							uiinvStat2.amount += num;
							flag = true;
							break;
						}
						j++;
					}
					if (!flag)
					{
						list.Add(new UIInvStat
						{
							id = uiinvStat.id,
							amount = num,
							modifier = uiinvStat.modifier
						});
					}
				}
				i++;
			}
			list.Sort(new Comparison<UIInvStat>(UIInvStat.CompareArmor));
		}
		return list;
	}

	// Token: 0x0400191D RID: 6429
	[SerializeField]
	private int mBaseItemID;

	// Token: 0x0400191E RID: 6430
	public UIInvGameItem.Quality quality = UIInvGameItem.Quality.Sturdy;

	// Token: 0x0400191F RID: 6431
	public int itemLevel = 1;

	// Token: 0x04001920 RID: 6432
	private UIInvBaseItem mBaseItem;

	// Token: 0x020003B0 RID: 944
	public enum Quality
	{
		// Token: 0x04001922 RID: 6434
		Broken,
		// Token: 0x04001923 RID: 6435
		Cursed,
		// Token: 0x04001924 RID: 6436
		Damaged,
		// Token: 0x04001925 RID: 6437
		Worn,
		// Token: 0x04001926 RID: 6438
		Sturdy,
		// Token: 0x04001927 RID: 6439
		Polished,
		// Token: 0x04001928 RID: 6440
		Improved,
		// Token: 0x04001929 RID: 6441
		Crafted,
		// Token: 0x0400192A RID: 6442
		Superior,
		// Token: 0x0400192B RID: 6443
		Enchanted,
		// Token: 0x0400192C RID: 6444
		Epic,
		// Token: 0x0400192D RID: 6445
		Legendary,
		// Token: 0x0400192E RID: 6446
		_LastDoNotUse
	}
}
