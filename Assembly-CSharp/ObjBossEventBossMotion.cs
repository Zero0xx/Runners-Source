using System;
using UnityEngine;

// Token: 0x0200084A RID: 2122
public class ObjBossEventBossMotion : ObjBossMotion
{
	// Token: 0x060039D4 RID: 14804 RVA: 0x00130F5C File Offset: 0x0012F15C
	protected override void OnSetup()
	{
	}

	// Token: 0x060039D5 RID: 14805 RVA: 0x00130F60 File Offset: 0x0012F160
	public void SetMotion(EventBossMotion id, bool flag = true)
	{
		if (this.m_animator == null)
		{
			this.m_animator = base.GetComponentInChildren<Animator>();
		}
		if (this.m_animator && (ulong)id < (ulong)((long)ObjBossEventBossMotion.MOTION_DATA.Length))
		{
			string flagName = ObjBossEventBossMotion.MOTION_DATA[(int)((UIntPtr)id)].m_flagName;
			if (flagName != string.Empty)
			{
				this.m_animator.SetBool(flagName, flag);
				if (this.m_debugDrawMotionInfo)
				{
					global::Debug.Log(string.Concat(new object[]
					{
						"SetMotion ",
						flagName,
						" flag=",
						flag
					}));
				}
				if (flag)
				{
					EventBossMotion motionID = (EventBossMotion)ObjBossEventBossMotion.MOTION_DATA[(int)((UIntPtr)id)].m_motionID;
					if (motionID != EventBossMotion.NONE)
					{
						this.SetMotion(motionID, false);
					}
				}
			}
		}
	}

	// Token: 0x04003055 RID: 12373
	private static readonly ObjBossMotion.BossMotionParam[] MOTION_DATA = new ObjBossMotion.BossMotionParam[]
	{
		new ObjBossMotion.BossMotionParam("Appear", 6),
		new ObjBossMotion.BossMotionParam("Pass", 6),
		new ObjBossMotion.BossMotionParam("Escape", 6),
		new ObjBossMotion.BossMotionParam("Damage", 4),
		new ObjBossMotion.BossMotionParam("Attack", 3)
	};
}
