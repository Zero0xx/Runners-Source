using System;
using Message;
using Player;
using UnityEngine;

// Token: 0x020008C6 RID: 2246
[AddComponentMenu("Scripts/Runners/Object/Common/ObjCannon")]
public class ObjCannon : SpawnableObject
{
	// Token: 0x06003BD1 RID: 15313 RVA: 0x0013BF30 File Offset: 0x0013A130
	protected override string GetModelName()
	{
		return "obj_cmn_cannon";
	}

	// Token: 0x06003BD2 RID: 15314 RVA: 0x0013BF38 File Offset: 0x0013A138
	protected override ResourceCategory GetModelCategory()
	{
		return ResourceCategory.OBJECT_RESOURCE;
	}

	// Token: 0x06003BD3 RID: 15315 RVA: 0x0013BF3C File Offset: 0x0013A13C
	protected virtual string GetShotEffectName()
	{
		return "ef_ob_canon_st01";
	}

	// Token: 0x06003BD4 RID: 15316 RVA: 0x0013BF44 File Offset: 0x0013A144
	protected virtual string GetShotAnimName()
	{
		return "obj_cannon_shot";
	}

	// Token: 0x06003BD5 RID: 15317 RVA: 0x0013BF4C File Offset: 0x0013A14C
	protected virtual bool IsRoulette()
	{
		return false;
	}

	// Token: 0x06003BD6 RID: 15318 RVA: 0x0013BF50 File Offset: 0x0013A150
	protected override void OnSpawned()
	{
		this.m_input = base.GetComponent<CharacterInput>();
		ObjUtil.StopAnimation(base.gameObject);
		this.m_centerRotation = Quaternion.Euler(0f, 0f, -45f) * base.transform.rotation;
	}

	// Token: 0x06003BD7 RID: 15319 RVA: 0x0013BFA0 File Offset: 0x0013A1A0
	private void Update()
	{
		float deltaTime = Time.deltaTime;
		switch (this.m_state)
		{
		case ObjCannon.State.Start:
			this.SetGuideline(false);
			this.m_state = ObjCannon.State.Idle;
			break;
		case ObjCannon.State.Set:
			this.UpdateStart(deltaTime);
			break;
		case ObjCannon.State.Shot:
			this.UpdateMove(deltaTime);
			break;
		}
		this.UpdateInputShot();
	}

	// Token: 0x06003BD8 RID: 15320 RVA: 0x0013C00C File Offset: 0x0013A20C
	public void SetObjCannonParameter(ObjCannonParameter param)
	{
		this.m_firstSpeed = param.firstSpeed;
		this.m_outOfcontrol = param.outOfcontrol;
		this.m_moveSpeed = param.moveSpeed;
		this.m_rotArea = param.moveArea * 0.5f;
	}

	// Token: 0x06003BD9 RID: 15321 RVA: 0x0013C050 File Offset: 0x0013A250
	protected virtual Quaternion GetStartRot()
	{
		return Quaternion.Euler(0f, 0f, -this.m_rotArea) * this.m_centerRotation;
	}

	// Token: 0x06003BDA RID: 15322 RVA: 0x0013C074 File Offset: 0x0013A274
	private void OnTriggerEnter(Collider other)
	{
		if (this.m_state == ObjCannon.State.Idle)
		{
			GameObject gameObject = other.gameObject;
			if (gameObject)
			{
				Quaternion rot = Quaternion.FromToRotation(base.transform.up, base.transform.right) * base.transform.rotation;
				MsgOnAbidePlayer msgOnAbidePlayer = new MsgOnAbidePlayer(base.transform.position, rot, 1f, base.gameObject);
				gameObject.SendMessage("OnAbidePlayer", msgOnAbidePlayer, SendMessageOptions.DontRequireReceiver);
				if (msgOnAbidePlayer.m_succeed)
				{
					ObjUtil.PlaySE("obj_cannon_in", "SE");
					this.m_sendObject = gameObject;
					this.m_state = ObjCannon.State.Wait;
				}
			}
		}
	}

