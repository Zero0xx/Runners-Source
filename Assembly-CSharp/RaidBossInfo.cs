using System;
using System.Collections.Generic;
using DataTable;
using Text;
using UnityEngine;

// Token: 0x0200022F RID: 559
public class RaidBossInfo : EventBaseInfo
{
	// Token: 0x17000237 RID: 567
	// (get) Token: 0x06000F0C RID: 3852 RVA: 0x000582D0 File Offset: 0x000564D0
	// (set) Token: 0x06000F0D RID: 3853 RVA: 0x000582D8 File Offset: 0x000564D8
	public static RaidBossData currentRaidData
	{
		get
		{
			return RaidBossInfo.m_currentRaidData;
		}
		set
		{
			RaidBossInfo.m_currentRaidData = value;
		}
	}

	// Token: 0x17000238 RID: 568
	// (get) Token: 0x06000F0E RID: 3854 RVA: 0x000582E0 File Offset: 0x000564E0
	public string bossName
	{
		get
		{
			return this.m_bossName;
		}
	}

	// Token: 0x17000239 RID: 569
	// (get) Token: 0x06000F0F RID: 3855 RVA: 0x000582E8 File Offset: 0x000564E8
	// (set) Token: 0x06000F10 RID: 3856 RVA: 0x000582F0 File Offset: 0x000564F0
	public long totalDestroyCount
	{
		get
		{
			return this.m_totalPoint;
		}
		set
		{
			this.m_totalPoint = value;
		}
	}

	// Token: 0x1700023A RID: 570
	// (get) Token: 0x06000F11 RID: 3857 RVA: 0x000582FC File Offset: 0x000564FC
	// (set) Token: 0x06000F12 RID: 3858 RVA: 0x0005830C File Offset: 0x0005650C
	public long raidRing
	{
		get
		{
			return this.m_raidRing + this.m_raidRingOffset;
		}
		set
		{
			this.m_raidRing = value;
			this.m_raidRingOffset = 0L;
		}
	}

	// Token: 0x1700023B RID: 571
	// (get) Token: 0x06000F13 RID: 3859 RVA: 0x00058320 File Offset: 0x00056520
	// (set) Token: 0x06000F14 RID: 3860 RVA: 0x00058328 File Offset: 0x00056528
	public long raidRingOffset
	{
		get
		{
			return this.m_raidRingOffset;
		}
		set
		{
			this.m_raidRingOffset = value;
		}
	}

	// Token: 0x1700023C RID: 572
	// (get) Token: 0x06000F15 RID: 3861 RVA: 0x00058334 File Offset: 0x00056534
	public List<RaidBossData> raidData
	{
		get
		{
			return this.m_raidData;
		}
	}

	// Token: 0x1700023D RID: 573
	// (set) Token: 0x06000F16 RID: 3862 RVA: 0x0005833C File Offset: 0x0005653C
	public RaidBossInfo.CallbackRaidBossInfoUpdate callback
	{
		set
		{
			this.m_callback = value;
		}
	}

	// Token: 0x1700023E RID: 574
	// (get) Token: 0x06000F17 RID: 3863 RVA: 0x00058348 File Offset: 0x00056548
	public int raidNumActive
	{
		get
		{
			if (this.m_raidData == null)
			{
				return 0;
			}
			int num = 0;
			foreach (RaidBossData raidBossData in this.m_raidData)
			{
				if (raidBossData != null && (!raidBossData.end || raidBossData.IsDiscoverer() || !raidBossData.participation))
				{
					num++;
				}
			}
			return num;
		}
	}

	// Token: 0x1700023F RID: 575
	// (get) Token: 0x06000F18 RID: 3864 RVA: 0x000583E4 File Offset: 0x000565E4
	public int raidNumLost
	{
		get
		{
			if (this.m_raidData == null)
			{
				return 0;
			}
			int num = 0;
			foreach (RaidBossData raidBossData in this.m_raidData)
			{
				if (raidBossData != null && raidBossData.end && !raidBossData.IsDiscoverer() && raidBossData.participation)
				{
					num++;
				}
			}
			return num;
		}
	}

