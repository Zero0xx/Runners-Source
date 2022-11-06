using System;
using Message;
using UnityEngine;

namespace Player
{
	// Token: 0x020009C3 RID: 2499
	public class StateUtil
	{
		// Token: 0x06004144 RID: 16708 RVA: 0x00153DBC File Offset: 0x00151FBC
		public static bool IsAnimationEnd(CharacterState context)
		{
			return context.GetAnimator().GetCurrentAnimatorStateInfo(0).normalizedTime > 0.98f;
		}

		// Token: 0x06004145 RID: 16709 RVA: 0x00153DEC File Offset: 0x00151FEC
		public static bool IsAnimationEnd(CharacterState context, string animName)
		{
			if (StateUtil.IsAnimationInTransition(context))
			{
				return false;
			}
			string name = "Base Layer." + animName;
			AnimatorStateInfo currentAnimatorStateInfo = context.GetAnimator().GetCurrentAnimatorStateInfo(0);
			return currentAnimatorStateInfo.IsName(name) && currentAnimatorStateInfo.normalizedTime > 0.98f;
		}

		// Token: 0x06004146 RID: 16710 RVA: 0x00153E40 File Offset: 0x00152040
		public static bool IsAnimationInTransition(CharacterState context)
		{
			return context.GetAnimator().IsInTransition(0);
		}

		// Token: 0x06004147 RID: 16711 RVA: 0x00153E50 File Offset: 0x00152050
		public static bool IsCurrentAnimation(CharacterState context, string animName, bool notTransition)
		{
			if (notTransition && StateUtil.IsAnimationInTransition(context))
			{
				return false;
			}
			string name = "Base Layer." + animName;
			return context.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName(name);
		}

		// Token: 0x06004148 RID: 16712 RVA: 0x00153E9C File Offset: 0x0015209C
		public static bool SetRunningAnimationSpeed(CharacterState context, ref float animationSpeed)
		{
			float spinDashSpeed = context.Parameter.m_spinDashSpeed;
			float magnitude = context.Movement.HorzVelocity.magnitude;
			bool result = false;
			float num;
			if (magnitude > spinDashSpeed)
			{
				num = 1f;
				result = true;
			}
			else
			{
				num = Mathf.Clamp(magnitude / spinDashSpeed, 0f, 1f) * 0.9f;
			}
			context.GetAnimator().SetFloat("RunSpeed", num);
			animationSpeed = num;
			return result;
		}

		// Token: 0x06004149 RID: 16713 RVA: 0x00153F14 File Offset: 0x00152114
		public static GameObject CreateEffect(MonoBehaviour context, string effectname, bool recreate)
		{
			return StateUtil.CreateEffect(context, context.gameObject, effectname, recreate, ResourceCategory.COMMON_EFFECT);
		}

		// Token: 0x0600414A RID: 16714 RVA: 0x00153F28 File Offset: 0x00152128
		public static GameObject CreateEffect(MonoBehaviour context, string effectname, bool recreate, ResourceCategory category)
		{
			return StateUtil.CreateEffect(context, context.gameObject, effectname, recreate, category);
		}

		// Token: 0x0600414B RID: 16715 RVA: 0x00153F3C File Offset: 0x0015213C
		public static GameObject CreateEffect(MonoBehaviour context, GameObject parentObject, string effectname, bool recreate, ResourceCategory category)
		{
			if (parentObject == null)
			{
				parentObject = context.gameObject;
			}
			if (!recreate)
			{
				GameObject gameObject = GameObjectUtil.FindChildGameObject(context.gameObject, effectname);
				if (gameObject != null)
				{
					gameObject.SetActive(true);
					return gameObject;
				}
			}
			if (ResourceManager.Instance)
			{
				GameObject gameObject2 = ResourceManager.Instance.GetGameObject(category, effectname);
				if (gameObject2)
				{
					GameObject gameObject3 = UnityEngine.Object.Instantiate(gameObject2, parentObject.transform.position, parentObject.transform.rotation) as GameObject;
					if (gameObject3 != null)
					{
						gameObject3.name = effectname;
						gameObject3.SetActive(true);
						gameObject3.transform.parent = parentObject.transform;
						return gameObject3;
					}
				}
			}
			return null;
		}

		// Token: 0x0600414C RID: 16716 RVA: 0x00154000 File Offset: 0x00152200
		public static GameObject CreateEffectOnTransform(MonoBehaviour context, Transform transform, string effectname, bool recreate)
		{
			return StateUtil.CreateEffectOnTransform(context, transform, effectname, recreate, ResourceCategory.COMMON_EFFECT);
		}

		// Token: 0x0600414D RID: 16717 RVA: 0x0015400C File Offset: 0x0015220C
		public static GameObject CreateEffectOnTransform(MonoBehaviour context, Transform transform, string effectname, bool recreate, ResourceCategory category)
		{
			if (!recreate)
			{
				GameObject gameObject = GameObjectUtil.FindChildGameObject(context.gameObject, effectname);
				if (gameObject != null)
				{
					gameObject.SetActive(true);
					return gameObject;
				}
			}
			if (ResourceManager.Instance)
			{
				GameObject gameObject2 = ResourceManager.Instance.GetGameObject(category, effectname);
				if (gameObject2)
				{
					GameObject gameObject3 = UnityEngine.Object.Instantiate(gameObject2, transform.position, transform.rotation) as GameObject;
					if (gameObject3 != null)
					{
						gameObject3.name = effectname;
						gameObject3.SetActive(true);
						gameObject3.transform.parent = context.transform;
						return gameObject3;
					}
				}
			}
			return null;
		}

		// Token: 0x0600414E RID: 16718 RVA: 0x001540B0 File Offset: 0x001522B0
		public static GameObject CreateEffect(MonoBehaviour context, Vector3 position, Quaternion rotation, string effectname, bool recreate)
		{
			return StateUtil.CreateEffect(context, position, rotation, effectname, recreate, ResourceCategory.COMMON_EFFECT);
		}

		// Token: 0x0600414F RID: 16719 RVA: 0x001540C0 File Offset: 0x001522C0
		public static GameObject CreateEffect(MonoBehaviour context, Vector3 position, Quaternion rotation, string effectname, bool recreate, ResourceCategory category)
		{
			if (!recreate)
			{
				GameObject gameObject = GameObjectUtil.FindChildGameObject(context.gameObject, effectname);
				if (gameObject != null)
				{
					gameObject.SetActive(true);
					return gameObject;
				}
			}
			if (ResourceManager.Instance)
			{
				GameObject gameObject2 = ResourceManager.Instance.GetGameObject(category, effectname);
				if (gameObject2)
				{
					GameObject gameObject3 = UnityEngine.Object.Instantiate(gameObject2, position, rotation) as GameObject;
					if (gameObject3 != null)
					{
						gameObject3.name = effectname;
						gameObject3.SetActive(true);
						return gameObject3;
					}
				}
			}
			return null;
		}

