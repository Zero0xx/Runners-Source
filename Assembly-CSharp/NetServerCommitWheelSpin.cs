using System;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

// Token: 0x02000794 RID: 1940
public class NetServerCommitWheelSpin : NetBase
{
	// Token: 0x06003369 RID: 13161 RVA: 0x0011B744 File Offset: 0x00119944
	public NetServerCommitWheelSpin(int count)
	{
		this.paramCount = count;
	}

	// Token: 0x0600336A RID: 13162 RVA: 0x0011B754 File Offset: 0x00119954
	protected override void DoRequest()
	{
		base.SetAction("Spin/commitWheelSpin");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string commitWheelSpinString = instance.GetCommitWheelSpinString(this.paramCount);
			global::Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(commitWheelSpinString);
		}
	}

	// Token: 0x0600336B RID: 13163 RVA: 0x0011B79C File Offset: 0x0011999C
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_PlayerState(jdata);
		this.GetResponse_CharacterState(jdata);
		this.GetResponse_ChaoState(jdata);
		this.GetResponse_WheelOptions(jdata);
	}

	// Token: 0x0600336C RID: 13164 RVA: 0x0011B7C8 File Offset: 0x001199C8
	protected override void DoDidSuccessEmulation()
	{
		this.resultPlayerState = ServerInterface.PlayerState;
		this.resultWheelOptions = ServerInterface.WheelOptions;
		this.resultPlayerState.RefreshFakeState();
		this.resultWheelOptions.RefreshFakeState();
		if (this.resultWheelOptions.m_spinCost > this.resultPlayerState.m_numRedRings)
		{
			base.resultStCd = ServerInterface.StatusCode.NotEnoughRedStarRings;
		}
		else
		{
			this.resultPlayerState.m_numRedRings -= this.resultWheelOptions.m_spinCost;
			this.resultWheelOptions.m_spinCost = this.resultWheelOptions.m_nextSpinCost;
			this.resultWheelOptions.m_nextSpinCost++;
			ServerWheelOptions.WheelItemType wheelItemType = (ServerWheelOptions.WheelItemType)this.resultWheelOptions.m_items[this.resultWheelOptions.m_itemWon];
			if (wheelItemType == ServerWheelOptions.WheelItemType.SpinAgain)
			{
				this.resultWheelOptions.m_nextSpinCost = this.resultWheelOptions.m_spinCost;
				this.resultWheelOptions.m_spinCost = 0;
			}
			DateTime now = DateTime.Now;
			if (this.resultWheelOptions.m_nextFreeSpin <= now)
			{
				TimeSpan t = new TimeSpan(1, 0, 0, 0);
				while (this.resultWheelOptions.m_nextFreeSpin <= now)
				{
					this.resultWheelOptions.m_nextFreeSpin += t;
				}
			}
			Array values = Enum.GetValues(typeof(ServerWheelOptions.WheelItemType));
			this.resultWheelOptions.m_items[this.resultWheelOptions.m_itemWon] = (int)values.GetValue(UnityEngine.Random.Range(0, values.Length - 1));
			this.resultWheelOptions.m_itemWon = UnityEngine.Random.Range(0, this.resultWheelOptions.m_items.Length);
		}
	}

	// Token: 0x0600336D RID: 13165 RVA: 0x0011B978 File Offset: 0x00119B78
	private void SetParameter_WheelSpin()
	{
		base.WriteActionParamValue("count", this.paramCount);
	}

	// Token: 0x17000704 RID: 1796
	// (get) Token: 0x0600336E RID: 13166 RVA: 0x0011B990 File Offset: 0x00119B90
	// (set) Token: 0x0600336F RID: 13167 RVA: 0x0011B998 File Offset: 0x00119B98
	public ServerPlayerState resultPlayerState { get; private set; }

	// Token: 0x17000705 RID: 1797
	// (get) Token: 0x06003370 RID: 13168 RVA: 0x0011B9A4 File Offset: 0x00119BA4
	// (set) Token: 0x06003371 RID: 13169 RVA: 0x0011B9AC File Offset: 0x00119BAC
	public ServerCharacterState[] resultCharacterState { get; private set; }

	// Token: 0x17000706 RID: 1798
	// (get) Token: 0x06003372 RID: 13170 RVA: 0x0011B9B8 File Offset: 0x00119BB8
	// (set) Token: 0x06003373 RID: 13171 RVA: 0x0011B9C0 File Offset: 0x00119BC0
	public List<ServerChaoState> resultChaoState { get; private set; }

	// Token: 0x17000707 RID: 1799
	// (get) Token: 0x06003374 RID: 13172 RVA: 0x0011B9CC File Offset: 0x00119BCC
	// (set) Token: 0x06003375 RID: 13173 RVA: 0x0011B9D4 File Offset: 0x00119BD4
	public ServerWheelOptions resultWheelOptions { get; private set; }

	// Token: 0x17000708 RID: 1800
	// (get) Token: 0x06003376 RID: 13174 RVA: 0x0011B9E0 File Offset: 0x00119BE0
	// (set) Token: 0x06003377 RID: 13175 RVA: 0x0011B9E8 File Offset: 0x00119BE8
	public ServerSpinResultGeneral resultSpinResultGeneral { get; private set; }

	// Token: 0x06003378 RID: 13176 RVA: 0x0011B9F4 File Offset: 0x00119BF4
	private void GetResponse_PlayerState(JsonData jdata)
	{
		this.resultPlayerState = NetUtil.AnalyzePlayerStateJson(jdata, "playerState");
	}

	// Token: 0x06003379 RID: 13177 RVA: 0x0011BA08 File Offset: 0x00119C08
	private void GetResponse_CharacterState(JsonData jdata)
	{
		this.resultCharacterState = NetUtil.AnalyzePlayerState_CharactersStates(jdata);
	}

	// Token: 0x0600337A RID: 13178 RVA: 0x0011BA18 File Offset: 0x00119C18
	private void GetResponse_ChaoState(JsonData jdata)
	{
		this.resultChaoState = NetUtil.AnalyzePlayerState_ChaoStates(jdata);
	}

	// Token: 0x0600337B RID: 13179 RVA: 0x0011BA28 File Offset: 0x00119C28
	private void GetResponse_WheelOptions(JsonData jdata)
	{
		this.resultWheelOptions = NetUtil.AnalyzeWheelOptionsJson(jdata, "wheelOptions");
	}

	// Token: 0x0600337C RID: 13180 RVA: 0x0011BA3C File Offset: 0x00119C3C
	private void GetResponse_WheelResult(JsonData jdata)
	{
		this.resultSpinResultGeneral = NetUtil.AnalyzeSpinResultJson(jdata, "spinResultList");
	}

	// Token: 0x04002BC8 RID: 11208
	public int paramCount;
}
