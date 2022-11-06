using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using SaveData;
using UnityEngine;

// Token: 0x0200021A RID: 538
public class EventManager : MonoBehaviour
{
	// Token: 0x17000211 RID: 529
	// (get) Token: 0x06000E09 RID: 3593 RVA: 0x00051738 File Offset: 0x0004F938
	public static EventManager Instance
	{
		get
		{
			return EventManager.instance;
		}
	}

	// Token: 0x17000212 RID: 530
	// (get) Token: 0x06000E0A RID: 3594 RVA: 0x00051740 File Offset: 0x0004F940
	public ServerEventState State
	{
		get
		{
			return this.m_state;
		}
	}

	// Token: 0x17000213 RID: 531
	// (get) Token: 0x06000E0B RID: 3595 RVA: 0x00051748 File Offset: 0x0004F948
	public List<ServerEventReward> RewardList
	{
		get
		{
			return this.m_rewardList;
		}
	}

	// Token: 0x17000214 RID: 532
	// (get) Token: 0x06000E0C RID: 3596 RVA: 0x00051750 File Offset: 0x0004F950
	public DateTime EventEndTime
	{
		get
		{
			return this.m_endTime;
		}
	}

	// Token: 0x17000215 RID: 533
	// (get) Token: 0x06000E0D RID: 3597 RVA: 0x00051758 File Offset: 0x0004F958
	public List<ServerEventRaidBossState> UserRaidBossList
	{
		get
		{
			return this.m_userRaidBossList;
		}
	}

	// Token: 0x17000216 RID: 534
	// (get) Token: 0x06000E0E RID: 3598 RVA: 0x00051760 File Offset: 0x0004F960
	// (set) Token: 0x06000E0F RID: 3599 RVA: 0x00051768 File Offset: 0x0004F968
	public bool AppearRaidBoss
	{
		get
		{
			return this.m_appearRaidBoss;
		}
		set
		{
			this.m_appearRaidBoss = value;
		}
	}

	// Token: 0x17000217 RID: 535
	// (get) Token: 0x06000E10 RID: 3600 RVA: 0x00051774 File Offset: 0x0004F974
	// (set) Token: 0x06000E11 RID: 3601 RVA: 0x0005177C File Offset: 0x0004F97C
	public ServerEventRaidBossBonus RaidBossBonus
	{
		get
		{
			return this.m_raidBossBonus;
		}
		set
		{
			this.m_raidBossBonus = value;
		}
	}

	// Token: 0x17000218 RID: 536
	// (get) Token: 0x06000E12 RID: 3602 RVA: 0x00051788 File Offset: 0x0004F988
	public DateTime EventCloseTime
	{
		get
		{
			return this.m_closeTime;
		}
	}

	// Token: 0x06000E13 RID: 3603 RVA: 0x00051790 File Offset: 0x0004F990
	public bool IsStandby()
	{
		return this.m_standbyType != EventManager.EventType.UNKNOWN;
	}

	// Token: 0x06000E14 RID: 3604 RVA: 0x000517A0 File Offset: 0x0004F9A0
	public bool IsInEvent()
	{
		return this.CheckCloseTime();
	}

	// Token: 0x06000E15 RID: 3605 RVA: 0x000517A8 File Offset: 0x0004F9A8
	public bool IsChallengeEvent()
	{
		return this.IsInEvent() && this.CheckEndTime();
	}

	// Token: 0x06000E16 RID: 3606 RVA: 0x000517C0 File Offset: 0x0004F9C0
	public bool IsPlayEventForStage()
	{
		return this.CheckPlayingTime();
	}

	// Token: 0x06000E17 RID: 3607 RVA: 0x000517C8 File Offset: 0x0004F9C8
	public bool IsResultEvent()
	{
		return this.CheckResultTime();
	}

	// Token: 0x06000E18 RID: 3608 RVA: 0x000517D0 File Offset: 0x0004F9D0
	public bool IsCautionPlayEvent()
	{
		return this.IsChallengeEvent() && (this.m_endTime - NetBase.GetCurrentTime()).TotalSeconds < 1800.0;
	}

	// Token: 0x06000E19 RID: 3609 RVA: 0x00051810 File Offset: 0x0004FA10
	public static EventManager.EventType GetType(int id)
	{
		if (id > 0)
		{
			switch (id / 100000000)
			{
			case 1:
				return EventManager.EventType.SPECIAL_STAGE;
			case 2:
				return EventManager.EventType.RAID_BOSS;
			case 3:
				return EventManager.EventType.COLLECT_OBJECT;
			case 4:
				return EventManager.EventType.GACHA;
			case 5:
				return EventManager.EventType.ADVERT;
			case 6:
				return EventManager.EventType.QUICK;
			case 7:
				return EventManager.EventType.BGM;
			}
		}
		return EventManager.EventType.UNKNOWN;
	}

	// Token: 0x06000E1A RID: 3610 RVA: 0x00051868 File Offset: 0x0004FA68
	public static EventManager.CollectEventType GetCollectEventType(int id)
	{
		EventManager.EventType type = EventManager.GetType(id);
		if (type == EventManager.EventType.COLLECT_OBJECT)
		{
			int num = id % 100000000;
			num /= 10000;
			for (int i = 0; i < 3; i++)
			{
				if (num == EventManager.COLLECT_EVENT_SPECIFIC_ID[i])
				{
					return (EventManager.CollectEventType)i;
				}
			}
		}
		return EventManager.CollectEventType.UNKNOWN;
	}

	// Token: 0x06000E1B RID: 3611 RVA: 0x000518B8 File Offset: 0x0004FAB8
	public static bool IsVaildEvent(int id)
	{
		if (id > 0)
		{
			int num = id / 10000;
			if (EventManager.instance != null)
			{
				int num2 = EventManager.instance.Id / 10000;
				return num2 == num;
			}
		}
		return false;
	}

	// Token: 0x06000E1C RID: 3612 RVA: 0x000518FC File Offset: 0x0004FAFC
	public static int GetSpecificId()
	{
		if (EventManager.instance != null)
		{
			return EventManager.instance.Id / 10000;
		}
		return -1;
	}

	// Token: 0x06000E1D RID: 3613 RVA: 0x0005192C File Offset: 0x0004FB2C
	public static int GetSpecificId(int eventId)
	{
		return eventId / 10000;
	}

	// Token: 0x06000E1E RID: 3614 RVA: 0x00051938 File Offset: 0x0004FB38
	public static string GetResourceName()
	{
		int specificId = EventManager.GetSpecificId();
		if (EventManager.instance != null)
		{
			return EventManager.instance.GetEventTypeName() + "_" + specificId.ToString();
		}
		return string.Empty;
	}

	// Token: 0x17000219 RID: 537
	// (get) Token: 0x06000E1F RID: 3615 RVA: 0x0005197C File Offset: 0x0004FB7C
	// (set) Token: 0x06000E20 RID: 3616 RVA: 0x00051984 File Offset: 0x0004FB84
	public int Id
	{
		get
		{
			return this.m_eventId;
		}
		set
		{
			this.m_eventId = value;
		}
	}

