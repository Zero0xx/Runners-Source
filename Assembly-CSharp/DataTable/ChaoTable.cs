using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Text;
using UnityEngine;

namespace DataTable
{
	// Token: 0x0200017E RID: 382
	public class ChaoTable : MonoBehaviour
	{
		// Token: 0x06000B1D RID: 2845 RVA: 0x00040BB0 File Offset: 0x0003EDB0
		private void Start()
		{
			if (ChaoTable.s_chaoDataTable == null)
			{
				string s = AESCrypt.Decrypt(this.m_chaoTabel.text);
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(ChaoData[]));
				StringReader textReader = new StringReader(s);
				ChaoTable.s_chaoDataTable = (ChaoData[])xmlSerializer.Deserialize(textReader);
			}
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00040C00 File Offset: 0x0003EE00
		private void OnDestroy()
		{
			ChaoTable.s_chaoDataTable = null;
			if (ChaoTable.s_chaoDataTableMarker != null)
			{
				ChaoTable.s_chaoDataTableMarker.Clear();
				ChaoTable.s_chaoDataTableMarker = null;
			}
			if (ChaoTable.s_loadingChaoList != null)
			{
				ChaoTable.s_loadingChaoList.Clear();
				ChaoTable.s_loadingChaoList = null;
			}
			ChaoTable.s_setup = false;
			ChaoTable.s_loadingCount = 0;
			ChaoTable.s_eventList = false;
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00040C5C File Offset: 0x0003EE5C
		private static void Setup()
		{
			if (ChaoTable.s_setup)
			{
				return;
			}
			if (ChaoTable.s_chaoDataTable != null)
			{
				Array.Sort<ChaoData>(ChaoTable.s_chaoDataTable, new Comparison<ChaoData>(ChaoData.ChaoCompareById));
				ChaoTable.s_chaoDataTableMarker = new Dictionary<int, List<int>>();
				int item = 0;
				int num = -1;
				foreach (ChaoData chaoData in ChaoTable.s_chaoDataTable)
				{
					int num2 = chaoData.id / 1000 % 10;
					if (num2 != num)
					{
						if (!ChaoTable.s_chaoDataTableMarker.ContainsKey(num2))
						{
							ChaoTable.s_chaoDataTableMarker[num2] = new List<int>();
							ChaoTable.s_chaoDataTableMarker[num2].Add(item);
						}
					}
					else if (chaoData.id % 10 == 0 && ChaoTable.s_chaoDataTableMarker.ContainsKey(num2))
					{
						ChaoTable.s_chaoDataTableMarker[num2].Add(item);
					}
					chaoData.index = item++;
					chaoData.name = TextUtility.GetChaoText("Chao", "name" + chaoData.id.ToString("D4"));
					chaoData.nameTwolines = TextUtility.GetChaoText("Chao", "name_for_menu_" + chaoData.id.ToString("D4"));
					chaoData.StatusUpdate();
					num = num2;
				}
				ChaoTable.s_setup = true;
			}
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00040DBC File Offset: 0x0003EFBC
		public static int ChaoMaxLevel()
		{
			return 10;
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00040DC0 File Offset: 0x0003EFC0
		public static bool ChangeChaoAbility(int chaoId, int abilityEventId)
		{
			bool result = false;
			if (ChaoTable.s_chaoDataTable != null)
			{
				foreach (ChaoData chaoData in ChaoTable.s_chaoDataTable)
				{
					if (chaoData.id == chaoId)
					{
						result = chaoData.SetChaoAbility(abilityEventId);
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00040E14 File Offset: 0x0003F014
		public static bool ChangeChaoAbilityIndex(int chaoId, int abilityIndex)
		{
			bool result = false;
			if (ChaoTable.s_chaoDataTable != null)
			{
				foreach (ChaoData chaoData in ChaoTable.s_chaoDataTable)
				{
					if (chaoData.id == chaoId)
					{
						result = chaoData.SetChaoAbilityIndex(abilityIndex);
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00040E68 File Offset: 0x0003F068
		public static bool ChangeChaoAbilityNext(int chaoId)
		{
			bool result = false;
			if (ChaoTable.s_chaoDataTable != null)
			{
				foreach (ChaoData chaoData in ChaoTable.s_chaoDataTable)
				{
					if (chaoData.id == chaoId)
					{
						if (chaoData.abilityNum - 1 > chaoData.currentAbility)
						{
							result = chaoData.SetChaoAbilityIndex(chaoData.currentAbility + 1);
						}
						else
						{
							result = chaoData.SetChaoAbilityIndex(0);
						}
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00040EE0 File Offset: 0x0003F0E0
		public static ChaoData[] GetDataTable()
		{
			ChaoTable.Setup();
			return ChaoTable.s_chaoDataTable;
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00040EEC File Offset: 0x0003F0EC
		public static List<ChaoData> GetDataTable(ChaoData.Rarity rarity)
		{
			List<ChaoData> result = null;
			if (rarity != ChaoData.Rarity.NONE)
			{
				ChaoTable.Setup();
				ChaoDataVisitorBase chaoDataVisitorBase = new ChaoDataVisitorRarity();
				if (ChaoTable.s_chaoDataTable != null)
				{
					foreach (ChaoData chaoData in ChaoTable.s_chaoDataTable)
					{
						chaoData.accept(ref chaoDataVisitorBase);
					}
				}
				ChaoDataVisitorRarity chaoDataVisitorRarity = (ChaoDataVisitorRarity)chaoDataVisitorBase;
				result = chaoDataVisitorRarity.GetChaoList(rarity, ChaoData.Rarity.NONE);
			}
			return result;
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00040F58 File Offset: 0x0003F158
		public static List<ChaoData> GetPossessionChaoData()
		{
			List<ChaoData> list = null;
			if (ChaoTable.s_chaoDataTable == null)
			{
				ChaoTable.Setup();
			}
			if (ChaoTable.s_chaoDataTable != null && ChaoTable.s_chaoDataTable.Length > 0)
			{
				int num = ChaoTable.s_chaoDataTable.Length;
				for (int i = 0; i < num; i++)
				{
					ChaoData chaoData = ChaoTable.s_chaoDataTable[i];
					if (chaoData != null && chaoData.level >= 0)
					{
						if (list == null)
						{
							list = new List<ChaoData>();
						}
						list.Add(chaoData);
					}
				}
			}
			return list;
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00040FD8 File Offset: 0x0003F1D8
		public static List<ChaoData> GetChaoData(List<int> ids)
		{
			List<ChaoData> list = null;
			if (ids != null && ids.Count > 0)
			{
				ChaoTable.Setup();
				int count = ids.Count;
				for (int i = 0; i < count; i++)
				{
					int id = ids[i];
					ChaoData chaoData = ChaoTable.GetChaoData(id);
					if (chaoData != null)
					{
						if (list == null)
						{
							list = new List<ChaoData>();
						}
						list.Add(chaoData);
					}
				}
			}
			return list;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00041044 File Offset: 0x0003F244
		public static ChaoData GetChaoData(int id)
		{
			ChaoData result = null;
			ChaoTable.Setup();
			if (id >= 0 && ChaoTable.s_chaoDataTable != null && ChaoTable.s_chaoDataTableMarker != null)
			{
				int num = ChaoTable.s_chaoDataTable.Length;
				bool flag = false;
				int num2 = 0;
				int num3 = id / 1000 % 10;
				if (ChaoTable.s_chaoDataTableMarker.ContainsKey(num3))
				{
					int num4 = id % 10;
					if (num4 >= 5)
					{
						flag = true;
					}
					int num5 = id / 10 % 100;
					if (ChaoTable.s_chaoDataTableMarker[num3].Count > num5)
					{
						if (flag && ChaoTable.s_chaoDataTableMarker[num3].Count - 1 == num5)
						{
							if (!ChaoTable.s_chaoDataTableMarker.ContainsKey(num3 + 1))
							{
								num2 = num - 1;
							}
							else
							{
								num2 = ChaoTable.s_chaoDataTableMarker[num3 + 1][0] - 1;
							}
						}
						else if (flag)
						{
							num2 = ChaoTable.s_chaoDataTableMarker[num3][num5 + 1] - 1;
						}
						else
						{
							num2 = ChaoTable.s_chaoDataTableMarker[num3][num5];
						}
					}
					if (num2 < 0)
					{
						num2 = 0;
					}
				}
				for (int i = 0; i < num; i++)
				{
					int num6;
					if (!flag)
					{
						num6 = (num2 + i) % num;
					}
					else
					{
						num6 = (num2 - i + num) % num;
					}
					if (ChaoTable.s_chaoDataTable[num6].id == id)
					{
						result = ChaoTable.s_chaoDataTable[num6];
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x000411C0 File Offset: 0x0003F3C0
		private static void ResetLoadingChao()
		{
			ChaoTable.s_loadingChaoList = null;
			ChaoTable.s_loadingCount = 0;
			ChaoTable.s_eventList = false;
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x000411D4 File Offset: 0x0003F3D4
		public static void CheckEventTime()
		{
			if (ChaoTable.s_eventList)
			{
				if (EventManager.Instance != null && !EventManager.Instance.IsInEvent())
				{
					ChaoTable.ResetLoadingChao();
				}
			}
			else if (EventManager.Instance != null && EventManager.Instance.IsInEvent())
			{
				ChaoTable.ResetLoadingChao();
			}
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00041238 File Offset: 0x0003F438
		public static List<ChaoData> GetEyeCatcherChaoData(List<ServerChaoState> serverChaoList)
		{
			if (serverChaoList != null)
			{
				List<int> list = new List<int>();
				EyeCatcherChaoData[] eyeCatcherChaoDatas = EventManager.Instance.GetEyeCatcherChaoDatas();
				if (eyeCatcherChaoDatas != null)
				{
					foreach (EyeCatcherChaoData eyeCatcherChaoData in eyeCatcherChaoDatas)
					{
						foreach (ServerChaoState serverChaoState in serverChaoList)
						{
							int num = eyeCatcherChaoData.chao_id + 400000;
							if (num == serverChaoState.Id)
							{
								list.Add(eyeCatcherChaoData.chao_id);
								break;
							}
						}
					}
				}
				RewardChaoData rewardChaoData = EventManager.Instance.GetRewardChaoData();
				if (rewardChaoData != null)
				{
					foreach (ServerChaoState serverChaoState2 in serverChaoList)
					{
						int num2 = rewardChaoData.chao_id + 400000;
						if (num2 == serverChaoState2.Id)
						{
							list.Add(rewardChaoData.chao_id);
							break;
						}
					}
				}
				if (list.Count > 0)
				{
					return ChaoTable.GetChaoData(list);
				}
			}
			return null;
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x000413A0 File Offset: 0x0003F5A0
		public static ChaoData GetLoadingChao()
		{
			ChaoTable.Setup();
			if (ServerInterface.LoggedInServerInterface == null && ChaoTable.s_chaoDataTable != null)
			{
				foreach (ChaoData chaoData in ChaoTable.s_chaoDataTable)
				{
					if (chaoData.id == 0)
					{
						return chaoData;
					}
				}
				return null;
			}
			ChaoTable.CheckEventTime();
			if (ChaoTable.s_loadingChaoList == null && ChaoTable.s_chaoDataTable != null)
			{
				List<ChaoData> list = null;
				List<ServerChaoState> list2 = null;
				ServerPlayerState playerState = ServerInterface.PlayerState;
				if (playerState != null && playerState.ChaoStates != null)
				{
					list2 = playerState.ChaoStates;
				}
				if (EventManager.Instance != null && EventManager.Instance.IsInEvent())
				{
					ChaoDataVisitorBase chaoDataVisitorBase = new ChaoDataVisitorEvent();
					foreach (ChaoData chaoData2 in ChaoTable.s_chaoDataTable)
					{
						if (list2 != null)
						{
							foreach (ServerChaoState serverChaoState in list2)
							{
								int num = chaoData2.id + 400000;
								if (serverChaoState.Id == num)
								{
									chaoData2.accept(ref chaoDataVisitorBase);
									break;
								}
							}
						}
						else
						{
							chaoData2.accept(ref chaoDataVisitorBase);
						}
					}
					ChaoDataVisitorEvent chaoDataVisitorEvent = (ChaoDataVisitorEvent)chaoDataVisitorBase;
					if (chaoDataVisitorEvent != null)
					{
						list = chaoDataVisitorEvent.GetChaoList(EventManager.GetSpecificId());
						if (list == null)
						{
							list = ChaoTable.GetEyeCatcherChaoData(list2);
						}
					}
					ChaoTable.s_eventList = true;
				}
				if (list == null)
				{
					ChaoDataVisitorBase chaoDataVisitorBase2 = new ChaoDataVisitorRarity();
					foreach (ChaoData chaoData3 in ChaoTable.s_chaoDataTable)
					{
						if (list2 != null)
						{
							foreach (ServerChaoState serverChaoState2 in list2)
							{
								int num2 = chaoData3.id + 400000;
								if (serverChaoState2.Id == num2)
								{
									chaoData3.accept(ref chaoDataVisitorBase2);
									break;
								}
							}
						}
						else
						{
							chaoData3.accept(ref chaoDataVisitorBase2);
						}
					}
					ChaoDataVisitorRarity chaoDataVisitorRarity = (ChaoDataVisitorRarity)chaoDataVisitorBase2;
					if (chaoDataVisitorRarity != null)
					{
						list = chaoDataVisitorRarity.GetChaoList(ChaoData.Rarity.SRARE, ChaoData.Rarity.RARE);
					}
				}
				if (list != null)
				{
					ChaoTable.s_loadingChaoList = (from i in list
					orderby Guid.NewGuid()
					select i).ToList<ChaoData>();
				}
			}
			if (ChaoTable.s_loadingChaoList == null || ChaoTable.s_loadingChaoList.Count <= 0)
			{
				return null;
			}
			int index = ChaoTable.s_loadingCount % ChaoTable.s_loadingChaoList.Count;
			ChaoTable.s_loadingCount++;
			return ChaoTable.s_loadingChaoList[index];
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x000416A0 File Offset: 0x0003F8A0
		public static ChaoData GetChaoDataOfIndex(int index)
		{
			ChaoData[] dataTable = ChaoTable.GetDataTable();
			return (dataTable == null || (ulong)index >= (ulong)((long)dataTable.Length)) ? null : dataTable[index];
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x000416D0 File Offset: 0x0003F8D0
		public static int GetChaoIdOfIndex(int index)
		{
			ChaoData chaoDataOfIndex = ChaoTable.GetChaoDataOfIndex(index);
			return (chaoDataOfIndex == null) ? -1 : chaoDataOfIndex.id;
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x000416F8 File Offset: 0x0003F8F8
		public static int GetRandomChaoId()
		{
			ChaoData[] dataTable = ChaoTable.GetDataTable();
			return (dataTable == null) ? -1 : dataTable[UnityEngine.Random.Range(0, dataTable.Length)].id;
		}

		// Token: 0x040008BE RID: 2238
		private const int MARKER_INTERVAL = 10;

		// Token: 0x040008BF RID: 2239
		private const int LEVEL_MAX = 10;

		// Token: 0x040008C0 RID: 2240
		[SerializeField]
		private TextAsset m_chaoTabel;

		// Token: 0x040008C1 RID: 2241
		private static ChaoData[] s_chaoDataTable;

		// Token: 0x040008C2 RID: 2242
		private static Dictionary<int, List<int>> s_chaoDataTableMarker;

		// Token: 0x040008C3 RID: 2243
		private static bool s_setup;

		// Token: 0x040008C4 RID: 2244
		private static bool s_eventList;

		// Token: 0x040008C5 RID: 2245
		private static int s_loadingCount;

		// Token: 0x040008C6 RID: 2246
		private static List<ChaoData> s_loadingChaoList;
	}
}
