using System;
using System.Collections.Generic;
using Text;

// Token: 0x020007D2 RID: 2002
public struct ServerItem
{
	// Token: 0x0600354D RID: 13645 RVA: 0x0011EA14 File Offset: 0x0011CC14
	public ServerItem(ServerItem.Id id)
	{
		this.m_id = id;
	}

	// Token: 0x0600354E RID: 13646 RVA: 0x0011EA20 File Offset: 0x0011CC20
	public ServerItem(CharaType characterType)
	{
		this.m_id = ServerItem.Id.NONE;
		if (characterType != CharaType.UNKNOWN && CharacterDataNameInfo.Instance != null)
		{
			CharacterDataNameInfo.Info dataByID = CharacterDataNameInfo.Instance.GetDataByID(characterType);
			if (dataByID != null)
			{
				this.m_id = (ServerItem.Id)dataByID.m_serverID;
			}
		}
	}

	// Token: 0x0600354F RID: 13647 RVA: 0x0011EA6C File Offset: 0x0011CC6C
	public ServerItem(ItemType itemType)
	{
		this.m_id = (ServerItem.Id)((itemType == ItemType.UNKNOWN) ? ItemType.UNKNOWN : (itemType + 120000));
	}

	// Token: 0x06003550 RID: 13648 RVA: 0x0011EA88 File Offset: 0x0011CC88
	public ServerItem(BoostItemType boostItemType)
	{
		this.m_id = (ServerItem.Id)((boostItemType >= BoostItemType.NUM) ? BoostItemType.UNKNOWN : (boostItemType + 110000));
	}

	// Token: 0x06003551 RID: 13649 RVA: 0x0011EAA4 File Offset: 0x0011CCA4
	public ServerItem(AbilityType abilityType)
	{
		if (ServerItem.AbilityToServerId.ContainsKey(abilityType))
		{
			this.m_id = ServerItem.AbilityToServerId[abilityType];
		}
		else
		{
			this.m_id = ServerItem.Id.NONE;
		}
	}

	// Token: 0x06003552 RID: 13650 RVA: 0x0011EAD4 File Offset: 0x0011CCD4
	public ServerItem(RewardType rewardType)
	{
		this.m_id = ServerItem.Id.NONE;
		foreach (ServerItem.Id id in ServerItem.s_dic_ServerItemId_to_RewardType.Keys)
		{
			if (ServerItem.s_dic_ServerItemId_to_RewardType[id] == rewardType)
			{
				this.m_id = id;
				break;
			}
		}
		if (this.m_id == ServerItem.Id.NONE)
		{
			this.m_id = (ServerItem.Id)rewardType;
		}
	}

	// Token: 0x06003554 RID: 13652 RVA: 0x0011EDD0 File Offset: 0x0011CFD0
	public static string GetIdTypeAtlasName(ServerItem.IdType idType)
	{
		string result = null;
		if (idType != ServerItem.IdType.NONE)
		{
			result = ServerItem.IdTypeAtlasName[idType];
		}
		return result;
	}

	// Token: 0x06003555 RID: 13653 RVA: 0x0011EDF4 File Offset: 0x0011CFF4
	public static ServerItem.Id ConvertAbilityId(AbilityType abilityType)
	{
		ServerItem.Id result = ServerItem.Id.NONE;
		if (ServerItem.AbilityToServerId.ContainsKey(abilityType))
		{
			result = ServerItem.AbilityToServerId[abilityType];
		}
		return result;
	}

	// Token: 0x17000760 RID: 1888
	// (get) Token: 0x06003556 RID: 13654 RVA: 0x0011EE20 File Offset: 0x0011D020
	public ServerItem.Id id
	{
		get
		{
			return this.m_id;
		}
	}

	// Token: 0x17000761 RID: 1889
	// (get) Token: 0x06003557 RID: 13655 RVA: 0x0011EE28 File Offset: 0x0011D028
	public ServerItem.IdType idType
	{
		get
		{
			return (ServerItem.IdType)(this.m_id / (ServerItem.Id)10000);
		}
	}

