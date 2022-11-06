using System;
using Message;
using UnityEngine;

// Token: 0x02000833 RID: 2099
public class ObjAnimalBase : MonoBehaviour
{
	// Token: 0x17000873 RID: 2163
	// (set) Token: 0x060038E8 RID: 14568 RVA: 0x0012DB28 File Offset: 0x0012BD28
	public bool Sleep
	{
		set
		{
			this.m_sleep = value;
		}
	}

	// Token: 0x060038E9 RID: 14569 RVA: 0x0012DB34 File Offset: 0x0012BD34
	public bool IsSleep()
	{
		return this.m_share && this.m_sleep;
	}

	// Token: 0x060038EA RID: 14570 RVA: 0x0012DB4C File Offset: 0x0012BD4C
	private void Start()
	{
		this.m_move_speed = 0.12f * ObjUtil.GetPlayerAddSpeed();
		this.m_hit_length = this.GetCheckGroundHitLength();
		this.SetMotorThrowComponent();
		this.m_stageComboManager = StageComboManager.Instance;
	}

	// Token: 0x060038EB RID: 14571 RVA: 0x0012DB88 File Offset: 0x0012BD88
	private void Update()
	{
		float deltaTime = Time.deltaTime;
		this.m_time += deltaTime;
		switch (this.m_state)
		{
		case ObjAnimalBase.State.Jump:
			if (this.m_time > 0.3f)
			{
				if (this.UpdateCheckComboChaoAbility())
				{
					this.m_time = 0f;
					this.m_state = ObjAnimalBase.State.Drawing;
				}
				else
				{
					Vector3 zero = Vector3.zero;
					if (ObjUtil.CheckGroundHit(base.transform.position, base.transform.up, 1f, this.m_hit_length, out zero))
					{
						this.SetCollider(true);
						this.EndThrowComponent();
						this.StartNextComponent();
						this.m_time = 0f;
						this.m_state = ObjAnimalBase.State.Wait;
					}
					else if (this.m_time > 7f)
					{
						this.m_state = ObjAnimalBase.State.End;
						this.SleepOrDestroy();
					}
				}
			}
			break;
		case ObjAnimalBase.State.Wait:
			if (this.UpdateCheckComboChaoAbility())
			{
				this.m_time = 0f;
				this.m_state = ObjAnimalBase.State.Drawing;
			}
			else if (this.m_time > 7f)
			{
				this.m_state = ObjAnimalBase.State.End;
				this.SleepOrDestroy();
			}
			break;
		case ObjAnimalBase.State.Drawing:
			if (this.m_time > 7f)
			{
				this.m_state = ObjAnimalBase.State.End;
				this.SleepOrDestroy();
			}
			break;
		}
	}

	// Token: 0x060038EC RID: 14572 RVA: 0x0012DCE0 File Offset: 0x0012BEE0
	protected virtual float GetCheckGroundHitLength()
	{
		return 1f;
	}

	// Token: 0x060038ED RID: 14573 RVA: 0x0012DCE8 File Offset: 0x0012BEE8
	protected virtual void StartNextComponent()
	{
	}

	// Token: 0x060038EE RID: 14574 RVA: 0x0012DCEC File Offset: 0x0012BEEC
	protected virtual void EndNextComponent()
	{
	}

	// Token: 0x060038EF RID: 14575 RVA: 0x0012DCF0 File Offset: 0x0012BEF0
	protected float GetMoveSpeed()
	{
		return this.m_move_speed;
	}

	// Token: 0x060038F0 RID: 14576 RVA: 0x0012DCF8 File Offset: 0x0012BEF8
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

	// Token: 0x060038F1 RID: 14577 RVA: 0x0012DDB0 File Offset: 0x0012BFB0
	public void OnRevival()
	{
		base.enabled = true;
		this.m_end = false;
		this.m_state = ObjAnimalBase.State.Jump;
		this.m_time = 0f;
		this.m_move_speed = 0.12f * ObjUtil.GetPlayerAddSpeed();
		this.SetMotorThrowComponent();
	}

	// Token: 0x060038F2 RID: 14578 RVA: 0x0012DDEC File Offset: 0x0012BFEC
	public void SetShareState(AnimalType animalType)
	{
		this.m_share = true;
		this.m_sleep = true;
		this.m_animalType = animalType;
	}

