using System;
using System.Collections.Generic;
using DataTable;
using UnityEngine;

// Token: 0x0200020E RID: 526
public abstract class EventBaseInfo
{
	// Token: 0x06000DD2 RID: 3538
	public abstract void Init();

	// Token: 0x06000DD3 RID: 3539
	public abstract void UpdateData(MonoBehaviour obj);

	// Token: 0x06000DD4 RID: 3540 RVA: 0x0005108C File Offset: 0x0004F28C
	protected virtual void DebugInit()
	{
	}

	// Token: 0x06000DD5 RID: 3541 RVA: 0x00051090 File Offset: 0x0004F290
	public ChaoData GetRewardChao()
	{
		ChaoData result = null;
		if (this.m_rewardChao != null && this.m_rewardChao.Count > 0)
		{
			result = this.m_rewardChao[0];
		}
		return result;
	}

	// Token: 0x170001FC RID: 508
	// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x000510CC File Offset: 0x0004F2CC
	public long totalPoint
	{
		get
		{
			return this.m_totalPoint;
		}
	}

	// Token: 0x170001FD RID: 509
	// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x000510D4 File Offset: 0x0004F2D4
	public EventBaseInfo.EVENT_AGGREGATE_TARGET totalPointTarget
	{
		get
		{
			return this.m_totalPointTarget;
		}
	}

	// Token: 0x170001FE RID: 510
	// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x000510DC File Offset: 0x0004F2DC
	public string totalPointUnitsString
	{
		get
		{
			string result = string.Empty;
			switch (this.m_totalPointTarget)
			{
			case EventBaseInfo.EVENT_AGGREGATE_TARGET.SP_CRYSTAL:
				result = "ko";
				break;
			case EventBaseInfo.EVENT_AGGREGATE_TARGET.RAID_BOSS:
				result = "tai";
				break;
			case EventBaseInfo.EVENT_AGGREGATE_TARGET.ANIMAL:
				result = "hiki";
				break;
			case EventBaseInfo.EVENT_AGGREGATE_TARGET.CRYSTAL:
				result = "ko";
				break;
			case EventBaseInfo.EVENT_AGGREGATE_TARGET.RING:
				result = "ko";
				break;
			case EventBaseInfo.EVENT_AGGREGATE_TARGET.DISTANCE:
				result = "me-toru";
				break;
			}
			return result;
		}
	}

	// Token: 0x170001FF RID: 511
	// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x0005115C File Offset: 0x0004F35C
	public string eventName
	{
		get
		{
			return this.m_eventName;
		}
	}

	// Token: 0x17000200 RID: 512
	// (get) Token: 0x06000DDA RID: 3546 RVA: 0x00051164 File Offset: 0x0004F364
	public string caption
	{
		get
		{
			return this.m_caption;
		}
	}

	// Token: 0x17000201 RID: 513
	// (get) Token: 0x06000DDB RID: 3547 RVA: 0x0005116C File Offset: 0x0004F36C
	public string leftTitle
	{
		get
		{
			return this.m_leftTitle;
		}
	}

	// Token: 0x17000202 RID: 514
	// (get) Token: 0x06000DDC RID: 3548 RVA: 0x00051174 File Offset: 0x0004F374
	public string leftName
	{
		get
		{
			return this.m_leftName;
		}
	}

	// Token: 0x17000203 RID: 515
	// (get) Token: 0x06000DDD RID: 3549 RVA: 0x0005117C File Offset: 0x0004F37C
	public string leftText
	{
		get
		{
			return this.m_leftText;
		}
	}

	// Token: 0x17000204 RID: 516
	// (get) Token: 0x06000DDE RID: 3550 RVA: 0x00051184 File Offset: 0x0004F384
	public string leftBg
	{
		get
		{
			return this.m_leftBg;
		}
	}

	// Token: 0x17000205 RID: 517
	// (get) Token: 0x06000DDF RID: 3551 RVA: 0x0005118C File Offset: 0x0004F38C
	public string chaoTypeIcon
	{
		get
		{
			return this.m_chaoTypeIcon;
		}
	}

	// Token: 0x17000206 RID: 518
	// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x00051194 File Offset: 0x0004F394
	public Texture leftTex
	{
		get
		{
			return this.m_leftTex;
		}
	}

	// Token: 0x17000207 RID: 519
	// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x0005119C File Offset: 0x0004F39C
	public string rightTitle
	{
		get
		{
			return this.m_rightTitle;
		}
	}

	// Token: 0x17000208 RID: 520
	// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x000511A4 File Offset: 0x0004F3A4
	public string rightTitleIcon
	{
		get
		{
			return this.m_rightTitleIcon;
		}
	}

	// Token: 0x17000209 RID: 521
	// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x000511AC File Offset: 0x0004F3AC
	public List<EventMission> eventMission
	{
		get
		{
			return this.m_eventMission;
		}
	}

