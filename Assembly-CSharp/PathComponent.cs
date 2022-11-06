using System;
using UnityEngine;

// Token: 0x0200029D RID: 669
public class PathComponent : MonoBehaviour
{
	// Token: 0x06001255 RID: 4693 RVA: 0x00066900 File Offset: 0x00064B00
	public void SetObject(ResPathObject pathObject)
	{
		this.m_resPathObject = pathObject;
	}

	// Token: 0x06001256 RID: 4694 RVA: 0x0006690C File Offset: 0x00064B0C
	public ResPathObject GetResPathObject()
	{
		return this.m_resPathObject;
	}

	// Token: 0x06001257 RID: 4695 RVA: 0x00066914 File Offset: 0x00064B14
	private PathManager GetManager()
	{
		return this.m_pathManager;
	}

	// Token: 0x06001258 RID: 4696 RVA: 0x0006691C File Offset: 0x00064B1C
	public string GetName()
	{
		if (this.m_resPathObject != null)
		{
			return this.m_resPathObject.Name;
		}
		return null;
	}

	// Token: 0x06001259 RID: 4697 RVA: 0x00066938 File Offset: 0x00064B38
	public uint GetID()
	{
		return this.m_id;
	}

	// Token: 0x0600125A RID: 4698 RVA: 0x00066940 File Offset: 0x00064B40
	public bool IsValid()
	{
		return this.m_resPathObject != null;
	}

	// Token: 0x0600125B RID: 4699 RVA: 0x00066950 File Offset: 0x00064B50
	private void Cleanup()
	{
		if (this.m_pathManager != null)
		{
			this.m_pathManager = null;
		}
		this.m_resPathObject = null;
	}

	// Token: 0x0600125C RID: 4700 RVA: 0x00066974 File Offset: 0x00064B74
	private void OnDestroy()
	{
		this.Cleanup();
	}

	// Token: 0x0600125D RID: 4701 RVA: 0x0006697C File Offset: 0x00064B7C
	public void SetManager(PathManager manager)
	{
		this.m_pathManager = manager;
	}

	// Token: 0x0600125E RID: 4702 RVA: 0x00066988 File Offset: 0x00064B88
	public void SetID(uint id)
	{
		this.m_id = id;
	}

	// Token: 0x0600125F RID: 4703 RVA: 0x00066994 File Offset: 0x00064B94
	public void DrawGizmos()
	{
		if (this.m_resPathObject == null)
		{
			return;
		}
		ResPathObjectData objectData = this.m_resPathObject.GetObjectData();
		Vector3 to = objectData.position[0];
		for (int i = 0; i < (int)objectData.numKeys; i++)
		{
			Vector3 vector = objectData.position[i];
			Vector3 a = objectData.normal[i];
			float d = 0.2f;
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(vector, vector + a * d);
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine(vector, to);
			to = vector;
		}
	}

	// Token: 0x04001076 RID: 4214
	private ResPathObject m_resPathObject;

	// Token: 0x04001077 RID: 4215
	private PathManager m_pathManager;

	// Token: 0x04001078 RID: 4216
	private uint m_id;
}
