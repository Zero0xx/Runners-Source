using System;
using System.Collections.Generic;
using DataTable;
using Text;

// Token: 0x02000822 RID: 2082
public class ServerSpinResultGeneral
{
	// Token: 0x060037D4 RID: 14292 RVA: 0x00126900 File Offset: 0x00124B00
	public ServerSpinResultGeneral()
	{
		this.m_acquiredChaoData = new Dictionary<int, List<ServerChaoData>>();
		this.m_acquiredChaoCount = new Dictionary<int, int>();
		this.IsRequiredChao = new Dictionary<int, bool>();
		this.m_requiredSpEggs = 0;
		this.m_itemState = new Dictionary<int, List<ServerItemState>>();
		this.ItemWon = 0;
		this.m_getAlreadyOverlap = new Dictionary<int, int>();
		this.m_getOtomoAndCharaMax = -1;
	}

	// Token: 0x060037D5 RID: 14293 RVA: 0x00126968 File Offset: 0x00124B68
	public ServerSpinResultGeneral(ServerChaoSpinResult result)
	{
		this.m_acquiredChaoData = new Dictionary<int, List<ServerChaoData>>();
		this.m_acquiredChaoCount = new Dictionary<int, int>();
		this.IsRequiredChao = new Dictionary<int, bool>();
		this.m_requiredSpEggs = 0;
		this.m_itemState = new Dictionary<int, List<ServerItemState>>();
		this.ItemWon = 0;
		this.m_getAlreadyOverlap = new Dictionary<int, int>();
		this.m_getOtomoAndCharaMax = 0;
		if (result.AcquiredChaoData != null)
		{
			this.AddChaoState(result.AcquiredChaoData);
			if (result.ItemState != null && result.ItemState.Count > 0)
			{
				Dictionary<int, ServerItemState>.KeyCollection keys = result.ItemState.Keys;
				foreach (int key in keys)
				{
					this.AddItemState(new ServerItemState
					{
						m_itemId = result.ItemState[key].m_itemId,
						m_num = result.ItemState[key].m_num
					});
				}
			}
			this.m_getOtomoAndCharaMax = 1;
		}
		this.ItemWon = result.ItemWon;
	}

	// Token: 0x060037D6 RID: 14294 RVA: 0x00126AA8 File Offset: 0x00124CA8
	public ServerSpinResultGeneral(ServerWheelOptions newOptions, ServerWheelOptions oldOptions)
	{
		this.m_acquiredChaoData = new Dictionary<int, List<ServerChaoData>>();
		this.m_acquiredChaoCount = new Dictionary<int, int>();
		this.IsRequiredChao = new Dictionary<int, bool>();
		this.m_requiredSpEggs = 0;
		this.m_itemState = new Dictionary<int, List<ServerItemState>>();
		this.ItemWon = oldOptions.m_itemWon;
		this.m_getAlreadyOverlap = new Dictionary<int, int>();
		this.m_getOtomoAndCharaMax = -1;
		ServerChaoData chao = oldOptions.GetChao();
		if (chao != null)
		{
			this.AddChaoState(chao);
			if (newOptions.IsItemList())
			{
				Dictionary<int, ServerItemState>.KeyCollection keys = newOptions.m_itemList.Keys;
				foreach (int key in keys)
				{
					this.AddItemState(newOptions.m_itemList[key]);
				}
			}
			this.m_getOtomoAndCharaMax = 1;
		}
		else
		{
			ServerItemState item = oldOptions.GetItem();
			if (item != null)
			{
				this.AddItemState(oldOptions.GetItem());
			}
			this.m_getOtomoAndCharaMax = 0;
		}
	}

	// Token: 0x17000842 RID: 2114
	// (get) Token: 0x060037D7 RID: 14295 RVA: 0x00126BCC File Offset: 0x00124DCC
	public int NumRequiredSpEggs
	{
		get
		{
			return this.m_requiredSpEggs;
		}
	}

