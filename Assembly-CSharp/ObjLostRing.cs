using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000904 RID: 2308
public class ObjLostRing : MonoBehaviour
{
	// Token: 0x06003CCD RID: 15565 RVA: 0x0013FD64 File Offset: 0x0013DF64
	private void Start()
	{
		this.m_count = this.m_ringCount;
		if (this.m_count > 6)
		{
			this.m_count = 6;
		}
		if (this.m_count > 0 && this.m_count <= ObjLostRing.LOSTRING_PARAM.Length)
		{
			this.CreateRing(0, Mathf.Min(this.m_count, 3));
		}
		if (this.m_chaoObj != null)
		{
			ObjUtil.RequestStartAbilityToChao(ChaoAbility.RECOVERY_RING, false);
		}
	}

	// Token: 0x06003CCE RID: 15566 RVA: 0x0013FDE0 File Offset: 0x0013DFE0
	private void Update()
	{
		if (this.m_createCount < this.m_count)
		{
			this.CreateRing(this.m_createCount, this.m_count);
		}
		else
		{
			this.m_time += Time.deltaTime;
			if (this.IsChaoMagnet())
			{
				if (!this.m_magnetStart)
				{
					if (this.m_time > 0.2f)
					{
						this.StartChaoMagnet();
						this.m_magnetStart = true;
					}
				}
				else if (this.UpdateChaoMagnet())
				{
					this.m_time = 2.4f;
				}
			}
			if (this.m_time > 1.2f)
			{
				if (this.m_magnetStart)
				{
					this.TakeChaoRing();
				}
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x06003CCF RID: 15567 RVA: 0x0013FEA4 File Offset: 0x0013E0A4
	private void CreateRing(int startCount, int endCount)
	{
		GameObject gameObject = ResourceManager.Instance.GetGameObject(ObjRing.GetRingModelCategory(), ObjRing.GetRingModelName());
		GameObject gameObject2 = ResourceManager.Instance.GetGameObject(ResourceCategory.OBJECT_PREFAB, "MotorThrow");
		if (gameObject != null && gameObject2 != null)
		{
			for (int i = startCount; i < endCount; i++)
			{
				GameObject gameObject3 = UnityEngine.Object.Instantiate(gameObject, base.transform.position, base.transform.rotation) as GameObject;
				GameObject gameObject4 = UnityEngine.Object.Instantiate(gameObject2, base.transform.position, base.transform.rotation) as GameObject;
				if (gameObject3 && gameObject4)
				{
					gameObject3.gameObject.SetActive(true);
					gameObject3.transform.parent = base.gameObject.transform;
					gameObject3.transform.localRotation = Quaternion.Euler(Vector3.zero);
					gameObject4.gameObject.SetActive(true);
					gameObject4.transform.parent = gameObject3.transform;
					MotorThrow component = gameObject4.GetComponent<MotorThrow>();
					if (component)
					{
						MotorThrow.ThrowParam throwParam = new MotorThrow.ThrowParam();
						throwParam.m_obj = gameObject3;
						throwParam.m_speed = 6f;
						throwParam.m_gravity = -6.1f;
						throwParam.m_add_x = ObjLostRing.LOSTRING_PARAM[i].m_add_x;
						throwParam.m_add_y = ObjLostRing.LOSTRING_PARAM[i].m_add_y;
						throwParam.m_up = base.transform.up;
						throwParam.m_rot_speed = 0f;
						throwParam.m_rot_angle = Vector3.zero;
						throwParam.m_forward = -base.transform.right;
						if (this.IsChaoMagnet())
						{
							MotorThrow.ThrowParam throwParam2 = throwParam;
							throwParam2.m_add_x = throwParam2.m_add_x;
							MotorThrow.ThrowParam throwParam3 = throwParam;
							throwParam3.m_add_y = throwParam3.m_add_y;
						}
						component.Setup(throwParam);
					}
					this.m_objList.Add(gameObject3);
				}
				this.m_createCount++;
			}
		}
	}

	// Token: 0x06003CD0 RID: 15568 RVA: 0x001400A0 File Offset: 0x0013E2A0
	public void SetChaoMagnet(GameObject chaoObj)
	{
		if (this.m_chaoObj == null)
		{
			this.m_chaoObj = chaoObj;
		}
	}

	// Token: 0x06003CD1 RID: 15569 RVA: 0x001400BC File Offset: 0x0013E2BC
	public void SetRingCount(int ringCount)
	{
		this.m_ringCount = ringCount;
	}

	// Token: 0x06003CD2 RID: 15570 RVA: 0x001400C8 File Offset: 0x0013E2C8
	private bool IsChaoMagnet()
	{
		return this.m_chaoObj != null;
	}

	// Token: 0x06003CD3 RID: 15571 RVA: 0x001400E0 File Offset: 0x0013E2E0
	private void StartChaoMagnet()
	{
		if (this.m_chaoObj != null)
		{
			float num = ObjUtil.GetPlayerAddSpeed();
			if (num < 0f)
			{
				num = 0f;
			}
			this.m_magnetSpeed = 0.1f + 0.01f * num;
			foreach (GameObject gameObject in this.m_objList)
			{
				if (gameObject)
				{
					for (int i = 0; i < gameObject.transform.childCount; i++)
					{
						GameObject gameObject2 = gameObject.transform.GetChild(i).gameObject;
						MotorThrow component = gameObject2.GetComponent<MotorThrow>();
						if (component)
						{
							component.SetEnd();
							break;
						}
					}
				}
			}
		}
	}

	// Token: 0x06003CD4 RID: 15572 RVA: 0x001401D8 File Offset: 0x0013E3D8
	private bool UpdateChaoMagnet()
	{
		if (this.m_chaoObj == null)
		{
			return true;
		}
		bool result = true;
		foreach (GameObject gameObject in this.m_objList)
		{
			if (gameObject)
			{
				float num = 0.1f - this.m_time * this.m_magnetSpeed;
				if (num < 0.02f)
				{
					num = 0f;
				}
				else
				{
					result = false;
				}
				Vector3 zero = Vector3.zero;
				Vector3 position = this.m_chaoObj.transform.position;
				gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, position, ref zero, num);
			}
		}
		return result;
	}

	// Token: 0x06003CD5 RID: 15573 RVA: 0x001402C0 File Offset: 0x0013E4C0
	private void TakeChaoRing()
	{
		StageAbilityManager instance = StageAbilityManager.Instance;
		if (instance)
		{
			instance.SetLostRingCount(this.m_ringCount);
		}
	}

	// Token: 0x040034FE RID: 13566
	private const float END_TIME = 1.2f;

	// Token: 0x040034FF RID: 13567
	private const float RING_SPEED = 6f;

	// Token: 0x04003500 RID: 13568
	private const float RING_GRAVITY = -6.1f;

	// Token: 0x04003501 RID: 13569
	private const int RING_MAX = 6;

	// Token: 0x04003502 RID: 13570
	private const float MAGNET_SPEED = 0.1f;

	// Token: 0x04003503 RID: 13571
	private const float MAGNET_TIME = 0.2f;

	// Token: 0x04003504 RID: 13572
	private const float MAGNET_ADDX = 0f;

	// Token: 0x04003505 RID: 13573
	private const float MAGNET_ADDY = 0f;

	// Token: 0x04003506 RID: 13574
	private int m_count;

	// Token: 0x04003507 RID: 13575
	private int m_createCount;

	// Token: 0x04003508 RID: 13576
	private float m_time;

	// Token: 0x04003509 RID: 13577
	private static readonly ObjLostRing.LostRingParam[] LOSTRING_PARAM = new ObjLostRing.LostRingParam[]
	{
		new ObjLostRing.LostRingParam(0.8f, 0.8f),
		new ObjLostRing.LostRingParam(0.5f, 1.2f),
		new ObjLostRing.LostRingParam(0.2f, 0.2f),
		new ObjLostRing.LostRingParam(0.4f, 0.4f),
		new ObjLostRing.LostRingParam(0.8f, 1.2f),
		new ObjLostRing.LostRingParam(0.5f, 0.5f),
		new ObjLostRing.LostRingParam(1.2f, 1.1f),
		new ObjLostRing.LostRingParam(0.6f, 0.5f),
		new ObjLostRing.LostRingParam(0.6f, 0.9f),
		new ObjLostRing.LostRingParam(1f, 1.3f)
	};

	// Token: 0x0400350A RID: 13578
	private GameObject m_chaoObj;

	// Token: 0x0400350B RID: 13579
	private List<GameObject> m_objList = new List<GameObject>();

	// Token: 0x0400350C RID: 13580
	private float m_magnetSpeed;

	// Token: 0x0400350D RID: 13581
	private bool m_magnetStart;

	// Token: 0x0400350E RID: 13582
	private int m_ringCount;

	// Token: 0x02000905 RID: 2309
	private class LostRingParam
	{
		// Token: 0x06003CD6 RID: 15574 RVA: 0x001402EC File Offset: 0x0013E4EC
		public LostRingParam(float add_x, float add_y)
		{
			this.m_add_x = add_x;
			this.m_add_y = add_y;
		}

		// Token: 0x0400350F RID: 13583
		public float m_add_x;

		// Token: 0x04003510 RID: 13584
		public float m_add_y;
	}
}
