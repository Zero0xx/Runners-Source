using System;
using App.Utility;
using UnityEngine;

namespace Player
{
	// Token: 0x02000980 RID: 2432
	public class CharacterMovement : MonoBehaviour
	{
		// Token: 0x06003FD1 RID: 16337 RVA: 0x0014BB18 File Offset: 0x00149D18
		private void Start()
		{
			this.SetupOnStart();
		}

		// Token: 0x06003FD2 RID: 16338 RVA: 0x0014BB20 File Offset: 0x00149D20
		public void SetupOnStart()
		{
			if (this.m_alreadySetup)
			{
				return;
			}
			this.m_prevRayPosition = base.transform.position;
			this.m_gravityDir = -Vector3.up;
			this.m_hitInfo = new HitInfo[3];
			this.m_sweepHitInfo = default(HitInfo);
			if (this.m_fsm == null)
			{
				this.m_fsm = new FSMSystem<CharacterMovement>();
				FSMStateFactory<CharacterMovement>[] stateTable = CharacterMovement.GetStateTable();
				foreach (FSMStateFactory<CharacterMovement> stateFactory in stateTable)
				{
					this.m_fsm.AddState(stateFactory);
				}
				this.m_fsm.Init(this, 2);
			}
			if (this.m_blockPathManager == null)
			{
				this.m_blockPathManager = GameObjectUtil.FindGameObjectComponent<StageBlockPathManager>("StageBlockManager");
			}
			if (this.m_information == null)
			{
				GameObject gameObject = GameObject.Find("PlayerInformation");
				this.m_information = gameObject.GetComponent<PlayerInformation>();
			}
			this.m_alreadySetup = true;
		}

		// Token: 0x06003FD3 RID: 16339 RVA: 0x0014BC18 File Offset: 0x00149E18
		private void LateUpdate()
		{
			this.m_information.SetMovementUpdated(false);
		}

		// Token: 0x06003FD4 RID: 16340 RVA: 0x0014BC28 File Offset: 0x00149E28
		private void FixedUpdate()
		{
			this.m_sweepHitInfo.Reset();
			this.m_status.Set(4, false);
			Vector3 position = base.transform.position;
			if (this.m_fsm != null && this.m_fsm.CurrentState != null)
			{
				this.m_fsm.CurrentState.Step(this, Time.deltaTime);
			}
			this.m_displacement = base.transform.position - position;
			if (!this.m_status.Test(4))
			{
				this.m_prevRayPosition = base.transform.position + base.transform.up * 0.1f;
			}
			this.UpdateHitInfo();
			this.m_information.SetMovementUpdated(true);
		}

		// Token: 0x06003FD5 RID: 16341 RVA: 0x0014BCF0 File Offset: 0x00149EF0
		private void OnDestroy()
		{
			if (this.m_fsm != null && this.m_fsm.CurrentState != null)
			{
				this.m_fsm.CurrentState.Leave(this);
				this.m_fsm = null;
			}
		}

		// Token: 0x06003FD6 RID: 16342 RVA: 0x0014BD28 File Offset: 0x00149F28
		public void ChangeState(MOVESTATE_ID state)
		{
			if (this.m_fsm != null && this.m_fsm.CurrentStateID != (StateID)state)
			{
				this.m_fsm.ChangeState(this, (int)state);
			}
		}

		// Token: 0x06003FD7 RID: 16343 RVA: 0x0014BD54 File Offset: 0x00149F54
		public T GetCurrentState<T>() where T : FSMState<CharacterMovement>
		{
			if (this.m_fsm == null)
			{
				return (T)((object)null);
			}
			return this.m_fsm.CurrentState as T;
		}

