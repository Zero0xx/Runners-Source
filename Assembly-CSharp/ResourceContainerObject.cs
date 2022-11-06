using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009E4 RID: 2532
public class ResourceContainerObject : MonoBehaviour
{
	// Token: 0x060042B6 RID: 17078 RVA: 0x0015ABF0 File Offset: 0x00158DF0
	private void Start()
	{
	}

	// Token: 0x060042B7 RID: 17079 RVA: 0x0015ABF4 File Offset: 0x00158DF4
	private void Update()
	{
	}

	// Token: 0x060042B8 RID: 17080 RVA: 0x0015ABF8 File Offset: 0x00158DF8
	public ResourceContainer GetContainer(string name)
	{
		ResourceContainer result = null;
		this.m_resContainer.TryGetValue(name, out result);
		return result;
	}

	// Token: 0x060042B9 RID: 17081 RVA: 0x0015AC18 File Offset: 0x00158E18
	public ResourceContainer CreateContainer(string name)
	{
		ResourceContainer resourceContainer = new ResourceContainer(this.Category, name);
		this.m_resContainer.Add(name, resourceContainer);
		return resourceContainer;
	}

	// Token: 0x060042BA RID: 17082 RVA: 0x0015AC40 File Offset: 0x00158E40
	public ResourceContainer CreateEmptyContainer(string name)
	{
		ResourceContainer resourceContainer = new ResourceContainer(this.Category, name);
		this.m_resContainer.Add(name, resourceContainer);
		GameObject gameObject = new GameObject(name);
		resourceContainer.SetRootObject(gameObject);
		resourceContainer.DontDestroyOnChangeScene = true;
		gameObject.transform.parent = base.transform;
		return resourceContainer;
	}

	// Token: 0x060042BB RID: 17083 RVA: 0x0015AC90 File Offset: 0x00158E90
	public GameObject GetGameObject(string name)
	{
		foreach (ResourceContainer resourceContainer in this.m_resContainer.Values)
		{
			if (resourceContainer.Active)
			{
				GameObject @object = resourceContainer.GetObject(name);
				if (@object != null)
				{
					return @object;
				}
			}
		}
		return null;
	}

	// Token: 0x060042BC RID: 17084 RVA: 0x0015AD24 File Offset: 0x00158F24
	public void RemoveAllResources()
	{
		foreach (ResourceContainer resourceContainer in this.m_resContainer.Values)
		{
			resourceContainer.RemoveAllResources();
		}
		this.m_resContainer.Clear();
	}

	// Token: 0x060042BD RID: 17085 RVA: 0x0015AD9C File Offset: 0x00158F9C
	public void RemoveResourcesOnThisScene()
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, ResourceContainer> keyValuePair in this.m_resContainer)
		{
			if (!keyValuePair.Value.DontDestroyOnChangeScene)
			{
				keyValuePair.Value.RemoveAllResources();
				list.Add(keyValuePair.Key);
			}
			else
			{
				keyValuePair.Value.RemoveResourcesOnThisScene();
			}
		}
		foreach (string key in list)
		{
			this.m_resContainer.Remove(key);
		}
	}

	// Token: 0x060042BE RID: 17086 RVA: 0x0015AE94 File Offset: 0x00159094
	public void RemoveResources(string[] removeList)
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, ResourceContainer> keyValuePair in this.m_resContainer)
		{
			string key = keyValuePair.Key;
			ResourceContainer value = keyValuePair.Value;
			if (value.Active)
			{
				value.RemoveResources(removeList);
				foreach (string text in removeList)
				{
					if (!string.IsNullOrEmpty(text))
					{
						if (value.Name == text)
						{
							list.Add(key);
							break;
						}
					}
				}
			}
		}
		if (list.Count > 0)
		{
			foreach (string text2 in list)
			{
				if (!string.IsNullOrEmpty(text2))
				{
					this.m_resContainer.Remove(text2);
				}
			}
		}
	}

	// Token: 0x060042BF RID: 17087 RVA: 0x0015AFF0 File Offset: 0x001591F0
	public void SetContainerActive(string name, bool value)
	{
		ResourceContainer container = this.GetContainer(name);
		if (container != null)
		{
			container.Active = value;
		}
	}

	// Token: 0x060042C0 RID: 17088 RVA: 0x0015B014 File Offset: 0x00159214
	public void RemoveNotActiveContainer()
	{
		List<string> list = new List<string>();
		foreach (ResourceContainer resourceContainer in this.m_resContainer.Values)
		{
			if (!resourceContainer.Active)
			{
				list.Add(resourceContainer.Name);
			}
		}
		foreach (string name in list)
		{
			this.RemoveContainer(name);
		}
	}

	// Token: 0x060042C1 RID: 17089 RVA: 0x0015B0E8 File Offset: 0x001592E8
	private void RemoveContainer(string name)
	{
		ResourceContainer container = this.GetContainer(name);
		if (container != null)
		{
			container.RemoveAllResources();
			this.m_resContainer.Remove(name);
		}
	}

	// Token: 0x17000922 RID: 2338
	// (get) Token: 0x060042C3 RID: 17091 RVA: 0x0015B124 File Offset: 0x00159324
	// (set) Token: 0x060042C2 RID: 17090 RVA: 0x0015B118 File Offset: 0x00159318
	public ResourceCategory Category { get; set; }

	// Token: 0x040038AF RID: 14511
	private Dictionary<string, ResourceContainer> m_resContainer = new Dictionary<string, ResourceContainer>();
}
