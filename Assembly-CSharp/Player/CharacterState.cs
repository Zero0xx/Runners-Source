using System;
using App;
using App.Utility;
using Message;
using UnityEngine;

namespace Player
{
	// Token: 0x0200096F RID: 2415
	public class CharacterState : MonoBehaviour
	{
		// Token: 0x06003F11 RID: 16145 RVA: 0x00147E0C File Offset: 0x0014600C
		public void SetPlayingType(PlayingCharacterType type)
		{
			this.m_playingCharacterType = type;
		}

		// Token: 0x06003F12 RID: 16146 RVA: 0x00147E18 File Offset: 0x00146018
		public void SetupModelsAndParameter()
		{
			if (this.m_isAlreadySetupModel)
			{
				return;
			}
			PlayerInformation playerInformation = GameObjectUtil.FindGameObjectComponent<PlayerInformation>("PlayerInformation");
			ResourceManager instance = ResourceManager.Instance;
			if (playerInformation != null && instance != null)
			{
				string text = (this.m_playingCharacterType != PlayingCharacterType.MAIN) ? playerInformation.SubCharacterName : playerInformation.MainCharacterName;
				if (!this.m_notLoadCharaParameter)
				{
					string name = text + "Parameter";
					GameObject gameObject = instance.GetGameObject(ResourceCategory.PLAYER_COMMON, name);
					if (gameObject != null)
					{
						CharacterParameter component = gameObject.GetComponent<CharacterParameter>();
						CharacterParameter component2 = base.GetComponent<CharacterParameter>();
						if (component2 != null && component != null)
						{
							component2.CopyData(component.GetData());
						}
					}
				}
				if (CharacterDataNameInfo.Instance)
				{
					CharacterDataNameInfo.Info dataByName = CharacterDataNameInfo.Instance.GetDataByName(text);
					if (dataByName != null)
					{
						this.m_charaType = dataByName.m_ID;
						this.m_attribute = dataByName.m_attribute;
						this.m_teamAttribute = dataByName.m_teamAttribute;
						this.m_options.Reset();
						this.SetOption(Option.BigSize, dataByName.BigSize);
						this.SetOption(Option.HighSpeedExEffect, dataByName.HighSpeedEffect);
						this.SetOption(Option.ThirdJump, dataByName.ThirdJump);
						this.SuffixName = dataByName.m_hud_suffix;
					}
				}
				if (base.name != null)
				{
					string text2 = "chr_" + text;
					text2 = text2.ToLower();
					GameObject gameObject2 = this.CreateChildModelObject(instance.GetGameObject(ResourceCategory.CHARA_MODEL, text2), true);
					if (gameObject2)
					{
						CharacterState.OffAnimatorRootAnimation(gameObject2);
					}
					this.BodyModelName = text2;
				}
				this.SetupAnimation();
				foreach (string name2 in CharacterDefs.PhantomBodyName)
				{
					GameObject gameObject3 = this.CreateChildModelObject(instance.GetGameObject(ResourceCategory.PLAYER_COMMON, name2), false);
					if (gameObject3)
					{
						CharacterState.OffAnimatorRootAnimation(gameObject3);
					}
					Collider[] componentsInChildren = gameObject3.GetComponentsInChildren<Collider>(true);
					foreach (Collider collider in componentsInChildren)
					{
						if (collider.gameObject.layer == LayerMask.NameToLayer("Magnet"))
						{
							collider.gameObject.AddComponent<CharacterMagnetPhantom>();
						}
						if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
						{
							collider.gameObject.AddComponent<CharacterPhantomCollision>();
						}
					}
				}
				GameObject gameObject4 = this.CreateChildObject(instance.GetGameObject(ResourceCategory.PLAYER_COMMON, "drill_truck"), false);
				if (gameObject4 != null)
				{
					this.SetupDrill(gameObject4);
				}
				this.CreateChildObject(instance.GetGameObject(ResourceCategory.COMMON_EFFECT, "ef_ph_laser_lp01"), false);
				if (text != null)
				{
					string effectName = "ef_pl_" + text.ToLower() + "_boost01";
					string spinDashSEName = CharaSEUtil.GetSpinDashSEName(this.m_charaType);
					this.CreateLoopEffectBehavior("CharacterBoost", effectName, spinDashSEName, ResourceCategory.CHARA_EFFECT);
					string effectName2 = "ef_pl_" + text.ToLower() + "_jump01";
					GameObject gameobj = this.CreateLoopEffectBehavior("CharacterSpinAttack", effectName2, null, ResourceCategory.CHARA_EFFECT);
					StateUtil.SetObjectLocalPositionToCenter(this, gameobj);
					if (this.IsExHighSpeedEffect())
					{
						string effectName3 = "ef_pl_" + text.ToLower() + "_infinityrun01";
						GameObject gameobj2 = this.CreateLoopEffectBehavior("CharacterBoostEx", effectName3, null, ResourceCategory.CHARA_EFFECT);
						StateUtil.SetObjectLocalPositionToCenter(this, gameobj2);
					}
					if (EventManager.Instance != null && EventManager.Instance.IsRaidBossStage())
					{
						for (int k = 0; k < 3; k++)
						{
							string str = (k + 1).ToString();
							string effectName4 = "ef_raid_speedup_lv" + str + "_atk01";
							GameObject gameObject5 = this.CreateLoopEffectBehavior("CharacterSpinAttackLv" + str, effectName4, null, ResourceCategory.EVENT_RESOURCE);
							if (gameObject5 != null)
							{
								StateUtil.SetObjectLocalPositionToCenter(this, gameObject5);
								if (k == 2)
								{
									CharacterMagnet partsComponentAlways = StateUtil.GetPartsComponentAlways<CharacterMagnet>(this, "CharacterMagnet");
									if (partsComponentAlways != null)
									{
										GameObject gameObject6 = UnityEngine.Object.Instantiate(partsComponentAlways.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
										if (gameObject6 != null)
										{
											gameObject6.name = "CharacterMagnetBossBoost";
											gameObject6.transform.parent = gameObject5.transform;
											gameObject6.transform.localPosition = partsComponentAlways.gameObject.transform.localPosition;
											gameObject6.transform.localRotation = partsComponentAlways.gameObject.transform.localRotation;
											gameObject6.transform.localScale = partsComponentAlways.gameObject.transform.localScale;
											SphereCollider component3 = gameObject6.GetComponent<SphereCollider>();
											if (component3 != null)
											{
												component3.radius = 1.5f;
											}
											gameObject6.SetActive(true);
										}
									}
								}
							}
						}
					}
					this.CharacterName = text.ToLower();
				}
				if (this.IsBigSize())
				{
					CharacterMagnet partsComponentAlways2 = StateUtil.GetPartsComponentAlways<CharacterMagnet>(this, "CharacterMagnet");
					if (partsComponentAlways2 != null)
					{
						partsComponentAlways2.IsBigSize = true;
					}
					CharacterMagnet partsComponentAlways3 = StateUtil.GetPartsComponentAlways<CharacterMagnet>(this, "CharacterMagnetBossBoost");
					if (partsComponentAlways3 != null)
					{
						partsComponentAlways3.IsBigSize = true;
					}
					CharacterInvincible partsComponentAlways4 = StateUtil.GetPartsComponentAlways<CharacterInvincible>(this, "CharacterInvincible");
					if (partsComponentAlways4 != null)
					{
						partsComponentAlways4.IsBigSize = true;
					}
					CharacterBarrier partsComponentAlways5 = StateUtil.GetPartsComponentAlways<CharacterBarrier>(this, "CharacterBarrier");
					if (partsComponentAlways5 != null)
					{
						partsComponentAlways5.IsBigSize = true;
					}
				}
			}
			this.m_isAlreadySetupModel = true;
		}

		// Token: 0x06003F13 RID: 16147 RVA: 0x00148390 File Offset: 0x00146590
		private GameObject CreateChildModelObject(GameObject srcObject, bool active)
		{
			if (srcObject != null)
			{
				Vector3 localPosition = srcObject.transform.localPosition;
				Quaternion localRotation = srcObject.transform.localRotation;
				GameObject gameObject = UnityEngine.Object.Instantiate(srcObject, base.transform.position, base.transform.rotation) as GameObject;
				if (gameObject != null)
				{
					gameObject.transform.parent = base.transform;
					gameObject.SetActive(active);
					gameObject.transform.localPosition = localPosition;
					gameObject.transform.localRotation = localRotation;
					gameObject.name = srcObject.name;
					return gameObject;
				}
			}
			return null;
		}

		// Token: 0x06003F14 RID: 16148 RVA: 0x00148430 File Offset: 0x00146630
		private static void OffAnimatorRootAnimation(GameObject modelObject)
		{
			if (modelObject == null)
			{
				return;
			}
			Animator component = modelObject.GetComponent<Animator>();
			if (component != null)
			{
				component.applyRootMotion = false;
			}
		}

		// Token: 0x06003F15 RID: 16149 RVA: 0x00148464 File Offset: 0x00146664
		private GameObject CreateChildObject(GameObject srcObject, bool active)
		{
			if (srcObject != null)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(srcObject, base.transform.position, base.transform.rotation) as GameObject;
				if (gameObject)
				{
					gameObject.SetActive(active);
					gameObject.transform.parent = base.transform;
					gameObject.name = srcObject.name;
					return gameObject;
				}
			}
			return null;
		}