	// Token: 0x17000762 RID: 1890
	// (get) Token: 0x06003558 RID: 13656 RVA: 0x0011EE38 File Offset: 0x0011D038
	public int idIndex
	{
		get
		{
			return (int)(this.m_id % ((this.idType != ServerItem.IdType.EQUIP_ITEM) ? ((ServerItem.Id)10000) : ((ServerItem.Id)100)));
		}
	}

	// Token: 0x17000763 RID: 1891
	// (get) Token: 0x06003559 RID: 13657 RVA: 0x0011EE68 File Offset: 0x0011D068
	public bool isPacked
	{
		get
		{
			return this.packedNumber != 0;
		}
	}

	// Token: 0x17000764 RID: 1892
	// (get) Token: 0x0600355A RID: 13658 RVA: 0x0011EE78 File Offset: 0x0011D078
	public bool isValid
	{
		get
		{
			return this.m_id != ServerItem.Id.NONE;
		}
	}

	// Token: 0x17000765 RID: 1893
	// (get) Token: 0x0600355B RID: 13659 RVA: 0x0011EE88 File Offset: 0x0011D088
	private int packedNumber
	{
		get
		{
			return (int)((this.idType != ServerItem.IdType.EQUIP_ITEM) ? ((ServerItem.Id)0) : (this.m_id % (ServerItem.Id)10000 / (ServerItem.Id)100));
		}
	}

	// Token: 0x17000766 RID: 1894
	// (get) Token: 0x0600355C RID: 13660 RVA: 0x0011EEB8 File Offset: 0x0011D0B8
	public int serverItemNum
	{
		get
		{
			return this.packedNumber;
		}
	}

	// Token: 0x17000767 RID: 1895
	// (get) Token: 0x0600355D RID: 13661 RVA: 0x0011EEC0 File Offset: 0x0011D0C0
	public string serverItemName
	{
		get
		{
			string result = null;
			int num = (int)(this.m_id % (ServerItem.Id)1000);
			ServerItem.IdType idType = this.idType;
			switch (idType)
			{
			case ServerItem.IdType.ROULLETE_TOKEN:
				break;
			case ServerItem.IdType.EGG_ITEM:
			{
				string cellID = "sp_egg_name";
				result = TextUtility.GetCommonText("ChaoRoulette", cellID);
				break;
			}
			case ServerItem.IdType.PREMIUM_ROULLETE_TICKET:
			{
				string cellID = "premium_roulette_ticket";
				result = TextUtility.GetCommonText("Item", cellID);
				break;
			}
			case ServerItem.IdType.ITEM_ROULLETE_TICKET:
			{
				string cellID = "item_roulette_ticket";
				result = TextUtility.GetCommonText("Item", cellID);
				break;
			}
			default:
				switch (idType)
				{
				case ServerItem.IdType.RSRING:
				{
					string cellID = "red_star_ring";
					result = TextUtility.GetCommonText("Item", cellID);
					break;
				}
				case ServerItem.IdType.RING:
				{
					string cellID = "ring";
					result = TextUtility.GetCommonText("Item", cellID);
					break;
				}
				case ServerItem.IdType.ENERGY:
				{
					string cellID = "energy";
					result = TextUtility.GetCommonText("Item", cellID);
					break;
				}
				case ServerItem.IdType.ENERGY_MAX:
				{
					string cellID = "energy";
					result = TextUtility.GetCommonText("Item", cellID);
					break;
				}
				default:
					if (idType != ServerItem.IdType.BOOST_ITEM)
					{
						if (idType != ServerItem.IdType.EQUIP_ITEM)
						{
							if (idType == ServerItem.IdType.CHAO)
							{
								string cellID2 = "name" + this.chaoId.ToString("D4");
								result = TextUtility.GetChaoText("Chao", cellID2);
							}
						}
						else
						{
							string cellID = string.Format("name{0}", num % 100 + 1);
							result = TextUtility.GetCommonText("ShopItem", cellID);
						}
					}
					break;
				case ServerItem.IdType.RAIDRING:
				{
					string cellID = "raidboss_ring";
					result = TextUtility.GetCommonText("Item", cellID);
					break;
				}
				}
				break;
			case ServerItem.IdType.CHARA:
			{
				CharacterDataNameInfo.Info dataByServerID = CharacterDataNameInfo.Instance.GetDataByServerID((int)this.m_id);
				if (dataByServerID != null)
				{
					string cellID = dataByServerID.m_name.ToLower();
					result = TextUtility.GetCommonText("CharaName", cellID);
				}
				break;
			}
			}
			return result;
		}
	}

