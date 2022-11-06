using System;
using Message;
using UnityEngine;

namespace Player
{
	// Token: 0x020009A5 RID: 2469
	public class StateHold : FSMState<CharacterState>
	{
		// Token: 0x060040A7 RID: 16551 RVA: 0x0014F7CC File Offset: 0x0014D9CC
		public override void Enter(CharacterState context)
		{
			StateUtil.ResetVelocity(context);
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
			MsgChaoStateUtil.SendMsgChaoState(MsgChaoState.State.STOP);
			context.SetNotCharaChange(true);
			context.SetNotUseItem(true);
			context.SetStatus(Status.Hold, true);
		}

		// Token: 0x060040A8 RID: 16552 RVA: 0x0014F848 File Offset: 0x0014DA48
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
			MsgChaoStateUtil.SendMsgChaoState(MsgChaoState.State.STOP_END);
			context.SetNotCharaChange(false);
			context.SetNotUseItem(false);
			context.SetStatus(Status.Hold, false);
		}

		// Token: 0x060040A9 RID: 16553 RVA: 0x0014F8B8 File Offset: 0x0014DAB8
		public override void Step(CharacterState context, float deltaTime)
		{
			StateUtil.ResetVelocity(context);
		}

		// Token: 0x060040AA RID: 16554 RVA: 0x0014F8C0 File Offset: 0x0014DAC0
		public override bool DispatchMessage(CharacterState context, int messageId, MessageBase msg)
		{
			int id = msg.ID;
			if (id == 12311)
			{
				StateUtil.SetVelocityForwardRun(context, false);
				context.ChangeState(STATE_ID.Run);
				return true;
			}
			if (id != 20483)
			{
				return false;
			}
			StateUtil.SetVelocityForwardRun(context, false);
			context.ChangeState(STATE_ID.Run);
			return true;
		}
	}
}