	// Token: 0x17000843 RID: 2115
	// (get) Token: 0x060037D8 RID: 14296 RVA: 0x00126BD4 File Offset: 0x00124DD4
	public bool IsRequiredSpEgg
	{
		get
		{
			return 0 < this.m_requiredSpEggs;
		}
	}

	// Token: 0x17000844 RID: 2116
	// (get) Token: 0x060037D9 RID: 14297 RVA: 0x00126BE0 File Offset: 0x00124DE0
	public Dictionary<int, ServerItemState> ItemState
	{
		get
		{
			Dictionary<int, ServerItemState> dictionary = new Dictionary<int, ServerItemState>();
			if (this.m_itemState != null && this.m_itemState.Count > 0)
			{
				Dictionary<int, List<ServerItemState>>.KeyCollection keys = this.m_itemState.Keys;
				foreach (int key in keys)
				{
					List<ServerItemState> list = this.m_itemState[key];
					if (list != null && list.Count > 0)
					{
						ServerItemState serverItemState = new ServerItemState();
						list[0].CopyTo(serverItemState);
						int num = 0;
						foreach (ServerItemState serverItemState2 in list)
						{
							if (serverItemState2 != null)
							{
								num += serverItemState2.m_num;
							}
						}
						serverItemState.m_num = num;
						dictionary.Add(key, serverItemState);
					}
				}
			}
			return dictionary;
		}
	}

	// Token: 0x17000845 RID: 2117
	// (get) Token: 0x060037DA RID: 14298 RVA: 0x00126D18 File Offset: 0x00124F18
	public Dictionary<int, ServerChaoData> AcquiredChaoData
	{
		get
		{
			Dictionary<int, ServerChaoData> dictionary = new Dictionary<int, ServerChaoData>();
			if (this.m_acquiredChaoData != null && this.m_acquiredChaoData.Count > 0)
			{
				Dictionary<int, List<ServerChaoData>>.KeyCollection keys = this.m_acquiredChaoData.Keys;
				foreach (int key in keys)
				{
					List<ServerChaoData> list = this.m_acquiredChaoData[key];
					if (list != null && list.Count > 0)
					{
						dictionary.Add(key, list[list.Count - 1]);
					}
				}
			}
			return dictionary;
		}
	}

	// Token: 0x17000846 RID: 2118
	// (get) Token: 0x060037DB RID: 14299 RVA: 0x00126DDC File Offset: 0x00124FDC
	// (set) Token: 0x060037DC RID: 14300 RVA: 0x00126DE4 File Offset: 0x00124FE4
	public Dictionary<int, bool> IsRequiredChao { get; private set; }

	// Token: 0x17000847 RID: 2119
	// (get) Token: 0x060037DD RID: 14301 RVA: 0x00126DF0 File Offset: 0x00124FF0
	// (set) Token: 0x060037DE RID: 14302 RVA: 0x00126DF8 File Offset: 0x00124FF8
	public int ItemWon { get; set; }

	// Token: 0x17000848 RID: 2120
	// (get) Token: 0x060037DF RID: 14303 RVA: 0x00126E04 File Offset: 0x00125004
	// (set) Token: 0x060037E0 RID: 14304 RVA: 0x00126E0C File Offset: 0x0012500C
	private Dictionary<int, int> m_getAlreadyOverlap { get; set; }

	// Token: 0x17000849 RID: 2121
	// (get) Token: 0x060037E1 RID: 14305 RVA: 0x00126E18 File Offset: 0x00125018
	public bool IsMulti
	{
		get
		{
			return this.ItemWon == -1;
		}
	}

	// Token: 0x1700084A RID: 2122
	// (get) Token: 0x060037E2 RID: 14306 RVA: 0x00126E24 File Offset: 0x00125024
	public string AcquiredListText
	{
		get
		{
			return this.GetAcquiredListText();
		}
	}

	// Token: 0x1700084B RID: 2123
	// (get) Token: 0x060037E3 RID: 14307 RVA: 0x00126E2C File Offset: 0x0012502C
	// (set) Token: 0x060037E4 RID: 14308 RVA: 0x00126E34 File Offset: 0x00125034
	private Dictionary<int, int> m_acquiredChaoCount { get; set; }

