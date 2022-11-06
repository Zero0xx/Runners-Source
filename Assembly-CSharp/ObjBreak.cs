using System;
using System.Collections.Generic;
using GameScore;
using Message;
using UnityEngine;

// Token: 0x020008C2 RID: 2242
[AddComponentMenu("Scripts/Runners/Object/Common/ObjBreak")]
public class ObjBreak : SpawnableObject
{
	// Token: 0x170008BD RID: 2237
	// (get) Token: 0x06003BBD RID: 15293 RVA: 0x0013B6E0 File Offset: 0x001398E0
	// (set) Token: 0x06003BBC RID: 15292 RVA: 0x0013B6AC File Offset: 0x001398AC
	public GameObject ModelObject
	{
		get
		{
			return this.m_modelObject;
		}
		set
		{
			if (this.m_modelObject == null)
			{
				this.m_modelObject = value;
				this.m_modelObject.SetActive(true);
			}
		}
	}

	// Token: 0x06003BBE RID: 15294 RVA: 0x0013B6E8 File Offset: 0x001398E8
	protected override void OnSpawned()
	{
		float num = ObjUtil.GetPlayerAddSpeed();
		if (num < 0f)
		{
			num = 0f;
		}
		this.m_move_speed = 0.8f * num;
	}

	// Token: 0x06003BBF RID: 15295 RVA: 0x0013B71C File Offset: 0x0013991C
	private void Update()
	{
		if (!this.m_setup)
		{
			this.m_setup = this.Setup(this.m_setup_name);
		}
		if (this.m_break)
		{
			this.m_time += Time.deltaTime;
			if (this.m_time > 2.5f)
			{
				this.m_break = false;
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
		}
	}

	// Token: 0x06003BC0 RID: 15296 RVA: 0x0013B788 File Offset: 0x00139988
	public void SetObjName(string name)
	{
		this.m_setup_name = name;
	}

	// Token: 0x06003BC1 RID: 15297 RVA: 0x0013B794 File Offset: 0x00139994
	private bool Setup(string name)
	{
		if (this.m_break_obj != null)
		{
			return true;
		}
		this.m_break_obj = ObjBreak.CreateBreakModel(base.gameObject, name);
		if (this.m_break_obj != null)
		{
			this.BreakModelVisible(false);
			this.m_model_count = 0U;
			if (this.m_break_obj)
			{
				Component[] componentsInChildren = this.m_break_obj.GetComponentsInChildren<MeshRenderer>(true);
				this.m_model_count = (uint)componentsInChildren.Length;
			}
			if ((ulong)this.m_model_count >= (ulong)((long)ObjBreak.MODEL_ROT_TBL.Length))
			{
				this.m_model_count = (uint)ObjBreak.MODEL_ROT_TBL.Length;
			}
			if ((ulong)this.m_model_count >= (ulong)((long)ObjBreak.BREAK_PARAM.Length))
			{
				this.m_model_count = (uint)ObjBreak.BREAK_PARAM.Length;
			}
			this.m_breakParam = new ObjBreak.BreakParam[this.m_model_count];
			int num = UnityEngine.Random.Range(0, ObjBreak.BREAK_PARAM.Length - 1);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)this.m_model_count))
			{
				int num3 = num + num2;
				if (num3 >= ObjBreak.BREAK_PARAM.Length)
				{
					num3 -= ObjBreak.BREAK_PARAM.Length;
				}
				this.m_breakParam[num2] = new ObjBreak.BreakParam(ObjBreak.BREAK_PARAM[num3].m_add_x + this.m_move_speed, ObjBreak.BREAK_PARAM[num3].m_add_y + this.m_move_speed, ObjBreak.BREAK_PARAM[num3].m_rot_speed);
				num2++;
			}
			return true;
		}
		return false;
	}

	// Token: 0x06003BC2 RID: 15298 RVA: 0x0013B8E8 File Offset: 0x00139AE8
	public void OnMsgObjectDead(MsgObjectDead msg)
	{
		if (base.enabled)
		{
			this.SetBroken();
		}
	}

	// Token: 0x06003BC3 RID: 15299 RVA: 0x0013B8FC File Offset: 0x00139AFC
	public void OnMsgStepObjectDead(MsgObjectDead msg)
	{
		if (base.enabled)
		{
			this.SetBroken();
		}
	}

	// Token: 0x06003BC4 RID: 15300 RVA: 0x0013B910 File Offset: 0x00139B10
	private void SetPlayerBroken(uint attribute_state)
	{
		int num = Data.Break;
		List<ChaoAbility> abilityList = new List<ChaoAbility>();
		ObjUtil.GetChaoAbliltyPhantomFlag(attribute_state, ref abilityList);
		num = ObjUtil.GetChaoAndEnemyScore(abilityList, num);
		ObjUtil.SendMessageAddScore(num);
		ObjUtil.SendMessageScoreCheck(new StageScoreData(1, num));
		this.SetBroken();
	}

	// Token: 0x06003BC5 RID: 15301 RVA: 0x0013B954 File Offset: 0x00139B54
	private void SetBroken()
	{
		if (this.m_break)
		{
			return;
		}
		if (this.m_break_obj && this.m_modelObject)
		{
			this.RootModelVisible(false);
			this.BreakModelVisible(true);
			this.BreakStart();
			ObjUtil.PlayEffectCollisionCenter(base.gameObject, "ef_com_explosion_m01", 1f, false);
			ObjUtil.LightPlaySE("obj_brk", "SE");
			this.m_break = true;
		}
	}

	// Token: 0x06003BC6 RID: 15302 RVA: 0x0013B9D0 File Offset: 0x00139BD0
	private void OnDamageHit(MsgHitDamage msg)
	{
		if (this.m_break)
		{
			return;
		}
		if (msg.m_attackPower >= 3 && msg.m_sender)
		{
			GameObject gameObject = msg.m_sender.gameObject;
			if (gameObject)
			{
				MsgHitDamageSucceed value = new MsgHitDamageSucceed(base.gameObject, 0, ObjUtil.GetCollisionCenterPosition(base.gameObject), base.transform.rotation);
				gameObject.SendMessage("OnDamageSucceed", value, SendMessageOptions.DontRequireReceiver);
				this.SetPlayerBroken(msg.m_attackAttribute);
				ObjUtil.CreateBrokenBonus(base.gameObject, gameObject, msg.m_attackAttribute);
			}
		}
	}

	// Token: 0x06003BC7 RID: 15303 RVA: 0x0013BA6C File Offset: 0x00139C6C
	private static GameObject CreateBreakModel(GameObject baseObj, string in_name)
	{
		GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.STAGE_RESOURCE, in_name + "_brk");
		GameObject gameObject2 = ResourceManager.Instance.GetGameObject(ResourceCategory.OBJECT_PREFAB, "MotorThrow");
		if (gameObject != null && gameObject2 != null)
		{
			GameObject gameObject3 = UnityEngine.Object.Instantiate(gameObject, baseObj.transform.position, baseObj.transform.rotation) as GameObject;
			if (gameObject3)
			{
				gameObject3.gameObject.SetActive(true);
				gameObject3.transform.parent = baseObj.transform;
				for (int i = 0; i < gameObject3.transform.childCount; i++)
				{
					GameObject gameObject4 = gameObject3.transform.GetChild(i).gameObject;
					if (gameObject4)
					{
						GameObject gameObject5 = UnityEngine.Object.Instantiate(gameObject2, baseObj.transform.position, baseObj.transform.rotation) as GameObject;
						if (gameObject5)
						{
							gameObject5.gameObject.SetActive(true);
							gameObject5.transform.parent = gameObject4.transform;
						}
					}
				}
				return gameObject3;
			}
		}
		return null;
	}

	// Token: 0x06003BC8 RID: 15304 RVA: 0x0013BB90 File Offset: 0x00139D90
	private void BreakModelVisible(bool flag)
	{
		if (this.m_break_obj)
		{
			MeshRenderer component = this.m_break_obj.GetComponent<MeshRenderer>();
			if (component)
			{
				component.enabled = flag;
			}
			Component[] componentsInChildren = this.m_break_obj.GetComponentsInChildren<MeshRenderer>(true);
			foreach (MeshRenderer meshRenderer in componentsInChildren)
			{
				meshRenderer.enabled = flag;
			}
		}
	}

	// Token: 0x06003BC9 RID: 15305 RVA: 0x0013BC04 File Offset: 0x00139E04
	private void RootModelVisible(bool flag)
	{
		if (this.m_modelObject)
		{
			Component[] componentsInChildren = base.GetComponentsInChildren<MeshRenderer>(true);
			foreach (MeshRenderer meshRenderer in componentsInChildren)
			{
				meshRenderer.enabled = flag;
				BoxCollider[] componentsInChildren2 = meshRenderer.gameObject.GetComponentsInChildren<BoxCollider>(true);
				foreach (BoxCollider boxCollider in componentsInChildren2)
				{
					boxCollider.enabled = flag;
				}
			}
		}
	}

	// Token: 0x06003BCA RID: 15306 RVA: 0x0013BC8C File Offset: 0x00139E8C
	private void BreakStart()
	{
		if (this.m_break_obj)
		{
			for (int i = 0; i < this.m_break_obj.transform.childCount; i++)
			{
				GameObject gameObject = this.m_break_obj.transform.GetChild(i).gameObject;
				if (gameObject)
				{
					for (int j = 0; j < gameObject.transform.childCount; j++)
					{
						GameObject gameObject2 = gameObject.transform.GetChild(j).gameObject;
						if (gameObject2)
						{
							MotorThrow component = gameObject2.GetComponent<MotorThrow>();
							if (component && i < this.m_breakParam.Length)
							{
								component.Setup(new MotorThrow.ThrowParam
								{
									m_obj = gameObject,
									m_speed = 6f,
									m_gravity = -6.1f,
									m_add_x = this.m_breakParam[i].m_add_x,
									m_add_y = this.m_breakParam[i].m_add_y,
									m_rot_speed = this.m_breakParam[i].m_rot_speed,
									m_up = base.transform.up,
									m_forward = base.transform.right,
									m_rot_angle = ObjBreak.MODEL_ROT_TBL[i]
								});
								break;
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x04003422 RID: 13346
	private const float ADD_SPEED = 0.8f;

	// Token: 0x04003423 RID: 13347
	private const float END_TIME = 2.5f;

	// Token: 0x04003424 RID: 13348
	private const float BREAK_SPEED = 6f;

	// Token: 0x04003425 RID: 13349
	private const float BREAK_GRAVITY = -6.1f;

	// Token: 0x04003426 RID: 13350
	private GameObject m_modelObject;

	// Token: 0x04003427 RID: 13351
	private static readonly Vector3[] MODEL_ROT_TBL = new Vector3[]
	{
		new Vector3(1f, 1f, 0f),
		new Vector3(0f, 1f, 1f),
		new Vector3(1f, 0f, 1f),
		new Vector3(1f, 1f, 0f)
	};

	// Token: 0x04003428 RID: 13352
	private static readonly ObjBreak.BreakParam[] BREAK_PARAM = new ObjBreak.BreakParam[]
	{
		new ObjBreak.BreakParam(0.8f, 1.2f, 15f),
		new ObjBreak.BreakParam(1.5f, 1.4f, 5f),
		new ObjBreak.BreakParam(1f, 1f, 10f),
		new ObjBreak.BreakParam(0.7f, 1.6f, 2f)
	};

	// Token: 0x04003429 RID: 13353
	private ObjBreak.BreakParam[] m_breakParam;

	// Token: 0x0400342A RID: 13354
	private bool m_break;

	// Token: 0x0400342B RID: 13355
	private uint m_model_count;

	// Token: 0x0400342C RID: 13356
	private float m_time;

	// Token: 0x0400342D RID: 13357
	private float m_move_speed;

	// Token: 0x0400342E RID: 13358
	private GameObject m_break_obj;

	// Token: 0x0400342F RID: 13359
	private bool m_setup;

	// Token: 0x04003430 RID: 13360
	private string m_setup_name = string.Empty;

	// Token: 0x020008C3 RID: 2243
	private class BreakParam
	{
		// Token: 0x06003BCB RID: 15307 RVA: 0x0013BDF4 File Offset: 0x00139FF4
		public BreakParam(float add_x, float add_y, float rot_speed)
		{
			this.m_add_x = add_x;
			this.m_add_y = add_y;
			this.m_rot_speed = rot_speed;
		}

		// Token: 0x04003431 RID: 13361
		public float m_add_x;

		// Token: 0x04003432 RID: 13362
		public float m_add_y;

		// Token: 0x04003433 RID: 13363
		public float m_rot_speed;
	}
}