	// Token: 0x17000768 RID: 1896
	// (get) Token: 0x0600355E RID: 13662 RVA: 0x0011F0AC File Offset: 0x0011D2AC
	public string serverItemComment
	{
		get
		{
			string result = null;
			int num = (int)(this.m_id % (ServerItem.Id)1000);
			ServerItem.IdType idType = this.idType;
			switch (idType)
			{
			case ServerItem.IdType.RSRING:
			{
				string cellID = "red_star_ring_details";
				result = TextUtility.GetCommonText("Item", cellID);
				break;
			}
			case ServerItem.IdType.RING:
			{
				string cellID = "ring_details";
				result = TextUtility.GetCommonText("Item", cellID);
				break;
			}
			case ServerItem.IdType.ENERGY:
			{
				string cellID = "energy_details";
				result = TextUtility.GetCommonText("Item", cellID);
				break;
			}
			case ServerItem.IdType.ENERGY_MAX:
			{
				string cellID = "energy_details";
				result = TextUtility.GetCommonText("Item", cellID);
				break;
			}
			default:
				if (idType != ServerItem.IdType.BOOST_ITEM)
				{
					if (idType != ServerItem.IdType.EQUIP_ITEM)
					{
						if (idType != ServerItem.IdType.ROULLETE_TOKEN)
						{
							if (idType != ServerItem.IdType.EGG_ITEM)
							{
								if (idType != ServerItem.IdType.CHARA)
								{
									if (idType != ServerItem.IdType.CHAO)
									{
									}
								}
								else
								{
									CharacterDataNameInfo.Info dataByServerID = CharacterDataNameInfo.Instance.GetDataByServerID((int)this.m_id);
									if (dataByServerID != null)
									{
										string cellID = string.Format("chara_attribute_{0}", dataByServerID.m_name.ToLower());
										result = TextUtility.GetCommonText("WindowText", cellID);
									}
								}
							}
							else
							{
								string cellID = "sp_egg_details";
								result = TextUtility.GetCommonText("ChaoRoulette", cellID);
							}
						}
						else if (num == 0)
						{
							result = "BIG";
						}
						else if (num == 1)
						{
							result = "SUPER";
						}
						else
						{
							result = "ジャックポット";
						}
					}
					else
					{
						string cellID = string.Format("details{0}", num % 100 + 1);
						result = TextUtility.GetCommonText("ShopItem", cellID);
					}
				}
				else
				{
					result = "[BOOST_ITEM]";
					if (num % 3 == 0)
					{
						result = "スコアボーナス100%";
					}
					else if (num % 3 == 1)
					{
						result = "アシストトランポリン";
					}
					else if (num % 3 == 2)
					{
						result = "サブキャラクター";
					}
				}
				break;
			}
			return result;
		}
	}

	// Token: 0x17000769 RID: 1897
	// (get) Token: 0x0600355F RID: 13663 RVA: 0x0011F274 File Offset: 0x0011D474
	public string serverItemSpriteName
	{
		get
		{
			string result = null;
			int num = (int)(this.m_id % (ServerItem.Id)1000);
			ServerItem.IdType idType = this.idType;
			switch (idType)
			{
			case ServerItem.IdType.RSRING:
				result = string.Format("ui_cmn_icon_item_{0}", 9);
				break;
			case ServerItem.IdType.RING:
				result = string.Format("ui_cmn_icon_item_{0}", 8);
				break;
			case ServerItem.IdType.ENERGY:
				result = string.Format("ui_cmn_icon_item_{0}", 92000);
				break;
			case ServerItem.IdType.ENERGY_MAX:
				result = string.Format("ui_cmn_icon_item_{0}", 92000);
				break;
			default:
				if (idType != ServerItem.IdType.BOOST_ITEM)
				{
					if (idType != ServerItem.IdType.EQUIP_ITEM)
					{
						if (idType != ServerItem.IdType.ROULLETE_TOKEN)
						{
							if (idType != ServerItem.IdType.EGG_ITEM)
							{
								if (idType != ServerItem.IdType.CHARA)
								{
									if (idType != ServerItem.IdType.CHAO)
									{
									}
								}
								else if (num % 100 < CharacterDataNameInfo.PrefixNameList.Length)
								{
									string arg = CharacterDataNameInfo.PrefixNameList[num % 100];
									result = string.Format("ui_tex_player_{0:00}_{1}", num % 100, arg);
								}
							}
							else
							{
								result = "ui_cmn_icon_item_220000";
							}
						}
						else
						{
							result = string.Format("ui_cmn_icon_item_{0}", num % 100);
						}
					}
					else
					{
						result = string.Format("ui_mm_player_icon_{0}", num % 100);
					}
				}
				else
				{
					result = string.Format("ui_itemset_2_boost_icon_{0}", num);
				}
				break;
			}
			return result;
		}
	}

