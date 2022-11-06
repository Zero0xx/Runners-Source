using System;

// Token: 0x020008F5 RID: 2293
[Serializable]
public class ObjJumpBoardParameter : SpawnableParameter
{
	// Token: 0x06003C98 RID: 15512 RVA: 0x0013EAE8 File Offset: 0x0013CCE8
	public ObjJumpBoardParameter() : base("ObjJumpBoard")
	{
		this.m_succeedFirstSpeed = 20f;
		this.m_succeedAngle = 45f;
		this.m_succeedOutOfcontrol = 0.2f;
		this.m_missFirstSpeed = 10f;
		this.m_missAngle = 30f;
		this.m_missOutOfcontrol = 0.2f;
	}

	// Token: 0x040034BB RID: 13499
	public float m_succeedFirstSpeed;

	// Token: 0x040034BC RID: 13500
	public float m_succeedAngle;

	// Token: 0x040034BD RID: 13501
	public float m_succeedOutOfcontrol;

	// Token: 0x040034BE RID: 13502
	public float m_missFirstSpeed;

	// Token: 0x040034BF RID: 13503
	public float m_missAngle;

	// Token: 0x040034C0 RID: 13504
	public float m_missOutOfcontrol;
}
