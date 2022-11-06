using System;
using Message;
using UnityEngine;

// Token: 0x0200087A RID: 2170
public class ObjBossMissile : MonoBehaviour
{
	// Token: 0x06003AB6 RID: 15030 RVA: 0x001368D0 File Offset: 0x00134AD0
	private void Update()
	{
		this.m_time += Time.deltaTime;
		if (this.m_time > 5f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06003AB7 RID: 15031 RVA: 0x00136900 File Offset: 0x00134B00
	public void OnMsgObjectDead(MsgObjectDead msg)
	{
		if (base.enabled)
		{
			this.SetBroken();
		}
	}

	// Token: 0x06003AB8 RID: 15032 RVA: 0x00136914 File Offset: 0x00134B14
	private void SetBroken()
	{
		ObjUtil.PlayEffect(this.m_typeParam.m_effectName, ObjUtil.GetCollisionCenterPosition(base.gameObject), base.transform.rotation, 1f, false);
		ObjUtil.PlaySE("obj_brk", "SE");
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06003AB9 RID: 15033 RVA: 0x00136968 File Offset: 0x00134B68
	public void Setup(GameObject obj, float speed, BossType type)
	{
		this.m_type = type;
		if (BossTypeUtil.GetBossCharaType(this.m_type) != BossCharaType.EGGMAN)
		{
			this.m_typeParam = new ObjBossMissile.BossMissileTypeParam(EventBossObjectTable.GetItemData(EventBossObjectTableItem.Obj1_ModelName), ResourceCategory.EVENT_RESOURCE, EventBossObjectTable.GetItemData(EventBossObjectTableItem.Obj1_EffectName), EventBossObjectTable.GetItemData(EventBossObjectTableItem.Obj1_LoopEffectName), EventBossObjectTable.GetItemData(EventBossObjectTableItem.Obj1_SetSeName));
		}
		string loopEffectName = this.m_typeParam.m_loopEffectName;
		if (loopEffectName != string.Empty)
		{
			ObjUtil.PlayEffectChild(base.gameObject, loopEffectName, ObjUtil.GetCollisionCenter(base.gameObject), Quaternion.identity, false);
		}
		this.CreateModel();
		ObjUtil.StartHudAlert(base.gameObject);
		MotorConstant component = base.GetComponent<MotorConstant>();
		if (component)
		{
			string se_category = "SE";
			if (BossTypeUtil.GetBossCharaType(this.m_type) != BossCharaType.EGGMAN)
			{
				se_category = "SE_" + EventManager.GetEventTypeName(EventManager.EventType.RAID_BOSS);
			}
			component.SetParam(speed, 20f, 20f, -base.transform.right, se_category, this.m_typeParam.m_seName);
		}
	}

	// Token: 0x06003ABA RID: 15034 RVA: 0x00136A68 File Offset: 0x00134C68
	private void OnTriggerEnter(Collider other)
	{
		if (other)
		{
			GameObject gameObject = other.gameObject;
			if (gameObject)
			{
				MsgHitDamage value = new MsgHitDamage(base.gameObject, AttackPower.PlayerColorPower);
				gameObject.SendMessage("OnDamageHit", value, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Token: 0x06003ABB RID: 15035 RVA: 0x00136AAC File Offset: 0x00134CAC
	private void OnDamageHit(MsgHitDamage msg)
	{
		if (msg.m_attackPower >= 4)
		{
			if (msg.m_sender)
			{
				GameObject gameObject = msg.m_sender.gameObject;
				if (gameObject)
				{
					MsgHitDamageSucceed value = new MsgHitDamageSucceed(base.gameObject, 0, ObjUtil.GetCollisionCenterPosition(base.gameObject), base.transform.rotation);
					gameObject.SendMessage("OnDamageSucceed", value, SendMessageOptions.DontRequireReceiver);
					this.SetBroken();
				}
			}
		}
		else
		{
			this.SetBroken();
		}
	}

	// Token: 0x06003ABC RID: 15036 RVA: 0x00136B30 File Offset: 0x00134D30
	private void CreateModel()
	{
		GameObject gameObject = ResourceManager.Instance.GetGameObject(this.m_typeParam.m_resCategory, this.m_typeParam.m_modelName);
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

	// Token: 0x040032C0 RID: 12992
	private const float END_TIME = 5f;

	// Token: 0x040032C1 RID: 12993
	private const float MOVE_DISTANCE = 20f;

	// Token: 0x040032C2 RID: 12994
	private const float START_MOVE_DISTANCE = 20f;

	// Token: 0x040032C3 RID: 12995
	private float m_time;

	// Token: 0x040032C4 RID: 12996
	private BossType m_type = BossType.NONE;

	// Token: 0x040032C5 RID: 12997
	private ObjBossMissile.BossMissileTypeParam m_typeParam = new ObjBossMissile.BossMissileTypeParam("obj_cmn_movetrap", ResourceCategory.OBJECT_RESOURCE, "ef_com_explosion_m01", string.Empty, "obj_missile_shoot");

	// Token: 0x0200087B RID: 2171
	private class BossMissileTypeParam
	{
		// Token: 0x06003ABD RID: 15037 RVA: 0x00136BD0 File Offset: 0x00134DD0
		public BossMissileTypeParam(string modelName, ResourceCategory resCategory, string effectName, string loopEffectName, string seName)
		{
			this.m_modelName = modelName;
			this.m_resCategory = resCategory;
			this.m_effectName = effectName;
			this.m_loopEffectName = loopEffectName;
			this.m_seName = seName;
		}

		// Token: 0x040032C6 RID: 12998
		public string m_modelName;

		// Token: 0x040032C7 RID: 12999
		public ResourceCategory m_resCategory;

		// Token: 0x040032C8 RID: 13000
		public string m_effectName;

		// Token: 0x040032C9 RID: 13001
		public string m_loopEffectName;

		// Token: 0x040032CA RID: 13002
		public string m_seName;
	}
}
