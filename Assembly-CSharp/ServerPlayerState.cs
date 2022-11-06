using System;
using System.Collections.Generic;

// Token: 0x02000817 RID: 2071
public class ServerPlayerState
{
	// Token: 0x06003790 RID: 14224 RVA: 0x001249CC File Offset: 0x00122BCC
	public ServerPlayerState()
	{
		this.m_highScore = 1234L;
		this.m_multiplier = 1;
		this.m_numContinuesUsed = 0;
		this.m_currentMissionSet = 0;
		this.m_missionsComplete = new bool[3];
		for (int i = 0; i < 3; i++)
		{
			this.m_missionsComplete[i] = false;
		}
		this.m_totalHighScoreQuick = 0L;
		this.m_totalHighScore = 0L;
		this.m_totalDistance = 0L;
		this.m_maxDistance = 0L;
		this.m_leagueIndex = 0;
		this.m_leagueIndexQuick = 0;
		this.m_characterState = new ServerCharacterState[29];
		for (int j = 0; j < 29; j++)
		{
			this.m_characterState[j] = new ServerCharacterState();
		}
		this.m_characterState[0].m_tokenCost = 0;
		this.m_characterState[0].m_numTokens = 0;
		this.m_characterState[0].Status = ServerCharacterState.CharacterStatus.Unlocked;
		this.m_characterState[1].m_tokenCost = 20;
		this.m_characterState[2].m_tokenCost = 30;
		this.m_chaoState = new List<ServerChaoState>();
		this.m_playCharacterState = new ServerPlayCharacterState[29];
		for (int k = 0; k < this.m_equipItemList.Length; k++)
		{
			this.m_equipItemList[k] = -1;
		}
		this.m_itemStates = new Dictionary<int, ServerItemState>();
		this.m_numRings = 0;
		this.m_numFreeRings = 0;
		this.m_numBuyRings = 0;
		this.m_numRedRings = 0;
		this.m_numFreeRedRings = 0;
		this.m_numBuyRedRings = 0;
		this.m_numEnergy = 0;
		this.m_numFreeEnergy = 0;
		this.m_numBuyEnergy = 0;
		this.m_numUnreadMessages = 0;
		this.m_dailyMissionId = 0;
		this.m_dailyMissionValue = 0;
		this.m_dailyChallengeComplete = false;
		this.m_numDailyChalCont = 0;
		this.m_mainChaoId = -1;
		this.m_subChaoId = -1;
		this.m_energyRenewsAt = DateTime.Now;
		this.m_nextWeeklyLeaderboard = DateTime.Now + new TimeSpan(7, 0, 0, 0);
		this.m_endDailyMissionDate = DateTime.Now;
	}

	// Token: 0x06003791 RID: 14225 RVA: 0x00124BBC File Offset: 0x00122DBC
	public ServerCharacterState CharacterState(CharaType type)
	{
		if (type == CharaType.UNKNOWN)
		{
			return null;
		}
		if (type >= CharaType.NUM)
		{
			return null;
		}
		return this.m_characterState[(int)type];
	}

	// Token: 0x06003792 RID: 14226 RVA: 0x00124BE8 File Offset: 0x00122DE8
	public void ClearCharacterState()
	{
		for (int i = 0; i < 29; i++)
		{
			this.m_characterState[i] = new ServerCharacterState();
		}
	}

	// Token: 0x06003793 RID: 14227 RVA: 0x00124C18 File Offset: 0x00122E18
	public ServerCharacterState CharacterStateByItemID(int itemID)
	{
		ServerItem serverItem = new ServerItem((ServerItem.Id)itemID);
		return this.CharacterState(serverItem.charaType);
	}

	// Token: 0x06003794 RID: 14228 RVA: 0x00124C3C File Offset: 0x00122E3C
	public ServerPlayCharacterState PlayCharacterState(CharaType type)
	{
		if (type == CharaType.UNKNOWN)
		{
			return null;
		}
		if (type >= CharaType.NUM)
		{
			return null;
		}
		return this.m_playCharacterState[(int)type];
	}

