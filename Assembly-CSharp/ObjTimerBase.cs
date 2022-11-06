using System;
using Message;
using UnityEngine;

// Token: 0x02000957 RID: 2391
public class ObjTimerBase : SpawnableObject
{
	// Token: 0x06003E43 RID: 15939 RVA: 0x00143DE8 File Offset: 0x00141FE8
	public void SetMoveType(ObjTimerBase.MoveType type)
	{
		this.m_moveType = type;
	}

	// Token: 0x06003E44 RID: 15940 RVA: 0x00143DF4 File Offset: 0x00141FF4
	protected virtual TimerType GetTimerType()
	{
		return TimerType.ERROR;
	}

	// Token: 0x06003E45 RID: 15941 RVA: 0x00143DF8 File Offset: 0x00141FF8
	protected override ResourceCategory GetModelCategory()
	{
		return ResourceCategory.OBJECT_RESOURCE;
	}

	// Token: 0x06003E46 RID: 15942 RVA: 0x00143DFC File Offset: 0x00141FFC
	private void Start()
	{
		this.m_timerType = this.GetTimerType();
		if (StageTimeManager.Instance != null)
		{
			StageTimeManager.Instance.ReserveExtendTime(this.GetExtendPattern());
		}
		MotorAnimalFly component = base.gameObject.GetComponent<MotorAnimalFly>();
		MotorThrow component2 = base.GetComponent<MotorThrow>();
		switch (this.m_moveType)
		{
		case ObjTimerBase.MoveType.Thorw:
			this.m_startPosY = base.transform.position.y;
			this.m_move_speed = 0.12f * ObjUtil.GetPlayerAddSpeed();
			this.m_hit_length = this.GetCheckGroundHitLength();
			this.SetMotorThrowComponent();
			break;
		case ObjTimerBase.MoveType.Bound:
			if (component != null)
			{
				component.enabled = false;
			}
			break;
		case ObjTimerBase.MoveType.Still:
			if (component != null)
			{
				component.enabled = false;
			}
			if (component2 != null)
			{
				component2.enabled = false;
			}
			break;
		}
	}

	// Token: 0x06003E47 RID: 15943 RVA: 0x00143EEC File Offset: 0x001420EC
	protected override void OnSpawned()
	{
	}

	// Token: 0x06003E48 RID: 15944 RVA: 0x00143EF0 File Offset: 0x001420F0
	protected override void OnDestroyed()
	{
		if (StageTimeManager.Instance != null)
		{
			StageTimeManager.Instance.CancelReservedExtendTime(this.GetExtendPattern());
		}
	}

	// Token: 0x06003E49 RID: 15945 RVA: 0x00143F20 File Offset: 0x00142120
	private void Update()
	{
		switch (this.m_moveType)
		{
		case ObjTimerBase.MoveType.Thorw:
			this.UpdateThorwType();
			break;
		}
	}

	// Token: 0x06003E4A RID: 15946 RVA: 0x00143F60 File Offset: 0x00142160
	private void UpdateThorwType()
	{
		float deltaTime = Time.deltaTime;
		this.m_time += deltaTime;
		switch (this.m_state)
		{
		case ObjTimerBase.State.Jump:
			if (this.m_time > 0.3f)
			{
				if (this.CheckComboChaoAbility())
				{
					this.m_time = 0f;
					this.m_state = ObjTimerBase.State.Drawing;
					this.OnDrawingRings(new MsgOnDrawingRings());
				}
				else
				{
					Vector3 zero = Vector3.zero;
					if (ObjUtil.CheckGroundHit(base.transform.position, base.transform.up, 1f, this.m_hit_length, out zero) || this.m_startPosY > base.transform.position.y)
					{
						this.SetCollider(true);
						this.EndThrowComponent();
						this.StartNextComponent();
						this.m_time = 0f;
						this.m_state = ObjTimerBase.State.Wait;
					}
					else if (this.m_time > 7f)
					{
						this.m_state = ObjTimerBase.State.End;
						UnityEngine.Object.Destroy(base.gameObject);
					}
				}
			}
			break;
		case ObjTimerBase.State.Wait:
			if (this.CheckComboChaoAbility())
			{
				this.m_time = 0f;
				this.m_state = ObjTimerBase.State.Drawing;
				this.OnDrawingRings(new MsgOnDrawingRings());
			}
			else if (this.m_time > 7f)
			{
				this.m_state = ObjTimerBase.State.End;
				UnityEngine.Object.Destroy(base.gameObject);
			}
			break;
		case ObjTimerBase.State.Drawing:
			if (this.m_time > 7f)
			{
				this.m_state = ObjTimerBase.State.End;
				UnityEngine.Object.Destroy(base.gameObject);
			}
			break;
		}
	}

