using System;

// Token: 0x0200090B RID: 2315
[Serializable]
public class ObjSpringAirParameter : SpawnableParameter
{
	// Token: 0x06003D0D RID: 15629 RVA: 0x00140CE4 File Offset: 0x0013EEE4
	public ObjSpringAirParameter() : base("ObjSpringAir")
	{
		this.firstSpeed = 2f;
		this.outOfcontrol = 0.5f;
	}

	// Token: 0x0400351F RID: 13599
	public float firstSpeed;

	// Token: 0x04003520 RID: 13600
	public float outOfcontrol;
}
