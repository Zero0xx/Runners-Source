using System;
using UnityEngine;

namespace Boss
{
	// Token: 0x020008A0 RID: 2208
	public class BossStateDamageBase : FSMState<ObjBossEggmanState>
	{
		// Token: 0x06003B43 RID: 15171 RVA: 0x00139A3C File Offset: 0x00137C3C
		public override void Enter(ObjBossEggmanState context)
		{
			context.DebugDrawState(this.GetStateName());
			context.SetHitCheck(false);
			context.AddDamage();
			context.BossEffect.PlayHitEffect();
			context.BossEffect.PlayBoostEffect(ObjBossEggmanEffect.BoostType.Normal);
			if (context.BossParam.BossHP > 0)
			{
				ObjUtil.PlaySE("boss_damage", "SE");
			}
			else
			{
				ObjUtil.PlaySE("boss_explosion", "SE");
				context.BossClear();
			}
			this.SetMotion(context, true);
			this.m_state = BossStateDamageBase.State.SpeedDown;
			this.m_start_rot = context.transform.rotation;
			this.m_speed_down = 0f;
			this.m_rot_speed = 0f;
			this.m_rot_speed_down = 0f;
			this.m_rot_time = 0f;
			this.m_rot_min = 0f;
			this.m_rot_agl = context.transform.right;
			this.m_distance = 0f;
			this.m_rot_end = false;
			this.Setup(context);
			if (context.ColorPowerHit)
			{
				context.DebugDrawState(this.GetStateName() + "ColorPowerHit");
				this.SetupColorPowerDamage(context);
			}
			else
			{
				context.CreateFeverRing();
			}
		}

		// Token: 0x06003B44 RID: 15172 RVA: 0x00139B6C File Offset: 0x00137D6C
		public override void Leave(ObjBossEggmanState context)
		{
			context.ColorPowerHit = false;
			context.ChaoHit = false;
		}

		// Token: 0x06003B45 RID: 15173 RVA: 0x00139B7C File Offset: 0x00137D7C
		public override void Step(ObjBossEggmanState context, float delta)
		{
			context.UpdateSpeedDown(delta, this.m_speed_down);
			if (!this.m_rot_end)
			{
				float d = delta * 60f * this.m_rot_speed;
				this.m_rot_speed -= delta * 60f * this.m_rot_speed_down;
				context.transform.rotation = Quaternion.Euler(this.m_rot_agl * d) * context.transform.rotation;
				float x = context.transform.rotation.eulerAngles.x;
				if (this.m_rot_speed < this.m_rot_min && x > 270f && x < 359f)
				{
					this.SetMotion(context, false);
					this.m_rot_time = 0f;
					this.m_rot_end = true;
				}
			}
			if (this.m_rot_end)
			{
				this.m_rot_time += delta * 5f;
				context.transform.rotation = Quaternion.Slerp(context.transform.rotation, this.m_start_rot, this.m_rot_time);
			}
			BossStateDamageBase.State state = this.m_state;
			if (state != BossStateDamageBase.State.SpeedDown)
			{
				if (state == BossStateDamageBase.State.Wait)
				{
					if (context.IsPlayerDead() && context.IsClear())
					{
						this.ChangeStateWait(context);
					}
					else
					{
						float playerDistance = context.GetPlayerDistance();
						if (playerDistance < this.m_distance)
						{
							context.SetSpeed(context.BossParam.PlayerSpeed * 0.7f);
							context.transform.rotation = this.m_start_rot;
							if (!this.m_rot_end)
							{
								this.SetMotion(context, false);
							}
							this.ChangeStateWait(context);
						}
					}
				}
			}
			else if (context.BossParam.Speed < context.BossParam.PlayerSpeed)
			{
				this.SetStateSpeedDown(context);
				this.m_state = BossStateDamageBase.State.Wait;
			}
		}

		// Token: 0x06003B46 RID: 15174 RVA: 0x00139D64 File Offset: 0x00137F64
		protected virtual string GetStateName()
		{
			return string.Empty;
		}

		// Token: 0x06003B47 RID: 15175 RVA: 0x00139D6C File Offset: 0x00137F6C
		protected virtual void Setup(ObjBossEggmanState context)
		{
		}