	// Token: 0x1700076A RID: 1898
	// (get) Token: 0x06003560 RID: 13664 RVA: 0x0011F3DC File Offset: 0x0011D5DC
	public string serverItemSpriteNameRoulette
	{
		get
		{
			string result = null;
			int num = (int)(this.m_id % (ServerItem.Id)1000);
			ServerItem.IdType idType = this.idType;
			switch (idType)
			{
			case ServerItem.IdType.RSRING:
				result = string.Format("ui_cmn_icon_item_{0}", 9);
				break;
			case ServerItem.IdType.RING:
				result = string.Format("ui_cmn_icon_item_{0}", 8);
				break;
			case ServerItem.IdType.ENERGY:
				result = string.Format("ui_cmn_icon_item_{0}", 92000);
				break;
			case ServerItem.IdType.ENERGY_MAX:
				result = string.Format("ui_cmn_icon_item_{0}", 92000);
				break;
			default:
				if (idType != ServerItem.IdType.EQUIP_ITEM)
				{
					if (idType != ServerItem.IdType.EGG_ITEM)
					{
						if (idType != ServerItem.IdType.CHARA)
						{
							if (idType != ServerItem.IdType.CHAO)
							{
							}
						}
						else if (num % 100 < CharacterDataNameInfo.PrefixNameList.Length)
						{
							string arg = CharacterDataNameInfo.PrefixNameList[num % 100];
							result = string.Format("ui_tex_player_{0:00}_{1}", num % 100, arg);
						}
					}
					else
					{
						result = "ui_cmn_icon_item_220000";
					}
				}
				else
				{
					result = string.Format("ui_cmn_icon_item_{0}", num % 100);
				}
				break;
			}
			return result;
		}
	}

	// Token: 0x06003561 RID: 13665 RVA: 0x0011F504 File Offset: 0x0011D704
	public static ServerItem CreateFromChaoId(int chaoId)
	{
		return new ServerItem((chaoId == -1) ? ServerItem.Id.NONE : (chaoId + ServerItem.Id.CHAO_BEGIN));
	}

	// Token: 0x1700076B RID: 1899
	// (get) Token: 0x06003562 RID: 13666 RVA: 0x0011F520 File Offset: 0x0011D720
	public CharaType charaType
	{
		get
		{
			if (this.idType == ServerItem.IdType.CHARA && CharacterDataNameInfo.Instance != null)
			{
				CharacterDataNameInfo.Info dataByServerID = CharacterDataNameInfo.Instance.GetDataByServerID((int)this.m_id);
				if (dataByServerID != null)
				{
					return dataByServerID.m_ID;
				}
			}
			return CharaType.UNKNOWN;
		}
	}

	// Token: 0x1700076C RID: 1900
	// (get) Token: 0x06003563 RID: 13667 RVA: 0x0011F56C File Offset: 0x0011D76C
	public ItemType itemType
	{
		get
		{
			return (ItemType)((this.idType != ServerItem.IdType.EQUIP_ITEM || this.idIndex >= 8) ? -1 : this.idIndex);
		}
	}

	// Token: 0x1700076D RID: 1901
	// (get) Token: 0x06003564 RID: 13668 RVA: 0x0011F594 File Offset: 0x0011D794
	public BoostItemType boostItemType
	{
		get
		{
			return (BoostItemType)((this.idType != ServerItem.IdType.BOOST_ITEM || this.idIndex >= 3) ? -1 : this.idIndex);
		}
	}

