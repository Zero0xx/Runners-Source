using System;
using System.Collections.Generic;

// Token: 0x02000518 RID: 1304
public class ServerWheelOptionsData
{
	// Token: 0x060027CB RID: 10187 RVA: 0x000F7CA8 File Offset: 0x000F5EA8
	public ServerWheelOptionsData(ServerWheelOptionsData data)
	{
		if (data.GetOrgGeneralData() != null)
		{
			this.m_wheelOption = new ServerWheelOptionsOrgGen(data.GetOrgGeneralData());
		}
		else if (data.GetOrgNormalData() != null)
		{
			this.m_wheelOption = new ServerWheelOptionsNormal(data.GetOrgNormalData());
		}
		else if (data.GetOrgRankupData() != null)
		{
			this.m_wheelOption = new ServerWheelOptionsRankup(data.GetOrgRankupData());
		}
	}

	// Token: 0x060027CC RID: 10188 RVA: 0x000F7D1C File Offset: 0x000F5F1C
	public ServerWheelOptionsData(ServerChaoWheelOptions data)
	{
		this.m_wheelOption = new ServerWheelOptionsNormal(data);
	}

	// Token: 0x060027CD RID: 10189 RVA: 0x000F7D30 File Offset: 0x000F5F30
	public ServerWheelOptionsData(ServerWheelOptions data)
	{
		this.m_wheelOption = new ServerWheelOptionsRankup(data);
	}

	// Token: 0x060027CE RID: 10190 RVA: 0x000F7D44 File Offset: 0x000F5F44
	public ServerWheelOptionsData(ServerWheelOptionsGeneral data)
	{
		this.m_wheelOption = new ServerWheelOptionsOrgGen(data);
	}

	// Token: 0x1700053D RID: 1341
	// (get) Token: 0x060027CF RID: 10191 RVA: 0x000F7D58 File Offset: 0x000F5F58
	public RouletteUtility.WheelType wheelType
	{
		get
		{
			return this.m_wheelOption.wheelType;
		}
	}

	// Token: 0x1700053E RID: 1342
	// (get) Token: 0x060027D0 RID: 10192 RVA: 0x000F7D68 File Offset: 0x000F5F68
	public ServerWheelOptionsData.DATA_TYPE dataType
	{
		get
		{
			if (this.m_dataType == ServerWheelOptionsData.DATA_TYPE.NONE && this.m_wheelOption != null)
			{
				if (this.m_wheelOption.GetOrgGeneralData() != null)
				{
					this.m_dataType = ServerWheelOptionsData.DATA_TYPE.GENERAL;
				}
				else if (this.m_wheelOption.GetOrgNormalData() != null)
				{
					this.m_dataType = ServerWheelOptionsData.DATA_TYPE.NORMAL;
				}
				else if (this.m_wheelOption.GetOrgRankupData() != null)
				{
					this.m_dataType = ServerWheelOptionsData.DATA_TYPE.RANKUP;
				}
			}
			return this.m_dataType;
		}
	}

	// Token: 0x1700053F RID: 1343
	// (get) Token: 0x060027D1 RID: 10193 RVA: 0x000F7DE0 File Offset: 0x000F5FE0
	public RouletteCategory category
	{
		get
		{
			return this.m_wheelOption.category;
		}
	}

	// Token: 0x17000540 RID: 1344
	// (get) Token: 0x060027D2 RID: 10194 RVA: 0x000F7DF0 File Offset: 0x000F5FF0
	public bool isValid
	{
		get
		{
			return this.m_wheelOption.isValid;
		}
	}

	// Token: 0x17000541 RID: 1345
	// (get) Token: 0x060027D3 RID: 10195 RVA: 0x000F7E00 File Offset: 0x000F6000
	public bool isRemainingRefresh
	{
		get
		{
			return this.m_wheelOption.isRemainingRefresh;
		}
	}

	// Token: 0x17000542 RID: 1346
	// (get) Token: 0x060027D4 RID: 10196 RVA: 0x000F7E10 File Offset: 0x000F6010
	public int itemWon
	{
		get
		{
			return this.m_wheelOption.itemWon;
		}
	}

	// Token: 0x17000543 RID: 1347
	// (get) Token: 0x060027D5 RID: 10197 RVA: 0x000F7E20 File Offset: 0x000F6020
	public ServerItem itemWonData
	{
		get
		{
			return this.m_wheelOption.itemWonData;
		}
	}

	// Token: 0x17000544 RID: 1348
	// (get) Token: 0x060027D6 RID: 10198 RVA: 0x000F7E30 File Offset: 0x000F6030
	public int rouletteId
	{
		get
		{
			return this.m_wheelOption.rouletteId;
		}
	}

