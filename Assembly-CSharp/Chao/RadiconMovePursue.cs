using System;
using UnityEngine;

namespace Chao
{
	// Token: 0x0200015D RID: 349
	public class RadiconMovePursue : ChaoMoveBase
	{
		// Token: 0x06000A34 RID: 2612 RVA: 0x0003CFC4 File Offset: 0x0003B1C4
		public override void Enter(ChaoMovement context)
		{
			this.m_offsetRadicon = context.OffsetPosition;
			this.m_preTargetPos = context.TargetPosition + this.m_offsetRadicon;
			if (context.FromComeIn)
			{
				this.m_basePosition = this.m_preTargetPos;
				this.m_speedOffsetX = 0f;
				this.m_phaseXAxis = RadiconMovePursue.PhaseXAxis.DEFAULT;
			}
			else
			{
				this.m_basePosition = context.Position;
				this.m_speedOffsetX = this.m_basePosition.x - this.m_preTargetPos.x;
				this.m_phaseXAxis = RadiconMovePursue.PhaseXAxis.DEFAULT;
				if (this.m_speedOffsetX < 0f)
				{
					this.m_speedOffsetX = 0f;
				}
			}
			this.m_prePosition = this.m_basePosition;
			this.m_vertSpeed = (Vector3.Dot(context.MovedVelocity, ChaoMovement.VertDir) * ChaoMovement.VertDir).magnitude;
			this.m_radicon_v_acc = context.ParameterData.m_radicon_v_acc_speed;
			this.m_radicon_v_dec = context.ParameterData.m_radicon_v_dec_speed;
			this.m_radicon_v_vel = context.ParameterData.m_radicon_v_vel;
			this.m_speedUpThreshold = 3f;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0003D0E0 File Offset: 0x0003B2E0
		public override void Leave(ChaoMovement context)
		{
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0003D0E4 File Offset: 0x0003B2E4
		public override void Step(ChaoMovement context, float deltaTime)
		{
			this.m_targetPos = context.TargetPosition + this.m_offsetRadicon;
			if (!context.IsPlyayerMoved)
			{
				context.Position = this.m_basePosition + context.Hovering;
				this.m_preTargetPos = this.m_targetPos;
				return;
			}
			if (deltaTime <= 0f)
			{
				return;
			}
			if ((this.m_targetPos - this.m_preTargetPos).x < -100f)
			{
				this.m_preTargetPos.x = 0f;
				Vector3 vector = context.TargetPosition - this.m_preTargetPos;
				this.m_speedOffsetX = 0f;
				this.m_phaseXAxis = RadiconMovePursue.PhaseXAxis.DEFAULT;
			}
			Vector3 vector2 = this.m_targetPos - this.m_basePosition;
			Vector3 b = Vector3.Dot(vector2, ChaoMovement.HorzDir) * ChaoMovement.HorzDir;
			Vector3 subVert = vector2 - b;
			this.CalcXPosition(context, deltaTime);
			this.CalcVertVelocity(context, deltaTime, subVert);
			Vector3 prePosition = this.m_prePosition;
			prePosition.x = this.m_targetPos.x - this.m_speedOffsetX;
			prePosition.y = this.m_prePosition.y + deltaTime * this.m_vertSpeed * subVert.y;
			this.m_basePosition = prePosition;
			if (this.m_basePosition.x < this.m_prePosition.x)
			{
				this.m_basePosition.x = this.m_prePosition.x;
				if (this.m_phaseXAxis == RadiconMovePursue.PhaseXAxis.CHARA_SPEED_UP)
				{
					this.m_phaseXAxis = RadiconMovePursue.PhaseXAxis.CHARA_SPEED_DOWN;
				}
			}
			context.Position = this.m_basePosition + context.Hovering;
			this.m_preTargetPos = this.m_targetPos;
			this.m_prePosition = this.m_basePosition;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0003D29C File Offset: 0x0003B49C
		private bool IsXAxisSpeedUp(ChaoMovement context)
		{
			if (context.PlayerInfo != null)
			{
				float defaultSpeed = context.PlayerInfo.DefaultSpeed;
				return context.PlayerInfo.HorizonVelocity.x > defaultSpeed + this.m_speedUpThreshold;
			}
			return false;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0003D2E8 File Offset: 0x0003B4E8
		private bool IsXAxisSpeedDown(ChaoMovement context)
		{
			return context.PlayerInfo != null && context.PlayerInfo.HorizonVelocity.x < context.PlayerInfo.DefaultSpeed - 4f;
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0003D330 File Offset: 0x0003B530
		private void SetSpeedUpThreshold()
		{
			StageAbilityManager instance = StageAbilityManager.Instance;
			if (instance != null)
			{
				float num = 1f;
				if (instance.IsEasySpeed(PlayingCharacterType.MAIN))
				{
					this.m_speedUpThreshold += num;
				}
				if (instance.IsEasySpeed(PlayingCharacterType.SUB))
				{
					this.m_speedUpThreshold += num;
				}
			}
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0003D38C File Offset: 0x0003B58C
		private void CalcXPosition(ChaoMovement context, float deltaTime)
		{
			bool flag = this.IsXAxisSpeedUp(context);
			if (flag)
			{
				this.m_phaseXAxis = RadiconMovePursue.PhaseXAxis.CHARA_SPEED_UP;
			}
			if (this.m_phaseXAxis == RadiconMovePursue.PhaseXAxis.CHARA_SPEED_UP)
			{
				this.m_speedOffsetX += this.m_radicon_v_dec * deltaTime;
				if (this.m_speedOffsetX > 3f)
				{
					this.m_speedOffsetX = 3f;
				}
				if (!flag && this.m_speedOffsetX > 2.9f)
				{
					this.m_phaseXAxis = RadiconMovePursue.PhaseXAxis.CHARA_SPEED_DOWN;
				}
				else if (this.IsXAxisSpeedDown(context))
				{
					this.m_phaseXAxis = RadiconMovePursue.PhaseXAxis.CHARA_SPEED_DOWN;
				}
			}
			else if (this.m_phaseXAxis == RadiconMovePursue.PhaseXAxis.CHARA_SPEED_DOWN)
			{
				this.m_speedOffsetX -= this.m_radicon_v_acc * deltaTime;
				if (this.m_speedOffsetX < 0f)
				{
					this.m_speedOffsetX = 0f;
					this.m_phaseXAxis = RadiconMovePursue.PhaseXAxis.DEFAULT;
				}
			}
			else
			{
				this.m_speedOffsetX = 0f;
			}
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0003D478 File Offset: 0x0003B678
		private void CalcVertVelocity(ChaoMovement context, float deltaTime, Vector3 subVert)
		{
			float magnitude = subVert.magnitude;
			if (this.m_vertSpeed < 0.05f)
			{
				if (magnitude > 2.5f)
				{
					this.m_vertSpeed = 0.05f;
				}
			}
			else
			{
				float num = 7f;
				if (magnitude < this.m_vertSpeed * deltaTime)
				{
					this.m_vertSpeed = 0f;
				}
				else if (magnitude > 7f)
				{
					this.m_vertSpeed = this.m_radicon_v_vel;
				}
				else
				{
					this.m_vertSpeed = Mathf.Min(this.m_vertSpeed, this.m_radicon_v_vel);
					float num2 = Mathf.Lerp(0f, this.m_radicon_v_vel, magnitude / num);
					if (this.m_vertSpeed < num2)
					{
						this.m_vertSpeed = Mathf.MoveTowards(this.m_vertSpeed, num2, this.m_radicon_v_acc * deltaTime);
					}
					else
					{
						this.m_vertSpeed = Mathf.MoveTowards(this.m_vertSpeed, num2, this.m_radicon_v_dec * deltaTime);
					}
				}
			}
		}

		// Token: 0x040007D4 RID: 2004
		private const float SPEED_UP_THRESHOLD_VEC = 3f;

		// Token: 0x040007D5 RID: 2005
		private const float MAX_OFFESET_X = 3f;

		// Token: 0x040007D6 RID: 2006
		private const float MAX_OFFESET_Y = 5f;

		// Token: 0x040007D7 RID: 2007
		private const float SPEED_DOWN_THRESHOLD_DISTANCE = 2.9f;

		// Token: 0x040007D8 RID: 2008
		private const float SPEED_DOWN_THRESHOLD_VEC = 4f;

		// Token: 0x040007D9 RID: 2009
		private const float MaxDist_V = 7f;

		// Token: 0x040007DA RID: 2010
		private const float FirstV_Speed = 0.05f;

		// Token: 0x040007DB RID: 2011
		private const float VertPursue_Height = 2.5f;

		// Token: 0x040007DC RID: 2012
		private float m_speedOffsetX;

		// Token: 0x040007DD RID: 2013
		private float m_vertSpeed;

		// Token: 0x040007DE RID: 2014
		private float m_radicon_v_acc = 1f;

		// Token: 0x040007DF RID: 2015
		private float m_radicon_v_dec = 1f;

		// Token: 0x040007E0 RID: 2016
		private float m_radicon_v_vel = 1f;

		// Token: 0x040007E1 RID: 2017
		private float m_speedUpThreshold = 4f;

		// Token: 0x040007E2 RID: 2018
		private Vector3 m_targetPos = new Vector3(0f, 0f, 0f);

		// Token: 0x040007E3 RID: 2019
		private Vector3 m_preTargetPos = new Vector3(0f, 0f, 0f);

		// Token: 0x040007E4 RID: 2020
		private Vector3 m_prePosition = new Vector3(0f, 0f, 0f);

		// Token: 0x040007E5 RID: 2021
		private Vector3 m_basePosition = new Vector3(0f, 0f, 0f);

		// Token: 0x040007E6 RID: 2022
		private Vector3 m_offsetRadicon = new Vector3(0f, 0f, 0f);

		// Token: 0x040007E7 RID: 2023
		private RadiconMovePursue.PhaseXAxis m_phaseXAxis;

		// Token: 0x0200015E RID: 350
		private enum PhaseXAxis
		{
			// Token: 0x040007E9 RID: 2025
			DEFAULT,
			// Token: 0x040007EA RID: 2026
			CHARA_SPEED_UP,
			// Token: 0x040007EB RID: 2027
			CHARA_SPEED_DOWN
		}
	}
}
