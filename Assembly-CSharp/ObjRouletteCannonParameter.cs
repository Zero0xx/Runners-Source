using System;

// Token: 0x020008CB RID: 2251
[Serializable]
public class ObjRouletteCannonParameter : SpawnableParameter
{
	// Token: 0x06003BED RID: 15341 RVA: 0x0013C5A0 File Offset: 0x0013A7A0
	public ObjRouletteCannonParameter() : base("ObjRouletteCannon")
	{
		this.firstSpeed = 10f;
		this.outOfcontrol = 0.5f;
		this.moveSpeed = 1f;
		this.angle = 60f;
	}

	// Token: 0x04003451 RID: 13393
	public float firstSpeed;

	// Token: 0x04003452 RID: 13394
	public float outOfcontrol;

	// Token: 0x04003453 RID: 13395
	public float moveSpeed;

	// Token: 0x04003454 RID: 13396
	public float angle;
}
