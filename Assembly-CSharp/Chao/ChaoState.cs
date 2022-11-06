using System;
using System.Collections;
using App.Utility;
using DataTable;
using Message;
using UnityEngine;

namespace Chao
{
	// Token: 0x02000133 RID: 307
	public class ChaoState : MonoBehaviour
	{
		// Token: 0x0600092C RID: 2348 RVA: 0x00033FA8 File Offset: 0x000321A8
		private void CreateCollider()
		{
			base.gameObject.AddComponent(typeof(SphereCollider));
			SphereCollider component = base.gameObject.GetComponent<SphereCollider>();
			if (component != null)
			{
				component.isTrigger = true;
				Vector3 colliCenter = this.m_setupdata.ColliCenter;
				component.center = colliCenter;
				component.radius = this.m_setupdata.ColliRadius;
			}
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00034010 File Offset: 0x00032210
		private void SetupModelAndParameter()
		{
			ResourceManager instance = ResourceManager.Instance;
			if (instance != null)
			{
				string name = null;
				GameObject gameObject = GameObjectUtil.FindChildGameObject(instance.gameObject, "ChaoModel" + this.m_chao_id.ToString("0000"));
				if (gameObject != null)
				{
					int childCount = gameObject.transform.childCount;
					for (int i = 0; i < childCount; i++)
					{
						GameObject gameObject2 = gameObject.transform.GetChild(i).gameObject;
						if (gameObject2.name.IndexOf("cho_") == 0)
						{
							name = gameObject2.name;
						}
					}
					ChaoSetupParameter component = gameObject.GetComponent<ChaoSetupParameter>();
					if (component != null)
					{
						component.Data.CopyTo(this.m_setupdata);
					}
				}
				GameObject gameObject3 = instance.GetGameObject(ResourceCategory.CHAO_MODEL, name);
				this.CreateChildModelObject(gameObject3, true);
			}
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x000340F4 File Offset: 0x000322F4
		private void SetupModelPostureController(GameObject modelObject)
		{
			this.m_modelControl = modelObject.AddComponent<ChaoModelPostureController>();
			this.m_modelControl.SetModelObject(modelObject);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00034110 File Offset: 0x00032310
		private void CreateChildModelObject(GameObject src_obj, bool active)
		{
			if (src_obj != null)
			{
				Vector3 localPosition = src_obj.transform.localPosition;
				Quaternion localRotation = src_obj.transform.localRotation;
				GameObject gameObject = UnityEngine.Object.Instantiate(src_obj, localPosition, localRotation) as GameObject;
				if (gameObject != null)
				{
					gameObject.transform.parent = base.transform;
					gameObject.SetActive(active);
					gameObject.transform.localPosition = localPosition;
					gameObject.transform.localRotation = localRotation;
					gameObject.name = src_obj.name;
					this.OffAnimatorRootAnimation(gameObject);
					this.m_modelObject = gameObject;
					this.SetupModelPostureController(this.m_modelObject);
					float shaderOffsetValue = this.GetShaderOffsetValue();
					this.ChangeShaderOffsetChild(this.m_modelObject, shaderOffsetValue);
					ChaoPartsObjectMagnet component = gameObject.GetComponent<ChaoPartsObjectMagnet>();
					if (component != null)
					{
						component.Setup();
					}
				}
			}
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x000341E4 File Offset: 0x000323E4
		private void OffAnimatorRootAnimation(GameObject modelObject)
		{
			if (modelObject != null)
			{
				Animator component = modelObject.GetComponent<Animator>();
				if (component != null)
				{
					component.applyRootMotion = false;
				}
			}
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x00034218 File Offset: 0x00032418
		private float GetShaderOffsetValue()
		{
			ChaoType chaoType = ChaoUtility.GetChaoType(base.gameObject);
			float result = (chaoType != ChaoType.MAIN) ? 15f : 5f;
			if (this.m_setupdata.ShaderOffset != ShaderType.NORMAL)
			{
				ChaoType type = (chaoType != ChaoType.MAIN) ? ChaoType.MAIN : ChaoType.SUB;
				if (this.m_setupdata.ShaderOffset == ShaderType.MAIN)
				{
					if (ChaoUtility.GetChaoShaderType(base.gameObject.transform.parent.gameObject, type) == ShaderType.SUB)
					{
						result = 5f;
					}
				}
				else if (this.m_setupdata.ShaderOffset == ShaderType.SUB && ChaoUtility.GetChaoShaderType(base.gameObject.transform.parent.gameObject, type) == ShaderType.MAIN)
				{
					result = 15f;
				}
			}
			return result;
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x000342DC File Offset: 0x000324DC
		private void ChangeShaderOffsetChild(GameObject parent, float offset)
		{
			foreach (object obj in parent.transform)
			{
				Transform transform = (Transform)obj;
				this.ChangeShaderOffsetChild(transform.gameObject, offset);
				Renderer component = transform.GetComponent<Renderer>();
				if (component != null)
				{
					Material[] materials = component.materials;
					foreach (Material material in materials)
					{
						if (material.HasProperty("_OutlineZOffset"))
						{
							float @float = material.GetFloat("_OutlineZOffset");
							material.SetFloat("_OutlineZOffset", @float + offset);
						}
						if (material.HasProperty("_InnerZOffset"))
						{
							float float2 = material.GetFloat("_InnerZOffset");
							material.SetFloat("_InnerZOffset", float2 + offset);
						}
					}
				}
			}
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x000343F0 File Offset: 0x000325F0
		public void Start()
		{
			this.SetChaoData();
			if (this.m_chao_id < 0)
			{
				base.gameObject.SetActive(false);
				return;
			}
			this.SetupModelAndParameter();
			this.CreateTinyFsm();
			this.SetChaoMovement();
			base.enabled = false;
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00034438 File Offset: 0x00032638
		private void OnDestroy()
		{
			if (this.m_fsmBehavior != null)
			{
				this.m_fsmBehavior.ShutDown();
				this.m_fsmBehavior = null;
			}
			this.DestroyStateWork();
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00034464 File Offset: 0x00032664
		private void CreateTinyFsm()
		{
			this.m_fsmBehavior = (base.gameObject.AddComponent(typeof(TinyFsmBehavior)) as TinyFsmBehavior);
			if (this.m_fsmBehavior != null)
			{
				TinyFsmBehavior.Description description = new TinyFsmBehavior.Description(this);
				description.initState = new TinyFsmState(new EventFunction(this.StateInit));
				this.m_fsmBehavior.SetUp(description);
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x000344CC File Offset: 0x000326CC
		public GameObject ModelObject
		{
			get
			{
				return this.m_modelObject;
			}
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x000344D4 File Offset: 0x000326D4
		private StageItemManager GetItemManager()
		{
			if (StageItemManager.Instance != null)
			{
				return StageItemManager.Instance;
			}
			return null;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x000344F0 File Offset: 0x000326F0
		public T GetCurrentMovement<T>() where T : FSMState<ChaoMovement>
		{
			return this.m_movement.GetCurrentState<T>();
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x00034500 File Offset: 0x00032700
		public ChaoParameterData Parameter
		{
			get
			{
				if (this.m_parameter != null)
				{
					return this.m_parameter.Data;
				}
				return null;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x00034520 File Offset: 0x00032720
		public ShaderType ShaderOffset
		{
			get
			{
				if (this.m_setupdata != null)
				{
					return this.m_setupdata.ShaderOffset;
				}
				return ShaderType.NORMAL;
			}
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0003453C File Offset: 0x0003273C
		private void SetChaoData()
		{
			this.m_chao_type = ChaoUtility.GetChaoType(base.gameObject);
			SaveDataManager instance = SaveDataManager.Instance;
			if (instance != null)
			{
				ChaoType chao_type = this.m_chao_type;
				if (chao_type != ChaoType.MAIN)
				{
					if (chao_type == ChaoType.SUB)
					{
						this.m_chao_id = instance.PlayerData.SubChaoID;
					}
				}
				else
				{
					this.m_chao_id = instance.PlayerData.MainChaoID;
				}
			}
			GameObject gameObject = GameObject.Find("StageChao/ChaoParameter");
			if (gameObject != null)
			{
				this.m_parameter = gameObject.GetComponent<ChaoParameter>();
			}
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x000345D4 File Offset: 0x000327D4
		private static ChaoData.Rarity GetRarity(int chaoId)
		{
			ChaoData chaoData = ChaoTable.GetChaoData(chaoId);
			if (chaoData != null)
			{
				return chaoData.rarity;
			}
			return ChaoData.Rarity.NONE;
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x000345F8 File Offset: 0x000327F8
		private void RequestStartEffect()
		{
			if (!this.m_startEffect)
			{
				this.m_startEffect = true;
				StageAbilityManager instance = StageAbilityManager.Instance;
				if (instance != null)
				{
					instance.RequestPlayChaoEffect(ChaoAbility.ALL_BONUS_COUNT, this.m_chao_type);
					instance.RequestPlayChaoEffect(ChaoAbility.SCORE_COUNT, this.m_chao_type);
					instance.RequestPlayChaoEffect(ChaoAbility.RING_COUNT, this.m_chao_type);
					instance.RequestPlayChaoEffect(ChaoAbility.RED_RING_COUNT, this.m_chao_type);
					instance.RequestPlayChaoEffect(ChaoAbility.ANIMAL_COUNT, this.m_chao_type);
					instance.RequestPlayChaoEffect(ChaoAbility.RUNNIGN_DISTANCE, this.m_chao_type);
					instance.RequestPlayChaoEffect(ChaoAbility.RARE_ENEMY_UP, this.m_chao_type);
					instance.RequestPlayChaoEffect(ChaoAbility.ENEMY_SCORE, this.m_chao_type);
					instance.RequestPlayChaoEffect(ChaoAbility.COMBO_RECEPTION_TIME, this.m_chao_type);
				}
			}
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x000346A4 File Offset: 0x000328A4
		private void SetChaoMovement()
		{
			this.m_movement = ChaoMovement.Create(base.gameObject, this.m_setupdata);
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x000346C0 File Offset: 0x000328C0
		private void ChangeState(TinyFsmState nextState)
		{
			if (this.m_fsmBehavior != null)
			{
				this.m_fsmBehavior.ChangeState(nextState);
			}
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x000346E0 File Offset: 0x000328E0
		private void ChangeMovement(MOVESTATE_ID state)
		{
			if (this.m_movement != null)
			{
				this.m_movement.ChangeState(state);
			}
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00034700 File Offset: 0x00032900
		private TinyFsmState GetCurrentState()
		{
			if (this.m_fsmBehavior != null)
			{
				return this.m_fsmBehavior.GetCurrentState();
			}
			return null;
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00034720 File Offset: 0x00032920
		private void OnMsgReceive(MsgChaoState message)
		{
			if (message != null && this.m_fsmBehavior != null)
			{
				TinyFsmEvent signal = TinyFsmEvent.CreateMessage(message);
				this.m_fsmBehavior.Dispatch(signal);
			}
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00034758 File Offset: 0x00032958
		private void OnMsgStageReplace(MsgStageReplace msg)
		{
			this.ChangeState(new TinyFsmState(new EventFunction(this.StateComeIn)));
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00034774 File Offset: 0x00032974
		private void OnMsgStartBoss()
		{
			this.m_attackCount = 0;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x00034780 File Offset: 0x00032980
		private void OnStartLastChance(MsgStartLastChance message)
		{
			StageAbilityManager instance = StageAbilityManager.Instance;
			if (instance != null && instance.HasChaoAbility(ChaoAbility.LAST_CHANCE, this.m_chao_type) && message != null && this.m_fsmBehavior != null)
			{
				MsgChaoState msg = new MsgChaoState(MsgChaoState.State.LAST_CHANCE);
				TinyFsmEvent signal = TinyFsmEvent.CreateMessage(msg);
				this.m_fsmBehavior.Dispatch(signal);
			}
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x000347E4 File Offset: 0x000329E4
		private void OnEndLastChance(MsgEndLastChance message)
		{
			if (message != null && this.m_fsmBehavior != null)
			{
				MsgChaoState msg = new MsgChaoState(MsgChaoState.State.LAST_CHANCE_END);
				TinyFsmEvent signal = TinyFsmEvent.CreateMessage(msg);
				this.m_fsmBehavior.Dispatch(signal);
			}
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00034824 File Offset: 0x00032A24
		private void OnPauseChangeLevel()
		{
			this.SetMagnetPause(true);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00034830 File Offset: 0x00032A30
		private void OnResumeChangeLevel()
		{
			this.SetMagnetPause(false);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0003483C File Offset: 0x00032A3C
		private void OnMsgChaoAbilityStart(MsgChaoAbilityStart msg)
		{
			StageAbilityManager instance = StageAbilityManager.Instance;
			foreach (ChaoAbility ability2 in msg.m_ability)
			{
				if (instance != null && instance.HasChaoAbility(ability2, this.m_chao_type))
				{
					switch (ability2)
					{
					case ChaoAbility.ANIMAL_COUNT:
						if (this.m_chao_id == 2014)
						{
							ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_sp4_deatheggstar_flash_sr01", -1f);
							base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						}
						break;
					case ChaoAbility.COMBO_ITEM_BOX:
						if (!this.IsNowLastChance())
						{
							if (this.m_stateFlag.Test(0))
							{
								this.m_stateFlag.Set(1, true);
							}
							this.ChangeState(new TinyFsmState(new EventFunction(this.StateItemPresent)));
						}
						break;
					case ChaoAbility.COMBO_BARRIER:
						this.m_chaoItemType = ItemType.BARRIER;
						if (this.CheckItemPresent(this.m_chaoItemType))
						{
							PlayerInformation playerInformation = GameObjectUtil.FindGameObjectComponentWithTag<PlayerInformation>("StageManager", "PlayerInformation");
							if (playerInformation != null)
							{
								if (playerInformation.PhantomType == PhantomType.NONE)
								{
									ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_sp2_erazordjinn_magic_sr01", -1f);
									base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
								}
								StageItemManager itemManager = this.GetItemManager();
								if (itemManager != null)
								{
									MsgAddItemToManager value = new MsgAddItemToManager(this.m_chaoItemType);
									itemManager.SendMessage("OnAddItem", value, SendMessageOptions.DontRequireReceiver);
								}
							}
						}
						break;
					case ChaoAbility.COMBO_WIPE_OUT_ENEMY:
						if (this.m_stateFlag.Test(0))
						{
							this.m_stateFlag.Set(1, true);
						}
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateKillOut)));
						break;
					case ChaoAbility.COMBO_COLOR_POWER:
						if (this.m_stateFlag.Test(0))
						{
							this.m_stateFlag.Set(1, true);
						}
						this.ChangeState(new TinyFsmState(new EventFunction(this.StatePhantomPresent)));
						break;
					case ChaoAbility.COMBO_ALL_SPECIAL_CRYSTAL:
						base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						if (this.m_chao_id == 1018)
						{
							ObjUtil.LightPlaySE("act_chao_puyo", "SE");
						}
						break;
					case ChaoAbility.COMBO_DESTROY_TRAP:
						ObjUtil.LightPlaySE("act_chao_mag", "SE");
						ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_combo_trap_cry_c01", -1f);
						base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						GameObjectUtil.SendDelayedMessageToTagObjects("Gimmick", "OnMsgObjectDeadChaoCombo", new MsgObjectDead(ChaoAbility.COMBO_DESTROY_TRAP));
						break;
					case ChaoAbility.COMBO_DESTROY_AIR_TRAP:
						ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_combo_brk_airtrap_c01", -1f);
						base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						if (this.m_chao_id == 24)
						{
							ObjUtil.LightPlaySE("act_chao_puyo", "SE");
						}
						GameObjectUtil.SendDelayedMessageToTagObjects("Gimmick", "OnMsgObjectDeadChaoCombo", new MsgObjectDead(ChaoAbility.COMBO_DESTROY_AIR_TRAP));
						break;
					case ChaoAbility.COMBO_RECOVERY_ALL_OBJ:
						if (this.m_chao_id == 2012)
						{
							ObjUtil.LightPlaySE("act_chao_puyo", "SE");
						}
						ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_sp3_carbuncle_magic_sr01", -1f);
						base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						break;
					case ChaoAbility.COMBO_STEP_DESTROY_GET_10_RING:
						base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_combo_brk_airfloor_r01", -1f);
						GameObjectUtil.SendDelayedMessageToTagObjects("Gimmick", "OnMsgStepObjectDead", new MsgObjectDead());
						ObjUtil.LightPlaySE("act_chao_killerwhale", "SE");
						break;
					case ChaoAbility.COMBO_EQUIP_ITEM:
						if (!this.IsNowLastChance())
						{
							if (this.m_stateFlag.Test(0))
							{
								this.m_stateFlag.Set(1, true);
							}
							this.ChangeState(new TinyFsmState(new EventFunction(this.StatePresentEquipItem)));
						}
						break;
					case ChaoAbility.COLOR_POWER_SCORE:
					case ChaoAbility.ASTEROID_SCORE:
					case ChaoAbility.DRILL_SCORE:
					case ChaoAbility.LASER_SCORE:
					case ChaoAbility.COLOR_POWER_TIME:
					case ChaoAbility.ASTEROID_TIME:
					case ChaoAbility.DRILL_TIME:
					case ChaoAbility.LASER_TIME:
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateItemPhantom)));
						break;
					case ChaoAbility.BOSS_STAGE_TIME:
						ObjUtil.LightPlaySE("act_arthur", "SE");
						base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						break;
					case ChaoAbility.COMBO_RECEPTION_TIME:
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateGameHardware)));
						break;
					case ChaoAbility.BOSS_RED_RING_RATE:
					case ChaoAbility.BOSS_SUPER_RING_RATE:
						ObjUtil.LightPlaySE("act_ship_laser", "SE");
						break;
					case ChaoAbility.RECOVERY_RING:
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateStockRing)));
						break;
					case ChaoAbility.BOSS_ATTACK:
						if ((float)this.m_attackCount < instance.GetChaoAbilityValue(ability2))
						{
							msg.m_flag = true;
							this.ChangeState(new TinyFsmState(new EventFunction(this.StateAttackBoss)));
						}
						break;
					case ChaoAbility.CHECK_POINT_ITEM_BOX:
					{
						PlayerInformation playerInformation2 = GameObjectUtil.FindGameObjectComponentWithTag<PlayerInformation>("StageManager", "PlayerInformation");
						if (playerInformation2 != null)
						{
							bool flag = false;
							if (playerInformation2.PhantomType == PhantomType.NONE)
							{
								this.m_chaoItemType = ChaoState.ChaoAbilityItemTbl[UnityEngine.Random.Range(0, ChaoState.ChaoAbilityItemTbl.Length)];
								flag = this.CheckItemPresent(this.m_chaoItemType);
							}
							if (!flag)
							{
								this.m_chaoItemType = ChaoState.ChaoAbilityItemTblPhantom[UnityEngine.Random.Range(0, ChaoState.ChaoAbilityItemTblPhantom.Length)];
								flag = this.CheckItemPresent(this.m_chaoItemType);
							}
							if (flag)
							{
								if (this.m_stateFlag.Test(0))
								{
									this.m_stateFlag.Set(1, true);
								}
								this.ChangeState(new TinyFsmState(new EventFunction(this.StateItemPresent2)));
							}
						}
						break;
					}
					case ChaoAbility.JUMP_RAMP_TRICK_SUCCESS:
						base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_jumpboard_success_r01", -1f);
						break;
					case ChaoAbility.PURSUES_TO_BOSS_AFTER_ATTACK:
					{
						float num = UnityEngine.Random.Range(0f, 100f);
						if (num < instance.GetChaoAbilityValue(ability2))
						{
							msg.m_flag = true;
							this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursuesAttackBoss)));
						}
						break;
					}
					case ChaoAbility.SPECIAL_ANIMAL_PSO2:
						base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						ObjUtil.LightPlaySE("act_chao_rappy", "SE");
						break;
					case ChaoAbility.DAMAGE_TRAMPOLINE:
						if (ObjUtil.GetRandomRange100() < (int)instance.GetChaoAbilityValue(ability2))
						{
							this.m_chaoItemType = ItemType.TRAMPOLINE;
							PlayerInformation playerInformation3 = GameObjectUtil.FindGameObjectComponentWithTag<PlayerInformation>("StageManager", "PlayerInformation");
							if (playerInformation3 != null && playerInformation3.PhantomType == PhantomType.NONE)
							{
								ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_damaged_trampoline_r01", -1f);
								base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
								if (this.m_chao_id == 1018)
								{
									ObjUtil.LightPlaySE("act_chao_puyo", "SE");
								}
							}
							StageItemManager itemManager2 = this.GetItemManager();
							if (itemManager2 != null)
							{
								itemManager2.SendMessage("OnAddDamageTrampoline", null, SendMessageOptions.DontRequireReceiver);
							}
						}
						break;
					case ChaoAbility.RECOVERY_FROM_FAILURE:
						ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_sp4_deatheggstar_flash_sr01", -1f);
						ObjUtil.LightPlaySE("act_chao_deathegg", "SE");
						break;
					case ChaoAbility.ADD_COMBO_VALUE:
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateGameCartridge)));
						break;
					case ChaoAbility.LOOP_COMBO_UP:
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateNights)));
						break;
					case ChaoAbility.LOOP_MAGNET:
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateReala)));
						break;
					case ChaoAbility.DAMAGE_DESTROY_ALL:
						if (ObjUtil.GetRandomRange100() < (int)instance.GetChaoAbilityValue(ability2))
						{
							base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
							ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_clb_pian_magic_r01", -1f);
							ObjUtil.LightPlaySE("act_chao_pian", "SE");
							MsgObjectDead value2 = new MsgObjectDead();
							GameObjectUtil.SendDelayedMessageToTagObjects("Gimmick", "OnMsgObjectDead", value2);
							GameObjectUtil.SendDelayedMessageToTagObjects("Enemy", "OnMsgObjectDead", value2);
						}
						break;
					case ChaoAbility.ITEM_REVIVE:
						if (!this.m_stateFlag.Test(2))
						{
							if (this.m_stateFlag.Test(0))
							{
								this.m_stateFlag.Set(1, true);
							}
							this.ChangeState(new TinyFsmState(new EventFunction(this.StateReviveEquipItem)));
						}
						break;
					case ChaoAbility.TRANSFER_DOUBLE_RING:
						if (this.m_stateFlag.Test(0))
						{
							this.m_stateFlag.Set(1, true);
						}
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateDoubleRing)));
						break;
					case ChaoAbility.CHAO_RING_MAGNET:
						if (this.m_stateFlag.Test(0))
						{
							this.m_stateFlag.Set(1, true);
						}
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateRingMagnet)));
						break;
					case ChaoAbility.CHAO_CRYSTAL_MAGNET:
						if (this.m_stateFlag.Test(0))
						{
							this.m_stateFlag.Set(1, true);
						}
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateCrystalMagnet)));
						break;
					case ChaoAbility.MAGNET_SPEED_TYPE_JUMP:
						base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_typeactionS_magnet_r01", -1f);
						ObjUtil.LightPlaySE("act_chao_effect", "SE");
						break;
					case ChaoAbility.MAGNET_FLY_TYPE_JUMP:
						base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_typeactionF_magnet_r01", -1f);
						ObjUtil.LightPlaySE("act_chao_effect", "SE");
						break;
					case ChaoAbility.MAGNET_POWER_TYPE_JUMP:
						base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_typeactionP_magnet_r01", -1f);
						ObjUtil.LightPlaySE("act_chao_effect", "SE");
						break;
					case ChaoAbility.COMBO_DESTROY_AND_RECOVERY:
						base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_combo_brkall_magnetall_sr01", -1f);
						ObjUtil.LightPlaySE("act_chao_papaopa", "SE");
						ObjUtil.SendMessageOnObjectDead();
						GameObjectUtil.SendDelayedMessageToTagObjects("Gimmick", "OnMsgStepObjectDead", new MsgObjectDead());
						break;
					case ChaoAbility.COMBO_RING_BANK:
						base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						base.StartCoroutine(this.RingBank());
						break;
					case ChaoAbility.ENEMY_COUNT_BOMB:
						base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_enemy_brk_enemy_r01", -1f);
						ObjUtil.LightPlaySE("act_chao_heavybomb", "SE");
						if (StageAbilityManager.Instance != null)
						{
							float chaoAbilityExtraValue = StageAbilityManager.Instance.GetChaoAbilityExtraValue(ChaoAbility.ENEMY_COUNT_BOMB);
							GameObjectUtil.SendDelayedMessageToTagObjects("Enemy", "OnMsgHeavyBombDead", chaoAbilityExtraValue);
						}
						break;
					case ChaoAbility.ENEMY_SCORE_SEVERALFOLD:
						if (!this.IsPlayingAbilityAnimation())
						{
							base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
							ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_sp6_kingboombo_magic_sr01", -1f);
							ObjUtil.LightPlaySE("act_chao_effect", "SE");
						}
						break;
					case ChaoAbility.COMBO_METAL_AND_METAL_SCORE:
						base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_combo_enemy_m_r01", -1f);
						ObjUtil.LightPlaySE("act_chao_effect", "SE");
						break;
					case ChaoAbility.GUARD_DROP_RING:
						base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_damaged_keepring_r01", -1f);
						ObjUtil.LightPlaySE("act_chao_effect", "SE");
						break;
					case ChaoAbility.INVALIDI_EXTREME_STAGE:
						if (this.m_stateFlag.Test(0))
						{
							this.m_stateFlag.Set(1, true);
						}
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateInvalidExtreme)));
						break;
					case ChaoAbility.COMBO_EQUIP_ITEM_EXTRA:
						if (!this.IsNowLastChance())
						{
							if (this.m_stateFlag.Test(0))
							{
								this.m_stateFlag.Set(1, true);
							}
							this.ChangeState(new TinyFsmState(new EventFunction(this.StatePresentSRareEquipItem)));
						}
						break;
					case ChaoAbility.EASY_SPEED:
						if (this.m_stateFlag.Test(0))
						{
							this.m_stateFlag.Set(1, true);
						}
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateEasySpeed)));
						break;
					case ChaoAbility.COMBO_CHANGE_EQUIP_ITEM:
					{
						StageItemManager itemManager3 = this.GetItemManager();
						if (itemManager3 != null && itemManager3.IsEquipItem())
						{
							if (this.m_stateFlag.Test(0))
							{
								this.m_stateFlag.Set(1, true);
							}
							this.ChangeState(new TinyFsmState(new EventFunction(this.StateChangeEquipItem)));
						}
						break;
					}
					case ChaoAbility.COMBO_COLOR_POWER_DRILL:
					{
						StageItemManager itemManager4 = this.GetItemManager();
						if (itemManager4 != null)
						{
							this.m_chaoItemType = ItemType.DRILL;
							itemManager4.SendMessage("OnAddColorItem", new MsgAddItemToManager(this.m_chaoItemType), SendMessageOptions.DontRequireReceiver);
							ObjUtil.LightPlaySE("act_chao_effect", "SE");
							ObjUtil.SendGetItemIcon(this.m_chaoItemType);
							ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_combo_drill_r01", -1f);
							base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						}
						break;
					}
					case ChaoAbility.COMBO_BONUS_UP:
						if (this.m_stateFlag.Test(0))
						{
							this.m_stateFlag.Set(1, true);
						}
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateComboBonusUp)));
						break;
					case ChaoAbility.COMBO_COLOR_POWER_LASER:
					{
						StageItemManager itemManager5 = this.GetItemManager();
						if (itemManager5 != null)
						{
							this.m_chaoItemType = ItemType.LASER;
							itemManager5.SendMessage("OnAddColorItem", new MsgAddItemToManager(this.m_chaoItemType), SendMessageOptions.DontRequireReceiver);
							ObjUtil.LightPlaySE("act_chao_effect", "SE");
							ObjUtil.SendGetItemIcon(this.m_chaoItemType);
							ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_combo_laser_r01", -1f);
							base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						}
						break;
					}
					case ChaoAbility.COMBO_COLOR_POWER_ASTEROID:
					{
						StageItemManager itemManager6 = this.GetItemManager();
						if (itemManager6 != null)
						{
							this.m_chaoItemType = ItemType.ASTEROID;
							itemManager6.SendMessage("OnAddColorItem", new MsgAddItemToManager(this.m_chaoItemType), SendMessageOptions.DontRequireReceiver);
							ObjUtil.LightPlaySE("act_chao_effect", "SE");
							ObjUtil.SendGetItemIcon(this.m_chaoItemType);
							ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_combo_asteroid_r01", -1f);
							base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						}
						break;
					}
					case ChaoAbility.COMBO_RANDOM_ITEM_MINUS_RING:
						if (!this.IsNowLastChance() && StageAbilityManager.Instance != null && StageScoreManager.Instance != null)
						{
							long costRing = (long)StageAbilityManager.Instance.GetChaoAbilityExtraValue(ChaoAbility.COMBO_RANDOM_ITEM_MINUS_RING);
							if (StageScoreManager.Instance.DefrayItemCostByRing(costRing))
							{
								if (this.m_stateFlag.Test(0))
								{
									this.m_stateFlag.Set(1, true);
								}
								this.ChangeState(new TinyFsmState(new EventFunction(this.StateOrbotItemPresent)));
							}
							else
							{
								ObjUtil.LightPlaySE("sys_error", "SE");
								ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_combo_randombuyitem_fail_r01", -1f);
							}
						}
						break;
					case ChaoAbility.COMBO_RANDOM_ITEM:
						if (!this.IsNowLastChance())
						{
							float num2 = 0f;
							if (StageAbilityManager.Instance != null)
							{
								num2 = StageAbilityManager.Instance.GetChaoAbilityExtraValue(ChaoAbility.COMBO_RANDOM_ITEM);
							}
							if (UnityEngine.Random.Range(0f, 100f) < num2)
							{
								if (this.m_stateFlag.Test(0))
								{
									this.m_stateFlag.Set(1, true);
								}
								this.ChangeState(new TinyFsmState(new EventFunction(this.StateCuebotItemPresent)));
							}
							else
							{
								if (this.m_stateFlag.Test(0))
								{
									this.m_stateFlag.Set(1, true);
								}
								this.ChangeState(new TinyFsmState(new EventFunction(this.StateFailCuebotItemPresent)));
							}
						}
						break;
					case ChaoAbility.JUMP_DESTROY_ENEMY_AND_TRAP:
					{
						MsgObjectDead value3 = new MsgObjectDead();
						GameObjectUtil.SendDelayedMessageToTagObjects("Gimmick", "OnMsgObjectDead", value3);
						GameObjectUtil.SendDelayedMessageToTagObjects("Enemy", "OnMsgObjectDead", value3);
						ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_raid2_ganmen_atk_sr01", -1f);
						ObjUtil.LightPlaySE("act_chao_effect", "SE");
						base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						break;
					}
					case ChaoAbility.JUMP_DESTROY_ENEMY:
						GameObjectUtil.SendDelayedMessageToTagObjects("Enemy", "OnMsgObjectDead", new MsgObjectDead());
						ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_nodamage_movetrap_r01", -1f);
						ObjUtil.LightPlaySE("act_chao_effect", "SE");
						base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						break;
					case ChaoAbility.SUPER_RING_UP:
						ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_up_ring10_r01", -1f);
						ObjUtil.LightPlaySE("act_chao_effect", "SE");
						base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						break;
					}
					return;
				}
			}
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00035960 File Offset: 0x00033B60
		private bool CheckItemPresent(ItemType itemType)
		{
			if (this.IsNowLastChance())
			{
				return false;
			}
			StageItemManager itemManager = this.GetItemManager();
			if (itemManager != null)
			{
				MsgAskEquipItemUsed msgAskEquipItemUsed = new MsgAskEquipItemUsed(itemType);
				itemManager.SendMessage("OnAskEquipItemUsed", msgAskEquipItemUsed);
				if (msgAskEquipItemUsed.m_ok)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x000359B0 File Offset: 0x00033BB0
		private bool IsNowLastChance()
		{
			PlayerInformation playerInformation = GameObjectUtil.FindGameObjectComponentWithTag<PlayerInformation>("StageManager", "PlayerInformation");
			return playerInformation != null && playerInformation.IsNowLastChance();
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x000359E4 File Offset: 0x00033BE4
		private void OnMsgChaoAbilityEnd(MsgChaoAbilityEnd msg)
		{
			StageAbilityManager instance = StageAbilityManager.Instance;
			foreach (ChaoAbility ability2 in msg.m_ability)
			{
				if (instance != null && instance.HasChaoAbility(ability2, this.m_chao_type) && instance != null && instance.HasChaoAbility(ability2, this.m_chao_type))
				{
					TinyFsmEvent signal = TinyFsmEvent.CreateMessage(msg);
					this.m_fsmBehavior.Dispatch(signal);
					return;
				}
			}
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00035A68 File Offset: 0x00033C68
		private void OnTriggerEnter(Collider other)
		{
			TinyFsmEvent signal = TinyFsmEvent.CreateUserEventObject(100, base.collider);
			this.m_fsmBehavior.Dispatch(signal);
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00035A90 File Offset: 0x00033C90
		private void OnTriggerStay(Collider other)
		{
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00035A94 File Offset: 0x00033C94
		private void OnTriggerExit(Collider other)
		{
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00035A98 File Offset: 0x00033C98
		private void OnDamageSucceed(MsgHitDamageSucceed msg)
		{
			if (this.m_fsmBehavior != null)
			{
				TinyFsmEvent signal = TinyFsmEvent.CreateMessage(msg);
				this.m_fsmBehavior.Dispatch(signal);
			}
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00035ACC File Offset: 0x00033CCC
		private T CreateStateWork<T>() where T : ChaoState.StateWork, new()
		{
			if (this.m_stateWork != null)
			{
				this.DestroyStateWork();
			}
			T t = Activator.CreateInstance<T>();
			t.Name = t.ToString();
			this.m_stateWork = t;
			return t;
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00035B18 File Offset: 0x00033D18
		private void DestroyStateWork()
		{
			if (this.m_stateWork != null)
			{
				this.m_stateWork.Destroy();
				this.m_stateWork = null;
			}
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00035B38 File Offset: 0x00033D38
		private T GetStateWork<T>() where T : ChaoState.StateWork
		{
			if (this.m_stateWork != null && this.m_stateWork is T)
			{
				return this.m_stateWork as T;
			}
			return (T)((object)null);
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00035B78 File Offset: 0x00033D78
		private TinyFsmState StateIdle(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				return TinyFsmState.End();
			case 1:
				return TinyFsmState.End();
			case 4:
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateComeIn)));
				return TinyFsmState.End();
			case 5:
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00035BE8 File Offset: 0x00033DE8
		private TinyFsmState StateInit(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				return TinyFsmState.End();
			case 1:
				return TinyFsmState.End();
			case 4:
				this.ChangeState(new TinyFsmState(new EventFunction(this.StateComeIn)));
				return TinyFsmState.End();
			case 5:
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00035C58 File Offset: 0x00033E58
		private TinyFsmState StateComeIn(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				return TinyFsmState.End();
			case 1:
				this.m_stateFlag.Set(0, false);
				this.m_stateFlag.Set(1, false);
				this.ChangeMovement(MOVESTATE_ID.ComeIn);
				return TinyFsmState.End();
			case 4:
				if (this.m_movement != null && this.m_movement.NextState)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
				}
				return TinyFsmState.End();
			case 5:
			{
				MsgChaoState msgChaoState = fsmEvent.GetMessage as MsgChaoState;
				if (msgChaoState != null)
				{
					MsgChaoState.State state = msgChaoState.state;
					if (state == MsgChaoState.State.STOP)
					{
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateStop)));
					}
				}
				return TinyFsmState.End();
			}
			}
			return TinyFsmState.End();
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00035D4C File Offset: 0x00033F4C
		private TinyFsmState StatePursue(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				return TinyFsmState.End();
			case 1:
				this.m_stateFlag.Set(0, false);
				this.m_stateFlag.Set(1, false);
				this.RequestStartEffect();
				this.ChangeMovement(MOVESTATE_ID.Pursue);
				return TinyFsmState.End();
			case 4:
				return TinyFsmState.End();
			case 5:
			{
				MsgChaoState msgChaoState = fsmEvent.GetMessage as MsgChaoState;
				if (msgChaoState != null)
				{
					switch (msgChaoState.state)
					{
					case MsgChaoState.State.STOP:
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateStop)));
						break;
					case MsgChaoState.State.LAST_CHANCE:
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateLastChance)));
						break;
					}
				}
				return TinyFsmState.End();
			}
			}
			return TinyFsmState.End();
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00035E38 File Offset: 0x00034038
		private TinyFsmState StateLastChance(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
			{
				StageAbilityManager instance = StageAbilityManager.Instance;
				if (instance != null)
				{
					instance.RequestStopChaoEffect(ChaoAbility.LAST_CHANCE);
				}
				return TinyFsmState.End();
			}
			case 1:
			{
				this.ChangeMovement(MOVESTATE_ID.LastChance);
				StageAbilityManager instance2 = StageAbilityManager.Instance;
				if (instance2 != null)
				{
					instance2.RequestPlayChaoEffect(ChaoAbility.LAST_CHANCE, this.m_chao_type);
				}
				ObjUtil.LightPlaySE("act_chip_fly", "SE");
				return TinyFsmState.End();
			}
			case 4:
				return TinyFsmState.End();
			case 5:
			{
				MsgChaoState msgChaoState = fsmEvent.GetMessage as MsgChaoState;
				if (msgChaoState != null && msgChaoState.state == MsgChaoState.State.LAST_CHANCE_END)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateLastChanceEnd)));
				}
				return TinyFsmState.End();
			}
			}
			return TinyFsmState.End();
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00035F14 File Offset: 0x00034114
		private TinyFsmState StateLastChanceEnd(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				this.SetAnimationFlagForAbility(false);
				return TinyFsmState.End();
			case 1:
				this.SetAnimationFlagForAbility(true);
				ObjUtil.LightPlaySE("act_chip_pose", "SE");
				this.ChangeMovement(MOVESTATE_ID.LastChanceEnd);
				return TinyFsmState.End();
			case 4:
				return TinyFsmState.End();
			case 5:
			{
				MsgChaoState msgChaoState = fsmEvent.GetMessage as MsgChaoState;
				if (msgChaoState != null)
				{
					if (msgChaoState.state == MsgChaoState.State.COME_IN)
					{
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateComeIn)));
					}
					else if (msgChaoState.state == MsgChaoState.State.STOP_END)
					{
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateStopEnd)));
					}
					else if (msgChaoState.state == MsgChaoState.State.STOP)
					{
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateStop)));
					}
				}
				return TinyFsmState.End();
			}
			}
			return TinyFsmState.End();
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x00036014 File Offset: 0x00034214
		private TinyFsmState StateStop(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				this.m_stateFlag.Set(0, false);
				return TinyFsmState.End();
			case 1:
				this.m_stateFlag.Set(0, true);
				this.m_stateFlag.Set(1, false);
				this.ChangeMovement(MOVESTATE_ID.Stop);
				return TinyFsmState.End();
			case 4:
				return TinyFsmState.End();
			case 5:
			{
				MsgChaoState msgChaoState = fsmEvent.GetMessage as MsgChaoState;
				if (msgChaoState != null)
				{
					switch (msgChaoState.state)
					{
					case MsgChaoState.State.COME_IN:
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateComeIn)));
						break;
					case MsgChaoState.State.STOP_END:
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateStopEnd)));
						break;
					case MsgChaoState.State.LAST_CHANCE:
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateLastChance)));
						break;
					}
				}
				return TinyFsmState.End();
			}
			}
			return TinyFsmState.End();
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00036124 File Offset: 0x00034324
		private TinyFsmState StateStopEnd(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				return TinyFsmState.End();
			case 1:
				this.ChangeMovement(MOVESTATE_ID.StopEnd);
				return TinyFsmState.End();
			case 4:
				if (this.m_movement != null && this.m_movement.NextState)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
				}
				return TinyFsmState.End();
			case 5:
			{
				MsgChaoState msgChaoState = fsmEvent.GetMessage as MsgChaoState;
				if (msgChaoState != null)
				{
					MsgChaoState.State state = msgChaoState.state;
					if (state != MsgChaoState.State.COME_IN)
					{
						if (state == MsgChaoState.State.STOP)
						{
							this.ChangeState(new TinyFsmState(new EventFunction(this.StateStop)));
						}
					}
					else
					{
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateComeIn)));
					}
				}
				return TinyFsmState.End();
			}
			}
			return TinyFsmState.End();
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0003621C File Offset: 0x0003441C
		private TinyFsmState StateInvalidExtreme(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				this.SetAnimationFlagForAbility(false);
				return TinyFsmState.End();
			case 1:
			{
				this.ChangeMovement(MOVESTATE_ID.GoCameraTarget2);
				ChaoMoveGoCameraTargetUsePlayerSpeed currentMovement = this.GetCurrentMovement<ChaoMoveGoCameraTargetUsePlayerSpeed>();
				if (currentMovement != null)
				{
					currentMovement.SetParameter(ChaoState.InvalidExtremeOffsetRate, 1.5f);
				}
				this.m_substate = 0;
				this.m_stateTimer = 0.8f;
				return TinyFsmState.End();
			}
			case 4:
			{
				float getDeltaTime = fsmEvent.GetDeltaTime;
				ChaoState.SubStateItemPresent substate = (ChaoState.SubStateItemPresent)this.m_substate;
				if (substate != ChaoState.SubStateItemPresent.Action)
				{
					if (substate == ChaoState.SubStateItemPresent.Wait)
					{
						this.m_stateTimer -= getDeltaTime;
						if (this.m_stateTimer < 0f)
						{
							if (this.m_stateFlag.Test(1))
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StateOutItemPhantom)));
							}
							else
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
							}
						}
					}
				}
				else
				{
					this.m_stateTimer -= getDeltaTime;
					if (this.m_stateTimer < 0f)
					{
						ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_magic_darkqueen_sr01", -1f);
						ObjUtil.LightPlaySE("act_sharla_magic", "SE");
						base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						this.m_stateTimer = 1.2f;
						this.m_substate = 1;
					}
				}
				return TinyFsmState.End();
			}
			case 5:
				this.SetMessageComeInOut(fsmEvent);
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x000363A8 File Offset: 0x000345A8
		private TinyFsmState StateComboBonusUp(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				this.SetAnimationFlagForAbility(false);
				return TinyFsmState.End();
			case 1:
			{
				this.ChangeMovement(MOVESTATE_ID.GoCameraTarget);
				ChaoMoveGoCameraTarget currentMovement = this.GetCurrentMovement<ChaoMoveGoCameraTarget>();
				if (currentMovement != null)
				{
					currentMovement.SetParameter(ChaoState.ItemPresentOffsetRate, 2f);
				}
				this.m_substate = 0;
				this.m_stateTimer = 1f;
				return TinyFsmState.End();
			}
			case 4:
			{
				ChaoState.SubStateItemPresent substate = (ChaoState.SubStateItemPresent)this.m_substate;
				if (substate != ChaoState.SubStateItemPresent.Action)
				{
					if (substate == ChaoState.SubStateItemPresent.Wait)
					{
						this.m_stateTimer -= fsmEvent.GetDeltaTime;
						if (this.m_stateTimer < 0f)
						{
							if (this.m_stateFlag.Test(1))
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StateOutItemPhantom)));
							}
							else
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
							}
						}
					}
				}
				else
				{
					this.m_stateTimer -= fsmEvent.GetDeltaTime;
					if (this.m_stateTimer < 0f)
					{
						ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_bonus_comboscore_sr01", -1f);
						ObjUtil.LightPlaySE("act_chao_effect", "SE");
						base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						this.m_stateTimer = 1f;
						this.m_substate = 1;
					}
				}
				return TinyFsmState.End();
			}
			case 5:
				this.SetMessageComeInOut(fsmEvent);
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00036538 File Offset: 0x00034738
		private TinyFsmState StateEasySpeed(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				this.SetAnimationFlagForAbility(false);
				return TinyFsmState.End();
			case 1:
			{
				this.ChangeMovement(MOVESTATE_ID.GoCameraTarget2);
				ChaoMoveGoCameraTargetUsePlayerSpeed currentMovement = this.GetCurrentMovement<ChaoMoveGoCameraTargetUsePlayerSpeed>();
				if (currentMovement != null)
				{
					currentMovement.SetParameter(ChaoState.InvalidExtremeOffsetRate, 3f);
				}
				this.m_substate = 0;
				this.m_stateTimer = 0.8f;
				return TinyFsmState.End();
			}
			case 4:
			{
				ChaoState.SubStateItemPresent substate = (ChaoState.SubStateItemPresent)this.m_substate;
				if (substate != ChaoState.SubStateItemPresent.Action)
				{
					if (substate == ChaoState.SubStateItemPresent.Wait)
					{
						this.m_stateTimer -= fsmEvent.GetDeltaTime;
						if (this.m_stateTimer < 0f)
						{
							if (this.m_stateFlag.Test(1))
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StateOutItemPhantom)));
							}
							else
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
							}
						}
					}
				}
				else
				{
					this.m_stateTimer -= fsmEvent.GetDeltaTime;
					if (this.m_stateTimer < 0f)
					{
						ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_clb_nights_magic_sr02", -1f);
						ObjUtil.LightPlaySE("act_chao_effect", "SE");
						base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						this.m_stateTimer = 1.2f;
						this.m_substate = 1;
					}
				}
				return TinyFsmState.End();
			}
			case 5:
				this.SetMessageComeInOut(fsmEvent);
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x000366C8 File Offset: 0x000348C8
		private TinyFsmState StateOutItemPhantom(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				return TinyFsmState.End();
			case 1:
			{
				this.ChangeMovement(MOVESTATE_ID.OutCameraTarget);
				ChaoMoveOutCameraTarget currentMovement = this.GetCurrentMovement<ChaoMoveOutCameraTarget>();
				if (currentMovement != null)
				{
					currentMovement.SetParameter(ChaoState.ItemPresentOffsetRate, 2f);
				}
				return TinyFsmState.End();
			}
			case 4:
				if (this.m_movement != null && this.m_movement.NextState)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateStop)));
				}
				else if (!this.m_stateFlag.Test(1))
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StateStopEnd)));
				}
				return TinyFsmState.End();
			case 5:
				this.SetMessageComeInOut(fsmEvent);
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x000367B0 File Offset: 0x000349B0
		private TinyFsmState StateItemPhantom(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				this.SetAnimationFlagForAbility(false);
				return TinyFsmState.End();
			case 1:
			{
				this.ChangeMovement(MOVESTATE_ID.GoCameraTarget);
				ChaoMoveGoCameraTarget currentMovement = this.GetCurrentMovement<ChaoMoveGoCameraTarget>();
				if (currentMovement != null)
				{
					Vector3 screenOffsetRate = (this.m_chao_type != ChaoType.SUB) ? new Vector3(0.5f, 0.5f, 0f) : new Vector3(0.5f, 0.7f, 0f);
					currentMovement.SetParameter(screenOffsetRate, 5f);
				}
				this.SetAnimationFlagForAbility(true);
				this.m_stateTimer = 1f;
				return TinyFsmState.End();
			}
			case 4:
			{
				float getDeltaTime = fsmEvent.GetDeltaTime;
				this.m_stateTimer -= getDeltaTime;
				if (this.m_stateTimer < 0f)
				{
					this.ChangeMovement(MOVESTATE_ID.Stay);
				}
				return TinyFsmState.End();
			}
			case 5:
				switch (fsmEvent.GetMessage.ID)
				{
				case 21760:
				{
					MsgChaoState msgChaoState = fsmEvent.GetMessage as MsgChaoState;
					if (msgChaoState != null && msgChaoState.state == MsgChaoState.State.COME_IN)
					{
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateComeIn)));
						return TinyFsmState.End();
					}
					break;
				}
				case 21762:
					this.ChangeMovement(MOVESTATE_ID.Stay);
					return TinyFsmState.End();
				}
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00036924 File Offset: 0x00034B24
		private TinyFsmState StateKillOut(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				if (StageAbilityManager.Instance != null)
				{
					StageAbilityManager.Instance.RequestStopChaoEffect(ChaoAbility.COMBO_WIPE_OUT_ENEMY);
				}
				if (StageComboManager.Instance != null)
				{
					StageComboManager.Instance.SetChaoFlagStatus(StageComboManager.ChaoFlagStatus.ENEMY_DEAD, false);
				}
				return TinyFsmState.End();
			case 1:
				this.ChangeMovement(MOVESTATE_ID.GoKillOut);
				this.m_stateTimer = 1f;
				this.m_stateFlag.Reset();
				this.m_substate = 0;
				if (StageAbilityManager.Instance != null)
				{
					StageAbilityManager.Instance.RequestPlayChaoEffect(ChaoAbility.COMBO_WIPE_OUT_ENEMY, this.m_chao_type);
				}
				return TinyFsmState.End();
			case 4:
			{
				float getDeltaTime = fsmEvent.GetDeltaTime;
				this.m_stateTimer -= getDeltaTime;
				switch (this.m_substate)
				{
				case 0:
					if (this.m_stateTimer < 0f)
					{
						this.m_stateTimer = 1f;
						ChaoMoveGoKillOut currentMovement = this.GetCurrentMovement<ChaoMoveGoKillOut>();
						if (currentMovement != null)
						{
							currentMovement.ChangeMode(ChaoMoveGoKillOut.Mode.Forward);
						}
						this.m_substate = 1;
					}
					break;
				case 1:
					if (this.m_stateTimer < 0f)
					{
						GameObjectUtil.SendDelayedMessageToTagObjects("Enemy", "OnMsgObjectDead", new MsgObjectDead());
						if (StageComboManager.Instance != null)
						{
							StageComboManager.Instance.SetChaoFlagStatus(StageComboManager.ChaoFlagStatus.ENEMY_DEAD, true);
						}
						ObjUtil.LightPlaySE("act_exp", "SE");
						Camera camera = GameObjectUtil.FindGameObjectComponentWithTag<Camera>("MainCamera", "GameMainCamera");
						if (camera != null)
						{
							Vector3 zero = Vector3.zero;
							zero.z = base.transform.position.z - camera.transform.position.z;
							ObjUtil.PlayChaoEffect(camera.gameObject, "ef_ch_bomber_atk_r01", zero, -1f);
						}
						this.m_substate = 2;
						this.m_stateTimer = StageComboManager.CHAO_OBJ_DEAD_TIME;
					}
					break;
				case 2:
					if (!this.m_stateFlag.Test(1) && this.m_stateTimer < 0f)
					{
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateComeIn)));
					}
					break;
				}
				return TinyFsmState.End();
			}
			case 5:
				this.SetMessageComeInOut(fsmEvent);
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x00036B80 File Offset: 0x00034D80
		private void SetAnimationFlagForAbility(bool value)
		{
			if (this.m_fsmBehavior.NowShutDown)
			{
				return;
			}
			if (this.m_modelObject != null)
			{
				Animator component = this.m_modelObject.GetComponent<Animator>();
				if (component != null && component.enabled)
				{
					component.SetBool("Ability", value);
				}
			}
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x00036BE0 File Offset: 0x00034DE0
		private IEnumerator SetAnimationFlagForAbilityCourutine(bool value)
		{
			if (this.m_fsmBehavior.NowShutDown)
			{
				yield break;
			}
			if (this.m_modelObject != null)
			{
				Animator animator = this.m_modelObject.GetComponent<Animator>();
				if (animator != null)
				{
					animator.SetBool("Ability", value);
					if (value)
					{
						int wait = 2;
						while (wait > 0)
						{
							wait--;
							yield return null;
						}
						if (animator != null && animator.enabled)
						{
							animator.SetBool("Ability", false);
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x00036C0C File Offset: 0x00034E0C
		private bool IsPlayingAbilityAnimation()
		{
			if (this.m_modelObject != null)
			{
				Animator component = this.m_modelObject.GetComponent<Animator>();
				if (component != null)
				{
					return component.GetBool("Ability");
				}
			}
			return false;
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x00036C50 File Offset: 0x00034E50
		private void SetMessageComeInOut(TinyFsmEvent fsmEvent)
		{
			MsgChaoState msgChaoState = fsmEvent.GetMessage as MsgChaoState;
			if (msgChaoState != null)
			{
				switch (msgChaoState.state)
				{
				case MsgChaoState.State.COME_IN:
					this.m_stateFlag.Set(1, false);
					break;
				case MsgChaoState.State.STOP:
					this.m_stateFlag.Set(1, true);
					break;
				case MsgChaoState.State.STOP_END:
					this.m_stateFlag.Set(1, false);
					break;
				}
			}
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00036CC8 File Offset: 0x00034EC8
		private TinyFsmState StateAttackBoss(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				this.DestroyStateWork();
				this.SetSpinModelControl(false);
				return TinyFsmState.End();
			case 1:
				this.ChangeMovement(MOVESTATE_ID.AttackBoss);
				this.CreateAttackBossWork();
				this.m_substate = 0;
				this.m_stateTimer = 0.8f;
				this.SetSpinModelControl(true);
				ObjUtil.LightPlaySE("act_sword_fly", "SE");
				return TinyFsmState.End();
			case 4:
			{
				ChaoState.AttackBossWork stateWork = this.GetStateWork<ChaoState.AttackBossWork>();
				if (stateWork == null)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
					return TinyFsmState.End();
				}
				if (stateWork.m_targetObject == null)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
					return TinyFsmState.End();
				}
				float getDeltaTime = fsmEvent.GetDeltaTime;
				if (stateWork != null)
				{
					this.UpdateAttackBoss(fsmEvent, getDeltaTime, stateWork);
				}
				return TinyFsmState.End();
			}
			case 5:
			{
				int id = fsmEvent.GetMessage.ID;
				if (id != 12323)
				{
					if (id == 16385)
					{
						ChaoState.AttackBossWork stateWork2 = this.GetStateWork<ChaoState.AttackBossWork>();
						this.AttackBossCheckAndChanegStateOnDamageBossSucceed(fsmEvent, stateWork2);
						return TinyFsmState.End();
					}
					if (id == 21762)
					{
						ChaoState.AttackBossWork stateWork3 = this.GetStateWork<ChaoState.AttackBossWork>();
						if (stateWork3 != null && this.m_substate < 2)
						{
							this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
							return TinyFsmState.End();
						}
					}
				}
				else
				{
					MsgBossCheckState msgBossCheckState = fsmEvent.GetMessage as MsgBossCheckState;
					if (msgBossCheckState != null && !msgBossCheckState.IsAttackOK())
					{
						ChaoState.AttackBossWork stateWork4 = this.GetStateWork<ChaoState.AttackBossWork>();
						if (stateWork4 != null && this.m_substate < 2)
						{
							this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
							return TinyFsmState.End();
						}
					}
				}
				return TinyFsmState.End();
			}
			}
			return TinyFsmState.End();
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00036EBC File Offset: 0x000350BC
		private TinyFsmState StatePursuesAttackBoss(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				this.DestroyStateWork();
				this.SetSpinModelControl(false);
				return TinyFsmState.End();
			case 1:
				this.ChangeMovement(MOVESTATE_ID.AttackBoss);
				this.CreateAttackBossWork();
				this.m_substate = 0;
				this.m_stateTimer = 0.8f;
				this.SetSpinModelControl(true);
				ObjUtil.LightPlaySE("act_sword_fly", "SE");
				return TinyFsmState.End();
			case 4:
			{
				ChaoState.AttackBossWork stateWork = this.GetStateWork<ChaoState.AttackBossWork>();
				if (stateWork == null)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
					return TinyFsmState.End();
				}
				if (stateWork.m_targetObject == null)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
					return TinyFsmState.End();
				}
				float getDeltaTime = fsmEvent.GetDeltaTime;
				if (stateWork != null)
				{
					this.UpdatePursuesAttackBoss(fsmEvent, getDeltaTime, stateWork);
				}
				return TinyFsmState.End();
			}
			case 5:
			{
				int id = fsmEvent.GetMessage.ID;
				if (id != 12323)
				{
					if (id == 16385)
					{
						ChaoState.AttackBossWork stateWork2 = this.GetStateWork<ChaoState.AttackBossWork>();
						this.PursuesAttackBossCheckAndChanegStateOnDamageBossSucceed(fsmEvent, stateWork2);
						return TinyFsmState.End();
					}
					if (id == 21762)
					{
						ChaoState.AttackBossWork stateWork3 = this.GetStateWork<ChaoState.AttackBossWork>();
						if (stateWork3 != null && this.m_substate < 2)
						{
							this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
							return TinyFsmState.End();
						}
					}
				}
				else
				{
					MsgBossCheckState msgBossCheckState = fsmEvent.GetMessage as MsgBossCheckState;
					if (msgBossCheckState != null && !msgBossCheckState.IsAttackOK())
					{
						ChaoState.AttackBossWork stateWork4 = this.GetStateWork<ChaoState.AttackBossWork>();
						if (stateWork4 != null && this.m_substate < 2)
						{
							this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
							return TinyFsmState.End();
						}
					}
				}
				return TinyFsmState.End();
			}
			}
			return TinyFsmState.End();
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x000370B0 File Offset: 0x000352B0
		private TinyFsmState UpdateAttackBoss(TinyFsmEvent fsmEvent, float deltaTime, ChaoState.AttackBossWork work)
		{
			switch (this.m_substate)
			{
			case 0:
				this.m_stateTimer -= deltaTime;
				if (this.m_stateTimer <= 0f)
				{
					this.m_stateTimer = 2f;
					this.SetChaoMoveAttackBoss(ChaoMoveAttackBoss.Mode.Homing);
					this.SetSpinModelControl(false);
					this.m_substate = 1;
				}
				break;
			case 1:
				this.m_stateTimer -= deltaTime;
				if (this.m_stateTimer <= 0f)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
					return TinyFsmState.End();
				}
				break;
			case 2:
				this.m_stateTimer -= deltaTime;
				if (this.m_stateTimer <= 0f)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
					return TinyFsmState.End();
				}
				break;
			}
			return TinyFsmState.End();
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x000371A4 File Offset: 0x000353A4
		private TinyFsmState UpdatePursuesAttackBoss(TinyFsmEvent fsmEvent, float deltaTime, ChaoState.AttackBossWork work)
		{
			switch (this.m_substate)
			{
			case 0:
				this.m_stateTimer -= deltaTime;
				if (this.m_stateTimer <= 0f)
				{
					this.m_stateTimer = 2f;
					this.SetChaoMoveAttackBoss(ChaoMoveAttackBoss.Mode.Homing);
					this.SetSpinModelControl(false);
					this.SetAnimationFlagForAbility(true);
					this.m_substate = 1;
				}
				break;
			case 1:
				this.m_stateTimer -= deltaTime;
				if (this.m_stateTimer <= 0f)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
					return TinyFsmState.End();
				}
				break;
			case 2:
				this.m_stateTimer -= deltaTime;
				if (this.m_stateTimer <= 0f)
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
					return TinyFsmState.End();
				}
				break;
			}
			return TinyFsmState.End();
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x000372A0 File Offset: 0x000354A0
		private bool AttackBossCheckAndChanegStateOnDamageBossSucceed(TinyFsmEvent fsmEvent, ChaoState.AttackBossWork work)
		{
			MsgHitDamageSucceed msgHitDamageSucceed = fsmEvent.GetMessage as MsgHitDamageSucceed;
			if (msgHitDamageSucceed != null && work != null && msgHitDamageSucceed.m_sender == work.m_targetObject)
			{
				if (work != null && this.m_substate < 2)
				{
					ObjUtil.PlayChaoEffect("ef_ch_slash_sr01", msgHitDamageSucceed.m_position, 3f);
					ObjUtil.LightPlaySE("act_sword_attack", "SE");
					this.m_stateTimer = 0.5f;
					this.m_substate = 2;
					this.SetChaoMoveAttackBoss(ChaoMoveAttackBoss.Mode.AfterAttack);
					work.DestroyAttackCollision();
					this.SetSpinModelControl(true);
					this.m_attackCount++;
				}
				else
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00037368 File Offset: 0x00035568
		private bool PursuesAttackBossCheckAndChanegStateOnDamageBossSucceed(TinyFsmEvent fsmEvent, ChaoState.AttackBossWork work)
		{
			MsgHitDamageSucceed msgHitDamageSucceed = fsmEvent.GetMessage as MsgHitDamageSucceed;
			if (msgHitDamageSucceed != null && work != null && msgHitDamageSucceed.m_sender == work.m_targetObject)
			{
				if (work != null && this.m_substate < 2)
				{
					ObjUtil.PlayChaoEffect("ef_ch_raid1_moon_atk_sr01", msgHitDamageSucceed.m_position, 3f);
					ObjUtil.LightPlaySE("act_sword_attack", "SE");
					this.m_stateTimer = 0.5f;
					this.m_substate = 2;
					this.SetAnimationFlagForAbility(false);
					this.SetChaoMoveAttackBoss(ChaoMoveAttackBoss.Mode.AfterAttack);
					work.DestroyAttackCollision();
					this.SetSpinModelControl(true);
				}
				else
				{
					this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00037428 File Offset: 0x00035628
		private void CreateAttackBossWork()
		{
			MsgBossInfo msgBossInfo = new MsgBossInfo();
			GameObjectUtil.SendMessageToTagObjects("Boss", "OnMsgBossInfo", msgBossInfo, SendMessageOptions.DontRequireReceiver);
			ChaoState.AttackBossWork attackBossWork = this.CreateStateWork<ChaoState.AttackBossWork>();
			if (msgBossInfo.m_succeed)
			{
				ChaoMoveAttackBoss currentMovement = this.GetCurrentMovement<ChaoMoveAttackBoss>();
				attackBossWork.m_targetObject = msgBossInfo.m_boss;
				if (currentMovement != null)
				{
					currentMovement.SetTarget(msgBossInfo.m_boss);
				}
			}
			attackBossWork.m_attackCollision = ChaoPartsAttackEnemy.Create(base.gameObject);
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x00037494 File Offset: 0x00035694
		private void SetChaoMoveAttackBoss(ChaoMoveAttackBoss.Mode mode)
		{
			ChaoMoveAttackBoss currentMovement = this.GetCurrentMovement<ChaoMoveAttackBoss>();
			if (currentMovement != null)
			{
				currentMovement.ChangeMode(mode);
			}
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x000374B8 File Offset: 0x000356B8
		private void SetSpinModelControl(bool flag)
		{
			if (this.m_modelControl != null)
			{
				if (flag)
				{
					this.m_modelControl.ChangeStateToSpin(ChaoState.AttackBossModelSpinSpeed);
				}
				else
				{
					this.m_modelControl.ChangeStateToReturnIdle();
				}
			}
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x000374F4 File Offset: 0x000356F4
		private TinyFsmState StateRingMagnet(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				return TinyFsmState.End();
			case 1:
			{
				this.ChangeMovement(MOVESTATE_ID.GoCameraTarget);
				ChaoMoveGoCameraTarget currentMovement = this.GetCurrentMovement<ChaoMoveGoCameraTarget>();
				if (currentMovement != null)
				{
					currentMovement.SetParameter(ChaoState.ChaoWalkerOffsetRate, 2f);
				}
				this.m_chaoWalkerState = ChaoState.ChaoWalkerState.Action;
				this.m_stateTimer = 0.6f;
				return TinyFsmState.End();
			}
			case 4:
			{
				ChaoState.ChaoWalkerState chaoWalkerState = this.m_chaoWalkerState;
				if (chaoWalkerState != ChaoState.ChaoWalkerState.Action)
				{
					if (chaoWalkerState == ChaoState.ChaoWalkerState.Wait)
					{
						this.m_stateTimer -= fsmEvent.GetDeltaTime;
						if (this.m_stateTimer < 0f)
						{
							if (this.m_stateFlag.Test(1))
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StateOutItemPhantom)));
							}
							else
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
							}
						}
					}
				}
				else
				{
					this.m_stateTimer -= fsmEvent.GetDeltaTime;
					if (this.m_stateTimer < 0f)
					{
						ObjUtil.LightPlaySE("act_chao_effect", "SE");
						this.SetEnableMagnetComponet(ChaoAbility.CHAO_RING_MAGNET);
						this.m_stateTimer = 1f;
						this.m_chaoWalkerState = ChaoState.ChaoWalkerState.Wait;
					}
				}
				return TinyFsmState.End();
			}
			case 5:
				this.SetMessageComeInOut(fsmEvent);
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00037660 File Offset: 0x00035860
		private TinyFsmState StateCrystalMagnet(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				return TinyFsmState.End();
			case 1:
			{
				this.ChangeMovement(MOVESTATE_ID.GoCameraTarget);
				ChaoMoveGoCameraTarget currentMovement = this.GetCurrentMovement<ChaoMoveGoCameraTarget>();
				if (currentMovement != null)
				{
					currentMovement.SetParameter(ChaoState.ChaoWalkerOffsetRate, 2f);
				}
				this.m_chaoWalkerState = ChaoState.ChaoWalkerState.Action;
				this.m_stateTimer = 0.6f;
				return TinyFsmState.End();
			}
			case 4:
			{
				ChaoState.ChaoWalkerState chaoWalkerState = this.m_chaoWalkerState;
				if (chaoWalkerState != ChaoState.ChaoWalkerState.Action)
				{
					if (chaoWalkerState == ChaoState.ChaoWalkerState.Wait)
					{
						this.m_stateTimer -= fsmEvent.GetDeltaTime;
						if (this.m_stateTimer < 0f)
						{
							if (this.m_stateFlag.Test(1))
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StateOutItemPhantom)));
							}
							else
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
							}
						}
					}
				}
				else
				{
					this.m_stateTimer -= fsmEvent.GetDeltaTime;
					if (this.m_stateTimer < 0f)
					{
						ObjUtil.LightPlaySE("act_chao_effect", "SE");
						this.SetEnableMagnetComponet(ChaoAbility.CHAO_CRYSTAL_MAGNET);
						this.m_stateTimer = 1f;
						this.m_chaoWalkerState = ChaoState.ChaoWalkerState.Wait;
					}
				}
				return TinyFsmState.End();
			}
			case 5:
				this.SetMessageComeInOut(fsmEvent);
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x000377CC File Offset: 0x000359CC
		private void SetEnableMagnetComponet(ChaoAbility ability)
		{
			float enable = 4f;
			if (StageAbilityManager.Instance != null)
			{
				enable = StageAbilityManager.Instance.GetChaoAbilityValue(ability);
			}
			for (int i = 0; i < base.transform.childCount; i++)
			{
				Transform child = base.transform.GetChild(i);
				ChaoPartsObjectMagnet component = child.GetComponent<ChaoPartsObjectMagnet>();
				if (component != null)
				{
					component.SetEnable(enable);
					break;
				}
			}
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00037844 File Offset: 0x00035A44
		private void SetMagnetPause(bool flag)
		{
			for (int i = 0; i < base.transform.childCount; i++)
			{
				Transform child = base.transform.GetChild(i);
				ChaoPartsObjectMagnet component = child.GetComponent<ChaoPartsObjectMagnet>();
				if (component != null)
				{
					component.SetPause(flag);
					break;
				}
			}
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0003789C File Offset: 0x00035A9C
		private TinyFsmState StateGameHardware(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				this.SetAnimationFlagForAbility(false);
				return TinyFsmState.End();
			case 1:
			{
				this.ChangeMovement(MOVESTATE_ID.GoCameraTarget);
				ChaoMoveGoCameraTarget currentMovement = this.GetCurrentMovement<ChaoMoveGoCameraTarget>();
				if (currentMovement != null)
				{
					currentMovement.SetParameter(ChaoState.HardwareCartridgeOffsetRate, 2f);
				}
				this.m_substate = 0;
				this.m_stateTimer = 0.8f;
				return TinyFsmState.End();
			}
			case 4:
			{
				ChaoState.SubStateItemPresent substate = (ChaoState.SubStateItemPresent)this.m_substate;
				if (substate != ChaoState.SubStateItemPresent.Action)
				{
					if (substate == ChaoState.SubStateItemPresent.Wait)
					{
						this.m_stateTimer -= fsmEvent.GetDeltaTime;
						if (this.m_stateTimer < 0f)
						{
							if (this.m_stateFlag.Test(1))
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StateOutItemPhantom)));
							}
							else
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
							}
						}
					}
				}
				else
				{
					this.m_stateTimer -= fsmEvent.GetDeltaTime;
					if (this.m_stateTimer < 0f)
					{
						ObjUtil.LightPlaySE("act_chao_megadrive", "SE");
						ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_time_combotime_r01", -1f);
						base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						this.m_stateTimer = 2f;
						this.m_substate = 1;
					}
				}
				return TinyFsmState.End();
			}
			case 5:
				this.SetMessageComeInOut(fsmEvent);
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00037A2C File Offset: 0x00035C2C
		private TinyFsmState StateGameCartridge(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				this.SetAnimationFlagForAbility(false);
				return TinyFsmState.End();
			case 1:
			{
				this.ChangeMovement(MOVESTATE_ID.GoCameraTarget);
				ChaoMoveGoCameraTarget currentMovement = this.GetCurrentMovement<ChaoMoveGoCameraTarget>();
				if (currentMovement != null)
				{
					currentMovement.SetParameter(ChaoState.HardwareCartridgeOffsetRate, 2f);
				}
				this.m_substate = 0;
				this.m_stateTimer = 0.8f;
				return TinyFsmState.End();
			}
			case 4:
			{
				ChaoState.SubStateItemPresent substate = (ChaoState.SubStateItemPresent)this.m_substate;
				if (substate != ChaoState.SubStateItemPresent.Action)
				{
					if (substate == ChaoState.SubStateItemPresent.Wait)
					{
						this.m_stateTimer -= fsmEvent.GetDeltaTime;
						if (this.m_stateTimer < 0f)
						{
							if (this.m_stateFlag.Test(1))
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StateOutItemPhantom)));
							}
							else
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
							}
						}
					}
				}
				else
				{
					this.m_stateTimer -= fsmEvent.GetDeltaTime;
					if (this.m_stateTimer < 0f)
					{
						if (StageAbilityManager.Instance != null)
						{
							int addCombo = (int)StageAbilityManager.Instance.GetChaoAbilityValue(ChaoAbility.ADD_COMBO_VALUE);
							if (StageComboManager.Instance != null)
							{
								StageComboManager.Instance.AddComboForChaoAbilityValue(addCombo);
							}
						}
						ObjUtil.LightPlaySE("act_chao_cartridge", "SE");
						ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_up_combocount_r01", -1f);
						base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						this.m_stateTimer = 2f;
						this.m_substate = 1;
					}
				}
				return TinyFsmState.End();
			}
			case 5:
				this.SetMessageComeInOut(fsmEvent);
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00037BF4 File Offset: 0x00035DF4
		private TinyFsmState StateNights(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				this.SetAnimationFlagForAbility(false);
				return TinyFsmState.End();
			case 1:
			{
				this.ChangeMovement(MOVESTATE_ID.GoCameraTarget);
				ChaoMoveGoCameraTarget currentMovement = this.GetCurrentMovement<ChaoMoveGoCameraTarget>();
				if (currentMovement != null)
				{
					currentMovement.SetParameter(ChaoState.NightsOffsetRate, 2f);
				}
				this.m_substate = 0;
				this.m_stateTimer = 1.5f;
				StageItemManager itemManager = this.GetItemManager();
				if (itemManager != null)
				{
					itemManager.SendMessage("OnChaoAbilityLoopComboUp", null, SendMessageOptions.DontRequireReceiver);
				}
				return TinyFsmState.End();
			}
			case 4:
			{
				ChaoState.SubStateItemPresent substate = (ChaoState.SubStateItemPresent)this.m_substate;
				if (substate != ChaoState.SubStateItemPresent.Action)
				{
					if (substate == ChaoState.SubStateItemPresent.Wait)
					{
						this.m_stateTimer -= fsmEvent.GetDeltaTime;
						if (this.m_stateTimer < 0f)
						{
							if (this.m_stateFlag.Test(1))
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StateOutItemPhantom)));
							}
							else
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
							}
						}
					}
				}
				else
				{
					this.m_stateTimer -= fsmEvent.GetDeltaTime;
					if (this.m_stateTimer < 0f)
					{
						ObjUtil.LightPlaySE("act_chao_nights", "SE");
						base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_clb_nights_magic_sr01", -1f);
						this.m_stateTimer = 2f;
						this.m_substate = 1;
					}
				}
				return TinyFsmState.End();
			}
			case 5:
				this.SetMessageComeInOut(fsmEvent);
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00037DA4 File Offset: 0x00035FA4
		private TinyFsmState StateReala(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				this.SetAnimationFlagForAbility(false);
				return TinyFsmState.End();
			case 1:
			{
				this.ChangeMovement(MOVESTATE_ID.GoCameraTarget);
				ChaoMoveGoCameraTarget currentMovement = this.GetCurrentMovement<ChaoMoveGoCameraTarget>();
				if (currentMovement != null)
				{
					currentMovement.SetParameter(ChaoState.RealaOffsetRate, 2f);
				}
				this.m_substate = 0;
				this.m_stateTimer = 1.5f;
				StageItemManager itemManager = this.GetItemManager();
				if (itemManager != null)
				{
					itemManager.SendMessage("OnChaoAbilityLoopMagnet", null, SendMessageOptions.DontRequireReceiver);
				}
				return TinyFsmState.End();
			}
			case 4:
			{
				ChaoState.SubStateItemPresent substate = (ChaoState.SubStateItemPresent)this.m_substate;
				if (substate != ChaoState.SubStateItemPresent.Action)
				{
					if (substate == ChaoState.SubStateItemPresent.Wait)
					{
						this.m_stateTimer -= fsmEvent.GetDeltaTime;
						if (this.m_stateTimer < 0f)
						{
							if (this.m_stateFlag.Test(1))
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StateOutItemPhantom)));
							}
							else
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
							}
						}
					}
				}
				else
				{
					this.m_stateTimer -= fsmEvent.GetDeltaTime;
					if (this.m_stateTimer < 0f)
					{
						ObjUtil.LightPlaySE("act_chao_reala", "SE");
						base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_clb_reala_magic_sr01", -1f);
						this.m_stateTimer = 2f;
						this.m_substate = 1;
					}
				}
				return TinyFsmState.End();
			}
			case 5:
				this.SetMessageComeInOut(fsmEvent);
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00037F54 File Offset: 0x00036154
		private void SetItemBtnObjAndUICamera()
		{
			if (this.m_uiCamera != null && this.m_itemBtnObj != null)
			{
				return;
			}
			GameObject cameraUIObject = HudMenuUtility.GetCameraUIObject();
			if (cameraUIObject != null)
			{
				this.m_uiCamera = cameraUIObject.GetComponent<Camera>();
				GameObject gameObject = GameObjectUtil.FindChildGameObject(cameraUIObject, "HudCockpit");
				if (gameObject != null)
				{
					this.m_itemBtnObj = GameObjectUtil.FindChildGameObject(gameObject, "HUD_btn_item");
				}
			}
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00037FCC File Offset: 0x000361CC
		private void SetTargetScreenPos()
		{
			if (this.m_uiCamera != null && this.m_itemBtnObj != null)
			{
				this.m_targetScreenPos = this.m_uiCamera.WorldToScreenPoint(this.m_itemBtnObj.transform.position);
			}
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0003801C File Offset: 0x0003621C
		private void SetPresentEquipItemPos()
		{
			if (Camera.main != null && this.m_uiCamera != null && this.m_itemBtnObj)
			{
				Vector3 position = Camera.main.WorldToScreenPoint(base.transform.position);
				Vector3 vector = this.m_uiCamera.WorldToScreenPoint(this.m_itemBtnObj.transform.position);
				position.x = vector.x;
				position.y = vector.y;
				Vector3 position2 = Camera.main.ScreenToWorldPoint(position);
				position2.z = 0f;
				this.m_effectObj.transform.position = position2;
			}
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x000380D4 File Offset: 0x000362D4
		private TinyFsmState StateItemPresent(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				this.SetAnimationFlagForAbility(false);
				if (!this.m_presentFlag)
				{
					this.m_chaoItemType = ChaoState.ChaoAbilityItemTblPhantom[UnityEngine.Random.Range(0, ChaoState.ChaoAbilityItemTblPhantom.Length)];
					StageItemManager itemManager = this.GetItemManager();
					if (this.CheckItemPresent(this.m_chaoItemType) && itemManager != null)
					{
						MsgAddItemToManager value = new MsgAddItemToManager(this.m_chaoItemType);
						itemManager.SendMessage("OnAddItem", value, SendMessageOptions.DontRequireReceiver);
					}
				}
				return TinyFsmState.End();
			case 1:
			{
				this.ChangeMovement(MOVESTATE_ID.GoCameraTarget);
				ChaoMoveGoCameraTarget currentMovement = this.GetCurrentMovement<ChaoMoveGoCameraTarget>();
				if (currentMovement != null)
				{
					currentMovement.SetParameter(ChaoState.ItemPresentOffsetRate, 2f);
				}
				this.m_substate = 0;
				this.m_stateTimer = 0.6f;
				this.m_presentFlag = false;
				return TinyFsmState.End();
			}
			case 4:
			{
				float getDeltaTime = fsmEvent.GetDeltaTime;
				ChaoState.SubStateItemPresent substate = (ChaoState.SubStateItemPresent)this.m_substate;
				if (substate != ChaoState.SubStateItemPresent.Action)
				{
					if (substate == ChaoState.SubStateItemPresent.Wait)
					{
						this.m_stateTimer -= getDeltaTime;
						if (this.m_stateTimer < 0f)
						{
							if (this.m_stateFlag.Test(1))
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StateOutItemPhantom)));
							}
							else
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
							}
						}
					}
				}
				else
				{
					this.m_stateTimer -= getDeltaTime;
					if (this.m_stateTimer < 0f)
					{
						bool flag = false;
						PlayerInformation playerInformation = GameObjectUtil.FindGameObjectComponentWithTag<PlayerInformation>("StageManager", "PlayerInformation");
						if (playerInformation != null)
						{
							if (playerInformation.PhantomType == PhantomType.NONE)
							{
								this.m_chaoItemType = ChaoState.ChaoAbilityItemTbl[UnityEngine.Random.Range(0, ChaoState.ChaoAbilityItemTbl.Length)];
								flag = this.CheckItemPresent(this.m_chaoItemType);
							}
							if (!flag)
							{
								this.m_chaoItemType = ChaoState.ChaoAbilityItemTblPhantom[UnityEngine.Random.Range(0, ChaoState.ChaoAbilityItemTblPhantom.Length)];
								flag = this.CheckItemPresent(this.m_chaoItemType);
							}
						}
						if (flag)
						{
							StageItemManager itemManager2 = this.GetItemManager();
							if (itemManager2 != null)
							{
								ItemType chaoItemType = this.m_chaoItemType;
								MsgAddItemToManager value2 = new MsgAddItemToManager(chaoItemType);
								itemManager2.SendMessage("OnAddItem", value2, SendMessageOptions.DontRequireReceiver);
								ObjUtil.LightPlaySE("obj_itembox", "SE");
								ObjUtil.LightPlaySE("act_sharla_magic", "SE");
								ObjUtil.SendGetItemIcon(chaoItemType);
								ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_magic_item_st_sr01", -1f);
								base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
								MsgAbilityEffectStart value3 = new MsgAbilityEffectStart(ChaoAbility.COMBO_ITEM_BOX, "ef_ch_magic_item_ht_sr01", false, true);
								GameObjectUtil.SendDelayedMessageToTagObjects("Player", "OnMsgAbilityEffectStart", value3);
							}
							this.m_presentFlag = true;
							this.m_stateTimer = 1.2f;
							this.m_substate = 1;
						}
						else if (this.m_stateFlag.Test(1))
						{
							this.ChangeState(new TinyFsmState(new EventFunction(this.StateOutItemPhantom)));
						}
						else
						{
							this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
						}
					}
				}
				return TinyFsmState.End();
			}
			case 5:
				this.SetMessageComeInOut(fsmEvent);
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00038414 File Offset: 0x00036614
		private TinyFsmState StateItemPresent2(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				this.SetAnimationFlagForAbility(false);
				if (!this.m_presentFlag)
				{
					this.m_chaoItemType = ChaoState.ChaoAbilityItemTblPhantom[UnityEngine.Random.Range(0, ChaoState.ChaoAbilityItemTblPhantom.Length)];
					StageItemManager itemManager = this.GetItemManager();
					if (this.CheckItemPresent(this.m_chaoItemType) && itemManager != null)
					{
						MsgAddItemToManager value = new MsgAddItemToManager(this.m_chaoItemType);
						itemManager.SendMessage("OnAddItem", value, SendMessageOptions.DontRequireReceiver);
					}
				}
				return TinyFsmState.End();
			case 1:
			{
				this.ChangeMovement(MOVESTATE_ID.GoCameraTarget);
				ChaoMoveGoCameraTarget currentMovement = this.GetCurrentMovement<ChaoMoveGoCameraTarget>();
				if (currentMovement != null)
				{
					currentMovement.SetParameter(ChaoState.ItemPresentOffsetRate, 2f);
				}
				this.m_substate = 0;
				this.m_stateTimer = 0.6f;
				this.m_presentFlag = false;
				return TinyFsmState.End();
			}
			case 4:
			{
				float getDeltaTime = fsmEvent.GetDeltaTime;
				ChaoState.SubStateItemPresent substate = (ChaoState.SubStateItemPresent)this.m_substate;
				if (substate != ChaoState.SubStateItemPresent.Action)
				{
					if (substate == ChaoState.SubStateItemPresent.Wait)
					{
						this.m_stateTimer -= getDeltaTime;
						if (this.m_stateTimer < 0f)
						{
							if (this.m_stateFlag.Test(1))
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StateOutItemPhantom)));
							}
							else
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
							}
						}
					}
				}
				else
				{
					this.m_stateTimer -= getDeltaTime;
					if (this.m_stateTimer < 0f)
					{
						bool flag = false;
						PlayerInformation playerInformation = GameObjectUtil.FindGameObjectComponentWithTag<PlayerInformation>("StageManager", "PlayerInformation");
						if (playerInformation != null)
						{
							if (playerInformation.PhantomType == PhantomType.NONE)
							{
								this.m_chaoItemType = ChaoState.ChaoAbilityItemTbl[UnityEngine.Random.Range(0, ChaoState.ChaoAbilityItemTbl.Length)];
								flag = this.CheckItemPresent(this.m_chaoItemType);
							}
							if (!flag)
							{
								this.m_chaoItemType = ChaoState.ChaoAbilityItemTblPhantom[UnityEngine.Random.Range(0, ChaoState.ChaoAbilityItemTblPhantom.Length)];
								flag = this.CheckItemPresent(this.m_chaoItemType);
							}
						}
						if (flag)
						{
							StageItemManager itemManager2 = this.GetItemManager();
							if (itemManager2 != null)
							{
								MsgAddItemToManager value2 = new MsgAddItemToManager(this.m_chaoItemType);
								itemManager2.SendMessage("OnAddItem", value2, SendMessageOptions.DontRequireReceiver);
								ObjUtil.LightPlaySE("obj_itembox", "SE");
								ObjUtil.LightPlaySE("act_chao_quna", "SE");
								ObjUtil.SendGetItemIcon(this.m_chaoItemType);
								ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_clb_quna_item_st_sr01", -1f);
								base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
								MsgAbilityEffectStart value3 = new MsgAbilityEffectStart(ChaoAbility.CHECK_POINT_ITEM_BOX, "ef_ch_clb_quna_item_ht_sr01", false, true);
								GameObjectUtil.SendDelayedMessageToTagObjects("Player", "OnMsgAbilityEffectStart", value3);
							}
							this.m_stateTimer = 1.2f;
							this.m_substate = 1;
							this.m_presentFlag = true;
						}
						else if (this.m_stateFlag.Test(1))
						{
							this.ChangeState(new TinyFsmState(new EventFunction(this.StateOutItemPhantom)));
						}
						else
						{
							this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
						}
					}
				}
				return TinyFsmState.End();
			}
			case 5:
				this.SetMessageComeInOut(fsmEvent);
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00038754 File Offset: 0x00036954
		private TinyFsmState StatePhantomPresent(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				this.SetAnimationFlagForAbility(false);
				if (!this.m_presentFlag)
				{
					StageItemManager itemManager = this.GetItemManager();
					if (itemManager != null)
					{
						MsgAddItemToManager value = new MsgAddItemToManager(this.m_chaoItemType);
						itemManager.SendMessage("OnAddColorItem", value, SendMessageOptions.DontRequireReceiver);
					}
				}
				return TinyFsmState.End();
			case 1:
			{
				PlayerInformation playerInformation = GameObjectUtil.FindGameObjectComponentWithTag<PlayerInformation>("StageManager", "PlayerInformation");
				if (playerInformation != null)
				{
					PhantomType phantomType = playerInformation.PhantomType;
					switch (phantomType + 1)
					{
					case PhantomType.LASER:
					{
						StageItemManager itemManager2 = this.GetItemManager();
						if (itemManager2 != null)
						{
							this.m_chaoItemType = itemManager2.GetPhantomItemType();
						}
						if (this.m_chaoItemType == ItemType.UNKNOWN)
						{
							this.m_chaoItemType = ChaoState.ChaoAbilityPhantomTbl[UnityEngine.Random.Range(0, ChaoState.ChaoAbilityPhantomTbl.Length)];
						}
						break;
					}
					case PhantomType.DRILL:
						this.m_chaoItemType = ItemType.LASER;
						break;
					case PhantomType.ASTEROID:
						this.m_chaoItemType = ItemType.DRILL;
						break;
					case PhantomType.NUM_MAX:
						this.m_chaoItemType = ItemType.ASTEROID;
						break;
					}
				}
				this.ChangeMovement(MOVESTATE_ID.GoCameraTarget);
				ChaoMoveGoCameraTarget currentMovement = this.GetCurrentMovement<ChaoMoveGoCameraTarget>();
				if (currentMovement != null)
				{
					currentMovement.SetParameter(ChaoState.ItemPresentOffsetRate, 2f);
				}
				this.m_substate = 0;
				this.m_stateTimer = 0.6f;
				this.m_presentFlag = false;
				return TinyFsmState.End();
			}
			case 4:
			{
				float getDeltaTime = fsmEvent.GetDeltaTime;
				ChaoState.SubStateItemPresent substate = (ChaoState.SubStateItemPresent)this.m_substate;
				if (substate != ChaoState.SubStateItemPresent.Action)
				{
					if (substate == ChaoState.SubStateItemPresent.Wait)
					{
						this.m_stateTimer -= getDeltaTime;
						if (this.m_stateTimer < 0f)
						{
							if (this.m_stateFlag.Test(1))
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StateOutItemPhantom)));
							}
							else
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
							}
						}
					}
				}
				else
				{
					this.m_stateTimer -= getDeltaTime;
					if (this.m_stateTimer < 0f)
					{
						StageItemManager itemManager3 = this.GetItemManager();
						if (itemManager3 != null)
						{
							MsgAddItemToManager value2 = new MsgAddItemToManager(this.m_chaoItemType);
							itemManager3.SendMessage("OnAddColorItem", value2, SendMessageOptions.DontRequireReceiver);
							ObjUtil.LightPlaySE("obj_itembox", "SE");
							ObjUtil.LightPlaySE("act_sharla_magic", "SE");
							ObjUtil.SendGetItemIcon(this.m_chaoItemType);
							ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_sp1_merlina_magic_sr01", -1f);
							base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						}
						this.m_presentFlag = true;
						this.m_stateTimer = 1.2f;
						this.m_substate = 1;
					}
				}
				return TinyFsmState.End();
			}
			case 5:
				this.SetMessageComeInOut(fsmEvent);
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00038A2C File Offset: 0x00036C2C
		private TinyFsmState StateReviveEquipItem(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				this.SetAnimationFlagForAbility(false);
				this.m_stateFlag.Set(2, false);
				return TinyFsmState.End();
			case 1:
			{
				this.m_stateFlag.Set(2, true);
				this.ChangeMovement(MOVESTATE_ID.GoCameraTarget);
				ChaoMoveGoCameraTarget currentMovement = this.GetCurrentMovement<ChaoMoveGoCameraTarget>();
				if (currentMovement != null)
				{
					currentMovement.SetParameter(ChaoState.ChaosOffsetRate, 2f);
				}
				this.m_substate = 0;
				this.m_stateTimer = 1f;
				this.SetItemBtnObjAndUICamera();
				return TinyFsmState.End();
			}
			case 4:
			{
				ChaoState.SubStateItemPresent substate = (ChaoState.SubStateItemPresent)this.m_substate;
				if (substate != ChaoState.SubStateItemPresent.Action)
				{
					if (substate == ChaoState.SubStateItemPresent.Wait)
					{
						this.m_stateTimer -= fsmEvent.GetDeltaTime;
						if (this.m_stateTimer < 0f)
						{
							if (this.m_stateFlag.Test(1))
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StateOutItemPhantom)));
							}
							else
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
							}
						}
					}
				}
				else
				{
					this.m_stateTimer -= fsmEvent.GetDeltaTime;
					if (this.m_stateTimer < 0f)
					{
						ObjUtil.LightPlaySE("act_chao_chaosadv", "SE");
						ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_itemrecycle_sr01", -1f);
						ObjUtil.PlayChaoEffectForHUD(this.m_itemBtnObj, "ef_ch_itemrecycle_ht_sr01", -1f);
						base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						this.m_stateTimer = 2.2f;
						this.m_substate = 1;
					}
				}
				return TinyFsmState.End();
			}
			case 5:
				this.SetMessageComeInOut(fsmEvent);
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00038BF4 File Offset: 0x00036DF4
		private TinyFsmState StatePresentSRareEquipItem(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				this.SetAnimationFlagForAbility(false);
				if (!this.m_presentFlag)
				{
					StageItemManager itemManager = this.GetItemManager();
					if (itemManager != null)
					{
						itemManager.SendMessage("OnAddEquipItem", null, SendMessageOptions.DontRequireReceiver);
						this.m_presentFlag = true;
					}
				}
				return TinyFsmState.End();
			case 1:
			{
				this.ChangeMovement(MOVESTATE_ID.GoCameraTarget);
				ChaoMoveGoCameraTarget currentMovement = this.GetCurrentMovement<ChaoMoveGoCameraTarget>();
				if (currentMovement != null)
				{
					currentMovement.SetParameter(ChaoState.ItemPresentOffsetRate, 2f);
				}
				this.m_substate = 0;
				this.m_stateTimer = 0.6f;
				this.SetItemBtnObjAndUICamera();
				this.m_presentFlag = false;
				return TinyFsmState.End();
			}
			case 4:
			{
				ChaoState.SubStateItemPresent substate = (ChaoState.SubStateItemPresent)this.m_substate;
				if (substate != ChaoState.SubStateItemPresent.Action)
				{
					if (substate == ChaoState.SubStateItemPresent.Wait)
					{
						this.m_stateTimer -= fsmEvent.GetDeltaTime;
						if (this.m_stateTimer < 0f)
						{
							if (this.m_stateFlag.Test(1))
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StateOutItemPhantom)));
							}
							else
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
							}
						}
					}
				}
				else
				{
					this.m_stateTimer -= fsmEvent.GetDeltaTime;
					if (this.m_stateTimer < 0f)
					{
						ObjUtil.LightPlaySE("act_sharla_magic", "SE");
						this.m_effectObj = ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_magic_darkqueen_st_sr01", -1f);
						ObjUtil.PlayChaoEffectForHUD(this.m_itemBtnObj, "ef_ch_magic_darkqueen_ht_sr01", -1f);
						base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						this.m_stateTimer = 1.2f;
						this.m_substate = 1;
					}
				}
				return TinyFsmState.End();
			}
			case 5:
				this.SetMessageComeInOut(fsmEvent);
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00038DDC File Offset: 0x00036FDC
		private TinyFsmState StatePresentEquipItem(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				this.SetAnimationFlagForAbility(false);
				this.m_effectObj = null;
				if (!this.m_presentFlag)
				{
					StageItemManager itemManager = this.GetItemManager();
					if (itemManager != null)
					{
						itemManager.SendMessage("OnAddEquipItem", null, SendMessageOptions.DontRequireReceiver);
						this.m_presentFlag = true;
					}
				}
				return TinyFsmState.End();
			case 1:
			{
				this.ChangeMovement(MOVESTATE_ID.GoCameraTarget);
				ChaoMoveGoCameraTarget currentMovement = this.GetCurrentMovement<ChaoMoveGoCameraTarget>();
				if (currentMovement != null)
				{
					currentMovement.SetParameter(ChaoState.PufferFishOffsetRate, 2.5f);
				}
				this.m_substate = 0;
				this.m_stateTimer = 1f;
				this.SetItemBtnObjAndUICamera();
				this.SetTargetScreenPos();
				this.m_presentFlag = false;
				return TinyFsmState.End();
			}
			case 4:
				switch (this.m_substate)
				{
				case 0:
					this.m_stateTimer -= fsmEvent.GetDeltaTime;
					if (this.m_stateTimer < 0f)
					{
						ObjUtil.LightPlaySE("act_chao_effect", "SE");
						this.m_effectObj = ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_combo_getitem_r01", 2.5f);
						if (this.m_effectObj != null && Camera.main != null)
						{
							this.m_effectScreenPos = Camera.main.WorldToScreenPoint(this.m_effectObj.transform.position);
							this.m_targetScreenPos.z = this.m_effectScreenPos.z;
						}
						base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						this.m_stateTimer = 0.5f;
						this.m_substate = 1;
					}
					break;
				case 1:
					this.m_stateTimer -= fsmEvent.GetDeltaTime;
					if (this.m_stateTimer < 0f)
					{
						this.m_distance = Vector3.Distance(this.m_effectScreenPos, this.m_targetScreenPos);
						this.m_stateTimer = 1f;
						this.m_substate = 2;
					}
					break;
				case 2:
					this.m_stateTimer -= fsmEvent.GetDeltaTime;
					if (this.m_effectObj != null)
					{
						this.m_effectScreenPos = Vector3.MoveTowards(this.m_effectScreenPos, this.m_targetScreenPos, this.m_distance * Time.deltaTime);
						if (Camera.main != null)
						{
							Vector3 position = Camera.main.ScreenToWorldPoint(this.m_effectScreenPos);
							position.z = -1.5f;
							this.m_effectObj.transform.position = position;
						}
					}
					if (this.m_stateTimer < 0f)
					{
						StageItemManager itemManager2 = this.GetItemManager();
						if (itemManager2 != null)
						{
							itemManager2.SendMessage("OnAddEquipItem", null, SendMessageOptions.DontRequireReceiver);
						}
						this.m_presentFlag = true;
						this.m_stateTimer = 0.5f;
						this.m_substate = 3;
					}
					break;
				case 3:
					this.m_stateTimer -= fsmEvent.GetDeltaTime;
					if (this.m_stateTimer < 0f)
					{
						if (this.m_stateFlag.Test(1))
						{
							this.ChangeState(new TinyFsmState(new EventFunction(this.StateOutItemPhantom)));
						}
						else
						{
							this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
						}
					}
					break;
				}
				return TinyFsmState.End();
			case 5:
				this.SetMessageComeInOut(fsmEvent);
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00039144 File Offset: 0x00037344
		private TinyFsmState StatePhantomPresentAsteroid(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				this.SetAnimationFlagForAbility(false);
				return TinyFsmState.End();
			case 1:
			{
				this.m_chaoItemType = ItemType.ASTEROID;
				this.ChangeMovement(MOVESTATE_ID.GoCameraTarget);
				ChaoMoveGoCameraTarget currentMovement = this.GetCurrentMovement<ChaoMoveGoCameraTarget>();
				if (currentMovement != null)
				{
					currentMovement.SetParameter(ChaoState.ItemPresentOffsetRate, 2f);
				}
				this.m_substate = 0;
				this.m_stateTimer = 0.6f;
				return TinyFsmState.End();
			}
			case 4:
			{
				ChaoState.SubStateItemPresent substate = (ChaoState.SubStateItemPresent)this.m_substate;
				if (substate != ChaoState.SubStateItemPresent.Action)
				{
					if (substate == ChaoState.SubStateItemPresent.Wait)
					{
						this.m_stateTimer -= fsmEvent.GetDeltaTime;
						if (this.m_stateTimer < 0f)
						{
							if (this.m_stateFlag.Test(1))
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StateOutItemPhantom)));
							}
							else
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
							}
						}
					}
				}
				else
				{
					this.m_stateTimer -= fsmEvent.GetDeltaTime;
					if (this.m_stateTimer < 0f)
					{
						StageItemManager itemManager = this.GetItemManager();
						if (itemManager != null)
						{
							MsgAddItemToManager value = new MsgAddItemToManager(this.m_chaoItemType);
							itemManager.SendMessage("OnAddColorItem", value, SendMessageOptions.DontRequireReceiver);
							ObjUtil.LightPlaySE("act_chao_effect", "SE");
							ObjUtil.SendGetItemIcon(this.m_chaoItemType);
							ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_combo_asteroid_r01", -1f);
							base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						}
						this.m_stateTimer = 1.2f;
						this.m_substate = 1;
					}
				}
				return TinyFsmState.End();
			}
			case 5:
				this.SetMessageComeInOut(fsmEvent);
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00039314 File Offset: 0x00037514
		private TinyFsmState StatePhantomPresentDrill(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				this.SetAnimationFlagForAbility(false);
				return TinyFsmState.End();
			case 1:
			{
				this.m_chaoItemType = ItemType.DRILL;
				this.ChangeMovement(MOVESTATE_ID.GoCameraTarget);
				ChaoMoveGoCameraTarget currentMovement = this.GetCurrentMovement<ChaoMoveGoCameraTarget>();
				if (currentMovement != null)
				{
					currentMovement.SetParameter(ChaoState.ItemPresentOffsetRate, 2f);
				}
				this.m_substate = 0;
				this.m_stateTimer = 0.6f;
				return TinyFsmState.End();
			}
			case 4:
			{
				ChaoState.SubStateItemPresent substate = (ChaoState.SubStateItemPresent)this.m_substate;
				if (substate != ChaoState.SubStateItemPresent.Action)
				{
					if (substate == ChaoState.SubStateItemPresent.Wait)
					{
						this.m_stateTimer -= fsmEvent.GetDeltaTime;
						if (this.m_stateTimer < 0f)
						{
							if (this.m_stateFlag.Test(1))
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StateOutItemPhantom)));
							}
							else
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
							}
						}
					}
				}
				else
				{
					this.m_stateTimer -= fsmEvent.GetDeltaTime;
					if (this.m_stateTimer < 0f)
					{
						StageItemManager itemManager = this.GetItemManager();
						if (itemManager != null)
						{
							MsgAddItemToManager value = new MsgAddItemToManager(this.m_chaoItemType);
							itemManager.SendMessage("OnAddColorItem", value, SendMessageOptions.DontRequireReceiver);
							ObjUtil.LightPlaySE("act_chao_effect", "SE");
							ObjUtil.SendGetItemIcon(this.m_chaoItemType);
							ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_combo_drill_r01", -1f);
							base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						}
						this.m_stateTimer = 1.2f;
						this.m_substate = 1;
					}
				}
				return TinyFsmState.End();
			}
			case 5:
				this.SetMessageComeInOut(fsmEvent);
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x000394E4 File Offset: 0x000376E4
		private TinyFsmState StatePhantomPresentLaser(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				this.SetAnimationFlagForAbility(false);
				return TinyFsmState.End();
			case 1:
			{
				this.m_chaoItemType = ItemType.LASER;
				this.ChangeMovement(MOVESTATE_ID.GoCameraTarget);
				ChaoMoveGoCameraTarget currentMovement = this.GetCurrentMovement<ChaoMoveGoCameraTarget>();
				if (currentMovement != null)
				{
					currentMovement.SetParameter(ChaoState.ItemPresentOffsetRate, 2f);
				}
				this.m_substate = 0;
				this.m_stateTimer = 0.6f;
				return TinyFsmState.End();
			}
			case 4:
			{
				ChaoState.SubStateItemPresent substate = (ChaoState.SubStateItemPresent)this.m_substate;
				if (substate != ChaoState.SubStateItemPresent.Action)
				{
					if (substate == ChaoState.SubStateItemPresent.Wait)
					{
						this.m_stateTimer -= fsmEvent.GetDeltaTime;
						if (this.m_stateTimer < 0f)
						{
							if (this.m_stateFlag.Test(1))
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StateOutItemPhantom)));
							}
							else
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
							}
						}
					}
				}
				else
				{
					this.m_stateTimer -= fsmEvent.GetDeltaTime;
					if (this.m_stateTimer < 0f)
					{
						StageItemManager itemManager = this.GetItemManager();
						if (itemManager != null)
						{
							MsgAddItemToManager value = new MsgAddItemToManager(this.m_chaoItemType);
							itemManager.SendMessage("OnAddColorItem", value, SendMessageOptions.DontRequireReceiver);
							ObjUtil.LightPlaySE("act_chao_effect", "SE");
							ObjUtil.SendGetItemIcon(this.m_chaoItemType);
							ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_combo_laser_r01", -1f);
							base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						}
						this.m_stateTimer = 1.2f;
						this.m_substate = 1;
					}
				}
				return TinyFsmState.End();
			}
			case 5:
				this.SetMessageComeInOut(fsmEvent);
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x000396B4 File Offset: 0x000378B4
		private TinyFsmState StateChangeEquipItem(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				this.SetAnimationFlagForAbility(false);
				if (!this.m_presentFlag)
				{
					StageItemManager itemManager = this.GetItemManager();
					if (itemManager != null)
					{
						itemManager.SendMessage("OnChangeItem", null, SendMessageOptions.DontRequireReceiver);
					}
					this.m_presentFlag = true;
				}
				return TinyFsmState.End();
			case 1:
			{
				this.ChangeMovement(MOVESTATE_ID.GoCameraTarget);
				ChaoMoveGoCameraTarget currentMovement = this.GetCurrentMovement<ChaoMoveGoCameraTarget>();
				if (currentMovement != null)
				{
					currentMovement.SetParameter(ChaoState.ItemPresentOffsetRate, 2f);
				}
				this.m_substate = 0;
				this.m_stateTimer = 0.6f;
				this.SetItemBtnObjAndUICamera();
				this.m_presentFlag = false;
				return TinyFsmState.End();
			}
			case 4:
			{
				ChaoState.SubStateItemPresent substate = (ChaoState.SubStateItemPresent)this.m_substate;
				if (substate != ChaoState.SubStateItemPresent.Action)
				{
					if (substate == ChaoState.SubStateItemPresent.Wait)
					{
						this.m_stateTimer -= fsmEvent.GetDeltaTime;
						if (this.m_stateTimer < 0f)
						{
							if (this.m_stateFlag.Test(1))
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StateOutItemPhantom)));
							}
							else
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
							}
						}
					}
				}
				else
				{
					this.m_stateTimer -= fsmEvent.GetDeltaTime;
					if (this.m_stateTimer < 0f)
					{
						StageItemManager itemManager2 = this.GetItemManager();
						if (itemManager2 != null)
						{
							itemManager2.SendMessage("OnChangeItem", null, SendMessageOptions.DontRequireReceiver);
							ObjUtil.LightPlaySE("act_chao_effect", "SE");
							ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_combo_changeitem_r01", -1f);
							ObjUtil.PlayChaoEffectForHUD(this.m_itemBtnObj, "ef_ch_combo_changeitem_ht_r01", -1f);
							base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						}
						this.m_stateTimer = 1f;
						this.m_substate = 1;
						this.m_presentFlag = true;
					}
				}
				return TinyFsmState.End();
			}
			case 5:
				this.SetMessageComeInOut(fsmEvent);
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x000398C4 File Offset: 0x00037AC4
		private TinyFsmState StateOrbotItemPresent(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				this.SetAnimationFlagForAbility(false);
				if (!this.m_presentFlag)
				{
					this.m_chaoItemType = ChaoState.ChaoAbilityItemTblPhantom[UnityEngine.Random.Range(0, ChaoState.ChaoAbilityItemTblPhantom.Length)];
					if (this.CheckItemPresent(this.m_chaoItemType) && StageItemManager.Instance != null)
					{
						StageItemManager.Instance.SendMessage("OnAddItem", new MsgAddItemToManager(this.m_chaoItemType), SendMessageOptions.DontRequireReceiver);
					}
				}
				return TinyFsmState.End();
			case 1:
			{
				this.ChangeMovement(MOVESTATE_ID.GoCameraTarget);
				ChaoMoveGoCameraTarget currentMovement = this.GetCurrentMovement<ChaoMoveGoCameraTarget>();
				if (currentMovement != null)
				{
					currentMovement.SetParameter(ChaoState.ItemPresentOffsetRate, 2f);
				}
				this.m_substate = 0;
				this.m_stateTimer = 0.6f;
				this.m_presentFlag = false;
				return TinyFsmState.End();
			}
			case 4:
			{
				float getDeltaTime = fsmEvent.GetDeltaTime;
				switch (this.m_substate)
				{
				case 0:
					this.m_stateTimer -= getDeltaTime;
					if (this.m_stateTimer < 0f)
					{
						bool flag = false;
						PlayerInformation playerInformation = GameObjectUtil.FindGameObjectComponentWithTag<PlayerInformation>("StageManager", "PlayerInformation");
						if (playerInformation != null)
						{
							if (playerInformation.PhantomType == PhantomType.NONE)
							{
								this.m_chaoItemType = ChaoState.ChaoAbilityItemTbl[UnityEngine.Random.Range(0, ChaoState.ChaoAbilityItemTbl.Length)];
								flag = this.CheckItemPresent(this.m_chaoItemType);
							}
							if (!flag)
							{
								this.m_chaoItemType = ChaoState.ChaoAbilityItemTblPhantom[UnityEngine.Random.Range(0, ChaoState.ChaoAbilityItemTblPhantom.Length)];
								flag = this.CheckItemPresent(this.m_chaoItemType);
							}
						}
						if (flag)
						{
							if (StageItemManager.Instance != null)
							{
								ItemType chaoItemType = this.m_chaoItemType;
								MsgAddItemToManager value = new MsgAddItemToManager(chaoItemType);
								StageItemManager.Instance.SendMessage("OnAddItem", value, SendMessageOptions.DontRequireReceiver);
								ObjUtil.LightPlaySE("obj_itembox", "SE");
								ObjUtil.LightPlaySE("act_chao_effect", "SE");
								ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_combo_randombuyitem_r01", -1f);
								ObjUtil.SendGetItemIcon(chaoItemType);
								base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
							}
							this.m_presentFlag = true;
							this.m_stateTimer = 0.8f;
							this.m_substate = 2;
						}
						else if (this.m_stateFlag.Test(1))
						{
							this.ChangeState(new TinyFsmState(new EventFunction(this.StateOutItemPhantom)));
						}
						else
						{
							this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
						}
					}
					break;
				case 1:
					this.m_stateTimer -= getDeltaTime;
					if (this.m_stateTimer < 0f)
					{
						if (this.m_stateFlag.Test(1))
						{
							this.ChangeState(new TinyFsmState(new EventFunction(this.StateOutItemPhantom)));
						}
						else
						{
							this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
						}
					}
					break;
				case 2:
					this.m_stateTimer -= getDeltaTime;
					if (this.m_stateTimer < 0f)
					{
						this.m_stateTimer += 1f;
						ObjUtil.LightPlaySE("act_ringspread", "SE");
						this.m_substate = 1;
					}
					break;
				}
				return TinyFsmState.End();
			}
			case 5:
				this.SetMessageComeInOut(fsmEvent);
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00039C28 File Offset: 0x00037E28
		private TinyFsmState StateCuebotItemPresent(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				this.SetAnimationFlagForAbility(false);
				if (!this.m_presentFlag)
				{
					this.m_chaoItemType = ChaoState.ChaoAbilityItemTblPhantom[UnityEngine.Random.Range(0, ChaoState.ChaoAbilityItemTblPhantom.Length)];
					if (this.CheckItemPresent(this.m_chaoItemType) && StageItemManager.Instance != null)
					{
						StageItemManager.Instance.SendMessage("OnAddItem", new MsgAddItemToManager(this.m_chaoItemType), SendMessageOptions.DontRequireReceiver);
					}
				}
				return TinyFsmState.End();
			case 1:
			{
				this.ChangeMovement(MOVESTATE_ID.GoCameraTarget);
				ChaoMoveGoCameraTarget currentMovement = this.GetCurrentMovement<ChaoMoveGoCameraTarget>();
				if (currentMovement != null)
				{
					currentMovement.SetParameter(ChaoState.CuebotItemOffsetRate, 2f);
				}
				this.m_substate = 0;
				this.m_stateTimer = 0.6f;
				this.m_presentFlag = false;
				return TinyFsmState.End();
			}
			case 4:
			{
				float getDeltaTime = fsmEvent.GetDeltaTime;
				ChaoState.SubStateItemPresent substate = (ChaoState.SubStateItemPresent)this.m_substate;
				if (substate != ChaoState.SubStateItemPresent.Action)
				{
					if (substate == ChaoState.SubStateItemPresent.Wait)
					{
						this.m_stateTimer -= getDeltaTime;
						if (this.m_stateTimer < 0f)
						{
							if (this.m_stateFlag.Test(1))
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StateOutItemPhantom)));
							}
							else
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
							}
						}
					}
				}
				else
				{
					this.m_stateTimer -= getDeltaTime;
					if (this.m_stateTimer < 0f)
					{
						bool flag = false;
						PlayerInformation playerInformation = GameObjectUtil.FindGameObjectComponentWithTag<PlayerInformation>("StageManager", "PlayerInformation");
						if (playerInformation != null)
						{
							if (playerInformation.PhantomType == PhantomType.NONE)
							{
								this.m_chaoItemType = ChaoState.ChaoAbilityItemTbl[UnityEngine.Random.Range(0, ChaoState.ChaoAbilityItemTbl.Length)];
								flag = this.CheckItemPresent(this.m_chaoItemType);
							}
							if (!flag)
							{
								this.m_chaoItemType = ChaoState.ChaoAbilityItemTblPhantom[UnityEngine.Random.Range(0, ChaoState.ChaoAbilityItemTblPhantom.Length)];
								flag = this.CheckItemPresent(this.m_chaoItemType);
							}
						}
						if (flag)
						{
							if (StageItemManager.Instance != null)
							{
								ItemType chaoItemType = this.m_chaoItemType;
								MsgAddItemToManager value = new MsgAddItemToManager(chaoItemType);
								StageItemManager.Instance.SendMessage("OnAddItem", value, SendMessageOptions.DontRequireReceiver);
								ObjUtil.LightPlaySE("obj_itembox", "SE");
								ObjUtil.LightPlaySE("act_chao_effect", "SE");
								ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_combo_randomitem_r01", -1f);
								ObjUtil.SendGetItemIcon(chaoItemType);
								base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
							}
							this.m_presentFlag = true;
							this.m_stateTimer = 1.8f;
							this.m_substate = 1;
						}
						else if (this.m_stateFlag.Test(1))
						{
							this.ChangeState(new TinyFsmState(new EventFunction(this.StateOutItemPhantom)));
						}
						else
						{
							this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
						}
					}
				}
				return TinyFsmState.End();
			}
			case 5:
				this.SetMessageComeInOut(fsmEvent);
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00039F3C File Offset: 0x0003813C
		private TinyFsmState StateFailCuebotItemPresent(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				this.SetAnimationFlagForAbility(false);
				return TinyFsmState.End();
			case 1:
			{
				this.ChangeMovement(MOVESTATE_ID.GoCameraTarget);
				ChaoMoveGoCameraTarget currentMovement = this.GetCurrentMovement<ChaoMoveGoCameraTarget>();
				if (currentMovement != null)
				{
					currentMovement.SetParameter(ChaoState.CuebotItemOffsetRate, 2f);
				}
				this.m_substate = 0;
				this.m_stateTimer = 0.6f;
				return TinyFsmState.End();
			}
			case 4:
			{
				float getDeltaTime = fsmEvent.GetDeltaTime;
				ChaoState.SubStateItemPresent substate = (ChaoState.SubStateItemPresent)this.m_substate;
				if (substate != ChaoState.SubStateItemPresent.Action)
				{
					if (substate == ChaoState.SubStateItemPresent.Wait)
					{
						this.m_stateTimer -= getDeltaTime;
						if (this.m_stateTimer < 0f)
						{
							if (this.m_stateFlag.Test(1))
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StateOutItemPhantom)));
							}
							else
							{
								this.ChangeState(new TinyFsmState(new EventFunction(this.StatePursue)));
							}
						}
					}
				}
				else
				{
					this.m_stateTimer -= getDeltaTime;
					if (this.m_stateTimer < 0f)
					{
						ObjUtil.LightPlaySE("sys_error", "SE");
						ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_combo_randomitem_fail_r01", -1f);
						base.StartCoroutine(this.SetAnimationFlagForAbilityCourutine(true));
						this.m_stateTimer = 1.8f;
						this.m_substate = 1;
					}
				}
				return TinyFsmState.End();
			}
			case 5:
				this.SetMessageComeInOut(fsmEvent);
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0003A0C8 File Offset: 0x000382C8
		private ItemType GetOrbotCuebotItem()
		{
			ItemType itemType = ItemType.BARRIER;
			PlayerInformation playerInformation = GameObjectUtil.FindGameObjectComponentWithTag<PlayerInformation>("StageManager", "PlayerInformation");
			if (playerInformation != null)
			{
				bool flag = false;
				if (playerInformation.PhantomType == PhantomType.NONE)
				{
					itemType = ChaoState.ChaoAbilityItemTbl[UnityEngine.Random.Range(0, ChaoState.ChaoAbilityItemTbl.Length)];
					flag = this.CheckItemPresent(itemType);
				}
				if (!flag)
				{
					itemType = ChaoState.ChaoAbilityItemTblPhantom[UnityEngine.Random.Range(0, ChaoState.ChaoAbilityItemTblPhantom.Length)];
				}
			}
			return itemType;
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0003A138 File Offset: 0x00038338
		private TinyFsmState StateStockRing(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
			{
				if (this.m_modelControl != null)
				{
					this.m_modelControl.ChangeStateToReturnIdle();
				}
				GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "ef_ch_tornade_fog_(Clone)");
				if (gameObject != null)
				{
					UnityEngine.Object.Destroy(gameObject);
				}
				return TinyFsmState.End();
			}
			case 1:
				this.m_stateTimer = 2f;
				this.m_stateFlag.Reset();
				this.m_stockRingState = ChaoState.StockRingState.CHANGE_MOVEMENT;
				return TinyFsmState.End();
			case 4:
			{
				float getDeltaTime = fsmEvent.GetDeltaTime;
				this.m_stateTimer -= getDeltaTime;
				switch (this.m_stockRingState)
				{
				case ChaoState.StockRingState.CHANGE_MOVEMENT:
					this.ChangeMovement(MOVESTATE_ID.GoRingBanking);
					this.m_stockRingState = ChaoState.StockRingState.PLAY_SPIN_MOTION;
					break;
				case ChaoState.StockRingState.PLAY_SPIN_MOTION:
					if (this.m_modelControl != null)
					{
						this.m_modelControl.ChangeStateToSpin(ChaoState.StockRingModelSpinSpeed);
					}
					this.m_stockRingState = ChaoState.StockRingState.PLAY_EFECT;
					break;
				case ChaoState.StockRingState.PLAY_EFECT:
				{
					string text = "ef_ch_tornade_fog_";
					switch (ChaoState.GetRarity(this.m_chao_id))
					{
					case ChaoData.Rarity.NORMAL:
						text += "c01";
						break;
					case ChaoData.Rarity.RARE:
						text += "r01";
						break;
					case ChaoData.Rarity.SRARE:
						text += "sr01";
						ObjUtil.LightPlaySE("act_tornado_fly", "SE");
						break;
					}
					ObjUtil.PlayEffectChild(base.gameObject, text, Vector3.zero, Quaternion.identity, false);
					this.m_stockRingState = ChaoState.StockRingState.PLAY_EFFECT;
					break;
				}
				case ChaoState.StockRingState.PLAY_EFFECT:
					if (this.m_stateTimer < 1f && StageAbilityManager.Instance != null)
					{
						StageAbilityManager.Instance.RequestPlayChaoEffect(ChaoAbility.RECOVERY_RING, this.m_chao_type);
						this.m_stockRingState = ChaoState.StockRingState.WAIT_END;
					}
					break;
				case ChaoState.StockRingState.WAIT_END:
					if (this.m_stateTimer < 0f)
					{
						this.ChangeState(new TinyFsmState(new EventFunction(this.StateComeIn)));
						this.m_stockRingState = ChaoState.StockRingState.IDLE;
					}
					break;
				}
				return TinyFsmState.End();
			}
			case 5:
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0003A370 File Offset: 0x00038570
		private TinyFsmState StateDoubleRing(TinyFsmEvent fsmEvent)
		{
			int signal = fsmEvent.Signal;
			switch (signal + 4)
			{
			case 0:
				if (this.m_modelControl != null)
				{
					this.m_modelControl.ChangeStateToReturnIdle();
				}
				return TinyFsmState.End();
			case 1:
				this.m_stateTimer = 2.5f;
				this.m_stockRingState = ChaoState.StockRingState.CHANGE_MOVEMENT;
				return TinyFsmState.End();
			case 4:
			{
				float getDeltaTime = fsmEvent.GetDeltaTime;
				this.m_stateTimer -= getDeltaTime;
				switch (this.m_stockRingState)
				{
				case ChaoState.StockRingState.CHANGE_MOVEMENT:
					this.ChangeMovement(MOVESTATE_ID.GoRingBanking);
					this.m_stockRingState = ChaoState.StockRingState.PLAY_SPIN_MOTION;
					break;
				case ChaoState.StockRingState.PLAY_SPIN_MOTION:
					if (this.m_modelControl != null)
					{
						this.m_modelControl.ChangeStateToSpin(ChaoState.StockRingModelSpinSpeed);
					}
					this.m_stockRingState = ChaoState.StockRingState.PLAY_EFECT;
					break;
				case ChaoState.StockRingState.PLAY_EFECT:
					ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_up_bank_sr01", -1f);
					ObjUtil.LightPlaySE("act_chao_tornado2", "SE");
					this.m_stockRingState = ChaoState.StockRingState.WAIT_END;
					break;
				case ChaoState.StockRingState.WAIT_END:
					if (this.m_stateTimer < 0f)
					{
						if (this.m_stateFlag.Test(1))
						{
							this.ChangeState(new TinyFsmState(new EventFunction(this.StateStop)));
						}
						else
						{
							this.ChangeState(new TinyFsmState(new EventFunction(this.StateComeIn)));
						}
						this.m_stockRingState = ChaoState.StockRingState.IDLE;
					}
					break;
				}
				return TinyFsmState.End();
			}
			case 5:
				this.SetMessageComeInOut(fsmEvent);
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0003A50C File Offset: 0x0003870C
		private IEnumerator RingBank()
		{
			ObjUtil.PlayChaoEffect(base.gameObject, "ef_ch_combo_bank_r01", -1f);
			ObjUtil.LightPlaySE("act_chao_effect", "SE");
			int waite_frame = 1;
			while (waite_frame > 0)
			{
				waite_frame--;
				yield return null;
			}
			int stockRing = 0;
			if (StageAbilityManager.Instance != null)
			{
				stockRing = (int)StageAbilityManager.Instance.GetChaoAbilityExtraValue(ChaoAbility.COMBO_RING_BANK);
				if (StageScoreManager.Instance != null)
				{
					StageScoreManager.Instance.TransferRingForChaoAbility(stockRing);
				}
			}
			waite_frame = 2;
			while (waite_frame > 0)
			{
				waite_frame--;
				yield return null;
			}
			GameObjectUtil.SendMessageFindGameObject("HudCockpit", "OnAddStockRing", new MsgAddStockRing(stockRing), SendMessageOptions.DontRequireReceiver);
			yield break;
		}

		// Token: 0x040006CC RID: 1740
		private const float ShaderLineOffsetMain = 5f;

		// Token: 0x040006CD RID: 1741
		private const float ShaderLineOffsetSub = 15f;

		// Token: 0x040006CE RID: 1742
		private const string ShaderName = "ykChrLine_dme1";

		// Token: 0x040006CF RID: 1743
		private const string ChangeParamOutline = "_OutlineZOffset";

		// Token: 0x040006D0 RID: 1744
		private const string ChangeParamInnner = "_InnerZOffset";

		// Token: 0x040006D1 RID: 1745
		private const float InvalidExtremeSpeedRate = 1.5f;

		// Token: 0x040006D2 RID: 1746
		private const float EasySpeed_SpeedRate = 3f;

		// Token: 0x040006D3 RID: 1747
		private const float PhantomStartSpeedRate = 5f;

		// Token: 0x040006D4 RID: 1748
		private const string StockRingEffectName = "ef_ch_tornade_fog_";

		// Token: 0x040006D5 RID: 1749
		private const string StockRingEffectPost_SR = "sr01";

		// Token: 0x040006D6 RID: 1750
		private const string StockRingEffectPost_R = "r01";

		// Token: 0x040006D7 RID: 1751
		private const string StockRingEffectPost_N = "c01";

		// Token: 0x040006D8 RID: 1752
		private const int CHAOS_ID = 2014;

		// Token: 0x040006D9 RID: 1753
		private const int DEATH_EGG_ID = 2015;

		// Token: 0x040006DA RID: 1754
		private const int PUYO_CARBUNCLE_ID = 2012;

		// Token: 0x040006DB RID: 1755
		private const int PUYO_SUKETOUDARA_ID = 1018;

		// Token: 0x040006DC RID: 1756
		private const int PUYO_PAPURISU_ID = 24;

		// Token: 0x040006DD RID: 1757
		private const float ChaoWalkerSpeedRate = 2f;

		// Token: 0x040006DE RID: 1758
		private const float m_hardwareAndCartridgeMoveTimer = 0.8f;

		// Token: 0x040006DF RID: 1759
		private const float m_hardwareAndCartridgeWaitTimer = 2f;

		// Token: 0x040006E0 RID: 1760
		private const float HardwareCartridgeSpeedRate = 2f;

		// Token: 0x040006E1 RID: 1761
		private const float m_nightsMoveTimer = 1.5f;

		// Token: 0x040006E2 RID: 1762
		private const float m_nightsWaitTimer = 2f;

		// Token: 0x040006E3 RID: 1763
		private const float NightsSpeedRate = 2f;

		// Token: 0x040006E4 RID: 1764
		private const float ChaosMoveTimer = 1f;

		// Token: 0x040006E5 RID: 1765
		private const float ChaosWaitTimer = 2.2f;

		// Token: 0x040006E6 RID: 1766
		private const float ChaosSpeedRate = 2f;

		// Token: 0x040006E7 RID: 1767
		private const float PufferFishMoveTimer = 1f;

		// Token: 0x040006E8 RID: 1768
		private const float PufferFishWaitTimer = 0.5f;

		// Token: 0x040006E9 RID: 1769
		private const float PufferFishSubActionTimer = 1f;

		// Token: 0x040006EA RID: 1770
		private const float PufferFishOutTimer = 0.5f;

		// Token: 0x040006EB RID: 1771
		private const float PufferFishSpeedRate = 2.5f;

		// Token: 0x040006EC RID: 1772
		private const float ItemPresentSpeedRate = 2f;

		// Token: 0x040006ED RID: 1773
		private const float ItemPresentOutSpeedRate = 2f;

		// Token: 0x040006EE RID: 1774
		private Bitset32 m_stateFlag;

		// Token: 0x040006EF RID: 1775
		private TinyFsmBehavior m_fsmBehavior;

		// Token: 0x040006F0 RID: 1776
		private ChaoMovement m_movement;

		// Token: 0x040006F1 RID: 1777
		private ChaoType m_chao_type;

		// Token: 0x040006F2 RID: 1778
		private int m_chao_id = -1;

		// Token: 0x040006F3 RID: 1779
		private bool m_startEffect;

		// Token: 0x040006F4 RID: 1780
		private ChaoSetupParameterData m_setupdata = new ChaoSetupParameterData();

		// Token: 0x040006F5 RID: 1781
		private ChaoState.StateWork m_stateWork;

		// Token: 0x040006F6 RID: 1782
		private GameObject m_modelObject;

		// Token: 0x040006F7 RID: 1783
		private ChaoParameter m_parameter;

		// Token: 0x040006F8 RID: 1784
		private ChaoModelPostureController m_modelControl;

		// Token: 0x040006F9 RID: 1785
		private int m_attackCount;

		// Token: 0x040006FA RID: 1786
		private int m_substate;

		// Token: 0x040006FB RID: 1787
		private float m_stateTimer;

		// Token: 0x040006FC RID: 1788
		private ItemType m_chaoItemType = ItemType.UNKNOWN;

		// Token: 0x040006FD RID: 1789
		private static readonly Vector3 InvalidExtremeOffsetRate = new Vector3(0.5f, 0.7f, 0f);

		// Token: 0x040006FE RID: 1790
		private static readonly ItemType[] ChaoAbilityItemTbl = new ItemType[]
		{
			ItemType.COMBO,
			ItemType.MAGNET,
			ItemType.INVINCIBLE,
			ItemType.BARRIER,
			ItemType.TRAMPOLINE
		};

		// Token: 0x040006FF RID: 1791
		private static readonly ItemType[] ChaoAbilityItemTblPhantom = new ItemType[]
		{
			ItemType.COMBO,
			ItemType.MAGNET
		};

		// Token: 0x04000700 RID: 1792
		private static readonly ItemType[] ChaoAbilityPhantomTbl = new ItemType[]
		{
			ItemType.LASER,
			ItemType.DRILL,
			ItemType.ASTEROID
		};

		// Token: 0x04000701 RID: 1793
		private static readonly Vector3 AttackBossModelSpinSpeed = new Vector3(1440f, 0f, 0f);

		// Token: 0x04000702 RID: 1794
		private ChaoState.ChaoWalkerState m_chaoWalkerState;

		// Token: 0x04000703 RID: 1795
		private static readonly Vector3 ChaoWalkerOffsetRate = new Vector3(0.5f, 0.7f, 0f);

		// Token: 0x04000704 RID: 1796
		private static readonly Vector3 HardwareCartridgeOffsetRate = new Vector3(0.5f, 0.5f, 0f);

		// Token: 0x04000705 RID: 1797
		private static readonly Vector3 NightsOffsetRate = new Vector3(0.66f, 0.6f, 0f);

		// Token: 0x04000706 RID: 1798
		private static readonly Vector3 RealaOffsetRate = new Vector3(0.33f, 0.6f, 0f);

		// Token: 0x04000707 RID: 1799
		private static readonly Vector3 ChaosOffsetRate = new Vector3(0.5f, 0.5f, 0f);

		// Token: 0x04000708 RID: 1800
		private static readonly Vector3 PufferFishOffsetRate = new Vector3(0.5f, 0.75f, 0f);

		// Token: 0x04000709 RID: 1801
		private static readonly Vector3 ItemPresentOffsetRate = new Vector3(0.5f, 0.7f, 0f);

		// Token: 0x0400070A RID: 1802
		private static readonly Vector3 CuebotItemOffsetRate = new Vector3(0.5f, 0.6f, 0f);

		// Token: 0x0400070B RID: 1803
		private static readonly Vector3 OrbotItemtOffsetRate = new Vector3(0.5f, 0.78f, 0f);

		// Token: 0x0400070C RID: 1804
		private Camera m_uiCamera;

		// Token: 0x0400070D RID: 1805
		private GameObject m_effectObj;

		// Token: 0x0400070E RID: 1806
		private GameObject m_itemBtnObj;

		// Token: 0x0400070F RID: 1807
		private Vector3 m_effectScreenPos = Vector3.zero;

		// Token: 0x04000710 RID: 1808
		private Vector3 m_targetScreenPos = Vector3.zero;

		// Token: 0x04000711 RID: 1809
		private float m_distance;

		// Token: 0x04000712 RID: 1810
		private bool m_presentFlag;

		// Token: 0x04000713 RID: 1811
		private ChaoState.StockRingState m_stockRingState;

		// Token: 0x04000714 RID: 1812
		private static readonly Vector3 StockRingModelSpinSpeed = new Vector3(0f, 0f, 360f);

		// Token: 0x02000134 RID: 308
		private enum EventSignal
		{
			// Token: 0x04000716 RID: 1814
			ENTER_TRIGGER = 100
		}

		// Token: 0x02000135 RID: 309
		private abstract class StateWork
		{
			// Token: 0x17000177 RID: 375
			// (get) Token: 0x0600098C RID: 2444 RVA: 0x0003A530 File Offset: 0x00038730
			// (set) Token: 0x0600098D RID: 2445 RVA: 0x0003A538 File Offset: 0x00038738
			public string Name
			{
				get
				{
					return this.name;
				}
				set
				{
					this.name = value;
				}
			}

			// Token: 0x0600098E RID: 2446
			public abstract void Destroy();

			// Token: 0x04000717 RID: 1815
			private string name;
		}

		// Token: 0x02000136 RID: 310
		private enum FlagState
		{
			// Token: 0x04000719 RID: 1817
			StatesStop,
			// Token: 0x0400071A RID: 1818
			ReqestStop,
			// Token: 0x0400071B RID: 1819
			RreviveProduct
		}

		// Token: 0x02000137 RID: 311
		private enum SubStateKillOut
		{
			// Token: 0x0400071D RID: 1821
			Up,
			// Token: 0x0400071E RID: 1822
			Forward,
			// Token: 0x0400071F RID: 1823
			Wait
		}

		// Token: 0x02000138 RID: 312
		private enum AttackBossSubState
		{
			// Token: 0x04000721 RID: 1825
			Up,
			// Token: 0x04000722 RID: 1826
			Attack,
			// Token: 0x04000723 RID: 1827
			After
		}

		// Token: 0x02000139 RID: 313
		private class AttackBossWork : ChaoState.StateWork
		{
			// Token: 0x06000990 RID: 2448 RVA: 0x0003A54C File Offset: 0x0003874C
			public void DestroyAttackCollision()
			{
				if (this.m_attackCollision != null)
				{
					UnityEngine.Object.Destroy(this.m_attackCollision);
					this.m_attackCollision = null;
				}
			}

			// Token: 0x06000991 RID: 2449 RVA: 0x0003A574 File Offset: 0x00038774
			public override void Destroy()
			{
				this.DestroyAttackCollision();
				this.m_targetObject = null;
			}

			// Token: 0x04000724 RID: 1828
			public GameObject m_attackCollision;

			// Token: 0x04000725 RID: 1829
			public GameObject m_targetObject;
		}

		// Token: 0x0200013A RID: 314
		private enum ChaoWalkerState
		{
			// Token: 0x04000727 RID: 1831
			Action,
			// Token: 0x04000728 RID: 1832
			Wait,
			// Token: 0x04000729 RID: 1833
			SubAction,
			// Token: 0x0400072A RID: 1834
			Out
		}

		// Token: 0x0200013B RID: 315
		private enum SubStateItemPresent
		{
			// Token: 0x0400072C RID: 1836
			Action,
			// Token: 0x0400072D RID: 1837
			Wait,
			// Token: 0x0400072E RID: 1838
			SubAction,
			// Token: 0x0400072F RID: 1839
			Out
		}

		// Token: 0x0200013C RID: 316
		private enum StockRingState
		{
			// Token: 0x04000731 RID: 1841
			IDLE,
			// Token: 0x04000732 RID: 1842
			CHANGE_MOVEMENT,
			// Token: 0x04000733 RID: 1843
			PLAY_SPIN_MOTION,
			// Token: 0x04000734 RID: 1844
			PLAY_EFECT,
			// Token: 0x04000735 RID: 1845
			PLAY_EFFECT,
			// Token: 0x04000736 RID: 1846
			WAIT_END
		}
	}
}
