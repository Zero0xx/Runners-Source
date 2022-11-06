using System;
using Message;
using UnityEngine;

namespace Player
{
	// Token: 0x02000962 RID: 2402
	public class CharacterAssistAttack : MonoBehaviour
	{
		// Token: 0x06003ED3 RID: 16083 RVA: 0x001461D0 File Offset: 0x001443D0
		private void Start()
		{
			if (this.m_animation != null)
			{
				this.m_animation.SetBool("Jump", true);
			}
		}

		// Token: 0x06003ED4 RID: 16084 RVA: 0x00146200 File Offset: 0x00144400
		public void Setup(string name, Vector3 playerPos, float speed)
		{
			this.m_name = name;
			GameObject gameObject = GameObject.FindGameObjectWithTag("MainCamera");
			if (gameObject != null)
			{
				Camera component = gameObject.GetComponent<Camera>();
				if (component != null)
				{
					Vector3 position = component.WorldToScreenPoint(playerPos);
					position = new Vector3(0f, component.pixelHeight * 0.5f, position.z);
					Vector3 position2 = component.ScreenToWorldPoint(position);
					base.transform.position = position2;
					base.transform.rotation = Quaternion.FromToRotation(Vector3.forward, CharacterDefs.BaseFrontTangent);
					this.m_mainCamera = component;
				}
			}
			string text = "chr_" + this.m_name;
			text = text.ToLower();
			GameObject gameObject2 = ResourceManager.Instance.GetGameObject(ResourceCategory.CHARA_MODEL, text);
			GameObject gameObject3 = UnityEngine.Object.Instantiate(gameObject2, base.transform.position, base.transform.rotation) as GameObject;
			if (gameObject3 != null)
			{
				Vector3 localPosition = gameObject2.transform.localPosition;
				Quaternion localRotation = gameObject2.transform.localRotation;
				gameObject3.transform.parent = base.transform;
				gameObject3.SetActive(true);
				gameObject3.transform.localPosition = localPosition;
				gameObject3.transform.localRotation = localRotation;
				this.m_animation = gameObject3.GetComponent<Animator>();
			}
			MsgBossInfo msgBossInfo = new MsgBossInfo();
			GameObjectUtil.SendMessageToTagObjects("Boss", "OnMsgBossInfo", msgBossInfo, SendMessageOptions.DontRequireReceiver);
			if (msgBossInfo.m_succeed)
			{
				this.m_targetObject = msgBossInfo.m_boss;
				this.m_targetPosition = msgBossInfo.m_position;
			}
			this.m_firstSpeed = speed * 4f;
			this.m_velocity = this.m_firstSpeed * base.transform.forward + 5f * Vector3.up;
		}

		// Token: 0x06003ED5 RID: 16085 RVA: 0x001463D4 File Offset: 0x001445D4
		private void Update()
		{
			switch (this.m_mode)
			{
			case CharacterAssistAttack.Mode.Homing:
				this.UpdateHoming(Time.deltaTime);
				break;
			case CharacterAssistAttack.Mode.ForceHoming:
				this.UpdateForcedHoming(Time.deltaTime);
				break;
			case CharacterAssistAttack.Mode.Up:
				this.UpdateUp(Time.deltaTime);
				break;
			case CharacterAssistAttack.Mode.Down:
				this.UpdateDown(Time.deltaTime);
				break;
			}
		}

		// Token: 0x06003ED6 RID: 16086 RVA: 0x00146444 File Offset: 0x00144644
		private void UpdateHoming(float deltaTime)
		{
			this.m_timer += deltaTime;
			this.UpdateTarget();
			if (this.m_targetObject == null)
			{
				base.transform.position += this.m_velocity * deltaTime;
				this.GoDown();
				return;
			}
			float firstSpeed = this.m_firstSpeed;
			this.MoveHoming(firstSpeed, deltaTime);
			if (this.m_timer > 0.8f)
			{
				this.m_mode = CharacterAssistAttack.Mode.ForceHoming;
				this.m_timer = 0f;
			}
		}

