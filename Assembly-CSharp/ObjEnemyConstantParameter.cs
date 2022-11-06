using System;

// Token: 0x02000932 RID: 2354
[Serializable]
public class ObjEnemyConstantParameter : SpawnableParameter
{
	// Token: 0x06003DBA RID: 15802 RVA: 0x00142244 File Offset: 0x00140444
	public ObjEnemyConstantParameter() : base("ObjEnemyConstant")
	{
		this.moveSpeed = 0f;
		this.moveDistance = 0f;
		this.startMoveDistance = 0f;
		this.tblID = 0;
	}

	// Token: 0x04003553 RID: 13651
	public float moveSpeed;

	// Token: 0x04003554 RID: 13652
	public float moveDistance;

	// Token: 0x04003555 RID: 13653
	public float startMoveDistance;

	// Token: 0x04003556 RID: 13654
	public int tblID;
}
