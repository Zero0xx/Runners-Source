using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002A0 RID: 672
public class PathManager : MonoBehaviour
{
	// Token: 0x170002FC RID: 764
	// (get) Token: 0x06001299 RID: 4761 RVA: 0x00067344 File Offset: 0x00065544
	// (set) Token: 0x0600129A RID: 4762 RVA: 0x0006734C File Offset: 0x0006554C
	public bool SetupEnd { get; private set; }

	// Token: 0x0600129B RID: 4763 RVA: 0x00067358 File Offset: 0x00065558
	private void Start()
	{
		if (this.m_pathList == null)
		{
			this.m_pathList = new Dictionary<string, ResPathObjectData>();
		}
	}

	// Token: 0x170002FD RID: 765
	// (get) Token: 0x0600129C RID: 4764 RVA: 0x00067370 File Offset: 0x00065570
	public Dictionary<string, ResPathObjectData> PathList
	{
		get
		{
			return this.m_pathList;
		}
	}

	// Token: 0x0600129D RID: 4765 RVA: 0x00067378 File Offset: 0x00065578
	public void CreatePathObjectData()
	{
		ResourceManager instance = ResourceManager.Instance;
		if (this.m_pathList == null)
		{
			this.m_pathList = new Dictionary<string, ResPathObjectData>();
		}
		GameObject gameObject = instance.GetGameObject(ResourceCategory.TERRAIN_MODEL, TerrainXmlData.DataAssetName);
		if (gameObject)
		{
			TerrainXmlData component = gameObject.GetComponent<TerrainXmlData>();
			base.StartCoroutine(this.ParseAndCreateDatas(component));
		}
	}

	// Token: 0x0600129E RID: 4766 RVA: 0x000673D0 File Offset: 0x000655D0
	public PathComponent CreatePathComponent(string name, Vector3 rootPosition)
	{
		ResPathObject resPathObject = this.CreatePathObject(name, rootPosition);
		if (resPathObject != null)
		{
			GameObject gameObject = new GameObject("PathComponentObject");
			PathComponent pathComponent = gameObject.AddComponent<PathComponent>();
			if (pathComponent)
			{
				gameObject.transform.position = rootPosition;
				gameObject.transform.parent = base.transform;
				pathComponent.SetManager(this);
				pathComponent.SetObject(resPathObject);
				pathComponent.SetID(this.m_idCounter);
				this.m_idCounter += 1U;
				return pathComponent;
			}
		}
		return null;
	}

	// Token: 0x0600129F RID: 4767 RVA: 0x00067454 File Offset: 0x00065654
	public PathComponent GetPathComponent(string name)
	{
		PathComponent[] componentsInChildren = base.GetComponentsInChildren<PathComponent>();
		foreach (PathComponent pathComponent in componentsInChildren)
		{
			if (pathComponent.GetName() == name)
			{
				return pathComponent;
			}
		}
		return null;
	}

	// Token: 0x060012A0 RID: 4768 RVA: 0x00067498 File Offset: 0x00065698
	public PathComponent GetPathComponent(uint id)
	{
		PathComponent[] componentsInChildren = base.GetComponentsInChildren<PathComponent>();
		foreach (PathComponent pathComponent in componentsInChildren)
		{
			if (pathComponent.GetID() == id)
			{
				return pathComponent;
			}
		}
		return null;
	}

	// Token: 0x060012A1 RID: 4769 RVA: 0x000674D8 File Offset: 0x000656D8
	private ResPathObject CreatePathObject(string name, Vector3 rootPosition)
	{
		if (this.m_pathList == null)
		{
			return null;
		}
		string key = name.ToLower();
		ResPathObjectData resPathObjectData;
		this.m_pathList.TryGetValue(key, out resPathObjectData);
		if (resPathObjectData != null)
		{
			return new ResPathObject(resPathObjectData, rootPosition);
		}
		return null;
	}

	// Token: 0x060012A2 RID: 4770 RVA: 0x0006751C File Offset: 0x0006571C
	public void DestroyComponent(string pathname)
	{
		PathComponent pathComponent = this.GetPathComponent(pathname);
		this.DestroyComponent(pathComponent);
	}

	// Token: 0x060012A3 RID: 4771 RVA: 0x00067538 File Offset: 0x00065738
	public void DestroyComponent(PathComponent component)
	{
		if (component != null)
		{
			UnityEngine.Object.Destroy(component.gameObject);
		}
	}

	// Token: 0x060012A4 RID: 4772 RVA: 0x00067554 File Offset: 0x00065754
	public string GetSVPathName()
	{
		return this.m_svPathName;
	}

	// Token: 0x060012A5 RID: 4773 RVA: 0x0006755C File Offset: 0x0006575C
	private IEnumerator ParseAndCreateDatas(TerrainXmlData terrainData)
	{
		if (terrainData != null)
		{
			TextAsset asset = terrainData.LoopPath;
			if (asset != null && asset.text != null)
			{
				yield return base.StartCoroutine(PathXmlDeserializer.CreatePathObjectData(asset, this.m_pathList));
			}
			asset = terrainData.SideViewPath;
			if (asset != null)
			{
				this.m_svPathName = asset.name;
			}
			if (asset != null && asset.text != null)
			{
				yield return base.StartCoroutine(PathXmlDeserializer.CreatePathObjectData(asset, this.m_pathList));
			}
		}
		this.SetupEnd = true;
		yield break;
	}

	// Token: 0x060012A6 RID: 4774 RVA: 0x00067588 File Offset: 0x00065788
	private void Update()
	{
	}

	// Token: 0x060012A7 RID: 4775 RVA: 0x0006758C File Offset: 0x0006578C
	private void OnDrawGizmos()
	{
	}

	// Token: 0x0400108B RID: 4235
	private Dictionary<string, ResPathObjectData> m_pathList;

	// Token: 0x0400108C RID: 4236
	private uint m_idCounter;

	// Token: 0x0400108D RID: 4237
	public bool m_drawGismos;

	// Token: 0x0400108E RID: 4238
	private string m_svPathName = string.Empty;
}
