using System;

// Token: 0x020008C8 RID: 2248
[Serializable]
public class ObjCannonParameter : SpawnableParameter
{
	// Token: 0x06003BE2 RID: 15330 RVA: 0x0013C470 File Offset: 0x0013A670
	public ObjCannonParameter() : base("ObjCannon")
	{
		this.firstSpeed = 10f;
		this.outOfcontrol = 0.5f;
		this.moveSpeed = 0.4f;
		this.moveArea = 50f;
	}

	// Token: 0x0400344A RID: 13386
	public float firstSpeed;

	// Token: 0x0400344B RID: 13387
	public float outOfcontrol;

	// Token: 0x0400344C RID: 13388
	public float moveSpeed;

	// Token: 0x0400344D RID: 13389
	public float moveArea;
}
