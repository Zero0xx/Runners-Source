using System;
using System.Collections.Generic;
using Text;
using UnityEngine;

namespace DataTable
{
	// Token: 0x020001A5 RID: 421
	public class RankingLeagueTable : MonoBehaviour
	{
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x000452F0 File Offset: 0x000434F0
		public static RankingLeagueTable Instance
		{
			get
			{
				return RankingLeagueTable.s_instance;
			}
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x000452F8 File Offset: 0x000434F8
		private void Awake()
		{
			if (RankingLeagueTable.s_instance == null)
			{
				UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
				RankingLeagueTable.s_instance = this;
			}
			else
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0004532C File Offset: 0x0004352C
		private void OnDestroy()
		{
			if (RankingLeagueTable.s_instance == this)
			{
				RankingLeagueTable.s_instance = null;
			}
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00045344 File Offset: 0x00043544
		private void Setup()
		{
			base.enabled = false;
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00045350 File Offset: 0x00043550
		public static string GetItemText(List<ServerRemainOperator> leagueDataRemainOperator, string unitText = null, string unitTextMore = null, int head = 0, bool descendingOrder = false)
		{
			int num = 9876543;
			string text = string.Empty;
			List<RankingLeagueItem> list = new List<RankingLeagueItem>();
			List<ServerRemainOperator> list2 = new List<ServerRemainOperator>();
			Dictionary<int, List<ServerRemainOperator>> dictionary = new Dictionary<int, List<ServerRemainOperator>>();
			if (head < 0)
			{
				head = 0;
			}
			int num2 = 0;
			foreach (ServerRemainOperator serverRemainOperator in leagueDataRemainOperator)
			{
				int num3 = 0;
				if (serverRemainOperator.operatorData != 6)
				{
					int number = serverRemainOperator.number;
					if (serverRemainOperator.operatorData == 2)
					{
						serverRemainOperator.operatorData = 4;
						serverRemainOperator.number++;
					}
					else if (serverRemainOperator.operatorData == 3)
					{
						serverRemainOperator.operatorData = 5;
						serverRemainOperator.number--;
					}
					if (serverRemainOperator.number >= head)
					{
						list2.Add(serverRemainOperator);
					}
					num3++;
					if (num2 < number)
					{
						num2 = number;
					}
				}
				else if (!dictionary.ContainsKey(num3))
				{
					dictionary.Add(num3, new List<ServerRemainOperator>
					{
						serverRemainOperator
					});
				}
				else
				{
					dictionary[num3].Add(serverRemainOperator);
				}
			}
			if (list2.Count == 0)
			{
				bool flag = true;
				if (dictionary.Count > 0)
				{
					Dictionary<int, List<ServerRemainOperator>>.KeyCollection keys = dictionary.Keys;
					foreach (int key in keys)
					{
						List<ServerRemainOperator> list3 = dictionary[key];
						foreach (ServerRemainOperator serverRemainOperator2 in list3)
						{
							if (head % serverRemainOperator2.number == 0)
							{
								flag = false;
								ServerRemainOperator serverRemainOperator3 = new ServerRemainOperator();
								serverRemainOperator2.CopyTo(serverRemainOperator3);
								serverRemainOperator3.number = head;
								serverRemainOperator3.operatorData = 0;
								list2.Add(serverRemainOperator3);
								serverRemainOperator3 = leagueDataRemainOperator[leagueDataRemainOperator.Count - 1];
								serverRemainOperator3.number = head + 1;
								if (serverRemainOperator3.operatorData == 2)
								{
									serverRemainOperator3.operatorData = 4;
									serverRemainOperator3.number++;
								}
								list2.Add(serverRemainOperator3);
								break;
							}
						}
						if (!flag)
						{
							break;
						}
					}
				}
				if (flag)
				{
					ServerRemainOperator serverRemainOperator4 = leagueDataRemainOperator[leagueDataRemainOperator.Count - 1];
					serverRemainOperator4.number = head;
					if (serverRemainOperator4.operatorData == 2)
					{
						serverRemainOperator4.operatorData = 4;
						serverRemainOperator4.number++;
					}
					list2.Add(serverRemainOperator4);
				}
			}
			if (dictionary.Count > 0)
			{
				global::Debug.Log(string.Concat(new object[]
				{
					"+serverRemainOpeLoop:",
					dictionary.Count,
					"   serverRemainOpeLoop:",
					list2.Count
				}));
				Dictionary<int, List<ServerRemainOperator>>.KeyCollection keys2 = dictionary.Keys;
				foreach (int key2 in keys2)
				{
					List<ServerRemainOperator> list4 = dictionary[key2];
					ServerRemainOperator serverRemainOperator5 = null;
					List<bool> list5 = new List<bool>();
					if (leagueDataRemainOperator.Count > 0)
					{
						serverRemainOperator5 = leagueDataRemainOperator[leagueDataRemainOperator.Count - 1];
					}
					int num4 = 0;
					foreach (ServerRemainOperator serverRemainOperator6 in list4)
					{
						if (num4 < serverRemainOperator6.number)
						{
							num4 = serverRemainOperator6.number;
						}
						list5.Add(false);
					}
					int num5 = head - num2;
					if (num5 < 0)
					{
						num5 = 0;
					}
					for (int i = num2 + 1 + num5; i < num4 + (num2 + 1) + head; i++)
					{
						ServerRemainOperator serverRemainOperator7 = null;
						for (int j = 0; j < list4.Count; j++)
						{
							if (i % list4[j].number == 0)
							{
								serverRemainOperator7 = new ServerRemainOperator();
								list4[j].CopyTo(serverRemainOperator7);
								list5[j] = true;
								break;
							}
						}
						if (serverRemainOperator7 != null)
						{
							serverRemainOperator7.number = i;
							serverRemainOperator7.operatorData = 0;
							list2.Add(serverRemainOperator7);
							if (serverRemainOperator5 != null && serverRemainOperator5.operatorData == 4)
							{
								ServerRemainOperator serverRemainOperator8 = new ServerRemainOperator();
								serverRemainOperator5.CopyTo(serverRemainOperator8);
								serverRemainOperator8.number = i + 1;
								list2.Add(serverRemainOperator8);
							}
						}
					}
				}
				global::Debug.Log(string.Concat(new object[]
				{
					"-serverRemainOpeLoop:",
					dictionary.Count,
					"   serverRemainOpeLoop:",
					list2.Count
				}));
			}
			if (list2.Count > 0)
			{
				list2.Sort(new Comparison<ServerRemainOperator>(RankingLeagueTable.RemainDownComparer));
				foreach (ServerRemainOperator serverRemainOperator9 in list2)
				{
					RankingLeagueItem rankingLeagueItem = new RankingLeagueItem();
					rankingLeagueItem.ranking1 = serverRemainOperator9.number;
					rankingLeagueItem.ranking2 = serverRemainOperator9.number;
					rankingLeagueItem.operatorData = serverRemainOperator9.operatorData;
					if (serverRemainOperator9.ItemState != null && serverRemainOperator9.ItemState.Count > 0)
					{
						Dictionary<int, ServerItemState>.KeyCollection keys3 = serverRemainOperator9.ItemState.Keys;
						foreach (int num6 in keys3)
						{
							ServerItem serverItem = new ServerItem((ServerItem.Id)num6);
							ServerItemState serverItemState = new ServerItemState();
							serverItemState.m_itemId = (int)serverItem.id;
							serverItemState.m_num = serverRemainOperator9.ItemState[num6].m_num;
							rankingLeagueItem.item.Add(serverItemState);
						}
					}
					list.Add(rankingLeagueItem);
				}
				switch (list[0].operatorData)
				{
				case 2:
				case 4:
					list[0].ranking2 = num;
					break;
				}
				int num7 = list[0].ranking2 + 1;
				foreach (RankingLeagueItem rankingLeagueItem2 in list)
				{
					rankingLeagueItem2.ranking2 = num7 - 1;
					int operatorData = rankingLeagueItem2.operatorData;
					if (operatorData == 2)
					{
						rankingLeagueItem2.ranking1++;
					}
					num7 = rankingLeagueItem2.ranking1;
				}
				if (!descendingOrder)
				{
					list.Reverse(0, list.Count);
				}
			}
			if (list.Count > 0)
			{
				string text2;
				if (string.IsNullOrEmpty(unitText))
				{
					text2 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "rank_unit").text;
				}
				else
				{
					text2 = unitText;
				}
				for (int k = 0; k < list.Count; k++)
				{
					string text3 = text2;
					RankingLeagueItem rankingLeagueItem3 = list[k];
					string text4 = " ";
					int ranking = rankingLeagueItem3.ranking1;
					int ranking2 = rankingLeagueItem3.ranking2;
					if (ranking == ranking2)
					{
						string text5 = text3;
						text5 = TextUtility.Replaces(text5, new Dictionary<string, string>
						{
							{
								"{PARAM}",
								ranking.ToString()
							}
						});
						text4 += text5;
					}
					else
					{
						string text5 = ranking.ToString();
						string text6 = text3;
						if (ranking2 != num)
						{
							text6 = TextUtility.Replaces(text6, new Dictionary<string, string>
							{
								{
									"{PARAM}",
									ranking2.ToString()
								}
							});
							text4 = text4 + text5 + " - " + text6;
						}
						else if (!string.IsNullOrEmpty(unitTextMore))
						{
							text5 = TextUtility.Replaces(unitTextMore, new Dictionary<string, string>
							{
								{
									"{PARAM}",
									ranking.ToString()
								}
							});
							text4 += text5;
						}
						else
						{
							text6 = "   ";
							text4 = text4 + text5 + " - " + text6;
						}
					}
					text4 += " ";
					text3 = string.Empty;
					for (int l = 0; l < rankingLeagueItem3.item.Count; l++)
					{
						text3 += RankingLeagueTable.GetRankingHelpItem(new ServerItem((ServerItem.Id)rankingLeagueItem3.item[l].m_itemId), rankingLeagueItem3.item[l].m_num);
						if (l + 1 < rankingLeagueItem3.item.Count)
						{
							text3 += ",";
						}
					}
					if (!string.IsNullOrEmpty(text3))
					{
						if (k > 0)
						{
							text += "\n";
						}
						text = text + text4 + text3;
					}
				}
			}
			return text;
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x00045D24 File Offset: 0x00043F24
		private static string GetRankingHelpItem(ServerItem serverItem, int num)
		{
			ServerItem.IdType idType = serverItem.idType;
			if (idType == ServerItem.IdType.RSRING)
			{
				TextObject text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ranking_help_rs_ring");
				text.ReplaceTag("{PARAM}", HudUtility.GetFormatNumString<int>(num));
				return text.text;
			}
			if (idType != ServerItem.IdType.RING)
			{
				if (idType != ServerItem.IdType.CHARA)
				{
					if (idType != ServerItem.IdType.CHAO)
					{
						string text2 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "tutorial_sp_egg1_text").text;
						text2 = text2.Replace("{COUNT}", HudUtility.GetFormatNumString<int>(num));
						return serverItem.serverItemName + " " + text2;
					}
					int idIndex = serverItem.idIndex;
					ChaoData chaoData = ChaoTable.GetChaoData(idIndex);
					if (chaoData != null)
					{
						return chaoData.name;
					}
				}
				else
				{
					int idIndex2 = serverItem.idIndex;
					if ((ulong)idIndex2 < (ulong)((long)CharaName.Name.Length))
					{
						return TextUtility.GetCommonText("CharaName", CharaName.Name[idIndex2]);
					}
				}
				return string.Empty;
			}
			TextObject text3 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ranking_help_ring");
			text3.ReplaceTag("{PARAM}", HudUtility.GetFormatNumString<int>(num));
			return text3.text;
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00045E4C File Offset: 0x0004404C
		private static int RankUpComparer(RankingLeagueItem itemA, RankingLeagueItem itemB)
		{
			if (itemA != null && itemB != null)
			{
				if (itemA.ranking1 > itemB.ranking1)
				{
					return 1;
				}
				if (itemA.ranking1 < itemB.ranking1)
				{
					return -1;
				}
			}
			return 0;
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00045E84 File Offset: 0x00044084
		private static int RemainDownComparer(ServerRemainOperator itemA, ServerRemainOperator itemB)
		{
			if (itemA != null && itemB != null)
			{
				if (itemA.number > itemB.number)
				{
					return -1;
				}
				if (itemA.number < itemB.number)
				{
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00045EC4 File Offset: 0x000440C4
		private static int RemainUpComparer(ServerRemainOperator itemA, ServerRemainOperator itemB)
		{
			if (itemA != null && itemB != null)
			{
				if (itemA.number > itemB.number)
				{
					return 1;
				}
				if (itemA.number < itemB.number)
				{
					return -1;
				}
			}
			return 0;
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00045F04 File Offset: 0x00044104
		public static void SetupRankingLeagueTable()
		{
			RankingLeagueTable instance = RankingLeagueTable.Instance;
			if (instance == null)
			{
				GameObject gameObject = new GameObject("RankingLeagueTable");
				gameObject.AddComponent<RankingLeagueTable>();
				instance = RankingLeagueTable.Instance;
				if (instance != null)
				{
					instance.Setup();
				}
			}
			else
			{
				instance.Setup();
			}
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00045F58 File Offset: 0x00044158
		public static RankingLeagueTable GetRankingLeagueTable()
		{
			RankingLeagueTable instance = RankingLeagueTable.Instance;
			if (instance == null)
			{
				RankingLeagueTable.SetupRankingLeagueTable();
				instance = RankingLeagueTable.Instance;
			}
			return instance;
		}

		// Token: 0x04000995 RID: 2453
		private static RankingLeagueTable s_instance;
	}
}
