using System;
using System.Collections.Generic;

// Token: 0x02000827 RID: 2087
public class ServerWheelOptions
{
	// Token: 0x0600380C RID: 14348 RVA: 0x00127CA0 File Offset: 0x00125EA0
	public ServerWheelOptions(ServerWheelOptions options = null)
	{
		this.m_nextSpinCost = 1;
		this.m_items = new int[8];
		this.m_itemQuantities = new int[8];
		this.m_itemWeight = new int[8];
		for (int i = 0; i < 8; i++)
		{
			if (i == 0)
			{
				this.m_items[i] = 200000;
			}
			else
			{
				this.m_items[i] = 120000 + i - 1;
			}
			this.m_itemQuantities[i] = 1;
			this.m_itemWeight[i] = 1;
		}
		this.m_itemWon = 0;
		this.m_spinCost = 0;
		this.m_rouletteRank = RouletteUtility.WheelRank.Normal;
		this.m_numRouletteToken = 0;
		this.m_numJackpotRing = 0;
		this.m_numRemaining = 1;
		this.m_nextFreeSpin = DateTime.Now;
		if (options != null)
		{
			options.CopyTo(this);
		}
	}

	// Token: 0x1700085A RID: 2138
	// (get) Token: 0x0600380D RID: 14349 RVA: 0x00127D70 File Offset: 0x00125F70
	public int m_numRemainingFree
	{
		get
		{
			int num = this.m_numRemaining - this.m_numRouletteToken;
			if (num < 0)
			{
				num = 0;
			}
			return num;
		}
	}

	// Token: 0x0600380E RID: 14350 RVA: 0x00127D98 File Offset: 0x00125F98
	public void Dump()
	{
		string text = string.Join(",", Array.ConvertAll<int, string>(this.m_items, (int item) => item.ToString()));
		Debug.Log(string.Format("items={0}, itemWon={1}, spinCost={2}, nextSpinCost={3}, nextFreeSpin={4}", new object[]
		{
			text,
			this.m_itemWon,
			this.m_spinCost,
			this.m_nextSpinCost,
			this.m_nextFreeSpin
		}));
	}

	// Token: 0x0600380F RID: 14351 RVA: 0x00127E2C File Offset: 0x0012602C
	public void RefreshFakeState()
	{
	}

	// Token: 0x1700085B RID: 2139
	// (get) Token: 0x06003810 RID: 14352 RVA: 0x00127E30 File Offset: 0x00126030
	public int NumRequiredSpEggs
	{
		get
		{
			int num = 0;
			if (this.m_itemList != null)
			{
				Dictionary<int, ServerItemState>.KeyCollection keys = this.m_itemList.Keys;
				foreach (int num2 in keys)
				{
					if (num2 == 220000)
					{
						num += this.m_itemList[num2].m_num;
					}
				}
			}
			return num;
		}
	}

	// Token: 0x06003811 RID: 14353 RVA: 0x00127EC4 File Offset: 0x001260C4
	public ServerItemState GetItem()
	{
		ServerItemState serverItemState = null;
		if (this.m_itemWon >= 0 && this.m_itemWon < this.m_items.Length)
		{
			ServerItem serverItem = new ServerItem((ServerItem.Id)this.m_items[this.m_itemWon]);
			if (serverItem.idType != ServerItem.IdType.CHAO && serverItem.idType != ServerItem.IdType.CHARA)
			{
				serverItemState = new ServerItemState();
				serverItemState.m_itemId = (int)serverItem.id;
				serverItemState.m_num = this.m_itemQuantities[this.m_itemWon];
			}
		}
		return serverItemState;
	}

	// Token: 0x06003812 RID: 14354 RVA: 0x00127F4C File Offset: 0x0012614C
	public ServerChaoState GetChao()
	{
		ServerChaoState serverChaoState = null;
		if (this.m_itemWon >= 0 && this.m_itemWon < this.m_items.Length)
		{
			ServerItem serverItem = new ServerItem((ServerItem.Id)this.m_items[this.m_itemWon]);
			if (serverItem.idType == ServerItem.IdType.CHAO || serverItem.idType == ServerItem.IdType.CHARA)
			{
				ServerPlayerState playerState = ServerInterface.PlayerState;
				serverChaoState = new ServerChaoState();
				if (serverItem.idType == ServerItem.IdType.CHAO)
				{
					ServerChaoState serverChaoState2 = playerState.ChaoStateByItemID((int)serverItem.id);
					serverChaoState.Id = (int)serverItem.id;
					if (serverChaoState2 != null)
					{
						serverChaoState.Status = serverChaoState2.Status;
						serverChaoState.Level = serverChaoState2.Level;
						serverChaoState.Rarity = serverChaoState2.Rarity;
					}
					else
					{
						serverChaoState.Status = ServerChaoState.ChaoStatus.NotOwned;
						serverChaoState.Level = 0;
						serverChaoState.Rarity = 0;
					}
				}
				else if (serverItem.idType == ServerItem.IdType.CHARA)
				{
					ServerCharacterState serverCharacterState = playerState.CharacterStateByItemID((int)serverItem.id);
					serverChaoState.Id = (int)serverItem.id;
					serverChaoState.Status = ServerChaoState.ChaoStatus.MaxLevel;
					serverChaoState.Level = 0;
					serverChaoState.Rarity = 100;
					if (serverCharacterState == null)
					{
						serverChaoState.Status = ServerChaoState.ChaoStatus.NotOwned;
					}
					else if (serverCharacterState.Id < 0 || !serverCharacterState.IsUnlocked)
					{
						serverChaoState.Status = ServerChaoState.ChaoStatus.NotOwned;
					}
				}
			}
		}
		return serverChaoState;
	}