	// Token: 0x17000545 RID: 1349
	// (get) Token: 0x060027D7 RID: 10199 RVA: 0x000F7E40 File Offset: 0x000F6040
	public int multi
	{
		get
		{
			return this.m_wheelOption.multi;
		}
	}

	// Token: 0x17000546 RID: 1350
	// (get) Token: 0x060027D8 RID: 10200 RVA: 0x000F7E50 File Offset: 0x000F6050
	public bool isGeneral
	{
		get
		{
			return this.m_wheelOption.GetOrgGeneralData() != null;
		}
	}

	// Token: 0x17000547 RID: 1351
	// (get) Token: 0x060027D9 RID: 10201 RVA: 0x000F7E64 File Offset: 0x000F6064
	public int numJackpotRing
	{
		get
		{
			return this.m_wheelOption.numJackpotRing;
		}
	}

	// Token: 0x060027DA RID: 10202 RVA: 0x000F7E74 File Offset: 0x000F6074
	public void Setup(ServerChaoWheelOptions data)
	{
		this.m_wheelOption.Setup(data);
	}

	// Token: 0x060027DB RID: 10203 RVA: 0x000F7E84 File Offset: 0x000F6084
	public void Setup(ServerWheelOptions data)
	{
		this.m_wheelOption.Setup(data);
	}

	// Token: 0x060027DC RID: 10204 RVA: 0x000F7E94 File Offset: 0x000F6094
	public void Setup(ServerWheelOptionsGeneral data)
	{
		this.m_wheelOption.Setup(data);
	}

	// Token: 0x060027DD RID: 10205 RVA: 0x000F7EA4 File Offset: 0x000F60A4
	public bool ChangeMulti(int multi)
	{
		return this.m_wheelOption.ChangeMulti(multi);
	}

	// Token: 0x060027DE RID: 10206 RVA: 0x000F7EB4 File Offset: 0x000F60B4
	public bool IsMulti(int multi)
	{
		return this.m_wheelOption.IsMulti(multi);
	}

	// Token: 0x060027DF RID: 10207 RVA: 0x000F7EC4 File Offset: 0x000F60C4
	public int GetRouletteBoardPattern()
	{
		if (this.m_wheelOption == null)
		{
			return 0;
		}
		return this.m_wheelOption.GetRouletteBoardPattern();
	}

	// Token: 0x060027E0 RID: 10208 RVA: 0x000F7EE0 File Offset: 0x000F60E0
	public string GetRouletteArrowSprite()
	{
		if (this.m_wheelOption == null)
		{
			return null;
		}
		return this.m_wheelOption.GetRouletteArrowSprite();
	}

	// Token: 0x060027E1 RID: 10209 RVA: 0x000F7EFC File Offset: 0x000F60FC
	public string GetRouletteBgSprite()
	{
		if (this.m_wheelOption == null)
		{
			return null;
		}
		return this.m_wheelOption.GetRouletteBgSprite();
	}

	// Token: 0x060027E2 RID: 10210 RVA: 0x000F7F18 File Offset: 0x000F6118
	public string GetRouletteBoardSprite()
	{
		if (this.m_wheelOption == null)
		{
			return null;
		}
		return this.m_wheelOption.GetRouletteBoardSprite();
	}

	// Token: 0x060027E3 RID: 10211 RVA: 0x000F7F34 File Offset: 0x000F6134
	public string GetRouletteTicketSprite()
	{
		if (this.m_wheelOption == null)
		{
			return null;
		}
		return this.m_wheelOption.GetRouletteTicketSprite();
	}

	// Token: 0x060027E4 RID: 10212 RVA: 0x000F7F50 File Offset: 0x000F6150
	public RouletteUtility.WheelRank GetRouletteRank()
	{
		if (this.m_wheelOption == null)
		{
			return RouletteUtility.WheelRank.Normal;
		}
		return this.m_wheelOption.GetRouletteRank();
	}

	// Token: 0x060027E5 RID: 10213 RVA: 0x000F7F6C File Offset: 0x000F616C
	public int GetCellEgg(int cellIndex)
	{
		if (this.m_wheelOption == null)
		{
			return 0;
		}
		return this.m_wheelOption.GetCellEgg(cellIndex);
	}

	// Token: 0x060027E6 RID: 10214 RVA: 0x000F7F88 File Offset: 0x000F6188
	public ServerItem GetCellItem(int cellIndex, out int num)
	{
		if (this.m_wheelOption == null)
		{
			num = 0;
			return default(ServerItem);
		}
		return this.m_wheelOption.GetCellItem(cellIndex, out num);
	}

	// Token: 0x060027E7 RID: 10215 RVA: 0x000F7FBC File Offset: 0x000F61BC
	public ServerItem GetCellItem(int cellIndex)
	{
		if (this.m_wheelOption == null)
		{
			return default(ServerItem);
		}
		int num = 0;
		return this.m_wheelOption.GetCellItem(cellIndex, out num);
	}

