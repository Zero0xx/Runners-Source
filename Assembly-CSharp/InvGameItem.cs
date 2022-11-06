using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000058 RID: 88
[Serializable]
public class InvGameItem
{
	// Token: 0x060002B8 RID: 696 RVA: 0x0000BD44 File Offset: 0x00009F44
	public InvGameItem(int id)
	{
		this.mBaseItemID = id;
	}

	// Token: 0x060002B9 RID: 697 RVA: 0x0000BD64 File Offset: 0x00009F64
	public InvGameItem(int id, InvBaseItem bi)
	{
		this.mBaseItemID = id;
		this.mBaseItem = bi;
	}

	// Token: 0x1700006B RID: 107
	// (get) Token: 0x060002BA RID: 698 RVA: 0x0000BD94 File Offset: 0x00009F94
	public int baseItemID
	{
		get
		{
			return this.mBaseItemID;
		}
	}

	// Token: 0x1700006C RID: 108
	// (get) Token: 0x060002BB RID: 699 RVA: 0x0000BD9C File Offset: 0x00009F9C
	public InvBaseItem baseItem
	{
		get
		{
			if (this.mBaseItem == null)
			{
				this.mBaseItem = InvDatabase.FindByID(this.baseItemID);
			}
			return this.mBaseItem;
		}
	}

	// Token: 0x1700006D RID: 109
	// (get) Token: 0x060002BC RID: 700 RVA: 0x0000BDCC File Offset: 0x00009FCC
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

	// Token: 0x1700006E RID: 110
	// (get) Token: 0x060002BD RID: 701 RVA: 0x0000BE0C File Offset: 0x0000A00C
	public float statMultiplier
	{
		get
		{
			float num = 0f;
			switch (this.quality)
			{
			case InvGameItem.Quality.Broken:
				num = 0f;
				break;
			case InvGameItem.Quality.Cursed:
				num = -1f;
				break;
			case InvGameItem.Quality.Damaged:
				num = 0.25f;
				break;
			case InvGameItem.Quality.Worn:
				num = 0.9f;
				break;
			case InvGameItem.Quality.Sturdy:
				num = 1f;
				break;
			case InvGameItem.Quality.Polished:
				num = 1.1f;
				break;
			case InvGameItem.Quality.Improved:
				num = 1.25f;
				break;
			case InvGameItem.Quality.Crafted:
				num = 1.5f;
				break;
			case InvGameItem.Quality.Superior:
				num = 1.75f;
				break;
			case InvGameItem.Quality.Enchanted:
				num = 2f;
				break;
			case InvGameItem.Quality.Epic:
				num = 2.5f;
				break;
			case InvGameItem.Quality.Legendary:
				num = 3f;
				break;
			}
			float num2 = (float)this.itemLevel / 50f;
			return num * Mathf.Lerp(num2, num2 * num2, 0.5f);
		}
	}

	// Token: 0x1700006F RID: 111
	// (get) Token: 0x060002BE RID: 702 RVA: 0x0000BF08 File Offset: 0x0000A108
	public Color color
	{
		get
		{
			Color result = Color.white;
			switch (this.quality)
			{
			case InvGameItem.Quality.Broken:
				result = new Color(0.4f, 0.2f, 0.2f);
				break;
			case InvGameItem.Quality.Cursed:
				result = Color.red;
				break;
			case InvGameItem.Quality.Damaged:
				result = new Color(0.4f, 0.4f, 0.4f);
				break;
			case InvGameItem.Quality.Worn:
				result = new Color(0.7f, 0.7f, 0.7f);
				break;
			case InvGameItem.Quality.Sturdy:
				result = new Color(1f, 1f, 1f);
				break;
			case InvGameItem.Quality.Polished:
				result = NGUIMath.HexToColor(3774856959U);
				break;
			case InvGameItem.Quality.Improved:
				result = NGUIMath.HexToColor(2480359935U);
				break;
			case InvGameItem.Quality.Crafted:
				result = NGUIMath.HexToColor(1325334783U);
				break;
			case InvGameItem.Quality.Superior:
				result = NGUIMath.HexToColor(12255231U);
				break;
			case InvGameItem.Quality.Enchanted:
				result = NGUIMath.HexToColor(1937178111U);
				break;
			case InvGameItem.Quality.Epic:
				result = NGUIMath.HexToColor(2516647935U);
				break;
			case InvGameItem.Quality.Legendary:
				result = NGUIMath.HexToColor(4287627519U);
				break;
			}
			return result;
		}
	}

	// Token: 0x060002BF RID: 703 RVA: 0x0000C048 File Offset: 0x0000A248
	public List<InvStat> CalculateStats()
	{
		List<InvStat> list = new List<InvStat>();
		if (this.baseItem != null)
		{
			float statMultiplier = this.statMultiplier;
			List<InvStat> stats = this.baseItem.stats;
			int i = 0;
			int count = stats.Count;
			while (i < count)
			{
				InvStat invStat = stats[i];
				int num = Mathf.RoundToInt(statMultiplier * (float)invStat.amount);
				if (num != 0)
				{
					bool flag = false;
					int j = 0;
					int count2 = list.Count;
					while (j < count2)
					{
						InvStat invStat2 = list[j];
						if (invStat2.id == invStat.id && invStat2.modifier == invStat.modifier)
						{
							invStat2.amount += num;
							flag = true;
							break;
						}
						j++;
					}
					if (!flag)
					{
						list.Add(new InvStat
						{
							id = invStat.id,
							amount = num,
							modifier = invStat.modifier
						});
					}
				}
				i++;
			}
			list.Sort(new Comparison<InvStat>(InvStat.CompareArmor));
		}
		return list;
	}

	// Token: 0x04000155 RID: 341
	[SerializeField]
	private int mBaseItemID;

	// Token: 0x04000156 RID: 342
	public InvGameItem.Quality quality = InvGameItem.Quality.Sturdy;

	// Token: 0x04000157 RID: 343
	public int itemLevel = 1;

	// Token: 0x04000158 RID: 344
	private InvBaseItem mBaseItem;

	// Token: 0x02000059 RID: 89
	public enum Quality
	{
		// Token: 0x0400015A RID: 346
		Broken,
		// Token: 0x0400015B RID: 347
		Cursed,
		// Token: 0x0400015C RID: 348
		Damaged,
		// Token: 0x0400015D RID: 349
		Worn,
		// Token: 0x0400015E RID: 350
		Sturdy,
		// Token: 0x0400015F RID: 351
		Polished,
		// Token: 0x04000160 RID: 352
		Improved,
		// Token: 0x04000161 RID: 353
		Crafted,
		// Token: 0x04000162 RID: 354
		Superior,
		// Token: 0x04000163 RID: 355
		Enchanted,
		// Token: 0x04000164 RID: 356
		Epic,
		// Token: 0x04000165 RID: 357
		Legendary,
		// Token: 0x04000166 RID: 358
		_LastDoNotUse
	}
}