		// Token: 0x06003F16 RID: 16150 RVA: 0x001484D4 File Offset: 0x001466D4
		private void SetupDrill(GameObject drillObject)
		{
			DrillTrack drillTrack = drillObject.AddComponent<DrillTrack>();
			if (drillTrack != null)
			{
				drillTrack.m_Target = base.gameObject;
				GameObject gameObject = GameObject.FindGameObjectWithTag("MainCamera");
				if (gameObject != null)
				{
					drillTrack.m_Camera = gameObject.transform;
				}
			}
		}

		// Token: 0x06003F17 RID: 16151 RVA: 0x00148524 File Offset: 0x00146724
		private void SetupAnimation()
		{
			GameObject gameObject = base.transform.FindChild(this.m_bodyName).gameObject;
			if (gameObject)
			{
				this.m_bodyAnimator = gameObject.GetComponent<Animator>();
			}
		}

		// Token: 0x06003F18 RID: 16152 RVA: 0x00148560 File Offset: 0x00146760
		private GameObject CreateLoopEffectBehavior(string objectName, string effectName, string sename, ResourceCategory category)
		{
			GameObject gameObject = new GameObject(objectName);
			if (gameObject != null)
			{
				CharacterLoopEffect characterLoopEffect = gameObject.AddComponent<CharacterLoopEffect>();
				if (characterLoopEffect != null)
				{
					characterLoopEffect.Setup(effectName, category);
					characterLoopEffect.SetSE(sename);
					gameObject.transform.position = base.transform.position;
					gameObject.transform.rotation = base.transform.rotation;
					gameObject.transform.parent = base.transform;
					gameObject.SetActive(false);
					return gameObject;
				}
				UnityEngine.Object.Destroy(gameObject);
			}
			return null;
		}

		// Token: 0x06003F19 RID: 16153 RVA: 0x001485F0 File Offset: 0x001467F0
		public void Start()
		{
			this.SetupOnStart();
		}

		// Token: 0x06003F1A RID: 16154 RVA: 0x001485F8 File Offset: 0x001467F8
		private void SetupOnStart()
		{
			if (this.m_information == null)
			{
				GameObject gameObject = GameObject.Find("PlayerInformation");
				this.m_information = gameObject.GetComponent<PlayerInformation>();
			}
			if (this.m_blockPathManager == null)
			{
				this.m_blockPathManager = GameObjectUtil.FindGameObjectComponent<StageBlockPathManager>("StageBlockManager");
			}
			if (this.m_characterContainer == null)
			{
				this.m_characterContainer = GameObjectUtil.FindGameObjectComponent<CharacterContainer>("CharacterContainer");
			}
			if (this.m_camera == null)
			{
				GameObject gameObject2 = GameObject.FindGameObjectWithTag("MainCamera");
				this.m_camera = gameObject2.GetComponent<CameraManager>();
			}
			if (this.m_levelInformation == null)
			{
				this.m_levelInformation = GameObjectUtil.FindGameObjectComponent<LevelInformation>("LevelInformation");
			}
			if (this.m_scoreManager == null)
			{
				this.m_scoreManager = StageScoreManager.Instance;
			}
			if (this.m_information != null)
			{
				this.m_nowSpeedLevel = this.m_information.SpeedLevel;
				this.m_information.SetPlayerAttribute(this.m_attribute, this.m_teamAttribute, this.m_playingCharacterType);
			}
			if (!this.m_isAlreadySetupModel)
			{
				this.SetupModelsAndParameter();
			}
			if (this.m_input == null)
			{
				this.m_input = base.GetComponent<CharacterInput>();
			}
			if (this.m_movement == null)
			{
				this.m_movement = base.GetComponent<CharacterMovement>();
			}
			this.SetSpeedLevel(this.m_nowSpeedLevel);
			StateUtil.NowLanding(this, false);
			this.MakeFSM();
			this.SetupChildObjectOnStart();
			this.Movement.SetupOnStart();
			this.m_input.CreateHistory();
		}