	// Token: 0x1700021A RID: 538
	// (get) Token: 0x06000E21 RID: 3617 RVA: 0x00051990 File Offset: 0x0004FB90
	public EventManager.EventType Type
	{
		get
		{
			return this.m_eventType;
		}
	}

	// Token: 0x1700021B RID: 539
	// (get) Token: 0x06000E22 RID: 3618 RVA: 0x00051998 File Offset: 0x0004FB98
	public EventManager.EventType StandbyType
	{
		get
		{
			return this.m_standbyType;
		}
	}

	// Token: 0x1700021C RID: 540
	// (get) Token: 0x06000E23 RID: 3619 RVA: 0x000519A0 File Offset: 0x0004FBA0
	public EventManager.EventType TypeInTime
	{
		get
		{
			if (this.m_eventType != EventManager.EventType.UNKNOWN && this.IsInEvent())
			{
				return this.m_eventType;
			}
			return EventManager.EventType.UNKNOWN;
		}
	}

	// Token: 0x1700021D RID: 541
	// (get) Token: 0x06000E24 RID: 3620 RVA: 0x000519C4 File Offset: 0x0004FBC4
	public EventManager.AdvertEventType AdvertType
	{
		get
		{
			return this.m_advertType;
		}
	}

	// Token: 0x1700021E RID: 542
	// (get) Token: 0x06000E25 RID: 3621 RVA: 0x000519CC File Offset: 0x0004FBCC
	public int NumberOfTimes
	{
		get
		{
			return this.m_numberOfTimes;
		}
	}

	// Token: 0x1700021F RID: 543
	// (get) Token: 0x06000E26 RID: 3622 RVA: 0x000519D4 File Offset: 0x0004FBD4
	public int ReservedId
	{
		get
		{
			return this.m_reservedId;
		}
	}

	// Token: 0x17000220 RID: 544
	// (get) Token: 0x06000E27 RID: 3623 RVA: 0x000519DC File Offset: 0x0004FBDC
	public EventManager.CollectEventType CollectType
	{
		get
		{
			return this.m_collectType;
		}
	}

	// Token: 0x17000221 RID: 545
	// (get) Token: 0x06000E28 RID: 3624 RVA: 0x000519E4 File Offset: 0x0004FBE4
	// (set) Token: 0x06000E29 RID: 3625 RVA: 0x000519EC File Offset: 0x0004FBEC
	public long CollectCount
	{
		get
		{
			return this.m_collectCount;
		}
		set
		{
			this.m_collectCount = value;
		}
	}

	// Token: 0x17000222 RID: 546
	// (get) Token: 0x06000E2A RID: 3626 RVA: 0x000519F8 File Offset: 0x0004FBF8
	// (set) Token: 0x06000E2B RID: 3627 RVA: 0x00051A00 File Offset: 0x0004FC00
	public bool EventStage
	{
		get
		{
			return this.m_eventStage;
		}
		set
		{
			this.m_eventStage = value;
		}
	}

	// Token: 0x17000223 RID: 547
	// (get) Token: 0x06000E2C RID: 3628 RVA: 0x00051A0C File Offset: 0x0004FC0C
	public SpecialStageInfo SpecialStageInfo
	{
		get
		{
			return this.m_specialStageInfo;
		}
	}

	// Token: 0x17000224 RID: 548
	// (get) Token: 0x06000E2D RID: 3629 RVA: 0x00051A14 File Offset: 0x0004FC14
	public RaidBossInfo RaidBossInfo
	{
		get
		{
			return this.m_raidBossInfo;
		}
	}

	// Token: 0x17000225 RID: 549
	// (get) Token: 0x06000E2E RID: 3630 RVA: 0x00051A1C File Offset: 0x0004FC1C
	public EtcEventInfo EtcEventInfo
	{
		get
		{
			return this.m_etcEventInfo;
		}
	}

	// Token: 0x17000226 RID: 550
	// (get) Token: 0x06000E2F RID: 3631 RVA: 0x00051A24 File Offset: 0x0004FC24
	public bool IsSetEventStateInfo
	{
		get
		{
			return this.m_setEventInfo;
		}
	}

	// Token: 0x06000E30 RID: 3632 RVA: 0x00051A2C File Offset: 0x0004FC2C
	public bool IsQuickEvent()
	{
		return this.m_eventType == EventManager.EventType.QUICK && this.IsInEvent();
	}

	// Token: 0x06000E31 RID: 3633 RVA: 0x00051A44 File Offset: 0x0004FC44
	public bool IsBGMEvent()
	{
		return this.m_eventType == EventManager.EventType.BGM && this.IsInEvent();
	}

	// Token: 0x06000E32 RID: 3634 RVA: 0x00051A5C File Offset: 0x0004FC5C
	public bool IsSpecialStage()
	{
		return this.m_eventStage && this.m_eventType == EventManager.EventType.SPECIAL_STAGE;
	}

	// Token: 0x06000E33 RID: 3635 RVA: 0x00051A78 File Offset: 0x0004FC78
	public bool IsRaidBossStage()
	{
		return this.m_eventStage && this.m_eventType == EventManager.EventType.RAID_BOSS;
	}

	// Token: 0x06000E34 RID: 3636 RVA: 0x00051A94 File Offset: 0x0004FC94
	public bool IsCollectEvent()
	{
		return this.m_eventStage && this.m_eventType == EventManager.EventType.COLLECT_OBJECT;
	}

	// Token: 0x06000E35 RID: 3637 RVA: 0x00051AB0 File Offset: 0x0004FCB0
	public bool IsGetAnimalStage()
	{
		return this.IsCollectEvent() && this.m_collectType == EventManager.CollectEventType.GET_ANIMALS;
	}