	// Token: 0x1700084C RID: 2124
	// (get) Token: 0x060037E5 RID: 14309 RVA: 0x00126E40 File Offset: 0x00125040
	public bool IsRankup
	{
		get
		{
			bool result = false;
			Dictionary<int, List<ServerItemState>>.KeyCollection keys = this.m_itemState.Keys;
			foreach (int id in keys)
			{
				ServerItem serverItem = new ServerItem((ServerItem.Id)id);
				if (serverItem.idType == ServerItem.IdType.ITEM_ROULLETE_WIN)
				{
					result = true;
					break;
				}
			}
			return result;
		}
	}

	// Token: 0x060037E6 RID: 14310 RVA: 0x00126EC8 File Offset: 0x001250C8
	private string GetAcquiredListText()
	{
		string text = null;
		if (this.IsMulti)
		{
			List<int> list = null;
			if (this.m_acquiredChaoData != null && this.m_acquiredChaoData.Count > 0)
			{
				list = new List<int>();
				Dictionary<int, List<ServerChaoData>>.KeyCollection keys = this.m_acquiredChaoData.Keys;
				foreach (int num in keys)
				{
					foreach (ServerChaoData serverChaoData in this.m_acquiredChaoData[num])
					{
						if (serverChaoData.Rarity >= 100)
						{
							list.Add(num);
						}
					}
				}
				foreach (int num2 in keys)
				{
					foreach (ServerChaoData serverChaoData2 in this.m_acquiredChaoData[num2])
					{
						if (serverChaoData2.Rarity < 100)
						{
							list.Add(num2);
						}
					}
				}
			}
			if (list != null)
			{
				Dictionary<int, int> dictionary = new Dictionary<int, int>();
				foreach (int num3 in list)
				{
					ServerItem serverItem = new ServerItem((ServerItem.Id)num3);
					string serverItemName = serverItem.serverItemName;
					if (string.IsNullOrEmpty(text))
					{
						text = serverItemName;
					}
					else
					{
						text = text + "\n" + serverItemName;
					}
					if (this.m_getAlreadyOverlap != null && this.m_getAlreadyOverlap.ContainsKey(num3) && this.m_getAlreadyOverlap[num3] > 0)
					{
						if (dictionary.ContainsKey(num3))
						{
							Dictionary<int, int> dictionary3;
							Dictionary<int, int> dictionary2 = dictionary3 = dictionary;
							int num4;
							int key = num4 = num3;
							num4 = dictionary3[num4];
							dictionary2[key] = num4 + 1;
						}
						else
						{
							dictionary.Add(num3, 1);
						}
						int num5 = this.m_acquiredChaoCount[num3] - this.m_getAlreadyOverlap[num3];
						if (num5 < dictionary[num3])
						{
							text = text + " " + TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "GeneralWindow", "get_item_overlap").text;
						}
					}
				}
			}
			if (this.m_itemState != null && this.m_itemState.Count > 0)
			{
				Dictionary<int, List<ServerItemState>>.KeyCollection keys2 = this.m_itemState.Keys;
				foreach (int num6 in keys2)
				{
					ServerItem serverItem2 = new ServerItem((ServerItem.Id)num6);
					string serverItemName2 = serverItem2.serverItemName;
					foreach (ServerItemState serverItemState in this.m_itemState[num6])
					{
						if (string.IsNullOrEmpty(text))
						{
							text = serverItemName2;
						}
						else
						{
							text = text + "\n" + serverItemName2;
						}
						text = text + " × " + serverItemState.m_num;
					}
				}
			}
		}
		return text;
	}

	// Token: 0x060037E7 RID: 14311 RVA: 0x001272E8 File Offset: 0x001254E8
	public void AddItemState(ServerItemState itemState)
	{
		if (this.m_itemState.ContainsKey(itemState.m_itemId))
		{
			this.m_itemState[itemState.m_itemId].Add(itemState);
		}
		else
		{
			List<ServerItemState> list = new List<ServerItemState>();
			list.Add(itemState);
			this.m_itemState.Add(itemState.m_itemId, list);
		}
		if (itemState.m_itemId == 220000)
		{
			this.m_requiredSpEggs += itemState.m_num;
		}
	}

	// Token: 0x060037E8 RID: 14312 RVA: 0x0012736C File Offset: 0x0012556C
	public void AddChaoState(ServerChaoData chaoState)
	{
		if (this.m_acquiredChaoData.ContainsKey(chaoState.Id))
		{
			this.m_acquiredChaoData[chaoState.Id].Add(chaoState);
		}
		else
		{
			List<ServerChaoData> list = new List<ServerChaoData>();
			list.Add(chaoState);
			this.m_acquiredChaoData.Add(chaoState.Id, list);
		}
		bool flag = false;
		int num = -1;
		ServerPlayerState playerState = ServerInterface.PlayerState;
		if (playerState != null)
		{
			if (chaoState.Rarity < 100)
			{
				ServerChaoState serverChaoState = playerState.ChaoStateByItemID(chaoState.Id);
				if (serverChaoState != null)
				{
					flag = (serverChaoState.Status != ServerChaoState.ChaoStatus.MaxLevel);
					if (serverChaoState.IsOwned)
					{
						num = serverChaoState.Level;
					}
					else
					{
						num = -1;
					}
				}
			}
			else
			{
				ServerCharacterState serverCharacterState = playerState.CharacterStateByItemID(chaoState.Id);
				if (serverCharacterState == null)
				{
					flag = true;
				}
				else if (serverCharacterState.Id < 0 || !serverCharacterState.IsUnlocked || serverCharacterState.star < serverCharacterState.starMax)
				{
					flag = true;
					num = serverCharacterState.star;
				}
			}
		}
		if (this.IsRequiredChao.ContainsKey(chaoState.Id))
		{
			this.IsRequiredChao[chaoState.Id] = flag;
		}
		else
		{
			this.IsRequiredChao.Add(chaoState.Id, flag);
		}
		if (this.m_acquiredChaoCount.ContainsKey(chaoState.Id))
		{
			Dictionary<int, int> acquiredChaoCount;
			Dictionary<int, int> dictionary = acquiredChaoCount = this.m_acquiredChaoCount;
			int num2;
			int key = num2 = chaoState.Id;
			num2 = acquiredChaoCount[num2];
			dictionary[key] = num2 + 1;
		}
		else
		{
			this.m_acquiredChaoCount.Add(chaoState.Id, 1);
		}
		if (this.m_getAlreadyOverlap.ContainsKey(chaoState.Id))
		{
			if (this.m_acquiredChaoCount.ContainsKey(chaoState.Id))
			{
				if (chaoState.Rarity < 100)
				{
					if (num + this.m_acquiredChaoCount[chaoState.Id] > ChaoTable.ChaoMaxLevel())
					{
						Dictionary<int, int> getAlreadyOverlap;
						Dictionary<int, int> dictionary2 = getAlreadyOverlap = this.m_getAlreadyOverlap;
						int num2;
						int key2 = num2 = chaoState.Id;
						num2 = getAlreadyOverlap[num2];
						dictionary2[key2] = num2 + 1;
					}
				}
				else
				{
					ServerCharacterState serverCharacterState2 = playerState.CharacterStateByItemID(chaoState.Id);
					if (serverCharacterState2 != null)
					{
						if (num + this.m_acquiredChaoCount[chaoState.Id] > serverCharacterState2.starMax)
						{
							Dictionary<int, int> getAlreadyOverlap2;
							Dictionary<int, int> dictionary3 = getAlreadyOverlap2 = this.m_getAlreadyOverlap;
							int num2;
							int key3 = num2 = chaoState.Id;
							num2 = getAlreadyOverlap2[num2];
							dictionary3[key3] = num2 + 1;
						}
					}
					else
					{
						this.m_getAlreadyOverlap[chaoState.Id] = 1;
					}
				}
			}
		}
		else if (!flag)
		{
			this.m_getAlreadyOverlap.Add(chaoState.Id, 1);
		}
		else
		{
			this.m_getAlreadyOverlap.Add(chaoState.Id, 0);
		}
	}

	// Token: 0x060037E9 RID: 14313 RVA: 0x00127634 File Offset: 0x00125834
	public void CopyTo(ServerSpinResultGeneral to)
	{
		to.IsRequiredChao = this.IsRequiredChao;
		to.m_requiredSpEggs = this.m_requiredSpEggs;
		to.m_itemState.Clear();
		to.m_acquiredChaoData.Clear();
		foreach (List<ServerChaoData> list in this.m_acquiredChaoData.Values)
		{
			List<ServerChaoData> list2 = new List<ServerChaoData>();
			int key = 0;
			foreach (ServerChaoData serverChaoData in list)
			{
				key = serverChaoData.Id;
				list2.Add(serverChaoData);
			}
			to.m_acquiredChaoData.Add(key, list2);
		}
		foreach (List<ServerItemState> list3 in this.m_itemState.Values)
		{
			List<ServerItemState> list4 = new List<ServerItemState>();
			int key2 = 0;
			foreach (ServerItemState serverItemState in list3)
			{
				key2 = serverItemState.m_itemId;
				list4.Add(serverItemState);
			}
			to.m_itemState.Add(key2, list4);
		}
		to.ItemWon = this.ItemWon;
		to.m_getAlreadyOverlap = this.m_getAlreadyOverlap;
		to.m_acquiredChaoCount = this.m_acquiredChaoCount;
		to.GetOtomoAndCharaMax();
	}

	// Token: 0x060037EA RID: 14314 RVA: 0x00127830 File Offset: 0x00125A30
	public int GetOtomoAndCharaMax()
	{
		int num = 0;
		Dictionary<int, bool>.KeyCollection keys = this.IsRequiredChao.Keys;
		foreach (int key in keys)
		{
			if (this.IsRequiredChao[key])
			{
				num++;
			}
		}
		this.m_getOtomoAndCharaMax = num;
		return num;
	}

	// Token: 0x060037EB RID: 14315 RVA: 0x001278B8 File Offset: 0x00125AB8
	public bool CheckGetChara()
	{
		bool result = false;
		Dictionary<int, bool>.KeyCollection keys = this.IsRequiredChao.Keys;
		foreach (int num in keys)
		{
			if (num >= 300000 && num < 400000)
			{
				result = true;
				break;
			}
		}
		return result;
	}

	// Token: 0x060037EC RID: 14316 RVA: 0x00127940 File Offset: 0x00125B40
	public ServerChaoData GetShowData(int index)
	{
		ServerChaoData result = null;
		if (this.m_getOtomoAndCharaMax > 0 && index >= 0 && index < this.m_getOtomoAndCharaMax)
		{
			Dictionary<int, bool>.KeyCollection keys = this.IsRequiredChao.Keys;
			List<int> list = new List<int>();
			foreach (int num in keys)
			{
				if (this.IsRequiredChao[num])
				{
					list.Add(num);
				}
			}
			list.Sort();
			if (list.Count > index && this.m_acquiredChaoData.ContainsKey(list[index]))
			{
				List<ServerChaoData> list2 = this.m_acquiredChaoData[list[index]];
				if (list2 != null && list2.Count > 0)
				{
					result = list2[list2.Count - 1];
				}
			}
		}
		return result;
	}

	// Token: 0x060037ED RID: 14317 RVA: 0x00127A4C File Offset: 0x00125C4C
	public void Dump()
	{
	}

	// Token: 0x04002F3F RID: 12095
	private Dictionary<int, List<ServerItemState>> m_itemState;

	// Token: 0x04002F40 RID: 12096
	private Dictionary<int, List<ServerChaoData>> m_acquiredChaoData;

	// Token: 0x04002F41 RID: 12097
	public int m_requiredSpEggs;

	// Token: 0x04002F42 RID: 12098
	private int m_getOtomoAndCharaMax = -1;
}
