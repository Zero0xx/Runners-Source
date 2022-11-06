using System;
using Boss;

// Token: 0x02000862 RID: 2146
public class ObjBossEggmanFever : ObjBossEggmanBase
{
	// Token: 0x06003A5B RID: 14939 RVA: 0x00133C08 File Offset: 0x00131E08
	public void SetObjBossEggmanFeverParameter(ObjBossEggmanFeverParameter param)
	{
		ObjBossEggmanState component = base.GetComponent<ObjBossEggmanState>();
		if (component != null)
		{
			component.Init();
			component.BossParam.TypeBoss = 0;
			component.BossParam.BossHPMax = param.m_hp;
			component.BossParam.BossDistance = param.m_distance;
			component.BossParam.TableID = param.m_tblId;
			component.BossParam.DownSpeed = param.m_downSpeed;
			component.BossParam.AttackInterspaceMin = param.m_attackInterspaceMin;
			component.BossParam.AttackInterspaceMax = param.m_attackInterspaceMax;
			component.BossParam.BoundParamMin = param.m_boundParamMin;
			component.BossParam.BoundParamMax = param.m_boundParamMax;
			component.BossParam.BoundMaxRand = param.m_boundMaxRand;
			component.BossParam.ShotSpeed = param.m_shotSpeed;
			component.BossParam.BumperFirstSpeed = param.m_bumperFirstSpeed;
			component.BossParam.BumperOutOfcontrol = param.m_bumperOutOfcontrol;
			component.BossParam.BumperSpeedup = param.m_bumperSpeedup;
			component.BossParam.BallSpeed = param.m_ballSpeed;
			component.SetInitState(STATE_ID.AppearFever);
			component.SetDamageState(STATE_ID.DamageFever);
			component.Setup();
		}
	}

	// Token: 0x06003A5C RID: 14940 RVA: 0x00133D40 File Offset: 0x00131F40
	protected override string GetModelName()
	{
		return "enm_eggmobile_f";
	}

	// Token: 0x0400318E RID: 12686
	private const string FeverModelName = "enm_eggmobile_f";
}