	// Token: 0x06000E36 RID: 3638 RVA: 0x00051AC8 File Offset: 0x0004FCC8
	public bool IsEncounterRaidBoss()
	{
		foreach (ServerEventRaidBossState serverEventRaidBossState in this.m_userRaidBossList)
		{
			if (serverEventRaidBossState.Encounter && serverEventRaidBossState.GetStatusType() != ServerEventRaidBossState.StatusType.PROCESS_END)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000E37 RID: 3639 RVA: 0x00051B48 File Offset: 0x0004FD48
	public string GetEventTypeName()
	{
		return EventManager.GetEventTypeName(this.m_eventType);
	}

	// Token: 0x06000E38 RID: 3640 RVA: 0x00051B58 File Offset: 0x0004FD58
	public static string GetEventTypeName(EventManager.EventType type)
	{
		if (type < EventManager.EventType.NUM)
		{
			return EventManager.EventTypeName[(int)type];
		}
		return string.Empty;
	}

	// Token: 0x06000E39 RID: 3641 RVA: 0x00051B70 File Offset: 0x0004FD70
	public int GetCollectEventSpecificId(EventManager.CollectEventType type)
	{
		if (type < EventManager.CollectEventType.NUM)
		{
			return EventManager.COLLECT_EVENT_SPECIFIC_ID[(int)type];
		}
		return -1;
	}

	// Token: 0x17000227 RID: 551
	// (get) Token: 0x06000E3A RID: 3642 RVA: 0x00051B84 File Offset: 0x0004FD84
	public List<ServerChaoData> RecommendedChaos
	{
		get
		{
			return null;
		}
	}

	// Token: 0x06000E3B RID: 3643 RVA: 0x00051B88 File Offset: 0x0004FD88
	public WindowEventData GetWindowEvenData(int texWindowId)
	{
		if (this.m_datas != null && this.m_datas.Count > 0)
		{
			foreach (WindowEventData windowEventData in this.m_datas[0].window_data)
			{
				if (windowEventData.id == texWindowId)
				{
					return windowEventData;
				}
			}
		}
		return null;
	}

	// Token: 0x06000E3C RID: 3644 RVA: 0x00051BEC File Offset: 0x0004FDEC
	public EventStageData GetStageData()
	{
		if (this.m_datas != null && this.m_datas.Count > 0)
		{
			return this.m_datas[0].stage_data;
		}
		return null;
	}

	// Token: 0x06000E3D RID: 3645 RVA: 0x00051C20 File Offset: 0x0004FE20
	public EyeCatcherChaoData[] GetEyeCatcherChaoDatas()
	{
		if (this.m_datas != null && this.m_datas.Count > 0 && this.m_datas[0].chao_data != null)
		{
			return this.m_datas[0].chao_data.eyeCatchers;
		}
		return null;
	}

	// Token: 0x06000E3E RID: 3646 RVA: 0x00051C78 File Offset: 0x0004FE78
	public RewardChaoData GetRewardChaoData()
	{
		if (this.m_datas != null && this.m_datas.Count > 0 && this.m_datas[0].chao_data != null && this.m_datas[0].chao_data.rewards != null && this.m_datas[0].chao_data.rewards.Length > 0)
		{
			return this.m_datas[0].chao_data.rewards[0];
		}
		return null;
	}

	// Token: 0x06000E3F RID: 3647 RVA: 0x00051D0C File Offset: 0x0004FF0C
	public EyeCatcherCharaData[] GetEyeCatcherCharaDatas()
	{
		if (this.m_datas != null && this.m_datas.Count > 0 && this.m_datas[0].chao_data != null)
		{
			return this.m_datas[0].chao_data.charaEyeCatchers;
		}
		return null;
	}

	// Token: 0x06000E40 RID: 3648 RVA: 0x00051D64 File Offset: 0x0004FF64
	public EventAvertData GetAvertData()
	{
		if (this.m_datas != null && this.m_datas.Count > 0)
		{
			return this.m_datas[0].advert_data;
		}
		return null;
	}

	// Token: 0x17000228 RID: 552
	// (get) Token: 0x06000E41 RID: 3649 RVA: 0x00051D98 File Offset: 0x0004FF98
	public ServerEventUserRaidBossState RaidBossState
	{
		get
		{
			return this.m_raidState;
		}
	}

	// Token: 0x17000229 RID: 553
	// (get) Token: 0x06000E42 RID: 3650 RVA: 0x00051DA0 File Offset: 0x0004FFA0
	public int RaidbossChallengeCount
	{
		get
		{
			if (this.m_raidState != null && this.m_raidState.RaidBossEnergyCount >= 0)
			{
				return this.m_raidState.RaidBossEnergyCount;
			}
			return 0;
		}
	}

	// Token: 0x1700022A RID: 554
	// (get) Token: 0x06000E43 RID: 3651 RVA: 0x00051DCC File Offset: 0x0004FFCC
	// (set) Token: 0x06000E44 RID: 3652 RVA: 0x00051DD4 File Offset: 0x0004FFD4
	public int UseRaidbossChallengeCount
	{
		get
		{
			return this.m_useRaidBossEnergy;
		}
		set
		{
			this.m_useRaidBossEnergy = value;
		}
	}

	// Token: 0x06000E45 RID: 3653 RVA: 0x00051DE0 File Offset: 0x0004FFE0
	public float GetRaidAttackRate(int useChallengeCount)
	{
		if (useChallengeCount > 0)
		{
			int num = useChallengeCount - 1;
			if (this.m_raidBossAttackRateList != null && this.m_raidBossAttackRateList[0].attackRate != null && num < this.m_raidBossAttackRateList[0].attackRate.Length)
			{
				return this.m_raidBossAttackRateList[0].attackRate[num];
			}
		}
		return 1f;
	}

	// Token: 0x06000E46 RID: 3654 RVA: 0x00051E4C File Offset: 0x0005004C
	public void SetEventMenuData(TextAsset xml_data)
	{
		if (this.m_datas == null)
		{
			this.m_datas = new List<EventMenuData>();
		}
		else
		{
			this.m_datas.Clear();
		}
		string s = AESCrypt.Decrypt(xml_data.text);
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(EventMenuData[]));
		StringReader textReader = new StringReader(s);
		EventMenuData[] array = (EventMenuData[])xmlSerializer.Deserialize(textReader);
		if (array != null && array.Length > 0 && this.m_datas != null)
		{
			this.m_datas.Add(array[0]);
		}
	}

	// Token: 0x06000E47 RID: 3655 RVA: 0x00051ED8 File Offset: 0x000500D8
	public void SetRaidBossAttacRate(TextAsset xml_data)
	{
		if (this.m_raidBossAttackRateList == null)
		{
			this.m_raidBossAttackRateList = new List<RaidBossAttackRateTable>();
		}
		else
		{
			this.m_raidBossAttackRateList.Clear();
		}
		string s = AESCrypt.Decrypt(xml_data.text);
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(RaidBossAttackRateTable[]));
		StringReader textReader = new StringReader(s);
		RaidBossAttackRateTable[] array = (RaidBossAttackRateTable[])xmlSerializer.Deserialize(textReader);
		if (array != null && array.Length > 0 && this.m_raidBossAttackRateList != null)
		{
			this.m_raidBossAttackRateList.Add(array[0]);
		}
	}

	// Token: 0x06000E48 RID: 3656 RVA: 0x00051F64 File Offset: 0x00050164
	public void SetParameter()
	{
		this.m_eventType = EventManager.EventType.UNKNOWN;
		this.m_standbyType = EventManager.EventType.UNKNOWN;
		this.SetCurrentEvent();
		this.CalcParameter();
		this.m_synchFlag = true;
	}

	// Token: 0x06000E49 RID: 3657 RVA: 0x00051F88 File Offset: 0x00050188
	public void SetState(ServerEventState state)
	{
		if (state != null)
		{
			state.CopyTo(this.m_state);
			this.m_collectCount = this.m_state.Param;
			this.m_setEventInfo = true;
		}
	}

