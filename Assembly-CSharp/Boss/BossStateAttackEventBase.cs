using System;
using UnityEngine;

namespace Boss
{
	// Token: 0x02000896 RID: 2198
	public class BossStateAttackEventBase : FSMState<ObjBossEventBossState>
	{
		// Token: 0x06003B1F RID: 15135 RVA: 0x001385E8 File Offset: 0x001367E8
		public override void Enter(ObjBossEventBossState context)
		{
			context.SetHitCheck(true);
			context.BossMotion.SetMotion(EventBossMotion.ATTACK, true);
			this.m_time = 0f;
			this.m_bumperTime.ResetParam(0f, context.BossParam.BumperInterspace * 0.1f);
			this.m_speed_up = 0f;
			this.m_speed_down = 0f;
			this.m_missileTime.ResetParam(0f, context.BossParam.MissileInterspace * 0.1f);
			this.m_missileWaitTime.ResetParam(0f, context.BossParam.MissileWaitTime * 0.1f);
			this.m_missileCount = 0;
			this.m_trapBallTime.ResetParam(0f, context.BossParam.AttackInterspaceMin * 0.1f);
			this.m_trapBallWaitTime.ResetParam(0f, context.BossParam.AttackInterspaceMax * 0.1f);
			this.m_trapBallCount = 0;
			this.m_trapBallData = null;
		}

		// Token: 0x06003B20 RID: 15136 RVA: 0x001386E4 File Offset: 0x001368E4
		public override void Leave(ObjBossEventBossState context)
		{
			context.SetHitCheck(false);
		}

		// Token: 0x06003B21 RID: 15137 RVA: 0x001386F0 File Offset: 0x001368F0
		protected bool UpdateBoost(ObjBossEventBossState context, float delta)
		{
			float playerBossPositionX = context.GetPlayerBossPositionX();
			if (playerBossPositionX < 2f)
			{
				context.SetSpeed(this.GetBoostSpeed(context, WispBoostLevel.LEVEL3));
			}
			else
			{
				context.SetSpeed(this.GetBoostSpeed(context, context.BossParam.BoostLevel));
			}
			if (playerBossPositionX < 0f && Mathf.Abs(playerBossPositionX) > 6f)
			{
				Vector3 position = new Vector3(context.transform.position.x + 26f, context.BossParam.StartPos.y, context.transform.position.z);
				context.transform.position = position;
				return true;
			}
			return false;
		}

