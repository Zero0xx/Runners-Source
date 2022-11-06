using System;
using System.Collections.Generic;
using GameScore;
using Message;
using UnityEngine;

// Token: 0x020008E3 RID: 2275
[AddComponentMenu("Scripts/Runners/Object/Common/ObjFloor")]
public class ObjAirFloor : SpawnableObject
{
	// Token: 0x170008BE RID: 2238
	// (get) Token: 0x06003C48 RID: 15432 RVA: 0x0013D3FC File Offset: 0x0013B5FC
	// (set) Token: 0x06003C47 RID: 15431 RVA: 0x0013D3BC File Offset: 0x0013B5BC
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
				this.m_modelObject.isStatic = true;
			}
		}
	}

	// Token: 0x06003C49 RID: 15433 RVA: 0x0013D404 File Offset: 0x0013B604
	protected override void OnSpawned()
	{
		base.enabled = false;
	}

	// Token: 0x06003C4A RID: 15434 RVA: 0x0013D410 File Offset: 0x0013B610
	public void Setup(string name)
	{
		for (int i = 0; i < ObjAirFloor.FLOOR_TYPENAME.Length; i++)
		{
			if (name.IndexOf(ObjAirFloor.FLOOR_TYPENAME[i]) != -1)
			{
				this.m_type_index = i;
				break;
			}
		}
		if (this.m_type_index < ObjAirFloor.FLOOR_TYPESIZE.Length)
		{
			BoxCollider component = base.GetComponent<BoxCollider>();
			if (component)
			{
				component.center = ObjAirFloor.COLLI_CENTER;
				component.size = new Vector3(ObjAirFloor.FLOOR_TYPESIZE[this.m_type_index], ObjAirFloor.COLLI_SIZE.y, ObjAirFloor.COLLI_SIZE.z);
			}
		}
	}

	// Token: 0x06003C4B RID: 15435 RVA: 0x0013D4B0 File Offset: 0x0013B6B0
	public void OnMsgStepObjectDead(MsgObjectDead msg)
	{
		if (this.m_end)
		{
			return;
		}
		GameObject gameObject = GameObject.FindWithTag("Player");
		if (gameObject != null)
		{
			ObjUtil.CreateBrokenBonusForChaoAbiilty(base.gameObject, gameObject);
		}
		this.SetBroken();
	}

	// Token: 0x06003C4C RID: 15436 RVA: 0x0013D4F4 File Offset: 0x0013B6F4
	private void OnDamageHit(MsgHitDamage msg)
	{
		if (this.m_end)
		{
			return;
		}
		if (ObjUtil.IsAttackAttribute(msg.m_attackAttribute, AttackAttribute.Invincible))
		{
			return;
		}
		if (msg.m_attackPower >= 4 && msg.m_sender)
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

	// Token: 0x06003C4D RID: 15437 RVA: 0x0013D5A4 File Offset: 0x0013B7A4
	private void SetPlayerBroken(uint attribute_state)
	{
		if (this.m_end)
		{
			return;
		}
		int num = Data.AirFloor;
		List<ChaoAbility> abilityList = new List<ChaoAbility>();
		ObjUtil.GetChaoAbliltyPhantomFlag(attribute_state, ref abilityList);
		num = ObjUtil.GetChaoAndEnemyScore(abilityList, num);
		ObjUtil.SendMessageAddScore(num);
		ObjUtil.SendMessageScoreCheck(new StageScoreData(1, num));
		this.SetBroken();
	}

	// Token: 0x06003C4E RID: 15438 RVA: 0x0013D5F4 File Offset: 0x0013B7F4
	private void SetBroken()
	{
		if (this.m_end)
		{
			return;
		}
		this.m_end = true;
		if (this.m_type_index < ObjAirFloor.FLOOR_EFFNAME.Length)
		{
			ObjUtil.PlayEffectCollisionCenter(base.gameObject, ObjAirFloor.FLOOR_EFFNAME[this.m_type_index], 1f, false);
		}
		ObjUtil.LightPlaySE("obj_brk", "SE");
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0400347A RID: 13434
	private GameObject m_modelObject;

	// Token: 0x0400347B RID: 13435
	private static readonly string[] FLOOR_TYPENAME = new string[]
	{
		"1m",
		"2m",
		"3m"
	};

	// Token: 0x0400347C RID: 13436
	private static readonly float[] FLOOR_TYPESIZE = new float[]
	{
		1f,
		2f,
		3f
	};

	// Token: 0x0400347D RID: 13437
	private static readonly string[] FLOOR_EFFNAME = new string[]
	{
		"ef_com_explosion_m01",
		"ef_com_explosion_m01",
		"ef_com_explosion_l01"
	};

	// Token: 0x0400347E RID: 13438
	private static Vector3 COLLI_CENTER = new Vector3(0f, -0.15f, 0f);

	// Token: 0x0400347F RID: 13439
	private static Vector3 COLLI_SIZE = new Vector3(0f, 0.4f, 3f);

	// Token: 0x04003480 RID: 13440
	private int m_type_index;

	// Token: 0x04003481 RID: 13441
	private bool m_end;
}
