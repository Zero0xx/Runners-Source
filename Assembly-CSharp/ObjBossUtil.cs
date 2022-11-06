using System;
using UnityEngine;

// Token: 0x02000873 RID: 2163
public class ObjBossUtil
{
	// Token: 0x06003A8F RID: 14991 RVA: 0x00135A04 File Offset: 0x00133C04
	public static void PlayShotEffect(GameObject boss_obj)
	{
		if (boss_obj)
		{
			Quaternion local_rot = Quaternion.Euler(ObjBossUtil.EFFECT_ROT);
			ObjUtil.PlayEffectChild(ObjBossUtil.GetBossHatchNode(boss_obj), "ef_bo_em_muzzle01", Vector3.zero, local_rot, 1f, true);
		}
	}

	// Token: 0x06003A90 RID: 14992 RVA: 0x00135A44 File Offset: 0x00133C44
	public static void PlayShotSE()
	{
		ObjUtil.PlaySE("boss_bomb_drop2", "SE");
	}

	// Token: 0x06003A91 RID: 14993 RVA: 0x00135A58 File Offset: 0x00133C58
	public static void SetupBallAppear(GameObject boss_obj, GameObject ball_obj)
	{
		if (boss_obj && ball_obj)
		{
			Vector3 bossHatchPos = ObjBossUtil.GetBossHatchPos(boss_obj);
			ball_obj.transform.position = new Vector3(bossHatchPos.x, ball_obj.transform.position.y, bossHatchPos.z);
			ObjUtil.SetModelVisible(ball_obj, true);
		}
	}

	// Token: 0x06003A92 RID: 14994 RVA: 0x00135ABC File Offset: 0x00133CBC
	public static bool UpdateBallAppear(float delta, GameObject boss_obj, GameObject ball_obj, float add_speed)
	{
		if (boss_obj && ball_obj)
		{
			Vector3 bossHatchPos = ObjBossUtil.GetBossHatchPos(boss_obj);
			float num = delta * (2f * add_speed);
			ball_obj.transform.position = new Vector3(bossHatchPos.x, ball_obj.transform.position.y - num, bossHatchPos.z);
			if (bossHatchPos.y - ball_obj.transform.position.y > 0.3f)
			{
				ball_obj.transform.position = new Vector3(bossHatchPos.x, bossHatchPos.y - 0.3f, bossHatchPos.z);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06003A93 RID: 14995 RVA: 0x00135B78 File Offset: 0x00133D78
	public static void UpdateBallAttack(GameObject boss_obj, GameObject ball_obj, float time, float attack_speed)
	{
		if (boss_obj && ball_obj)
		{
			float num = 0.1f - time * attack_speed;
			if (num < 0.01f)
			{
				num = 0f;
			}
			Vector3 zero = Vector3.zero;
			Vector3 position = boss_obj.transform.position;
			ball_obj.transform.position = Vector3.SmoothDamp(ball_obj.transform.position, position, ref zero, num);
		}
	}

	// Token: 0x06003A94 RID: 14996 RVA: 0x00135BE8 File Offset: 0x00133DE8
	public static void UpdateBallRot(float delta, GameObject ball_obj, Vector3 agl, float speed)
	{
		if (ball_obj)
		{
			ball_obj.transform.rotation = Quaternion.Euler(agl * 60f * delta * speed) * ball_obj.transform.rotation;
		}
	}

	// Token: 0x06003A95 RID: 14997 RVA: 0x00135C38 File Offset: 0x00133E38
	public static GameObject GetBossHatchNode(GameObject boss_obj)
	{
		if (boss_obj)
		{
			return GameObjectUtil.FindChildGameObject(boss_obj, "BombPoint");
		}
		return null;
	}

	// Token: 0x06003A96 RID: 14998 RVA: 0x00135C54 File Offset: 0x00133E54
	public static Vector3 GetBossHatchPos(GameObject boss_obj)
	{
		GameObject bossHatchNode = ObjBossUtil.GetBossHatchNode(boss_obj);
		if (bossHatchNode != null)
		{
			return bossHatchNode.transform.position;
		}
		return Vector3.zero;
	}

	// Token: 0x06003A97 RID: 14999 RVA: 0x00135C88 File Offset: 0x00133E88
	public static Quaternion GetShotRotation(Quaternion rot, bool playerDead)
	{
		if (playerDead)
		{
			Vector3 eulerAngles = rot.eulerAngles;
			return Quaternion.Euler(0f, eulerAngles.y, eulerAngles.z);
		}
		return rot;
	}

	// Token: 0x06003A98 RID: 15000 RVA: 0x00135CC0 File Offset: 0x00133EC0
	public static bool IsNowLastChance(PlayerInformation playerInfo)
	{
		return playerInfo != null && playerInfo.IsNowLastChance();
	}

	// Token: 0x0400327B RID: 12923
	private const float BALL_DOWNSPEED = 2f;

	// Token: 0x0400327C RID: 12924
	private const float BALL_DOWNPOS = 0.3f;

	// Token: 0x0400327D RID: 12925
	private static Vector3 EFFECT_ROT = new Vector3(-90f, 0f, 0f);
}