	// Token: 0x060027E8 RID: 10216 RVA: 0x000F7FF0 File Offset: 0x000F61F0
	public float GetCellWeight(int cellIndex)
	{
		if (this.m_wheelOption == null)
		{
			return 0f;
		}
		return this.m_wheelOption.GetCellWeight(cellIndex);
	}

	// Token: 0x060027E9 RID: 10217 RVA: 0x000F8010 File Offset: 0x000F6210
	public void PlayBgm(float delay = 0f)
	{
		if (this.m_wheelOption == null)
		{
			return;
		}
		this.m_wheelOption.PlayBgm(delay);
	}

	// Token: 0x060027EA RID: 10218 RVA: 0x000F802C File Offset: 0x000F622C
	public void PlaySe(ServerWheelOptionsData.SE_TYPE seType, float delay = 0f)
	{
		if (this.m_wheelOption == null)
		{
			return;
		}
		this.m_wheelOption.PlaySe(seType, delay);
	}

	// Token: 0x060027EB RID: 10219 RVA: 0x000F8048 File Offset: 0x000F6248
	public ServerWheelOptionsData.SPIN_BUTTON GetSpinButtonSeting(out int count, out bool btnActive)
	{
		if (this.m_wheelOption == null)
		{
			count = 0;
			btnActive = false;
			return ServerWheelOptionsData.SPIN_BUTTON.NONE;
		}
		return this.m_wheelOption.GetSpinButtonSeting(out count, out btnActive);
	}

	// Token: 0x060027EC RID: 10220 RVA: 0x000F8078 File Offset: 0x000F6278
	public ServerWheelOptionsData.SPIN_BUTTON GetSpinButtonSeting()
	{
		if (this.m_wheelOption == null)
		{
			return ServerWheelOptionsData.SPIN_BUTTON.NONE;
		}
		return this.m_wheelOption.GetSpinButtonSeting();
	}

	// Token: 0x060027ED RID: 10221 RVA: 0x000F8094 File Offset: 0x000F6294
	public int GetSpinCostItemId()
	{
		if (this.m_wheelOption == null)
		{
			return 0;
		}
		return this.m_wheelOption.GetSpinCostItemId();
	}

	// Token: 0x060027EE RID: 10222 RVA: 0x000F80B0 File Offset: 0x000F62B0
	public List<int> GetSpinCostItemIdList()
	{
		if (this.m_wheelOption == null)
		{
			return null;
		}
		return this.m_wheelOption.GetSpinCostItemIdList();
	}

	// Token: 0x060027EF RID: 10223 RVA: 0x000F80CC File Offset: 0x000F62CC
	public int GetSpinCostItemNum(int costItemId)
	{
		if (this.m_wheelOption == null)
		{
			return 0;
		}
		return this.m_wheelOption.GetSpinCostItemNum(costItemId);
	}

	// Token: 0x060027F0 RID: 10224 RVA: 0x000F80E8 File Offset: 0x000F62E8
	public int GetSpinCostItemCost(int costItemId)
	{
		if (this.m_wheelOption == null)
		{
			return 0;
		}
		return this.m_wheelOption.GetSpinCostItemCost(costItemId);
	}

	// Token: 0x060027F1 RID: 10225 RVA: 0x000F8104 File Offset: 0x000F6304
	public bool ChangeSpinCost(int selectIndex)
	{
		return this.m_wheelOption != null && this.m_wheelOption.ChangeSpinCost(selectIndex);
	}

	// Token: 0x060027F2 RID: 10226 RVA: 0x000F8120 File Offset: 0x000F6320
	public int GetCurrentSpinCostIndex()
	{
		if (this.m_wheelOption == null)
		{
			return 0;
		}
		return this.m_wheelOption.GetSpinCostCurrentIndex();
	}

	// Token: 0x060027F3 RID: 10227 RVA: 0x000F813C File Offset: 0x000F633C
	public bool GetEggSeting(out int count)
	{
		if (this.m_wheelOption == null)
		{
			count = 0;
			return false;
		}
		return this.m_wheelOption.GetEggSeting(out count);
	}

	// Token: 0x060027F4 RID: 10228 RVA: 0x000F815C File Offset: 0x000F635C
	public ServerWheelOptions GetOrgRankupData()
	{
		if (this.m_wheelOption == null)
		{
			return null;
		}
		return this.m_wheelOption.GetOrgRankupData();
	}

	// Token: 0x060027F5 RID: 10229 RVA: 0x000F8178 File Offset: 0x000F6378
	public ServerChaoWheelOptions GetOrgNormalData()
	{
		if (this.m_wheelOption == null)
		{
			return null;
		}
		return this.m_wheelOption.GetOrgNormalData();
	}

