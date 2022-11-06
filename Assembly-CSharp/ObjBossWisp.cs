using System;
using Message;
using UnityEngine;

// Token: 0x02000881 RID: 2177
public class ObjBossWisp : MonoBehaviour
{
	// Token: 0x06003AD2 RID: 15058 RVA: 0x001375C0 File Offset: 0x001357C0
	public void Setup(GameObject objBoss, float speed, float distance, float addX)
	{
		this.m_objBoss = objBoss;
		this.m_speed = speed;
		this.m_distance = distance;
		this.m_addX = addX;
		this.m_move_speed = 0.12f * ObjUtil.GetPlayerAddSpeed();
		this.CreateModel();
		MotorAnimalFly component = base.GetComponent<MotorAnimalFly>();
		if (component)
		{
			component.SetupParam(this.m_speed, this.m_distance, this.m_addX + this.m_move_speed, base.transform.right, 0f, false);
		}
	}

	// Token: 0x06003AD3 RID: 15059 RVA: 0x00137644 File Offset: 0x00135844
	private void Update()
	{
		this.m_time += Time.deltaTime;
		if (this.m_time > 7f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06003AD4 RID: 15060 RVA: 0x00137674 File Offset: 0x00135874
	private void EndMotorComponent()
	{
		MotorAnimalFly component = base.GetComponent<MotorAnimalFly>();
		if (component)
		{
			component.enabled = false;
		}
	}

	// Token: 0x06003AD5 RID: 15061 RVA: 0x0013769C File Offset: 0x0013589C
	private void OnTriggerEnter(Collider other)
	{
		if (other)
		{
			GameObject gameObject = other.gameObject;
			if (gameObject)
			{
				string a = LayerMask.LayerToName(gameObject.layer);
				if (a == "Player" || a == "Chao")
				{
					this.TakeWisp();
				}
				else if (a == "Magnet")
				{
				}
			}
		}
	}

	// Token: 0x06003AD6 RID: 15062 RVA: 0x00137710 File Offset: 0x00135910
	private void OnDrawingRings(MsgOnDrawingRings msg)
	{
		ObjUtil.StartMagnetControl(base.gameObject);
		this.EndMotorComponent();
		this.m_time = 0f;
	}

	// Token: 0x06003AD7 RID: 15063 RVA: 0x00137730 File Offset: 0x00135930
	private void TakeWisp()
	{
		if (this.m_objBoss != null)
		{
			this.m_objBoss.SendMessage("OnGetWisp", SendMessageOptions.DontRequireReceiver);
		}
		ObjUtil.PlayEffect(ObjBossWisp.EFFECT_NAME, ObjUtil.GetCollisionCenterPosition(base.gameObject), base.transform.rotation, 1f, false);
		ObjUtil.PlayEventSE(ObjBossWisp.SE_NAME, EventManager.EventType.RAID_BOSS);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06003AD8 RID: 15064 RVA: 0x0013779C File Offset: 0x0013599C
	private void CreateModel()
	{
		GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.EVENT_RESOURCE, "obj_raid_wisp");
		if (gameObject)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, base.transform.position, base.transform.rotation) as GameObject;
			if (gameObject2)
			{
				gameObject2.gameObject.SetActive(true);
				gameObject2.transform.parent = base.gameObject.transform;
				gameObject2.transform.localRotation = Quaternion.Euler(ObjBossWisp.ModelLocalRotation);
			}
		}
	}

	// Token: 0x040032EE RID: 13038
	private const string MODEL_NAME = "obj_raid_wisp";

	// Token: 0x040032EF RID: 13039
	private const ResourceCategory MODEL_CATEGORY = ResourceCategory.EVENT_RESOURCE;

	// Token: 0x040032F0 RID: 13040
	private const float WAIT_END_TIME = 7f;

	// Token: 0x040032F1 RID: 13041
	private const float ADD_SPEED = 0.12f;

	// Token: 0x040032F2 RID: 13042
	public static string EFFECT_NAME = "ef_raid_wisp_get01";

	// Token: 0x040032F3 RID: 13043
	public static string SE_NAME = "rb_wisp_get";

	// Token: 0x040032F4 RID: 13044
	private float m_speed = 0.5f;

	// Token: 0x040032F5 RID: 13045
	private float m_distance = 1f;

	// Token: 0x040032F6 RID: 13046
	private float m_addX = 1f;

	// Token: 0x040032F7 RID: 13047
	private float m_time;

	// Token: 0x040032F8 RID: 13048
	private float m_move_speed;

	// Token: 0x040032F9 RID: 13049
	private GameObject m_objBoss;

	// Token: 0x040032FA RID: 13050
	private static Vector3 ModelLocalRotation = new Vector3(0f, 180f, 0f);
}
