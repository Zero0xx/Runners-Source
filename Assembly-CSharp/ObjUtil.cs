using System;
using System.Collections.Generic;
using Message;
using Mission;
using Tutorial;
using UnityEngine;

// Token: 0x02000960 RID: 2400
public class ObjUtil
{
	// Token: 0x06003E6F RID: 15983 RVA: 0x0014480C File Offset: 0x00142A0C
	public static void SendMessageAddScore(int score)
	{
		if (StageScoreManager.Instance != null)
		{
			StageScoreManager.Instance.AddScore((long)score);
		}
	}

	// Token: 0x06003E70 RID: 15984 RVA: 0x0014482C File Offset: 0x00142A2C
	public static void SendMessageAddBonusScore(int score)
	{
		ObjUtil.SendMessageAddScore(score);
	}

	// Token: 0x06003E71 RID: 15985 RVA: 0x00144834 File Offset: 0x00142A34
	public static void SendMessageAddAnimal(int addCount)
	{
		if (StageScoreManager.Instance != null)
		{
			StageScoreManager.Instance.AddAnimal((long)addCount);
		}
	}

	// Token: 0x06003E72 RID: 15986 RVA: 0x00144854 File Offset: 0x00142A54
	public static void SendMessageAddRedRing()
	{
		if (StageScoreManager.Instance != null)
		{
			StageScoreManager.Instance.AddRedRing();
		}
	}

	// Token: 0x06003E73 RID: 15987 RVA: 0x00144870 File Offset: 0x00142A70
	public static void SendMessageTransferRing()
	{
		if (StageScoreManager.Instance != null)
		{
			StageScoreManager.Instance.TransferRing();
		}
	}

	// Token: 0x06003E74 RID: 15988 RVA: 0x0014488C File Offset: 0x00142A8C
	public static void SendMessageTransferRingForContinue(int ring)
	{
		if (StageScoreManager.Instance != null)
		{
			StageScoreManager.Instance.TransferRingForContinue(ring);
		}
	}

	// Token: 0x06003E75 RID: 15989 RVA: 0x001448AC File Offset: 0x00142AAC
	public static void SendMessageFinalScore()
	{
		if (StageScoreManager.Instance != null)
		{
			StageScoreManager.Instance.OnCalcFinalScore();
		}
	}

	// Token: 0x06003E76 RID: 15990 RVA: 0x001448C8 File Offset: 0x00142AC8
	public static void SendMessageFinalScoreBeforeResult()
	{
		if (StageScoreManager.Instance != null)
		{
			StageScoreManager.Instance.SendMessageFinalScoreBeforeResult();
		}
	}

	// Token: 0x06003E77 RID: 15991 RVA: 0x001448E4 File Offset: 0x00142AE4
	public static void SendMessageAddSpecialCrystal(int count)
	{
		if (StageScoreManager.Instance != null)
		{
			StageScoreManager.Instance.AddSpecialCrystal((long)count);
		}
	}

	// Token: 0x06003E78 RID: 15992 RVA: 0x00144904 File Offset: 0x00142B04
	public static void SendMessageScoreCheck(StageScoreData scoreData)
	{
		if (StageScoreManager.Instance != null)
		{
			StageScoreManager.Instance.AddScoreCheck(scoreData);
		}
	}

	// Token: 0x06003E79 RID: 15993 RVA: 0x00144924 File Offset: 0x00142B24
	public static void AddCombo()
	{
		StageComboManager instance = StageComboManager.Instance;
		if (instance != null)
		{
			instance.AddCombo();
		}
	}

	// Token: 0x06003E7A RID: 15994 RVA: 0x0014494C File Offset: 0x00142B4C
	public static void SendMessageMission(MsgMissionEvent msg)
	{
		if (StageMissionManager.Instance)
		{
			GameObjectUtil.SendDelayedMessageToGameObject(StageMissionManager.Instance.gameObject, "OnMissionEvent", msg);
		}
	}

