using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000947 RID: 2375
public class MultiSetBase : SpawnableObject
{
	// Token: 0x06003DFA RID: 15866 RVA: 0x001426DC File Offset: 0x001408DC
	protected override void OnSpawned()
	{
	}

	// Token: 0x06003DFB RID: 15867 RVA: 0x001426E0 File Offset: 0x001408E0
	protected virtual void OnCreateSetup()
	{
	}

	// Token: 0x06003DFC RID: 15868 RVA: 0x001426E4 File Offset: 0x001408E4
	protected virtual void UpdateLocal()
	{
	}

	// Token: 0x06003DFD RID: 15869 RVA: 0x001426E8 File Offset: 0x001408E8
	protected override void OnDestroyed()
	{
		for (int i = 0; i < this.m_dataList.Count; i++)
		{
			if (this.m_dataList[i].m_obj != null)
			{
				SpawnableObject component = this.m_dataList[i].m_obj.GetComponent<SpawnableObject>();
				if (component != null && component.Share)
				{
					base.SetSleep(this.m_dataList[i].m_obj);
				}
			}
		}
	}

	// Token: 0x06003DFE RID: 15870 RVA: 0x00142774 File Offset: 0x00140974
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
					this.m_dataList[i].m_create = true;
					objCreateData.m_obj = this.CreateObject(base.gameObject, objCreateData.m_src, objCreateData.m_pos, objCreateData.m_rot);
					if (objCreateData.m_obj != null)
					{
						this.OnCreateSetup();
					}
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
		this.UpdateLocal();
	}

	// Token: 0x06003DFF RID: 15871 RVA: 0x00142854 File Offset: 0x00140A54
	public void Setup()
	{
		if (this.m_dataList == null)
		{
			this.m_dataList = new List<ObjCreateData>();
		}
		this.m_playerInformation = ObjUtil.GetPlayerInformation();
		this.m_levelInformation = ObjUtil.GetLevelInformation();
	}

	// Token: 0x06003E00 RID: 15872 RVA: 0x00142890 File Offset: 0x00140A90
	public void AddObject(GameObject srcObject, Vector3 pos, Quaternion rot)
	{
		if (this.m_dataList == null)
		{
			return;
		}
		if (srcObject != null)
		{
			SpawnableObject component = srcObject.GetComponent<SpawnableObject>();
			if (component != null && component.IsValid())
			{
				this.m_dataList.Add(new ObjCreateData(srcObject, pos, rot));
			}
		}
		if (this.m_dataList.Count > 0)
		{
			if (this.m_dataList.Count >= MultiSetBase.CREATE_COUNT)
			{
				this.m_createCountMax = this.m_dataList.Count / MultiSetBase.CREATE_COUNT + 1;
			}
			else
			{
				this.m_createCountMax = 1;
			}
		}
	}

	// Token: 0x06003E01 RID: 15873 RVA: 0x00142934 File Offset: 0x00140B34
	private GameObject CreateObject(GameObject parent, GameObject srcObject, Vector3 pos, Quaternion rot)
	{
		GameObject gameObject = ObjUtil.GetChangeObject(ResourceManager.Instance, this.m_playerInformation, this.m_levelInformation, srcObject.name);
		SpawnableObject spawnableObject;
		if (gameObject != null)
		{
			spawnableObject = this.GetReviveSpawnableObject(gameObject);
		}
		else
		{
			gameObject = ObjUtil.GetCrystalChangeObject(ResourceManager.Instance, srcObject);
			if (gameObject != null)
			{
				spawnableObject = this.GetReviveSpawnableObject(gameObject);
			}
			else
			{
				spawnableObject = this.GetReviveSpawnableObject(srcObject);
			}
		}
		GameObject gameObject2;
		if (spawnableObject != null)
		{
			this.SetRevivalSpawnableObject(spawnableObject, pos, rot);
			gameObject2 = spawnableObject.gameObject;
		}
		else
		{
			if (gameObject != null)
			{
				gameObject2 = (UnityEngine.Object.Instantiate(gameObject, pos, rot) as GameObject);
			}
			else
			{
				gameObject2 = (UnityEngine.Object.Instantiate(srcObject, pos, rot) as GameObject);
			}
			spawnableObject = gameObject2.GetComponent<SpawnableObject>();
			if (spawnableObject != null)
			{
				spawnableObject.AttachModelObject();
			}
		}
		if (gameObject2 && parent)
		{
			gameObject2.SetActive(true);
			gameObject2.transform.parent = parent.transform;
		}
		return gameObject2;
	}

	// Token: 0x06003E02 RID: 15874 RVA: 0x00142A44 File Offset: 0x00140C44
	private SpawnableObject GetReviveSpawnableObject(GameObject srcObj)
	{
		if (srcObj == null)
		{
			return null;
		}
		SpawnableObject component = srcObj.GetComponent<SpawnableObject>();
		if (component == null)
		{
			return null;
		}
		ObjectSpawnManager manager = base.GetManager();
		if (manager != null && component.IsStockObject())
		{
			return manager.GetSpawnableObject(component.GetStockObjectType());
		}
		return null;
	}

	// Token: 0x06003E03 RID: 15875 RVA: 0x00142AA0 File Offset: 0x00140CA0
	private void SetRevivalSpawnableObject(SpawnableObject spawnableObject, Vector3 pos, Quaternion rot)
	{
		if (spawnableObject != null)
		{
			spawnableObject.Sleep = false;
			spawnableObject.gameObject.transform.position = pos;
			spawnableObject.gameObject.transform.rotation = rot;
			spawnableObject.OnRevival();
		}
	}

	// Token: 0x0400356D RID: 13677
	private static int CREATE_COUNT = 5;

	// Token: 0x0400356E RID: 13678
	protected List<ObjCreateData> m_dataList;

	// Token: 0x0400356F RID: 13679
	private int m_createCountMax;

	// Token: 0x04003570 RID: 13680
	private PlayerInformation m_playerInformation;

	// Token: 0x04003571 RID: 13681
	private LevelInformation m_levelInformation;
}