	// Token: 0x06000DE4 RID: 3556 RVA: 0x000511B4 File Offset: 0x0004F3B4
	private int GetAttainmentMissionNum(long point)
	{
		int num = 0;
		if (this.m_eventMission != null && this.m_eventMission.Count > 0)
		{
			for (int i = 0; i < this.m_eventMission.Count; i++)
			{
				if (this.m_eventMission[i] != null && this.m_eventMission[i].IsAttainment(point))
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x06000DE5 RID: 3557 RVA: 0x00051228 File Offset: 0x0004F428
	public bool SetTotalPoint(int point, out List<EventMission> mission)
	{
		long totalPoint = this.totalPoint;
		this.m_totalPoint = (long)point;
		if (totalPoint != this.m_totalPoint)
		{
			EventBaseInfo.s_pointSetCount++;
		}
		return this.GetCurrentClearMission(totalPoint, out mission, false);
	}

	// Token: 0x06000DE6 RID: 3558 RVA: 0x0005126C File Offset: 0x0004F46C
	public bool SetTotalPoint(int point)
	{
		long totalPoint = this.totalPoint;
		this.m_totalPoint = (long)point;
		if (totalPoint != this.m_totalPoint)
		{
			EventBaseInfo.s_pointSetCount++;
		}
		return this.GetCurrentClearMission(totalPoint);
	}

	// Token: 0x06000DE7 RID: 3559 RVA: 0x000512AC File Offset: 0x0004F4AC
	public bool GetCurrentClearMission(long oldTotalPoint, out List<EventMission> mission, bool nextMission = false)
	{
		bool result = false;
		mission = null;
		if (this.totalPoint >= oldTotalPoint)
		{
			int attainmentMissionNum = this.GetAttainmentMissionNum(this.totalPoint);
			int attainmentMissionNum2 = this.GetAttainmentMissionNum(oldTotalPoint);
			if (attainmentMissionNum >= attainmentMissionNum2 && this.m_eventMission != null && this.m_eventMission.Count > 0)
			{
				for (int i = 0; i < this.m_eventMission.Count; i++)
				{
					if (this.m_eventMission[i] != null)
					{
						if (!this.m_eventMission[i].IsAttainment(this.totalPoint))
						{
							if (nextMission)
							{
								if (mission == null)
								{
									mission = new List<EventMission>();
									mission.Add(this.m_eventMission[i]);
								}
								else
								{
									mission.Add(this.m_eventMission[i]);
								}
								result = true;
							}
							break;
						}
						if (!this.m_eventMission[i].IsAttainment(oldTotalPoint))
						{
							if (mission == null)
							{
								mission = new List<EventMission>();
								mission.Add(this.m_eventMission[i]);
							}
							else
							{
								mission.Add(this.m_eventMission[i]);
							}
							result = true;
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06000DE8 RID: 3560 RVA: 0x000513EC File Offset: 0x0004F5EC
	public bool GetCurrentClearMission(long oldTotalPoint)
	{
		bool result = false;
		if (this.totalPoint > oldTotalPoint)
		{
			int attainmentMissionNum = this.GetAttainmentMissionNum(this.totalPoint);
			int attainmentMissionNum2 = this.GetAttainmentMissionNum(oldTotalPoint);
			if (attainmentMissionNum > attainmentMissionNum2)
			{
				result = true;
			}
		}
		return result;
	}

	// Token: 0x04000BC5 RID: 3013
	private static int s_pointSetCount;

	// Token: 0x04000BC6 RID: 3014
	protected List<ChaoData> m_rewardChao;

	// Token: 0x04000BC7 RID: 3015
	protected string m_eventName;

	// Token: 0x04000BC8 RID: 3016
	protected bool m_init;

	// Token: 0x04000BC9 RID: 3017
	protected bool m_dummyData;

	// Token: 0x04000BCA RID: 3018
	protected long m_totalPoint;

	// Token: 0x04000BCB RID: 3019
	protected EventBaseInfo.EVENT_AGGREGATE_TARGET m_totalPointTarget = EventBaseInfo.EVENT_AGGREGATE_TARGET.NONE;

	// Token: 0x04000BCC RID: 3020
	protected string m_caption;

	// Token: 0x04000BCD RID: 3021
	protected string m_leftTitle;

	// Token: 0x04000BCE RID: 3022
	protected string m_leftName;

	// Token: 0x04000BCF RID: 3023
	protected string m_leftText;

	// Token: 0x04000BD0 RID: 3024
	protected string m_leftBg;

	// Token: 0x04000BD1 RID: 3025
	protected string m_chaoTypeIcon;

	// Token: 0x04000BD2 RID: 3026
	protected Texture m_leftTex;

	// Token: 0x04000BD3 RID: 3027
	protected string m_rightTitle;

	// Token: 0x04000BD4 RID: 3028
	protected string m_rightTitleIcon;

	// Token: 0x04000BD5 RID: 3029
	protected List<EventMission> m_eventMission;

	// Token: 0x0200020F RID: 527
	public enum EVENT_AGGREGATE_TARGET
	{
		// Token: 0x04000BD7 RID: 3031
		SP_CRYSTAL,
		// Token: 0x04000BD8 RID: 3032
		RAID_BOSS,
		// Token: 0x04000BD9 RID: 3033
		ANIMAL,
		// Token: 0x04000BDA RID: 3034
		CRYSTAL,
		// Token: 0x04000BDB RID: 3035
		RING,
		// Token: 0x04000BDC RID: 3036
		DISTANCE,
		// Token: 0x04000BDD RID: 3037
		NONE
	}
}