	// Token: 0x06003E4B RID: 15947 RVA: 0x001440FC File Offset: 0x001422FC
	public void SetMotorThrowComponent()
	{
		MotorThrow component = base.GetComponent<MotorThrow>();
		if (component)
		{
			component.enabled = true;
			component.SetEnd();
			component.Setup(new MotorThrow.ThrowParam
			{
				m_obj = base.gameObject,
				m_speed = 6f,
				m_gravity = -6.1f,
				m_add_x = 4.2f + this.m_move_speed,
				m_add_y = 3f + this.m_move_speed,
				m_rot_speed = 0f,
				m_up = base.transform.up,
				m_forward = base.transform.right,
				m_rot_angle = Vector3.zero
			});
		}
	}

	// Token: 0x06003E4C RID: 15948 RVA: 0x001441B4 File Offset: 0x001423B4
	private bool CheckComboChaoAbility()
	{
		return StageComboManager.Instance != null && (StageComboManager.Instance.IsActiveComboChaoAbility(ChaoAbility.COMBO_RECOVERY_ALL_OBJ) || StageComboManager.Instance.IsActiveComboChaoAbility(ChaoAbility.COMBO_DESTROY_AND_RECOVERY));
	}

	// Token: 0x06003E4D RID: 15949 RVA: 0x001441F8 File Offset: 0x001423F8
	protected float GetCheckGroundHitLength()
	{
		return 2f;
	}

	// Token: 0x06003E4E RID: 15950 RVA: 0x00144200 File Offset: 0x00142400
	private void OnTriggerEnter(Collider other)
	{
		if (this.m_end)
		{
			return;
		}
		if (other)
		{
			GameObject gameObject = other.gameObject;
			if (gameObject)
			{
				string a = LayerMask.LayerToName(gameObject.layer);
				if (a == "Player" && gameObject.tag != "ChaoAttack")
				{
					this.TakeTimer();
				}
			}
		}
	}

	// Token: 0x06003E4F RID: 15951 RVA: 0x00144270 File Offset: 0x00142470
	private StageTimeManager.ExtendPattern GetExtendPattern()
	{
		StageTimeManager.ExtendPattern result = StageTimeManager.ExtendPattern.UNKNOWN;
		switch (this.m_timerType)
		{
		case TimerType.BRONZE:
			result = StageTimeManager.ExtendPattern.BRONZE_WATCH;
			break;
		case TimerType.SILVER:
			result = StageTimeManager.ExtendPattern.SILVER_WATCH;
			break;
		case TimerType.GOLD:
			result = StageTimeManager.ExtendPattern.GOLD_WATCH;
			break;
		}
		return result;
	}

	// Token: 0x06003E50 RID: 15952 RVA: 0x001442B4 File Offset: 0x001424B4
	private int GetAtlasIndex()
	{
		int result = 0;
		switch (this.m_timerType)
		{
		case TimerType.BRONZE:
			result = 0;
			break;
		case TimerType.SILVER:
			result = 1;
			break;
		case TimerType.GOLD:
			result = 2;
			break;
		}
		return result;
	}

	// Token: 0x06003E51 RID: 15953 RVA: 0x001442F8 File Offset: 0x001424F8
	private int GetAddTimer()
	{
		int result = 0;
		if (StageTimeManager.Instance != null)
		{
			result = (int)StageTimeManager.Instance.GetExtendTime(this.GetExtendPattern());
		}
		return result;
	}