		// Token: 0x06003FD8 RID: 16344 RVA: 0x0014BD8C File Offset: 0x00149F8C
		private void UpdateHitInfo()
		{
			Vector3 position = base.transform.position;
			Vector3 up = base.transform.up;
			float num = 0.1f;
			float distance = 0.1f + num;
			CapsuleCollider component = base.GetComponent<CapsuleCollider>();
			float d = 0f;
			float d2 = 0f;
			Vector3 direction = Vector3.zero;
			if (component)
			{
				d = component.height;
				d2 = component.radius;
				direction = component.center;
			}
			this.m_dir[0] = -base.transform.up;
			this.m_dir[1] = up;
			this.m_dir[2] = base.transform.forward;
			this.m_rayOffset[0] = Vector3.zero;
			this.m_rayOffset[1] = this.m_dir[1] * d;
			this.m_rayOffset[2] = component.transform.TransformDirection(direction) + this.m_dir[2] * d2;
			for (int i = 0; i < 3; i++)
			{
				Vector3 vector = this.m_dir[i];
				Vector3 origin = position + this.m_rayOffset[i] - vector * num;
				RaycastHit hit;
				if (Physics.Raycast(origin, vector, out hit, distance))
				{
					this.m_hitInfo[i].Set(hit);
				}
				else
				{
					this.m_hitInfo[i].Reset();
				}
			}
			if (!this.m_hitInfo[0].IsValid() && this.m_sweepHitInfo.IsValid())
			{
				Vector3 rhs = up;
				float num2 = Vector3.Dot(this.m_sweepHitInfo.info.normal, rhs);
				if (num2 > this.m_enableLandCos)
				{
					this.m_hitInfo[0].Set(this.m_sweepHitInfo.info);
				}
			}
			this.m_status.Set(0, this.m_hitInfo[0].valid);
			if (this.IsOnGround())
			{
				this.m_groundUpDir = this.m_hitInfo[0].info.normal;
				this.m_distanceToGround = 0f;
			}
			else
			{
				this.m_groundUpDir = -this.GetGravityDir();
				RaycastHit raycastHit;
				if (Physics.Raycast(base.transform.position, this.GetGravityDir(), out raycastHit, 30f))
				{
					this.m_distanceToGround = raycastHit.distance;
				}
				else
				{
					this.m_distanceToGround = 30f;
				}
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06003FD9 RID: 16345 RVA: 0x0014C06C File Offset: 0x0014A26C
		// (set) Token: 0x06003FDA RID: 16346 RVA: 0x0014C074 File Offset: 0x0014A274
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

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06003FDB RID: 16347 RVA: 0x0014C080 File Offset: 0x0014A280
		// (set) Token: 0x06003FDC RID: 16348 RVA: 0x0014C0A4 File Offset: 0x0014A2A4
		public Vector3 HorzVelocity
		{
			get
			{
				return this.m_velocity - Vector3.Project(this.m_velocity, base.transform.up);
			}
			set
			{
				this.m_velocity = value + this.VertVelocity;
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06003FDD RID: 16349 RVA: 0x0014C0B8 File Offset: 0x0014A2B8
		// (set) Token: 0x06003FDE RID: 16350 RVA: 0x0014C0D0 File Offset: 0x0014A2D0
		public Vector3 VertVelocity
		{
			get
			{
				return Vector3.Project(this.m_velocity, base.transform.up);
			}
			set
			{
				this.m_velocity = value + this.HorzVelocity;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06003FDF RID: 16351 RVA: 0x0014C0E4 File Offset: 0x0014A2E4
		public float EnableLandCos
		{
			get
			{
				return this.m_enableLandCos;
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06003FE0 RID: 16352 RVA: 0x0014C0EC File Offset: 0x0014A2EC
		public Vector3 RaycastCheckPosition
		{
			get
			{
				return this.m_prevRayPosition;
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06003FE1 RID: 16353 RVA: 0x0014C0F4 File Offset: 0x0014A2F4
		public Vector3 SideViewPathPos
		{
			get
			{
				return this.m_information.SideViewPathPos;
			}
		}

		// Token: 0x06003FE2 RID: 16354 RVA: 0x0014C104 File Offset: 0x0014A304
		public void SetRaycastCheckPosition(Vector3 pos)
		{
			this.m_prevRayPosition = pos;
			this.m_status.Set(4, true);
		}

		// Token: 0x06003FE3 RID: 16355 RVA: 0x0014C11C File Offset: 0x0014A31C
		public bool GetHitInfo(CharacterMovement.HitType type, out HitInfo info)
		{
			info = this.m_hitInfo[(int)type];
			return info.valid;
		}

		// Token: 0x06003FE4 RID: 16356 RVA: 0x0014C13C File Offset: 0x0014A33C
		public bool IsHit(CharacterMovement.HitType type)
		{
			return this.m_hitInfo[(int)type].IsValid();
		}

		// Token: 0x06003FE5 RID: 16357 RVA: 0x0014C150 File Offset: 0x0014A350
		public void SetSweepHitInfo(HitInfo info)
		{
			this.m_sweepHitInfo = info;
		}

		// Token: 0x06003FE6 RID: 16358 RVA: 0x0014C15C File Offset: 0x0014A35C
		public bool GetGroundInfo(out HitInfo info)
		{
			info = this.m_hitInfo[0];
			return this.m_hitInfo[0].valid;
		}

		// Token: 0x06003FE7 RID: 16359 RVA: 0x0014C194 File Offset: 0x0014A394
		public bool IsOnGround()
		{
			return this.m_status.Test(0);
		}

		// Token: 0x06003FE8 RID: 16360 RVA: 0x0014C1A4 File Offset: 0x0014A3A4
		public void OffGround()
		{
			this.m_status.Set(0, false);
			this.m_hitInfo[0].Reset();
			this.m_groundUpDir = -this.GetGravityDir();
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06003FEA RID: 16362 RVA: 0x0014C1F4 File Offset: 0x0014A3F4
		// (set) Token: 0x06003FE9 RID: 16361 RVA: 0x0014C1E4 File Offset: 0x0014A3E4
		public bool ThroughBreakable
		{
			get
			{
				return this.m_status.Test(3);
			}
			set
			{
				this.m_status.Set(3, value);
			}
		}

		// Token: 0x06003FEB RID: 16363 RVA: 0x0014C204 File Offset: 0x0014A404
		public void ResetPosition(Vector3 pos)
		{
			base.transform.position = pos;
			this.m_prevRayPosition = pos;
		}

		// Token: 0x06003FEC RID: 16364 RVA: 0x0014C21C File Offset: 0x0014A41C
		public void ResetRotation(Quaternion rot)
		{
			base.transform.rotation = rot;
		}

		// Token: 0x06003FED RID: 16365 RVA: 0x0014C22C File Offset: 0x0014A42C
		public void SetLookRotation(Vector3 front, Vector3 up)
		{
			Quaternion identity = Quaternion.identity;
			identity.SetLookRotation(front, up);
			base.transform.rotation = identity;
		}

		// Token: 0x06003FEE RID: 16366 RVA: 0x0014C254 File Offset: 0x0014A454
		public float GetVertVelocityScalar()
		{
			return Vector3.Dot(this.m_velocity, base.transform.up);
		}

		// Token: 0x06003FEF RID: 16367 RVA: 0x0014C26C File Offset: 0x0014A46C
		public float GetForwardVelocityScalar()
		{
			return Vector3.Dot(this.m_velocity, this.GetForwardDir());
		}

		// Token: 0x06003FF0 RID: 16368 RVA: 0x0014C280 File Offset: 0x0014A480
		public Vector3 GetForwardDir()
		{
			return base.transform.forward;
		}

		// Token: 0x06003FF1 RID: 16369 RVA: 0x0014C290 File Offset: 0x0014A490
		public Vector3 GetUpDir()
		{
			return base.transform.up;
		}

		// Token: 0x06003FF2 RID: 16370 RVA: 0x0014C2A0 File Offset: 0x0014A4A0
		public Vector3 GetGravity()
		{
			return this.m_gravityDir * this.m_gravitySize;
		}

		// Token: 0x06003FF3 RID: 16371 RVA: 0x0014C2B4 File Offset: 0x0014A4B4
		public Vector3 GetGravityDir()
		{
			return this.m_gravityDir;
		}

		// Token: 0x06003FF4 RID: 16372 RVA: 0x0014C2BC File Offset: 0x0014A4BC
		public Vector3 GetDisplacement()
		{
			return this.m_displacement;
		}

		// Token: 0x06003FF5 RID: 16373 RVA: 0x0014C2C4 File Offset: 0x0014A4C4
		public StageBlockPathManager GetBlockPathManager()
		{
			return this.m_blockPathManager;
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06003FF6 RID: 16374 RVA: 0x0014C2CC File Offset: 0x0014A4CC
		public float DistanceToGround
		{
			get
			{
				return this.m_distanceToGround;
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06003FF7 RID: 16375 RVA: 0x0014C2D4 File Offset: 0x0014A4D4
		public Vector3 GroundUpDirection
		{
			get
			{
				return this.m_groundUpDir;
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06003FF8 RID: 16376 RVA: 0x0014C2DC File Offset: 0x0014A4DC
		public CharacterParameterData Parameter
		{
			get
			{
				return base.GetComponent<CharacterParameter>().GetData();
			}
		}

		// Token: 0x06003FF9 RID: 16377 RVA: 0x0014C2EC File Offset: 0x0014A4EC
		private static FSMStateFactory<CharacterMovement>[] GetStateTable()
		{
			return new FSMStateFactory<CharacterMovement>[]
			{
				new FSMStateFactory<CharacterMovement>(2, new CharacterMoveRun()),
				new FSMStateFactory<CharacterMovement>(3, new CharacterMoveAir()),
				new FSMStateFactory<CharacterMovement>(4, new CharacterMoveIgnoreCollision()),
				new FSMStateFactory<CharacterMovement>(5, new CharacterMoveOnPath()),
				new FSMStateFactory<CharacterMovement>(6, new CharacterMoveTarget()),
				new FSMStateFactory<CharacterMovement>(7, new CharacterMoveOnPathPhantom()),
				new FSMStateFactory<CharacterMovement>(8, new CharacterMoveTargetBoss()),
				new FSMStateFactory<CharacterMovement>(9, new CharacterMoveOnPathPhantomDrill()),
				new FSMStateFactory<CharacterMovement>(10, new CharacterMoveAsteroid())
			};
		}

		// Token: 0x040036C2 RID: 14018
		private const float PlayerGravitySize = 45f;

		// Token: 0x040036C3 RID: 14019
		private const float maxLengthOnSearchGround = 30f;

		// Token: 0x040036C4 RID: 14020
		private Vector3[] m_dir = new Vector3[3];

		// Token: 0x040036C5 RID: 14021
		private Vector3[] m_rayOffset = new Vector3[3];

		// Token: 0x040036C6 RID: 14022
		private FSMSystem<CharacterMovement> m_fsm;

		// Token: 0x040036C7 RID: 14023
		private Vector3 m_velocity;

		// Token: 0x040036C8 RID: 14024
		private Vector3 m_displacement;

		// Token: 0x040036C9 RID: 14025
		private Vector3 m_prevRayPosition;

		// Token: 0x040036CA RID: 14026
		private float m_distanceToGround;

		// Token: 0x040036CB RID: 14027
		private StageBlockPathManager m_blockPathManager;

		// Token: 0x040036CC RID: 14028
		private bool m_alreadySetup;

		// Token: 0x040036CD RID: 14029
		private readonly float m_enableLandCos = Mathf.Cos(0.7853982f);

		// Token: 0x040036CE RID: 14030
		public bool m_doneFixedUpdate;

		// Token: 0x040036CF RID: 14031
		private PlayerInformation m_information;

		// Token: 0x040036D0 RID: 14032
		private bool m_dispInfo;

		// Token: 0x040036D1 RID: 14033
		private HitInfo[] m_hitInfo = new HitInfo[3];

		// Token: 0x040036D2 RID: 14034
		private HitInfo m_sweepHitInfo = default(HitInfo);

		// Token: 0x040036D3 RID: 14035
		private float m_gravitySize = 45f;

		// Token: 0x040036D4 RID: 14036
		private Vector3 m_gravityDir = -Vector3.up;

		// Token: 0x040036D5 RID: 14037
		private Vector3 m_groundUpDir = Vector3.up;

		// Token: 0x040036D6 RID: 14038
		private Bitset32 m_status;

		// Token: 0x02000981 RID: 2433
		private enum Status
		{
			// Token: 0x040036D8 RID: 14040
			OnGround,
			// Token: 0x040036D9 RID: 14041
			OnRunPath,
			// Token: 0x040036DA RID: 14042
			IgnoreCollision,
			// Token: 0x040036DB RID: 14043
			ThroughBroken,
			// Token: 0x040036DC RID: 14044
			RayPos_Dirty
		}

		// Token: 0x02000982 RID: 2434
		public enum HitType
		{
			// Token: 0x040036DE RID: 14046
			Down,
			// Token: 0x040036DF RID: 14047
			Up,
			// Token: 0x040036E0 RID: 14048
			Front,
			// Token: 0x040036E1 RID: 14049
			NUM
		}
	}
}
