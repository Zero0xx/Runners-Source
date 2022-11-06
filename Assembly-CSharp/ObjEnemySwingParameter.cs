using System;

// Token: 0x02000935 RID: 2357
[Serializable]
public class ObjEnemySwingParameter : SpawnableParameter
{
	// Token: 0x06003DC2 RID: 15810 RVA: 0x001423D0 File Offset: 0x001405D0
	public ObjEnemySwingParameter() : base("ObjEnemySwing")
	{
		this.moveSpeed = 0f;
		this.moveDistanceX = 0f;
		this.moveDistanceY = 0f;
		this.tblID = 0;
	}

	// Token: 0x04003558 RID: 13656
	public float moveSpeed;

	// Token: 0x04003559 RID: 13657
	public float moveDistanceX;

	// Token: 0x0400355A RID: 13658
	public float moveDistanceY;

	// Token: 0x0400355B RID: 13659
	public int tblID;
}
