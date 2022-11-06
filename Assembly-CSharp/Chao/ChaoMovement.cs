using System;
using App;
using Player;
using UnityEngine;

namespace Chao
{
	// Token: 0x0200015C RID: 348
	public class ChaoMovement : MonoBehaviour
	{
		// Token: 0x06000A0D RID: 2573 RVA: 0x0003C968 File Offset: 0x0003AB68
		private void Start()
		{
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0003C96C File Offset: 0x0003AB6C
		private void Update()
		{
			float deltaTime = Time.deltaTime;
			if (App.Math.NearZero(deltaTime, 1E-06f))
			{
				return;
			}
			Vector3 position = this.Position;
			if (this.m_player_info != null)
			{
				this.m_target_pos = this.m_player_info.Position;
				this.m_target_pos.z = 0f;
			}
			if (this.m_fsm != null)
			{
				this.m_fsm.CurrentState.Step(this, deltaTime);
			}
			this.m_moved_velocity = (this.Position - position) / deltaTime;
			if (this.m_player_info != null)
			{
				this.m_prevPlayerPos = this.PlayerInfo.Position;
			}
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0003CA20 File Offset: 0x0003AC20
		private void OnDestroy()
		{
			if (this.m_fsm != null && this.m_fsm.CurrentState != null)
			{
				this.m_fsm.CurrentState.Leave(this);
				this.m_fsm = null;
			}
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0003CA58 File Offset: 0x0003AC58
		public void ChangeState(MOVESTATE_ID state)
		{
			this.m_next_state_flag = false;
			if (this.m_fsm != null && this.m_fsm.CurrentStateID != (StateID)state)
			{
				this.m_fromComeIn = (this.m_fsm.CurrentStateID == (StateID)3);
				this.m_fsm.ChangeState(this, (int)state);
			}
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0003CAAC File Offset: 0x0003ACAC
		public static ChaoMovement Create(GameObject gameObject, ChaoSetupParameterData parameter)
		{
			ChaoMovement chaoMovement = gameObject.AddComponent<ChaoMovement>();
			FSMStateFactory<ChaoMovement>[] movementStateTable = MovementSetupChao.GetMovementStateTable();
			ChaoMovementType moveType = parameter.MoveType;
			if (moveType != ChaoMovementType.CHAO)
			{
				if (moveType == ChaoMovementType.RADICON)
				{
					movementStateTable = MovementSetupRadicon.GetMovementStateTable();
				}
			}
			chaoMovement.Setup(parameter, movementStateTable);
			return chaoMovement;
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0003CAF8 File Offset: 0x0003ACF8
		private void Setup(ChaoSetupParameterData parameter, FSMStateFactory<ChaoMovement>[] movementTable)
		{
			this.SetupBase(parameter);
			this.SetupFsm(movementTable);
			this.CreateHovering(parameter);
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0003CB10 File Offset: 0x0003AD10
		private void SetupFsm(FSMStateFactory<ChaoMovement>[] fsmtable)
		{
			this.m_fsm = new FSMSystem<ChaoMovement>();
			if (this.m_fsm != null && fsmtable != null)
			{
				foreach (FSMStateFactory<ChaoMovement> fsmstateFactory in fsmtable)
				{
					this.m_fsm.AddState(fsmstateFactory.stateID, fsmstateFactory.state);
				}
				this.m_fsm.Init(this, 3);
			}
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0003CB78 File Offset: 0x0003AD78
		private void SetupBase(ChaoSetupParameterData setupParameter)
		{
			this.m_player_info = ObjUtil.GetPlayerInformation();
			if (this.m_player_info != null)
			{
				this.m_prevPlayerPos = this.m_player_info.Position;
			}
			GameObject gameObject = GameObject.Find("StageChao/ChaoParameter");
			if (gameObject != null)
			{
				this.m_chao_param = gameObject.GetComponent<ChaoParameter>();
			}
			ChaoType chaoType = ChaoUtility.GetChaoType(base.gameObject);
			if (setupParameter != null)
			{
				ChaoType chaoType2 = chaoType;
				if (chaoType2 != ChaoType.MAIN)
				{
					if (chaoType2 == ChaoType.SUB)
					{
						this.OffsetPosition = setupParameter.SubOffset;
					}
				}
				else
				{
					this.OffsetPosition = setupParameter.MainOffset;
				}
				if (setupParameter.MoveType == ChaoMovementType.RADICON)
				{
					this.OffsetPosition += this.m_offsetRadicon;
				}
			}
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0003CC40 File Offset: 0x0003AE40
		private void CreateHovering(ChaoSetupParameterData setupParameter)
		{
			ChaoHoverType hoverType = setupParameter.HoverType;
			if (hoverType == ChaoHoverType.CHAO)
			{
				this.CreateChaoHover(setupParameter);
			}
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0003CC6C File Offset: 0x0003AE6C
		private void CreateChaoHover(ChaoSetupParameterData setupParameter)
		{
			ChaoHovering chaoHovering = base.gameObject.AddComponent<ChaoHovering>();
			ChaoHovering.CInfo cinfo = new ChaoHovering.CInfo(this);
			cinfo.height = setupParameter.HoverHeight;
			cinfo.speed = setupParameter.HoverSpeed;
			ChaoType chaoType = ChaoUtility.GetChaoType(base.gameObject);
			ChaoType chaoType2 = chaoType;
			if (chaoType2 != ChaoType.MAIN)
			{
				if (chaoType2 == ChaoType.SUB)
				{
					cinfo.startAngle = setupParameter.HoverStartDegreeSub * 0.017453292f;
				}
			}
			else
			{
				cinfo.startAngle = setupParameter.HoverStartDegreeMain * 0.017453292f;
			}
			chaoHovering.Setup(cinfo);
			this.SetHoveringMove(chaoHovering);
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0003CD00 File Offset: 0x0003AF00
		public T GetCurrentState<T>() where T : FSMState<ChaoMovement>
		{
			if (this.m_fsm == null)
			{
				return (T)((object)null);
			}
			return this.m_fsm.CurrentState as T;
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x0003CD38 File Offset: 0x0003AF38
		// (set) Token: 0x06000A19 RID: 2585 RVA: 0x0003CD48 File Offset: 0x0003AF48
		public Vector3 Position
		{
			get
			{
				return base.transform.position;
			}
			set
			{
				base.transform.position = value;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x0003CD58 File Offset: 0x0003AF58
		// (set) Token: 0x06000A1B RID: 2587 RVA: 0x0003CD68 File Offset: 0x0003AF68
		public Vector3 Angles
		{
			get
			{
				return base.transform.localEulerAngles;
			}
			set
			{
				base.transform.localEulerAngles = value;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0003CD78 File Offset: 0x0003AF78
		public float ComeInSpeed
		{
			get
			{
				return this.m_come_in_speed;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x0003CD80 File Offset: 0x0003AF80
		public float TargetAccessSpeed
		{
			get
			{
				return this.m_target_access_speed;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0003CD88 File Offset: 0x0003AF88
		public Vector3 Hovering
		{
			get
			{
				if (this.m_hoveringMove != null)
				{
					return this.m_hoveringMove.Position;
				}
				return Vector3.zero;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x0003CDB8 File Offset: 0x0003AFB8
		// (set) Token: 0x06000A20 RID: 2592 RVA: 0x0003CDC0 File Offset: 0x0003AFC0
		public Vector3 OffsetPosition
		{
			get
			{
				return this.m_offset_pos;
			}
			protected set
			{
				this.m_offset_pos = value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x0003CDCC File Offset: 0x0003AFCC
		public Vector3 TargetPosition
		{
			get
			{
				return this.m_target_pos;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x0003CDD4 File Offset: 0x0003AFD4
		public PlayerInformation PlayerInfo
		{
			get
			{
				return this.m_player_info;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x0003CDDC File Offset: 0x0003AFDC
		public bool IsPlyayerMoved
		{
			get
			{
				return this.m_player_info != null && this.m_player_info.IsMovementUpdated();
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0003CDFC File Offset: 0x0003AFFC
		// (set) Token: 0x06000A25 RID: 2597 RVA: 0x0003CE04 File Offset: 0x0003B004
		public Vector3 Velocity
		{
			get
			{
				return this.m_velocity;
			}
			set
			{
				this.m_velocity = value;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x0003CE10 File Offset: 0x0003B010
		public Vector3 MovedVelocity
		{
			get
			{
				return this.m_moved_velocity;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x0003CE18 File Offset: 0x0003B018
		public Vector3 VertVelocity
		{
			get
			{
				return Vector3.Dot(this.m_velocity, Vector3.up) * Vector3.up;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x0003CE34 File Offset: 0x0003B034
		public Vector3 HorzVelocity
		{
			get
			{
				return this.m_velocity - this.VertVelocity;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x0003CE48 File Offset: 0x0003B048
		// (set) Token: 0x06000A2A RID: 2602 RVA: 0x0003CE50 File Offset: 0x0003B050
		public bool NextState
		{
			get
			{
				return this.m_next_state_flag;
			}
			set
			{
				this.m_next_state_flag = value;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x0003CE5C File Offset: 0x0003B05C
		public ChaoParameter Parameter
		{
			get
			{
				return this.m_chao_param;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x0003CE64 File Offset: 0x0003B064
		public ChaoParameterData ParameterData
		{
			get
			{
				return this.m_chao_param.Data;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x0003CE74 File Offset: 0x0003B074
		public Vector3 PlayerPosition
		{
			get
			{
				if (this.m_player_info != null)
				{
					return this.m_player_info.Position;
				}
				return Vector3.zero;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x0003CEA4 File Offset: 0x0003B0A4
		public Vector3 PrevPlayerPosition
		{
			get
			{
				return this.m_prevPlayerPos;
			}
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0003CEAC File Offset: 0x0003B0AC
		public float GetPlayerDisplacement()
		{
			return Vector3.Distance(this.PlayerPosition, this.PrevPlayerPosition);
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x0003CECC File Offset: 0x0003B0CC
		public bool FromComeIn
		{
			get
			{
				return this.m_fromComeIn;
			}
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0003CED4 File Offset: 0x0003B0D4
		private void SetHoveringMove(ChaoHoveringBase hovering)
		{
			this.m_hoveringMove = hovering;
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0003CEE0 File Offset: 0x0003B0E0
		public void ResetHovering()
		{
			if (this.m_hoveringMove != null)
			{
				this.m_hoveringMove.Reset();
			}
		}

		// Token: 0x040007C4 RID: 1988
		private FSMSystem<ChaoMovement> m_fsm;

		// Token: 0x040007C5 RID: 1989
		private PlayerInformation m_player_info;

		// Token: 0x040007C6 RID: 1990
		private ChaoParameter m_chao_param;

		// Token: 0x040007C7 RID: 1991
		private ChaoHoveringBase m_hoveringMove;

		// Token: 0x040007C8 RID: 1992
		[SerializeField]
		private Vector3 m_offset_pos = new Vector3(-0.5f, 0.8f, 0f);

		// Token: 0x040007C9 RID: 1993
		private Vector3 m_target_pos = new Vector3(0f, 0f, 0f);

		// Token: 0x040007CA RID: 1994
		private Vector3 m_offsetRadicon = new Vector3(-1f, 0f, 0f);

		// Token: 0x040007CB RID: 1995
		private Vector3 m_velocity = Vector3.zero;

		// Token: 0x040007CC RID: 1996
		private float m_come_in_speed = 5f;

		// Token: 0x040007CD RID: 1997
		private float m_target_access_speed = 5f;

		// Token: 0x040007CE RID: 1998
		private bool m_next_state_flag;

		// Token: 0x040007CF RID: 1999
		private Vector3 m_moved_velocity = Vector3.zero;

		// Token: 0x040007D0 RID: 2000
		private Vector3 m_prevPlayerPos = Vector3.zero;

		// Token: 0x040007D1 RID: 2001
		private bool m_fromComeIn;

		// Token: 0x040007D2 RID: 2002
		public static readonly Vector3 HorzDir = CharacterDefs.BaseFrontTangent;

		// Token: 0x040007D3 RID: 2003
		public static readonly Vector3 VertDir = Vector3.up;
	}
}
