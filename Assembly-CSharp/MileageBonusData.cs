using System;

// Token: 0x020002DF RID: 735
public struct MileageBonusData
{
	// Token: 0x06001555 RID: 5461 RVA: 0x0007651C File Offset: 0x0007471C
	public MileageBonusData(MileageBonus type, float value)
	{
		this.type = type;
		this.value = value;
	}

	// Token: 0x040012BD RID: 4797
	public MileageBonus type;

	// Token: 0x040012BE RID: 4798
	public float value;
}