		// Token: 0x06003B48 RID: 15176 RVA: 0x00139D70 File Offset: 0x00137F70
		private void SetupColorPowerDamage(ObjBossEggmanState context)
		{
			float damageSpeedParam = context.GetDamageSpeedParam();
			context.SetSpeed(60f * damageSpeedParam);
			this.SetSpeedDown(120f);
			this.SetRotSpeed(30f);
			this.SetRotSpeedDown(0.3f);
			this.SetRotMin(10f);
			this.SetRotAngle(-context.transform.right);
		}

		// Token: 0x06003B49 RID: 15177 RVA: 0x00139DD4 File Offset: 0x00137FD4
		private void SetMotion(ObjBossEggmanState context, bool flag)
		{
			if (flag)
			{
				context.BossMotion.SetMotion(BossMotion.DAMAGE_R_HC, flag);
			}
			else if (context.BossParam.BossHP > 0)
			{
				context.BossMotion.SetMotion(BossMotion.DAMAGE_R_HC, false);
			}
			else
			{
				context.BossEffect.PlayEscapeEffect(context);
				context.BossMotion.SetMotion(BossMotion.ESCAPE, true);
			}
		}

		// Token: 0x06003B4A RID: 15178 RVA: 0x00139E38 File Offset: 0x00138038
		protected virtual void SetStateSpeedDown(ObjBossEggmanState context)
		{
		}

		// Token: 0x06003B4B RID: 15179 RVA: 0x00139E3C File Offset: 0x0013803C
		protected virtual void ChangeStateWait(ObjBossEggmanState context)
		{
		}

		// Token: 0x06003B4C RID: 15180 RVA: 0x00139E40 File Offset: 0x00138040
		protected void SetRotAngle(Vector3 angle)
		{
			this.m_rot_agl = angle;
		}

		// Token: 0x06003B4D RID: 15181 RVA: 0x00139E4C File Offset: 0x0013804C
		protected void SetSpeedDown(float val)
		{
			this.m_speed_down = val;
		}

		// Token: 0x06003B4E RID: 15182 RVA: 0x00139E58 File Offset: 0x00138058
		protected void SetRotSpeed(float val)
		{
			this.m_rot_speed = val;
		}

		// Token: 0x06003B4F RID: 15183 RVA: 0x00139E64 File Offset: 0x00138064
		protected void SetRotSpeedDown(float val)
		{
			this.m_rot_speed_down = val;
		}

		// Token: 0x06003B50 RID: 15184 RVA: 0x00139E70 File Offset: 0x00138070
		protected void SetRotMin(float val)
		{
			this.m_rot_min = val;
		}

		// Token: 0x06003B51 RID: 15185 RVA: 0x00139E7C File Offset: 0x0013807C
		protected void SetDistance(float val)
		{
			this.m_distance = val;
		}

		// Token: 0x04003390 RID: 13200
		private const float START_SPEED = 60f;

		// Token: 0x04003391 RID: 13201
		private const float START_DOWNSPEED = 120f;

		// Token: 0x04003392 RID: 13202
		private const float ROT_SPEED = 30f;

		// Token: 0x04003393 RID: 13203
		private const float ROT_DOWNSPEED = 0.3f;

		// Token: 0x04003394 RID: 13204
		private const float ROT_MIN = 10f;

		// Token: 0x04003395 RID: 13205
		private BossStateDamageBase.State m_state;

		// Token: 0x04003396 RID: 13206
		private Quaternion m_start_rot = Quaternion.identity;

		// Token: 0x04003397 RID: 13207
		private float m_speed_down;

		// Token: 0x04003398 RID: 13208
		private float m_rot_speed;

		// Token: 0x04003399 RID: 13209
		private float m_rot_speed_down;

		// Token: 0x0400339A RID: 13210
		private float m_rot_time;

		// Token: 0x0400339B RID: 13211
		private float m_rot_min;

		// Token: 0x0400339C RID: 13212
		private Vector3 m_rot_agl = Vector3.zero;

		// Token: 0x0400339D RID: 13213
		private float m_distance;

		// Token: 0x0400339E RID: 13214
		private bool m_rot_end;

		// Token: 0x020008A1 RID: 2209
		private enum State
		{
			// Token: 0x040033A0 RID: 13216
			Idle,
			// Token: 0x040033A1 RID: 13217
			SpeedDown,
			// Token: 0x040033A2 RID: 13218
			Wait
		}
	}
}
