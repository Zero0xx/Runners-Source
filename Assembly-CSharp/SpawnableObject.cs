using System;
using UnityEngine;

// Token: 0x0200027B RID: 635
[AddComponentMenu("Scripts/Runners/Game/Level")]
public abstract class SpawnableObject : MonoBehaviour
{
	// Token: 0x0600114E RID: 4430 RVA: 0x00062AC4 File Offset: 0x00060CC4
	private void Start()
	{
		this.Spawn();
	}

	// Token: 0x0600114F RID: 4431 RVA: 0x00062ACC File Offset: 0x00060CCC
	private void Spawn()
	{
		if (!this.IsSpawnedByManager())
		{
			SpawnableBehavior component = base.GetComponent<SpawnableBehavior>();
			if (component)
			{
				component.SetParameters(component.GetParameter());
			}
		}
		this.OnSpawned();
	}

	// Token: 0x06001150 RID: 4432 RVA: 0x00062B08 File Offset: 0x00060D08
	public GameObject AttachModelObject()
	{
		string modelName = this.GetModelName();
		if (modelName != null)
		{
			ResourceCategory modelCategory = this.GetModelCategory();
			GameObject gameObject = ResourceManager.Instance.GetGameObject(modelCategory, modelName);
			if (gameObject)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, Vector3.zero, Quaternion.identity) as GameObject;
				if (gameObject2)
				{
					gameObject2.SetActive(true);
					gameObject2.transform.parent = base.transform;
					gameObject2.transform.localPosition = Vector3.zero;
					gameObject2.transform.localRotation = Quaternion.Euler(Vector3.zero);
					return gameObject2;
				}
			}
		}
		return null;
	}

	// Token: 0x06001151 RID: 4433 RVA: 0x00062BA4 File Offset: 0x00060DA4
	protected GameObject AttachObject(ResourceCategory category, string objectName)
	{
		return this.AttachObject(category, objectName, Vector3.zero, Quaternion.identity);
	}

	// Token: 0x06001152 RID: 4434 RVA: 0x00062BB8 File Offset: 0x00060DB8
	protected GameObject AttachObject(ResourceCategory category, string objectName, Vector3 localPosition, Quaternion localRotation)
	{
		if (this.IsValid() && objectName != null)
		{
			GameObject gameObject = ResourceManager.Instance.GetGameObject(category, objectName);
			if (gameObject)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, base.transform.position, base.transform.rotation) as GameObject;
				if (gameObject2)
				{
					gameObject2.SetActive(true);
					gameObject2.transform.parent = base.transform;
					gameObject2.transform.localPosition = localPosition;
					gameObject2.transform.localRotation = localRotation;
					return gameObject2;
				}
			}
		}
		return null;
	}

	// Token: 0x06001153 RID: 4435 RVA: 0x00062C50 File Offset: 0x00060E50
	private void OnDestroy()
	{
		this.OnDestroyed();
		ObjectSpawnManager manager = this.GetManager();
		if (this.m_spawnableInfo != null && manager != null)
		{
			manager.DetachObject(this.m_spawnableInfo);
		}
		this.m_spawnableInfo = null;
	}

	// Token: 0x06001154 RID: 4436 RVA: 0x00062C94 File Offset: 0x00060E94
	protected void SetSleep(GameObject obj)
	{
		ObjectSpawnManager manager = this.GetManager();
		if (manager != null)
		{
			SpawnableObject component = obj.GetComponent<SpawnableObject>();
			if (component.IsStockObject())
			{
				manager.SleepSpawnableObject(component);
			}
		}
	}

	// Token: 0x06001155 RID: 4437 RVA: 0x00062CD0 File Offset: 0x00060ED0
	public void SetSleep()
	{
		ObjectSpawnManager manager = this.GetManager();
		if (manager != null && this.IsStockObject())
		{
			manager.SleepSpawnableObject(this);
		}
	}

	// Token: 0x06001156 RID: 4438
	protected abstract void OnSpawned();

	// Token: 0x06001157 RID: 4439 RVA: 0x00062D04 File Offset: 0x00060F04
	public virtual void OnCreate()
	{
	}

	// Token: 0x06001158 RID: 4440 RVA: 0x00062D08 File Offset: 0x00060F08
	public virtual void OnRevival()
	{
	}

	// Token: 0x06001159 RID: 4441 RVA: 0x00062D0C File Offset: 0x00060F0C
	protected virtual void OnDestroyed()
	{
	}

	// Token: 0x0600115A RID: 4442 RVA: 0x00062D10 File Offset: 0x00060F10
	public void AttachSpawnableInfo(SpawnableInfo info)
	{
		this.m_spawnableInfo = info;
	}

	// Token: 0x0600115B RID: 4443 RVA: 0x00062D1C File Offset: 0x00060F1C
	public bool IsSpawnedByManager()
	{
		return this.m_spawnableInfo != null;
	}

	// Token: 0x0600115C RID: 4444 RVA: 0x00062D2C File Offset: 0x00060F2C
	protected ObjectSpawnManager GetManager()
	{
		if (this.m_spawnableInfo != null)
		{
			return this.m_spawnableInfo.Manager;
		}
		return null;
	}

	// Token: 0x0600115D RID: 4445 RVA: 0x00062D48 File Offset: 0x00060F48
	protected virtual string GetModelName()
	{
		return null;
	}

	// Token: 0x0600115E RID: 4446 RVA: 0x00062D4C File Offset: 0x00060F4C
	protected virtual ResourceCategory GetModelCategory()
	{
		return ResourceCategory.UNKNOWN;
	}

	// Token: 0x0600115F RID: 4447 RVA: 0x00062D50 File Offset: 0x00060F50
	protected virtual bool isStatic()
	{
		return false;
	}

	// Token: 0x06001160 RID: 4448 RVA: 0x00062D54 File Offset: 0x00060F54
	public virtual bool IsValid()
	{
		return true;
	}

	// Token: 0x06001161 RID: 4449 RVA: 0x00062D58 File Offset: 0x00060F58
	protected void SetOnlyOneObject()
	{
		if (this.m_spawnableInfo != null)
		{
			this.m_spawnableInfo.AttributeOnlyOne = true;
			ObjectSpawnManager manager = this.GetManager();
			if (manager)
			{
				manager.RegisterOnlyOneObject(this.m_spawnableInfo);
			}
		}
	}

	// Token: 0x06001162 RID: 4450 RVA: 0x00062D9C File Offset: 0x00060F9C
	protected void SetNotRageout(bool value)
	{
		if (this.m_spawnableInfo != null)
		{
			this.m_spawnableInfo.NotRangeOut = value;
		}
	}

	// Token: 0x170002BD RID: 701
	// (get) Token: 0x06001163 RID: 4451 RVA: 0x00062DB8 File Offset: 0x00060FB8
	// (set) Token: 0x06001164 RID: 4452 RVA: 0x00062DC0 File Offset: 0x00060FC0
	public bool Sleep
	{
		get
		{
			return this.m_sleep;
		}
		set
		{
			this.m_sleep = value;
		}
	}

	// Token: 0x170002BE RID: 702
	// (get) Token: 0x06001165 RID: 4453 RVA: 0x00062DCC File Offset: 0x00060FCC
	// (set) Token: 0x06001166 RID: 4454 RVA: 0x00062DD4 File Offset: 0x00060FD4
	public bool Share
	{
		get
		{
			return this.m_share;
		}
		set
		{
			this.m_share = value;
		}
	}

	// Token: 0x06001167 RID: 4455 RVA: 0x00062DE0 File Offset: 0x00060FE0
	public StockObjectType GetStockObjectType()
	{
		return this.m_stockObjectType;
	}

	// Token: 0x06001168 RID: 4456 RVA: 0x00062DE8 File Offset: 0x00060FE8
	public bool IsStockObject()
	{
		return this.m_stockObjectType != StockObjectType.UNKNOWN;
	}

	// Token: 0x04000FA8 RID: 4008
	[SerializeField]
	private StockObjectType m_stockObjectType = StockObjectType.UNKNOWN;

	// Token: 0x04000FA9 RID: 4009
	private SpawnableInfo m_spawnableInfo;

	// Token: 0x04000FAA RID: 4010
	private bool m_sleep;

	// Token: 0x04000FAB RID: 4011
	private bool m_share;
}