	// Token: 0x1700076E RID: 1902
	// (get) Token: 0x06003565 RID: 13669 RVA: 0x0011F5BC File Offset: 0x0011D7BC
	public AbilityType abilityType
	{
		get
		{
			if (ServerItem.ServerIdToAbility.ContainsKey(this.m_id))
			{
				return ServerItem.ServerIdToAbility[this.m_id];
			}
			return AbilityType.NONE;
		}
	}

	// Token: 0x1700076F RID: 1903
	// (get) Token: 0x06003566 RID: 13670 RVA: 0x0011F5E8 File Offset: 0x0011D7E8
	public int chaoId
	{
		get
		{
			return (this.idType != ServerItem.IdType.CHAO) ? -1 : this.idIndex;
		}
	}

	// Token: 0x06003567 RID: 13671 RVA: 0x0011F604 File Offset: 0x0011D804
	public static ServerItem[] GetServerItemTable(ServerItem.IdType idType)
	{
		if (!ServerItem.s_dicServerItemTable.ContainsKey(idType))
		{
			List<ServerItem> list = new List<ServerItem>();
			foreach (object obj in Enum.GetValues(typeof(ServerItem.Id)))
			{
				ServerItem.Id id = (ServerItem.Id)((int)obj);
				ServerItem serverItem = new ServerItem(id);
				if (serverItem.idType == idType)
				{
					list.Add(new ServerItem(id));
				}
			}
			ServerItem.s_dicServerItemTable[idType] = list.ToArray();
		}
		return ServerItem.s_dicServerItemTable[idType];
	}

	// Token: 0x06003568 RID: 13672 RVA: 0x0011F6D0 File Offset: 0x0011D8D0
	public static int GetServerItemCount(ServerItem.IdType idType)
	{
		return ServerItem.GetServerItemTable(idType).Length;
	}

	// Token: 0x17000770 RID: 1904
	// (get) Token: 0x06003569 RID: 13673 RVA: 0x0011F6DC File Offset: 0x0011D8DC
	public RewardType rewardType
	{
		get
		{
			RewardType id;
			if (!ServerItem.s_dic_ServerItemId_to_RewardType.TryGetValue(this.id, out id))
			{
				id = (RewardType)this.id;
			}
			return id;
		}
	}

	// Token: 0x04002CBB RID: 11451
	private const int SERVER_ID_INDEX_DIVISOR = 10000;

	// Token: 0x04002CBC RID: 11452
	private const int SERVER_ID_EQUIP_ITEM_INDEX_DIVISOR = 100;

	// Token: 0x04002CBD RID: 11453
	private static Dictionary<ServerItem.IdType, string> IdTypeAtlasName = new Dictionary<ServerItem.IdType, string>
	{
		{
			ServerItem.IdType.NONE,
			string.Empty
		},
		{
			ServerItem.IdType.BOOST_ITEM,
			"ui_item_set_3_Atlas"
		},
		{
			ServerItem.IdType.EQUIP_ITEM,
			"ui_player_set_icon_Atlas"
		},
		{
			ServerItem.IdType.ITEM_ROULLETE_WIN,
			string.Empty
		},
		{
			ServerItem.IdType.ROULLETE_TOKEN,
			"ui_cmn_item_Atlas"
		},
		{
			ServerItem.IdType.EGG_ITEM,
			"ui_mainmenu_Atlas"
		},
		{
			ServerItem.IdType.CHARA,
			"ui_cmn_player_bundle_Atlas"
		},
		{
			ServerItem.IdType.CHAO,
			"ui_cmn_chao_Atlas"
		},
		{
			ServerItem.IdType.RSRING,
			"ui_cmn_item_Atlas"
		},
		{
			ServerItem.IdType.RING,
			"ui_cmn_item_Atlas"
		},
		{
			ServerItem.IdType.ENERGY,
			"ui_cmn_item_Atlas"
		},
		{
			ServerItem.IdType.ENERGY_MAX,
			"ui_cmn_item_Atlas"
		}
	};

