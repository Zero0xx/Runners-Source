using System;

// Token: 0x02000918 RID: 2328
[Serializable]
public class ObjMoveTrapParameter : SpawnableParameter
{
	// Token: 0x06003D3F RID: 15679 RVA: 0x00141440 File Offset: 0x0013F640
	public ObjMoveTrapParameter() : base("ObjMoveTrap")
	{
		this.moveSpeed = 3f;
		this.moveDistance = 20f;
		this.startMoveDistance = 20f;
	}

	// Token: 0x04003541 RID: 13633
	public float moveSpeed;

	// Token: 0x04003542 RID: 13634
	public float moveDistance;

	// Token: 0x04003543 RID: 13635
	public float startMoveDistance;
}
