using System;

// Token: 0x02000910 RID: 2320
[Serializable]
public class ObjAirTrapParameter : SpawnableParameter
{
	// Token: 0x06003D1F RID: 15647 RVA: 0x00140F1C File Offset: 0x0013F11C
	public ObjAirTrapParameter() : base("ObjAirTrap")
	{
		this.moveSpeed = 0f;
		this.moveDistanceX = 0f;
		this.moveDistanceY = 0f;
	}

	// Token: 0x04003527 RID: 13607
	public float moveSpeed;

	// Token: 0x04003528 RID: 13608
	public float moveDistanceX;

	// Token: 0x04003529 RID: 13609
	public float moveDistanceY;
}
