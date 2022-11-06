using System;
using System.Collections.Generic;
using DataTable;
using Message;
using Mission;
using UnityEngine;

// Token: 0x0200029A RID: 666
public class StageMissionManager : MonoBehaviour
{
	// Token: 0x170002E8 RID: 744
	// (get) Token: 0x06001232 RID: 4658 RVA: 0x00065F04 File Offset: 0x00064104
	// (set) Token: 0x06001233 RID: 4659 RVA: 0x00065F0C File Offset: 0x0006410C
	public bool Completed { get; private set; }

	// Token: 0x06001234 RID: 4660 RVA: 0x00065F18 File Offset: 0x00064118
	protected void Awake()
	{
		this.CheckInstance();
	}

	// Token: 0x06001235 RID: 4661 RVA: 0x00065F24 File Offset: 0x00064124
	private void Update()
	{
		if (this.m_missionChecks == null)
		{
			return;
		}
		foreach (MissionCheck missionCheck in this.m_missionChecks)
		{
			missionCheck.Update(Time.deltaTime);
		}
	}

	// Token: 0x06001236 RID: 4662 RVA: 0x00065F9C File Offset: 0x0006419C
	private void OnDestroy()
	{
		this.DeleteAllMissionCheck();
		if (this == StageMissionManager.instance)
		{
			StageMissionManager.instance = null;
		}
	}

	// Token: 0x06001237 RID: 4663 RVA: 0x00065FBC File Offset: 0x000641BC
	public void SetupMissions()
	{
		if (this.m_missionChecks == null)
		{
			this.m_missionChecks = new List<MissionCheck>();
		}
		if (this.m_missionChecks != null && SaveDataManager.Instance)
		{
			DailyMissionData dailyMission = SaveDataManager.Instance.PlayerData.DailyMission;
			int id = dailyMission.id;
			MissionData missionData = MissionTable.GetMissionData(id);
			if (missionData != null)
			{
				bool flag = true;
				if (EventManager.Instance != null && EventManager.Instance.IsRaidBossStage() && missionData.type == MissionData.Type.RING)
				{
					flag = false;
				}
				if (dailyMission.missions_complete)
				{
					flag = false;
				}
				if (flag)
				{
					bool isSetInitialValue = missionData.save;
					long progress = dailyMission.progress;
					this.CreateMissionCheck(missionData, isSetInitialValue, progress);
				}
			}
		}
	}

	// Token: 0x06001238 RID: 4664 RVA: 0x00066080 File Offset: 0x00064280
	public void SaveMissions()
	{
		if (this.m_missionChecks == null)
		{
			return;
		}
		foreach (MissionCheck missionCheck in this.m_missionChecks)
		{
			MissionData data = missionCheck.GetData();
			if (data != null && SaveDataManager.Instance != null)
			{
				DailyMissionData dailyMission = SaveDataManager.Instance.PlayerData.DailyMission;
				if (data.save && dailyMission.id == data.id)
				{
					dailyMission.progress = missionCheck.GetValue();
					if (missionCheck.IsCompleted())
					{
						dailyMission.missions_complete = true;
						this.Completed = true;
					}
				}
			}
		}
	}

	// Token: 0x06001239 RID: 4665 RVA: 0x0006615C File Offset: 0x0006435C
	private void CreateMissionCheck(MissionData data, bool isSetInitialValue = false, long initialValue = 0L)
	{
		MissionCheck missionCheck = null;
		if ((ulong)data.type < (ulong)((long)StageMissionManager.MISSION_TYPE_PARAM_TBL.Length))
		{
			MissionCategory category = StageMissionManager.MISSION_TYPE_PARAM_TBL[(int)data.type].m_category;
			if (category == MissionCategory.COUNT)
			{
				MissionCheckCount missionCheckCount = new MissionCheckCount(StageMissionManager.MISSION_TYPE_PARAM_TBL[(int)data.type].m_eventID);
				missionCheck = missionCheckCount;
			}
		}
		if (missionCheck != null)
		{
			if (isSetInitialValue)
			{
				missionCheck.SetInitialValue(initialValue);
			}
			missionCheck.SetData(data);
			this.m_missionChecks.Add(missionCheck);
		}
	}

