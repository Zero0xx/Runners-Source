using System;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x0200091B RID: 2331
public class ObjTrapBase : SpawnableObject
{
	// Token: 0x06003D4C RID: 15692 RVA: 0x001415B0 File Offset: 0x0013F7B0
	protected override void OnSpawned()
	{
	}

	// Token: 0x06003D4D RID: 15693 RVA: 0x001415B4 File Offset: 0x0013F7B4
	public void OnMsgObjectDead(MsgObjectDead msg)
	{
		if (!this.m_end)
		{
			this.SetBroken();
		}
	}

	// Token: 0x06003D4E RID: 15694 RVA: 0x001415C8 File Offset: 0x0013F7C8
	protected virtual int GetScore()
	{
		return 0;
	}

	// Token: 0x06003D4F RID: 15695 RVA: 0x001415CC File Offset: 0x0013F7CC
	protected virtual void PlayEffect()
	{
	}

	// Token: 0x06003D50 RID: 15696 RVA: 0x001415D0 File Offset: 0x0013F7D0
	protected virtual void TrapDamageHit()
	{
		PlayerInformation playerInformation = ObjUtil.GetPlayerInformation();
		if (playerInformation != null && playerInformation.IsNowLastChance())
		{
			return;
		}
		ObjUtil.LightPlaySE("obj_needle_damage", "SE");
	}

	// Token: 0x06003D51 RID: 15697 RVA: 0x0014160C File Offset: 0x0013F80C
	private void SetPlayerBroken(uint attribute_state)
	{
		int num = this.GetScore();
		List<ChaoAbility> abilityList = new List<ChaoAbility>();
		ObjUtil.GetChaoAbliltyPhantomFlag(attribute_state, ref abilityList);
		num = ObjUtil.GetChaoAndEnemyScore(abilityList, num);
		ObjUtil.SendMessageAddScore(num);
		ObjUtil.SendMessageScoreCheck(new StageScoreData(1, num));
		this.SetBroken();
	}

	// Token: 0x06003D52 RID: 15698 RVA: 0x00141650 File Offset: 0x0013F850
	protected void SetBroken()
	{
		if (this.m_end)
		{
			return;
		}
		this.m_end = true;
		this.PlayEffect();
		ObjUtil.LightPlaySE("obj_brk", "SE");
		if (base.Share)
		{
			base.gameObject.SetActive(false);
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06003D53 RID: 15699 RVA: 0x001416B0 File Offset: 0x0013F8B0
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
				MsgHitDamage value = new MsgHitDamage(base.gameObject, AttackPower.PlayerColorPower);
				gameObject.SendMessage("OnDamageHit", value, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Token: 0x06003D54 RID: 15700 RVA: 0x00141700 File Offset: 0x0013F900
	private void OnDamageHit(MsgHitDamage msg)
	{
		if (this.m_end)
		{
			return;
		}
		if (msg.m_attackPower >= 4)
		{
			if (msg.m_sender)
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
		else
		{
			this.TrapDamageHit();
		}
	}

	// Token: 0x04003546 RID: 13638
	protected bool m_end;
}
