using System;
using UnityEngine;

// Token: 0x0200086F RID: 2159
public class ObjBossEggmanMotion : ObjBossMotion
{
	// Token: 0x06003A75 RID: 14965 RVA: 0x001355EC File Offset: 0x001337EC
	protected override void OnSetup()
	{
	}

	// Token: 0x06003A76 RID: 14966 RVA: 0x001355F0 File Offset: 0x001337F0
	public void SetMotion(BossMotion id, bool flag = true)
	{
		if (this.m_animator == null)
		{
			this.m_animator = base.GetComponentInChildren<Animator>();
		}
		if (this.m_animator && (ulong)id < (ulong)((long)ObjBossEggmanMotion.MOTION_DATA.Length))
		{
			string flagName = ObjBossEggmanMotion.MOTION_DATA[(int)((UIntPtr)id)].m_flagName;
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
					BossMotion motionID = (BossMotion)ObjBossEggmanMotion.MOTION_DATA[(int)((UIntPtr)id)].m_motionID;
					if (motionID != BossMotion.NONE)
					{
						this.SetMotion(motionID, false);
					}
				}
			}
		}
	}

	// Token: 0x0400325F RID: 12895
	private static readonly ObjBossMotion.BossMotionParam[] MOTION_DATA = new ObjBossMotion.BossMotionParam[]
	{
		new ObjBossMotion.BossMotionParam("Appear", 11),
		new ObjBossMotion.BossMotionParam("BomStart", 11),
		new ObjBossMotion.BossMotionParam("MissileStart", 9),
		new ObjBossMotion.BossMotionParam("MoveR", 11),
		new ObjBossMotion.BossMotionParam("Notice", 11),
		new ObjBossMotion.BossMotionParam("Pass", 11),
		new ObjBossMotion.BossMotionParam("Escape", 11),
		new ObjBossMotion.BossMotionParam("EscapeStart", 11),
		new ObjBossMotion.BossMotionParam("Damage", 2),
		new ObjBossMotion.BossMotionParam("Attack", 2)
	};
}