	// Token: 0x06003813 RID: 14355 RVA: 0x0012809C File Offset: 0x0012629C
	public bool IsItemList()
	{
		return this.m_itemList != null;
	}

	// Token: 0x06003814 RID: 14356 RVA: 0x001280AC File Offset: 0x001262AC
	public void ResetItemList()
	{
		if (this.m_itemList != null)
		{
			this.m_itemList.Clear();
		}
		this.m_itemList = null;
	}

	// Token: 0x06003815 RID: 14357 RVA: 0x001280CC File Offset: 0x001262CC
	public void AddItemList(ServerItemState item)
	{
		if (this.m_itemList == null)
		{
			this.m_itemList = new Dictionary<int, ServerItemState>();
		}
		Debug.Log(string.Concat(new object[]
		{
			"ServerWheelOptions AddItemList id:",
			item.m_itemId,
			"  num:",
			item.m_num,
			"  !!!!!!!!!!!!!!!!!!!!!!!!!"
		}));
		if (this.m_itemList.ContainsKey(item.m_itemId))
		{
			this.m_itemList[item.m_itemId].m_num += item.m_num;
		}
		else
		{
			this.m_itemList.Add(item.m_itemId, item);
		}
	}

	// Token: 0x06003816 RID: 14358 RVA: 0x00128184 File Offset: 0x00126384
	public void CopyTo(ServerWheelOptions to)
	{
		to.m_nextSpinCost = this.m_nextSpinCost;
		to.m_itemWon = this.m_itemWon;
		to.m_items = (this.m_items.Clone() as int[]);
		to.m_itemQuantities = (this.m_itemQuantities.Clone() as int[]);
		to.m_itemWeight = (this.m_itemWeight.Clone() as int[]);
		to.m_spinCost = this.m_spinCost;
		to.m_rouletteRank = this.m_rouletteRank;
		to.m_numRouletteToken = this.m_numRouletteToken;
		to.m_numJackpotRing = this.m_numJackpotRing;
		to.m_numRemaining = this.m_numRemaining;
		to.m_nextFreeSpin = this.m_nextFreeSpin;
		to.ResetItemList();
		if (this.m_itemList != null && this.m_itemList.Count > 0)
		{
			Dictionary<int, ServerItemState>.KeyCollection keys = this.m_itemList.Keys;
			foreach (int key in keys)
			{
				to.AddItemList(this.m_itemList[key]);
			}
		}
	}

	// Token: 0x04002F55 RID: 12117
	public int m_nextSpinCost;

	// Token: 0x04002F56 RID: 12118
	public int[] m_items;

	// Token: 0x04002F57 RID: 12119
	public int[] m_itemQuantities;

	// Token: 0x04002F58 RID: 12120
	public int[] m_itemWeight;

	// Token: 0x04002F59 RID: 12121
	public int m_itemWon;

	// Token: 0x04002F5A RID: 12122
	public int m_spinCost;

	// Token: 0x04002F5B RID: 12123
	public RouletteUtility.WheelRank m_rouletteRank;

	// Token: 0x04002F5C RID: 12124
	public int m_numRouletteToken;

	// Token: 0x04002F5D RID: 12125
	public int m_numJackpotRing;

	// Token: 0x04002F5E RID: 12126
	public int m_numRemaining;

	// Token: 0x04002F5F RID: 12127
	public DateTime m_nextFreeSpin;

	// Token: 0x04002F60 RID: 12128
	public Dictionary<int, ServerItemState> m_itemList;

	// Token: 0x02000828 RID: 2088
	public enum WheelItemType
	{
		// Token: 0x04002F63 RID: 12131
		CharacterTokenAmy,
		// Token: 0x04002F64 RID: 12132
		CharacterTokenTails,
		// Token: 0x04002F65 RID: 12133
		CharacterTokenKnuckles,
		// Token: 0x04002F66 RID: 12134
		CharacterTokenShadow,
		// Token: 0x04002F67 RID: 12135
		CharacterTokenBlaze,
		// Token: 0x04002F68 RID: 12136
		FeverTime,
		// Token: 0x04002F69 RID: 12137
		GoldenEnemy,
		// Token: 0x04002F6A RID: 12138
		RedStarRingsSmall,
		// Token: 0x04002F6B RID: 12139
		RedStarRingsMedium,
		// Token: 0x04002F6C RID: 12140
		RedStarRingsLarge,
		// Token: 0x04002F6D RID: 12141
		RedStarRingsJackpot,
		// Token: 0x04002F6E RID: 12142
		RingsSmall,
		// Token: 0x04002F6F RID: 12143
		RingsMedium,
		// Token: 0x04002F70 RID: 12144
		RingsLarge,
		// Token: 0x04002F71 RID: 12145
		RingsJackpot,
		// Token: 0x04002F72 RID: 12146
		SpinAgain,
		// Token: 0x04002F73 RID: 12147
		Energy,
		// Token: 0x04002F74 RID: 12148
		Max
	}
}
