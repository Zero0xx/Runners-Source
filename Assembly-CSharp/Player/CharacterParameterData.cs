using System;
using UnityEngine;

namespace Player
{
	// Token: 0x0200096D RID: 2413
	[ExecuteInEditMode]
	[Serializable]
	public class CharacterParameterData
	{
		// Token: 0x04003603 RID: 13827
		public float m_maxSpeed = 20f;

		// Token: 0x04003604 RID: 13828
		public float m_runAccel = 5f;

		// Token: 0x04003605 RID: 13829
		public float m_runLoopAccel = 10f;

		// Token: 0x04003606 RID: 13830
		public float m_runDec = 2f;

		// Token: 0x04003607 RID: 13831
		public float m_airForwardAccel = 4f;

		// Token: 0x04003608 RID: 13832
		public float m_jumpForce = 13.5f;

		// Token: 0x04003609 RID: 13833
		public float m_jumpAddAcc;

		// Token: 0x0400360A RID: 13834
		public float m_jumpAddSec;

		// Token: 0x0400360B RID: 13835
		public float m_doubleJumpForce = 13.5f;

		// Token: 0x0400360C RID: 13836
		public float m_doubleJumpAddAcc;

		// Token: 0x0400360D RID: 13837
		public float m_doubleJumpAddSec;

		// Token: 0x0400360E RID: 13838
		public float m_spinAttackForce = 12f;

		// Token: 0x0400360F RID: 13839
		public float m_limitHeitht = 13f;

		// Token: 0x04003610 RID: 13840
		public float m_goStumbleMaxHeight = 0.5f;

		// Token: 0x04003611 RID: 13841
		public float m_stumbleJumpForce = 20f;

		// Token: 0x04003612 RID: 13842
		public float m_level1Speed = 8f;

		// Token: 0x04003613 RID: 13843
		public float m_level2Speed = 10f;

		// Token: 0x04003614 RID: 13844
		public float m_level3Speed = 14f;

		// Token: 0x04003615 RID: 13845
		public float m_spinDashSpeed = 15f;

		// Token: 0x04003616 RID: 13846
		public float m_asteroidSpeed = 7.5f;

		// Token: 0x04003617 RID: 13847
		public float m_laserSpeed = 18f;

		// Token: 0x04003618 RID: 13848
		public float m_drillSpeed = 18f;

		// Token: 0x04003619 RID: 13849
		public float m_minLoopRunSpeed = 10f;

		// Token: 0x0400361A RID: 13850
		public float m_lastChanceSpeed = 20f;

		// Token: 0x0400361B RID: 13851
		public float m_asteroidUpForce = 4f;

		// Token: 0x0400361C RID: 13852
		public float m_asteroidDownForce = 4f;

		// Token: 0x0400361D RID: 13853
		public float m_enableStompDec = 85f;

		// Token: 0x0400361E RID: 13854
		public float m_drillJumpForce = 17.5f;

		// Token: 0x0400361F RID: 13855
		public float m_drillJumpGravity = 35f;

		// Token: 0x04003620 RID: 13856
		public float m_laserDrawingTime = 0.2f;

		// Token: 0x04003621 RID: 13857
		public float m_laserWaitDrawingTime = 0.5f;

		// Token: 0x04003622 RID: 13858
		public float m_damageSpeedRate = 0.75f;

		// Token: 0x04003623 RID: 13859
		public float m_damageStumbleTime = 0.75f;

		// Token: 0x04003624 RID: 13860
		public float m_damageEnableJumpTime = 0.75f;

		// Token: 0x04003625 RID: 13861
		public float m_damageInvincibleTime = 1f;

		// Token: 0x04003626 RID: 13862
		public float m_flyUpFirstSpeed = 8f;

		// Token: 0x04003627 RID: 13863
		public float m_flyUpSpeedMax = 8f;

		// Token: 0x04003628 RID: 13864
		public float m_flydownSpeedMax = 6f;

		// Token: 0x04003629 RID: 13865
		public float m_flyUpForce = 6f;

		// Token: 0x0400362A RID: 13866
		public float m_flySpeedRate = 1f;

		// Token: 0x0400362B RID: 13867
		public float m_canFlyTime = 0.5f;

		// Token: 0x0400362C RID: 13868
		public float m_flyGravityRate = 1f;

		// Token: 0x0400362D RID: 13869
		public float m_flyDecSec2ndPress;

		// Token: 0x0400362E RID: 13870
		public float m_powerGrideSpeedRate = 1.2f;

		// Token: 0x0400362F RID: 13871
		public float m_disableGravityTime = 0.3f;

		// Token: 0x04003630 RID: 13872
		public float m_grideTime = 0.4f;

		// Token: 0x04003631 RID: 13873
		public float m_grideGravityRate = 0.2f;

		// Token: 0x04003632 RID: 13874
		public float m_grideFirstUpForce = 1f;

		// Token: 0x04003633 RID: 13875
		public float m_hitWallDeadTime = 0.25f;

		// Token: 0x04003634 RID: 13876
		public float m_hitWallUpSpeedGround = 0.2f;

		// Token: 0x04003635 RID: 13877
		public float m_hitWallUpSpeedAir;
	}
}
