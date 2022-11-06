using System;

// Token: 0x020008E1 RID: 2273
[Serializable]
public class ObjRainbowRingParameter : SpawnableParameter
{
	// Token: 0x06003C41 RID: 15425 RVA: 0x0013D2A4 File Offset: 0x0013B4A4
	public ObjRainbowRingParameter() : base("ObjRainbowRing")
	{
		this.firstSpeed = 20f;
		this.outOfcontrol = 0.5f;
	}

	// Token: 0x04003477 RID: 13431
	public float firstSpeed;

	// Token: 0x04003478 RID: 13432
	public float outOfcontrol;
}
