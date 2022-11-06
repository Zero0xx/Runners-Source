using System;
using UnityEngine;

namespace Chao
{
	// Token: 0x02000151 RID: 337
	public class ChaoMovePursue : ChaoMoveBase
	{
		// Token: 0x060009EB RID: 2539 RVA: 0x0003BE50 File Offset: 0x0003A050
		public override void Enter(ChaoMovement context)
		{
			this.m_offsetChao = context.OffsetPosition;
			this.m_preTargetPos = context.TargetPosition + this.m_offsetChao;
			if (context.FromComeIn)
			{
				this.m_basePosition = this.m_preTargetPos;
				this.m_speedOffsetX = 0f;
				this.m_phaseXAxis = ChaoMovePursue.PhaseXAxis.DEFAULT;
			}
			else
			{
				this.m_basePosition = context.Position;
				this.m_speedOffsetX = this.m_basePosition.x - this.m_preTargetPos.x;
				this.m_phaseXAxis = ChaoMovePursue.PhaseXAxis.DEFAULT;
				if (this.m_speedOffsetX < 0f)
				{
					this.m_speedOffsetX = 0f;
				}
			}
			this.m_prePosition = this.m_basePosition;
			this.m_chao_v_acc = context.ParameterData.m_chao_v_acc_speed;
			this.m_chao_v_dec = context.ParameterData.m_chao_v_dec_speed;
			this.m_pursue_v_acc = context.Parameter.Data.m_chao_v_acc;
			this.m_deltaVecY = 0f;
			this.m_speedUpThreshold = 3.5f;
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0003BF54 File Offset: 0x0003A154
		public override void Leave(ChaoMovement context)
		{
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0003BF58 File Offset: 0x0003A158
		public override void Step(ChaoMovement context, float deltaTime)
		{
			this.m_targetPos = context.TargetPosition + this.m_offsetChao;
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
				this.m_phaseXAxis = ChaoMovePursue.PhaseXAxis.DEFAULT;
			}
			this.CalcXPosition(context, deltaTime);
			this.CalcYPosition(context, deltaTime);
			Vector3 preTargetPos = this.m_preTargetPos;
			preTargetPos.x = this.m_targetPos.x - this.m_speedOffsetX;
			preTargetPos.y = this.m_basePosition.y + this.m_speedOffsetY;
			this.m_basePosition = preTargetPos;
			if (this.m_basePosition.x < this.m_prePosition.x)
			{
				this.m_basePosition.x = this.m_prePosition.x;
				if (this.m_phaseXAxis == ChaoMovePursue.PhaseXAxis.CHARA_SPEED_UP)
				{
					this.m_phaseXAxis = ChaoMovePursue.PhaseXAxis.CHARA_SPEED_DOWN;
				}
			}
			context.Position = this.m_basePosition + context.Hovering;
			this.m_preTargetPos = this.m_targetPos;
			this.m_prePosition = this.m_basePosition;
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0003C0D4 File Offset: 0x0003A2D4
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

		// Token: 0x060009EF RID: 2543 RVA: 0x0003C130 File Offset: 0x0003A330
		private bool IsXAxisSpeedUp(ChaoMovement context)
		{
			if (context.PlayerInfo != null)
			{
				float defaultSpeed = context.PlayerInfo.DefaultSpeed;
				return context.PlayerInfo.HorizonVelocity.x > defaultSpeed + this.m_speedUpThreshold;
			}
			return false;
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0003C17C File Offset: 0x0003A37C
		private bool IsXAxisSpeedDown(ChaoMovement context)
		{
			return context.PlayerInfo != null && context.PlayerInfo.HorizonVelocity.x < context.PlayerInfo.DefaultSpeed - 4f;
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0003C1C4 File Offset: 0x0003A3C4
		private void CalcXPosition(ChaoMovement context, float deltaTime)
		{
			bool flag = this.IsXAxisSpeedUp(context);
			if (flag)
			{
				this.m_phaseXAxis = ChaoMovePursue.PhaseXAxis.CHARA_SPEED_UP;
			}
			if (this.m_phaseXAxis == ChaoMovePursue.PhaseXAxis.CHARA_SPEED_UP)
			{
				this.m_speedOffsetX += this.m_chao_v_dec * deltaTime;
				if (this.m_speedOffsetX > 3f)
				{
					this.m_speedOffsetX = 3f;
				}
				if (!flag && this.m_speedOffsetX > 2.9f)
				{
					this.m_phaseXAxis = ChaoMovePursue.PhaseXAxis.CHARA_SPEED_DOWN;
				}
				else if (this.IsXAxisSpeedDown(context))
				{
					this.m_phaseXAxis = ChaoMovePursue.PhaseXAxis.CHARA_SPEED_DOWN;
				}
			}
			else if (this.m_phaseXAxis == ChaoMovePursue.PhaseXAxis.CHARA_SPEED_DOWN)
			{
				this.m_speedOffsetX -= this.m_chao_v_acc * deltaTime;
				if (this.m_speedOffsetX < 0f)
				{
					this.m_speedOffsetX = 0f;
					this.m_phaseXAxis = ChaoMovePursue.PhaseXAxis.DEFAULT;
				}
			}
			else
			{
				this.m_speedOffsetX = 0f;
			}
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0003C2B0 File Offset: 0x0003A4B0
		private bool ChangePhaseY(ChaoMovePursue.PhaseYAxis phase)
		{
			if (this.m_phaseYAxis != phase)
			{
				this.m_phaseYAxis = phase;
				return true;
			}
			return false;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0003C2C8 File Offset: 0x0003A4C8
		private void CalcYPosition(ChaoMovement context, float deltaTime)
		{
			float num = this.m_targetPos.y - this.m_basePosition.y;
			if (this.m_phaseYAxis == ChaoMovePursue.PhaseYAxis.DEFAULT)
			{
				if (num > this.m_offsetChao.y)
				{
					if (this.ChangePhaseY(ChaoMovePursue.PhaseYAxis.CHARA_UP))
					{
						this.m_deltaVecY = 0f;
					}
				}
				else
				{
					if (num >= -0.01f)
					{
						this.m_speedOffsetY = 0f;
						this.ChangePhaseY(ChaoMovePursue.PhaseYAxis.DEFAULT);
						return;
					}
					if (this.ChangePhaseY(ChaoMovePursue.PhaseYAxis.CHARA_DWON))
					{
						this.m_deltaVecY = 0f;
					}
				}
			}
			if (num < 3f)
			{
				this.m_deltaVecY -= this.m_pursue_v_acc * deltaTime;
				if (this.m_deltaVecY < 2f)
				{
					this.m_deltaVecY = 2f;
				}
			}
			else
			{
				this.m_deltaVecY += this.m_pursue_v_acc * deltaTime;
				if (this.m_deltaVecY > 3f)
				{
					this.m_deltaVecY = 3f;
				}
			}
			switch (this.m_phaseYAxis)
			{
			case ChaoMovePursue.PhaseYAxis.CHARA_UP:
			{
				float num2 = this.m_deltaVecY * deltaTime;
				float num3 = num - num2;
				if (num3 < 0f)
				{
					this.m_speedOffsetY = num;
					this.m_phaseYAxis = ChaoMovePursue.PhaseYAxis.DEFAULT;
				}
				else if (num3 > 5f)
				{
					this.m_speedOffsetY = num - 5f;
				}
				else
				{
					this.m_speedOffsetY = num2;
				}
				break;
			}
			case ChaoMovePursue.PhaseYAxis.CHARA_DWON:
			{
				float num4 = this.m_deltaVecY * deltaTime;
				float num5 = num + num4;
				if (num5 > 0f)
				{
					if (num5 > this.m_offsetChao.y)
					{
						this.m_speedOffsetY = -num4;
						this.m_phaseYAxis = ChaoMovePursue.PhaseYAxis.CHARA_UP;
					}
					else
					{
						this.m_speedOffsetY = num;
						this.m_phaseYAxis = ChaoMovePursue.PhaseYAxis.DEFAULT;
					}
				}
				else if (num5 < -5f)
				{
					this.m_speedOffsetY = num + 5f;
				}
				else
				{
					this.m_speedOffsetY = -num4;
				}
				break;
			}
			}
			if (-0.0001 < (double)this.m_speedOffsetY && this.m_speedOffsetY < 0.0001f)
			{
				this.m_speedOffsetY = 0f;
			}
		}

		// Token: 0x0400078E RID: 1934
		private const float SPEED_DOWN_THRESHOLD_DISTANCE = 2.9f;

		// Token: 0x0400078F RID: 1935
		private const float SPEED_DOWN_THRESHOLD_VEC = 4f;

		// Token: 0x04000790 RID: 1936
		private const float SPEED_UP_THRESHOLD_VEC = 3.5f;

		// Token: 0x04000791 RID: 1937
		private const float MAX_OFFESET_X = 3f;

		// Token: 0x04000792 RID: 1938
		private const float MAX_OFFESET_Y = 5f;

		// Token: 0x04000793 RID: 1939
		private const float PURSUE_MAX_SPEED_Y = 3f;

		// Token: 0x04000794 RID: 1940
		private const float PURSUE_MIN_SPEED_Y = 2f;

		// Token: 0x04000795 RID: 1941
		private const float Y_PURSUE_DISTANCE_THRESHOLD = 3f;

		// Token: 0x04000796 RID: 1942
		private float m_deltaVecY;

		// Token: 0x04000797 RID: 1943
		private float m_chao_v_acc = 1f;

		// Token: 0x04000798 RID: 1944
		private float m_chao_v_dec = 1f;

		// Token: 0x04000799 RID: 1945
		private float m_pursue_v_acc = 1f;

		// Token: 0x0400079A RID: 1946
		private float m_speedOffsetX;

		// Token: 0x0400079B RID: 1947
		private float m_speedOffsetY;

		// Token: 0x0400079C RID: 1948
		private float m_speedUpThreshold = 4f;

		// Token: 0x0400079D RID: 1949
		private Vector3 m_targetPos = new Vector3(0f, 0f, 0f);

		// Token: 0x0400079E RID: 1950
		private Vector3 m_preTargetPos = new Vector3(0f, 0f, 0f);

		// Token: 0x0400079F RID: 1951
		private Vector3 m_prePosition = new Vector3(0f, 0f, 0f);

		// Token: 0x040007A0 RID: 1952
		private Vector3 m_basePosition = new Vector3(0f, 0f, 0f);

		// Token: 0x040007A1 RID: 1953
		private Vector3 m_offsetChao = new Vector3(0f, 0f, 0f);

		// Token: 0x040007A2 RID: 1954
		private ChaoMovePursue.PhaseXAxis m_phaseXAxis;

		// Token: 0x040007A3 RID: 1955
		private ChaoMovePursue.PhaseYAxis m_phaseYAxis;

		// Token: 0x02000152 RID: 338
		private enum PhaseXAxis
		{
			// Token: 0x040007A5 RID: 1957
			DEFAULT,
			// Token: 0x040007A6 RID: 1958
			CHARA_SPEED_UP,
			// Token: 0x040007A7 RID: 1959
			CHARA_SPEED_DOWN
		}

		// Token: 0x02000153 RID: 339
		private enum PhaseYAxis
		{
			// Token: 0x040007A9 RID: 1961
			DEFAULT,
			// Token: 0x040007AA RID: 1962
			CHARA_UP,
			// Token: 0x040007AB RID: 1963
			CHARA_DWON
		}
	}
}
