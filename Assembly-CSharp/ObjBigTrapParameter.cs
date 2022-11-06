using System;

// Token: 0x02000913 RID: 2323
[Serializable]
public class ObjBigTrapParameter : SpawnableParameter
{
	// Token: 0x06003D2D RID: 15661 RVA: 0x00141130 File Offset: 0x0013F330
	public ObjBigTrapParameter() : base("ObjBigTrap")
	{
		this.moveSpeedX = -1f;
		this.moveSpeedY = 0.5f;
		this.moveDistanceY = 1f;
		this.startMoveDistance = 20f;
	}

	// Token: 0x04003530 RID: 13616
	public float moveSpeedX;

	// Token: 0x04003531 RID: 13617
	public float moveSpeedY;

	// Token: 0x04003532 RID: 13618
	public float moveDistanceY;

	// Token: 0x04003533 RID: 13619
	public float startMoveDistance;
}