	// Token: 0x0600123A RID: 4666 RVA: 0x000661E8 File Offset: 0x000643E8
	private void OnMissionEvent(MsgMissionEvent msg)
	{
		if (this.m_missionChecks == null)
		{
			return;
		}
		foreach (MsgMissionEvent.Data data in msg.m_missions)
		{
			MissionEvent missionEvent = new MissionEvent(data.eventid, data.num);
			this.ProcEvent(missionEvent);
		}
	}

	// Token: 0x0600123B RID: 4667 RVA: 0x00066270 File Offset: 0x00064470
	private void ProcEvent(MissionEvent missionEvent)
	{
		if (this.m_missionChecks == null)
		{
			return;
		}
		foreach (MissionCheck missionCheck in this.m_missionChecks)
		{
			missionCheck.ProcEvent(missionEvent);
		}
	}

	// Token: 0x0600123C RID: 4668 RVA: 0x000662E4 File Offset: 0x000644E4
	private void DeleteAllMissionCheck()
	{
		this.m_missionChecks = null;
	}

	// Token: 0x0600123D RID: 4669 RVA: 0x000662F0 File Offset: 0x000644F0
	private bool GetMissionIsCompletedAndValue(int index, ref bool? isCompleted, ref int? value)
	{
		return false;
	}

	// Token: 0x0600123E RID: 4670 RVA: 0x000662F4 File Offset: 0x000644F4
	private bool IsCompletedMission(int index)
	{
		bool? flag = new bool?(false);
		int? num = null;
		this.GetMissionIsCompletedAndValue(index, ref flag, ref num);
		return flag.Value;
	}

	// Token: 0x0600123F RID: 4671 RVA: 0x00066328 File Offset: 0x00064528
	private int GetMissionValue(int index)
	{
		bool? flag = null;
		int? num = new int?(0);
		if (this.GetMissionIsCompletedAndValue(index, ref flag, ref num))
		{
			return num.Value;
		}
		return 0;
	}

	// Token: 0x06001240 RID: 4672 RVA: 0x00066360 File Offset: 0x00064560
	private void DebugComplete(int missionNo)
	{
		if (this.m_missionChecks == null)
		{
			return;
		}
		foreach (MissionCheck missionCheck in this.m_missionChecks)
		{
			if (missionCheck.GetIndex() == missionNo)
			{
				missionCheck.DebugComplete(missionNo);
				break;
			}
		}
	}

	// Token: 0x06001241 RID: 4673 RVA: 0x000663E4 File Offset: 0x000645E4
	private void DebugComplete()
	{
		if (this.m_missionChecks == null)
		{
			return;
		}
		foreach (MissionCheck missionCheck in this.m_missionChecks)
		{
			missionCheck.DebugComplete(missionCheck.GetIndex());
		}
	}

	// Token: 0x170002E9 RID: 745
	// (get) Token: 0x06001242 RID: 4674 RVA: 0x0006645C File Offset: 0x0006465C
	public static StageMissionManager Instance
	{
		get
		{
			return StageMissionManager.instance;
		}
	}

	// Token: 0x06001243 RID: 4675 RVA: 0x00066464 File Offset: 0x00064664
	protected bool CheckInstance()
	{
		if (StageMissionManager.instance == null)
		{
			StageMissionManager.instance = this;
			return true;
		}
		if (this == StageMissionManager.Instance)
		{
			return true;
		}
		UnityEngine.Object.Destroy(base.gameObject);
		return false;
	}

	// Token: 0x04001062 RID: 4194
	private static readonly MissionTypeParam[] MISSION_TYPE_PARAM_TBL = new MissionTypeParam[]
	{
		new MissionTypeParam(EventID.ENEMYDEAD, MissionCategory.COUNT),
		new MissionTypeParam(EventID.GOLDENENEMYDEAD, MissionCategory.COUNT),
		new MissionTypeParam(EventID.TOTALDISTANCE, MissionCategory.COUNT),
		new MissionTypeParam(EventID.GET_ANIMALS, MissionCategory.COUNT),
		new MissionTypeParam(EventID.GET_SCORE, MissionCategory.COUNT),
		new MissionTypeParam(EventID.GET_RING, MissionCategory.COUNT)
	};

	// Token: 0x04001063 RID: 4195
	private List<MissionCheck> m_missionChecks;

	// Token: 0x04001064 RID: 4196
	private static StageMissionManager instance;
}