	// Token: 0x1700083B RID: 2107
	// (get) Token: 0x06003795 RID: 14229 RVA: 0x00124C68 File Offset: 0x00122E68
	public int unlockedCharacterNum
	{
		get
		{
			int num = 0;
			if (this.m_characterState != null && this.m_characterState.Length > 0)
			{
				foreach (ServerCharacterState serverCharacterState in this.m_characterState)
				{
					if (serverCharacterState != null && serverCharacterState.IsUnlocked)
					{
						num++;
					}
				}
			}
			return num;
		}
	}

	// Token: 0x06003796 RID: 14230 RVA: 0x00124CC8 File Offset: 0x00122EC8
	public void SetCharacterState(ServerCharacterState characterState)
	{
		if (characterState == null)
		{
			return;
		}
		ServerItem serverItem = new ServerItem((ServerItem.Id)characterState.Id);
		CharaType charaType = serverItem.charaType;
		if (charaType == CharaType.UNKNOWN)
		{
			return;
		}
		int num = (int)charaType;
		if (num >= 29)
		{
			return;
		}
		ServerCharacterState serverCharacterState = this.m_characterState[num];
		ServerItem serverItem2 = new ServerItem((ServerItem.Id)serverCharacterState.Id);
		CharaType charaType2 = serverItem2.charaType;
		if (charaType2 != CharaType.UNKNOWN)
		{
			characterState.OldStatus = serverCharacterState.Status;
			characterState.OldCost = serverCharacterState.Cost;
			characterState.OldExp = serverCharacterState.Exp;
			characterState.OldAbiltyLevel.Clear();
			foreach (int item in serverCharacterState.AbilityLevel)
			{
				characterState.OldAbiltyLevel.Add(item);
			}
		}
		this.m_characterState[num] = characterState;
		NetUtil.SyncSaveDataAndDataBase(this.m_characterState);
	}

	// Token: 0x06003797 RID: 14231 RVA: 0x00124DD0 File Offset: 0x00122FD0
	public void SetCharacterState(ServerCharacterState[] characterStates)
	{
		if (characterStates == null)
		{
			return;
		}
		foreach (ServerCharacterState serverCharacterState in characterStates)
		{
			if (serverCharacterState != null)
			{
				this.SetCharacterState(serverCharacterState);
			}
		}
	}

	// Token: 0x06003798 RID: 14232 RVA: 0x00124E10 File Offset: 0x00123010
	public void SetPlayCharacterState(ServerPlayCharacterState playCharacterState)
	{
		if (playCharacterState == null)
		{
			return;
		}
		ServerItem serverItem = new ServerItem((ServerItem.Id)playCharacterState.Id);
		CharaType charaType = serverItem.charaType;
		if (charaType == CharaType.UNKNOWN)
		{
			return;
		}
		int num = (int)charaType;
		if (num >= 29)
		{
			return;
		}
		ServerCharacterState serverCharacterState = this.m_characterState[num];
		if (serverCharacterState != null)
		{
			serverCharacterState.OldAbiltyLevel.Clear();
			foreach (int item in serverCharacterState.AbilityLevel)
			{
				serverCharacterState.OldAbiltyLevel.Add(item);
			}
			serverCharacterState.OldStatus = serverCharacterState.Status;
			serverCharacterState.OldCost = serverCharacterState.Cost;
			serverCharacterState.OldExp = serverCharacterState.Exp;
			serverCharacterState.AbilityLevel.Clear();
			foreach (int item2 in playCharacterState.AbilityLevel)
			{
				serverCharacterState.AbilityLevel.Add(item2);
			}
			serverCharacterState.Level = playCharacterState.Level;
			serverCharacterState.Cost = playCharacterState.Cost;
			serverCharacterState.Exp = playCharacterState.Exp;
			serverCharacterState.NumRedRings = playCharacterState.NumRedRings;
			this.m_characterState[num] = serverCharacterState;
			NetUtil.SyncSaveDataAndDataBase(this.m_characterState);
		}
		this.m_playCharacterState[num] = playCharacterState;
	}

