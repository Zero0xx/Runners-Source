using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009E6 RID: 2534
public class ResourceManager : MonoBehaviour
{
	// Token: 0x060042C6 RID: 17094 RVA: 0x0015B13C File Offset: 0x0015933C
	protected void Awake()
	{
		this.CheckInstance();
	}

	// Token: 0x060042C7 RID: 17095 RVA: 0x0015B148 File Offset: 0x00159348
	private void Start()
	{
		this.Initialize();
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x060042C8 RID: 17096 RVA: 0x0015B15C File Offset: 0x0015935C
	private void Initialize()
	{
		if (this.m_container == null)
		{
			this.m_container = new Dictionary<ResourceCategory, ResourceContainerObject>();
			for (ResourceCategory resourceCategory = ResourceCategory.OBJECT_RESOURCE; resourceCategory < ResourceCategory.NUM; resourceCategory++)
			{
				ResourceContainerObject resourceContainerObject = new GameObject(resourceCategory.ToString())
				{
					transform = 
					{
						parent = base.transform
					}
				}.AddComponent<ResourceContainerObject>();
				resourceContainerObject.Category = resourceCategory;
				resourceContainerObject.CreateEmptyContainer("SimpleContainer");
				this.m_container.Add(resourceCategory, resourceContainerObject);
			}
		}
	}

	// Token: 0x060042C9 RID: 17097 RVA: 0x0015B1DC File Offset: 0x001593DC
	private void Update()
	{
	}

	// Token: 0x060042CA RID: 17098 RVA: 0x0015B1E0 File Offset: 0x001593E0
	private void OnDestroy()
	{
		if (this.m_container != null)
		{
			this.RemoveAllResources();
		}
		if (ResourceManager.instance == this)
		{
			ResourceManager.instance = null;
		}
	}

	// Token: 0x060042CB RID: 17099 RVA: 0x0015B20C File Offset: 0x0015940C
	public void AddCategorySceneObjects(ResourceCategory category, string containerName, GameObject resourceRootObject, bool dontDestroyOnChangeScene)
	{
		if (resourceRootObject == null)
		{
			return;
		}
		ResourceContainerObject resourceContainerObject = this.m_container[category];
		if (containerName == null)
		{
			containerName = resourceRootObject.name;
		}
		ResourceContainer resourceContainer = resourceContainerObject.GetContainer(containerName);
		if (resourceContainer == null)
		{
			resourceContainer = resourceContainerObject.CreateContainer(containerName);
			resourceContainer.DontDestroyOnChangeScene = dontDestroyOnChangeScene;
			resourceContainer.SetRootObject(resourceRootObject);
			resourceRootObject.transform.parent = resourceContainerObject.gameObject.transform;
		}
		foreach (object obj in resourceRootObject.transform)
		{
			Transform transform = (Transform)obj;
			if (!resourceContainer.IsExist(transform.gameObject))
			{
				resourceContainer.AddChildObject(transform.gameObject, dontDestroyOnChangeScene);
				transform.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x060042CC RID: 17100 RVA: 0x0015B308 File Offset: 0x00159508
	public void AddCategorySceneObjectsAndSetActive(ResourceCategory category, string containerName, GameObject resourceRootObject, bool dontDestroyOnSceneChange)
	{
		this.AddCategorySceneObjects(category, containerName, resourceRootObject, dontDestroyOnSceneChange);
		this.SetContainerActive(category, containerName, true);
	}

	// Token: 0x060042CD RID: 17101 RVA: 0x0015B320 File Offset: 0x00159520
	public GameObject GetGameObject(ResourceCategory category, string name)
	{
		return this.m_container[category].GetGameObject(name);
	}

	// Token: 0x060042CE RID: 17102 RVA: 0x0015B334 File Offset: 0x00159534
	public GameObject GetSpawnableGameObject(string name)
	{
		ResourceCategory[] array = new ResourceCategory[]
		{
			ResourceCategory.OBJECT_PREFAB,
			ResourceCategory.ENEMY_PREFAB,
			ResourceCategory.STAGE_RESOURCE,
			ResourceCategory.EVENT_RESOURCE
		};
		foreach (ResourceCategory category in array)
		{
			GameObject gameObject = this.GetGameObject(category, name);
			if (gameObject)
			{
				return gameObject;
			}
		}
		return null;
	}

	// Token: 0x060042CF RID: 17103 RVA: 0x0015B390 File Offset: 0x00159590
	public bool IsExistContainer(string name)
	{
		for (ResourceCategory resourceCategory = ResourceCategory.OBJECT_RESOURCE; resourceCategory < ResourceCategory.NUM; resourceCategory++)
		{
			if (this.m_container[resourceCategory] == null)
			{
				return false;
			}
			if (this.m_container[resourceCategory].GetContainer(name) != null)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060042D0 RID: 17104 RVA: 0x0015B3E4 File Offset: 0x001595E4
	private void AddObject(ResourceCategory category, GameObject addObject, bool dontDestroyOnChangeScene)
	{
		this.AddObject(category, "SimpleContainer", addObject, dontDestroyOnChangeScene);
	}

	// Token: 0x060042D1 RID: 17105 RVA: 0x0015B3F4 File Offset: 0x001595F4
	private void AddObject(ResourceCategory category, string containerName, GameObject addObject, bool dontDestroyOnChangeScene)
	{
		if (this.m_container == null)
		{
			return;
		}
		if (category == ResourceCategory.ETC)
		{
			dontDestroyOnChangeScene = false;
		}
		ResourceContainer container = this.m_container[category].GetContainer(containerName);
		if (container != null)
		{
			container.AddChildObject(addObject, dontDestroyOnChangeScene);
		}
	}

	// Token: 0x060042D2 RID: 17106 RVA: 0x0015B43C File Offset: 0x0015963C
	public void RemoveAllResources()
	{
		if (this.m_container == null)
		{
			return;
		}
		foreach (ResourceContainerObject resourceContainerObject in this.m_container.Values)
		{
			resourceContainerObject.RemoveAllResources();
		}
	}

	// Token: 0x060042D3 RID: 17107 RVA: 0x0015B4B4 File Offset: 0x001596B4
	public void RemoveResourcesOnThisScene()
	{
		foreach (ResourceContainerObject resourceContainerObject in this.m_container.Values)
		{
			resourceContainerObject.RemoveResourcesOnThisScene();
		}
	}

	// Token: 0x060042D4 RID: 17108 RVA: 0x0015B520 File Offset: 0x00159720
	public void RemoveResources(ResourceCategory category)
	{
		this.m_container[category].RemoveAllResources();
	}

	// Token: 0x060042D5 RID: 17109 RVA: 0x0015B534 File Offset: 0x00159734
	public void RemoveResources(ResourceCategory category, string[] removeList)
	{
		this.m_container[category].RemoveResources(removeList);
	}

	// Token: 0x060042D6 RID: 17110 RVA: 0x0015B548 File Offset: 0x00159748
	public void SetContainerActive(ResourceCategory category, string name, bool value)
	{
		this.m_container[category].SetContainerActive(name, value);
	}

	// Token: 0x060042D7 RID: 17111 RVA: 0x0015B560 File Offset: 0x00159760
	public void RemoveNotActiveContainer(ResourceCategory category)
	{
		this.m_container[category].RemoveNotActiveContainer();
	}

	// Token: 0x17000923 RID: 2339
	// (get) Token: 0x060042D8 RID: 17112 RVA: 0x0015B574 File Offset: 0x00159774
	public static ResourceManager Instance
	{
		get
		{
			return ResourceManager.instance;
		}
	}

	// Token: 0x060042D9 RID: 17113 RVA: 0x0015B57C File Offset: 0x0015977C
	protected bool CheckInstance()
	{
		if (ResourceManager.instance == null)
		{
			ResourceManager.instance = this;
			this.Initialize();
			return true;
		}
		if (this == ResourceManager.Instance)
		{
			return true;
		}
		UnityEngine.Object.Destroy(base.gameObject);
		return false;
	}

	// Token: 0x040038B1 RID: 14513
	private const string SimpleContainerName = "SimpleContainer";

	// Token: 0x040038B2 RID: 14514
	private Dictionary<ResourceCategory, ResourceContainerObject> m_container;

	// Token: 0x040038B3 RID: 14515
	private static ResourceManager instance;
}