	// Token: 0x06000E4A RID: 3658 RVA: 0x00051FC0 File Offset: 0x000501C0
	public void ReCalcEndPlayTime()
	{
		if (this.m_eventType == EventManager.EventType.RAID_BOSS)
		{
			this.m_endPlayingTimeMinutes = UnityEngine.Random.Range(4, 9);
			this.m_endPlayTime = this.m_endTime + new TimeSpan(0, this.m_endPlayingTimeMinutes, 0);
		}
		else
		{
			this.m_endPlayingTimeMinutes = UnityEngine.Random.Range(24, 29);
			this.m_endPlayTime = this.m_endTime + new TimeSpan(0, this.m_endPlayingTimeMinutes, 0);
		}
	}

	// Token: 0x06000E4B RID: 3659 RVA: 0x00052038 File Offset: 0x00050238
	public void SetDebugParameter()
	{
		this.m_endTimeHours = 72;
		this.m_debugFlag = true;
		this.SetParameter();
	}

	// Token: 0x06000E4C RID: 3660 RVA: 0x00052050 File Offset: 0x00050250
	public void SetEventInfo()
	{
		switch (this.m_eventType)
		{
		case EventManager.EventType.SPECIAL_STAGE:
			if (this.m_debugFlag)
			{
				if (this.m_specialStageInfo == null)
				{
					this.m_specialStageInfo = SpecialStageInfo.CreateDummyData();
				}
			}
			else
			{
				this.m_specialStageInfo = SpecialStageInfo.CreateData();
			}
			break;
		case EventManager.EventType.RAID_BOSS:
			if (this.m_debugFlag)
			{
				if (this.m_raidBossInfo == null)
				{
					this.DebugSetRaidBossData();
				}
			}
			else
			{
				this.SetRaidBossData();
			}
			break;
		case EventManager.EventType.COLLECT_OBJECT:
			if (this.m_debugFlag)
			{
				if (this.m_etcEventInfo == null)
				{
					this.m_etcEventInfo = EtcEventInfo.CreateDummyData();
				}
			}
			else
			{
				this.m_etcEventInfo = EtcEventInfo.CreateData();
			}
			break;
		}
	}

	// Token: 0x06000E4D RID: 3661 RVA: 0x00052114 File Offset: 0x00050314
	public void CheckEvent()
	{
		if (this.IsStandby())
		{
			if (this.IsInEvent())
			{
				this.SetParameter();
			}
			else if (this.m_closeTime < NetBase.GetCurrentTime())
			{
				this.ResetParameter();
				this.SetParameter();
			}
		}
		else if (this.m_eventType != EventManager.EventType.UNKNOWN)
		{
			if (this.IsInEvent())
			{
				if (this.m_eventType == EventManager.EventType.ADVERT && this.IsStartOtherEvent())
				{
					this.ResetParameter();
					this.SetParameter();
				}
			}
			else
			{
				this.ResetParameter();
				this.SetParameter();
			}
		}
	}

	// Token: 0x06000E4E RID: 3662 RVA: 0x000521B4 File Offset: 0x000503B4
	public void ResetData()
	{
		if (this.m_datas != null)
		{
			this.m_datas.Clear();
			this.m_datas = null;
		}
		if (this.m_raidBossInfo != null)
		{
			this.m_raidBossInfo = null;
		}
		if (this.m_etcEventInfo != null)
		{
			this.m_etcEventInfo = null;
		}
		if (this.m_specialStageInfo != null)
		{
			this.m_specialStageInfo = null;
		}
		this.m_setEventInfo = false;
		this.m_eventStage = false;
		this.m_appearRaidBoss = false;
	}

	// Token: 0x06000E4F RID: 3663 RVA: 0x0005222C File Offset: 0x0005042C
	protected void Awake()
	{
		this.SetInstance();
	}

	// Token: 0x06000E50 RID: 3664 RVA: 0x00052234 File Offset: 0x00050434
	private void Start()
	{
		base.enabled = false;
		this.SynchServerEntryList();
	}

	// Token: 0x06000E51 RID: 3665 RVA: 0x00052244 File Offset: 0x00050444
	private void OnDestroy()
	{
		if (EventManager.instance == this)
		{
			EventManager.instance = null;
		}
	}

	// Token: 0x06000E52 RID: 3666 RVA: 0x0005225C File Offset: 0x0005045C
	public void SynchServerEntryList()
	{
		this.m_synchFlag = false;
		this.m_entryList.Clear();
		List<ServerEventEntry> eventEntryList = ServerInterface.EventEntryList;
		if (eventEntryList != null)
		{
			int count = eventEntryList.Count;
			for (int i = 0; i < count; i++)
			{
				this.m_entryList.Add(eventEntryList[i]);
			}
		}
		this.SetParameter();
	}

	// Token: 0x06000E53 RID: 3667 RVA: 0x000522B8 File Offset: 0x000504B8
	public void SynchServerEventState()
	{
		ServerEventState eventState = ServerInterface.EventState;
		if (eventState != null)
		{
			eventState.CopyTo(this.m_state);
			this.m_collectCount = this.m_state.Param;
			this.m_setEventInfo = true;
		}
	}

	// Token: 0x06000E54 RID: 3668 RVA: 0x000522F8 File Offset: 0x000504F8
	public void SynchServerRewardList()
	{
		this.m_rewardList.Clear();
		List<ServerEventReward> eventRewardList = ServerInterface.EventRewardList;
		if (eventRewardList != null)
		{
			int count = eventRewardList.Count;
			for (int i = 0; i < count; i++)
			{
				this.m_rewardList.Add(eventRewardList[i]);
			}
		}
	}

	// Token: 0x06000E55 RID: 3669 RVA: 0x00052348 File Offset: 0x00050548
	public void SynchServerEventRaidBossList(List<ServerEventRaidBossState> raidBossList)
	{
		this.m_userRaidBossList.Clear();
		if (raidBossList != null)
		{
			int count = raidBossList.Count;
			bool flag = false;
			for (int i = 0; i < count; i++)
			{
				this.m_userRaidBossList.Add(raidBossList[i]);
				EventUtility.SetRaidbossEntry(raidBossList[i]);
				if (raidBossList[i] != null && RaidBossInfo.currentRaidData != null && RaidBossInfo.currentRaidData.id == raidBossList[i].Id)
				{
					RaidBossInfo.currentRaidData = new RaidBossData(raidBossList[i]);
					flag = true;
				}
			}
			if (!flag && RaidBossInfo.currentRaidData != null)
			{
				RaidBossInfo.currentRaidData = null;
			}
		}
		this.SetRaidBossData();
	}

