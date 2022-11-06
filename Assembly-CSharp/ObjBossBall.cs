using System;
using Message;
using UnityEngine;

// Token: 0x02000875 RID: 2165
public class ObjBossBall : MonoBehaviour
{
	// Token: 0x06003A9B RID: 15003 RVA: 0x00135D24 File Offset: 0x00133F24
	private void Update()
	{
		float deltaTime = Time.deltaTime;
		switch (this.m_state)
		{
		case ObjBossBall.State.Start:
			if (this.m_bossAppear)
			{
				ObjBossUtil.SetupBallAppear(this.m_boss_obj, base.gameObject);
			}
			this.m_state = ObjBossBall.State.Down;
			break;
		case ObjBossBall.State.Down:
			if (this.m_boss_obj)
			{
				if (this.m_bossAppear)
				{
					if (ObjBossUtil.UpdateBallAppear(deltaTime, this.m_boss_obj, base.gameObject, this.m_add_speed))
					{
						ObjBossUtil.PlayShotEffect(this.m_boss_obj);
						ObjBossUtil.PlayShotSE();
						this.MotorShot();
						this.m_time = 0f;
						this.m_state = ObjBossBall.State.Bound;
					}
				}
				else
				{
					this.MotorShot();
					this.m_time = 0f;
					this.m_state = ObjBossBall.State.Bound;
				}
			}
			break;
		case ObjBossBall.State.Bound:
			this.m_time += deltaTime;
			if (this.m_time > 5f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			break;
		case ObjBossBall.State.Attack:
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

	// Token: 0x06003A9C RID: 15004 RVA: 0x00135EB8 File Offset: 0x001340B8
	public void OnMsgObjectDead(MsgObjectDead msg)
	{
		if (base.enabled)
		{
			this.SetBroken();
		}
	}

	// Token: 0x06003A9D RID: 15005 RVA: 0x00135ECC File Offset: 0x001340CC
	private void SetBroken()
	{
		ObjUtil.PlayEffectCollisionCenter(base.gameObject, "ef_com_explosion_s01", 1f, false);
		ObjUtil.PlaySE("obj_brk", "SE");
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06003A9E RID: 15006 RVA: 0x00135F00 File Offset: 0x00134100
	public void Setup(ObjBossBall.SetData setData)
	{
		this.CreateModel(setData.type);
		this.m_state = ObjBossBall.State.Start;
		this.m_boss_obj = setData.obj;
		this.m_bound_param = setData.bound_param;
		this.m_type = setData.type;
		this.m_shot_rotation = setData.shot_rot;
		this.m_shot_speed = setData.shot_speed;
		this.m_attack_speed = setData.attack_speed;
		this.m_firstSpeed = setData.firstSpeed;
		this.m_outOfcontrol = setData.outOfcontrol;
		this.m_ballSpeed = setData.ballSpeed;
		this.m_bossAppear = setData.bossAppear;
		if (this.m_bossAppear)
		{
			ObjUtil.SetModelVisible(base.gameObject, false);
		}
	}

	// Token: 0x06003A9F RID: 15007 RVA: 0x00135FBC File Offset: 0x001341BC
	public void MotorShot()
	{
		this.m_motor_cmp = base.GetComponent<MotorShot>();
		if (this.m_motor_cmp)
		{
			MotorShot.ShotParam shotParam = new MotorShot.ShotParam();
			shotParam.m_obj = base.gameObject;
			shotParam.m_gravity = -6.1f;
			shotParam.m_rot_speed = 0f;
			shotParam.m_rot_downspeed = 0f;
			shotParam.m_rot_angle = Vector3.zero;
			shotParam.m_shot_rotation = ObjBossUtil.GetShotRotation(this.m_shot_rotation, this.m_playerDead);
			shotParam.m_shot_time = 1f;
			shotParam.m_shot_speed = this.m_shot_speed;
			shotParam.m_shot_downspeed = 0f;
			shotParam.m_bound = true;
			SphereCollider component = base.GetComponent<SphereCollider>();
			if (component)
			{
				shotParam.m_bound_pos_y = component.radius;
			}
			shotParam.m_bound_add_y = Mathf.Max(this.m_bound_param, 0f);
			shotParam.m_bound_down_x = 0f;
			shotParam.m_bound_down_y = 0.01f;
			shotParam.m_after_speed = this.m_ballSpeed;
			shotParam.m_after_add_x = 0f;
			shotParam.m_after_up = base.transform.up;
			shotParam.m_after_forward = base.transform.right;
			this.m_motor_cmp.Setup(shotParam);
		}
	}

	// Token: 0x06003AA0 RID: 15008 RVA: 0x001360F4 File Offset: 0x001342F4
	private void StartAttack()
	{
		this.m_time = 0f;
		this.m_state = ObjBossBall.State.Attack;
		ObjUtil.PlaySE("boss_counterattack", "SE");
		if (this.m_motor_cmp)
		{
			this.m_motor_cmp.SetEnd();
		}
	}

	// Token: 0x06003AA1 RID: 15009 RVA: 0x00136134 File Offset: 0x00134334
	private void HitAttack(GameObject obj)
	{
		if (obj)
		{
			AttackPower myPower = ObjBossBall.GetMyPower(this.m_type);
			MsgHitDamage value = new MsgHitDamage(base.gameObject, myPower);
			obj.SendMessage("OnDamageHit", value, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06003AA2 RID: 15010 RVA: 0x00136174 File Offset: 0x00134374
	private void OnTriggerEnter(Collider other)
	{
		if (this.m_type == BossBallType.BUMPER)
		{
			MsgOnSpringImpulse msgOnSpringImpulse = new MsgOnSpringImpulse(base.transform.position, base.transform.rotation, this.m_firstSpeed, this.m_outOfcontrol);
			other.gameObject.SendMessage("OnSpringImpulse", msgOnSpringImpulse, SendMessageOptions.DontRequireReceiver);
			if (this.m_boss_obj != null)
			{
				this.m_boss_obj.SendMessage("OnHitBumper", SendMessageOptions.DontRequireReceiver);
			}
			if (msgOnSpringImpulse.m_succeed)
			{
				if (this.m_model_obj != null)
				{
					Animation componentInChildren = this.m_model_obj.GetComponentInChildren<Animation>();
					if (componentInChildren)
					{
						componentInChildren.wrapMode = WrapMode.Once;
						componentInChildren.Play("obj_boss_bumper_bounce");
					}
				}
				ObjUtil.PlaySE("obj_spring", "SE");
			}
		}
		else if (other)
		{
			GameObject gameObject = other.gameObject;
			if (gameObject)
			{
				this.HitAttack(gameObject);
			}
		}
	}

	// Token: 0x06003AA3 RID: 15011 RVA: 0x00136268 File Offset: 0x00134468
	private void OnDamageHit(MsgHitDamage msg)
	{
		if (this.m_type == BossBallType.BUMPER)
		{
			return;
		}
		if (ObjBossBall.IsAttackPower(this.m_type, msg.m_attackPower) && msg.m_sender)
		{
			GameObject gameObject = msg.m_sender.gameObject;
			if (gameObject)
			{
				MsgHitDamageSucceed value = new MsgHitDamageSucceed(base.gameObject, 0, ObjUtil.GetCollisionCenterPosition(base.gameObject), base.transform.rotation);
				gameObject.SendMessage("OnDamageSucceed", value, SendMessageOptions.DontRequireReceiver);
				if (ObjBossBall.IsBrokenPower(this.m_type, msg.m_attackPower) || gameObject.name == "ChaoPartsAttackEnemy")
				{
					this.SetBroken();
				}
				else
				{
					this.StartAttack();
				}
			}
		}
	}

	// Token: 0x06003AA4 RID: 15012 RVA: 0x0013632C File Offset: 0x0013452C
	private static bool IsAttackPower(BossBallType type, int plPower)
	{
		AttackPower attackPower = AttackPower.PlayerSpin;
		if (type == BossBallType.TRAP)
		{
			attackPower = AttackPower.PlayerColorPower;
		}
		return plPower >= (int)attackPower;
	}

	// Token: 0x06003AA5 RID: 15013 RVA: 0x0013635C File Offset: 0x0013455C
	private static bool IsBrokenPower(BossBallType type, int plPower)
	{
		if (type == BossBallType.ATTACK)
		{
			if (plPower == 5)
			{
				return false;
			}
		}
		return plPower >= 4;
	}

	// Token: 0x06003AA6 RID: 15014 RVA: 0x00136390 File Offset: 0x00134590
	private static AttackPower GetMyPower(BossBallType type)
	{
		AttackPower result = AttackPower.PlayerSpin;
		if (type == BossBallType.TRAP)
		{
			result = AttackPower.PlayerColorPower;
		}
		return result;
	}

	// Token: 0x06003AA7 RID: 15015 RVA: 0x001363B8 File Offset: 0x001345B8
	private void CreateModel(BossBallType type)
	{
		if (type < (BossBallType)ObjBossBall.MODEL_FILES.Length)
		{
			string name = ObjBossBall.MODEL_FILES[(int)type];
			GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.OBJECT_RESOURCE, name);
			if (gameObject)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, base.transform.position, base.transform.rotation) as GameObject;
				if (gameObject2)
				{
					ObjUtil.StopAnimation(gameObject2);
					gameObject2.gameObject.SetActive(true);
					gameObject2.transform.parent = base.gameObject.transform;
					gameObject2.transform.localRotation = Quaternion.Euler(Vector3.zero);
					this.m_model_obj = gameObject2;
				}
			}
		}
	}

	// Token: 0x06003AA8 RID: 15016 RVA: 0x00136464 File Offset: 0x00134664
	public void OnMsgNotifyDead(MsgNotifyDead msg)
	{
		this.m_playerDead = true;
	}

	// Token: 0x04003283 RID: 12931
	private const ResourceCategory MODEL_CATEGORY = ResourceCategory.OBJECT_RESOURCE;

	// Token: 0x04003284 RID: 12932
	private const float END_TIME = 5f;

	// Token: 0x04003285 RID: 12933
	private const float BALL_GRAVITY = -6.1f;

	// Token: 0x04003286 RID: 12934
	private const float ATTACK_ROT_SPEED = 10f;

	// Token: 0x04003287 RID: 12935
	private static readonly string[] MODEL_FILES = new string[]
	{
		"obj_boss_ironball",
		"obj_boss_thornball",
		"obj_boss_bumper"
	};

	// Token: 0x04003288 RID: 12936
	private ObjBossBall.State m_state;

	// Token: 0x04003289 RID: 12937
	private float m_time;

	// Token: 0x0400328A RID: 12938
	private float m_bound_param;

	// Token: 0x0400328B RID: 12939
	private BossBallType m_type;

	// Token: 0x0400328C RID: 12940
	private GameObject m_boss_obj;

	// Token: 0x0400328D RID: 12941
	private GameObject m_model_obj;

	// Token: 0x0400328E RID: 12942
	private MotorShot m_motor_cmp;

	// Token: 0x0400328F RID: 12943
	private Quaternion m_shot_rotation = Quaternion.identity;

	// Token: 0x04003290 RID: 12944
	private float m_shot_speed;

	// Token: 0x04003291 RID: 12945
	private float m_attack_speed;

	// Token: 0x04003292 RID: 12946
	private float m_add_speed = 1f;

	// Token: 0x04003293 RID: 12947
	private float m_firstSpeed;

	// Token: 0x04003294 RID: 12948
	private float m_outOfcontrol;

	// Token: 0x04003295 RID: 12949
	private bool m_playerDead;

	// Token: 0x04003296 RID: 12950
	private float m_ballSpeed;

	// Token: 0x04003297 RID: 12951
	private bool m_bossAppear;

	// Token: 0x02000876 RID: 2166
	public struct SetData
	{
		// Token: 0x04003298 RID: 12952
		public GameObject obj;

		// Token: 0x04003299 RID: 12953
		public float bound_param;

		// Token: 0x0400329A RID: 12954
		public BossBallType type;

		// Token: 0x0400329B RID: 12955
		public Quaternion shot_rot;

		// Token: 0x0400329C RID: 12956
		public float shot_speed;

		// Token: 0x0400329D RID: 12957
		public float attack_speed;

		// Token: 0x0400329E RID: 12958
		public float firstSpeed;

		// Token: 0x0400329F RID: 12959
		public float outOfcontrol;

		// Token: 0x040032A0 RID: 12960
		public float ballSpeed;

		// Token: 0x040032A1 RID: 12961
		public bool bossAppear;
	}

	// Token: 0x02000877 RID: 2167
	private enum State
	{
		// Token: 0x040032A3 RID: 12963
		Idle,
		// Token: 0x040032A4 RID: 12964
		Start,
		// Token: 0x040032A5 RID: 12965
		Down,
		// Token: 0x040032A6 RID: 12966
		Bound,
		// Token: 0x040032A7 RID: 12967
		Attack
	}
}