	// Token: 0x04002CBE RID: 11454
	private static Dictionary<AbilityType, ServerItem.Id> AbilityToServerId = new Dictionary<AbilityType, ServerItem.Id>
	{
		{
			AbilityType.LASER,
			ServerItem.Id.LASER
		},
		{
			AbilityType.DRILL,
			ServerItem.Id.DRILL
		},
		{
			AbilityType.ASTEROID,
			ServerItem.Id.ASTEROID
		},
		{
			AbilityType.RING_BONUS,
			ServerItem.Id.RING_BONUS
		},
		{
			AbilityType.DISTANCE_BONUS,
			ServerItem.Id.DISTANCE_BONUS
		},
		{
			AbilityType.TRAMPOLINE,
			ServerItem.Id.TRAMPOLINE
		},
		{
			AbilityType.ANIMAL,
			ServerItem.Id.ANIMAL_BONUS
		},
		{
			AbilityType.COMBO,
			ServerItem.Id.COMBO
		},
		{
			AbilityType.MAGNET,
			ServerItem.Id.MAGNET
		},
		{
			AbilityType.INVINCIBLE,
			ServerItem.Id.INVINCIBLE
		}
	};

	// Token: 0x04002CBF RID: 11455
	private static Dictionary<ServerItem.Id, AbilityType> ServerIdToAbility = new Dictionary<ServerItem.Id, AbilityType>
	{
		{
			ServerItem.Id.LASER,
			AbilityType.LASER
		},
		{
			ServerItem.Id.DRILL,
			AbilityType.DRILL
		},
		{
			ServerItem.Id.ASTEROID,
			AbilityType.ASTEROID
		},
		{
			ServerItem.Id.RING_BONUS,
			AbilityType.RING_BONUS
		},
		{
			ServerItem.Id.DISTANCE_BONUS,
			AbilityType.DISTANCE_BONUS
		},
		{
			ServerItem.Id.TRAMPOLINE,
			AbilityType.TRAMPOLINE
		},
		{
			ServerItem.Id.ANIMAL_BONUS,
			AbilityType.ANIMAL
		},
		{
			ServerItem.Id.COMBO,
			AbilityType.COMBO
		},
		{
			ServerItem.Id.MAGNET,
			AbilityType.MAGNET
		},
		{
			ServerItem.Id.INVINCIBLE,
			AbilityType.INVINCIBLE
		}
	};

	// Token: 0x04002CC0 RID: 11456
	private static int[] s_chaoIdTable;

	// Token: 0x04002CC1 RID: 11457
	private static Dictionary<ServerItem.IdType, ServerItem[]> s_dicServerItemTable = new Dictionary<ServerItem.IdType, ServerItem[]>();

	// Token: 0x04002CC2 RID: 11458
	private ServerItem.Id m_id;

	// Token: 0x04002CC3 RID: 11459
	private static Dictionary<ServerItem.Id, RewardType> s_dic_ServerItemId_to_RewardType = new Dictionary<ServerItem.Id, RewardType>
	{
		{
			ServerItem.Id.INVINCIBLE,
			RewardType.ITEM_INVINCIBLE
		},
		{
			ServerItem.Id.BARRIER,
			RewardType.ITEM_BARRIER
		},
		{
			ServerItem.Id.MAGNET,
			RewardType.ITEM_MAGNET
		},
		{
			ServerItem.Id.TRAMPOLINE,
			RewardType.ITEM_TRAMPOLINE
		},
		{
			ServerItem.Id.COMBO,
			RewardType.ITEM_COMBO
		},
		{
			ServerItem.Id.LASER,
			RewardType.ITEM_LASER
		},
		{
			ServerItem.Id.DRILL,
			RewardType.ITEM_DRILL
		},
		{
			ServerItem.Id.ASTEROID,
			RewardType.ITEM_ASTEROID
		},
		{
			ServerItem.Id.RING,
			RewardType.RING
		},
		{
			ServerItem.Id.RSRING,
			RewardType.RSRING
		},
		{
			ServerItem.Id.ENERGY,
			RewardType.ENERGY
		}
	};

