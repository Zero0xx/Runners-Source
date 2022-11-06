using System;
using Message;
using UnityEngine;

// Token: 0x020008F7 RID: 2295
[AddComponentMenu("Scripts/Runners/Object/Common/ObjFloor")]
public class ObjLoopTerrain : SpawnableObject
{
	// Token: 0x06003C9D RID: 15517 RVA: 0x0013EB90 File Offset: 0x0013CD90
	protected override void OnSpawned()
	{
		if (this.m_pathName != null)
		{
			PathManager pathManager = GameObjectUtil.FindGameObjectComponent<PathManager>("StagePathManager");
			if (pathManager != null)
			{
				Vector3 position = base.transform.position;
				position.z += this.m_pathZOffset + 1.5f;
				this.m_component = pathManager.CreatePathComponent(this.m_pathName, position);
			}
		}
	}

	// Token: 0x06003C9E RID: 15518 RVA: 0x0013EBF8 File Offset: 0x0013CDF8
	private void OnDestroy()
	{
		PathManager pathManager = GameObjectUtil.FindGameObjectComponent<PathManager>("StagePathManager");
		if (pathManager != null && this.m_component != null)
		{
			pathManager.DestroyComponent(this.m_component);
		}
		this.m_component = null;
	}

	// Token: 0x06003C9F RID: 15519 RVA: 0x0013EC40 File Offset: 0x0013CE40
	private void OnTriggerEnter(Collider other)
	{
		if (this.m_component != null)
		{
			MsgRunLoopPath value = new MsgRunLoopPath(this.m_component);
			other.gameObject.SendMessage("OnRunLoopPath", value, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06003CA0 RID: 15520 RVA: 0x0013EC7C File Offset: 0x0013CE7C
	public void SetPathName(string pathName)
	{
		this.m_pathName = pathName;
	}

	// Token: 0x06003CA1 RID: 15521 RVA: 0x0013EC88 File Offset: 0x0013CE88
	public void SetZOffset(float zoffset)
	{
		this.m_pathZOffset = zoffset;
	}

	// Token: 0x040034C2 RID: 13506
	private const float Path_DefaultZOFfset = 1.5f;

	// Token: 0x040034C3 RID: 13507
	private PathComponent m_component;

	// Token: 0x040034C4 RID: 13508
	private string m_pathName;

	// Token: 0x040034C5 RID: 13509
	private float m_pathZOffset;
}
