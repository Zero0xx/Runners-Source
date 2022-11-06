using System;
using UnityEngine;

// Token: 0x0200095E RID: 2398
public class ObjTimerUtil
{
	// Token: 0x06003E64 RID: 15972 RVA: 0x001445E8 File Offset: 0x001427E8
	private static bool CheckType(TimerType type)
	{
		return TimerType.BRONZE <= type && type < TimerType.NUM;
	}

	// Token: 0x06003E65 RID: 15973 RVA: 0x001445FC File Offset: 0x001427FC
	private static void CreateTimerObj(GameObject enm_obj, TimerType type)
	{
		if (enm_obj != null)
		{
			string name = ObjTimerUtil.OBJ_NAME[(int)type];
			string name2 = ObjTimerUtil.MODEL_NAME[(int)type];
			GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.OBJECT_PREFAB, name);
			GameObject gameObject2 = ResourceManager.Instance.GetGameObject(ResourceCategory.OBJECT_RESOURCE, name2);
			if (gameObject != null && gameObject2 != null)
			{
				GameObject gameObject3 = UnityEngine.Object.Instantiate(gameObject, enm_obj.transform.position, Quaternion.identity) as GameObject;
				GameObject gameObject4 = UnityEngine.Object.Instantiate(gameObject2, gameObject3.transform.position, gameObject3.transform.rotation) as GameObject;
				if (gameObject3 != null && gameObject4 != null)
				{
					gameObject3.gameObject.SetActive(true);
					SphereCollider component = gameObject3.GetComponent<SphereCollider>();
					if (component != null)
					{
						component.enabled = false;
					}
					gameObject4.gameObject.SetActive(true);
					gameObject4.transform.parent = gameObject3.transform;
					gameObject4.transform.localRotation = Quaternion.identity;
				}
			}
		}
	}

	// Token: 0x06003E66 RID: 15974 RVA: 0x00144710 File Offset: 0x00142910
	public static string GetObjectName(TimerType type)
	{
		if (ObjTimerUtil.CheckType(type))
		{
			return ObjTimerUtil.OBJ_NAME[(int)type];
		}
		return string.Empty;
	}

	// Token: 0x06003E67 RID: 15975 RVA: 0x0014472C File Offset: 0x0014292C
	public static string GetModelName(TimerType type)
	{
		if (ObjTimerUtil.CheckType(type))
		{
			return ObjTimerUtil.MODEL_NAME[(int)type];
		}
		return string.Empty;
	}

	// Token: 0x06003E68 RID: 15976 RVA: 0x00144748 File Offset: 0x00142948
	public static string GetEffectName(TimerType type)
	{
		if (ObjTimerUtil.CheckType(type))
		{
			return ObjTimerUtil.EFFECT_NAME[(int)type];
		}
		return string.Empty;
	}

	// Token: 0x06003E69 RID: 15977 RVA: 0x00144764 File Offset: 0x00142964
	public static string GetSEName(TimerType type)
	{
		if (ObjTimerUtil.CheckType(type))
		{
			return ObjTimerUtil.SE_NAME[(int)type];
		}
		return string.Empty;
	}

	// Token: 0x06003E6A RID: 15978 RVA: 0x00144780 File Offset: 0x00142980
	public static void CreateTimer(GameObject enm_obj, TimerType type)
	{
		if (ObjTimerUtil.CheckType(type))
		{
			ObjTimerUtil.CreateTimerObj(enm_obj, type);
		}
	}

	// Token: 0x06003E6B RID: 15979 RVA: 0x00144794 File Offset: 0x00142994
	public static bool IsEnableCreateTimer()
	{
		return StageTimeManager.Instance != null && !StageTimeManager.Instance.IsReachedExtendedLimit();
	}

	// Token: 0x040035BB RID: 13755
	private static readonly string[] OBJ_NAME = new string[]
	{
		"ObjTimerBronze",
		"ObjTimerSilver",
		"ObjTimerGold"
	};

	// Token: 0x040035BC RID: 13756
	private static readonly string[] MODEL_NAME = new string[]
	{
		"obj_cmn_timeextenditem_copper",
		"obj_cmn_timeextenditem_silver",
		"obj_cmn_timeextenditem_gold"
	};

	// Token: 0x040035BD RID: 13757
	private static readonly string[] EFFECT_NAME = new string[]
	{
		"ef_ob_get_timeextend_copper",
		"ef_ob_get_timeextend_silver",
		"ef_ob_get_timeextend_gold"
	};

	// Token: 0x040035BE RID: 13758
	private static readonly string[] SE_NAME = new string[]
	{
		"obj_timer_bronze",
		"obj_timer_silver",
		"obj_timer_gold"
	};
}
