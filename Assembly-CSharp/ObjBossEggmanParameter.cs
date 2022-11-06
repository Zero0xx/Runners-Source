using System;

// Token: 0x02000870 RID: 2160
public class ObjBossEggmanParameter : ObjBossParameter
{
	// Token: 0x06003A78 RID: 14968 RVA: 0x001356CC File Offset: 0x001338CC
	protected override void OnSetup()
	{
		base.BossAttackPower = 1;
		if (base.TypeBoss == 0)
		{
			base.BossHP = base.BossHPMax;
		}
		else
		{
			int num = 0;
			LevelInformation levelInformation = ObjUtil.GetLevelInformation();
			if (levelInformation != null)
			{
				levelInformation.NumBossHpMax = base.BossHPMax;
				num = base.BossHPMax - levelInformation.NumBossAttack;
			}
			if (num < 1)
			{
				num = 1;
			}
			base.BossHP = num;
		}
	}
}