	// Token: 0x06000E56 RID: 3670 RVA: 0x00052400 File Offset: 0x00050600
	public void SynchServerEventRaidBossList(ServerEventRaidBossState raidBossState)
	{
		if (raidBossState != null)
		{
			bool flag = true;
			foreach (ServerEventRaidBossState serverEventRaidBossState in this.m_userRaidBossList)
			{
				if (serverEventRaidBossState.Id == raidBossState.Id)
				{
					raidBossState.CopyTo(serverEventRaidBossState);
					flag = false;
					break;
				}
			}
			if (flag)
			{
				this.m_userRaidBossList.Add(raidBossState);
				EventUtility.SetRaidbossEntry(raidBossState);
			}
		}
		this.SetRaidBossData();
	}

	// Token: 0x06000E57 RID: 3671 RVA: 0x000524A4 File Offset: 0x000506A4
	public void SynchServerEventUserRaidBossState(ServerEventUserRaidBossState state)
	{
		if (state != null)
		{
			state.CopyTo(this.m_raidState);
		}
		if (this.m_raidBossInfo != null)
		{
			this.m_raidBossInfo.raidRing = (long)this.m_raidState.NumRaidbossRings;
			this.m_raidBossInfo.totalDestroyCount = (long)this.m_raidState.NumBeatedEnterprise;
		}
	}

	// Token: 0x06000E58 RID: 3672 RVA: 0x000524FC File Offset: 0x000506FC
	public void SynchServerEventRaidBossUserList(List<ServerEventRaidBossUserState> userList, long raidBossId, ServerEventRaidBossBonus bonus)
	{
		RaidBossData raidBossData = null;
		if (this.m_raidBossInfo != null)
		{
			List<RaidBossData> raidData = this.m_raidBossInfo.raidData;
			if (raidData != null)
			{
				foreach (RaidBossData raidBossData2 in raidData)
				{
					if (raidBossData2.id == raidBossId)
					{
						raidBossData2.SetUserList(userList);
						raidBossData2.SetReward(bonus);
						raidBossData = raidBossData2;
						break;
					}
				}
			}
		}
		if (raidBossData != null && RaidBossInfo.currentRaidData != null && RaidBossInfo.currentRaidData.id == raidBossId)
		{
			RaidBossInfo.currentRaidData = raidBossData;
		}
	}