	// Token: 0x06003BDB RID: 15323 RVA: 0x0013C120 File Offset: 0x0013A320
	private void UpdateStart(float delta)
	{
		this.m_time += delta;
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.m_startRotation, this.m_time * this.m_moveSpeed * 0.5f * Time.timeScale);
		float num = Mathf.Abs(Vector3.Distance(base.transform.rotation.eulerAngles, this.m_startRotation.eulerAngles));
		if (num < 0.1f)
		{
			this.m_time = 0f;
			this.m_state = ObjCannon.State.Shot;
		}
	}

	// Token: 0x06003BDC RID: 15324 RVA: 0x0013C1BC File Offset: 0x0013A3BC
	private void UpdateMove(float delta)
	{
		if (this.m_rotArea > 0f)
		{
			this.m_time += delta;
			float num = Mathf.Cos(6.2831855f * this.m_time * this.m_moveSpeed) * -1f;
			base.transform.rotation = Quaternion.Euler(0f, 0f, num * this.m_rotArea) * this.m_centerRotation;
		}
	}

	// Token: 0x06003BDD RID: 15325 RVA: 0x0013C234 File Offset: 0x0013A434
	private void UpdateInputShot()
	{
		if (this.m_shot && this.m_input && this.m_input.IsTouched())
		{
			this.Shot();
			this.SetGuideline(false);
			this.m_shot = false;
			this.m_state = ObjCannon.State.Idle;
		}
	}

	// Token: 0x06003BDE RID: 15326 RVA: 0x0013C288 File Offset: 0x0013A488
	private void Shot()
	{
		if (this.m_sendObject)
		{
			Quaternion rot = Quaternion.FromToRotation(base.transform.up, base.transform.right) * base.transform.rotation;
			if (!this.IsRoulette())
			{
				float num = rot.eulerAngles.z + 2.5f;
				if (num != 0f)
				{
					int num2 = (int)(num / 5f);
					rot = Quaternion.Euler(rot.eulerAngles.x, rot.eulerAngles.y, 5f * (float)num2);
				}
			}
			MsgOnCannonImpulse value = new MsgOnCannonImpulse(base.transform.position, rot, this.m_firstSpeed, this.m_outOfcontrol, this.IsRoulette());
			this.m_sendObject.SendMessage("OnCannonImpulse", value, SendMessageOptions.DontRequireReceiver);
			Animation componentInChildren = base.GetComponentInChildren<Animation>();
			if (componentInChildren)
			{
				componentInChildren.wrapMode = WrapMode.Once;
				componentInChildren.Play(this.GetShotAnimName());
			}
			ObjUtil.PlayEffectChild(base.gameObject, this.GetShotEffectName(), new Vector3(1f, 0f, 0f), Quaternion.Euler(new Vector3(0f, 0f, -90f)), 1f, true);
			ObjUtil.PlaySE("obj_cannon_shoot", "SE");
		}
		ObjUtil.PopCamera(CameraType.CANNON, 0.5f);
	}

	// Token: 0x06003BDF RID: 15327 RVA: 0x0013C3F4 File Offset: 0x0013A5F4
	private void OnAbidePlayerLocked(MsgOnAbidePlayerLocked msg)
	{
		this.SetGuideline(true);
		this.m_startRotation = this.GetStartRot();
		this.m_state = ObjCannon.State.Set;
		this.m_shot = true;
		ObjUtil.PushCamera(CameraType.CANNON, 0.5f);
	}

	// Token: 0x06003BE0 RID: 15328 RVA: 0x0013C430 File Offset: 0x0013A630
	private void OnExitAbideObject(MsgOnExitAbideObject msg)
	{
		this.m_state = ObjCannon.State.Idle;
	}

	// Token: 0x06003BE1 RID: 15329 RVA: 0x0013C43C File Offset: 0x0013A63C
	private void SetGuideline(bool on)
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "obj_cmn_cannonguideline");
		if (gameObject != null)
		{
			gameObject.SetActive(on);
		}
	}

	// Token: 0x04003436 RID: 13366
	private const string ModelName = "obj_cmn_cannon";

	// Token: 0x04003437 RID: 13367
	private const float m_roundParam = 5f;

	// Token: 0x04003438 RID: 13368
	private float m_firstSpeed = 5f;

	// Token: 0x04003439 RID: 13369
	private float m_outOfcontrol = 0.5f;

	// Token: 0x0400343A RID: 13370
	private GameObject m_sendObject;

	// Token: 0x0400343B RID: 13371
	private float m_moveSpeed = 0.4f;

	// Token: 0x0400343C RID: 13372
	private float m_rotArea = 25f;

	// Token: 0x0400343D RID: 13373
	private float m_time;

	// Token: 0x0400343E RID: 13374
	private bool m_shot;

	// Token: 0x0400343F RID: 13375
	private Quaternion m_centerRotation = Quaternion.identity;

	// Token: 0x04003440 RID: 13376
	private Quaternion m_startRotation = Quaternion.identity;

	// Token: 0x04003441 RID: 13377
	private CharacterInput m_input;

	// Token: 0x04003442 RID: 13378
	private CameraManager m_camera;

	// Token: 0x04003443 RID: 13379
	private ObjCannon.State m_state;

	// Token: 0x020008C7 RID: 2247
	private enum State
	{
		// Token: 0x04003445 RID: 13381
		Start,
		// Token: 0x04003446 RID: 13382
		Idle,
		// Token: 0x04003447 RID: 13383
		Wait,
		// Token: 0x04003448 RID: 13384
		Set,
		// Token: 0x04003449 RID: 13385
		Shot
	}
}
