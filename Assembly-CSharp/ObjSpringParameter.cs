using System;

// Token: 0x0200090D RID: 2317
[Serializable]
public class ObjSpringParameter : SpawnableParameter
{
	// Token: 0x06003D11 RID: 15633 RVA: 0x00140D4C File Offset: 0x0013EF4C
	public ObjSpringParameter() : base("ObjSpring")
	{
		this.firstSpeed = 2f;
		this.outOfcontrol = 0.5f;
	}

	// Token: 0x04003522 RID: 13602
	public float firstSpeed;

	// Token: 0x04003523 RID: 13603
	public float outOfcontrol;
}