	// Token: 0x06003E7B RID: 15995 RVA: 0x00144980 File Offset: 0x00142B80
	public static void SendMessageMission2(MsgMissionEvent msg)
	{
		if (StageMissionManager.Instance)
		{
			GameObjectUtil.SendMessageFindGameObject("StageMissionManager", "OnMissionEvent", msg, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06003E7C RID: 15996 RVA: 0x001449A4 File Offset: 0x00142BA4
	public static void SendMessageMission(Mission.EventID id, int num)
	{
		MsgMissionEvent msg = new MsgMissionEvent(id, (long)num);
		ObjUtil.SendMessageMission(msg);
	}

	// Token: 0x06003E7D RID: 15997 RVA: 0x001449C0 File Offset: 0x00142BC0
	public static void SendMessageMission(Mission.EventID id)
	{
		MsgMissionEvent msg = new MsgMissionEvent(id);
		ObjUtil.SendMessageMission(msg);
	}

	// Token: 0x06003E7E RID: 15998 RVA: 0x001449DC File Offset: 0x00142BDC
	public static void SendMessageTutorialClear(Tutorial.EventID id)
	{
		if (StageTutorialManager.Instance)
		{
			MsgTutorialClear value = new MsgTutorialClear(id);
			GameObjectUtil.SendDelayedMessageToGameObject(StageTutorialManager.Instance.gameObject, "OnMsgTutorialClear", value);
		}
	}

	// Token: 0x06003E7F RID: 15999 RVA: 0x00144A14 File Offset: 0x00142C14
	public static Vector3 GetCollisionCenter(GameObject obj)
	{
		if (obj)
		{
			SphereCollider component = obj.GetComponent<SphereCollider>();
			if (component)
			{
				return component.center;
			}
			BoxCollider component2 = obj.GetComponent<BoxCollider>();
			if (component2)
			{
				return component2.center;
			}
		}
		return Vector3.zero;
	}

	// Token: 0x06003E80 RID: 16000 RVA: 0x00144A64 File Offset: 0x00142C64
	public static Vector3 GetCollisionCenterPosition(GameObject obj)
	{
		if (obj)
		{
			Vector3 collisionCenter = ObjUtil.GetCollisionCenter(obj);
			return obj.transform.position + obj.transform.TransformDirection(collisionCenter);
		}
		return Vector3.zero;
	}

	// Token: 0x06003E81 RID: 16001 RVA: 0x00144AA8 File Offset: 0x00142CA8
	public static void PlayEffectCollisionCenter(GameObject obj, string name, float destroy_time, bool playLightMode = false)
	{
		if (obj)
		{
			ObjUtil.PlayEffect(name, ObjUtil.GetCollisionCenterPosition(obj), obj.transform.rotation, destroy_time, playLightMode);
		}
	}

	// Token: 0x06003E82 RID: 16002 RVA: 0x00144ADC File Offset: 0x00142CDC
	public static void PlayEffect(string name, Vector3 pos, Quaternion rot, float destroy_time, bool playLightMode = false)
	{
		if (!playLightMode && ObjUtil.IsLightMode())
		{
			return;
		}
		GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.COMMON_EFFECT, name);
		if (gameObject == null)
		{
			gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.EVENT_RESOURCE, name);
		}
		if (gameObject != null)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, pos, rot) as GameObject;
			if (gameObject2)
			{
				gameObject2.SetActive(true);
				ParticleSystem component = gameObject2.GetComponent<ParticleSystem>();
				if (component)
				{
					component.Play();
				}
				if (destroy_time > 0f)
				{
					UnityEngine.Object.Destroy(gameObject2, destroy_time);
				}
			}
		}
	}

	// Token: 0x06003E83 RID: 16003 RVA: 0x00144B78 File Offset: 0x00142D78
	public static void PlayEffectCollisionCenterChild(GameObject obj, string name, float destroy_time, bool playLightMode = false)
	{
		if (obj)
		{
			ObjUtil.PlayEffectChild(obj, name, ObjUtil.GetCollisionCenter(obj), Quaternion.identity, destroy_time, playLightMode);
		}
	}

	// Token: 0x06003E84 RID: 16004 RVA: 0x00144BA4 File Offset: 0x00142DA4
	public static void PlayEffectChild(GameObject parent, string name, Vector3 local_pos, Quaternion local_rot, float destroy_time, bool playLightMode = true)
	{
		GameObject gameObject = ObjUtil.PlayEffectChild(parent, name, local_pos, local_rot, playLightMode);
		if (gameObject && destroy_time > 0f)
		{
			UnityEngine.Object.Destroy(gameObject, destroy_time);
		}
	}

	// Token: 0x06003E85 RID: 16005 RVA: 0x00144BDC File Offset: 0x00142DDC
	public static GameObject PlayEffectChild(GameObject parent, string name, Vector3 local_pos, Quaternion local_rot, bool playLightMode = false)
	{
		if (!playLightMode && ObjUtil.IsLightMode())
		{
			return null;
		}
		GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.COMMON_EFFECT, name);
		if (gameObject == null)
		{
			gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.EVENT_RESOURCE, name);
		}
		if (gameObject != null && parent != null)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, parent.transform.position, parent.transform.rotation) as GameObject;
			if (gameObject2)
			{
				gameObject2.SetActive(true);
				gameObject2.transform.parent = parent.transform;
				gameObject2.transform.localPosition = local_pos;
				gameObject2.transform.localRotation = local_rot;
				ParticleSystem component = gameObject2.GetComponent<ParticleSystem>();
				if (component)
				{
					component.Play();
				}
				return gameObject2;
			}
		}
		return null;
	}

	// Token: 0x06003E86 RID: 16006 RVA: 0x00144CB4 File Offset: 0x00142EB4
	public static GameObject PlayChaoEffect(GameObject parent, string name, Vector3 pos, float destroy_time, bool playLightMode)
	{
		if (!playLightMode && ObjUtil.IsLightMode())
		{
			return null;
		}
		return ObjUtil.PlayChaoEffect(parent, name, pos, destroy_time);
	}

	// Token: 0x06003E87 RID: 16007 RVA: 0x00144CD4 File Offset: 0x00142ED4
	public static GameObject PlayChaoEffect(GameObject parent, string name, float destroy_time)
	{
		return ObjUtil.PlayChaoEffect(parent, name, ObjUtil.GetCollisionCenter(parent), destroy_time);
	}

	// Token: 0x06003E88 RID: 16008 RVA: 0x00144CE4 File Offset: 0x00142EE4
	public static void PlayChaoEffect(string name, Vector3 pos, float destroy_time)
	{
		GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.CHAO_MODEL, name);
		if (gameObject != null)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, pos, Quaternion.identity) as GameObject;
			if (gameObject2 != null)
			{
				gameObject2.SetActive(true);
				ParticleSystem component = gameObject2.GetComponent<ParticleSystem>();
				if (component != null)
				{
					component.Play();
				}
				if (destroy_time > 0f)
				{
					UnityEngine.Object.Destroy(gameObject2, destroy_time);
				}
			}
		}
	}

	// Token: 0x06003E89 RID: 16009 RVA: 0x00144D5C File Offset: 0x00142F5C
	public static GameObject PlayChaoEffect(GameObject parent, string name, Vector3 pos, float destroy_time)
	{
		if (parent != null)
		{
			GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.CHAO_MODEL, name);
			if (gameObject != null)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, parent.transform.position, parent.transform.rotation) as GameObject;
				if (gameObject2)
				{
					gameObject2.SetActive(true);
					gameObject2.transform.parent = parent.transform;
					gameObject2.transform.localPosition = pos;
					gameObject2.transform.localRotation = Quaternion.identity;
					ParticleSystem component = gameObject2.GetComponent<ParticleSystem>();
					if (component)
					{
						component.Play();
					}
					if (destroy_time > 0f)
					{
						UnityEngine.Object.Destroy(gameObject2, destroy_time);
					}
					return gameObject2;
				}
			}
		}
		return null;
	}

	// Token: 0x06003E8A RID: 16010 RVA: 0x00144E20 File Offset: 0x00143020
	public static GameObject PlayChaoEffectForHUD(GameObject parent, string name, float destroy_time)
	{
		if (parent != null)
		{
			GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.CHAO_MODEL, name);
			if (gameObject != null)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, parent.transform.position, parent.transform.rotation) as GameObject;
				if (gameObject2)
				{
					Vector3 localPosition = gameObject2.transform.localPosition;
					Vector3 localScale = gameObject2.transform.localScale;
					Quaternion localRotation = gameObject2.transform.localRotation;
					gameObject2.SetActive(true);
					gameObject2.transform.parent = parent.transform;
					gameObject2.transform.localPosition = localPosition;
					gameObject2.transform.localRotation = localRotation;
					gameObject2.transform.localScale = localScale;
					ParticleSystem component = gameObject2.GetComponent<ParticleSystem>();
					if (component)
					{
						component.Play();
					}
					if (destroy_time > 0f)
					{
						UnityEngine.Object.Destroy(gameObject2, destroy_time);
					}
					return gameObject2;
				}
			}
		}
		return null;
	}

	// Token: 0x06003E8B RID: 16011 RVA: 0x00144F14 File Offset: 0x00143114
	public static void StopAnimation(GameObject obj)
	{
		if (obj)
		{
			Animation componentInChildren = obj.GetComponentInChildren<Animation>();
			if (componentInChildren)
			{
				componentInChildren.playAutomatically = false;
				componentInChildren.Stop();
			}
		}
	}

	// Token: 0x06003E8C RID: 16012 RVA: 0x00144F4C File Offset: 0x0014314C
	public static SoundManager.PlayId PlaySE(string name, string cueSheetName = "SE")
	{
		return SoundManager.SePlay(name, cueSheetName);
	}

	// Token: 0x06003E8D RID: 16013 RVA: 0x00144F58 File Offset: 0x00143158
	public static SoundManager.PlayId PlayEventSE(string name, EventManager.EventType eventType)
	{
		return ObjUtil.PlaySE(name, "SE_" + EventManager.GetEventTypeName(eventType));
	}

	// Token: 0x06003E8E RID: 16014 RVA: 0x00144F70 File Offset: 0x00143170
	public static SoundManager.PlayId LightPlaySE(string name, string cueSheetName = "SE")
	{
		if (ObjUtil.IsLightMode())
		{
			return SoundManager.PlayId.NONE;
		}
		return ObjUtil.PlaySE(name, cueSheetName);
	}

	// Token: 0x06003E8F RID: 16015 RVA: 0x00144F88 File Offset: 0x00143188
	public static SoundManager.PlayId LightPlayEventSE(string name, EventManager.EventType eventType)
	{
		return ObjUtil.LightPlaySE(name, "SE_" + EventManager.GetEventTypeName(eventType));
	}

	// Token: 0x06003E90 RID: 16016 RVA: 0x00144FA0 File Offset: 0x001431A0
	public static void StopSE(SoundManager.PlayId id)
	{
		SoundManager.SeStop(id);
	}

	// Token: 0x06003E91 RID: 16017 RVA: 0x00144FA8 File Offset: 0x001431A8
	public static void CreateLostRing(Vector3 pos, Quaternion rot, int ringCount)
	{
		GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.OBJECT_PREFAB, "ObjLostRing");
		if (gameObject != null)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, pos, Quaternion.identity) as GameObject;
			if (gameObject2)
			{
				gameObject2.gameObject.SetActive(true);
				ObjLostRing component = gameObject2.GetComponent<ObjLostRing>();
				if (component != null)
				{
					component.SetRingCount(ringCount);
					StageAbilityManager instance = StageAbilityManager.Instance;
					if (instance)
					{
						GameObject lostRingChao = instance.GetLostRingChao();
						if (lostRingChao != null)
						{
							component.SetChaoMagnet(lostRingChao);
						}
					}
				}
			}
		}
	}

	// Token: 0x06003E92 RID: 16018 RVA: 0x00145044 File Offset: 0x00143244
	public static void SetPlayerDeadRecoveryRing(PlayerInformation information)
	{
		StageAbilityManager instance = StageAbilityManager.Instance;
		if (instance != null && information != null && instance.HasChaoAbility(ChaoAbility.RECOVERY_RING) && information.NumRings > 0)
		{
			instance.SetLostRingCount(information.NumRings);
			information.LostRings();
		}
	}

	// Token: 0x06003E93 RID: 16019 RVA: 0x0014509C File Offset: 0x0014329C
	public static void StartMagnetControl(GameObject obj)
	{
		ObjUtil.StartMagnetControl(obj, 0f);
	}

	// Token: 0x06003E94 RID: 16020 RVA: 0x001450AC File Offset: 0x001432AC
	public static void StartMagnetControl(GameObject obj, GameObject target)
	{
		ObjUtil.StartMagnetControl(obj, target, 0f);
	}

	// Token: 0x06003E95 RID: 16021 RVA: 0x001450BC File Offset: 0x001432BC
	public static void StartMagnetControl(GameObject obj, float time)
	{
		ObjUtil.StartMagnetControl(obj, null, time);
	}

	// Token: 0x06003E96 RID: 16022 RVA: 0x001450C8 File Offset: 0x001432C8
	public static void StartMagnetControl(GameObject obj, GameObject target, float time)
	{
		MagnetControl component = obj.GetComponent<MagnetControl>();
		if (component != null)
		{
			component.OnUseMagnet(new MsgUseMagnet(obj, target, time));
		}
	}

	// Token: 0x06003E97 RID: 16023 RVA: 0x001450F8 File Offset: 0x001432F8
	public static float GetPlayerDefaultSpeed()
	{
		PlayerInformation playerInformation = ObjUtil.GetPlayerInformation();
		if (playerInformation && playerInformation.DefaultSpeed > 0f)
		{
			return playerInformation.DefaultSpeed;
		}
		return ObjUtil.PLAYER_SPEED1;
	}

	// Token: 0x06003E98 RID: 16024 RVA: 0x00145134 File Offset: 0x00143334
	public static float GetPlayerAddSpeed()
	{
		float playerDefaultSpeed = ObjUtil.GetPlayerDefaultSpeed();
		return Mathf.Max(playerDefaultSpeed - ObjUtil.PLAYER_SPEED1, 0f);
	}

	// Token: 0x06003E99 RID: 16025 RVA: 0x00145158 File Offset: 0x00143358
	public static float GetPlayerAddSpeedRatio()
	{
		float playerDefaultSpeed = ObjUtil.GetPlayerDefaultSpeed();
		if (playerDefaultSpeed > 0f)
		{
			return Mathf.Max(playerDefaultSpeed / ObjUtil.PLAYER_SPEED1, 1f);
		}
		return 1f;
	}

	// Token: 0x06003E9A RID: 16026 RVA: 0x00145190 File Offset: 0x00143390
	public static void StartHudAlert(GameObject obj)
	{
		GameObject gameObject = GameObject.Find("UI Root (2D)");
		if (gameObject)
		{
			HudAlert hudAlert = GameObjectUtil.FindChildGameObjectComponent<HudAlert>(gameObject, "HudAlert");
			if (hudAlert)
			{
				hudAlert.StartAlert(obj);
				ObjUtil.PlaySE("obj_missile_warning", "SE");
			}
		}
	}

	// Token: 0x06003E9B RID: 16027 RVA: 0x001451E4 File Offset: 0x001433E4
	public static int GetRandomRange100()
	{
		return UnityEngine.Random.Range(0, 100);
	}

	// Token: 0x06003E9C RID: 16028 RVA: 0x001451F0 File Offset: 0x001433F0
	public static int GetRandomRange(int maxRate)
	{
		return UnityEngine.Random.Range(0, maxRate);
	}

	// Token: 0x06003E9D RID: 16029 RVA: 0x001451FC File Offset: 0x001433FC
	public static void SetModelVisible(GameObject obj, bool flag)
	{
		if (obj)
		{
			Component[] componentsInChildren = obj.GetComponentsInChildren<MeshRenderer>(true);
			foreach (MeshRenderer meshRenderer in componentsInChildren)
			{
				meshRenderer.enabled = flag;
			}
		}
	}

	// Token: 0x06003E9E RID: 16030 RVA: 0x00145244 File Offset: 0x00143444
	public static bool CheckGroundHit(Vector3 pos, Vector3 up, float up_hit_length, float down_hit_length, out Vector3 hit_pos)
	{
		Vector3 direction = -up;
		Vector3 origin = pos + up * up_hit_length;
		RaycastHit raycastHit;
		if (Physics.Raycast(origin, direction, out raycastHit, down_hit_length))
		{
			hit_pos = raycastHit.point;
			return true;
		}
		hit_pos = pos;
		return false;
	}

	// Token: 0x06003E9F RID: 16031 RVA: 0x00145290 File Offset: 0x00143490
	public static void SendMessageAppearTrampoline()
	{
		MsgUseItem value = new MsgUseItem(ItemType.TRAMPOLINE);
		GameObjectUtil.SendMessageToTagObjects("Gimmick", "OnUseItem", value, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06003EA0 RID: 16032 RVA: 0x001452B8 File Offset: 0x001434B8
	public static void SendMessageOnObjectDead()
	{
		MsgObjectDead value = new MsgObjectDead();
		GameObjectUtil.SendDelayedMessageToTagObjects("Gimmick", "OnMsgObjectDead", value);
		GameObjectUtil.SendDelayedMessageToTagObjects("Enemy", "OnMsgObjectDead", value);
	}

	// Token: 0x06003EA1 RID: 16033 RVA: 0x001452EC File Offset: 0x001434EC
	public static void SendMessageOnBossObjectDead()
	{
		MsgObjectDead value = new MsgObjectDead();
		GameObjectUtil.SendMessageToTagObjects("Gimmick", "OnMsgObjectDead", value, SendMessageOptions.DontRequireReceiver);
		GameObjectUtil.SendMessageToTagObjects("Enemy", "OnMsgObjectDead", value, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06003EA2 RID: 16034 RVA: 0x00145324 File Offset: 0x00143524
	private static ObjectPartTable GetObjectPartTable()
	{
		GameObject gameObject = GameObject.Find("GameModeStage");
		if (gameObject != null)
		{
			GameModeStage component = gameObject.GetComponent<GameModeStage>();
			if (component != null)
			{
				return component.GetObjectPartTable();
			}
		}
		return null;
	}

	// Token: 0x06003EA3 RID: 16035 RVA: 0x00145364 File Offset: 0x00143564
	private static BrokenBonusType GetGameModeBrokenBonusType()
	{
		ObjectPartTable objectPartTable = ObjUtil.GetObjectPartTable();
		if (objectPartTable != null)
		{
			return objectPartTable.GetBrokenBonusType();
		}
		return BrokenBonusType.NONE;
	}

	// Token: 0x06003EA4 RID: 16036 RVA: 0x00145388 File Offset: 0x00143588
	private static BrokenBonusType GetGameModeBrokenBonusTypeForChaoAbility()
	{
		ObjectPartTable objectPartTable = ObjUtil.GetObjectPartTable();
		if (objectPartTable != null)
		{
			return objectPartTable.GetBrokenBonusTypeForChaoAbility();
		}
		return BrokenBonusType.NONE;
	}

	// Token: 0x06003EA5 RID: 16037 RVA: 0x001453AC File Offset: 0x001435AC
	public static void CreateBrokenBonus(GameObject broken_obj, GameObject playerObject, uint attackAttribute)
	{
		if (ObjUtil.IsAttackAttribute(attackAttribute, AttackAttribute.PhantomAsteroid) && broken_obj != null)
		{
			BrokenBonusType brokenBonusType = ObjUtil.GetGameModeBrokenBonusType();
			if (brokenBonusType != BrokenBonusType.NONE)
			{
				LevelInformation levelInformation = ObjUtil.GetLevelInformation();
				if (levelInformation != null && levelInformation.DestroyRingMode && brokenBonusType == BrokenBonusType.SUPER_RING)
				{
					brokenBonusType = BrokenBonusType.CRYSTAL10;
				}
				GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.OBJECT_PREFAB, "ObjBrokenBonus");
				if (gameObject != null)
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, broken_obj.transform.position, Quaternion.identity) as GameObject;
					if (gameObject2)
					{
						gameObject2.gameObject.SetActive(true);
						ObjBrokenBonus component = gameObject2.GetComponent<ObjBrokenBonus>();
						if (component)
						{
							component.Setup(brokenBonusType, playerObject);
						}
					}
				}
			}
		}
	}

	// Token: 0x06003EA6 RID: 16038 RVA: 0x00145470 File Offset: 0x00143670
	public static void CreateBrokenBonusForChaoAbiilty(GameObject broken_obj, GameObject playerObject)
	{
		if (broken_obj != null)
		{
			BrokenBonusType gameModeBrokenBonusTypeForChaoAbility = ObjUtil.GetGameModeBrokenBonusTypeForChaoAbility();
			if (gameModeBrokenBonusTypeForChaoAbility != BrokenBonusType.NONE)
			{
				GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.OBJECT_PREFAB, "ObjBrokenBonus");
				if (gameObject != null)
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, broken_obj.transform.position, Quaternion.identity) as GameObject;
					if (gameObject2)
					{
						gameObject2.gameObject.SetActive(true);
						ObjBrokenBonus component = gameObject2.GetComponent<ObjBrokenBonus>();
						if (component)
						{
							component.Setup(gameModeBrokenBonusTypeForChaoAbility, playerObject);
						}
					}
				}
			}
		}
	}

	// Token: 0x06003EA7 RID: 16039 RVA: 0x00145500 File Offset: 0x00143700
	public static bool IsAttackAttribute(uint state, AttackAttribute attribute)
	{
		return (state & (uint)attribute) != 0U;
	}

	// Token: 0x06003EA8 RID: 16040 RVA: 0x0014550C File Offset: 0x0014370C
	public static PlayerInformation GetPlayerInformation()
	{
		return GameObjectUtil.FindGameObjectComponent<PlayerInformation>("PlayerInformation");
	}

	// Token: 0x06003EA9 RID: 16041 RVA: 0x00145518 File Offset: 0x00143718
	public static LevelInformation GetLevelInformation()
	{
		return GameObjectUtil.FindGameObjectComponent<LevelInformation>("LevelInformation");
	}

	// Token: 0x06003EAA RID: 16042 RVA: 0x00145524 File Offset: 0x00143724
	public static bool IsUseTemporarySet()
	{
		return false;
	}

	// Token: 0x06003EAB RID: 16043 RVA: 0x00145534 File Offset: 0x00143734
	public static int GetChaoAbliltyValue(ChaoAbility ability, int src_value)
	{
		StageAbilityManager instance = StageAbilityManager.Instance;
		if (instance)
		{
			return instance.GetChaoAbliltyValue(ability, src_value);
		}
		return src_value;
	}

	// Token: 0x06003EAC RID: 16044 RVA: 0x0014555C File Offset: 0x0014375C
	public static int GetChaoAbliltyScore(List<ChaoAbility> abilityList, int src_value)
	{
		StageAbilityManager instance = StageAbilityManager.Instance;
		if (instance)
		{
			return instance.GetChaoAndTeamAbliltyScoreValue(abilityList, TeamAttributeBonusType.NONE, src_value);
		}
		return src_value;
	}

	// Token: 0x06003EAD RID: 16045 RVA: 0x00145588 File Offset: 0x00143788
	public static int GetChaoAndEnemyScore(List<ChaoAbility> abilityList, int src_value)
	{
		StageAbilityManager instance = StageAbilityManager.Instance;
		if (instance)
		{
			return instance.GetChaoAndEnemyScoreValue(abilityList, src_value);
		}
		return src_value;
	}

	// Token: 0x06003EAE RID: 16046 RVA: 0x001455B0 File Offset: 0x001437B0
	public static void GetChaoAbliltyPhantomFlag(uint attribute_state, ref List<ChaoAbility> abilityList)
	{
		PhantomType phantomType = PhantomType.NONE;
		if (ObjUtil.IsAttackAttribute(attribute_state, AttackAttribute.PhantomAsteroid))
		{
			phantomType = PhantomType.ASTEROID;
		}
		if (ObjUtil.IsAttackAttribute(attribute_state, AttackAttribute.PhantomLaser))
		{
			phantomType = PhantomType.LASER;
		}
		if (ObjUtil.IsAttackAttribute(attribute_state, AttackAttribute.PhantomDrill))
		{
			phantomType = PhantomType.DRILL;
		}
		if (phantomType != PhantomType.NONE)
		{
			ObjUtil.GetChaoAbliltyPhantomFlag(phantomType, ref abilityList);
		}
	}

	// Token: 0x06003EAF RID: 16047 RVA: 0x001455F8 File Offset: 0x001437F8
	public static void GetChaoAbliltyPhantomFlag(PlayerInformation playerInfo, ref List<ChaoAbility> abilityList)
	{
		if (playerInfo)
		{
			PhantomType phantomType = playerInfo.PhantomType;
			if (phantomType != PhantomType.NONE)
			{
				ObjUtil.GetChaoAbliltyPhantomFlag(phantomType, ref abilityList);
			}
		}
	}

	// Token: 0x06003EB0 RID: 16048 RVA: 0x00145628 File Offset: 0x00143828
	public static void GetChaoAbliltyPhantomFlag(PhantomType type, ref List<ChaoAbility> abilityList)
	{
		if (type != PhantomType.NONE)
		{
			abilityList.Add(ChaoAbility.COLOR_POWER_SCORE);
			switch (type)
			{
			case PhantomType.LASER:
				abilityList.Add(ChaoAbility.LASER_SCORE);
				break;
			case PhantomType.DRILL:
				abilityList.Add(ChaoAbility.DRILL_SCORE);
				break;
			case PhantomType.ASTEROID:
				abilityList.Add(ChaoAbility.ASTEROID_SCORE);
				break;
			}
		}
	}

	// Token: 0x06003EB1 RID: 16049 RVA: 0x00145688 File Offset: 0x00143888
	public static bool RequestStartAbilityToChao(ChaoAbility ability, bool withEffect)
	{
		if (StageAbilityManager.Instance != null && StageAbilityManager.Instance.HasChaoAbility(ability))
		{
			MsgChaoAbilityStart msgChaoAbilityStart = new MsgChaoAbilityStart(ability);
			GameObjectUtil.SendMessageToTagObjects("Chao", "OnMsgChaoAbilityStart", msgChaoAbilityStart, SendMessageOptions.DontRequireReceiver);
			if (withEffect)
			{
				StageAbilityManager instance = StageAbilityManager.Instance;
				if (instance != null)
				{
					instance.RequestPlayChaoEffect(ability);
				}
			}
			return msgChaoAbilityStart.m_flag;
		}
		return false;
	}

	// Token: 0x06003EB2 RID: 16050 RVA: 0x001456F4 File Offset: 0x001438F4
	public static void RequestStartAbilityToChao(ChaoAbility[] ability, bool withEffect)
	{
		MsgChaoAbilityStart value = new MsgChaoAbilityStart(ability);
		GameObjectUtil.SendMessageToTagObjects("Chao", "OnMsgChaoAbilityStart", value, SendMessageOptions.DontRequireReceiver);
		if (withEffect)
		{
			StageAbilityManager instance = StageAbilityManager.Instance;
			if (instance != null)
			{
				StageAbilityManager.Instance.RequestPlayChaoEffect(ability);
			}
		}
	}

	// Token: 0x06003EB3 RID: 16051 RVA: 0x0014573C File Offset: 0x0014393C
	public static void RequestEndAbilityToChao(ChaoAbility ability)
	{
		MsgChaoAbilityEnd value = new MsgChaoAbilityEnd(ability);
		GameObjectUtil.SendMessageToTagObjects("Chao", "OnMsgChaoAbilityEnd", value, SendMessageOptions.DontRequireReceiver);
		StageAbilityManager instance = StageAbilityManager.Instance;
		if (instance != null)
		{
			instance.RequestStopChaoEffect(ability);
		}
	}

	// Token: 0x06003EB4 RID: 16052 RVA: 0x0014577C File Offset: 0x0014397C
	public static void RequestEndAbilityToChao(ChaoAbility[] ability)
	{
		MsgChaoAbilityEnd value = new MsgChaoAbilityEnd(ability);
		GameObjectUtil.SendMessageToTagObjects("Chao", "OnMsgChaoAbilityEnd", value, SendMessageOptions.DontRequireReceiver);
		StageAbilityManager instance = StageAbilityManager.Instance;
		if (instance != null)
		{
			instance.RequestStopChaoEffect(ability);
		}
	}

	// Token: 0x06003EB5 RID: 16053 RVA: 0x001457BC File Offset: 0x001439BC
	public static void PushCamera(CameraType type, float interpolateTime)
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("MainCamera");
		if (gameObject)
		{
			CameraManager component = gameObject.GetComponent<CameraManager>();
			if (component)
			{
				MsgPushCamera value = new MsgPushCamera(type, interpolateTime, null);
				component.SendMessage("OnPushCamera", value);
			}
		}
	}

	// Token: 0x06003EB6 RID: 16054 RVA: 0x00145808 File Offset: 0x00143A08
	public static void PopCamera(CameraType type, float interpolateTime)
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("MainCamera");
		if (gameObject)
		{
			CameraManager component = gameObject.GetComponent<CameraManager>();
			if (component)
			{
				MsgPopCamera value = new MsgPopCamera(type, interpolateTime);
				component.SendMessage("OnPopCamera", value);
			}
		}
	}

	// Token: 0x06003EB7 RID: 16055 RVA: 0x00145854 File Offset: 0x00143A54
	public static void SetTextureAnimationSpeed(GameObject obj, float speed)
	{
		if (obj)
		{
			ykTextureSheetAnimation component = obj.GetComponent<ykTextureSheetAnimation>();
			if (component)
			{
				component.SetSpeed(speed);
			}
		}
	}

	// Token: 0x06003EB8 RID: 16056 RVA: 0x00145888 File Offset: 0x00143A88
	public static void CreatePrism()
	{
		StageBlockPathManager stageBlockPathManager = GameObjectUtil.FindGameObjectComponent<StageBlockPathManager>("StageBlockManager");
		if (stageBlockPathManager)
		{
			float? num = null;
			PathComponent curentLaserPath = stageBlockPathManager.GetCurentLaserPath(ref num);
			if (curentLaserPath != null)
			{
				ResPathObject resPathObject = curentLaserPath.GetResPathObject();
				if (resPathObject != null && resPathObject.NumKeys > 0)
				{
					GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.OBJECT_PREFAB, "ObjPrism");
					if (gameObject != null)
					{
						float num2 = 0f;
						PlayerInformation playerInformation = ObjUtil.GetPlayerInformation();
						if (playerInformation)
						{
							num2 = playerInformation.Position.x;
						}
						for (int i = 0; i < resPathObject.NumKeys; i++)
						{
							Vector3 position = resPathObject.GetPosition(i);
							if (position.x > num2)
							{
								GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, position, Quaternion.identity) as GameObject;
								if (gameObject2)
								{
									gameObject2.SetActive(true);
									SpawnableObject component = gameObject2.GetComponent<SpawnableObject>();
									if (component)
									{
										component.AttachModelObject();
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06003EB9 RID: 16057 RVA: 0x001459A8 File Offset: 0x00143BA8
	public static void SendStartItemAndChao()
	{
		if (StageItemManager.Instance != null)
		{
			MsgUseEquipItem msg = new MsgUseEquipItem();
			StageItemManager.Instance.OnUseEquipItem(msg);
		}
		ObjUtil.RequestStartAbilityToChao(ChaoAbility.ADD_COMBO_VALUE, false);
		ObjUtil.RequestStartAbilityToChao(ChaoAbility.COMBO_RECEPTION_TIME, false);
		ObjUtil.RequestStartAbilityToChao(ChaoAbility.ANIMAL_COUNT, false);
		ObjUtil.RequestStartAbilityToChao(ChaoAbility.COMBO_BONUS_UP, false);
		ObjUtil.RequestStartAbilityToChao(ChaoAbility.SUPER_RING_UP, false);
	}

	// Token: 0x06003EBA RID: 16058 RVA: 0x00145A04 File Offset: 0x00143C04
	public static bool IsLightMode()
	{
		LevelInformation levelInformation = ObjUtil.GetLevelInformation();
		return levelInformation != null && levelInformation.LightMode;
	}

	// Token: 0x06003EBB RID: 16059 RVA: 0x00145A2C File Offset: 0x00143C2C
	public static void SendGetItemIcon(ItemType type)
	{
		HudCaution.Instance.SetCaution(new MsgCaution(HudCaution.Type.GET_ITEM, type));
	}

	// Token: 0x06003EBC RID: 16060 RVA: 0x00145A40 File Offset: 0x00143C40
	public static void SendGetTimerIcon(int number, int second)
	{
		HudCaution.Instance.SetCaution(new MsgCaution(HudCaution.Type.GET_TIMER, number, second));
	}

	// Token: 0x06003EBD RID: 16061 RVA: 0x00145A58 File Offset: 0x00143C58
	public static void PauseCombo(MsgPauseComboTimer.State value, float time = -1f)
	{
		if (StageComboManager.Instance != null)
		{
			MsgPauseComboTimer value2 = new MsgPauseComboTimer(value, time);
			GameObjectUtil.SendDelayedMessageToGameObject(StageComboManager.Instance.gameObject, "OnMsgPauseComboTimer", value2);
		}
	}

	// Token: 0x06003EBE RID: 16062 RVA: 0x00145A94 File Offset: 0x00143C94
	public static void StopCombo()
	{
		if (StageComboManager.Instance != null)
		{
			MsgStopCombo value = new MsgStopCombo();
			GameObjectUtil.SendDelayedMessageToGameObject(StageComboManager.Instance.gameObject, "OnMsgStopCombo", value);
		}
	}

	// Token: 0x06003EBF RID: 16063 RVA: 0x00145ACC File Offset: 0x00143CCC
	public static void SetDisableEquipItem(bool disable)
	{
		if (StageItemManager.Instance != null)
		{
			StageItemManager.Instance.OnDisableEquipItem(new MsgDisableEquipItem(disable));
		}
	}

	// Token: 0x06003EC0 RID: 16064 RVA: 0x00145AFC File Offset: 0x00143CFC
	public static void SetQuickModeTimePause(bool pauseFlag)
	{
		if (StageModeManager.Instance != null && StageModeManager.Instance.IsQuickMode() && StageTimeManager.Instance != null)
		{
			if (pauseFlag)
			{
				StageTimeManager.Instance.Pause();
			}
			else
			{
				StageTimeManager.Instance.Resume();
			}
		}
	}

	// Token: 0x06003EC1 RID: 16065 RVA: 0x00145B58 File Offset: 0x00143D58
	public static void CreateSharedMateriaDummyObject(ResourceCategory category, string name)
	{
		GameObject gameObject = ResourceManager.Instance.GetGameObject(category, name);
		if (gameObject != null)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, new Vector3(-100f, -100f, 0f), Quaternion.identity) as GameObject;
			if (gameObject2 != null)
			{
				ykTextureSheetSharedMaterialAnimation component = gameObject2.GetComponent<ykTextureSheetSharedMaterialAnimation>();
				if (component)
				{
					if (component.enabled)
					{
					}
					component.enabled = true;
				}
				gameObject2.SetActive(true);
			}
		}
	}

	// Token: 0x06003EC2 RID: 16066 RVA: 0x00145BE0 File Offset: 0x00143DE0
	private static string GetRingToSuperRing(ResourceManager resourceManager, string objName)
	{
		StageComboManager instance = StageComboManager.Instance;
		if (instance != null && instance.IsActiveComboChaoAbility(ChaoAbility.COMBO_SUPER_RING) && objName == "ObjRing")
		{
			return "ObjSuperRing";
		}
		return objName;
	}

	// Token: 0x06003EC3 RID: 16067 RVA: 0x00145C24 File Offset: 0x00143E24
	private static string GetAllRingToCrystal(ResourceManager resourceManager, LevelInformation levelInfo, string objName)
	{
		if (levelInfo != null && !levelInfo.NowFeverBoss && levelInfo.DestroyRingMode)
		{
			if (objName == "ObjRing")
			{
				return ObjCrystalData.GetCrystalObjectName(CtystalType.SMALL_A);
			}
			if (objName == "ObjSuperRing")
			{
				return ObjCrystalData.GetCrystalObjectName(CtystalType.BIG_A);
			}
		}
		return objName;
	}

	// Token: 0x06003EC4 RID: 16068 RVA: 0x00145C84 File Offset: 0x00143E84
	private static string GetEventCrystal(ResourceManager resourceManager, PlayerInformation playerInfo, string objName)
	{
		if (EventManager.Instance != null && EventManager.Instance.Type == EventManager.EventType.QUICK && StageModeManager.Instance != null && StageModeManager.Instance.IsQuickMode())
		{
			EventObjectTableItem eventObjectTableItem = EventObjectTable.GetEventObjectTableItem(objName);
			if (eventObjectTableItem != EventObjectTableItem.NONE && playerInfo != null)
			{
				int num = EventObjectTable.GetItemData((int)playerInfo.SpeedLevel, eventObjectTableItem);
				if (EventObjectTable.IsCyrstal(eventObjectTableItem))
				{
					StageComboManager instance = StageComboManager.Instance;
					if (instance != null && instance.IsActiveComboChaoAbility(ChaoAbility.COMBO_ALL_SPECIAL_CRYSTAL))
					{
						num = 100;
					}
					else
					{
						num = ObjUtil.GetChaoAbliltyValue(ChaoAbility.SPECIAL_CRYSTAL_RATE, num);
					}
				}
				if (ObjUtil.GetRandomRange100() < num)
				{
					string result = "ObjEventCrystal";
					if (EventObjectTable.IsEventCrystalBig(eventObjectTableItem))
					{
						result = "ObjEventCrystal10";
					}
					return result;
				}
			}
		}
		return objName;
	}

	// Token: 0x06003EC5 RID: 16069 RVA: 0x00145D58 File Offset: 0x00143F58
	public static GameObject GetChangeObject(ResourceManager resourceManager, PlayerInformation playerInfo, LevelInformation levelInfo, string objName)
	{
		string ringToSuperRing = ObjUtil.GetRingToSuperRing(resourceManager, objName);
		string allRingToCrystal = ObjUtil.GetAllRingToCrystal(resourceManager, levelInfo, ringToSuperRing);
		string eventCrystal = ObjUtil.GetEventCrystal(resourceManager, playerInfo, allRingToCrystal);
		if (eventCrystal != objName)
		{
			return resourceManager.GetSpawnableGameObject(eventCrystal);
		}
		return null;
	}

	// Token: 0x06003EC6 RID: 16070 RVA: 0x00145D94 File Offset: 0x00143F94
	public static GameObject GetCrystalChangeObject(ResourceManager resourceManager, GameObject srcObj)
	{
		if (srcObj != null)
		{
			SpawnableObject component = srcObj.GetComponent<SpawnableObject>();
			if (component == null)
			{
				return null;
			}
			CtystalType ctystalType;
			switch (component.GetStockObjectType())
			{
			case StockObjectType.CrystalS_A:
				ctystalType = CtystalType.SMALL_A;
				break;
			case StockObjectType.CrystalS_B:
				ctystalType = CtystalType.SMALL_B;
				break;
			case StockObjectType.CrystalS_C:
				ctystalType = CtystalType.SMALL_C;
				break;
			case StockObjectType.CrystalB_A:
				ctystalType = CtystalType.BIG_A;
				break;
			case StockObjectType.CrystalB_B:
				ctystalType = CtystalType.BIG_B;
				break;
			case StockObjectType.CrystalB_C:
				ctystalType = CtystalType.BIG_C;
				break;
			default:
				return null;
			}
			CtystalType crystalModelType = ObjCrystalUtil.GetCrystalModelType(ctystalType);
			if (crystalModelType != ctystalType)
			{
				return resourceManager.GetSpawnableGameObject(ObjCrystalData.GetCrystalObjectName(crystalModelType));
			}
		}
		return null;
	}

	// Token: 0x06003EC7 RID: 16071 RVA: 0x00145E38 File Offset: 0x00144038
	public static void SetHudStockRingEffectOff(bool off)
	{
		GameObjectUtil.SendMessageFindGameObject("HudCockpit", "OnMsgStockRingEffect", new MsgHudStockRingEffect(off), SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x040035C4 RID: 13764
	private static float PLAYER_SPEED1 = 8f;

	// Token: 0x040035C5 RID: 13765
	private static float PLAYER_SPEED2 = 10f;

	// Token: 0x040035C6 RID: 13766
	private static float PLAYER_SPEED3 = 14f;
}
