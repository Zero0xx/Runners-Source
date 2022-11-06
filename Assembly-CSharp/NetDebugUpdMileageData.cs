using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x020006DA RID: 1754
public class NetDebugUpdMileageData : NetBase
{
	// Token: 0x06002EF2 RID: 12018 RVA: 0x001124B0 File Offset: 0x001106B0
	public NetDebugUpdMileageData() : this(null)
	{
	}

	// Token: 0x06002EF3 RID: 12019 RVA: 0x001124BC File Offset: 0x001106BC
	public NetDebugUpdMileageData(ServerMileageMapState mileageMapState)
	{
		this.mileageMapState = mileageMapState;
	}

	// Token: 0x06002EF4 RID: 12020 RVA: 0x001124CC File Offset: 0x001106CC
	protected override void DoRequest()
	{
		base.SetAction("Debug/updMileageData");
		this.SetParameter_MileageMapState();
	}

	// Token: 0x06002EF5 RID: 12021 RVA: 0x001124E0 File Offset: 0x001106E0
	protected override void DoDidSuccess(JsonData jdata)
	{
	}

	// Token: 0x06002EF6 RID: 12022 RVA: 0x001124E4 File Offset: 0x001106E4
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000628 RID: 1576
	// (get) Token: 0x06002EF7 RID: 12023 RVA: 0x001124E8 File Offset: 0x001106E8
	// (set) Token: 0x06002EF8 RID: 12024 RVA: 0x001124F0 File Offset: 0x001106F0
	public ServerMileageMapState mileageMapState { get; set; }

	// Token: 0x06002EF9 RID: 12025 RVA: 0x001124FC File Offset: 0x001106FC
	private void SetParameter_MileageMapState()
	{
		long num = (long)NetUtil.GetCurrentUnixTime();
		int num2 = 0;
		int num3 = 0;
		base.WriteActionParamObject("mileageMapState", new Dictionary<string, object>
		{
			{
				"episode",
				this.mileageMapState.m_episode
			},
			{
				"chapter",
				this.mileageMapState.m_chapter
			},
			{
				"point",
				this.mileageMapState.m_point
			},
			{
				"mapDistance",
				num2
			},
			{
				"numBossAttack",
				this.mileageMapState.m_numBossAttack
			},
			{
				"stageDistance",
				num3
			},
			{
				"chapterStartTime",
				num
			},
			{
				"stageTotalScore",
				this.mileageMapState.m_stageTotalScore
			},
			{
				"stageMaxScore",
				this.mileageMapState.m_stageMaxScore
			}
		});
	}
}