		// Token: 0x06003ED7 RID: 16087 RVA: 0x001464D4 File Offset: 0x001446D4
		private void UpdateForcedHoming(float deltaTime)
		{
			this.m_timer += deltaTime;
			if (!this.UpdateTarget())
			{
				base.transform.position += this.m_velocity * deltaTime;
				this.GoDown();
				return;
			}
			float magnitude = (this.m_targetPosition - base.transform.position).magnitude;
			float speed = magnitude / 0.2f;
			this.MoveHoming(speed, deltaTime);
			if (this.m_timer > 2f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
		}

		// Token: 0x06003ED8 RID: 16088 RVA: 0x00146574 File Offset: 0x00144774
		private void UpdateUp(float deltaTime)
		{
			base.transform.position += this.m_velocity * deltaTime;
			this.m_timer += deltaTime;
			if (this.m_timer > 1f || this.IsOutsideOfCamera(base.transform.position))
			{
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
		}

		// Token: 0x06003ED9 RID: 16089 RVA: 0x001465E4 File Offset: 0x001447E4
		private void UpdateDown(float deltaTime)
		{
			this.m_velocity += -Vector3.up * 35f * deltaTime;
			base.transform.position += this.m_velocity * deltaTime;
			this.m_timer += deltaTime;
			if (this.m_timer > 1f || this.IsOutsideOfCamera(base.transform.position))
			{
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
		}

		// Token: 0x06003EDA RID: 16090 RVA: 0x00146680 File Offset: 0x00144880
		private void GoUp()
		{
			this.m_mode = CharacterAssistAttack.Mode.Up;
			this.m_velocity = base.transform.forward * 4f + 10f * Vector3.up;
			this.m_timer = 0f;
		}

		// Token: 0x06003EDB RID: 16091 RVA: 0x001466D0 File Offset: 0x001448D0
		private void GoDown()
		{
			this.m_mode = CharacterAssistAttack.Mode.Down;
			this.m_timer = 0f;
		}

		// Token: 0x06003EDC RID: 16092 RVA: 0x001466E4 File Offset: 0x001448E4
		private void MoveHoming(float speed, float deltaTime)
		{
			Vector3 vector = this.m_targetPosition - base.transform.position;
			float magnitude = vector.magnitude;
			Vector3 normalized = vector.normalized;
			if (magnitude < speed * deltaTime)
			{
				base.transform.position = this.m_targetPosition;
			}
			else
			{
				this.m_velocity = normalized * speed;
				base.transform.position += this.m_velocity * deltaTime;
			}
		}

		// Token: 0x06003EDD RID: 16093 RVA: 0x00146768 File Offset: 0x00144968
		private bool UpdateTarget()
		{
			if (this.m_targetObject != null)
			{
				MsgBossInfo msgBossInfo = new MsgBossInfo();
				this.m_targetObject.SendMessage("OnMsgBossInfo", msgBossInfo);
				if (msgBossInfo.m_succeed)
				{
					Vector3 position = msgBossInfo.m_position;
					if (position.x + 1f > base.transform.position.x && !this.IsOutsideOfCamera(position))
					{
						this.m_targetPosition = position;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003EDE RID: 16094 RVA: 0x001467EC File Offset: 0x001449EC
		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject != this.m_targetObject)
			{
				return;
			}
			AttackPower attack = AttackPower.PlayerInvincible;
			MsgHitDamage value = new MsgHitDamage(base.gameObject, attack);
			other.gameObject.SendMessage("OnDamageHit", value, SendMessageOptions.DontRequireReceiver);
			this.GoUp();
		}

		// Token: 0x06003EDF RID: 16095 RVA: 0x00146838 File Offset: 0x00144A38
		private bool IsOutsideOfCamera(Vector3 position)
		{
			if (this.m_mainCamera != null)
			{
				Vector3 vector = this.m_mainCamera.WorldToViewportPoint(position);
				return vector.x < -0.3f || vector.x > 1.3f || vector.y < -0.3f || vector.y > 1.3f;
			}
			return true;
		}

		// Token: 0x040035C8 RID: 13768
		private const float FirstUpSpeed = 5f;

		// Token: 0x040035C9 RID: 13769
		private const float AttackUpSpeed = 10f;

		// Token: 0x040035CA RID: 13770
		private const float LimitTime = 0.8f;

		// Token: 0x040035CB RID: 13771
		private const float ForcedHomingTime = 0.2f;

		// Token: 0x040035CC RID: 13772
		private const float GraityAcc = 35f;

		// Token: 0x040035CD RID: 13773
		private const float TargetLimitOffset = 1f;

		// Token: 0x040035CE RID: 13774
		private const float LimitForcedHomingTime = 2f;

		// Token: 0x040035CF RID: 13775
		private const float FirstSpeedRate = 4f;

		// Token: 0x040035D0 RID: 13776
		private string m_name;

		// Token: 0x040035D1 RID: 13777
		private bool m_hitDamage;

		// Token: 0x040035D2 RID: 13778
		private float m_firstSpeed;

		// Token: 0x040035D3 RID: 13779
		private Vector3 m_velocity;

		// Token: 0x040035D4 RID: 13780
		private CharacterAssistAttack.Mode m_mode;

		// Token: 0x040035D5 RID: 13781
		private float m_timer;

		// Token: 0x040035D6 RID: 13782
		private GameObject m_targetObject;

		// Token: 0x040035D7 RID: 13783
		private Vector3 m_targetPosition;

		// Token: 0x040035D8 RID: 13784
		private Camera m_mainCamera;

		// Token: 0x040035D9 RID: 13785
		private Animator m_animation;

		// Token: 0x02000963 RID: 2403
		private enum Mode
		{
			// Token: 0x040035DB RID: 13787
			Homing,
			// Token: 0x040035DC RID: 13788
			ForceHoming,
			// Token: 0x040035DD RID: 13789
			Up,
			// Token: 0x040035DE RID: 13790
			Down
		}
	}
}