	// Token: 0x020007D3 RID: 2003
	public enum Id
	{
		// Token: 0x04002CC5 RID: 11461
		NONE = -1,
		// Token: 0x04002CC6 RID: 11462
		BOOST_SCORE = 110000,
		// Token: 0x04002CC7 RID: 11463
		BOOST_TRAMPOLINE,
		// Token: 0x04002CC8 RID: 11464
		BOOST_SUBCHARA,
		// Token: 0x04002CC9 RID: 11465
		INVINCIBLE = 120000,
		// Token: 0x04002CCA RID: 11466
		BARRIER,
		// Token: 0x04002CCB RID: 11467
		MAGNET,
		// Token: 0x04002CCC RID: 11468
		TRAMPOLINE,
		// Token: 0x04002CCD RID: 11469
		COMBO,
		// Token: 0x04002CCE RID: 11470
		LASER,
		// Token: 0x04002CCF RID: 11471
		DRILL,
		// Token: 0x04002CD0 RID: 11472
		ASTEROID,
		// Token: 0x04002CD1 RID: 11473
		RING_BONUS,
		// Token: 0x04002CD2 RID: 11474
		DISTANCE_BONUS,
		// Token: 0x04002CD3 RID: 11475
		ANIMAL_BONUS,
		// Token: 0x04002CD4 RID: 11476
		PACKED_INVINCIBLE_0 = 120100,
		// Token: 0x04002CD5 RID: 11477
		PACKED_BARRIER_0,
		// Token: 0x04002CD6 RID: 11478
		PACKED_MAGNET_0,
		// Token: 0x04002CD7 RID: 11479
		PACKED_TRAMPOLINE_0,
		// Token: 0x04002CD8 RID: 11480
		PACKED_COMBO_0,
		// Token: 0x04002CD9 RID: 11481
		PACKED_LASER_0,
		// Token: 0x04002CDA RID: 11482
		PACKED_DRILL_0,
		// Token: 0x04002CDB RID: 11483
		PACKED_ASTEROID_0,
		// Token: 0x04002CDC RID: 11484
		PACKED_RING_BONUS_0,
		// Token: 0x04002CDD RID: 11485
		PACKED_SCORE_BONUS_0,
		// Token: 0x04002CDE RID: 11486
		PACKED_ANIMAL_BONUS_0,
		// Token: 0x04002CDF RID: 11487
		PACKED_INVINCIBLE_1 = 121000,
		// Token: 0x04002CE0 RID: 11488
		PACKED_BARRIER_1,
		// Token: 0x04002CE1 RID: 11489
		PACKED_MAGNET_1,
		// Token: 0x04002CE2 RID: 11490
		PACKED_TRAMPOLINE_1,
		// Token: 0x04002CE3 RID: 11491
		PACKED_COMBO_1,
		// Token: 0x04002CE4 RID: 11492
		PACKED_LASER_1,
		// Token: 0x04002CE5 RID: 11493
		PACKED_DRILL_1,
		// Token: 0x04002CE6 RID: 11494
		PACKED_ASTEROID_1,
		// Token: 0x04002CE7 RID: 11495
		PACKED_RING_BONUS_1,
		// Token: 0x04002CE8 RID: 11496
		PACKED_SCORE_BONUS_1,
		// Token: 0x04002CE9 RID: 11497
		PACKED_ANIMAL_BONUS_1,
		// Token: 0x04002CEA RID: 11498
		BIG = 200000,
		// Token: 0x04002CEB RID: 11499
		SUPER,
		// Token: 0x04002CEC RID: 11500
		JACKPOT,
		// Token: 0x04002CED RID: 11501
		ROULLETE_TOKEN = 210000,
		// Token: 0x04002CEE RID: 11502
		SPECIAL_EGG = 220000,
		// Token: 0x04002CEF RID: 11503
		ROULLETE_TICKET_BEGIN = 229999,
		// Token: 0x04002CF0 RID: 11504
		ROULLETE_TICKET_PREMIAM,
		// Token: 0x04002CF1 RID: 11505
		ROULLETE_TICKET_ITEM = 240000,
		// Token: 0x04002CF2 RID: 11506
		ROULLETE_TICKET_RAID = 250000,
		// Token: 0x04002CF3 RID: 11507
		ROULLETE_TICKET_EVENT = 260000,
		// Token: 0x04002CF4 RID: 11508
		ROULLETE_TICKET_END = 299999,
		// Token: 0x04002CF5 RID: 11509
		CHARA_BEGIN,
		// Token: 0x04002CF6 RID: 11510
		CHAO_BEGIN = 400000,
		// Token: 0x04002CF7 RID: 11511
		CHAO_BEGIN_RARE = 401000,
		// Token: 0x04002CF8 RID: 11512
		CHAO_BEGIN_SRARE = 402000,
		// Token: 0x04002CF9 RID: 11513
		RSRING = 900000,
		// Token: 0x04002CFA RID: 11514
		RSRING_0 = 900010,
		// Token: 0x04002CFB RID: 11515
		RSRING_1 = 900030,
		// Token: 0x04002CFC RID: 11516
		RSRING_2 = 900060,
		// Token: 0x04002CFD RID: 11517
		RSRING_3 = 900210,
		// Token: 0x04002CFE RID: 11518
		RSRING_4 = 900380,
		// Token: 0x04002CFF RID: 11519
		RING = 910000,
		// Token: 0x04002D00 RID: 11520
		RING_0 = 910021,
		// Token: 0x04002D01 RID: 11521
		RING_1 = 910045,
		// Token: 0x04002D02 RID: 11522
		RING_2 = 910094,
		// Token: 0x04002D03 RID: 11523
		RING_3 = 910147,
		// Token: 0x04002D04 RID: 11524
		RING_4 = 910204,
		// Token: 0x04002D05 RID: 11525
		RING_5 = 910265,
		// Token: 0x04002D06 RID: 11526
		ENERGY = 920000,
		// Token: 0x04002D07 RID: 11527
		ENERGY_0,
		// Token: 0x04002D08 RID: 11528
		ENERGY_1 = 920005,
		// Token: 0x04002D09 RID: 11529
		ENERGY_2 = 920010,
		// Token: 0x04002D0A RID: 11530
		ENERGY_3 = 920015,
		// Token: 0x04002D0B RID: 11531
		ENERGY_4 = 920020,
		// Token: 0x04002D0C RID: 11532
		ENERGY_5 = 920025,
		// Token: 0x04002D0D RID: 11533
		ENERGY_MAX = 930000,
		// Token: 0x04002D0E RID: 11534
		SUB_CHARA = 940000,
		// Token: 0x04002D0F RID: 11535
		CONTINUE = 950000,
		// Token: 0x04002D10 RID: 11536
		RAIDRING = 960000,
		// Token: 0x04002D11 RID: 11537
		DAILY_BATTLE_RESET_0 = 980000,
		// Token: 0x04002D12 RID: 11538
		DAILY_BATTLE_RESET_1,
		// Token: 0x04002D13 RID: 11539
		DAILY_BATTLE_RESET_2
	}

