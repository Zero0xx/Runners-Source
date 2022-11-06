using System;
using UnityEngine;

namespace Player
{
	// Token: 0x020009A0 RID: 2464
	public class StateEdit : FSMState<CharacterState>
	{
		// Token: 0x0600408E RID: 16526 RVA: 0x0014EEE0 File Offset: 0x0014D0E0
		public override void Enter(CharacterState context)
		{
			context.ChangeMovement(MOVESTATE_ID.IgnoreCollision);
			Collider[] componentsInChildren = context.GetComponentsInChildren<Collider>();
			foreach (Collider collider in componentsInChildren)
			{
				collider.enabled = false;
			}
			Collider component = context.GetComponent<Collider>();
			if (component)
			{
				component.enabled = false;
			}
		}

		// Token: 0x0600408F RID: 16527 RVA: 0x0014EF38 File Offset: 0x0014D138
		public override void Leave(CharacterState context)
		{
			Collider[] componentsInChildren = context.GetComponentsInChildren<Collider>();
			foreach (Collider collider in componentsInChildren)
			{
				collider.enabled = true;
			}
			Collider component = context.GetComponent<Collider>();
			if (component)
			{
				component.enabled = true;
			}
		}

		// Token: 0x06004090 RID: 16528 RVA: 0x0014EF8C File Offset: 0x0014D18C
		public override void Step(CharacterState context, float deltaTime)
		{
			CharacterInput component = context.GetComponent<CharacterInput>();
			Vector3 b = component.GetStick() * this.m_moveSpeed * Time.deltaTime;
			context.transform.position += b;
			if (Input.GetButtonDown("ButtonX"))
			{
				this.m_moveSpeed *= 2f;
				if (this.m_moveSpeed >= 50f)
				{
					this.m_moveSpeed = 2f;
				}
			}
			if (Input.GetButtonDown("ButtonA"))
			{
				context.ChangeState(STATE_ID.Fall);
				return;
			}
		}

		// Token: 0x06004091 RID: 16529 RVA: 0x0014F028 File Offset: 0x0014D228
		public override void OnGUI(CharacterState context)
		{
			Rect position = new Rect(10f, 10f, 120f, 30f);
			string text = "Cursor Speed :" + this.m_moveSpeed;
			GUI.TextField(position, text);
		}

		// Token: 0x0400374B RID: 14155
		public float m_moveSpeed = 2f;
	}
}
