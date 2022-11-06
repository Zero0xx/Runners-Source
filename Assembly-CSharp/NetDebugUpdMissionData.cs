using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x020006DB RID: 1755
public class NetDebugUpdMissionData : NetBase
{
	// Token: 0x06002EFA RID: 12026 RVA: 0x001125FC File Offset: 0x001107FC
	public NetDebugUpdMissionData() : this(0, new bool[3])
	{
	}

	// Token: 0x06002EFB RID: 12027 RVA: 0x0011260C File Offset: 0x0011080C
	public NetDebugUpdMissionData(int missionSet, params bool[] missionComplete)
	{
		this.paramMissionSet = missionSet;
		this.paramMissionComplete = missionComplete;
	}

	// Token: 0x06002EFC RID: 12028 RVA: 0x00112624 File Offset: 0x00110824
	protected override void DoRequest()
	{
		base.SetAction("Debug/updMissionData");
		this.SetParameter_Mission();
	}

	// Token: 0x06002EFD RID: 12029 RVA: 0x00112638 File Offset: 0x00110838
	protected override void DoDidSuccess(JsonData jdata)
	{
	}

	// Token: 0x06002EFE RID: 12030 RVA: 0x0011263C File Offset: 0x0011083C
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000629 RID: 1577
	// (get) Token: 0x06002EFF RID: 12031 RVA: 0x00112640 File Offset: 0x00110840
	// (set) Token: 0x06002F00 RID: 12032 RVA: 0x00112648 File Offset: 0x00110848
	public int paramMissionSet { get; set; }

	// Token: 0x1700062A RID: 1578
	// (get) Token: 0x06002F01 RID: 12033 RVA: 0x00112654 File Offset: 0x00110854
	// (set) Token: 0x06002F02 RID: 12034 RVA: 0x0011265C File Offset: 0x0011085C
	public bool[] paramMissionComplete
	{
		get
		{
			return this.mParamMissionComplete;
		}
		set
		{
			this.mParamMissionComplete = (value.Clone() as bool[]);
		}
	}

	// Token: 0x06002F03 RID: 12035 RVA: 0x00112670 File Offset: 0x00110870
	private void SetParameter_Mission()
	{
		if (this.paramMissionComplete != null)
		{
			base.WriteActionParamValue("missionSet", this.paramMissionSet);
			List<object> list = new List<object>();
			for (int i = 0; i < this.paramMissionComplete.Length; i++)
			{
				list.Add((!this.paramMissionComplete[i]) ? 0 : 1);
			}
			base.WriteActionParamArray("missionsComplete", list);
			list.Clear();
		}
	}

	// Token: 0x04002A5D RID: 10845
	private bool[] mParamMissionComplete;
}