		// Token: 0x06003B22 RID: 15138 RVA: 0x001387AC File Offset: 0x001369AC
		protected bool UpdateBumper(ObjBossEventBossState context, float delta)
		{
			this.m_bumperTime.m_time += delta;
			if (this.m_bumperTime.m_time > this.m_bumperTime.m_timeMax)
			{
				this.m_bumperTime.m_time = 0f;
				this.m_bumperTime.m_timeMax = context.BossParam.BumperInterspace;
				int randomRange = ObjUtil.GetRandomRange100();
				if (randomRange < context.BossParam.BumperRand)
				{
					context.CreateBumper(false, 15f);
				}
			}
			if (context.IsHitBumper())
			{
				float playerBossPositionX = context.GetPlayerBossPositionX();
				if (playerBossPositionX < 10f)
				{
					this.m_speed_up = context.BossParam.BumperSpeedup;
					context.SetSpeed(this.m_speed_up * 0.1f);
					this.m_speed_down = 0f;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003B23 RID: 15139 RVA: 0x00138880 File Offset: 0x00136A80
		protected bool UpdateBumperSpeedup(ObjBossEventBossState context, float delta)
		{
			context.UpdateSpeedDown(delta, this.m_speed_down);
			float num = this.GetBoostSpeed(context, context.BossParam.BoostLevel) * 0.7f;
			if (context.BossParam.Speed < num)
			{
				context.SetSpeed(num);
				return true;
			}
			this.m_speed_down += delta * this.m_speed_up * 1.2f;
			return false;
		}

		// Token: 0x06003B24 RID: 15140 RVA: 0x001388EC File Offset: 0x00136AEC
		protected void UpdateMissile(ObjBossEventBossState context, float delta)
		{
			if (context.BossParam.MissileCount <= 0)
			{
				return;
			}
			this.m_missileWaitTime.m_time += delta;
			if (this.m_missileWaitTime.m_time > this.m_missileWaitTime.m_timeMax)
			{
				this.m_missileTime.m_time += delta;
				if (this.m_missileTime.m_time > this.m_missileTime.m_timeMax)
				{
					this.m_missileTime.m_time = 0f;
					this.m_missileTime.m_timeMax = context.BossParam.MissileInterspace;
					int num = 2;
					int randomRange = ObjUtil.GetRandomRange100();
					int missilePos = context.BossParam.MissilePos1;
					int missilePos2 = context.BossParam.MissilePos2;
					if (randomRange < missilePos)
					{
						num = 0;
					}
					else if (randomRange < missilePos + missilePos2)
					{
						num = 1;
					}
					float y = BossStateAttackEventBase.MISSILE_POSY[num];
					Vector3 pos = new Vector3(context.GetPlayerPosition().x + 17f, y, context.transform.position.z);
					context.CreateMissile(pos);
					this.m_missileCount++;
					if (this.m_missileCount >= context.BossParam.MissileCount)
					{
						this.m_missileCount = 0;
						this.m_missileTime.m_time = 0f;
						this.m_missileTime.m_timeMax = context.BossParam.MissileWaitTime;
					}
				}
			}
		}

		// Token: 0x06003B25 RID: 15141 RVA: 0x00138A5C File Offset: 0x00136C5C
		protected void UpdateTrapBall(ObjBossEventBossState context, float delta)
		{
			this.m_trapBallWaitTime.m_time += delta;
			if (this.m_trapBallWaitTime.m_time > this.m_trapBallWaitTime.m_timeMax)
			{
				this.m_trapBallTime.m_time += delta;
				if (this.m_trapBallTime.m_time > this.m_trapBallTime.m_timeMax)
				{
					this.m_trapBallTime.m_time = 0f;
					this.m_trapBallTime.m_timeMax = context.BossParam.AttackInterspaceMin;
					if (this.m_trapBallData == null)
					{
						this.m_trapBallData = context.BossParam.GetMap3AttackData();
					}
					Map3AttackData trapBallData = this.m_trapBallData;
					if (trapBallData != null)
					{
						context.BossParam.AttackBallFlag = true;
						if (this.m_trapBallCount == 0)
						{
							context.CreateTrapBall(context.BossParam.GetMap3BomTblA(trapBallData.m_type), BossStateAttackEventBase.TRAPBLL_POSY[(int)trapBallData.m_posA], 0, false);
						}
						else
						{
							context.CreateTrapBall(context.BossParam.GetMap3BomTblB(trapBallData.m_type), BossStateAttackEventBase.TRAPBLL_POSY[(int)trapBallData.m_posB], 0, false);
						}
						this.m_trapBallCount++;
						if (this.m_trapBallCount >= trapBallData.GetAttackCount())
						{
							this.m_trapBallData = null;
							this.m_trapBallCount = 0;
							this.m_trapBallWaitTime.m_time = 0f;
							this.m_trapBallWaitTime.m_timeMax = context.BossParam.AttackInterspaceMax;
						}
					}
				}
			}
		}

		// Token: 0x06003B26 RID: 15142 RVA: 0x00138BD0 File Offset: 0x00136DD0
		private float GetBoostSpeed(ObjBossEventBossState context, WispBoostLevel level)
		{
			if (level == WispBoostLevel.NONE)
			{
				return context.BossParam.PlayerSpeed;
			}
			float boostSpeedParam = context.BossParam.GetBoostSpeedParam(level);
			return context.BossParam.PlayerSpeed - boostSpeedParam;
		}

		// Token: 0x06003B27 RID: 15143 RVA: 0x00138C0C File Offset: 0x00136E0C
		protected bool UpdateTime(float delta, float time_max)
		{
			this.m_time += delta;
			return this.m_time > time_max;
		}

		// Token: 0x06003B28 RID: 15144 RVA: 0x00138C2C File Offset: 0x00136E2C
		protected void ResetTime()
		{
			this.m_time = 0f;
		}

		// Token: 0x0400333C RID: 13116
		private const float PASS_DISTANCE = 6f;

		// Token: 0x0400333D RID: 13117
		private const float PASS_DISTANCE2 = 2f;

		// Token: 0x0400333E RID: 13118
		private const float PASS_WARP_DISTANCE = 26f;

		// Token: 0x0400333F RID: 13119
		private const float MISSILE_POSX = 17f;

		// Token: 0x04003340 RID: 13120
		private const float BUMPER_POSX = 15f;

		// Token: 0x04003341 RID: 13121
		private const float SPEEDUP_DISTANCE = 10f;

		// Token: 0x04003342 RID: 13122
		private static readonly float[] MISSILE_POSY = new float[]
		{
			1f,
			2f,
			3f
		};

		// Token: 0x04003343 RID: 13123
		private static readonly float[] TRAPBLL_POSY = new float[]
		{
			1f,
			1f,
			2f,
			3f
		};

		// Token: 0x04003344 RID: 13124
		private float m_time;

		// Token: 0x04003345 RID: 13125
		private BossStateAttackEventBase.TimeParam m_bumperTime = new BossStateAttackEventBase.TimeParam();

		// Token: 0x04003346 RID: 13126
		private float m_speed_up;

		// Token: 0x04003347 RID: 13127
		private float m_speed_down;

		// Token: 0x04003348 RID: 13128
		private BossStateAttackEventBase.TimeParam m_missileTime = new BossStateAttackEventBase.TimeParam();

		// Token: 0x04003349 RID: 13129
		private BossStateAttackEventBase.TimeParam m_missileWaitTime = new BossStateAttackEventBase.TimeParam();

		// Token: 0x0400334A RID: 13130
		private int m_missileCount;

		// Token: 0x0400334B RID: 13131
		private BossStateAttackEventBase.TimeParam m_trapBallTime = new BossStateAttackEventBase.TimeParam();

		// Token: 0x0400334C RID: 13132
		private BossStateAttackEventBase.TimeParam m_trapBallWaitTime = new BossStateAttackEventBase.TimeParam();

		// Token: 0x0400334D RID: 13133
		private int m_trapBallCount;

		// Token: 0x0400334E RID: 13134
		private Map3AttackData m_trapBallData;

		// Token: 0x02000897 RID: 2199
		public class TimeParam
		{
			// Token: 0x06003B29 RID: 15145 RVA: 0x00138C3C File Offset: 0x00136E3C
			public TimeParam()
			{
				this.ResetParam(0f, 0f);
			}

			// Token: 0x06003B2A RID: 15146 RVA: 0x00138C54 File Offset: 0x00136E54
			public void ResetParam(float time, float timeMax)
			{
				this.m_time = time;
				this.m_timeMax = timeMax;
			}

			// Token: 0x0400334F RID: 13135
			public float m_time;

			// Token: 0x04003350 RID: 13136
			public float m_timeMax;
		}
	}
}