	// Token: 0x06003E52 RID: 15954 RVA: 0x0014432C File Offset: 0x0014252C
	private void TakeTimer()
	{
		this.m_end = true;
		if (StageTimeManager.Instance != null)
		{
			StageTimeManager.Instance.ExtendTime(this.GetExtendPattern());
		}
		ObjUtil.PlayEffectCollisionCenter(base.gameObject, ObjTimerUtil.GetEffectName(this.m_timerType), 1f, false);
		ObjUtil.PlaySE(ObjTimerUtil.GetSEName(this.m_timerType), "SE");
		ObjUtil.SendGetTimerIcon(this.GetAtlasIndex(), this.GetAddTimer());
		ObjUtil.AddCombo();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06003E53 RID: 15955 RVA: 0x001443B4 File Offset: 0x001425B4
	protected void StartNextComponent()
	{
		MotorAnimalFly component = base.GetComponent<MotorAnimalFly>();
		if (component)
		{
			component.enabled = true;
			component.SetupParam(0.5f, 1f, 1f + this.m_move_speed, base.transform.right, 3f, true);
		}
	}

	// Token: 0x06003E54 RID: 15956 RVA: 0x00144408 File Offset: 0x00142608
	protected void EndNextComponent()
	{
		MotorAnimalFly component = base.GetComponent<MotorAnimalFly>();
		if (component)
		{
			component.enabled = false;
			component.SetEnd();
		}
	}

	// Token: 0x06003E55 RID: 15957 RVA: 0x00144434 File Offset: 0x00142634
	private void EndThrowComponent()
	{
		MotorThrow component = base.GetComponent<MotorThrow>();
		if (component)
		{
			component.enabled = false;
			component.SetEnd();
		}
	}

	// Token: 0x06003E56 RID: 15958 RVA: 0x00144460 File Offset: 0x00142660
	private void OnDrawingRings(MsgOnDrawingRings msg)
	{
		if (this.m_state != ObjTimerBase.State.Drawing && this.m_state != ObjTimerBase.State.End)
		{
			ObjUtil.StartMagnetControl(base.gameObject);
			this.SetCollider(true);
			this.EndThrowComponent();
			this.EndNextComponent();
			this.m_time = 0f;
			this.m_state = ObjTimerBase.State.Drawing;
		}
	}

	// Token: 0x06003E57 RID: 15959 RVA: 0x001444B8 File Offset: 0x001426B8
	private void OnDrawingRingsChaoAbility(MsgOnDrawingRings msg)
	{
		if (msg.m_chaoAbility == ChaoAbility.COMBO_RECOVERY_ALL_OBJ || msg.m_chaoAbility == ChaoAbility.COMBO_DESTROY_AND_RECOVERY)
		{
			this.OnDrawingRings(new MsgOnDrawingRings());
		}
	}

	// Token: 0x06003E58 RID: 15960 RVA: 0x001444E0 File Offset: 0x001426E0
	private void SetCollider(bool on)
	{
		SphereCollider component = base.GetComponent<SphereCollider>();
		if (component != null)
		{
			component.enabled = on;
		}
	}

	// Token: 0x04003597 RID: 13719
	private const float JUMP_END_TIME = 0.3f;

	// Token: 0x04003598 RID: 13720
	private const float WAIT_END_TIME = 7f;

	// Token: 0x04003599 RID: 13721
	private const float ANIMAL_SPEED = 6f;

	// Token: 0x0400359A RID: 13722
	private const float ANIMAL_GRAVITY = -6.1f;

	// Token: 0x0400359B RID: 13723
	private const float ADD_SPEED = 0.12f;

	// Token: 0x0400359C RID: 13724
	private const float ADD_X = 4.2f;

	// Token: 0x0400359D RID: 13725
	private const float ADD_Y = 3f;

	// Token: 0x0400359E RID: 13726
	private const float FLY_SPEED = 0.5f;

	// Token: 0x0400359F RID: 13727
	private const float FLY_DISTANCE = 1f;

	// Token: 0x040035A0 RID: 13728
	private const float FLY_ADD_X = 1f;

	// Token: 0x040035A1 RID: 13729
	private const float GROUND_DISTANCE = 3f;

	// Token: 0x040035A2 RID: 13730
	private const float HIT_CHECK_DISTANCE = 2f;

	// Token: 0x040035A3 RID: 13731
	private float m_time;

	// Token: 0x040035A4 RID: 13732
	private float m_move_speed;

	// Token: 0x040035A5 RID: 13733
	private float m_hit_length;

	// Token: 0x040035A6 RID: 13734
	private bool m_end;

	// Token: 0x040035A7 RID: 13735
	private TimerType m_timerType = TimerType.ERROR;

	// Token: 0x040035A8 RID: 13736
	private float m_startPosY;

	// Token: 0x040035A9 RID: 13737
	private ObjTimerBase.MoveType m_moveType;

	// Token: 0x040035AA RID: 13738
	private ObjTimerBase.State m_state;

	// Token: 0x02000958 RID: 2392
	public enum MoveType
	{
		// Token: 0x040035AC RID: 13740
		Thorw,
		// Token: 0x040035AD RID: 13741
		Bound,
		// Token: 0x040035AE RID: 13742
		Still
	}

	// Token: 0x02000959 RID: 2393
	private enum State
	{
		// Token: 0x040035B0 RID: 13744
		Jump,
		// Token: 0x040035B1 RID: 13745
		Wait,
		// Token: 0x040035B2 RID: 13746
		Drawing,
		// Token: 0x040035B3 RID: 13747
		End
	}
}