	// Token: 0x06003799 RID: 14233 RVA: 0x00124FA4 File Offset: 0x001231A4
	public void ClearPlayCharacterState()
	{
		for (int i = 0; i < 29; i++)
		{
			this.m_playCharacterState[i] = null;
		}
	}

	// Token: 0x1700083C RID: 2108
	// (get) Token: 0x0600379A RID: 14234 RVA: 0x00124FD0 File Offset: 0x001231D0
	public List<ServerChaoState> ChaoStates
	{
		get
		{
			return this.m_chaoState;
		}
	}

	// Token: 0x0600379B RID: 14235 RVA: 0x00124FD8 File Offset: 0x001231D8
	public ServerChaoState ChaoStateByItemID(int itemID)
	{
		if (this.m_chaoState != null)
		{
			int count = this.m_chaoState.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.m_chaoState[i].Id == itemID)
				{
					return this.m_chaoState[i];
				}
			}
		}
		return null;
	}

	// Token: 0x0600379C RID: 14236 RVA: 0x00125034 File Offset: 0x00123234
	public ServerChaoState ChaoStateByArrayIndex(int index)
	{
		if (this.m_chaoState != null && index < this.m_chaoState.Count)
		{
			return this.m_chaoState[index];
		}
		return null;
	}

	// Token: 0x0600379D RID: 14237 RVA: 0x0012506C File Offset: 0x0012326C
	public void SetChaoState(List<ServerChaoState> newChaoStateList)
	{
		if (this.m_chaoState == null)
		{
			return;
		}
		foreach (ServerChaoState serverChaoState in newChaoStateList)
		{
			if (serverChaoState != null)
			{
				bool flag = false;
				for (int i = 0; i < this.m_chaoState.Count; i++)
				{
					if (this.m_chaoState[i].Id == serverChaoState.Id)
					{
						this.m_chaoState[i] = serverChaoState;
						flag = true;
					}
				}
				if (!flag)
				{
					this.m_chaoState.Add(serverChaoState);
				}
			}
		}
		NetUtil.SyncSaveDataAndDataBase(this.m_chaoState);
	}

	// Token: 0x0600379E RID: 14238 RVA: 0x00125144 File Offset: 0x00123344
	public ServerItemState GetItemStateByType(ServerConstants.RunModifierType type)
	{
		int id = 0;
		switch (type)
		{
		case ServerConstants.RunModifierType.SpringBonus:
			id = 3;
			break;
		case ServerConstants.RunModifierType.RingStreak:
			id = 2;
			break;
		case ServerConstants.RunModifierType.EnemyCombo:
			id = 4;
			break;
		}
		return this.GetItemStateById(id);
	}

	// Token: 0x0600379F RID: 14239 RVA: 0x00125188 File Offset: 0x00123388
	public ServerItemState GetItemStateById(int id)
	{
		if (this.m_itemStates.ContainsKey(id))
		{
			return this.m_itemStates[id];
		}
		return null;
	}

	// Token: 0x060037A0 RID: 14240 RVA: 0x001251AC File Offset: 0x001233AC
	public int GetNumItemByType(ServerConstants.RunModifierType type)
	{
		ServerItemState itemStateByType = this.GetItemStateByType(type);
		if (itemStateByType != null)
		{
			return itemStateByType.m_num;
		}
		return 0;
	}

	// Token: 0x060037A1 RID: 14241 RVA: 0x001251D0 File Offset: 0x001233D0
	public int GetNumItemById(int id)
	{
		ServerItemState itemStateById = this.GetItemStateById(id);
		if (itemStateById != null)
		{
			return itemStateById.m_num;
		}
		return 0;
	}

	// Token: 0x060037A2 RID: 14242 RVA: 0x001251F4 File Offset: 0x001233F4
	public void AddItemState(ServerItemState itemState)
	{
		if (this.m_itemStates.ContainsKey(itemState.m_itemId))
		{
			this.m_itemStates[itemState.m_itemId].m_num += itemState.m_num;
		}
		else
		{
			this.m_itemStates.Add(itemState.m_itemId, itemState);
		}
	}

	// Token: 0x060037A3 RID: 14243 RVA: 0x00125254 File Offset: 0x00123454
	public void Dump()
	{
		string arg = string.Join(":", Array.ConvertAll<bool, string>(this.m_missionsComplete, (bool item) => item.ToString()));
		Debug.Log(string.Format("highScore={0}, multiplier={1}, numRings={2}, numRedRings={3}, energy={4}, energyRenewsAt={5}", new object[]
		{
			this.m_highScore,
			this.m_multiplier,
			this.m_numRings,
			this.m_numRedRings,
			this.m_numEnergy,
			this.m_energyRenewsAt
		}));
		Debug.Log(string.Format("currentMissionSet={0}, missions={1}, numContinuesUsed={2}", this.m_currentMissionSet, arg, this.m_numContinuesUsed));
		for (int i = 0; i < 29; i++)
		{
			this.m_characterState[i].Dump();
		}
	}

	// Token: 0x060037A4 RID: 14244 RVA: 0x00125344 File Offset: 0x00123544
	public void RefreshFakeState()
	{
	}

	// Token: 0x060037A5 RID: 14245 RVA: 0x00125348 File Offset: 0x00123548
	public void CopyTo(ServerPlayerState to)
	{
		to.m_highScore = this.m_highScore;
		to.m_numContinuesUsed = this.m_numContinuesUsed;
		to.m_multiplier = this.m_multiplier;
		to.m_currentMissionSet = this.m_currentMissionSet;
		to.m_missionsComplete = (this.m_missionsComplete.Clone() as bool[]);
		to.m_totalHighScoreQuick = this.m_totalHighScoreQuick;
		to.m_totalHighScore = this.m_totalHighScore;
		to.m_totalDistance = this.m_totalDistance;
		to.m_maxDistance = this.m_maxDistance;
		to.m_leagueIndex = this.m_leagueIndex;
		to.m_leagueIndexQuick = this.m_leagueIndexQuick;
		to.m_numRings = this.m_numRings;
		to.m_numFreeRings = this.m_numFreeRings;
		to.m_numBuyRings = this.m_numBuyRings;
		to.m_numRedRings = this.m_numRedRings;
		to.m_numFreeRedRings = this.m_numFreeRedRings;
		to.m_numBuyRedRings = this.m_numBuyRedRings;
		to.m_numEnergy = this.m_numEnergy;
		to.m_numFreeEnergy = this.m_numFreeEnergy;
		to.m_numBuyEnergy = this.m_numBuyEnergy;
		to.m_energyRenewsAt = this.m_energyRenewsAt;
		to.m_numUnreadMessages = this.m_numUnreadMessages;
		to.m_dailyMissionId = this.m_dailyMissionId;
		to.m_dailyMissionValue = this.m_dailyMissionValue;
		to.m_dailyChallengeComplete = this.m_dailyChallengeComplete;
		to.m_numDailyChalCont = this.m_numDailyChalCont;
		to.m_nextWeeklyLeaderboard = this.m_nextWeeklyLeaderboard;
		to.m_endDailyMissionDate = this.m_endDailyMissionDate;
		to.m_mainChaoId = this.m_mainChaoId;
		to.m_mainCharaId = this.m_mainCharaId;
		to.m_subCharaId = this.m_subCharaId;
		to.m_useSubCharacter = this.m_useSubCharacter;
		to.m_subChaoId = this.m_subChaoId;
		to.m_numPlaying = this.m_numPlaying;
		to.m_numAnimals = this.m_numAnimals;
		to.m_numRank = this.m_numRank;
		to.m_itemStates.Clear();
		foreach (ServerItemState serverItemState in this.m_itemStates.Values)
		{
			to.m_itemStates.Add(serverItemState.m_itemId, serverItemState);
		}
		for (int i = 0; i < this.m_equipItemList.Length; i++)
		{
			to.m_equipItemList[i] = this.m_equipItemList[i];
		}
		foreach (ServerCharacterState serverCharacterState in this.m_characterState)
		{
			if (serverCharacterState != null)
			{
				if (serverCharacterState.Id >= 0)
				{
					to.SetCharacterState(serverCharacterState);
				}
			}
		}
		to.SetChaoState(this.m_chaoState);
		NetUtil.SyncSaveDataAndDataBase(this);
	}

	// Token: 0x060037A6 RID: 14246 RVA: 0x00125608 File Offset: 0x00123808
	private void SetChaoState(ServerChaoState srcState, ref ServerChaoState dstState)
	{
		dstState.IsLvUp = (0 != dstState.Id & (dstState.IsLvUp | dstState.Level < srcState.Level));
		dstState.IsNew = (0 != dstState.Id & (dstState.IsNew | (dstState.Status == ServerChaoState.ChaoStatus.NotOwned && srcState.Status != ServerChaoState.ChaoStatus.NotOwned)));
		dstState.Id = srcState.Id;
		dstState.Level = srcState.Level;
		dstState.Rarity = srcState.Rarity;
		dstState.Status = srcState.Status;
		dstState.Dealing = srcState.Dealing;
		dstState.NumInvite = srcState.NumInvite;
		dstState.NumAcquired = srcState.NumAcquired;
		dstState.Hidden = srcState.Hidden;
		if (dstState.IsNew)
		{
			Debug.Log("requrired new chao. id : " + dstState.Id);
		}
		if (dstState.IsLvUp)
		{
			Debug.Log("chao level up. id : " + dstState.Id);
		}
	}

	// Token: 0x060037A7 RID: 14247 RVA: 0x00125730 File Offset: 0x00123930
	public List<CharaType> GetCharacterTypeList(ServerPlayerState.CHARA_SORT sort = ServerPlayerState.CHARA_SORT.NONE, bool descending = false, int offset = 0)
	{
		List<CharaType> list = null;
		Dictionary<CharaType, ServerCharacterState> characterStateList = this.GetCharacterStateList(sort, descending, offset);
		if (characterStateList != null && characterStateList.Count > 0)
		{
			list = new List<CharaType>();
			Dictionary<CharaType, ServerCharacterState>.KeyCollection keys = characterStateList.Keys;
			foreach (CharaType item in keys)
			{
				list.Add(item);
			}
		}
		return list;
	}

	// Token: 0x060037A8 RID: 14248 RVA: 0x001257C0 File Offset: 0x001239C0
	public Dictionary<CharaType, ServerCharacterState> GetCharacterStateList(ServerPlayerState.CHARA_SORT sort = ServerPlayerState.CHARA_SORT.NONE, bool descending = false, int offset = 0)
	{
		Dictionary<CharaType, ServerCharacterState> dictionary = new Dictionary<CharaType, ServerCharacterState>();
		Dictionary<CharaType, ServerCharacterState> dictionary2 = null;
		if (this.m_characterState != null && this.m_characterState.Length > 0)
		{
			foreach (ServerCharacterState serverCharacterState in this.m_characterState)
			{
				CharaType charaType = serverCharacterState.charaType;
				if (charaType != CharaType.UNKNOWN)
				{
					if (sort == ServerPlayerState.CHARA_SORT.NONE)
					{
						dictionary.Add(charaType, serverCharacterState);
					}
					else
					{
						if (dictionary2 == null)
						{
							dictionary2 = new Dictionary<CharaType, ServerCharacterState>();
						}
						dictionary2.Add(charaType, serverCharacterState);
					}
				}
			}
		}
		if (sort != ServerPlayerState.CHARA_SORT.NONE && dictionary2 != null)
		{
			if (sort != ServerPlayerState.CHARA_SORT.CHARA_ATTR)
			{
				if (sort != ServerPlayerState.CHARA_SORT.TEAM_ATTR)
				{
					dictionary = dictionary2;
				}
				else
				{
					this.GetCharacterStateListTeamAttr(ref dictionary, dictionary2, descending, offset);
				}
			}
			else
			{
				this.GetCharacterStateListCharaAttr(ref dictionary, dictionary2, descending, offset);
			}
		}
		return dictionary;
	}

	// Token: 0x060037A9 RID: 14249 RVA: 0x00125898 File Offset: 0x00123A98
	private void GetCharacterStateListCharaAttr(ref Dictionary<CharaType, ServerCharacterState> outList, Dictionary<CharaType, ServerCharacterState> orgList, bool descending, int offset)
	{
		Dictionary<CharacterAttribute, List<ServerCharacterState>> dictionary = new Dictionary<CharacterAttribute, List<ServerCharacterState>>();
		CharacterDataNameInfo instance = CharacterDataNameInfo.Instance;
		if (instance != null)
		{
			Dictionary<CharaType, ServerCharacterState>.KeyCollection keys = orgList.Keys;
			foreach (CharaType charaType in keys)
			{
				CharacterDataNameInfo.Info dataByID = instance.GetDataByID(charaType);
				if (dictionary.ContainsKey(dataByID.m_attribute))
				{
					dictionary[dataByID.m_attribute].Add(orgList[charaType]);
				}
				else
				{
					List<ServerCharacterState> list = new List<ServerCharacterState>();
					list.Add(orgList[charaType]);
					dictionary.Add(dataByID.m_attribute, list);
				}
			}
		}
		if (dictionary.Count > 0 && outList != null)
		{
			int num = 3;
			for (int i = 0; i < num; i++)
			{
				CharacterAttribute key;
				if (descending)
				{
					key = (CharacterAttribute)((offset - i + num) % num);
				}
				else
				{
					key = (CharacterAttribute)((offset + i) % num);
				}
				if (dictionary.ContainsKey(key))
				{
					foreach (ServerCharacterState serverCharacterState in dictionary[key])
					{
						outList.Add(serverCharacterState.charaType, serverCharacterState);
					}
				}
			}
		}
	}

	// Token: 0x060037AA RID: 14250 RVA: 0x00125A34 File Offset: 0x00123C34
	private void GetCharacterStateListTeamAttr(ref Dictionary<CharaType, ServerCharacterState> outList, Dictionary<CharaType, ServerCharacterState> orgList, bool descending, int offset)
	{
		Dictionary<TeamAttribute, List<ServerCharacterState>> dictionary = new Dictionary<TeamAttribute, List<ServerCharacterState>>();
		CharacterDataNameInfo instance = CharacterDataNameInfo.Instance;
		if (instance != null)
		{
			Dictionary<CharaType, ServerCharacterState>.KeyCollection keys = orgList.Keys;
			foreach (CharaType charaType in keys)
			{
				CharacterDataNameInfo.Info dataByID = instance.GetDataByID(charaType);
				if (dictionary.ContainsKey(dataByID.m_teamAttribute))
				{
					dictionary[dataByID.m_teamAttribute].Add(orgList[charaType]);
				}
				else
				{
					List<ServerCharacterState> list = new List<ServerCharacterState>();
					list.Add(orgList[charaType]);
					dictionary.Add(dataByID.m_teamAttribute, list);
				}
			}
		}
		if (dictionary.Count > 0 && outList != null)
		{
			int num = 8;
			for (int i = 0; i < num; i++)
			{
				TeamAttribute key;
				if (descending)
				{
					key = (TeamAttribute)((offset - i + num) % num);
				}
				else
				{
					key = (TeamAttribute)((offset + i) % num);
				}
				if (dictionary.ContainsKey(key))
				{
					foreach (ServerCharacterState serverCharacterState in dictionary[key])
					{
						outList.Add(serverCharacterState.charaType, serverCharacterState);
					}
				}
			}
		}
	}

	// Token: 0x04002EDE RID: 11998
	public long m_highScore;

	// Token: 0x04002EDF RID: 11999
	public int m_multiplier;

	// Token: 0x04002EE0 RID: 12000
	public int m_numContinuesUsed;

	// Token: 0x04002EE1 RID: 12001
	public int m_currentMissionSet;

	// Token: 0x04002EE2 RID: 12002
	public bool[] m_missionsComplete;

	// Token: 0x04002EE3 RID: 12003
	public long m_totalHighScore;

	// Token: 0x04002EE4 RID: 12004
	public long m_totalHighScoreQuick;

	// Token: 0x04002EE5 RID: 12005
	public long m_totalDistance;

	// Token: 0x04002EE6 RID: 12006
	public long m_maxDistance;

	// Token: 0x04002EE7 RID: 12007
	public int m_leagueIndex;

	// Token: 0x04002EE8 RID: 12008
	public int m_leagueIndexQuick;

	// Token: 0x04002EE9 RID: 12009
	public int m_numRings;

	// Token: 0x04002EEA RID: 12010
	public int m_numFreeRings;

	// Token: 0x04002EEB RID: 12011
	public int m_numBuyRings;

	// Token: 0x04002EEC RID: 12012
	public int m_numRedRings;

	// Token: 0x04002EED RID: 12013
	public int m_numFreeRedRings;

	// Token: 0x04002EEE RID: 12014
	public int m_numBuyRedRings;

	// Token: 0x04002EEF RID: 12015
	public int m_numEnergy;

	// Token: 0x04002EF0 RID: 12016
	public int m_numFreeEnergy;

	// Token: 0x04002EF1 RID: 12017
	public int m_numBuyEnergy;

	// Token: 0x04002EF2 RID: 12018
	public DateTime m_energyRenewsAt;

	// Token: 0x04002EF3 RID: 12019
	public DateTime m_nextWeeklyLeaderboard;

	// Token: 0x04002EF4 RID: 12020
	public DateTime m_endDailyMissionDate;

	// Token: 0x04002EF5 RID: 12021
	public int m_numUnreadMessages;

	// Token: 0x04002EF6 RID: 12022
	public int m_dailyMissionId;

	// Token: 0x04002EF7 RID: 12023
	public int m_dailyMissionValue;

	// Token: 0x04002EF8 RID: 12024
	public bool m_dailyChallengeComplete;

	// Token: 0x04002EF9 RID: 12025
	public int m_numDailyChalCont;

	// Token: 0x04002EFA RID: 12026
	public int m_mainCharaId;

	// Token: 0x04002EFB RID: 12027
	public int m_subCharaId;

	// Token: 0x04002EFC RID: 12028
	public bool m_useSubCharacter;

	// Token: 0x04002EFD RID: 12029
	public int m_mainChaoId;

	// Token: 0x04002EFE RID: 12030
	public int m_subChaoId;

	// Token: 0x04002EFF RID: 12031
	public int[] m_equipItemList = new int[3];

	// Token: 0x04002F00 RID: 12032
	public int m_numPlaying;

	// Token: 0x04002F01 RID: 12033
	public int m_numAnimals;

	// Token: 0x04002F02 RID: 12034
	public int m_numRank;

	// Token: 0x04002F03 RID: 12035
	private ServerCharacterState[] m_characterState;

	// Token: 0x04002F04 RID: 12036
	private List<ServerChaoState> m_chaoState;

	// Token: 0x04002F05 RID: 12037
	private Dictionary<int, ServerItemState> m_itemStates;

	// Token: 0x04002F06 RID: 12038
	private ServerPlayCharacterState[] m_playCharacterState;

	// Token: 0x02000818 RID: 2072
	public enum CHARA_SORT
	{
		// Token: 0x04002F09 RID: 12041
		NONE,
		// Token: 0x04002F0A RID: 12042
		CHARA_ATTR,
		// Token: 0x04002F0B RID: 12043
		TEAM_ATTR
	}
}
