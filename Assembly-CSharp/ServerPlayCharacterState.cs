using System;
using System.Collections.Generic;

// Token: 0x02000814 RID: 2068
public class ServerPlayCharacterState
{
	// Token: 0x06003771 RID: 14193 RVA: 0x00124480 File Offset: 0x00122680
	public ServerPlayCharacterState()
	{
		this.Id = -1;
		this.Status = ServerPlayCharacterState.CharacterStatus.Locked;
		this.Level = 10;
		this.Cost = 0;
		this.star = 0;
		this.starMax = 0;
		this.priceNumRings = 0;
		this.priceNumRedRings = 0;
	}

	// Token: 0x1700082B RID: 2091
	// (get) Token: 0x06003772 RID: 14194 RVA: 0x001244F8 File Offset: 0x001226F8
	// (set) Token: 0x06003773 RID: 14195 RVA: 0x00124500 File Offset: 0x00122700
	public int Id { get; set; }

	// Token: 0x1700082C RID: 2092
	// (get) Token: 0x06003774 RID: 14196 RVA: 0x0012450C File Offset: 0x0012270C
	// (set) Token: 0x06003775 RID: 14197 RVA: 0x00124514 File Offset: 0x00122714
	public ServerPlayCharacterState.CharacterStatus Status { get; set; }

	// Token: 0x1700082D RID: 2093
	// (get) Token: 0x06003776 RID: 14198 RVA: 0x00124520 File Offset: 0x00122720
	// (set) Token: 0x06003777 RID: 14199 RVA: 0x00124528 File Offset: 0x00122728
	public int Level { get; set; }

	// Token: 0x1700082E RID: 2094
	// (get) Token: 0x06003778 RID: 14200 RVA: 0x00124534 File Offset: 0x00122734
	// (set) Token: 0x06003779 RID: 14201 RVA: 0x0012453C File Offset: 0x0012273C
	public int Cost { get; set; }

	// Token: 0x1700082F RID: 2095
	// (get) Token: 0x0600377A RID: 14202 RVA: 0x00124548 File Offset: 0x00122748
	// (set) Token: 0x0600377B RID: 14203 RVA: 0x00124550 File Offset: 0x00122750
	public int NumRedRings { get; set; }

	// Token: 0x17000830 RID: 2096
	// (get) Token: 0x0600377C RID: 14204 RVA: 0x0012455C File Offset: 0x0012275C
	// (set) Token: 0x0600377D RID: 14205 RVA: 0x00124564 File Offset: 0x00122764
	public ServerPlayCharacterState.LockCondition Condition { get; set; }

	// Token: 0x17000831 RID: 2097
	// (get) Token: 0x0600377E RID: 14206 RVA: 0x00124570 File Offset: 0x00122770
	// (set) Token: 0x0600377F RID: 14207 RVA: 0x00124578 File Offset: 0x00122778
	public int Exp { get; set; }

	// Token: 0x17000832 RID: 2098
	// (get) Token: 0x06003780 RID: 14208 RVA: 0x00124584 File Offset: 0x00122784
	// (set) Token: 0x06003781 RID: 14209 RVA: 0x0012458C File Offset: 0x0012278C
	public int star { get; set; }

	// Token: 0x17000833 RID: 2099
	// (get) Token: 0x06003782 RID: 14210 RVA: 0x00124598 File Offset: 0x00122798
	// (set) Token: 0x06003783 RID: 14211 RVA: 0x001245A0 File Offset: 0x001227A0
	public int starMax { get; set; }

	// Token: 0x17000834 RID: 2100
	// (get) Token: 0x06003784 RID: 14212 RVA: 0x001245AC File Offset: 0x001227AC
	// (set) Token: 0x06003785 RID: 14213 RVA: 0x001245B4 File Offset: 0x001227B4
	public int priceNumRings { get; set; }

	// Token: 0x17000835 RID: 2101
	// (get) Token: 0x06003786 RID: 14214 RVA: 0x001245C0 File Offset: 0x001227C0
	// (set) Token: 0x06003787 RID: 14215 RVA: 0x001245C8 File Offset: 0x001227C8
	public int priceNumRedRings { get; set; }

	// Token: 0x17000836 RID: 2102
	// (get) Token: 0x06003788 RID: 14216 RVA: 0x001245D4 File Offset: 0x001227D4
	public int UnlockCost
	{
		get
		{
			if (this.Status == ServerPlayCharacterState.CharacterStatus.Locked)
			{
				return this.Cost;
			}
			return -1;
		}
	}

	// Token: 0x17000837 RID: 2103
	// (get) Token: 0x06003789 RID: 14217 RVA: 0x001245EC File Offset: 0x001227EC
	public int LevelUpCost
	{
		get
		{
			if (this.Status == ServerPlayCharacterState.CharacterStatus.Unlocked)
			{
				return this.Cost;
			}
			return -1;
		}
	}

	// Token: 0x17000838 RID: 2104
	// (get) Token: 0x0600378A RID: 14218 RVA: 0x00124604 File Offset: 0x00122804
	public bool IsUnlocked
	{
		get
		{
			return ServerPlayCharacterState.CharacterStatus.Locked != this.Status;
		}
	}