	// Token: 0x060027F6 RID: 10230 RVA: 0x000F8194 File Offset: 0x000F6394
	public ServerWheelOptionsGeneral GetOrgGeneralData()
	{
		if (this.m_wheelOption == null)
		{
			return null;
		}
		return this.m_wheelOption.GetOrgGeneralData();
	}

	// Token: 0x060027F7 RID: 10231 RVA: 0x000F81B0 File Offset: 0x000F63B0
	public List<Constants.Campaign.emType> GetCampaign()
	{
		if (this.m_wheelOption == null)
		{
			return null;
		}
		return this.m_wheelOption.GetCampaign();
	}

	// Token: 0x060027F8 RID: 10232 RVA: 0x000F81CC File Offset: 0x000F63CC
	public bool IsCampaign()
	{
		return this.m_wheelOption != null && this.m_wheelOption.IsCampaign();
	}

	// Token: 0x060027F9 RID: 10233 RVA: 0x000F81E8 File Offset: 0x000F63E8
	public bool IsCampaign(Constants.Campaign.emType campaign)
	{
		return this.m_wheelOption != null && this.m_wheelOption.IsCampaign(campaign);
	}

	// Token: 0x060027FA RID: 10234 RVA: 0x000F8204 File Offset: 0x000F6404
	public List<string[]> GetItemOdds()
	{
		if (this.m_wheelOption == null)
		{
			return null;
		}
		return this.m_wheelOption.GetItemOdds();
	}

	// Token: 0x060027FB RID: 10235 RVA: 0x000F8220 File Offset: 0x000F6420
	public string ShowSpinErrorWindow()
	{
		if (this.m_wheelOption == null)
		{
			return null;
		}
		return this.m_wheelOption.ShowSpinErrorWindow();
	}

	// Token: 0x060027FC RID: 10236 RVA: 0x000F823C File Offset: 0x000F643C
	public List<ServerItem> GetAttentionItemList()
	{
		if (this.m_wheelOption == null)
		{
			return null;
		}
		return this.m_wheelOption.GetAttentionItemList();
	}

	// Token: 0x060027FD RID: 10237 RVA: 0x000F8258 File Offset: 0x000F6458
	public bool IsPrizeDataList()
	{
		return this.m_wheelOption != null && this.m_wheelOption.IsPrizeDataList();
	}

	// Token: 0x060027FE RID: 10238 RVA: 0x000F8274 File Offset: 0x000F6474
	public void CopyTo(ServerWheelOptionsData to)
	{
		to.Setup(this.m_wheelOption.GetOrgGeneralData());
		to.Setup(this.m_wheelOption.GetOrgNormalData());
		to.Setup(this.m_wheelOption.GetOrgRankupData());
	}

	// Token: 0x040023E8 RID: 9192
	private ServerWheelOptionsData.DATA_TYPE m_dataType;

	// Token: 0x040023E9 RID: 9193
	private ServerWheelOptionsOrg m_wheelOption;

	// Token: 0x02000519 RID: 1305
	public enum DATA_TYPE
	{
		// Token: 0x040023EB RID: 9195
		NONE,
		// Token: 0x040023EC RID: 9196
		NORMAL,
		// Token: 0x040023ED RID: 9197
		RANKUP,
		// Token: 0x040023EE RID: 9198
		GENERAL
	}

	// Token: 0x0200051A RID: 1306
	public enum SPIN_BUTTON
	{
		// Token: 0x040023F0 RID: 9200
		FREE,
		// Token: 0x040023F1 RID: 9201
		RING,
		// Token: 0x040023F2 RID: 9202
		RSRING,
		// Token: 0x040023F3 RID: 9203
		TICKET,
		// Token: 0x040023F4 RID: 9204
		RAID,
		// Token: 0x040023F5 RID: 9205
		NONE
	}

	// Token: 0x0200051B RID: 1307
	public enum SE_TYPE
	{
		// Token: 0x040023F7 RID: 9207
		NONE,
		// Token: 0x040023F8 RID: 9208
		Open,
		// Token: 0x040023F9 RID: 9209
		Close,
		// Token: 0x040023FA RID: 9210
		Click,
		// Token: 0x040023FB RID: 9211
		Spin,
		// Token: 0x040023FC RID: 9212
		SpinError,
		// Token: 0x040023FD RID: 9213
		Arrow,
		// Token: 0x040023FE RID: 9214
		Skip,
		// Token: 0x040023FF RID: 9215
		GetItem,
		// Token: 0x04002400 RID: 9216
		GetRare,
		// Token: 0x04002401 RID: 9217
		GetRankup,
		// Token: 0x04002402 RID: 9218
		GetJackpot,
		// Token: 0x04002403 RID: 9219
		Multi,
		// Token: 0x04002404 RID: 9220
		Change
	}
}
