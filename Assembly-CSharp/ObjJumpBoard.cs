using System;
using Message;
using UnityEngine;

// Token: 0x020008F3 RID: 2291
[AddComponentMenu("Scripts/Runners/Object/Common/ObjJumpBoard")]
public class ObjJumpBoard : SpawnableObject
{
	// Token: 0x06003C92 RID: 15506 RVA: 0x0013E954 File Offset: 0x0013CB54
	protected override string GetModelName()
	{
		return "obj_cmn_jumpboard";
	}

	// Token: 0x06003C93 RID: 15507 RVA: 0x0013E95C File Offset: 0x0013CB5C
	protected override ResourceCategory GetModelCategory()
	{
		return ResourceCategory.OBJECT_RESOURCE;
	}

	// Token: 0x06003C94 RID: 15508 RVA: 0x0013E960 File Offset: 0x0013CB60
	protected override void OnSpawned()
	{
		ObjUtil.StopAnimation(base.gameObject);
	}

	// Token: 0x06003C95 RID: 15509 RVA: 0x0013E970 File Offset: 0x0013CB70
	public void SetObjJumpBoardParameter(ObjJumpBoardParameter param)
	{
		this.m_param = param;
	}

	// Token: 0x06003C96 RID: 15510 RVA: 0x0013E97C File Offset: 0x0013CB7C
	private void OnTriggerEnter(Collider other)
	{
		if (this.m_state == ObjJumpBoard.State.Idle)
		{
			MsgOnJumpBoardHit value = new MsgOnJumpBoardHit(base.transform.position, base.transform.rotation);
			other.gameObject.SendMessage("OnJumpBoardHit", value, SendMessageOptions.DontRequireReceiver);
			this.m_state = ObjJumpBoard.State.Hit;
		}
	}

	// Token: 0x06003C97 RID: 15511 RVA: 0x0013E9CC File Offset: 0x0013CBCC
	private void OnTriggerExit(Collider other)
	{
		if (this.m_state == ObjJumpBoard.State.Hit)
		{
			Quaternion rot = Quaternion.Euler(0f, 0f, -this.m_param.m_succeedAngle) * base.transform.rotation;
			Quaternion rot2 = Quaternion.Euler(0f, 0f, -this.m_param.m_missAngle) * base.transform.rotation;
			Vector3 pos = base.transform.position + base.transform.up * 0.25f;
			MsgOnJumpBoardJump msgOnJumpBoardJump = new MsgOnJumpBoardJump(pos, rot, rot2, this.m_param.m_succeedFirstSpeed, this.m_param.m_missFirstSpeed, this.m_param.m_succeedOutOfcontrol, this.m_param.m_missOutOfcontrol);
			other.gameObject.SendMessage("OnJumpBoardJump", msgOnJumpBoardJump, SendMessageOptions.DontRequireReceiver);
			if (msgOnJumpBoardJump.m_succeed)
			{
				Animation componentInChildren = base.GetComponentInChildren<Animation>();
				if (componentInChildren)
				{
					componentInChildren.wrapMode = WrapMode.Once;
					componentInChildren.Play("obj_jumpboard_bounce");
				}
			}
			this.m_state = ObjJumpBoard.State.Jump;
		}
	}

	// Token: 0x040034B4 RID: 13492
	private const string ModelName = "obj_cmn_jumpboard";

	// Token: 0x040034B5 RID: 13493
	private ObjJumpBoard.State m_state;

	// Token: 0x040034B6 RID: 13494
	private ObjJumpBoardParameter m_param;

	// Token: 0x020008F4 RID: 2292
	private enum State
	{
		// Token: 0x040034B8 RID: 13496
		Idle,
		// Token: 0x040034B9 RID: 13497
		Hit,
		// Token: 0x040034BA RID: 13498
		Jump
	}
}