	// Token: 0x06000E59 RID: 3673 RVA: 0x000525BC File Offset: 0x000507BC
	private void SetInstance()
	{
		if (EventManager.instance == null)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			EventManager.instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000E5A RID: 3674 RVA: 0x000525F0 File Offset: 0x000507F0
	private void CalcParameter()
	{
		if (this.m_eventType != EventManager.EventType.UNKNOWN && this.m_eventType < EventManager.EventType.NUM)
		{
			this.m_specificId = this.m_eventId % 100000000;
			this.m_specificId /= 10000;
			this.m_numberOfTimes = this.m_eventId % 10000;
			this.m_numberOfTimes /= 100;
			this.m_reservedId = this.m_eventId % 100;
			if (this.m_eventType == EventManager.EventType.COLLECT_OBJECT)
			{
				this.SetCollectEventType();
			}
			else if (this.m_eventType == EventManager.EventType.SPECIAL_STAGE || this.m_eventType == EventManager.EventType.RAID_BOSS)
			{
				if (SystemSaveManager.Instance != null)
				{
					SystemData systemdata = SystemSaveManager.Instance.GetSystemdata();
					if (systemdata != null && systemdata.pictureShowEventId != this.m_eventId)
					{
						systemdata.pictureShowEventId = this.m_eventId;
						systemdata.pictureShowProgress = -1;
						systemdata.pictureShowEmergeRaidBossProgress = -1;
						systemdata.pictureShowRaidBossFirstBattle = -1;
						SystemSaveManager.Instance.SaveSystemData();
					}
				}
			}
			else if (this.m_eventType == EventManager.EventType.ADVERT)
			{
				this.SetAdvertEventType();
			}
		}
	}

	// Token: 0x06000E5B RID: 3675 RVA: 0x0005270C File Offset: 0x0005090C
	private void SetCurrentEvent()
	{
		ServerEventEntry serverEventEntry = null;
		ServerEventEntry serverEventEntry2 = null;
		DateTime currentTime = NetBase.GetCurrentTime();
		foreach (ServerEventEntry serverEventEntry3 in this.m_entryList)
		{
			DateTime eventCloseTime = serverEventEntry3.EventCloseTime;
			if (eventCloseTime > currentTime)
			{
				EventManager.EventType type = EventManager.GetType(serverEventEntry3.EventId);
				if (type == EventManager.EventType.ADVERT)
				{
					if (serverEventEntry2 == null)
					{
						serverEventEntry2 = serverEventEntry3;
					}
					else if (serverEventEntry3.EventStartTime < serverEventEntry2.EventStartTime)
					{
						serverEventEntry2 = serverEventEntry3;
					}
				}
				else if (serverEventEntry == null)
				{
					serverEventEntry = serverEventEntry3;
				}
				else if (serverEventEntry3.EventStartTime < serverEventEntry.EventStartTime)
				{
					serverEventEntry = serverEventEntry3;
				}
			}
		}
		bool flag = false;
		if (serverEventEntry != null && serverEventEntry2 != null)
		{
			if (serverEventEntry2.EventStartTime < serverEventEntry.EventStartTime)
			{
				DateTime eventStartTime = serverEventEntry.EventStartTime;
				if (eventStartTime > currentTime)
				{
					flag = true;
				}
			}
		}
		else if (serverEventEntry == null && serverEventEntry2 != null)
		{
			flag = true;
		}
		ServerEventEntry serverEventEntry4 = (!flag) ? serverEventEntry : serverEventEntry2;
		if (serverEventEntry4 != null)
		{
			this.m_startTime = serverEventEntry4.EventStartTime;
			this.m_endTime = serverEventEntry4.EventEndTime;
			this.m_closeTime = serverEventEntry4.EventCloseTime;
			this.m_eventId = serverEventEntry4.EventId;
			if (this.IsInEvent())
			{
				this.m_eventType = EventManager.GetType(this.m_eventId);
				this.ReCalcEndPlayTime();
				if (this.m_eventType == EventManager.EventType.RAID_BOSS)
				{
					this.m_endResultTimeMinutes = 10;
					this.m_endResultTime = this.m_endTime + new TimeSpan(0, this.m_endResultTimeMinutes, 0);
				}
				else
				{
					this.m_endResultTimeMinutes = 30;
					this.m_endResultTime = this.m_endTime + new TimeSpan(0, this.m_endResultTimeMinutes, 0);
				}
			}
			else
			{
				this.m_standbyType = EventManager.GetType(this.m_eventId);
			}
		}
	}

	// Token: 0x06000E5C RID: 3676 RVA: 0x00052924 File Offset: 0x00050B24
	private bool IsStartOtherEvent()
	{
		ServerEventEntry serverEventEntry = null;
		DateTime currentTime = NetBase.GetCurrentTime();
		foreach (ServerEventEntry serverEventEntry2 in this.m_entryList)
		{
			if (EventManager.GetType(serverEventEntry2.EventId) != EventManager.EventType.ADVERT)
			{
				DateTime eventEndTime = serverEventEntry2.EventEndTime;
				if (eventEndTime > currentTime)
				{
					if (serverEventEntry == null)
					{
						serverEventEntry = serverEventEntry2;
					}
					else if (serverEventEntry2.EventStartTime < serverEventEntry.EventStartTime)
					{
						serverEventEntry = serverEventEntry2;
					}
				}
			}
		}
		if (serverEventEntry != null)
		{
			DateTime eventStartTime = serverEventEntry.EventStartTime;
			if (eventStartTime <= currentTime)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000E5D RID: 3677 RVA: 0x000529F4 File Offset: 0x00050BF4
	private void DebugSetCurrentEvent()
	{
		if (this.m_debugFlag && this.m_eventId > 0)
		{
			if (!this.m_synchFlag && this.m_endTimeHours + this.m_endTimeMinutes + this.m_endTimeSeconds > 0)
			{
				TimeSpan timeSpan = new TimeSpan(this.m_endTimeHours, this.m_endTimeMinutes, this.m_endTimeSeconds);
				TimeSpan timeSpan2 = new TimeSpan(this.m_closeTimeHours, this.m_closeTimeMinutes, this.m_closeTimeSeconds);
				if (timeSpan > timeSpan2)
				{
					timeSpan2 = timeSpan;
				}
				TimeSpan t = new TimeSpan(this.m_startTimeHours, this.m_startTimeMinutes, this.m_startTimeSeconds);
				this.m_startTime = NetBase.GetCurrentTime() + t;
				this.m_endTime = this.m_startTime + timeSpan;
				this.m_closeTime = this.m_startTime + timeSpan2;
				this.m_endPlayTime = this.m_endTime + new TimeSpan(0, this.m_endPlayingTimeMinutes, 0);
				this.m_endResultTime = this.m_endTime + new TimeSpan(0, this.m_endResultTimeMinutes, 0);
			}
			if (this.IsInEvent())
			{
				this.m_eventType = EventManager.GetType(this.m_eventId);
			}
			else
			{
				this.m_standbyType = EventManager.GetType(this.m_eventId);
			}
		}
	}

	// Token: 0x06000E5E RID: 3678 RVA: 0x00052B3C File Offset: 0x00050D3C
	private void SetRaidBossData()
	{
		if (this.m_eventType != EventManager.EventType.RAID_BOSS)
		{
			return;
		}
		if (this.m_raidBossInfo != null)
		{
			if (this.m_raidBossInfo.raidData != null)
			{
				this.m_raidBossInfo.raidData.Clear();
				foreach (ServerEventRaidBossState state in this.m_userRaidBossList)
				{
					this.m_raidBossInfo.raidData.Add(new RaidBossData(state));
				}
			}
		}
		else
		{
			List<RaidBossData> list = new List<RaidBossData>();
			foreach (ServerEventRaidBossState state2 in this.m_userRaidBossList)
			{
				list.Add(new RaidBossData(state2));
			}
			this.m_raidBossInfo = RaidBossInfo.CreateData(list);
		}
		if (this.m_raidBossInfo != null)
		{
			this.m_raidBossInfo.raidRing = (long)this.m_raidState.NumRaidbossRings;
			this.m_raidBossInfo.totalDestroyCount = (long)this.m_raidState.NumBeatedEnterprise;
		}
		this.m_setEventInfo = true;
	}

	// Token: 0x06000E5F RID: 3679 RVA: 0x00052C9C File Offset: 0x00050E9C
	private void DebugSetRaidBossData()
	{
		if (this.m_eventType != EventManager.EventType.RAID_BOSS)
		{
			return;
		}
		List<RaidBossData> list = new List<RaidBossData>();
		foreach (EventManager.DebugRaidBoss debugRaidBoss in this.m_debugRaidBossInfo.m_debugRaidBossDatas)
		{
			if (debugRaidBoss.m_validFlag)
			{
				ServerEventRaidBossState serverEventRaidBossState = new ServerEventRaidBossState();
				DateTime escapeAt = NetBase.GetCurrentTime() + new TimeSpan(0, debugRaidBoss.m_endTimeMinutes, 0);
				serverEventRaidBossState.Id = (long)debugRaidBoss.m_id;
				serverEventRaidBossState.Rarity = debugRaidBoss.m_rarity;
				serverEventRaidBossState.Level = debugRaidBoss.m_level;
				serverEventRaidBossState.EncounterName = debugRaidBoss.m_discovererName;
				serverEventRaidBossState.Encounter = debugRaidBoss.m_findMyself;
				serverEventRaidBossState.Status = debugRaidBoss.m_state;
				serverEventRaidBossState.EscapeAt = escapeAt;
				serverEventRaidBossState.HitPoint = debugRaidBoss.m_hp;
				serverEventRaidBossState.MaxHitPoint = debugRaidBoss.m_hpMax;
				list.Add(new RaidBossData(serverEventRaidBossState));
			}
		}
		this.m_setEventInfo = true;
		this.m_raidBossInfo = RaidBossInfo.CreateDataForDebugData(list);
		if (this.m_raidBossInfo != null)
		{
			int debugCurrentRaidBossDataIndex = this.m_debugRaidBossInfo.m_debugCurrentRaidBossDataIndex;
			if (this.m_raidBossInfo.raidData.Count > debugCurrentRaidBossDataIndex && debugCurrentRaidBossDataIndex >= 0)
			{
				RaidBossInfo.currentRaidData = this.m_raidBossInfo.raidData[debugCurrentRaidBossDataIndex];
			}
			this.m_appearRaidBoss = this.m_debugRaidBossInfo.m_debugRaidBossDescentFlag;
			this.m_raidBossInfo.raidRing = (long)this.m_debugRaidBossInfo.m_raidBossRingNum;
			this.m_raidBossInfo.totalDestroyCount = (long)this.m_debugRaidBossInfo.m_raidBossKillNum;
		}
		if (this.m_raidState != null)
		{
			this.m_raidState.RaidBossEnergy = 20;
		}
	}

	// Token: 0x06000E60 RID: 3680 RVA: 0x00052E44 File Offset: 0x00051044
	private void ResetParameter()
	{
		this.ResetData();
		this.m_eventType = EventManager.EventType.UNKNOWN;
		this.m_standbyType = EventManager.EventType.UNKNOWN;
		this.m_collectType = EventManager.CollectEventType.UNKNOWN;
		this.m_advertType = EventManager.AdvertEventType.UNKNOWN;
		this.m_eventId = -1;
		this.m_specificId = -1;
		this.m_numberOfTimes = -1;
		this.m_reservedId = -1;
		this.m_useRaidBossEnergy = 0;
		this.m_startTime = DateTime.MinValue;
		this.m_endTime = DateTime.MinValue;
		this.m_closeTime = DateTime.MinValue;
		this.m_endPlayTime = DateTime.MinValue;
		this.m_endResultTime = DateTime.MinValue;
	}

	// Token: 0x06000E61 RID: 3681 RVA: 0x00052ED0 File Offset: 0x000510D0
	private bool CheckCloseTime()
	{
		DateTime currentTime = NetBase.GetCurrentTime();
		return currentTime >= this.m_startTime && this.m_closeTime > currentTime;
	}

	// Token: 0x06000E62 RID: 3682 RVA: 0x00052F04 File Offset: 0x00051104
	private bool CheckEndTime()
	{
		DateTime currentTime = NetBase.GetCurrentTime();
		return currentTime >= this.m_startTime && this.m_endTime > currentTime;
	}

	// Token: 0x06000E63 RID: 3683 RVA: 0x00052F38 File Offset: 0x00051138
	private bool CheckPlayingTime()
	{
		if (this.m_eventType != EventManager.EventType.UNKNOWN)
		{
			DateTime currentTime = NetBase.GetCurrentTime();
			if (currentTime >= this.m_startTime)
			{
				return this.m_endPlayTime > currentTime;
			}
		}
		return false;
	}

	// Token: 0x06000E64 RID: 3684 RVA: 0x00052F78 File Offset: 0x00051178
	private bool CheckResultTime()
	{
		if (this.m_eventType != EventManager.EventType.UNKNOWN)
		{
			DateTime currentTime = NetBase.GetCurrentTime();
			if (currentTime >= this.m_startTime)
			{
				return this.m_endResultTime > currentTime;
			}
		}
		return false;
	}

	// Token: 0x06000E65 RID: 3685 RVA: 0x00052FB8 File Offset: 0x000511B8
	private void SetCollectEventType()
	{
		this.m_collectType = EventManager.CollectEventType.GET_ANIMALS;
		for (int i = 0; i < 3; i++)
		{
			if (EventManager.COLLECT_EVENT_SPECIFIC_ID[i] == this.m_specificId)
			{
				this.m_collectType = (EventManager.CollectEventType)i;
				break;
			}
		}
	}

	// Token: 0x06000E66 RID: 3686 RVA: 0x00052FFC File Offset: 0x000511FC
	private void SetAdvertEventType()
	{
		this.m_advertType = EventManager.AdvertEventType.UNKNOWN;
		if (this.m_specificId < 1000)
		{
			this.m_advertType = EventManager.AdvertEventType.ROULETTE;
		}
		else if (this.m_specificId < 2000)
		{
			this.m_advertType = EventManager.AdvertEventType.CHARACTER;
		}
		else if (this.m_specificId < 3000)
		{
			this.m_advertType = EventManager.AdvertEventType.SHOP;
		}
	}

	// Token: 0x06000E67 RID: 3687 RVA: 0x00053064 File Offset: 0x00051264
	public EventProductionData GetPuductionData()
	{
		if (this.m_datas != null && this.m_datas.Count > 0)
		{
			return this.m_datas[0].puduction_data;
		}
		return null;
	}

	// Token: 0x06000E68 RID: 3688 RVA: 0x00053098 File Offset: 0x00051298
	public EventRaidProductionData GetRaidProductionData()
	{
		if (this.m_datas != null && this.m_datas.Count > 0)
		{
			return this.m_datas[0].raid_data;
		}
		return null;
	}

	// Token: 0x04000BFB RID: 3067
	private const int EVENT_TYPE_COFFI = 100000000;

	// Token: 0x04000BFC RID: 3068
	private const int SPECIFIC_TYPE_COFFI = 10000;

	// Token: 0x04000BFD RID: 3069
	private const int NUMBER_OF_TIMES_COFFI = 100;

	// Token: 0x04000BFE RID: 3070
	private const int DEBUG_RAID_BOSS_COUNT = 6;

	// Token: 0x04000BFF RID: 3071
	private static readonly string[] EventTypeName = new string[]
	{
		"SpecialStage",
		"RaidBoss",
		"CollectObject",
		"Gacha",
		"Advert",
		"Quick",
		"BGM"
	};

	// Token: 0x04000C00 RID: 3072
	private static readonly int[] COLLECT_EVENT_SPECIFIC_ID = new int[]
	{
		1,
		2,
		3
	};

	// Token: 0x04000C01 RID: 3073
	private static EventManager instance = null;

	// Token: 0x04000C02 RID: 3074
	[Header("debugFlag にチェックを入れると、指定した時間で始められます")]
	[SerializeField]
	private bool m_debugFlag;

	// Token: 0x04000C03 RID: 3075
	[Header("eventId の詳細はwiki[イベントIDルール]を見てください。")]
	[SerializeField]
	private int m_eventId = -1;

	// Token: 0x04000C04 RID: 3076
	[SerializeField]
	[Header("イベントスタートまでの時間を設定")]
	private int m_startTimeHours;

	// Token: 0x04000C05 RID: 3077
	[SerializeField]
	private int m_startTimeMinutes;

	// Token: 0x04000C06 RID: 3078
	[SerializeField]
	private int m_startTimeSeconds;

	// Token: 0x04000C07 RID: 3079
	[SerializeField]
	[Header("イベントの残り時間を設定")]
	private int m_endTimeHours;

	// Token: 0x04000C08 RID: 3080
	[SerializeField]
	private int m_endTimeMinutes;

	// Token: 0x04000C09 RID: 3081
	[SerializeField]
	private int m_endTimeSeconds;

	// Token: 0x04000C0A RID: 3082
	[Header("イベントのクローズまでの時間を設定")]
	[SerializeField]
	private int m_closeTimeHours;

	// Token: 0x04000C0B RID: 3083
	[SerializeField]
	private int m_closeTimeMinutes;

	// Token: 0x04000C0C RID: 3084
	[SerializeField]
	private int m_closeTimeSeconds;

	// Token: 0x04000C0D RID: 3085
	[SerializeField]
	[Header("イベントのステージプレイ猶予時間を設定")]
	private int m_endPlayingTimeMinutes = 25;

	// Token: 0x04000C0E RID: 3086
	[Header("イベントのステージリザルト猶予時間を設定")]
	[SerializeField]
	private int m_endResultTimeMinutes = 30;

	// Token: 0x04000C0F RID: 3087
	[Header("Debug レイドボスインフォ(データが足りないようなら、変数を足してください)")]
	[SerializeField]
	private EventManager.DebugRaidBossInfo m_debugRaidBossInfo = new EventManager.DebugRaidBossInfo();

	// Token: 0x04000C10 RID: 3088
	private int m_specificId = -1;

	// Token: 0x04000C11 RID: 3089
	private int m_numberOfTimes = -1;

	// Token: 0x04000C12 RID: 3090
	private int m_reservedId;

	// Token: 0x04000C13 RID: 3091
	private int m_useRaidBossEnergy;

	// Token: 0x04000C14 RID: 3092
	private long m_collectCount;

	// Token: 0x04000C15 RID: 3093
	private EventManager.EventType m_eventType = EventManager.EventType.UNKNOWN;

	// Token: 0x04000C16 RID: 3094
	private EventManager.EventType m_standbyType = EventManager.EventType.UNKNOWN;

	// Token: 0x04000C17 RID: 3095
	private EventManager.CollectEventType m_collectType = EventManager.CollectEventType.UNKNOWN;

	// Token: 0x04000C18 RID: 3096
	private EventManager.AdvertEventType m_advertType = EventManager.AdvertEventType.UNKNOWN;

	// Token: 0x04000C19 RID: 3097
	private bool m_eventStage;

	// Token: 0x04000C1A RID: 3098
	private bool m_setEventInfo;

	// Token: 0x04000C1B RID: 3099
	private bool m_appearRaidBoss;

	// Token: 0x04000C1C RID: 3100
	private bool m_synchFlag;

	// Token: 0x04000C1D RID: 3101
	private List<EventMenuData> m_datas;

	// Token: 0x04000C1E RID: 3102
	private List<RaidBossAttackRateTable> m_raidBossAttackRateList;

	// Token: 0x04000C1F RID: 3103
	private SpecialStageInfo m_specialStageInfo;

	// Token: 0x04000C20 RID: 3104
	private RaidBossInfo m_raidBossInfo;

	// Token: 0x04000C21 RID: 3105
	private EtcEventInfo m_etcEventInfo;

	// Token: 0x04000C22 RID: 3106
	private DateTime m_startTime = DateTime.MinValue;

	// Token: 0x04000C23 RID: 3107
	private DateTime m_endTime = DateTime.MinValue;

	// Token: 0x04000C24 RID: 3108
	private DateTime m_endPlayTime = DateTime.MinValue;

	// Token: 0x04000C25 RID: 3109
	private DateTime m_endResultTime = DateTime.MinValue;

	// Token: 0x04000C26 RID: 3110
	private DateTime m_closeTime = DateTime.MinValue;

	// Token: 0x04000C27 RID: 3111
	private List<ServerEventEntry> m_entryList = new List<ServerEventEntry>();

	// Token: 0x04000C28 RID: 3112
	private List<ServerEventReward> m_rewardList = new List<ServerEventReward>();

	// Token: 0x04000C29 RID: 3113
	private List<ServerEventRaidBossState> m_userRaidBossList = new List<ServerEventRaidBossState>();

	// Token: 0x04000C2A RID: 3114
	private ServerEventState m_state = new ServerEventState();

	// Token: 0x04000C2B RID: 3115
	private ServerEventUserRaidBossState m_raidState = new ServerEventUserRaidBossState();

	// Token: 0x04000C2C RID: 3116
	private ServerEventRaidBossBonus m_raidBossBonus = new ServerEventRaidBossBonus();

	// Token: 0x0200021B RID: 539
	public enum EventType
	{
		// Token: 0x04000C2E RID: 3118
		SPECIAL_STAGE,
		// Token: 0x04000C2F RID: 3119
		RAID_BOSS,
		// Token: 0x04000C30 RID: 3120
		COLLECT_OBJECT,
		// Token: 0x04000C31 RID: 3121
		GACHA,
		// Token: 0x04000C32 RID: 3122
		ADVERT,
		// Token: 0x04000C33 RID: 3123
		QUICK,
		// Token: 0x04000C34 RID: 3124
		BGM,
		// Token: 0x04000C35 RID: 3125
		NUM,
		// Token: 0x04000C36 RID: 3126
		UNKNOWN = -1
	}

	// Token: 0x0200021C RID: 540
	public enum CollectEventType
	{
		// Token: 0x04000C38 RID: 3128
		GET_ANIMALS,
		// Token: 0x04000C39 RID: 3129
		GET_RING,
		// Token: 0x04000C3A RID: 3130
		RUN_DISTANCE,
		// Token: 0x04000C3B RID: 3131
		NUM,
		// Token: 0x04000C3C RID: 3132
		UNKNOWN = -1
	}

	// Token: 0x0200021D RID: 541
	public enum AdvertEventType
	{
		// Token: 0x04000C3E RID: 3134
		ROULETTE,
		// Token: 0x04000C3F RID: 3135
		CHARACTER,
		// Token: 0x04000C40 RID: 3136
		SHOP,
		// Token: 0x04000C41 RID: 3137
		NUM,
		// Token: 0x04000C42 RID: 3138
		UNKNOWN = -1
	}

	// Token: 0x0200021E RID: 542
	[Serializable]
	public class DebugRaidBoss
	{
		// Token: 0x04000C43 RID: 3139
		[SerializeField]
		public int m_id;

		// Token: 0x04000C44 RID: 3140
		[SerializeField]
		public int m_level;

		// Token: 0x04000C45 RID: 3141
		[SerializeField]
		public int m_rarity;

		// Token: 0x04000C46 RID: 3142
		[SerializeField]
		public int m_hp = 1;

		// Token: 0x04000C47 RID: 3143
		[SerializeField]
		public int m_hpMax = 1;

		// Token: 0x04000C48 RID: 3144
		[SerializeField]
		public string m_discovererName;

		// Token: 0x04000C49 RID: 3145
		[SerializeField]
		public int m_endTimeMinutes = 60;

		// Token: 0x04000C4A RID: 3146
		[SerializeField]
		public int m_state;

		// Token: 0x04000C4B RID: 3147
		[SerializeField]
		public bool m_findMyself = true;

		// Token: 0x04000C4C RID: 3148
		[SerializeField]
		public bool m_validFlag;
	}

	// Token: 0x0200021F RID: 543
	[Serializable]
	public class DebugRaidBossInfo
	{
		// Token: 0x04000C4D RID: 3149
		[SerializeField]
		[Header("レイドボスの基本データ")]
		public int m_raidBossRingNum;

		// Token: 0x04000C4E RID: 3150
		[SerializeField]
		public int m_raidBossKillNum;

		// Token: 0x04000C4F RID: 3151
		[SerializeField]
		public int m_raidBossChallengeNum;

		// Token: 0x04000C50 RID: 3152
		[SerializeField]
		[Header("レイドボスに挑むときのチャレンジ使用数")]
		public int m_raidBossUseChallengeNum = 1;

		// Token: 0x04000C51 RID: 3153
		[SerializeField]
		[Header("レイドボス情報リスト(データが足りないようなら、クラスの変数を足してください)")]
		public EventManager.DebugRaidBoss[] m_debugRaidBossDatas = new EventManager.DebugRaidBoss[6];

		// Token: 0x04000C52 RID: 3154
		[Header("レイドボス情報リストの指定番号を使用して、レイドボス戦(レイドボスステージ用)")]
		[SerializeField]
		public int m_debugCurrentRaidBossDataIndex;

		// Token: 0x04000C53 RID: 3155
		[Header("レイドボスの襲来フラグ(通常ステージ用)")]
		[SerializeField]
		public bool m_debugRaidBossDescentFlag;
	}
}
