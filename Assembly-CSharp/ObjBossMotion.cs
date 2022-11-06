using System;
using UnityEngine;

// Token: 0x02000843 RID: 2115
public class ObjBossMotion : MonoBehaviour
{
	// Token: 0x06003920 RID: 14624 RVA: 0x0012EDF0 File Offset: 0x0012CFF0
	public void Setup()
	{
		this.OnSetup();
	}

	// Token: 0x06003921 RID: 14625 RVA: 0x0012EDF8 File Offset: 0x0012CFF8
	protected virtual void OnSetup()
	{
	}

	// Token: 0x04002FFD RID: 12285
	public bool m_debugDrawMotionInfo;

	// Token: 0x04002FFE RID: 12286
	protected Animator m_animator;

	// Token: 0x02000844 RID: 2116
	protected class BossMotionParam
	{
		// Token: 0x06003922 RID: 14626 RVA: 0x0012EDFC File Offset: 0x0012CFFC
		public BossMotionParam(string flagName, int motionID)
		{
			this.m_flagName = flagName;
			this.m_motionID = motionID;
		}

		// Token: 0x04002FFF RID: 12287
		public string m_flagName;

		// Token: 0x04003000 RID: 12288
		public int m_motionID;
	}
}
