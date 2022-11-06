using System;
using Message;
using UnityEngine;

// Token: 0x0200087D RID: 2173
public class ObjBossTrapBall : MonoBehaviour
{
	// Token: 0x06003AC0 RID: 15040 RVA: 0x00136C74 File Offset: 0x00134E74
	private void Update()
	{
		float deltaTime = Time.deltaTime;
		switch (this.m_state)
		{
		case ObjBossTrapBall.State.Start:
			if (this.m_bossAppear)
			{
				ObjBossUtil.SetupBallAppear(this.m_boss_obj, base.gameObject);
			}
			this.m_state = ObjBossTrapBall.State.Down;
			break;
		case ObjBossTrapBall.State.Down:
			if (this.m_boss_obj)
			{
				if (this.m_bossAppear)
				{
					if (ObjBossUtil.UpdateBallAppear(deltaTime, this.m_boss_obj, base.gameObject, this.m_add_speed))
					{
						ObjBossUtil.PlayShotEffect(this.m_boss_obj);
						ObjBossUtil.PlayShotSE();
						this.Shot(this.m_colli);
						this.m_time = 0f;
						this.m_state = ObjBossTrapBall.State.Wait;
					}
				}
				else
				{
					this.Shot(this.m_colli);
					this.m_time = 0f;
					this.m_state = ObjBossTrapBall.State.Wait;
				}
			}
			break;
		case ObjBossTrapBall.State.Wait:
			if (this.m_colli_model_obj != null)
			{
				for (int i = 0; i < this.m_colli_model_obj.Length; i++)
				{
					if (this.m_colli_model_obj[i] != null)
					{
						ObjBossUtil.UpdateBallRot(deltaTime, this.m_colli_model_obj[i], ObjBossTrapBall.TRAP_TYPE_BALLROT, this.m_rot_speed);
					}
				}
			}
			this.m_time += deltaTime;
			if (this.m_time > 5f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			break;
		case ObjBossTrapBall.State.Attack:
			if (this.m_boss_obj)
			{
				this.m_time += deltaTime;
				ObjBossUtil.UpdateBallAttack(this.m_boss_obj, base.gameObject, this.m_time, this.m_attack_speed);
				float num = Mathf.Abs(Vector3.Distance(base.transform.position, this.m_boss_obj.transform.position));
				if (num < 0.1f)
				{
					this.HitAttack(this.m_boss_obj);
					this.SetBroken();
				}
			}
			break;
		}
	}

	// Token: 0x06003AC1 RID: 15041 RVA: 0x00136E64 File Offset: 0x00135064
	public void OnMsgObjectDead(MsgObjectDead msg)
	{
		if (base.enabled)
		{
			this.SetBroken();
		}
	}

	// Token: 0x06003AC2 RID: 15042 RVA: 0x00136E78 File Offset: 0x00135078
	private void SetBroken()
	{
		ObjUtil.PlayEffectCollisionCenter(base.gameObject, this.m_typeParam.m_effectName, 1f, false);
		ObjUtil.PlaySE("obj_brk", "SE");
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06003AC3 RID: 15043 RVA: 0x00136EB4 File Offset: 0x001350B4
	public void Setup(GameObject obj, Vector3 colli, float rot_speed, float attack_speed, BossTrapBallType type, BossType bossType, bool bossAppear)
	{
		this.m_boss_obj = obj;
		this.m_colli = colli;
		this.m_rot_speed = rot_speed;
		this.m_attack_speed = attack_speed;
		this.m_type = type;
		this.m_bossAppear = bossAppear;
		if (BossTypeUtil.GetBossCharaType(bossType) != BossCharaType.EGGMAN)
		{
			this.m_typeParam.m_modelName1 = EventBossObjectTable.GetItemData(EventBossObjectTableItem.Obj2_ModelName);
			this.m_typeParam.m_resCategory = ResourceCategory.EVENT_RESOURCE;
			this.m_typeParam.m_effectName = EventBossObjectTable.GetItemData(EventBossObjectTableItem.Obj2_EffectName);
		}
		this.m_typeParam.m_modelName2 = this.m_typeParam.m_modelName1 + "brk";
		this.m_typeParam.m_modelNameLeft = this.m_typeParam.m_modelName1 + "_left";
		this.m_typeParam.m_modelNameRight = this.m_typeParam.m_modelName1 + "_right";
		this.m_typeParam.m_modelNameTop = this.m_typeParam.m_modelName1 + "_top";
		this.m_typeParam.m_modelNameUnder = this.m_typeParam.m_modelName1 + "_under";
		this.CreateModel(this.m_type);
		if (this.m_bossAppear)
		{
			ObjUtil.SetModelVisible(base.gameObject, false);
		}
		this.m_time = 0f;
		this.m_state = ObjBossTrapBall.State.Start;
	}

	// Token: 0x06003AC4 RID: 15044 RVA: 0x00137000 File Offset: 0x00135200
	public void Shot(Vector3 colli)
	{
		if (this.m_model_obj)
		{
			Animator componentInChildren = base.GetComponentInChildren<Animator>();
			if (componentInChildren != null)
			{
				if (colli.x > 0f)
				{
					componentInChildren.Play(this.m_typeParam.m_modelName1 + "_width");
				}
				else
				{
					componentInChildren.Play(this.m_typeParam.m_modelName1 + "_length");
				}
			}
		}
		if (this.m_colli_obj == null)
		{
			this.m_colli_obj = new GameObject[2];
		}
		if (this.m_colli_model_obj == null)
		{
			this.m_colli_model_obj = new GameObject[2];
		}
		for (int i = 0; i < 2; i++)
		{
			if (colli.x > 0f)
			{
				string name = (i != 0) ? this.m_typeParam.m_modelNameRight : this.m_typeParam.m_modelNameLeft;
				this.m_colli_obj[i] = this.CreateCollision(name);
				this.m_colli_model_obj[i] = GameObjectUtil.FindChildGameObject(base.gameObject, name);
			}
			else
			{
				string name2 = (i != 0) ? this.m_typeParam.m_modelNameUnder : this.m_typeParam.m_modelNameTop;
				this.m_colli_obj[i] = this.CreateCollision(name2);
				this.m_colli_model_obj[i] = GameObjectUtil.FindChildGameObject(base.gameObject, name2);
			}
		}
	}

	// Token: 0x06003AC5 RID: 15045 RVA: 0x0013715C File Offset: 0x0013535C
	private void StartAttack()
	{
		if (this.m_colli_obj != null)
		{
			for (int i = 0; i < this.m_colli_obj.Length; i++)
			{
				if (this.m_colli_obj[i] != null)
				{
					UnityEngine.Object.Destroy(this.m_colli_obj[i].gameObject);
				}
			}
		}
		ObjUtil.PlaySE("boss_counterattack", "SE");
		this.m_time = 0f;
		this.m_rot_speed = 25f;
		this.m_state = ObjBossTrapBall.State.Attack;
	}

	// Token: 0x06003AC6 RID: 15046 RVA: 0x001371E0 File Offset: 0x001353E0
	private void HitAttack(GameObject obj)
	{
		if (obj)
		{
			MsgHitDamage value = new MsgHitDamage(base.gameObject, AttackPower.PlayerSpin);
			obj.SendMessage("OnDamageHit", value, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06003AC7 RID: 15047 RVA: 0x00137214 File Offset: 0x00135414
	private void OnTriggerEnter(Collider other)
	{
		if (other)
		{
			GameObject gameObject = other.gameObject;
			if (gameObject)
			{
				this.HitAttack(gameObject);
			}
		}
	}

	// Token: 0x06003AC8 RID: 15048 RVA: 0x00137248 File Offset: 0x00135448
	private void OnDamageHit(MsgHitDamage msg)
	{
		if (msg.m_attackPower > 0 && msg.m_sender)
		{
			GameObject gameObject = msg.m_sender.gameObject;
			if (gameObject)
			{
				MsgHitDamageSucceed value = new MsgHitDamageSucceed(base.gameObject, 0, ObjUtil.GetCollisionCenterPosition(base.gameObject), base.transform.rotation);
				gameObject.SendMessage("OnDamageSucceed", value, SendMessageOptions.DontRequireReceiver);
				if (ObjBossTrapBall.IsBrokenPower(this.m_type, msg.m_attackPower) || gameObject.name == "ChaoPartsAttackEnemy")
				{
					this.SetBroken();
				}
				else if (this.m_type == BossTrapBallType.ATTACK)
				{
					this.StartAttack();
				}
			}
		}
	}

	// Token: 0x06003AC9 RID: 15049 RVA: 0x00137300 File Offset: 0x00135500
	private static bool IsBrokenPower(BossTrapBallType type, int plPower)
	{
		if (type != BossTrapBallType.ATTACK)
		{
			if (type == BossTrapBallType.BREAK)
			{
				return true;
			}
		}
		else
		{
			if (plPower == 5)
			{
				return false;
			}
			if (plPower >= 4)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06003ACA RID: 15050 RVA: 0x0013733C File Offset: 0x0013553C
	private void CreateModel(BossTrapBallType type)
	{
		string name = (type != BossTrapBallType.ATTACK) ? this.m_typeParam.m_modelName2 : this.m_typeParam.m_modelName1;
		GameObject gameObject = ResourceManager.Instance.GetGameObject(this.m_typeParam.m_resCategory, name);
		if (gameObject)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, base.transform.position, base.transform.rotation) as GameObject;
			if (gameObject2)
			{
				gameObject2.gameObject.SetActive(true);
				gameObject2.transform.parent = base.gameObject.transform;
				gameObject2.transform.localRotation = Quaternion.Euler(Vector3.zero);
				this.m_model_obj = gameObject2;
			}
		}
	}

	// Token: 0x06003ACB RID: 15051 RVA: 0x001373F8 File Offset: 0x001355F8
	private GameObject CreateCollision(string name)
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, name);
		if (gameObject)
		{
			GameObject gameObject2 = ResourceManager.Instance.GetGameObject(ResourceCategory.ENEMY_PREFAB, "ObjBossTrapBallCollision");
			if (gameObject2)
			{
				GameObject gameObject3 = UnityEngine.Object.Instantiate(gameObject2, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
				if (gameObject3)
				{
					gameObject3.gameObject.SetActive(true);
					gameObject3.transform.parent = gameObject.transform;
					return gameObject3;
				}
			}
		}
		return null;
	}

	// Token: 0x040032CF RID: 13007
	private const float END_TIME = 5f;

	// Token: 0x040032D0 RID: 13008
	private const float ATTACK_ROT_SPEED = 25f;

	// Token: 0x040032D1 RID: 13009
	private const int COLLI_NUM = 2;

	// Token: 0x040032D2 RID: 13010
	private static Vector3 TRAP_TYPE_BALLROT = new Vector3(0f, 0f, 1f);

	// Token: 0x040032D3 RID: 13011
	private ObjBossTrapBall.State m_state;

	// Token: 0x040032D4 RID: 13012
	private float m_time;

	// Token: 0x040032D5 RID: 13013
	private GameObject m_boss_obj;

	// Token: 0x040032D6 RID: 13014
	private GameObject m_model_obj;

	// Token: 0x040032D7 RID: 13015
	private GameObject[] m_colli_obj;

	// Token: 0x040032D8 RID: 13016
	private GameObject[] m_colli_model_obj;

	// Token: 0x040032D9 RID: 13017
	private Vector3 m_colli = Vector3.zero;

	// Token: 0x040032DA RID: 13018
	private float m_rot_speed;

	// Token: 0x040032DB RID: 13019
	private float m_attack_speed;

	// Token: 0x040032DC RID: 13020
	private float m_add_speed = 3f;

	// Token: 0x040032DD RID: 13021
	private BossTrapBallType m_type;

	// Token: 0x040032DE RID: 13022
	private bool m_bossAppear;

	// Token: 0x040032DF RID: 13023
	private ObjBossTrapBall.BossTrapBallModelTypeParam m_typeParam = new ObjBossTrapBall.BossTrapBallModelTypeParam("obj_boss_movetrap", string.Empty, ResourceCategory.OBJECT_RESOURCE, "ef_com_explosion_m01", string.Empty, string.Empty, string.Empty, string.Empty);

	// Token: 0x0200087E RID: 2174
	private class BossTrapBallModelTypeParam
	{
		// Token: 0x06003ACC RID: 15052 RVA: 0x00137488 File Offset: 0x00135688
		public BossTrapBallModelTypeParam(string model1, string model2, ResourceCategory resCategory, string effect, string modelL, string modelR, string modelT, string modelU)
		{
			this.m_modelName1 = model1;
			this.m_modelName2 = model2;
			this.m_resCategory = resCategory;
			this.m_effectName = effect;
			this.m_modelNameLeft = modelL;
			this.m_modelNameRight = modelR;
			this.m_modelNameTop = modelT;
			this.m_modelNameUnder = modelU;
		}

		// Token: 0x040032E0 RID: 13024
		public string m_modelName1;

		// Token: 0x040032E1 RID: 13025
		public string m_modelName2;

		// Token: 0x040032E2 RID: 13026
		public ResourceCategory m_resCategory;

		// Token: 0x040032E3 RID: 13027
		public string m_effectName;

		// Token: 0x040032E4 RID: 13028
		public string m_modelNameLeft;

		// Token: 0x040032E5 RID: 13029
		public string m_modelNameRight;

		// Token: 0x040032E6 RID: 13030
		public string m_modelNameTop;

		// Token: 0x040032E7 RID: 13031
		public string m_modelNameUnder;
	}

	// Token: 0x0200087F RID: 2175
	private enum State
	{
		// Token: 0x040032E9 RID: 13033
		Idle,
		// Token: 0x040032EA RID: 13034
		Start,
		// Token: 0x040032EB RID: 13035
		Down,
		// Token: 0x040032EC RID: 13036
		Wait,
		// Token: 0x040032ED RID: 13037
		Attack
	}
}