	// Token: 0x17000839 RID: 2105
	// (get) Token: 0x0600378B RID: 14219 RVA: 0x00124614 File Offset: 0x00122814
	public bool IsMaxLevel
	{
		get
		{
			return ServerPlayCharacterState.CharacterStatus.MaxLevel == this.Status;
		}
	}

	// Token: 0x1700083A RID: 2106
	// (get) Token: 0x0600378C RID: 14220 RVA: 0x00124620 File Offset: 0x00122820
	public float QuickModeTimeExtension
	{
		get
		{
			float result = 0f;
			if (this.starMax > 0)
			{
				StageTimeTable stageTimeTable = GameObjectUtil.FindGameObjectComponent<StageTimeTable>("StageTimeTable");
				if (stageTimeTable != null)
				{
					float num = (float)stageTimeTable.GetTableItemData(StageTimeTableItem.OverlapBonus);
					result = (float)this.star * num;
				}
			}
			return result;
		}
	}

	// Token: 0x0600378D RID: 14221 RVA: 0x0012466C File Offset: 0x0012286C
	public static ServerCharacterState CreateCharacterState(ServerPlayCharacterState playCharaState)
	{
		if (playCharaState == null)
		{
			return null;
		}
		ServerCharacterState serverCharacterState = new ServerCharacterState();
		serverCharacterState.Id = playCharaState.Id;
		serverCharacterState.Status = (ServerCharacterState.CharacterStatus)playCharaState.Status;
		serverCharacterState.Level = playCharaState.Level;
		serverCharacterState.Cost = playCharaState.Cost;
		serverCharacterState.NumRedRings = playCharaState.NumRedRings;
		serverCharacterState.star = playCharaState.star;
		serverCharacterState.starMax = playCharaState.starMax;
		serverCharacterState.priceNumRings = playCharaState.priceNumRings;
		serverCharacterState.priceNumRedRings = playCharaState.priceNumRedRings;
		foreach (int item in playCharaState.AbilityLevel)
		{
			serverCharacterState.AbilityLevel.Add(item);
		}
		foreach (int item2 in playCharaState.AbilityNumRings)
		{
			serverCharacterState.AbilityNumRings.Add(item2);
		}
		serverCharacterState.Condition = (ServerCharacterState.LockCondition)playCharaState.Condition;
		serverCharacterState.Exp = playCharaState.Exp;
		return serverCharacterState;
	}

	// Token: 0x0600378E RID: 14222 RVA: 0x001247C8 File Offset: 0x001229C8
	public static ServerPlayCharacterState CreatePlayCharacterState(ServerCharacterState charaState)
	{
		if (charaState == null)
		{
			return null;
		}
		ServerPlayCharacterState serverPlayCharacterState = new ServerPlayCharacterState();
		serverPlayCharacterState.Id = charaState.Id;
		serverPlayCharacterState.Status = serverPlayCharacterState.Status;
		serverPlayCharacterState.Level = charaState.Level;
		serverPlayCharacterState.Cost = charaState.Cost;
		serverPlayCharacterState.NumRedRings = charaState.NumRedRings;
		serverPlayCharacterState.star = charaState.star;
		serverPlayCharacterState.starMax = charaState.starMax;
		serverPlayCharacterState.priceNumRings = charaState.priceNumRings;
		serverPlayCharacterState.priceNumRedRings = charaState.priceNumRedRings;
		foreach (int item in charaState.AbilityLevel)
		{
			serverPlayCharacterState.AbilityLevel.Add(item);
		}
		foreach (int item2 in charaState.AbilityNumRings)
		{
			serverPlayCharacterState.AbilityNumRings.Add(item2);
		}
		serverPlayCharacterState.Condition = (ServerPlayCharacterState.LockCondition)charaState.Condition;
		serverPlayCharacterState.Exp = charaState.Exp;
		return serverPlayCharacterState;
	}

	// Token: 0x0600378F RID: 14223 RVA: 0x00124924 File Offset: 0x00122B24
	public void Dump()
	{
		Debug.Log(string.Concat(new object[]
		{
			"Id=",
			this.Id,
			", Status=",
			this.Status,
			", Level=",
			this.Level,
			", Cost=",
			this.Cost,
			", UnlockCost=",
			this.UnlockCost,
			", LevelUpCost=",
			this.LevelUpCost
		}));
	}

	// Token: 0x04002EC7 RID: 11975
	public List<int> AbilityLevel = new List<int>();

	// Token: 0x04002EC8 RID: 11976
	public List<int> AbilityNumRings = new List<int>();

	// Token: 0x04002EC9 RID: 11977
	public List<int> abilityLevelUp = new List<int>();

	// Token: 0x04002ECA RID: 11978
	public List<int> abilityLevelUpExp = new List<int>();

	// Token: 0x02000815 RID: 2069
	public enum CharacterStatus
	{
		// Token: 0x04002ED7 RID: 11991
		Locked,
		// Token: 0x04002ED8 RID: 11992
		Unlocked,
		// Token: 0x04002ED9 RID: 11993
		MaxLevel
	}

	// Token: 0x02000816 RID: 2070
	public enum LockCondition
	{
		// Token: 0x04002EDB RID: 11995
		OPEN,
		// Token: 0x04002EDC RID: 11996
		MILEAGE_EPISODE,
		// Token: 0x04002EDD RID: 11997
		RING_OR_RED_STAR_RING
	}
}
