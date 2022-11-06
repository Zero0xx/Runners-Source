using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009E3 RID: 2531
public class ResourceContainer
{
	// Token: 0x060042A5 RID: 17061 RVA: 0x0015A7EC File Offset: 0x001589EC
	public ResourceContainer(ResourceCategory category, string name)
	{
		this.m_rootResource = new ResourceInfo(category);
		this.m_resources = new Dictionary<string, ResourceInfo>();
		this.m_name = name;
	}

	// Token: 0x1700091E RID: 2334
	// (get) Token: 0x060042A6 RID: 17062 RVA: 0x0015A81C File Offset: 0x00158A1C
	public string Name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x1700091F RID: 2335
	// (get) Token: 0x060042A7 RID: 17063 RVA: 0x0015A824 File Offset: 0x00158A24
	public ResourceCategory Category
	{
		get
		{
			return this.m_rootResource.Category;
		}
	}

	// Token: 0x17000920 RID: 2336
	// (get) Token: 0x060042A9 RID: 17065 RVA: 0x0015A840 File Offset: 0x00158A40
	// (set) Token: 0x060042A8 RID: 17064 RVA: 0x0015A834 File Offset: 0x00158A34
	public bool Active
	{
		get
		{
			return this.m_active;
		}
		set
		{
			this.m_active = value;
		}
	}

	// Token: 0x060042AA RID: 17066 RVA: 0x0015A848 File Offset: 0x00158A48
	public bool SetRootObject(GameObject srcObject)
	{
		if (this.m_rootResource.ResObject == null)
		{
			this.m_rootResource.ResObject = srcObject;
			return true;
		}
		return false;
	}

	// Token: 0x060042AB RID: 17067 RVA: 0x0015A870 File Offset: 0x00158A70
	public bool SetRootObject(ResourceInfo resInfo)
	{
		if (this.m_rootResource.ResObject == null)
		{
			resInfo.CopyTo(this.m_rootResource);
			return true;
		}
		return false;
	}

	// Token: 0x060042AC RID: 17068 RVA: 0x0015A898 File Offset: 0x00158A98
	public void AddChildObject(GameObject srcObject, bool dontDestoryOnChangeScene)
	{
		ResourceInfo resourceInfo = new ResourceInfo(this.m_rootResource.Category);
		resourceInfo.ResObject = srcObject;
		resourceInfo.PathName = this.m_rootResource.PathName;
		resourceInfo.DontDestroyOnChangeScene = dontDestoryOnChangeScene;
		this.m_resources.Add(srcObject.name, resourceInfo);
	}

	// Token: 0x060042AD RID: 17069 RVA: 0x0015A8E8 File Offset: 0x00158AE8
	public bool IsExist(GameObject gameObject)
	{
		return this.m_resources.ContainsKey(gameObject.name);
	}

	// Token: 0x060042AE RID: 17070 RVA: 0x0015A8FC File Offset: 0x00158AFC
	public GameObject GetObject(string objectName)
	{
		if (this.m_rootResource.ResObject != null && this.m_rootResource.ResObject.name == objectName)
		{
			return this.m_rootResource.ResObject;
		}
		ResourceInfo resourceInfo;
		this.m_resources.TryGetValue(objectName, out resourceInfo);
		if (resourceInfo != null)
		{
			return resourceInfo.ResObject;
		}
		return null;
	}

	// Token: 0x060042AF RID: 17071 RVA: 0x0015A964 File Offset: 0x00158B64
	public void RemoveAllResources()
	{
		foreach (ResourceInfo info in this.m_resources.Values)
		{
			this.DestroyObject(info);
		}
		this.m_resources.Clear();
		if (this.m_rootResource != null && this.m_rootResource.ResObject != null)
		{
			UnityEngine.Object.Destroy(this.m_rootResource.ResObject);
			this.m_rootResource = null;
		}
	}

	// Token: 0x060042B0 RID: 17072 RVA: 0x0015AA14 File Offset: 0x00158C14
	public void RemoveResourcesOnThisScene()
	{
		List<string> list = new List<string>();
		foreach (ResourceInfo resourceInfo in this.m_resources.Values)
		{
			if (!resourceInfo.DontDestroyOnChangeScene)
			{
				string name = resourceInfo.ResObject.name;
				this.DestroyObject(resourceInfo);
				list.Add(name);
			}
		}
		foreach (string key in list)
		{
			this.m_resources.Remove(key);
		}
	}

	// Token: 0x060042B1 RID: 17073 RVA: 0x0015AB00 File Offset: 0x00158D00
	public void RemoveResources(string[] names)
	{
		foreach (string text in names)
		{
			ResourceInfo resourceInfo;
			this.m_resources.TryGetValue(text, out resourceInfo);
			if (resourceInfo != null)
			{
				this.DestroyObject(resourceInfo);
				this.m_resources.Remove(text);
			}
			if (this.m_rootResource.ResObject != null && this.m_rootResource.ResObject.name == text)
			{
				this.DestroyObject(this.m_rootResource);
				break;
			}
		}
	}

	// Token: 0x060042B2 RID: 17074 RVA: 0x0015AB94 File Offset: 0x00158D94
	private void DestroyObject(ResourceInfo info)
	{
		UnityEngine.Object.Destroy(info.ResObject);
	}

	// Token: 0x17000921 RID: 2337
	// (get) Token: 0x060042B3 RID: 17075 RVA: 0x0015ABA4 File Offset: 0x00158DA4
	// (set) Token: 0x060042B4 RID: 17076 RVA: 0x0015ABC0 File Offset: 0x00158DC0
	public bool DontDestroyOnChangeScene
	{
		get
		{
			return this.m_rootResource != null && this.m_rootResource.DontDestroyOnChangeScene;
		}
		set
		{
			if (this.m_rootResource != null)
			{
				this.m_rootResource.DontDestroyOnChangeScene = value;
			}
		}
	}

	// Token: 0x040038AB RID: 14507
	private bool m_active = true;

	// Token: 0x040038AC RID: 14508
	private ResourceInfo m_rootResource;

	// Token: 0x040038AD RID: 14509
	private Dictionary<string, ResourceInfo> m_resources;

	// Token: 0x040038AE RID: 14510
	private string m_name;
}
