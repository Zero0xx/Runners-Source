using System;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x020008BD RID: 2237
public class ObjTrampolineFloorCollision : SpawnableObject
{
	// Token: 0x06003BA2 RID: 15266 RVA: 0x0013AED8 File Offset: 0x001390D8
	protected override void OnSpawned()
	{
		if (this.m_end)
		{
			return;
		}
		if (this.m_createCountMax == 0 && StageItemManager.Instance != null && StageItemManager.Instance.IsActiveTrampoline())
		{
			this.CreateTrampolineFloor();
		}
	}

	// Token: 0x06003BA3 RID: 15267 RVA: 0x0013AF24 File Offset: 0x00139124
	private void Update()
	{
		if (this.m_createCountMax > 0 && this.m_dataList != null)
		{
			int num = 0;
			for (int i = 0; i < this.m_dataList.Count; i++)
			{
				ObjCreateData objCreateData = this.m_dataList[i];
				if (objCreateData.m_obj == null && !objCreateData.m_create)
				{
					objCreateData.m_create = true;
					objCreateData.m_obj = this.CreateObject(objCreateData.m_src, objCreateData.m_pos, objCreateData.m_rot);
					num++;
					if (num >= this.m_createCountMax)
					{
						break;
					}
				}
			}
			if (num == 0)
			{
				this.m_createCountMax = 0;
			}
		}
	}

	// Token: 0x06003BA4 RID: 15268 RVA: 0x0013AFD8 File Offset: 0x001391D8
	public void SetObjCollisionParameter(ObjTrampolineFloorCollisionParameter param)
	{
		this.m_param = param;
	}

	// Token: 0x06003BA5 RID: 15269 RVA: 0x0013AFE4 File Offset: 0x001391E4
	public void OnTransformPhantom(MsgTransformPhantom msg)
	{
		if (this.m_dataList != null)
		{
			foreach (ObjCreateData objCreateData in this.m_dataList)
			{
				if (objCreateData.m_obj)
				{
					UnityEngine.Object.Destroy(objCreateData.m_obj);
				}
			}
			this.m_dataList.Clear();
		}
		this.m_end = false;
	}

	// Token: 0x06003BA6 RID: 15270 RVA: 0x0013B07C File Offset: 0x0013927C
	private void OnUseItem(MsgUseItem item)
	{
		if (this.m_end)
		{
			return;
		}
		if (item.m_itemType == ItemType.TRAMPOLINE && this.m_createCountMax == 0)
		{
			this.CreateTrampolineFloor();
		}
	}

	// Token: 0x06003BA7 RID: 15271 RVA: 0x0013B0A8 File Offset: 0x001392A8
	private void CreateTrampolineFloor()
	{
		GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.OBJECT_PREFAB, "ObjTrampolineFloor");
		if (gameObject)
		{
			BoxCollider component = base.GetComponent<BoxCollider>();
			if (component)
			{
				int num = Mathf.FloorToInt(component.size.x);
				float num2 = 1f;
				float num3 = 0f;
				float num4 = num2 / 2f;
				if (num % 2 != 0)
				{
					this.AddObject(gameObject, base.transform.position, base.transform.rotation);
					num3 = num4;
				}
				int num5 = num / 2;
				for (int i = 0; i < num5; i++)
				{
					float d = num4 + (float)i * num2 + num3;
					Vector3 pos = base.transform.position + base.transform.right * d;
					this.AddObject(gameObject, pos, base.transform.rotation);
					Vector3 pos2 = base.transform.position + -base.transform.right * d;
					this.AddObject(gameObject, pos2, base.transform.rotation);
				}
				this.m_createCountMax = Mathf.Min(this.m_dataList.Count, 5);
			}
		}
		this.m_end = true;
	}

	// Token: 0x06003BA8 RID: 15272 RVA: 0x0013B1F4 File Offset: 0x001393F4
	private void AddObject(GameObject src, Vector3 pos, Quaternion rot)
	{
		this.m_dataList.Add(new ObjCreateData(src, pos, rot));
	}

	// Token: 0x06003BA9 RID: 15273 RVA: 0x0013B20C File Offset: 0x0013940C
	private GameObject CreateObject(GameObject src, Vector3 pos, Quaternion rot)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(src, pos, rot) as GameObject;
		if (gameObject)
		{
			gameObject.SetActive(true);
			gameObject.transform.parent = base.transform;
			SpawnableObject component = gameObject.GetComponent<SpawnableObject>();
			if (component)
			{
				component.AttachModelObject();
			}
			if (this.m_param != null)
			{
				ObjTrampolineFloor component2 = gameObject.GetComponent<ObjTrampolineFloor>();
				if (component2)
				{
					component2.SetParam(this.m_param.m_firstSpeed, this.m_param.m_outOfcontrol);
				}
			}
		}
		return gameObject;
	}

	// Token: 0x06003BAA RID: 15274 RVA: 0x0013B2A0 File Offset: 0x001394A0
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere(base.transform.position, 0.5f);
	}

	// Token: 0x04003415 RID: 13333
	private const int CREATE_COUNT = 5;

	// Token: 0x04003416 RID: 13334
	private ObjTrampolineFloorCollisionParameter m_param;

	// Token: 0x04003417 RID: 13335
	private List<ObjCreateData> m_dataList = new List<ObjCreateData>();

	// Token: 0x04003418 RID: 13336
	private int m_createCountMax;

	// Token: 0x04003419 RID: 13337
	private bool m_end;
}
