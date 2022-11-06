using System;

// Token: 0x020008DE RID: 2270
[Serializable]
public class ObjDashRingParameter : SpawnableParameter
{
	// Token: 0x06003C37 RID: 15415 RVA: 0x0013D1E0 File Offset: 0x0013B3E0
	public ObjDashRingParameter() : base("ObjDashRing")
	{
		this.firstSpeed = 8f;
		this.outOfcontrol = 0.5f;
	}

	// Token: 0x04003474 RID: 13428
	public float firstSpeed;

	// Token: 0x04003475 RID: 13429
	public float outOfcontrol;
}