	// Token: 0x06000F19 RID: 3865 RVA: 0x00058480 File Offset: 0x00056680
	public override void Init()
	{
		if (this.m_init)
		{
			return;
		}
		this.m_eventName = "RaidBoss(正式なテキストを追加してください)";
		string text = TextUtility.GetText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "Result", "ui_Lbl_word_boss_destroy");
		List<ServerEventReward> rewardList = EventManager.Instance.RewardList;
		this.m_eventMission = new List<EventMission>();
		if (rewardList != null)
		{
			for (int i = 0; i < rewardList.Count; i++)
			{
				ServerEventReward serverEventReward = rewardList[i];
				this.m_eventMission.Add(new EventMission(text, serverEventReward.Param, serverEventReward.m_itemId, serverEventReward.m_num));
			}
		}
		this.m_rewardChao = new List<ChaoData>();
		RewardChaoData rewardChaoData = EventManager.Instance.GetRewardChaoData();
		if (rewardChaoData != null)
		{
			ChaoData chaoData = ChaoTable.GetChaoData(rewardChaoData.chao_id);
			if (chaoData != null)
			{
				this.m_rewardChao.Add(chaoData);
			}
		}
		EyeCatcherChaoData[] eyeCatcherChaoDatas = EventManager.Instance.GetEyeCatcherChaoDatas();
		if (eyeCatcherChaoDatas != null)
		{
			foreach (EyeCatcherChaoData eyeCatcherChaoData in eyeCatcherChaoDatas)
			{
				ChaoData chaoData2 = ChaoTable.GetChaoData(eyeCatcherChaoData.chao_id);
				if (chaoData2 != null)
				{
					this.m_rewardChao.Add(chaoData2);
				}
			}
		}
		int chaoLevel = ChaoTable.ChaoMaxLevel();
		this.m_leftTitle = TextUtility.GetCommonText("Roulette", "ui_Lbl_word_best_chao");
		if (this.m_rewardChao.Count > 0)
		{
			this.m_leftName = this.m_rewardChao[0].nameTwolines;
			this.m_leftText = this.m_rewardChao[0].GetDetailsLevel(chaoLevel);
			switch (this.m_rewardChao[0].rarity)
			{
			case ChaoData.Rarity.NORMAL:
				this.m_leftBg = "ui_tex_chao_bg_0";
				break;
			case ChaoData.Rarity.RARE:
				this.m_leftBg = "ui_tex_chao_bg_1";
				break;
			case ChaoData.Rarity.SRARE:
				this.m_leftBg = "ui_tex_chao_bg_2";
				break;
			}
			switch (this.m_rewardChao[0].charaAtribute)
			{
			case CharacterAttribute.SPEED:
				this.m_chaoTypeIcon = "ui_chao_set_type_icon_speed";
				break;
			case CharacterAttribute.FLY:
				this.m_chaoTypeIcon = "ui_chao_set_type_icon_fly";
				break;
			case CharacterAttribute.POWER:
				this.m_chaoTypeIcon = "ui_chao_set_type_icon_power";
				break;
			}
		}
		this.m_caption = TextUtility.GetCommonText("Event", "ui_Lbl_event_reward_list");
		this.m_rightTitle = TextUtility.GetText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "Menu", "ui_Lbl_word_destroy_total");
		this.m_rightTitleIcon = "ui_event_object_icon";
		this.m_init = true;
	}

	// Token: 0x06000F1A RID: 3866 RVA: 0x000586FC File Offset: 0x000568FC
	public override void UpdateData(MonoBehaviour obj)
	{
		if (!this.m_init)
		{
			this.Init();
		}
		else
		{
			this.m_callback(this);
		}
	}

	// Token: 0x06000F1B RID: 3867 RVA: 0x0005872C File Offset: 0x0005692C
	public bool IsAttention()
	{
		bool result = false;
		if (this.m_raidData != null && this.m_raidData.Count > 0)
		{
			foreach (RaidBossData raidBossData in this.m_raidData)
			{
				if (!raidBossData.end && raidBossData.id != 0L)
				{
					result = true;
					break;
				}
				if (raidBossData.participation)
				{
					result = true;
					break;
				}
			}
		}
		return result;
	}

	// Token: 0x06000F1C RID: 3868 RVA: 0x000587DC File Offset: 0x000569DC
	public static RaidBossInfo CreateData(List<RaidBossData> raidBossDatas)
	{
		RaidBossInfo raidBossInfo = new RaidBossInfo();
		raidBossInfo.Init();
		raidBossInfo.m_raidData = new List<RaidBossData>();
		if (raidBossDatas != null)
		{
			foreach (RaidBossData item in raidBossDatas)
			{
				raidBossInfo.m_raidData.Add(item);
			}
		}
		return raidBossInfo;
	}

	// Token: 0x06000F1D RID: 3869 RVA: 0x00058860 File Offset: 0x00056A60
	public static RaidBossInfo CreateDataForDebugData(List<RaidBossData> raidBossDatas)
	{
		RaidBossInfo result = null;
		global::Debug.LogWarning("RaidBossInfo:DummyDataCreate  not create!!!");
		return result;
	}

	// Token: 0x04000CFD RID: 3325
	private static RaidBossData m_currentRaidData;

	// Token: 0x04000CFE RID: 3326
	private string m_bossName;

	// Token: 0x04000CFF RID: 3327
	private long m_raidRing;

	// Token: 0x04000D00 RID: 3328
	private long m_raidRingOffset;

	// Token: 0x04000D01 RID: 3329
	private List<RaidBossData> m_raidData;

	// Token: 0x04000D02 RID: 3330
	private RaidBossInfo.CallbackRaidBossInfoUpdate m_callback;

	// Token: 0x02000A7C RID: 2684
	// (Invoke) Token: 0x0600482A RID: 18474
	public delegate void CallbackRaidBossInfoUpdate(RaidBossInfo info);
}
