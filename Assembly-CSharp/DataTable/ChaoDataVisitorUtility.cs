using System;
using System.Collections.Generic;
using System.Linq;

namespace DataTable
{
	// Token: 0x0200017F RID: 383
	public class ChaoDataVisitorUtility
	{
		// Token: 0x06000B32 RID: 2866 RVA: 0x00041738 File Offset: 0x0003F938
		public static void SortKeyDictionaryInt(ref Dictionary<int, List<ChaoData>> dictionary, bool descending = false)
		{
			Dictionary<int, List<ChaoData>> dictionary2 = new Dictionary<int, List<ChaoData>>();
			IOrderedEnumerable<KeyValuePair<int, List<ChaoData>>> orderedEnumerable;
			if (descending)
			{
				orderedEnumerable = from x in dictionary
				orderby x.Key descending
				select x;
			}
			else
			{
				orderedEnumerable = from x in dictionary
				orderby x.Key
				select x;
			}
			foreach (KeyValuePair<int, List<ChaoData>> keyValuePair in orderedEnumerable)
			{
				dictionary2.Add(keyValuePair.Key, keyValuePair.Value);
			}
			dictionary = dictionary2;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00041804 File Offset: 0x0003FA04
		public static void AddListInt(ref List<ChaoData> list, Dictionary<int, List<ChaoData>> dictionary, bool raritySort = false, bool descending = false)
		{
			if (dictionary == null)
			{
				return;
			}
			if (dictionary.Count <= 0)
			{
				return;
			}
			Dictionary<int, List<ChaoData>>.KeyCollection keys = dictionary.Keys;
			if (raritySort)
			{
				foreach (int key in keys)
				{
					if (dictionary[key] != null && dictionary[key].Count > 0)
					{
						if (!descending)
						{
							dictionary[key].Sort((ChaoData chaoA, ChaoData chaoB) => chaoB.rarity - chaoA.rarity);
						}
						else
						{
							dictionary[key].Sort((ChaoData chaoA, ChaoData chaoB) => chaoA.rarity - chaoB.rarity);
						}
					}
				}
			}
			foreach (int key2 in keys)
			{
				if (list == null)
				{
					list = dictionary[key2];
				}
				else
				{
					list.AddRange(dictionary[key2]);
				}
			}
		}
	}
}