	// Token: 0x060038F3 RID: 14579 RVA: 0x0012DE04 File Offset: 0x0012C004
	private void SleepOrDestroy()
	{
		if (this.m_share)
		{
			base.gameObject.SetActive(false);
			if (AnimalResourceManager.Instance != null)
			{
				AnimalResourceManager.Instance.SetSleep(this.m_animalType, base.gameObject);
			}
			this.EndThrowComponent();
			this.EndNextComponent();
			MagnetControl component = base.gameObject.GetComponent<MagnetControl>();
			if (component != null)
			{
				component.Reset();
			}
			this.SetCollider(false);
			this.m_state = ObjAnimalBase.State.End;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060038F4 RID: 14580 RVA: 0x0012DE98 File Offset: 0x0012C098
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
				if (a == "Player" || a == "Chao")
				{
					this.TakeAnimal();
				}
				else if (a == "Magnet")
				{
				}
			}
		}
	}

	// Token: 0x060038F5 RID: 14581 RVA: 0x0012DF18 File Offset: 0x0012C118
	private void OnDrawingRings(MsgOnDrawingRings msg)
	{
		if (this.m_state != ObjAnimalBase.State.Drawing && this.m_state != ObjAnimalBase.State.End)
		{
			ObjUtil.StartMagnetControl(base.gameObject);
			this.SetCollider(true);
			this.EndThrowComponent();
			this.EndNextComponent();
			this.m_time = 0f;
			this.m_state = ObjAnimalBase.State.Drawing;
		}
	}

	// Token: 0x060038F6 RID: 14582 RVA: 0x0012DF70 File Offset: 0x0012C170
	public void OnDestroyAnimal()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060038F7 RID: 14583 RVA: 0x0012DF80 File Offset: 0x0012C180
	private void TakeAnimal()
	{
		this.m_end = true;
		if (StageEffectManager.Instance != null)
		{
			StageEffectManager.Instance.PlayEffect(EffectPlayType.ANIMAL, ObjUtil.GetCollisionCenterPosition(base.gameObject), Quaternion.identity);
		}
		ObjUtil.SendMessageAddAnimal(this.m_addCount);
		ObjUtil.SendMessageScoreCheck(new StageScoreData(7, this.m_addCount));
		ObjUtil.LightPlaySE("obj_animal_get", "SE");
		ObjUtil.AddCombo();
		this.SleepOrDestroy();
	}

	// Token: 0x060038F8 RID: 14584 RVA: 0x0012DFF8 File Offset: 0x0012C1F8
	private void EndThrowComponent()
	{
		MotorThrow component = base.GetComponent<MotorThrow>();
		if (component)
		{
			component.enabled = false;
			component.SetEnd();
		}
	}

	// Token: 0x060038F9 RID: 14585 RVA: 0x0012E024 File Offset: 0x0012C224
	public static void DestroyAnimalEffect()
	{
		string name = ObjAnimalBase.EFFECT_NAME + "(Clone)";
		GameObject gameObject = GameObject.Find(name);
		if (gameObject != null)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	// Token: 0x060038FA RID: 14586 RVA: 0x0012E05C File Offset: 0x0012C25C
	private bool UpdateCheckComboChaoAbility()
	{
		if (this.m_stageComboManager != null && (this.m_stageComboManager.IsActiveComboChaoAbility(ChaoAbility.COMBO_RECOVERY_ANIMAL) || this.m_stageComboManager.IsActiveComboChaoAbility(ChaoAbility.COMBO_RECOVERY_ALL_OBJ) || this.m_stageComboManager.IsActiveComboChaoAbility(ChaoAbility.COMBO_DESTROY_AND_RECOVERY)))
		{
			this.OnDrawingRings(new MsgOnDrawingRings());
			return true;
		}
		return false;
	}

	// Token: 0x060038FB RID: 14587 RVA: 0x0012E0C0 File Offset: 0x0012C2C0
	public void SetAnimalAddCount(int addCount)
	{
		this.m_addCount = addCount;
	}

	// Token: 0x060038FC RID: 14588 RVA: 0x0012E0CC File Offset: 0x0012C2CC
	private void SetCollider(bool on)
	{
		SphereCollider component = base.GetComponent<SphereCollider>();
		if (component != null)
		{
			component.enabled = on;
		}
	}

	// Token: 0x04002F97 RID: 12183
	private const float JUMP_END_TIME = 0.3f;

	// Token: 0x04002F98 RID: 12184
	private const float WAIT_END_TIME = 7f;

	// Token: 0x04002F99 RID: 12185
	private const float ANIMAL_SPEED = 6f;

	// Token: 0x04002F9A RID: 12186
	private const float ANIMAL_GRAVITY = -6.1f;

	// Token: 0x04002F9B RID: 12187
	private const float ADD_SPEED = 0.12f;

	// Token: 0x04002F9C RID: 12188
	private const float ADD_X = 4.2f;

	// Token: 0x04002F9D RID: 12189
	private const float ADD_Y = 3f;

	// Token: 0x04002F9E RID: 12190
	public static string EFFECT_NAME = "ef_ob_get_animal01";

	// Token: 0x04002F9F RID: 12191
	private ObjAnimalBase.State m_state;

	// Token: 0x04002FA0 RID: 12192
	private float m_time;

	// Token: 0x04002FA1 RID: 12193
	private float m_move_speed;

	// Token: 0x04002FA2 RID: 12194
	private float m_hit_length;

	// Token: 0x04002FA3 RID: 12195
	private int m_addCount = 1;

	// Token: 0x04002FA4 RID: 12196
	private StageComboManager m_stageComboManager;

	// Token: 0x04002FA5 RID: 12197
	private bool m_end;

	// Token: 0x04002FA6 RID: 12198
	private bool m_share;

	// Token: 0x04002FA7 RID: 12199
	private bool m_sleep;

	// Token: 0x04002FA8 RID: 12200
	private AnimalType m_animalType = AnimalType.ERROR;

	// Token: 0x02000834 RID: 2100
	private enum State
	{
		// Token: 0x04002FAA RID: 12202
		Jump,
		// Token: 0x04002FAB RID: 12203
		Wait,
		// Token: 0x04002FAC RID: 12204
		Drawing,
		// Token: 0x04002FAD RID: 12205
		End
	}
}