		// Token: 0x06003F1B RID: 16155 RVA: 0x00148794 File Offset: 0x00146994
		private void SetupChildObjectOnStart()
		{
			foreach (object obj in base.gameObject.transform)
			{
				Transform transform = (Transform)obj;
				transform.gameObject.SetActive(false);
			}
			GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "ShadowProjector");
			if (gameObject != null)
			{
				gameObject.SetActive(true);
			}
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, this.BodyModelName);
			if (gameObject2 != null)
			{
				gameObject2.SetActive(true);
			}
			this.m_hitWallTimer = 0f;
		}

		// Token: 0x06003F1C RID: 16156 RVA: 0x00148864 File Offset: 0x00146A64
		private void OnDestroy()
		{
			if (this.m_fsm != null && this.m_fsm.CurrentState != null)
			{
				this.m_nowOnDestroy = true;
				this.m_fsm.CurrentState.Leave(this);
				this.m_fsm = null;
			}
		}

		// Token: 0x06003F1D RID: 16157 RVA: 0x001488AC File Offset: 0x00146AAC
		private void Update()
		{
			if (App.Math.NearZero(Time.deltaTime, 1E-06f))
			{
				return;
			}
			if (this.m_fsm != null && this.m_fsm.CurrentState != null)
			{
				this.m_fsm.CurrentState.Step(this, Time.deltaTime);
			}
			this.UpdateInfomations();
			this.CheckHitWallTimerDirty();
			this.CheckFallingDead();
			this.UpdateTransformInformations();
		}

		// Token: 0x06003F1E RID: 16158 RVA: 0x00148918 File Offset: 0x00146B18
		private void MakeFSM()
		{
			if (this.m_fsm == null)
			{
				this.m_fsm = new FSMSystem<CharacterState>();
				FSMStateFactory<CharacterState>[] commonFSMTable = CharacterState.GetCommonFSMTable();
				foreach (FSMStateFactory<CharacterState> stateFactory in commonFSMTable)
				{
					this.m_fsm.AddState(stateFactory);
				}
				this.SetAttributeState();
				if (!this.m_isEdit)
				{
					this.m_fsm.Init(this, 2);
				}
				else
				{
					this.m_fsm.Init(this, 1);
				}
			}
		}

		// Token: 0x06003F1F RID: 16159 RVA: 0x00148998 File Offset: 0x00146B98
		private void SetAttributeState()
		{
			switch (this.m_attribute)
			{
			case CharacterAttribute.SPEED:
				this.m_fsm.AddState(4, new StateDoubleJump());
				this.m_numEnableJump = 2;
				break;
			case CharacterAttribute.FLY:
				this.m_fsm.AddState(4, new StateFly());
				this.m_numEnableJump = 1;
				break;
			case CharacterAttribute.POWER:
				this.m_fsm.AddState(4, new StateGride());
				this.m_numEnableJump = 1;
				break;
			}
		}

		// Token: 0x06003F20 RID: 16160 RVA: 0x00148A1C File Offset: 0x00146C1C
		public void ChangeState(STATE_ID state)
		{
			this.m_fsm.ChangeState(this, (int)state);
			this.m_enteringParam = null;
			this.SetStatus(Status.NowLanding, false);
		}

		// Token: 0x06003F21 RID: 16161 RVA: 0x00148A3C File Offset: 0x00146C3C
		public void ChangeMovement(MOVESTATE_ID state)
		{
			if (this.m_movement)
			{
				this.m_movement.ChangeState(state);
			}
		}

		// Token: 0x06003F22 RID: 16162 RVA: 0x00148A5C File Offset: 0x00146C5C
		public T CreateEnteringParameter<T>() where T : StateEnteringParameter, new()
		{
			this.m_enteringParam = Activator.CreateInstance<T>();
			return (T)((object)this.m_enteringParam);
		}

		// Token: 0x06003F23 RID: 16163 RVA: 0x00148A7C File Offset: 0x00146C7C
		public T GetEnteringParameter<T>() where T : StateEnteringParameter
		{
			if (this.m_enteringParam == null)
			{
				return (T)((object)null);
			}
			if (this.m_enteringParam is T)
			{
				return (T)((object)this.m_enteringParam);
			}
			return (T)((object)null);
		}

		// Token: 0x06003F24 RID: 16164 RVA: 0x00148AC8 File Offset: 0x00146CC8
		public void SetVisibleBlink(bool value)
		{
			this.m_visibleStatus.Set(0, value);
			this.SetRenderEnable(!this.m_visibleStatus.Any());
		}

		// Token: 0x06003F25 RID: 16165 RVA: 0x00148AF8 File Offset: 0x00146CF8
		public void SetModelNotDraw(bool value)
		{
			this.m_visibleStatus.Set(1, value);
			this.SetRenderEnable(!this.m_visibleStatus.Any());
			if (this.m_bodyName == "chr_omega")
			{
				GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "chr_omega");
				if (gameObject != null)
				{
					GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "Booster_R");
					if (gameObject2 != null)
					{
						gameObject2.SetActive(!value);
					}
					GameObject gameObject3 = GameObjectUtil.FindChildGameObject(gameObject, "Booster_L");
					if (gameObject3 != null)
					{
						gameObject3.SetActive(!value);
					}
				}
			}
		}

		// Token: 0x06003F26 RID: 16166 RVA: 0x00148BA0 File Offset: 0x00146DA0
		private void SetRenderEnable(bool value)
		{
			Component[] componentsInChildren = base.GetComponentsInChildren<SkinnedMeshRenderer>(true);
			foreach (SkinnedMeshRenderer skinnedMeshRenderer in componentsInChildren)
			{
				skinnedMeshRenderer.enabled = value;
			}
		}

		// Token: 0x06003F27 RID: 16167 RVA: 0x00148BDC File Offset: 0x00146DDC
		public void StartDamageBlink()
		{
			if (this.m_blinkTimer == null)
			{
				this.m_blinkTimer = base.gameObject.AddComponent<CharacterBlinkTimer>();
			}
			this.m_blinkTimer.Setup(this, this.Parameter.m_damageInvincibleTime);
		}

		// Token: 0x06003F28 RID: 16168 RVA: 0x00148C24 File Offset: 0x00146E24
		private void UpdateTransformInformations()
		{
			if (this.m_information == null)
			{
				return;
			}
			this.m_information.SetTransform(base.transform);
			this.m_information.SetVelocity(this.m_movement.Velocity);
			this.m_information.SetFrontSpeed(this.m_movement.GetForwardVelocityScalar());
			this.m_information.SetHorzAndVertVelocity(this.m_movement.HorzVelocity, this.m_movement.VertVelocity);
		}

		// Token: 0x06003F29 RID: 16169 RVA: 0x00148CA4 File Offset: 0x00146EA4
		private void UpdateInfomations()
		{
			if (this.m_information == null)
			{
				return;
			}
			this.UpdateTransformInformations();
			Vector3 displacement = this.m_movement.GetDisplacement();
			if (this.m_information.IsMovementUpdated())
			{
				float nowDistance;
				if (this.m_movement.IsOnGround())
				{
					nowDistance = Mathf.Max(0f, Vector3.Dot(displacement, this.m_movement.GetForwardDir()));
				}
				else
				{
					nowDistance = Mathf.Max(0f, Vector3.Dot(displacement, CharacterDefs.BaseFrontTangent));
				}
				this.m_information.AddTotalDistance(nowDistance);
			}
			this.m_information.SetDistanceToGround(this.m_movement.DistanceToGround);
			this.m_information.SetGravityDirection(this.m_movement.GetGravityDir());
			this.m_information.SetUpDirection(this.m_movement.GroundUpDirection);
			this.m_information.SetDefautlSpeed(this.DefaultSpeed);
			this.m_information.SetDead(this.IsDead());
			this.m_information.SetDamaged(this.IsDamaged());
			this.m_information.SetOnGround(this.m_movement.IsOnGround());
			this.m_information.SetEnableCharaChange(this.IsEnableCharaChange(false));
			this.m_information.SetParaloop(this.IsParaloop());
			this.m_information.SetPhantomType(this.m_nowPhantomType);
			this.m_information.SetLastChance(this.IsNowLastChance());
			StageBlockPathManager blockPathManager = this.m_blockPathManager;
			if (blockPathManager != null)
			{
				Vector3 position = this.Position;
				position.y = 0f;
				Vector3? vector = new Vector3?(position);
				Vector3? vector2 = new Vector3?(base.transform.up);
				Vector3? vector3 = null;
				PathEvaluator curentPathEvaluator = blockPathManager.GetCurentPathEvaluator(BlockPathController.PathType.SV);
				if (curentPathEvaluator != null)
				{
					float distance = curentPathEvaluator.Distance;
					curentPathEvaluator.GetClosestPositionAlongSpline(vector.Value, distance - 10f, distance + 10f, out distance);
					curentPathEvaluator.GetPNT(distance, ref vector, ref vector2, ref vector3);
					this.m_information.SetSideViewPath(vector.Value, vector2.Value);
				}
			}
		}

		// Token: 0x06003F2A RID: 16170 RVA: 0x00148EBC File Offset: 0x001470BC
		private void CheckHitWallTimerDirty()
		{
			if (!this.TestStatus(Status.HitWallTimerDirty))
			{
				this.m_hitWallTimer = 0f;
			}
			else
			{
				this.SetStatus(Status.HitWallTimerDirty, false);
			}
		}

		// Token: 0x06003F2B RID: 16171 RVA: 0x00148EF0 File Offset: 0x001470F0
		private void CheckFallingDead()
		{
			if (this.IsDead() || this.IsNowLastChance() || this.IsHold())
			{
				return;
			}
			if (base.transform.position.y < -100f)
			{
				this.ChangeState(STATE_ID.FallingDead);
			}
		}

		// Token: 0x06003F2C RID: 16172 RVA: 0x00148F44 File Offset: 0x00147144
		public void SetStatus(Status st, bool value)
		{
			this.m_status.Set((int)st, value);
		}

		// Token: 0x06003F2D RID: 16173 RVA: 0x00148F54 File Offset: 0x00147154
		public bool TestStatus(Status st)
		{
			return this.m_status.Test((int)st);
		}

		// Token: 0x06003F2E RID: 16174 RVA: 0x00148F64 File Offset: 0x00147164
		public void SetOption(Option op, bool value)
		{
			this.m_options.Set((int)op, value);
		}

		// Token: 0x06003F2F RID: 16175 RVA: 0x00148F74 File Offset: 0x00147174
		public bool TestOption(Option op)
		{
			return this.m_options.Test((int)op);
		}

		// Token: 0x06003F30 RID: 16176 RVA: 0x00148F84 File Offset: 0x00147184
		public bool IsDead()
		{
			return this.TestStatus(Status.Dead);
		}

		// Token: 0x06003F31 RID: 16177 RVA: 0x00148F90 File Offset: 0x00147190
		public bool IsDamaged()
		{
			return this.TestStatus(Status.Damaged);
		}

		// Token: 0x06003F32 RID: 16178 RVA: 0x00148F9C File Offset: 0x0014719C
		public bool IsParaloop()
		{
			return this.TestStatus(Status.Paraloop);
		}

		// Token: 0x06003F33 RID: 16179 RVA: 0x00148FA8 File Offset: 0x001471A8
		public bool IsHold()
		{
			return this.TestStatus(Status.Hold);
		}

		// Token: 0x06003F34 RID: 16180 RVA: 0x00148FB4 File Offset: 0x001471B4
		public bool IsBigSize()
		{
			return this.TestOption(Option.BigSize);
		}

		// Token: 0x06003F35 RID: 16181 RVA: 0x00148FC0 File Offset: 0x001471C0
		public bool IsExHighSpeedEffect()
		{
			return this.TestOption(Option.HighSpeedExEffect);
		}

		// Token: 0x06003F36 RID: 16182 RVA: 0x00148FCC File Offset: 0x001471CC
		public bool IsThirdJump()
		{
			return this.TestOption(Option.ThirdJump);
		}

		// Token: 0x06003F37 RID: 16183 RVA: 0x00148FD8 File Offset: 0x001471D8
		public bool IsNowLastChance()
		{
			return this.TestStatus(Status.LastChance);
		}

		// Token: 0x06003F38 RID: 16184 RVA: 0x00148FE4 File Offset: 0x001471E4
		public bool IsOnDestroy()
		{
			return this.m_nowOnDestroy;
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06003F39 RID: 16185 RVA: 0x00148FEC File Offset: 0x001471EC
		// (set) Token: 0x06003F3A RID: 16186 RVA: 0x00148FF4 File Offset: 0x001471F4
		public PhantomType NowPhantomType
		{
			get
			{
				return this.m_nowPhantomType;
			}
			set
			{
				this.m_nowPhantomType = value;
			}
		}

		// Token: 0x06003F3B RID: 16187 RVA: 0x00149000 File Offset: 0x00147200
		public bool IsNowPhantom()
		{
			return this.m_nowPhantomType != PhantomType.NONE;
		}

		// Token: 0x06003F3C RID: 16188 RVA: 0x00149010 File Offset: 0x00147210
		public CharacterAttribute GetCharacterAttribute()
		{
			return this.m_attribute;
		}

		// Token: 0x06003F3D RID: 16189 RVA: 0x00149018 File Offset: 0x00147218
		public void SetSpeedLevel(PlayerSpeed speed)
		{
			switch (speed)
			{
			case PlayerSpeed.LEVEL_1:
				this.m_defaultSpeed = this.Parameter.m_level1Speed;
				break;
			case PlayerSpeed.LEVEL_2:
				this.m_defaultSpeed = this.Parameter.m_level2Speed;
				break;
			case PlayerSpeed.LEVEL_3:
				this.m_defaultSpeed = this.Parameter.m_level3Speed;
				break;
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06003F3E RID: 16190 RVA: 0x00149080 File Offset: 0x00147280
		public CharacterMovement Movement
		{
			get
			{
				return this.m_movement;
			}
		}

		// Token: 0x06003F3F RID: 16191 RVA: 0x00149088 File Offset: 0x00147288
		public T GetMovementState<T>() where T : FSMState<CharacterMovement>
		{
			return this.Movement.GetCurrentState<T>();
		}

		// Token: 0x06003F40 RID: 16192 RVA: 0x00149098 File Offset: 0x00147298
		public Animator GetAnimator()
		{
			return this.m_bodyAnimator;
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06003F41 RID: 16193 RVA: 0x001490A0 File Offset: 0x001472A0
		// (set) Token: 0x06003F42 RID: 16194 RVA: 0x001490A8 File Offset: 0x001472A8
		public string BodyModelName
		{
			get
			{
				return this.m_bodyName;
			}
			set
			{
				this.m_bodyName = value;
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06003F43 RID: 16195 RVA: 0x001490B4 File Offset: 0x001472B4
		// (set) Token: 0x06003F44 RID: 16196 RVA: 0x001490BC File Offset: 0x001472BC
		public string CharacterName
		{
			get
			{
				return this.m_charaName;
			}
			set
			{
				this.m_charaName = value;
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06003F45 RID: 16197 RVA: 0x001490C8 File Offset: 0x001472C8
		// (set) Token: 0x06003F46 RID: 16198 RVA: 0x001490D0 File Offset: 0x001472D0
		public string SuffixName
		{
			get
			{
				return this.m_suffixName;
			}
			set
			{
				this.m_suffixName = value;
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06003F47 RID: 16199 RVA: 0x001490DC File Offset: 0x001472DC
		public Vector3 Position
		{
			get
			{
				return base.transform.position;
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06003F48 RID: 16200 RVA: 0x001490EC File Offset: 0x001472EC
		public CharacterParameterData Parameter
		{
			get
			{
				return base.GetComponent<CharacterParameter>().GetData();
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06003F49 RID: 16201 RVA: 0x001490FC File Offset: 0x001472FC
		public float DefaultSpeed
		{
			get
			{
				return this.m_defaultSpeed;
			}
		}

		// Token: 0x06003F4A RID: 16202 RVA: 0x00149104 File Offset: 0x00147304
		public void OnAttack(AttackPower attack, DefensePower defense)
		{
			this.m_attack = attack;
			this.m_defense = defense;
		}

		// Token: 0x06003F4B RID: 16203 RVA: 0x00149114 File Offset: 0x00147314
		public void OffAttack()
		{
			this.m_attack = AttackPower.PlayerNormal;
			this.m_defense = DefensePower.PlayerNormal;
			this.m_attackAttribute = 0U;
		}

		// Token: 0x06003F4C RID: 16204 RVA: 0x0014912C File Offset: 0x0014732C
		public void OnAttackAttribute(AttackAttribute attribute)
		{
			this.m_attackAttribute |= (uint)attribute;
		}

		// Token: 0x06003F4D RID: 16205 RVA: 0x0014913C File Offset: 0x0014733C
		public void SetLastChance(bool value)
		{
			this.SetStatus(Status.LastChance, value);
		}

		// Token: 0x06003F4E RID: 16206 RVA: 0x00149148 File Offset: 0x00147348
		public void SetNotCharaChange(bool value)
		{
			this.SetStatus(Status.NotCharaChange, value);
			GameObjectUtil.SendMessageFindGameObject("HudCockpit", "OnChangeCharaButton", new MsgChangeCharaButton(!value, false), SendMessageOptions.DontRequireReceiver);
		}

		// Token: 0x06003F4F RID: 16207 RVA: 0x00149170 File Offset: 0x00147370
		public void SetNotUseItem(bool value)
		{
			MsgItemButtonEnable value2 = new MsgItemButtonEnable(!value);
			GameObjectUtil.SendMessageFindGameObject("HudCockpit", "OnItemEnable", value2, SendMessageOptions.DontRequireReceiver);
		}

		// Token: 0x06003F50 RID: 16208 RVA: 0x0014919C File Offset: 0x0014739C
		public bool IsEnableCharaChange(bool changeByMiss)
		{
			return !this.TestStatus(Status.NotCharaChange) && (!this.IsDead() || changeByMiss);
		}

		// Token: 0x06003F51 RID: 16209 RVA: 0x001491D0 File Offset: 0x001473D0
		public StageBlockPathManager GetStagePathManager()
		{
			return this.m_blockPathManager;
		}

		// Token: 0x06003F52 RID: 16210 RVA: 0x001491D8 File Offset: 0x001473D8
		public CharacterContainer GetCharacterContainer()
		{
			return this.m_characterContainer;
		}

		// Token: 0x06003F53 RID: 16211 RVA: 0x001491E0 File Offset: 0x001473E0
		public CameraManager GetCamera()
		{
			return this.m_camera;
		}

		// Token: 0x06003F54 RID: 16212 RVA: 0x001491E8 File Offset: 0x001473E8
		public LevelInformation GetLevelInformation()
		{
			return this.m_levelInformation;
		}

		// Token: 0x06003F55 RID: 16213 RVA: 0x001491F0 File Offset: 0x001473F0
		public PlayerInformation GetPlayerInformation()
		{
			return this.m_information;
		}

		// Token: 0x06003F56 RID: 16214 RVA: 0x001491F8 File Offset: 0x001473F8
		public StageScoreManager GetStageScoreManager()
		{
			return this.m_scoreManager;
		}

		// Token: 0x06003F57 RID: 16215 RVA: 0x00149200 File Offset: 0x00147400
		public void ActiveCharacter(bool jump, bool damageBlink, Vector3 position, Quaternion rotation)
		{
			this.SetupOnStart();
			if (damageBlink)
			{
				this.StartDamageBlink();
			}
			this.WarpAndCheckOverlap(position, rotation);
			if (jump)
			{
				this.ClearAirAction();
				this.GetAnimator().CrossFade("SpringJump", 0.01f);
				JumpSpringParameter jumpSpringParameter = this.CreateEnteringParameter<JumpSpringParameter>();
				jumpSpringParameter.Set(base.transform.position, base.transform.rotation, 7f, 0.3f);
				this.ChangeState(STATE_ID.SpringJump);
			}
			else
			{
				this.m_numAirAction = 99;
				this.GetAnimator().Play("Fall");
				this.Movement.Velocity = new Vector3(0f, 2f, 0f);
				this.ChangeState(STATE_ID.Fall);
			}
			if (this.m_information.NumRings == 0)
			{
				StateUtil.SetEmergency(this, true);
			}
		}

		// Token: 0x06003F58 RID: 16216 RVA: 0x001492D8 File Offset: 0x001474D8
		private void WarpAndCheckOverlap(Vector3 pos, Quaternion rotation)
		{
			CapsuleCollider component = base.GetComponent<CapsuleCollider>();
			this.Movement.ResetPosition(pos);
			this.Movement.ResetRotation(rotation);
			float num = Mathf.Max(component.height, component.radius) + 0.2f;
			Vector3 position = pos + component.center.y * base.transform.up;
			int layerMask = 1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Terrain");
			Collider[] array = Physics.OverlapSphere(position, num, layerMask);
			if (array.Length > 0)
			{
				Vector3 a = -this.Movement.GetGravityDir();
				float num2 = 5f;
				Vector3 a2 = pos + a * num2;
				num = component.radius;
				float d = component.height * 0.5f - num;
				Vector3 a3 = a2 + base.transform.TransformDirection(component.center);
				Vector3 point = a3 - a * d;
				Vector3 point2 = a3 + a * d;
				Vector3 vector = -a;
				RaycastHit raycastHit;
				if (Physics.CapsuleCast(point, point2, num, vector, out raycastHit, num2, layerMask))
				{
					Vector3 b = vector * (raycastHit.distance - 0.02f);
					pos = a2 + b;
					this.Movement.ResetPosition(pos);
					global::Debug.Log(string.Concat(new object[]
					{
						"WarpAndCheckOverlap CapsuleCast Hit:",
						pos.x,
						" ",
						pos.y,
						" ",
						pos.z
					}));
				}
			}
		}

		// Token: 0x06003F59 RID: 16217 RVA: 0x00149498 File Offset: 0x00147698
		public void DeactiveCharacter()
		{
			StateUtil.DeactiveCombo(this, true);
			StateUtil.DeactiveInvincible(this);
			StateUtil.DeactiveBarrier(this);
			StateUtil.DeactiveMagetObject(this);
			StateUtil.DeactiveTrampoline(this);
			bool flag = this.TestStatus(Status.Emergency);
			if (flag)
			{
				StateUtil.SetEmergency(this, false);
			}
			this.m_status.Reset();
		}

		// Token: 0x06003F5A RID: 16218 RVA: 0x001494E8 File Offset: 0x001476E8
		public void AddAirAction()
		{
			this.m_numAirAction++;
		}

		// Token: 0x06003F5B RID: 16219 RVA: 0x001494F8 File Offset: 0x001476F8
		public void ClearAirAction()
		{
			this.m_numAirAction = 0;
		}

		// Token: 0x06003F5C RID: 16220 RVA: 0x00149504 File Offset: 0x00147704
		public void SetAirAction(int num)
		{
			this.m_numAirAction = num;
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06003F5D RID: 16221 RVA: 0x00149510 File Offset: 0x00147710
		public int NumAirAction
		{
			get
			{
				return this.m_numAirAction;
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06003F5E RID: 16222 RVA: 0x00149518 File Offset: 0x00147718
		public int NumEnableJump
		{
			get
			{
				return this.m_numEnableJump;
			}
		}

		// Token: 0x06003F5F RID: 16223 RVA: 0x00149520 File Offset: 0x00147720
		public CharacterLoopEffect GetSpinAttackEffect()
		{
			string text = "CharacterSpinAttack";
			if (this.m_wispBoostLevel != WispBoostLevel.NONE)
			{
				text = text + "Lv" + ((int)(this.m_wispBoostLevel + 1)).ToString();
			}
			return GameObjectUtil.FindChildGameObjectComponent<CharacterLoopEffect>(base.gameObject, text);
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06003F60 RID: 16224 RVA: 0x00149568 File Offset: 0x00147768
		public CharaType charaType
		{
			get
			{
				return this.m_charaType;
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06003F61 RID: 16225 RVA: 0x00149570 File Offset: 0x00147770
		public WispBoostLevel BossBoostLevel
		{
			get
			{
				return this.m_wispBoostLevel;
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06003F62 RID: 16226 RVA: 0x00149578 File Offset: 0x00147778
		public string BossBoostEffect
		{
			get
			{
				return this.m_wispBoostEffect;
			}
		}

		// Token: 0x06003F63 RID: 16227 RVA: 0x00149580 File Offset: 0x00147780
		public void SetBoostLevel(WispBoostLevel wispBoostLevel, string effect)
		{
			this.m_wispBoostLevel = wispBoostLevel;
			this.m_wispBoostEffect = effect;
		}

		// Token: 0x06003F64 RID: 16228 RVA: 0x00149590 File Offset: 0x00147790
		public void SetChangePhantomCancel(ItemType itemType)
		{
			this.m_changePhantomCancel = itemType;
		}

		// Token: 0x06003F65 RID: 16229 RVA: 0x0014959C File Offset: 0x0014779C
		public ItemType GetChangePhantomCancel()
		{
			return this.m_changePhantomCancel;
		}

		// Token: 0x06003F66 RID: 16230 RVA: 0x001495A4 File Offset: 0x001477A4
		public void OnAddRings(int numRing)
		{
			this.m_information.AddNumRings(numRing);
			ObjUtil.AddCombo();
			StateUtil.SetEmergency(this, false);
		}

		// Token: 0x06003F67 RID: 16231 RVA: 0x001495C0 File Offset: 0x001477C0
		private void OnMsgTutorialGetRingNum(MsgTutorialGetRingNum msg)
		{
			msg.m_ring = this.m_information.NumRings;
		}

		// Token: 0x06003F68 RID: 16232 RVA: 0x001495D4 File Offset: 0x001477D4
		private void OnMsgTutorialResetForRetry(MsgTutorialResetForRetry msg)
		{
			this.m_information.SetNumRings(msg.m_ring);
			if (msg.m_blink)
			{
				StateUtil.SetEmergency(this, msg.m_ring == 0);
			}
		}

		// Token: 0x06003F69 RID: 16233 RVA: 0x00149604 File Offset: 0x00147804
		private void OnResetRingsForCheckPoint(MsgPlayerTransferRing msg)
		{
			if (msg.m_hud)
			{
				StateUtil.SetEmergency(this, true);
			}
		}

		// Token: 0x06003F6A RID: 16234 RVA: 0x00149618 File Offset: 0x00147818
		private void OnResetRingsForContinue()
		{
			StateUtil.SetEmergency(this, false);
		}

		// Token: 0x06003F6B RID: 16235 RVA: 0x00149624 File Offset: 0x00147824
		private void OnDebugAddDistance(int distance)
		{
		}

		// Token: 0x06003F6C RID: 16236 RVA: 0x00149628 File Offset: 0x00147828
		private void OnDebugWarpPlayer(Vector3 pos)
		{
		}

		// Token: 0x06003F6D RID: 16237 RVA: 0x0014962C File Offset: 0x0014782C
		private bool IsStompHitObject(Collider other)
		{
			float vertVelocityScalar = this.Movement.GetVertVelocityScalar();
			if (vertVelocityScalar > -1f)
			{
				return false;
			}
			Vector3 b = other.transform.position;
			CapsuleCollider capsuleCollider = other as CapsuleCollider;
			if (capsuleCollider != null)
			{
				b = capsuleCollider.transform.TransformPoint(capsuleCollider.center);
			}
			else
			{
				SphereCollider sphereCollider = other as SphereCollider;
				if (sphereCollider != null)
				{
					b = sphereCollider.transform.TransformPoint(sphereCollider.center);
				}
				else
				{
					BoxCollider boxCollider = other as BoxCollider;
					if (boxCollider != null)
					{
						b = boxCollider.transform.TransformPoint(boxCollider.center);
					}
				}
			}
			Vector3 vector = base.transform.TransformPoint(StateUtil.GetBodyCenterPosition(this)) - b;
			return vector.sqrMagnitude > float.Epsilon && Vector3.Dot(vector.normalized, base.transform.up) > Mathf.Cos(0.017453292f * this.Parameter.m_enableStompDec);
		}

		// Token: 0x06003F6E RID: 16238 RVA: 0x0014973C File Offset: 0x0014793C
		public void OnTriggerEnter(Collider other)
		{
			AttackPower attackPower = this.m_attack;
			uint num = this.m_attackAttribute;
			if (StateUtil.IsInvincibleActive(this))
			{
				attackPower = AttackPower.PlayerInvincible;
				num |= 16U;
			}
			else if (attackPower == AttackPower.PlayerStomp && this.IsStompHitObject(other))
			{
				attackPower = AttackPower.PlayerSpin;
				if (this.m_attribute == CharacterAttribute.POWER)
				{
					num |= 8U;
				}
			}
			MsgHitDamage msgHitDamage = new MsgHitDamage(base.gameObject, attackPower);
			msgHitDamage.m_attackAttribute = num;
			GameObjectUtil.SendDelayedMessageToGameObject(other.gameObject, "OnDamageHit", msgHitDamage);
		}

		// Token: 0x06003F6F RID: 16239 RVA: 0x001497B8 File Offset: 0x001479B8
		public void OnDefrayRing()
		{
			if (this.m_information.NumRings == 0)
			{
				StateUtil.SetEmergency(this, true);
			}
		}

		// Token: 0x06003F70 RID: 16240 RVA: 0x001497D4 File Offset: 0x001479D4
		public void OnDamageHit(MsgHitDamage msg)
		{
			if (!this.IsDamaged() && !this.IsDead() && !this.IsNowLastChance() && !this.IsHold())
			{
				this.m_notDropRing = false;
				if (StateUtil.IsInvincibleActive(this))
				{
					return;
				}
				int num = (int)this.m_defense;
				Collider component = msg.m_sender.GetComponent<Collider>();
				if (component != null && this.IsStompHitObject(component))
				{
					num = 2;
				}
				if (msg.m_attackPower > num)
				{
					CharacterBarrier barrier = StateUtil.GetBarrier(this);
					if (barrier != null)
					{
						barrier.Damaged();
						this.StartDamageBlink();
						return;
					}
					Vector3 bodyCenterPosition = StateUtil.GetBodyCenterPosition(this);
					Vector3 position = base.transform.TransformPoint(bodyCenterPosition);
					StateUtil.CreateEffect(this, position, base.transform.rotation, "ef_pl_damage_com01", true);
					if (this.m_information.NumRings == 0)
					{
						this.ChangeState(STATE_ID.Dead);
						return;
					}
					if (StageAbilityManager.Instance != null && StageAbilityManager.Instance.HasChaoAbility(ChaoAbility.GUARD_DROP_RING))
					{
						float chaoAbilityValue = StageAbilityManager.Instance.GetChaoAbilityValue(ChaoAbility.GUARD_DROP_RING);
						float num2 = UnityEngine.Random.Range(0f, 99.9f);
						if (chaoAbilityValue >= num2)
						{
							this.m_notDropRing = true;
							ObjUtil.RequestStartAbilityToChao(ChaoAbility.GUARD_DROP_RING, false);
						}
					}
					if (!this.m_notDropRing)
					{
						ObjUtil.CreateLostRing(base.transform.position, base.transform.rotation, this.m_information.NumRings);
						this.m_information.LostRings();
						StateUtil.SetEmergency(this, true);
					}
					ObjUtil.RequestStartAbilityToChao(ChaoAbility.DAMAGE_TRAMPOLINE, false);
					ObjUtil.RequestStartAbilityToChao(ChaoAbility.DAMAGE_DESTROY_ALL, false);
					this.m_levelInformation.Missed = true;
					if (!this.IsNowPhantom())
					{
						this.ChangeState(STATE_ID.Damage);
					}
					return;
				}
			}
		}

		// Token: 0x06003F71 RID: 16241 RVA: 0x00149990 File Offset: 0x00147B90
		private void OnDamageSucceed(MsgHitDamageSucceed msg)
		{
			if (this.m_fsm != null)
			{
				this.m_fsm.CurrentState.DispatchMessage(this, msg.ID, msg);
			}
		}

		// Token: 0x06003F72 RID: 16242 RVA: 0x001499C4 File Offset: 0x00147BC4
		public void OnAttackGuard(MsgAttackGuard msg)
		{
			if (this.m_fsm != null)
			{
				this.m_fsm.CurrentState.DispatchMessage(this, msg.ID, msg);
			}
		}

		// Token: 0x06003F73 RID: 16243 RVA: 0x001499F8 File Offset: 0x00147BF8
		public void OnFallingDead()
		{
			if (this.NowPhantomType == PhantomType.DRILL)
			{
				return;
			}
			if (!this.IsDead() && !this.IsNowLastChance() && !this.IsHold())
			{
				this.ChangeState(STATE_ID.FallingDead);
			}
		}

		// Token: 0x06003F74 RID: 16244 RVA: 0x00149A3C File Offset: 0x00147C3C
		private void OnSpringImpulse(MsgOnSpringImpulse msg)
		{
			if (!this.IsDead() && !this.IsNowPhantom() && !this.IsNowLastChance() && !this.IsHold())
			{
				JumpSpringParameter jumpSpringParameter = this.CreateEnteringParameter<JumpSpringParameter>();
				jumpSpringParameter.Set(msg.m_position, msg.m_rotation, msg.m_firstSpeed, msg.m_outOfControl);
				this.ChangeState(STATE_ID.SpringJump);
				msg.m_succeed = true;
			}
		}

		// Token: 0x06003F75 RID: 16245 RVA: 0x00149AA8 File Offset: 0x00147CA8
		private void OnDashRingImpulse(MsgOnDashRingImpulse msg)
		{
			if (!this.IsDead() && !this.IsNowPhantom() && !this.IsNowLastChance() && !this.IsHold())
			{
				JumpSpringParameter jumpSpringParameter = this.CreateEnteringParameter<JumpSpringParameter>();
				jumpSpringParameter.Set(msg.m_position, msg.m_rotation, msg.m_firstSpeed, msg.m_outOfControl);
				this.ChangeState(STATE_ID.DashRing);
				msg.m_succeed = true;
			}
		}

		// Token: 0x06003F76 RID: 16246 RVA: 0x00149B14 File Offset: 0x00147D14
		private void OnCannonImpulse(MsgOnCannonImpulse msg)
		{
			if (this.m_fsm != null && this.m_fsm.CurrentState != null)
			{
				this.m_fsm.CurrentState.DispatchMessage(this, msg.ID, msg);
			}
		}

		// Token: 0x06003F77 RID: 16247 RVA: 0x00149B58 File Offset: 0x00147D58
		private void OnAbidePlayer(MsgOnAbidePlayer msg)
		{
			if (!this.IsDead() && !this.IsNowPhantom() && !this.IsNowLastChance())
			{
				CannonReachParameter cannonReachParameter = this.CreateEnteringParameter<CannonReachParameter>();
				cannonReachParameter.Set(msg.m_position, msg.m_rotation, msg.m_height, msg.m_abideObject);
				this.ChangeState(STATE_ID.ReachCannon);
				msg.m_succeed = true;
			}
		}

		// Token: 0x06003F78 RID: 16248 RVA: 0x00149BBC File Offset: 0x00147DBC
		private void OnJumpBoardHit(MsgOnJumpBoardHit msg)
		{
			if (!this.IsDead() && !this.IsNowPhantom() && !this.IsNowLastChance() && !this.IsHold())
			{
				CharacterCheckTrickJump characterCheckTrickJump = base.GetComponent<CharacterCheckTrickJump>();
				if (characterCheckTrickJump == null)
				{
					characterCheckTrickJump = base.gameObject.AddComponent<CharacterCheckTrickJump>();
				}
				if (characterCheckTrickJump != null)
				{
					characterCheckTrickJump.Reset();
				}
			}
		}

		// Token: 0x06003F79 RID: 16249 RVA: 0x00149C28 File Offset: 0x00147E28
		private void OnJumpBoardJump(MsgOnJumpBoardJump msg)
		{
			if (!this.IsDead() && !this.IsNowPhantom() && !this.IsNowLastChance() && !this.IsHold())
			{
				bool flag = false;
				CharacterCheckTrickJump component = base.GetComponent<CharacterCheckTrickJump>();
				if (component != null)
				{
					flag = component.IsTouched;
					UnityEngine.Object.Destroy(component);
				}
				TrickJumpParameter trickJumpParameter = this.CreateEnteringParameter<TrickJumpParameter>();
				if (flag)
				{
					trickJumpParameter.Set(msg.m_position, msg.m_succeedRotation, msg.m_succeedFirstSpeed, msg.m_succeedOutOfcontrol, msg.m_succeedRotation, msg.m_succeedFirstSpeed, msg.m_succeedOutOfcontrol, flag);
				}
				else
				{
					trickJumpParameter.Set(msg.m_position, msg.m_missRotation, msg.m_missFirstSpeed, msg.m_missOutOfcontrol, msg.m_succeedRotation, msg.m_succeedFirstSpeed, msg.m_succeedOutOfcontrol, flag);
				}
				this.ChangeState(STATE_ID.TrickJump);
				msg.m_succeed = true;
			}
		}

		// Token: 0x06003F7A RID: 16250 RVA: 0x00149D08 File Offset: 0x00147F08
		private void OnUpSpeedLevel(MsgUpSpeedLevel msg)
		{
			this.m_nowSpeedLevel = msg.m_level;
			this.SetSpeedLevel(this.m_nowSpeedLevel);
		}

		// Token: 0x06003F7B RID: 16251 RVA: 0x00149D24 File Offset: 0x00147F24
		private void OnRunLoopPath(MsgRunLoopPath msg)
		{
			if (this.m_fsm != null)
			{
				this.m_fsm.CurrentState.DispatchMessage(this, msg.ID, msg);
			}
		}

		// Token: 0x06003F7C RID: 16252 RVA: 0x00149D58 File Offset: 0x00147F58
		private void OnUseItem(MsgUseItem msg)
		{
			if (this.IsDead() || this.IsNowLastChance())
			{
				return;
			}
			switch (msg.m_itemType)
			{
			case ItemType.INVINCIBLE:
				if (!this.IsNowPhantom())
				{
					StateUtil.ActiveInvincible(this, msg.m_time);
				}
				break;
			case ItemType.BARRIER:
				StateUtil.ActiveBarrier(this);
				if (this.IsNowPhantom())
				{
					StateUtil.SetNotDrawBarrierEffect(this, true);
				}
				break;
			case ItemType.MAGNET:
				StateUtil.ActiveMagnetObject(this, msg.m_time);
				break;
			case ItemType.TRAMPOLINE:
				StateUtil.ActiveTrampoline(this, msg.m_time);
				break;
			case ItemType.COMBO:
				StateUtil.ActiveCombo(this, msg.m_time);
				break;
			case ItemType.LASER:
				if (this.NowPhantomType == PhantomType.NONE)
				{
					StateUtil.ChangeStateToChangePhantom(this, PhantomType.LASER, msg.m_time);
					return;
				}
				break;
			case ItemType.DRILL:
				if (this.NowPhantomType == PhantomType.NONE)
				{
					StateUtil.ChangeStateToChangePhantom(this, PhantomType.DRILL, msg.m_time);
					return;
				}
				break;
			case ItemType.ASTEROID:
				if (this.NowPhantomType == PhantomType.NONE)
				{
					StateUtil.ChangeStateToChangePhantom(this, PhantomType.ASTEROID, msg.m_time);
					return;
				}
				break;
			}
		}

		// Token: 0x06003F7D RID: 16253 RVA: 0x00149E7C File Offset: 0x0014807C
		private void OnInvalidateItem(MsgInvalidateItem msg)
		{
			if (this.IsDead())
			{
				return;
			}
			if (this.IsNowLastChance())
			{
				if (msg.m_itemType == ItemType.COMBO)
				{
					StateUtil.DeactiveCombo(this, true);
				}
				return;
			}
			switch (msg.m_itemType)
			{
			case ItemType.INVINCIBLE:
				StateUtil.DeactiveInvincible(this);
				break;
			case ItemType.MAGNET:
				StateUtil.DeactiveMagetObject(this);
				break;
			case ItemType.TRAMPOLINE:
				StateUtil.DeactiveTrampoline(this);
				break;
			case ItemType.COMBO:
				StateUtil.DeactiveCombo(this, true);
				break;
			case ItemType.LASER:
				if (this.m_fsm != null && this.m_fsm.CurrentState != null)
				{
					StateUtil.SetChangePhantomCancel(this, msg.m_itemType);
					this.m_fsm.CurrentState.DispatchMessage(this, msg.ID, msg);
				}
				break;
			case ItemType.DRILL:
				if (this.m_fsm != null && this.m_fsm.CurrentState != null)
				{
					StateUtil.SetChangePhantomCancel(this, msg.m_itemType);
					this.m_fsm.CurrentState.DispatchMessage(this, msg.ID, msg);
				}
				break;
			case ItemType.ASTEROID:
				if (this.m_fsm != null && this.m_fsm.CurrentState != null)
				{
					StateUtil.SetChangePhantomCancel(this, msg.m_itemType);
					this.m_fsm.CurrentState.DispatchMessage(this, msg.ID, msg);
				}
				break;
			}
		}

		// Token: 0x06003F7E RID: 16254 RVA: 0x00149FEC File Offset: 0x001481EC
		private void WarpPosition(Vector3 pos, Quaternion rotation, bool hold)
		{
			Vector3 gravityDir = this.Movement.GetGravityDir();
			float distance = 0.2f;
			RaycastHit raycastHit;
			if (Physics.Raycast(pos, gravityDir, out raycastHit, distance))
			{
				pos = raycastHit.point + raycastHit.normal * 0.01f;
			}
			this.Movement.ResetPosition(pos);
			this.Movement.ResetRotation(rotation);
			if (this.m_information)
			{
				this.m_information.SetTransform(base.transform);
			}
			if (hold && this.m_fsm != null && this.m_fsm.CurrentStateID != (StateID)1)
			{
				this.ChangeState(STATE_ID.Hold);
			}
		}

		// Token: 0x06003F7F RID: 16255 RVA: 0x0014A09C File Offset: 0x0014829C
		private void OnMsgStageReplace(MsgStageReplace msg)
		{
			Vector3 vector = msg.m_position;
			Vector3 b = new Vector3(0.5f, 0.2f, 0f);
			vector += b;
			this.WarpPosition(vector, msg.m_rotation, true);
		}

		// Token: 0x06003F80 RID: 16256 RVA: 0x0014A0DC File Offset: 0x001482DC
		private void OnMsgWarpPlayer(MsgWarpPlayer msg)
		{
			this.WarpPosition(msg.m_position, msg.m_rotation, msg.m_hold);
		}

		// Token: 0x06003F81 RID: 16257 RVA: 0x0014A0F8 File Offset: 0x001482F8
		private void OnMsgStageRestart(MsgStageRestart msg)
		{
			if (this.m_fsm != null && this.m_fsm.CurrentState != null)
			{
				this.m_fsm.CurrentState.DispatchMessage(this, msg.ID, msg);
			}
		}

		// Token: 0x06003F82 RID: 16258 RVA: 0x0014A13C File Offset: 0x0014833C
		private void OnMsgPLHold(MsgPLHold msg)
		{
			this.ChangeState(STATE_ID.Hold);
		}

		// Token: 0x06003F83 RID: 16259 RVA: 0x0014A148 File Offset: 0x00148348
		private void OnMsgPLReleaseHold(MsgPLReleaseHold msg)
		{
			if (this.m_fsm != null && this.m_fsm.CurrentState != null)
			{
				this.m_fsm.CurrentState.DispatchMessage(this, msg.ID, msg);
			}
		}

		// Token: 0x06003F84 RID: 16260 RVA: 0x0014A18C File Offset: 0x0014838C
		private void OnPauseItemOnBoss(MsgPauseItemOnBoss msg)
		{
			if (this.m_fsm != null && this.m_fsm.CurrentState != null)
			{
				this.m_fsm.CurrentState.DispatchMessage(this, msg.ID, msg);
			}
		}

		// Token: 0x06003F85 RID: 16261 RVA: 0x0014A1D0 File Offset: 0x001483D0
		private void OnMsgExitStage(MsgExitStage msg)
		{
			base.enabled = false;
			this.Movement.enabled = false;
			StateUtil.DeactiveCombo(this, true);
		}

		// Token: 0x06003F86 RID: 16262 RVA: 0x0014A1F8 File Offset: 0x001483F8
		private void OnMsgAbilityEffectStart(MsgAbilityEffectStart msg)
		{
			if (msg.m_loop)
			{
				GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, msg.m_effectName);
				if (gameObject != null)
				{
					UnityEngine.Object.Destroy(gameObject);
				}
			}
			GameObject gameObject2 = StateUtil.CreateEffect(this, msg.m_effectName, true, ResourceCategory.CHAO_MODEL);
			if (gameObject2 != null && msg.m_center)
			{
				StateUtil.SetObjectLocalPositionToCenter(this, gameObject2);
			}
		}

		// Token: 0x06003F87 RID: 16263 RVA: 0x0014A264 File Offset: 0x00148464
		private void OnMsgAbilityEffectEnd(MsgAbilityEffectEnd msg)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, msg.m_effectName);
			if (gameObject != null)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}

		// Token: 0x06003F88 RID: 16264 RVA: 0x0014A298 File Offset: 0x00148498
		private void OnBossBoostLevel(MsgBossBoostLevel msg)
		{
			this.SetBoostLevel(msg.m_wispBoostLevel, msg.m_wispBoostEffect);
		}

		// Token: 0x06003F89 RID: 16265 RVA: 0x0014A2AC File Offset: 0x001484AC
		public static FSMStateFactory<CharacterState>[] GetCommonFSMTable()
		{
			return new FSMStateFactory<CharacterState>[]
			{
				new FSMStateFactory<CharacterState>(1, new StateEdit()),
				new FSMStateFactory<CharacterState>(2, new StateRun()),
				new FSMStateFactory<CharacterState>(3, new StateJump()),
				new FSMStateFactory<CharacterState>(8, new StateFall()),
				new FSMStateFactory<CharacterState>(9, new StateDamage()),
				new FSMStateFactory<CharacterState>(5, new StateSpringJump()),
				new FSMStateFactory<CharacterState>(6, new StateDashRing()),
				new FSMStateFactory<CharacterState>(10, new StateFallingDead()),
				new FSMStateFactory<CharacterState>(11, new StateDead()),
				new FSMStateFactory<CharacterState>(7, new StateAfterSpinAttack()),
				new FSMStateFactory<CharacterState>(12, new StateRunLoop()),
				new FSMStateFactory<CharacterState>(13, new StateChangePhantom()),
				new FSMStateFactory<CharacterState>(14, new StateReturnFromPhantom()),
				new FSMStateFactory<CharacterState>(15, new StatePhantomLaser()),
				new FSMStateFactory<CharacterState>(16, new StatePhantomLaserBoss()),
				new FSMStateFactory<CharacterState>(17, new StatePhantomDrill()),
				new FSMStateFactory<CharacterState>(18, new StatePhantomDrillBoss()),
				new FSMStateFactory<CharacterState>(19, new StatePhantomAsteroid()),
				new FSMStateFactory<CharacterState>(20, new StatePhantomAsteroidBoss()),
				new FSMStateFactory<CharacterState>(21, new StateReachCannon()),
				new FSMStateFactory<CharacterState>(22, new StateLaunchCannon()),
				new FSMStateFactory<CharacterState>(23, new StateHold()),
				new FSMStateFactory<CharacterState>(24, new StateTrickJump()),
				new FSMStateFactory<CharacterState>(25, new StateStumble()),
				new FSMStateFactory<CharacterState>(26, new StateDoubleJump()),
				new FSMStateFactory<CharacterState>(27, new StateThirdJump()),
				new FSMStateFactory<CharacterState>(28, new StateLastChance())
			};
		}

		// Token: 0x04003637 RID: 13879
		private PlayerInformation m_information;

		// Token: 0x04003638 RID: 13880
		private CameraManager m_camera;

		// Token: 0x04003639 RID: 13881
		private CharaType m_charaType = CharaType.UNKNOWN;

		// Token: 0x0400363A RID: 13882
		private bool m_subPlayer;

		// Token: 0x0400363B RID: 13883
		public CharacterInput m_input;

		// Token: 0x0400363C RID: 13884
		private CharacterMovement m_movement;

		// Token: 0x0400363D RID: 13885
		private FSMSystem<CharacterState> m_fsm;

		// Token: 0x0400363E RID: 13886
		private Animator m_bodyAnimator;

		// Token: 0x0400363F RID: 13887
		private string m_bodyName;

		// Token: 0x04003640 RID: 13888
		private string m_charaName;

		// Token: 0x04003641 RID: 13889
		private string m_suffixName;

		// Token: 0x04003642 RID: 13890
		private Bitset32 m_status;

		// Token: 0x04003643 RID: 13891
		private Bitset32 m_visibleStatus;

		// Token: 0x04003644 RID: 13892
		private Bitset32 m_options;

		// Token: 0x04003645 RID: 13893
		private StateEnteringParameter m_enteringParam;

		// Token: 0x04003646 RID: 13894
		private CharacterBlinkTimer m_blinkTimer;

		// Token: 0x04003647 RID: 13895
		private AttackPower m_attack;

		// Token: 0x04003648 RID: 13896
		private DefensePower m_defense;

		// Token: 0x04003649 RID: 13897
		private uint m_attackAttribute;

		// Token: 0x0400364A RID: 13898
		private PlayerSpeed m_nowSpeedLevel;

		// Token: 0x0400364B RID: 13899
		private PhantomType m_nowPhantomType = PhantomType.NONE;

		// Token: 0x0400364C RID: 13900
		private CharacterAttribute m_attribute;

		// Token: 0x0400364D RID: 13901
		private TeamAttribute m_teamAttribute;

		// Token: 0x0400364E RID: 13902
		[SerializeField]
		private float m_defaultSpeed = 8f;

		// Token: 0x0400364F RID: 13903
		[SerializeField]
		private bool m_notLoadCharaParameter = true;

		// Token: 0x04003650 RID: 13904
		private PlayingCharacterType m_playingCharacterType;

		// Token: 0x04003651 RID: 13905
		private StageBlockPathManager m_blockPathManager;

		// Token: 0x04003652 RID: 13906
		private CharacterContainer m_characterContainer;

		// Token: 0x04003653 RID: 13907
		private LevelInformation m_levelInformation;

		// Token: 0x04003654 RID: 13908
		private StageScoreManager m_scoreManager;

		// Token: 0x04003655 RID: 13909
		private bool m_nowOnDestroy;

		// Token: 0x04003656 RID: 13910
		private int m_numAirAction;

		// Token: 0x04003657 RID: 13911
		private int m_numEnableJump = 1;

		// Token: 0x04003658 RID: 13912
		public float m_hitWallTimer;

		// Token: 0x04003659 RID: 13913
		private ItemType m_changePhantomCancel = ItemType.UNKNOWN;

		// Token: 0x0400365A RID: 13914
		private WispBoostLevel m_wispBoostLevel = WispBoostLevel.NONE;

		// Token: 0x0400365B RID: 13915
		private string m_wispBoostEffect = string.Empty;

		// Token: 0x0400365C RID: 13916
		public bool m_isEdit;

		// Token: 0x0400365D RID: 13917
		public bool m_notDeadNoRing;

		// Token: 0x0400365E RID: 13918
		public bool m_noCrushDead;

		// Token: 0x0400365F RID: 13919
		public bool m_notDropRing;

		// Token: 0x04003660 RID: 13920
		private bool m_isAlreadySetupModel;
	}
}
