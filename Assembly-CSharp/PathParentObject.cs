using System;
using UnityEngine;

// Token: 0x02000961 RID: 2401
public class PathParentObject : MonoBehaviour
{
	// Token: 0x06003ECA RID: 16074 RVA: 0x00145E78 File Offset: 0x00144078
	private void Update()
	{
		this.UpdatePose();
	}

	// Token: 0x06003ECB RID: 16075 RVA: 0x00145E80 File Offset: 0x00144080
	public void UpdatePose()
	{
		if (base.transform.childCount > 0)
		{
			GameObject gameObject = base.transform.GetChild(0).gameObject;
			if (gameObject)
			{
				Vector3 start = gameObject.transform.position;
				for (int i = 0; i < base.transform.childCount; i++)
				{
					GameObject gameObject2 = base.transform.GetChild(i).gameObject;
					if (gameObject2)
					{
						int num = i + 1;
						if (num < base.transform.childCount)
						{
							GameObject gameObject3 = base.transform.GetChild(num).gameObject;
							if (gameObject3)
							{
								gameObject2.transform.LookAt(gameObject3.transform);
							}
						}
						else
						{
							GameObject gameObject4 = base.transform.GetChild(i - 1).gameObject;
							if (gameObject4)
							{
								gameObject2.transform.rotation = gameObject4.transform.rotation;
							}
						}
						Vector3 position = gameObject2.transform.position;
						global::Debug.DrawLine(start, position, Color.red);
						start = position;
					}
				}
			}
		}
	}

	// Token: 0x06003ECC RID: 16076 RVA: 0x00145FA4 File Offset: 0x001441A4
	public void AddPathObject(string name, float size)
	{
		int childCount = base.transform.childCount;
		if (childCount > 0)
		{
			GameObject pathObject = this.GetPathObject((uint)(childCount - 1));
			if (pathObject != null)
			{
				this.CreatePathObject(name, pathObject.transform.position + PathParentObject.NEW_OFFSET_POS, Quaternion.identity, size);
			}
		}
		else
		{
			this.CreatePathObject(name, Vector3.zero, Quaternion.identity, size);
		}
	}

	// Token: 0x06003ECD RID: 16077 RVA: 0x00146014 File Offset: 0x00144214
	public void CreatePathObject(string name, Vector3 pos, Quaternion rot, float size)
	{
		string name2 = name + base.transform.childCount.ToString();
		GameObject gameObject = new GameObject(name2);
		if (gameObject)
		{
			gameObject.transform.parent = base.transform;
			gameObject.transform.position = pos;
			gameObject.transform.rotation = rot;
			gameObject.SetActive(true);
			SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>();
			if (sphereCollider)
			{
				sphereCollider.radius = size;
			}
		}
	}

	// Token: 0x06003ECE RID: 16078 RVA: 0x00146098 File Offset: 0x00144298
	public int GetPathObjectCount()
	{
		return base.transform.childCount;
	}

	// Token: 0x06003ECF RID: 16079 RVA: 0x001460A8 File Offset: 0x001442A8
	public GameObject GetPathObject(uint index)
	{
		if ((ulong)index < (ulong)((long)base.transform.childCount))
		{
			return base.transform.GetChild((int)index).gameObject;
		}
		return null;
	}

	// Token: 0x06003ED0 RID: 16080 RVA: 0x001460DC File Offset: 0x001442DC
	public void SetZeroZ()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			GameObject gameObject = base.transform.GetChild(i).gameObject;
			if (gameObject)
			{
				gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0f);
			}
		}
	}

	// Token: 0x06003ED1 RID: 16081 RVA: 0x00146160 File Offset: 0x00144360
	private void OnDrawGizmos()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			GameObject gameObject = base.transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Gizmos.color = Color.red;
				Gizmos.DrawSphere(gameObject.transform.position, 0.2f);
			}
		}
	}

	// Token: 0x040035C7 RID: 13767
	private static Vector3 NEW_OFFSET_POS = new Vector3(1f, 0f, 0f);
}