		// Token: 0x06004150 RID: 16720 RVA: 0x0015414C File Offset: 0x0015234C
		public static void DestroyEffect(MonoBehaviour context, string effectname)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(context.gameObject, effectname);
			if (gameObject != null)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}

		// Token: 0x06004151 RID: 16721 RVA: 0x00154178 File Offset: 0x00152378
		public static void StopEffect(MonoBehaviour context, string effectname, float destroyTime)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(context.gameObject, effectname);
			if (gameObject != null)
			{
				StateUtil.DestroyParticle(gameObject, destroyTime);
			}
		}

		// Token: 0x06004152 RID: 16722 RVA: 0x001541A8 File Offset: 0x001523A8
		public static void SetActiveEffect(MonoBehaviour context, string effectname, bool value)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(context.gameObject, effectname);
			if (gameObject != null)
			{
				gameObject.SetActive(value);
			}
		}

		// Token: 0x06004153 RID: 16723 RVA: 0x001541D8 File Offset: 0x001523D8
		public static void CreateJumpEffect(MonoBehaviour context)
		{
			if (StageEffectManager.Instance != null)
			{
				StageEffectManager.Instance.PlayEffect(EffectPlayType.JUMP, context.transform.position, context.transform.rotation);
			}
		}

		// Token: 0x06004154 RID: 16724 RVA: 0x00154218 File Offset: 0x00152418
		public static void CreateLandingEffect(CharacterState context)
		{
			if (StageEffectManager.Instance != null)
			{
				StageEffectManager.Instance.PlayEffect(EffectPlayType.LAND, context.transform.position, context.transform.rotation);
			}
		}

		// Token: 0x06004155 RID: 16725 RVA: 0x00154258 File Offset: 0x00152458
		public static void CreateRunEffect(CharacterState context, float animSpeed)
		{
			if (context.GetLevelInformation().LightMode && context.BossBoostLevel == WispBoostLevel.NONE)
			{
				return;
			}
			if (context.BossBoostLevel != WispBoostLevel.NONE)
			{
				ResourceCategory category = ResourceCategory.EVENT_RESOURCE;
				string bossBoostEffect = context.BossBoostEffect;
				StateUtil.CreateEffect(context, context.transform.position, context.transform.rotation, bossBoostEffect, true, category);
			}
			else if (StageEffectManager.Instance != null)
			{
				StageEffectManager.Instance.PlayEffect((animSpeed < 0.6f) ? EffectPlayType.RUN : EffectPlayType.FAST_RUN, context.transform.position, context.transform.rotation);
			}
		}

		// Token: 0x06004156 RID: 16726 RVA: 0x00154304 File Offset: 0x00152504
		public static void CreateStartBoostEffect(CharacterState context)
		{
			if (context.CharacterName != null)
			{
				string effectname = "ef_pl_" + context.CharacterName + "_boost_st01";
				StateUtil.CreateEffect(context, context.transform.position, context.transform.rotation, effectname, true, ResourceCategory.CHARA_EFFECT);
			}
		}

		// Token: 0x06004157 RID: 16727 RVA: 0x00154354 File Offset: 0x00152554
		public static void Create2ndJumpEffect(CharacterState context)
		{
			CharaSEUtil.Play2ndJumpSE(context.charaType);
			string effectname = "ef_pl_" + context.CharacterName + "_2stepjump01";
			GameObject gameobj = StateUtil.CreateEffect(context, effectname, true, ResourceCategory.CHARA_EFFECT);
			StateUtil.SetObjectLocalPositionToCenter(context, gameobj);
		}

		// Token: 0x06004158 RID: 16728 RVA: 0x00154394 File Offset: 0x00152594
		public static void CheckAndCreateRunEffect(CharacterState context, ref float effectTimer, float speed, float animationSpeed, float deltaTime)
		{
			effectTimer -= speed * deltaTime;
			if (effectTimer < 0f)
			{
				StateUtil.CreateRunEffect(context, animationSpeed);
				effectTimer = 2f;
			}
		}

		// Token: 0x06004159 RID: 16729 RVA: 0x001543BC File Offset: 0x001525BC
		public static void DestroyParticle(GameObject effect, float destroyTime)
		{
			if (effect != null)
			{
				foreach (ParticleSystem particleSystem in effect.transform.GetComponentsInChildren<ParticleSystem>())
				{
					particleSystem.Stop();
				}
				UnityEngine.Object.Destroy(effect, destroyTime);
			}
		}

		// Token: 0x0600415A RID: 16730 RVA: 0x00154408 File Offset: 0x00152608
		public static void StopParticle(GameObject effect)
		{
			if (effect != null)
			{
				foreach (ParticleSystem particleSystem in effect.transform.GetComponentsInChildren<ParticleSystem>())
				{
					particleSystem.Stop();
				}
			}
		}

		// Token: 0x0600415B RID: 16731 RVA: 0x0015444C File Offset: 0x0015264C
		public static void PlayParticle(GameObject effect)
		{
			if (effect != null)
			{
				foreach (ParticleSystem particleSystem in effect.transform.GetComponentsInChildren<ParticleSystem>())
				{
					particleSystem.Play();
				}
			}
		}

		// Token: 0x0600415C RID: 16732 RVA: 0x00154490 File Offset: 0x00152690
		public static bool ChangeAfterSpinattack(CharacterState context, int messageId, MessageBase msg)
		{
			if (messageId != 16385)
			{
				if (messageId != 16386)
				{
					return false;
				}
				context.ChangeState(STATE_ID.AfterSpinAttack);
				return true;
			}
			else
			{
				if (StateUtil.IsThroughBreakable(context))
				{
					return true;
				}
				if (!context.TestStatus(Status.InvincibleByChao))
				{
					context.ChangeState(STATE_ID.AfterSpinAttack);
				}
				return true;
			}
		}

		// Token: 0x0600415D RID: 16733 RVA: 0x001544E8 File Offset: 0x001526E8
		public static void SendMessageToTerminateItem(ItemType item)
		{
			if (StageItemManager.Instance != null)
			{
				StageItemManager.Instance.OnTerminateItem(new MsgTerminateItem(item));
			}
		}

		// Token: 0x0600415E RID: 16734 RVA: 0x00154518 File Offset: 0x00152718
		public static MsgBossInfo GetBossInfo(GameObject bossObject)
		{
			MsgBossInfo msgBossInfo = new MsgBossInfo();
			if (bossObject == null)
			{
				GameObjectUtil.SendMessageToTagObjects("Boss", "OnMsgBossInfo", msgBossInfo, SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				bossObject.SendMessage("OnMsgBossInfo", msgBossInfo);
			}
			return msgBossInfo;
		}

		// Token: 0x0600415F RID: 16735 RVA: 0x0015455C File Offset: 0x0015275C
		public static void SetAirMovementToRotateGround(CharacterState context, bool value)
		{
			CharacterMovement movement = context.Movement;
			if (movement)
			{
				CharacterMoveAir currentState = movement.GetCurrentState<CharacterMoveAir>();
				if (currentState != null)
				{
					currentState.SetRotateToGround(value);
				}
			}
		}

		// Token: 0x06004160 RID: 16736 RVA: 0x00154590 File Offset: 0x00152790
		public static void SetTargetMovement(CharacterState context, Vector3 position, Quaternion rotation, float time)
		{
			CharacterMovement movement = context.Movement;
			if (movement)
			{
				CharacterMoveTarget currentState = movement.GetCurrentState<CharacterMoveTarget>();
				if (currentState != null)
				{
					currentState.SetTarget(context.Movement, position, rotation, time);
				}
			}
		}

		// Token: 0x06004161 RID: 16737 RVA: 0x001545CC File Offset: 0x001527CC
		public static void ThroughBreakable(CharacterState context, bool value)
		{
			CharacterMovement movement = context.Movement;
			if (movement)
			{
				movement.ThroughBreakable = value;
			}
		}

		// Token: 0x06004162 RID: 16738 RVA: 0x001545F4 File Offset: 0x001527F4
		public static bool IsThroughBreakable(CharacterState context)
		{
			CharacterMovement movement = context.Movement;
			return movement && movement.ThroughBreakable;
		}

		// Token: 0x06004163 RID: 16739 RVA: 0x0015461C File Offset: 0x0015281C
		public static void ActiveMagnetObject(CharacterState context, float time)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(context.gameObject, "CharacterMagnet");
			if (gameObject != null)
			{
				CharacterMagnet component = gameObject.GetComponent<CharacterMagnet>();
				if (component != null)
				{
					component.SetEnable();
					component.SetTime(time);
					component.SetDefaultRadiusAndOffset();
				}
			}
		}

		// Token: 0x06004164 RID: 16740 RVA: 0x0015466C File Offset: 0x0015286C
		public static void DeactiveMagetObject(CharacterState context)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(context.gameObject, "CharacterMagnet");
			if (gameObject != null && gameObject.activeInHierarchy)
			{
				CharacterMagnet component = gameObject.GetComponent<CharacterMagnet>();
				if (component != null)
				{
					component.SetDisable();
				}
			}
		}

		// Token: 0x06004165 RID: 16741 RVA: 0x001546BC File Offset: 0x001528BC
		public static void ActiveBarrier(CharacterState context)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(context.gameObject, "CharacterBarrier");
			if (gameObject != null)
			{
				CharacterBarrier component = gameObject.GetComponent<CharacterBarrier>();
				component.SetEnable();
			}
		}

		// Token: 0x06004166 RID: 16742 RVA: 0x001546F4 File Offset: 0x001528F4
		public static void DeactiveBarrier(CharacterState context)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(context.gameObject, "CharacterBarrier");
			if (gameObject != null && gameObject.activeSelf)
			{
				CharacterBarrier component = gameObject.GetComponent<CharacterBarrier>();
				component.SetDisable();
			}
		}

		// Token: 0x06004167 RID: 16743 RVA: 0x00154738 File Offset: 0x00152938
		public static bool IsBarrierActive(CharacterState context)
		{
			return StateUtil.IsPartsActive(context, "CharacterBarrier");
		}

		// Token: 0x06004168 RID: 16744 RVA: 0x00154748 File Offset: 0x00152948
		public static CharacterBarrier GetBarrier(CharacterState context)
		{
			return StateUtil.GetPartsComponent<CharacterBarrier>(context, "CharacterBarrier");
		}

		// Token: 0x06004169 RID: 16745 RVA: 0x00154758 File Offset: 0x00152958
		public static void ActiveInvincible(CharacterState context, float time)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(context.gameObject, "CharacterInvincible");
			if (gameObject != null)
			{
				CharacterInvincible component = gameObject.GetComponent<CharacterInvincible>();
				if (component != null)
				{
					component.SetActive(time);
				}
			}
		}

		// Token: 0x0600416A RID: 16746 RVA: 0x0015479C File Offset: 0x0015299C
		public static void DeactiveInvincible(CharacterState context)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(context.gameObject, "CharacterInvincible");
			if (gameObject != null && gameObject.activeInHierarchy)
			{
				CharacterInvincible component = gameObject.GetComponent<CharacterInvincible>();
				if (component != null)
				{
					component.SetDisable();
				}
			}
		}

		// Token: 0x0600416B RID: 16747 RVA: 0x001547EC File Offset: 0x001529EC
		public static bool IsInvincibleActive(CharacterState context)
		{
			return StateUtil.IsPartsActive(context, "CharacterInvincible") || context.TestStatus(Status.InvincibleByChao);
		}

		// Token: 0x0600416C RID: 16748 RVA: 0x00154808 File Offset: 0x00152A08
		private static bool IsPartsActive(CharacterState context, string name)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(context.gameObject, name);
			return gameObject != null && gameObject.activeInHierarchy;
		}

		// Token: 0x0600416D RID: 16749 RVA: 0x00154838 File Offset: 0x00152A38
		public static void ActiveCombo(CharacterState context, float time)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(context.gameObject, "CharacterCombo");
			if (gameObject != null)
			{
				CharacterCombo component = gameObject.GetComponent<CharacterCombo>();
				if (component != null)
				{
					component.SetEnable();
					component.SetTime(time);
				}
			}
		}

		// Token: 0x0600416E RID: 16750 RVA: 0x00154884 File Offset: 0x00152A84
		public static void DeactiveCombo(CharacterState context, bool immediate)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(context.gameObject, "CharacterCombo");
			if (gameObject != null && gameObject.activeInHierarchy)
			{
				CharacterCombo component = gameObject.GetComponent<CharacterCombo>();
				if (component != null)
				{
					if (immediate)
					{
						component.SetDisable();
					}
					else
					{
						component.RequestEnd();
					}
				}
			}
		}

		// Token: 0x0600416F RID: 16751 RVA: 0x001548E4 File Offset: 0x00152AE4
		public static void ActiveTrampoline(CharacterState context, float time)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(context.gameObject, "CharacterTrampoline");
			if (gameObject != null)
			{
				CharacterTrampoline component = gameObject.GetComponent<CharacterTrampoline>();
				if (component != null)
				{
					component.SetEnable();
					component.SetTime(time);
				}
			}
		}

		// Token: 0x06004170 RID: 16752 RVA: 0x00154930 File Offset: 0x00152B30
		public static void DeactiveTrampoline(CharacterState context)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(context.gameObject, "CharacterTrampoline");
			if (gameObject != null && gameObject.activeInHierarchy)
			{
				CharacterTrampoline component = gameObject.GetComponent<CharacterTrampoline>();
				if (component != null)
				{
					component.SetDisable();
				}
			}
		}

		// Token: 0x06004171 RID: 16753 RVA: 0x00154980 File Offset: 0x00152B80
		public static T GetPartsComponent<T>(CharacterState context, string componentName) where T : Component
		{
			T result = GameObjectUtil.FindChildGameObjectComponent<T>(context.gameObject, componentName);
			if (result.gameObject.activeInHierarchy)
			{
				return result;
			}
			return (T)((object)null);
		}

		// Token: 0x06004172 RID: 16754 RVA: 0x001549BC File Offset: 0x00152BBC
		public static T GetPartsComponentAlways<T>(CharacterState context, string componentName) where T : Component
		{
			return GameObjectUtil.FindChildGameObjectComponent<T>(context.gameObject, componentName);
		}

		// Token: 0x06004173 RID: 16755 RVA: 0x001549D8 File Offset: 0x00152BD8
		public static Vector3 GetBodyCenterPosition(MonoBehaviour context)
		{
			CapsuleCollider component;
			if (context.transform.parent == null || context.gameObject.tag == "Player")
			{
				component = context.gameObject.GetComponent<CapsuleCollider>();
			}
			else
			{
				component = context.gameObject.transform.parent.GetComponent<CapsuleCollider>();
			}
			if (component != null)
			{
				return component.center;
			}
			return Vector3.zero;
		}

		// Token: 0x06004174 RID: 16756 RVA: 0x00154A58 File Offset: 0x00152C58
		public static void SetObjectLocalPositionToCenter(MonoBehaviour context, GameObject gameobj)
		{
			Vector3 bodyCenterPosition = StateUtil.GetBodyCenterPosition(context);
			if (gameobj != null)
			{
				gameobj.transform.localPosition = bodyCenterPosition;
			}
		}

		// Token: 0x06004175 RID: 16757 RVA: 0x00154A84 File Offset: 0x00152C84
		public static void SetShadowActive(CharacterState context, bool value)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(context.gameObject, "ShadowProjector");
			if (gameObject != null)
			{
				gameObject.SetActive(value);
			}
		}

		// Token: 0x06004176 RID: 16758 RVA: 0x00154AB8 File Offset: 0x00152CB8
		public static void SetNotDrawBarrierEffect(CharacterState context, bool value)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(context.gameObject, "CharacterBarrier");
			if (gameObject != null && gameObject.activeInHierarchy)
			{
				CharacterBarrier component = gameObject.GetComponent<CharacterBarrier>();
				component.SetNotDraw(value);
			}
		}

		// Token: 0x06004177 RID: 16759 RVA: 0x00154AFC File Offset: 0x00152CFC
		public static void SetNotDrawItemEffect(CharacterState context, bool value)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(context.gameObject, "CharacterInvincible");
			if (gameObject != null && gameObject.activeInHierarchy)
			{
				CharacterInvincible component = gameObject.GetComponent<CharacterInvincible>();
				component.SetNotDraw(value);
			}
			StateUtil.SetNotDrawBarrierEffect(context, value);
		}

		// Token: 0x06004178 RID: 16760 RVA: 0x00154B48 File Offset: 0x00152D48
		public static void ActiveChaoAbilityMagnetObject(CharacterState context)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(context.gameObject, "CharacterMagnetChaoAbility");
			if (gameObject != null)
			{
				CharacterMagnet component = gameObject.GetComponent<CharacterMagnet>();
				if (component != null)
				{
					component.SetEnable();
					component.SetDefaultRadiusAndOffset();
				}
			}
		}

		// Token: 0x06004179 RID: 16761 RVA: 0x00154B94 File Offset: 0x00152D94
		public static void DeactiveChaoAbilityMagetObject(CharacterState context)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(context.gameObject, "CharacterMagnetChaoAbility");
			if (gameObject != null && gameObject.activeInHierarchy)
			{
				CharacterMagnet component = gameObject.GetComponent<CharacterMagnet>();
				if (component != null)
				{
					component.SetDisable();
				}
			}
		}

		// Token: 0x0600417A RID: 16762 RVA: 0x00154BE4 File Offset: 0x00152DE4
		public static void SetChangePhantomCancel(CharacterState context, ItemType itemType)
		{
			context.SetChangePhantomCancel(itemType);
		}

		// Token: 0x0600417B RID: 16763 RVA: 0x00154BF0 File Offset: 0x00152DF0
		public static PathEvaluator GetStagePathEvaluator(CharacterState context, BlockPathController.PathType patytype)
		{
			StageBlockPathManager stagePathManager = context.GetStagePathManager();
			if (stagePathManager != null)
			{
				PathEvaluator curentPathEvaluator = stagePathManager.GetCurentPathEvaluator(patytype);
				if (curentPathEvaluator != null)
				{
					return curentPathEvaluator;
				}
			}
			return null;
		}

		// Token: 0x0600417C RID: 16764 RVA: 0x00154C24 File Offset: 0x00152E24
		public static float GetForwardSpeedAir(CharacterState context, float targetSpeed, float deltaTime)
		{
			float num = context.Movement.GetForwardVelocityScalar();
			if (num < targetSpeed)
			{
				num = Mathf.Max(num + context.Parameter.m_airForwardAccel * deltaTime, targetSpeed);
			}
			else
			{
				num = Mathf.Min(num - context.Parameter.m_airForwardAccel * deltaTime, targetSpeed);
			}
			return num;
		}

		// Token: 0x0600417D RID: 16765 RVA: 0x00154C78 File Offset: 0x00152E78
		public static void ResetVelocity(CharacterState context)
		{
			context.Movement.Velocity = Vector3.zero;
		}

		// Token: 0x0600417E RID: 16766 RVA: 0x00154C8C File Offset: 0x00152E8C
		public static void SetVelocityForwardRun(CharacterState context, bool setHorz)
		{
			Vector3 vector = context.Movement.GetForwardDir() * context.DefaultSpeed;
			if (setHorz)
			{
				context.Movement.HorzVelocity = vector;
			}
			else
			{
				context.Movement.Velocity = vector;
			}
		}

		// Token: 0x0600417F RID: 16767 RVA: 0x00154CD4 File Offset: 0x00152ED4
		public static void SetRotation(CharacterState context, Vector3 up)
		{
			context.transform.rotation = Quaternion.FromToRotation(context.transform.up, up) * context.transform.rotation;
		}

		// Token: 0x06004180 RID: 16768 RVA: 0x00154D10 File Offset: 0x00152F10
		public static void SetRotation(CharacterState context, Vector3 up, Vector3 front)
		{
			Quaternion identity = Quaternion.identity;
			identity.SetLookRotation(front, up);
			context.transform.rotation = identity;
		}

		// Token: 0x06004181 RID: 16769 RVA: 0x00154D38 File Offset: 0x00152F38
		public static void SetRotateOnGround(CharacterState context)
		{
			CharacterMovement component = context.GetComponent<CharacterMovement>();
			HitInfo hitInfo;
			if (component.GetGroundInfo(out hitInfo))
			{
				MovementUtil.RotateByCollision(context.transform, context.GetComponent<CapsuleCollider>(), hitInfo.info.normal);
			}
		}

		// Token: 0x06004182 RID: 16770 RVA: 0x00154D78 File Offset: 0x00152F78
		public static void SetRotationOnGravityUp(CharacterState context)
		{
			MovementUtil.RotateByCollision(context.transform, context.GetComponent<CapsuleCollider>(), -context.Movement.GetGravityDir());
		}

		// Token: 0x06004183 RID: 16771 RVA: 0x00154DA8 File Offset: 0x00152FA8
		public static void GetBaseGroundPosition(CharacterState context, ref Vector3 pos)
		{
			Vector3 worldPosition = new Vector3(pos.x, 0f, pos.z);
			StageBlockPathManager stagePathManager = context.GetStagePathManager();
			if (stagePathManager != null)
			{
				PathEvaluator curentPathEvaluator = stagePathManager.GetCurentPathEvaluator(BlockPathController.PathType.SV);
				if (curentPathEvaluator != null)
				{
					worldPosition = curentPathEvaluator.GetWorldPosition();
				}
			}
			pos = worldPosition;
		}

		// Token: 0x06004184 RID: 16772 RVA: 0x00154DFC File Offset: 0x00152FFC
		public static bool CheckOverlap(CapsuleCollider collider, Vector3 pos, Vector3 upDir, int layerMask)
		{
			float d = collider.height * 0.5f - collider.radius;
			float num = 0.01f;
			float radius = collider.radius - num;
			Vector3 a = pos + upDir * collider.height * 0.5f;
			Vector3 start = a - upDir * d;
			Vector3 end = a + upDir * d;
			return Physics.CheckCapsule(start, end, radius, layerMask);
		}

		// Token: 0x06004185 RID: 16773 RVA: 0x00154E7C File Offset: 0x0015307C
		public static bool CheckOverlapSphere(Vector3 pos, Vector3 upDir, float radius, int layerMask)
		{
			Vector3 position = pos + upDir * radius;
			return Physics.CheckSphere(position, radius, layerMask);
		}

		// Token: 0x06004186 RID: 16774 RVA: 0x00154EA8 File Offset: 0x001530A8
		public static bool CapsuleCast(CapsuleCollider collider, Vector3 pos, Vector3 upDir, int layerMask, Vector3 direction, float distance, ref Vector3 outPos, bool noskin)
		{
			float d = collider.height * 0.5f - collider.radius;
			float num = (!noskin) ? 0.01f : 0f;
			float radius = collider.radius - num;
			Vector3 a = pos + upDir * collider.height * 0.5f;
			Vector3 point = a - upDir * d;
			Vector3 point2 = a + upDir * d;
			RaycastHit raycastHit;
			if (Physics.CapsuleCast(point, point2, radius, direction, out raycastHit, distance, layerMask))
			{
				num = 0.01f;
				Vector3 vector = pos + direction * (raycastHit.distance - num);
				outPos = vector;
				return true;
			}
			outPos = pos + direction * distance;
			return false;
		}

		// Token: 0x06004187 RID: 16775 RVA: 0x00154F7C File Offset: 0x0015317C
		public static void EnableChildObject(CharacterState context, string name, bool value)
		{
			if (name == null)
			{
				return;
			}
			GameObject gameObject = GameObjectUtil.FindChildGameObject(context.gameObject, name);
			if (gameObject != null)
			{
				gameObject.SetActive(value);
			}
		}

		// Token: 0x06004188 RID: 16776 RVA: 0x00154FB0 File Offset: 0x001531B0
		public static void EnablePlayerCollision(CharacterState context, bool value)
		{
			CapsuleCollider component = context.GetComponent<CapsuleCollider>();
			if (component == null)
			{
				return;
			}
			component.enabled = value;
		}

		// Token: 0x06004189 RID: 16777 RVA: 0x00154FD8 File Offset: 0x001531D8
		public static bool CheckDeadByHitWall(CharacterState context, float deltaTime)
		{
			HitInfo hitInfo;
			if (context.Movement.GetHitInfo(CharacterMovement.HitType.Front, out hitInfo))
			{
				float num = Vector3.Dot(context.Movement.GetDisplacement(), context.Movement.GetForwardDir());
				float vertVelocityScalar = context.Movement.GetVertVelocityScalar();
				float num2 = (!context.Movement.IsOnGround()) ? context.Parameter.m_hitWallUpSpeedAir : context.Parameter.m_hitWallUpSpeedGround;
				float num3 = Vector3.Dot(context.Movement.GetForwardDir(), hitInfo.info.normal);
				if (num3 < -0.94f && num < 0.5f * deltaTime && vertVelocityScalar < num2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600418A RID: 16778 RVA: 0x00155090 File Offset: 0x00153290
		public static bool CheckHitWallAndGoDeadOrStumble(CharacterState context, float deltaTime, ref STATE_ID state)
		{
			if (!StateUtil.CheckDeadByHitWall(context, deltaTime))
			{
				return false;
			}
			Vector3 origin = context.Position + -context.Movement.GetGravityDir() * context.Parameter.m_goStumbleMaxHeight;
			Vector3 baseFrontTangent = CharacterDefs.BaseFrontTangent;
			int layerMask = 1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Terrain");
			Ray ray = new Ray(origin, baseFrontTangent);
			RaycastHit raycastHit;
			if (!Physics.Raycast(ray, out raycastHit, 1.5f, layerMask))
			{
				state = STATE_ID.Stumble;
				return true;
			}
			StateUtil.AddHitWallTimer(context, deltaTime);
			if (context.m_hitWallTimer > context.Parameter.m_hitWallDeadTime)
			{
				state = STATE_ID.Dead;
				return true;
			}
			return false;
		}

		// Token: 0x0600418B RID: 16779 RVA: 0x00155144 File Offset: 0x00153344
		private static bool IsPrecedeInputTouch(CharacterState context, float precedeSec)
		{
			return context.m_input.IsTouchedLastSecond(precedeSec);
		}

		// Token: 0x0600418C RID: 16780 RVA: 0x00155154 File Offset: 0x00153354
		public static bool ChangeToJumpStateIfPrecedeInputTouch(CharacterState context, float precedeSec, bool rotateOnGround)
		{
			if (StateUtil.IsPrecedeInputTouch(context, precedeSec))
			{
				context.ClearAirAction();
				StateUtil.NowLanding(context, rotateOnGround);
				context.ChangeState(STATE_ID.Jump);
				return true;
			}
			return false;
		}

		// Token: 0x0600418D RID: 16781 RVA: 0x0015517C File Offset: 0x0015337C
		public static void ChangeStateToChangePhantom(CharacterState context, PhantomType phantom, float time)
		{
			MsgChaoStateUtil.SendMsgChaoState(MsgChaoState.State.STOP);
			context.NowPhantomType = phantom;
			ChangePhantomParameter changePhantomParameter = context.CreateEnteringParameter<ChangePhantomParameter>();
			changePhantomParameter.Set(phantom, time);
			context.ChangeState(STATE_ID.ChangePhantom);
		}

		// Token: 0x0600418E RID: 16782 RVA: 0x001551B0 File Offset: 0x001533B0
		public static void ReturnFromPhantomAndChangeState(CharacterState context, PhantomType nowPhantom, bool hitBoss)
		{
			context.NowPhantomType = PhantomType.NONE;
			string effectname = null;
			switch (nowPhantom)
			{
			case PhantomType.LASER:
				effectname = "ef_pl_change_laser01";
				break;
			case PhantomType.DRILL:
				effectname = "ef_pl_change_drill01";
				break;
			case PhantomType.ASTEROID:
				effectname = "ef_pl_change_asteroid01";
				break;
			}
			GameObject gameobj = StateUtil.CreateEffect(context, effectname, true);
			StateUtil.SetObjectLocalPositionToCenter(context, gameobj);
			context.SetNotCharaChange(false);
			if (!hitBoss)
			{
				context.SetNotUseItem(false);
			}
			if (hitBoss)
			{
				context.ChangeState(STATE_ID.AfterSpinAttack);
			}
			else
			{
				context.ChangeState(STATE_ID.Fall);
			}
		}

		// Token: 0x0600418F RID: 16783 RVA: 0x00155240 File Offset: 0x00153440
		public static void SendMessageTransformPhantom(CharacterState context, PhantomType phantom)
		{
			MsgTransformPhantom msgTransformPhantom = new MsgTransformPhantom(phantom);
			if (msgTransformPhantom != null)
			{
				GameObjectUtil.SendMessageFindGameObject("GameModeStage", "OnTransformPhantom", msgTransformPhantom, SendMessageOptions.DontRequireReceiver);
				if (StageItemManager.Instance != null)
				{
					StageItemManager.Instance.OnTransformPhantom(msgTransformPhantom);
				}
				GameObjectUtil.SendDelayedMessageToTagObjects("Gimmick", "OnTransformPhantom", msgTransformPhantom);
				GameObjectUtil.SendDelayedMessageToTagObjects("Boss", "OnTransformPhantom", msgTransformPhantom);
				ObjUtil.PauseCombo(MsgPauseComboTimer.State.PAUSE_TIMER, -1f);
			}
		}

		// Token: 0x06004190 RID: 16784 RVA: 0x001552B4 File Offset: 0x001534B4
		public static void SendMessageReturnFromPhantom(CharacterState context, PhantomType phantom)
		{
			if (context.IsOnDestroy())
			{
				return;
			}
			context.NowPhantomType = PhantomType.NONE;
			MsgReturnFromPhantom msgReturnFromPhantom = new MsgReturnFromPhantom();
			if (msgReturnFromPhantom != null)
			{
				GameObjectUtil.SendMessageFindGameObject("GameModeStage", "OnReturnFromPhantom", msgReturnFromPhantom, SendMessageOptions.DontRequireReceiver);
				if (StageItemManager.Instance != null)
				{
					StageItemManager.Instance.OnReturnFromPhantom(msgReturnFromPhantom);
				}
				GameObjectUtil.SendDelayedMessageToTagObjects("Gimmick", "OnReturnFromPhantom", msgReturnFromPhantom);
				GameObjectUtil.SendDelayedMessageToTagObjects("Boss", "OnReturnFromPhantom", msgReturnFromPhantom);
				if (context.GetLevelInformation().NowBoss)
				{
					ObjUtil.SendMessageOnBossObjectDead();
				}
				else
				{
					ObjUtil.SendMessageOnObjectDead();
				}
				ObjUtil.SendMessageAppearTrampoline();
				ObjUtil.PauseCombo(MsgPauseComboTimer.State.PLAY_SET, 3f);
			}
			ItemType item = ItemType.UNKNOWN;
			switch (phantom)
			{
			case PhantomType.LASER:
				item = ItemType.LASER;
				break;
			case PhantomType.DRILL:
				item = ItemType.DRILL;
				break;
			case PhantomType.ASTEROID:
				item = ItemType.ASTEROID;
				break;
			}
			StateUtil.SendMessageToTerminateItem(item);
			MsgChaoStateUtil.SendMsgChaoState(MsgChaoState.State.COME_IN);
		}

		// Token: 0x06004191 RID: 16785 RVA: 0x00155398 File Offset: 0x00153598
		public static bool CheckCharaChangeOnDieAndSendMessage(CharacterState context)
		{
			StateUtil.SendMessageToTerminateItem(ItemType.LASER);
			StateUtil.SendMessageToTerminateItem(ItemType.DRILL);
			StateUtil.SendMessageToTerminateItem(ItemType.ASTEROID);
			StageAbilityManager instance = StageAbilityManager.Instance;
			CharacterContainer characterContainer = context.GetCharacterContainer();
			if (characterContainer != null)
			{
				if (characterContainer.IsEnableRecovery() && (float)ObjUtil.GetRandomRange100() < instance.GetChaoAbilityValue(ChaoAbility.RECOVERY_FROM_FAILURE))
				{
					characterContainer.PrepareRecovery(context.GetLevelInformation().NowBoss);
					return false;
				}
				if (characterContainer.IsEnableChangeCharacter())
				{
					MsgChangeChara msgChangeChara = new MsgChangeChara();
					msgChangeChara.m_changeByMiss = true;
					characterContainer.SendMessage("OnMsgChangeChara", msgChangeChara);
					if (msgChangeChara.m_succeed)
					{
						return false;
					}
				}
			}
			if (context.GetLevelInformation().NowTutorial)
			{
				return false;
			}
			if (instance.HasChaoAbility(ChaoAbility.LAST_CHANCE) && !context.GetLevelInformation().NowBoss && !context.GetLevelInformation().NowFeverBoss)
			{
				context.ChangeState(STATE_ID.LastChance);
				return false;
			}
			MsgNotifyDead value = new MsgNotifyDead();
			GameObject gameObject = GameObject.Find("GameModeStage");
			if (gameObject)
			{
				gameObject.SendMessage("OnMsgNotifyDead", value, SendMessageOptions.DontRequireReceiver);
			}
			GameObjectUtil.SendDelayedMessageToTagObjects("Boss", "OnMsgNotifyDead", value);
			return true;
		}

		// Token: 0x06004192 RID: 16786 RVA: 0x001554B8 File Offset: 0x001536B8
		public static bool CheckAndChangeStateToAirAttack(CharacterState context, bool checkAfterActionJump, bool resetUpDirection)
		{
			STATE_ID state_ID = STATE_ID.Non;
			if (StateUtil.GetNextStateToAirAttack(context, ref state_ID, checkAfterActionJump))
			{
				if (state_ID == STATE_ID.Jump)
				{
					JumpParameter jumpParameter = context.CreateEnteringParameter<JumpParameter>();
					jumpParameter.Set(true);
					if (context.IsThirdJump())
					{
						state_ID = STATE_ID.ThirdJump;
					}
				}
				if (resetUpDirection)
				{
					MovementUtil.RotateByCollision(context.transform, context.GetComponent<CapsuleCollider>(), -context.Movement.GetGravityDir());
				}
				context.ChangeState(state_ID);
				return true;
			}
			return false;
		}

		// Token: 0x06004193 RID: 16787 RVA: 0x0015552C File Offset: 0x0015372C
		public static bool GetNextStateToAirAttack(CharacterState context, ref STATE_ID id, bool checkAfterActionJump)
		{
			if (context.NumAirAction == 0)
			{
				id = STATE_ID.Jump;
				return true;
			}
			if (context.NumAirAction < context.NumEnableJump)
			{
				id = STATE_ID.DoubleJump;
				return true;
			}
			if (context.NumAirAction == context.NumEnableJump)
			{
				id = STATE_ID.AirAttackAction;
				return true;
			}
			if (checkAfterActionJump && context.NumAirAction == context.NumEnableJump + 1 && context.GetCharacterAttribute() == CharacterAttribute.POWER)
			{
				id = STATE_ID.Jump;
				return true;
			}
			return false;
		}

		// Token: 0x06004194 RID: 16788 RVA: 0x001555A4 File Offset: 0x001537A4
		public static void NowLanding(CharacterState context, bool rotateOnGround)
		{
			if (rotateOnGround)
			{
				StateUtil.SetRotateOnGround(context);
			}
			StateUtil.CreateLandingEffect(context);
			context.SetStatus(Status.NowLanding, true);
		}

		// Token: 0x06004195 RID: 16789 RVA: 0x001555C0 File Offset: 0x001537C0
		public static void SetOnBoost(CharacterState context, CharacterLoopEffect characterboost, bool value)
		{
			if (value)
			{
				context.OnAttack(AttackPower.PlayerSpin, DefensePower.PlayerSpin);
				StageAbilityManager instance = StageAbilityManager.Instance;
				if (instance != null)
				{
					float chaoAbilityValue = instance.GetChaoAbilityValue(ChaoAbility.SPIN_DASH_MAGNET);
					float num = UnityEngine.Random.Range(0f, 99.9f);
					if (chaoAbilityValue >= num)
					{
						StateUtil.ActiveChaoAbilityMagnetObject(context);
						instance.RequestPlayChaoEffect(ChaoAbility.SPIN_DASH_MAGNET);
					}
				}
			}
			else
			{
				context.OffAttack();
				StateUtil.DeactiveChaoAbilityMagetObject(context);
			}
			if (characterboost != null)
			{
				characterboost.SetValid(value);
			}
		}

		// Token: 0x06004196 RID: 16790 RVA: 0x00155640 File Offset: 0x00153840
		public static void SetOnBoostEx(CharacterState context, CharacterLoopEffect characterBoostEx, bool value)
		{
			if (characterBoostEx != null)
			{
				characterBoostEx.SetValid(value);
			}
		}

		// Token: 0x06004197 RID: 16791 RVA: 0x00155658 File Offset: 0x00153858
		public static void Dead(CharacterState context)
		{
			context.SetStatus(Status.Dead, true);
		}

		// Token: 0x06004198 RID: 16792 RVA: 0x00155664 File Offset: 0x00153864
		public static void AddHitWallTimer(CharacterState context, float deltaTime)
		{
			context.m_hitWallTimer += deltaTime;
			context.SetStatus(Status.HitWallTimerDirty, true);
		}

		// Token: 0x06004199 RID: 16793 RVA: 0x00155680 File Offset: 0x00153880
		public static void SetEmergency(CharacterState context, bool value)
		{
			bool flag = context.TestStatus(Status.Emergency);
			if (value && !flag)
			{
				if (context.GetLevelInformation().BossStage && context.GetLevelInformation().BossDestroy)
				{
					return;
				}
				HudCaution.Instance.SetCaution(new MsgCaution(HudCaution.Type.NO_RING));
			}
			else if (!value && flag)
			{
				HudCaution.Instance.SetInvisibleCaution(new MsgCaution(HudCaution.Type.NO_RING));
			}
			context.SetStatus(Status.Emergency, value);
		}

		// Token: 0x0600419A RID: 16794 RVA: 0x00155700 File Offset: 0x00153900
		public static void SetAttackAttributePowerIfPowerType(CharacterState context, bool value)
		{
			if (context.GetCharacterAttribute() == CharacterAttribute.POWER)
			{
				if (value)
				{
					context.OnAttackAttribute(AttackAttribute.Power);
				}
				else
				{
					context.OnAttackAttribute(AttackAttribute.Power);
				}
			}
		}

		// Token: 0x0600419B RID: 16795 RVA: 0x00155728 File Offset: 0x00153928
		public static void SetDashRingMagnet(CharacterState context, bool onFlag)
		{
			if (onFlag)
			{
				StageAbilityManager instance = StageAbilityManager.Instance;
				if (instance != null)
				{
					float chaoAbilityValue = instance.GetChaoAbilityValue(ChaoAbility.DASH_RING_MAGNET);
					float num = UnityEngine.Random.Range(0f, 99.9f);
					if (chaoAbilityValue >= num)
					{
						StateUtil.ActiveChaoAbilityMagnetObject(context);
						instance.RequestPlayChaoEffect(ChaoAbility.DASH_RING_MAGNET);
					}
				}
			}
			else
			{
				StateUtil.DeactiveChaoAbilityMagetObject(context);
			}
		}

		// Token: 0x0600419C RID: 16796 RVA: 0x00155788 File Offset: 0x00153988
		public static void SetCannonMagnet(CharacterState context, bool onFlag)
		{
			if (onFlag)
			{
				StageAbilityManager instance = StageAbilityManager.Instance;
				if (instance != null)
				{
					float chaoAbilityValue = instance.GetChaoAbilityValue(ChaoAbility.CANNON_MAGNET);
					float num = UnityEngine.Random.Range(0f, 99.9f);
					if (chaoAbilityValue >= num)
					{
						StateUtil.ActiveChaoAbilityMagnetObject(context);
						instance.RequestPlayChaoEffect(ChaoAbility.CANNON_MAGNET);
					}
				}
			}
			else
			{
				StateUtil.DeactiveChaoAbilityMagetObject(context);
			}
		}

		// Token: 0x0600419D RID: 16797 RVA: 0x001557E8 File Offset: 0x001539E8
		public static void SetJumpRampMagnet(CharacterState context, bool onFlag)
		{
			if (onFlag)
			{
				StageAbilityManager instance = StageAbilityManager.Instance;
				if (instance != null)
				{
					float chaoAbilityValue = instance.GetChaoAbilityValue(ChaoAbility.JUMP_RAMP_MAGNET);
					float num = UnityEngine.Random.Range(0f, 99.9f);
					if (chaoAbilityValue >= num)
					{
						StateUtil.ActiveChaoAbilityMagnetObject(context);
						instance.RequestPlayChaoEffect(ChaoAbility.JUMP_RAMP_MAGNET);
					}
				}
			}
			else
			{
				StateUtil.DeactiveChaoAbilityMagetObject(context);
			}
		}

		// Token: 0x0600419E RID: 16798 RVA: 0x00155848 File Offset: 0x00153A48
		public static void SetSpecialtyJumpMagnet(CharacterState context, CharacterAttribute attri, ChaoAbility ability, bool onFlag)
		{
			if (attri != context.GetCharacterAttribute())
			{
				return;
			}
			if (context.IsNowPhantom())
			{
				return;
			}
			if (onFlag)
			{
				if (StageAbilityManager.Instance != null && StageAbilityManager.Instance.HasChaoAbility(ability))
				{
					float chaoAbilityValue = StageAbilityManager.Instance.GetChaoAbilityValue(ability);
					float num = UnityEngine.Random.Range(0f, 99.9f);
					if (chaoAbilityValue >= num)
					{
						StateUtil.ActiveChaoAbilityMagnetObject(context);
						ObjUtil.RequestStartAbilityToChao(ability, false);
					}
				}
			}
			else
			{
				StateUtil.DeactiveChaoAbilityMagetObject(context);
			}
		}

		// Token: 0x0600419F RID: 16799 RVA: 0x001558D0 File Offset: 0x00153AD0
		public static void SetSpecialtyJumpDestroyEnemy(ChaoAbility ability)
		{
			if (StageAbilityManager.Instance != null && StageAbilityManager.Instance.HasChaoAbility(ability))
			{
				float chaoAbilityValue = StageAbilityManager.Instance.GetChaoAbilityValue(ability);
				float num = UnityEngine.Random.Range(0f, 99.9f);
				if (chaoAbilityValue >= num)
				{
					ObjUtil.RequestStartAbilityToChao(ability, false);
				}
			}
		}

		// Token: 0x060041A0 RID: 16800 RVA: 0x00155928 File Offset: 0x00153B28
		public static void SetPhantomMagnetColliderRange(CharacterState context, PhantomType phantom)
		{
			if (phantom < PhantomType.NUM_MAX)
			{
				GameObject gameObject = GameObjectUtil.FindChildGameObject(context.gameObject, CharacterDefs.PhantomBodyName[(int)phantom]);
				if (gameObject != null)
				{
					CharacterMagnetPhantom characterMagnetPhantom = GameObjectUtil.FindChildGameObjectComponent<CharacterMagnetPhantom>(gameObject, "MagnetCollision");
					if (characterMagnetPhantom != null)
					{
						characterMagnetPhantom.SetDefaultRadiusAndOffset();
					}
				}
			}
		}

		// Token: 0x060041A1 RID: 16801 RVA: 0x0015597C File Offset: 0x00153B7C
		public static void SetPhantomQuickTimerPause(bool pauseFlag)
		{
			if (StageModeManager.Instance != null && StageModeManager.Instance.IsQuickMode() && StageTimeManager.Instance != null)
			{
				StageTimeManager.Instance.PhantomPause(pauseFlag);
			}
		}

		// Token: 0x040037F6 RID: 14326
		private const float RunEffectTime = 2f;

		// Token: 0x040037F7 RID: 14327
		private const string MagnetObjectName = "CharacterMagnet";

		// Token: 0x040037F8 RID: 14328
		private const string ChaoAbilityMagnetObjectName = "CharacterMagnetChaoAbility";

		// Token: 0x040037F9 RID: 14329
		private const string InvincibleObjectName = "CharacterInvincible";

		// Token: 0x040037FA RID: 14330
		private const string BarrierObjectName = "CharacterBarrier";

		// Token: 0x040037FB RID: 14331
		private const string ComboObjectName = "CharacterCombo";

		// Token: 0x040037FC RID: 14332
		private const string TrampoilneObjectName = "CharacterTrampoline";

		// Token: 0x040037FD RID: 14333
		private const int NumAirActionNothing = 0;

		// Token: 0x040037FE RID: 14334
		private const int NumAirActionJump = 1;
	}
}
