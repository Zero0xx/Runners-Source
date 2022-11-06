using System;
using LitJson;

// Token: 0x0200077F RID: 1919
public class NetServerGetCharacterState : NetBase
{
	// Token: 0x06003317 RID: 13079 RVA: 0x0011AE80 File Offset: 0x00119080
	protected override void DoRequest()
	{
		base.SetAction("Player/getCharacterState");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string onlySendBaseParamString = instance.GetOnlySendBaseParamString();
			base.WriteJsonString(onlySendBaseParamString);
		}
	}

	// Token: 0x06003318 RID: 13080 RVA: 0x0011AEB8 File Offset: 0x001190B8
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_CharacterState(jdata);
	}

	// Token: 0x06003319 RID: 13081 RVA: 0x0011AEC4 File Offset: 0x001190C4
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x170006FA RID: 1786
	// (get) Token: 0x0600331A RID: 13082 RVA: 0x0011AEC8 File Offset: 0x001190C8
	// (set) Token: 0x0600331B RID: 13083 RVA: 0x0011AED0 File Offset: 0x001190D0
	public ServerCharacterState[] resultCharacterState { get; private set; }

	// Token: 0x0600331C RID: 13084 RVA: 0x0011AEDC File Offset: 0x001190DC
	private void GetResponse_CharacterState(JsonData jdata)
	{
		this.resultCharacterState = NetUtil.AnalyzePlayerState_CharactersStates(jdata);
	}
}