	// Token: 0x020007D4 RID: 2004
	public enum IdType
	{
		// Token: 0x04002D15 RID: 11541
		NONE = -1,
		// Token: 0x04002D16 RID: 11542
		BOOST_ITEM = 11,
		// Token: 0x04002D17 RID: 11543
		EQUIP_ITEM,
		// Token: 0x04002D18 RID: 11544
		ITEM_ROULLETE_WIN = 20,
		// Token: 0x04002D19 RID: 11545
		ROULLETE_TOKEN,
		// Token: 0x04002D1A RID: 11546
		EGG_ITEM,
		// Token: 0x04002D1B RID: 11547
		PREMIUM_ROULLETE_TICKET,
		// Token: 0x04002D1C RID: 11548
		ITEM_ROULLETE_TICKET,
		// Token: 0x04002D1D RID: 11549
		CHARA = 30,
		// Token: 0x04002D1E RID: 11550
		CHAO = 40,
		// Token: 0x04002D1F RID: 11551
		RSRING = 90,
		// Token: 0x04002D20 RID: 11552
		RING,
		// Token: 0x04002D21 RID: 11553
		ENERGY,
		// Token: 0x04002D22 RID: 11554
		ENERGY_MAX,
		// Token: 0x04002D23 RID: 11555
		RAIDRING = 96
	}
}
