using System;
using Message;
using UnityEngine;

// Token: 0x02000878 RID: 2168
public class ObjBossBom : MonoBehaviour
{
	// Token: 0x06003AAA RID: 15018 RVA: 0x0013649C File Offset: 0x0013469C
	private void Update()
	{
		float deltaTime = Time.deltaTime;
		switch (this.m_state)
		{
		case ObjBossBom.State.Start:
			ObjBossUtil.SetupBallAppear(this.m_boss_obj, base.gameObject);
			this.m_state = ObjBossBom.State.Down;
			break;
		case ObjBossBom.State.Down:
			if (this.m_boss_obj && ObjBossUtil.UpdateBallAppear(deltaTime, this.m_boss_obj, base.gameObject, this.m_add_speed) && this.m_shot)
			{
				ObjBossUtil.PlayShotEffect(this.m_boss_obj);
				ObjBossUtil.PlayShotSE();
				this.MotorShot();
				this.m_wait_time = 5f;
				this.m_state = ObjBossBom.State.Wait;
			}
			break;
		case ObjBossBom.State.Bom:
			ObjUtil.PlayEffectCollisionCenter(base.gameObject, this.m_blast_effect_name, this.m_blast_destroy_time, true);
			ObjUtil.PlaySE("obj_common_exp", "SE");
			this.m_time = 0f;
			this.m_wait_time = 0.1f;
			this.m_state = ObjBossBom.State.Wait;
			break;
		case ObjBossBom.State.Wait:
			this.m_time += deltaTime;
			if (this.m_time > this.m_wait_time)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			break;
		}
	}

	// Token: 0x06003AAB RID: 15019 RVA: 0x001365D0 File Offset: 0x001347D0
	public void OnMsgObjectDead(MsgObjectDead msg)
	{
		if (base.enabled && this.m_hit)
		{
			this.SetBroken();
		}
	}

	// Token: 0x06003AAC RID: 15020 RVA: 0x001365F0 File Offset: 0x001347F0
	private void SetBroken()
	{
		this.Blast("ef_bo_em_bom01", 2f);
	}

	// Token: 0x06003AAD RID: 15021 RVA: 0x00136604 File Offset: 0x00134804
	public void Setup(GameObject obj, bool colli, Quaternion shot_rot, float shot_speed, float add_speed, bool shot)
	{
		this.CreateModel();
		ObjUtil.SetModelVisible(base.gameObject, false);
		this.m_hit = colli;
		this.m_boss_obj = obj;
		this.m_shot_rotation = shot_rot;
		this.m_shot_speed = shot_speed;
		this.m_add_speed = add_speed;
		this.m_shot = shot;
		this.m_state = ObjBossBom.State.Start;
	}

	// Token: 0x06003AAE RID: 15022 RVA: 0x00136658 File Offset: 0x00134858
	public void Blast(string name, float destroy_time)
	{
		this.m_blast_effect_name = name;
		this.m_blast_destroy_time = destroy_time;
		this.m_state = ObjBossBom.State.Bom;
	}

	// Token: 0x06003AAF RID: 15023 RVA: 0x00136670 File Offset: 0x00134870
	public void SetShot(bool shot)
	{
		this.m_shot = shot;
	}

	// Token: 0x06003AB0 RID: 15024 RVA: 0x0013667C File Offset: 0x0013487C
	public void MotorShot()
	{
		MotorShot component = base.GetComponent<MotorShot>();
		if (component)
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
			SphereCollider component2 = base.GetComponent<SphereCollider>();
			if (component2)
			{
				this.m_bom_pos_y = component2.radius;
			}
			shotParam.m_bound_pos_y = this.m_bom_pos_y;
			shotParam.m_bound_add_y = 0f;
			component.Setup(shotParam);
		}
	}

	// Token: 0x06003AB1 RID: 15025 RVA: 0x00136758 File Offset: 0x00134958
	private void OnTriggerEnter(Collider other)
	{
		if (other && this.m_hit)
		{
			GameObject gameObject = other.gameObject;
			if (gameObject)
			{
				MsgHitDamage value = new MsgHitDamage(base.gameObject, AttackPower.PlayerColorPower);
				gameObject.SendMessage("OnDamageHit", value, SendMessageOptions.DontRequireReceiver);
				this.SetBroken();
			}
		}
	}

	// Token: 0x06003AB2 RID: 15026 RVA: 0x001367B0 File Offset: 0x001349B0
	private void OnDamageHit(MsgHitDamage msg)
	{
		if (this.m_hit && msg.m_attackPower >= 4 && msg.m_sender)
		{
			GameObject gameObject = msg.m_sender.gameObject;
			if (gameObject)
			{
				this.SetBroken();
			}
		}
	}

	// Token: 0x06003AB3 RID: 15027 RVA: 0x00136804 File Offset: 0x00134A04
	private void CreateModel()
	{
		GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.OBJECT_RESOURCE, "obj_boss_bomb");
		if (gameObject)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, base.transform.position, base.transform.rotation) as GameObject;
			if (gameObject2)
			{
				gameObject2.gameObject.SetActive(true);
				gameObject2.transform.parent = base.gameObject.transform;
				gameObject2.transform.localRotation = Quaternion.Euler(Vector3.zero);
			}
		}
	}

	// Token: 0x06003AB4 RID: 15028 RVA: 0x00136894 File Offset: 0x00134A94
	public void OnMsgNotifyDead(MsgNotifyDead msg)
	{
		this.m_playerDead = true;
	}

	// Token: 0x040032A8 RID: 12968
	private const string MODEL_NAME = "obj_boss_bomb";

	// Token: 0x040032A9 RID: 12969
	private const ResourceCategory MODEL_CATEGORY = ResourceCategory.OBJECT_RESOURCE;

	// Token: 0x040032AA RID: 12970
	private const float BOM_END_TIME = 0.1f;

	// Token: 0x040032AB RID: 12971
	private const float WAIT_END_TIME = 5f;

	// Token: 0x040032AC RID: 12972
	private const float BALL_GRAVITY = -6.1f;

	// Token: 0x040032AD RID: 12973
	private ObjBossBom.State m_state;

	// Token: 0x040032AE RID: 12974
	private float m_time;

	// Token: 0x040032AF RID: 12975
	private bool m_hit;

	// Token: 0x040032B0 RID: 12976
	private GameObject m_boss_obj;

	// Token: 0x040032B1 RID: 12977
	private Quaternion m_shot_rotation = Quaternion.identity;

	// Token: 0x040032B2 RID: 12978
	private float m_shot_speed;

	// Token: 0x040032B3 RID: 12979
	private float m_bom_pos_y;

	// Token: 0x040032B4 RID: 12980
	private float m_wait_time;

	// Token: 0x040032B5 RID: 12981
	private string m_blast_effect_name = string.Empty;

	// Token: 0x040032B6 RID: 12982
	private float m_blast_destroy_time;

	// Token: 0x040032B7 RID: 12983
	private float m_add_speed = 1f;

	// Token: 0x040032B8 RID: 12984
	private bool m_shot;

	// Token: 0x040032B9 RID: 12985
	private bool m_playerDead;

	// Token: 0x02000879 RID: 2169
	private enum State
	{
		// Token: 0x040032BB RID: 12987
		Idle,
		// Token: 0x040032BC RID: 12988
		Start,
		// Token: 0x040032BD RID: 12989
		Down,
		// Token: 0x040032BE RID: 12990
		Bom,
		// Token: 0x040032BF RID: 12991
		Wait
	}
}
