using System;
using UnityEngine;

// Token: 0x0200029B RID: 667
public class BlockPathController : MonoBehaviour
{
	// Token: 0x06001246 RID: 4678 RVA: 0x000664D8 File Offset: 0x000646D8
	private void Start()
	{
		this.m_dispInfo = false;
	}

	// Token: 0x06001247 RID: 4679 RVA: 0x000664E4 File Offset: 0x000646E4
	private void Update()
	{
		if (!this.m_nowCurrent)
		{
			return;
		}
		if (this.m_pathEvaluator == null || this.m_playerInformation == null)
		{
			return;
		}
		Vector3 position = this.m_playerInformation.Position;
		position.y = 0f;
		foreach (PathEvaluator pathEvaluator2 in this.m_pathEvaluator)
		{
			if (pathEvaluator2 != null)
			{
				float distance = pathEvaluator2.Distance;
				float distance2 = distance;
				pathEvaluator2.GetClosestPositionAlongSpline(position, distance - 5f, distance + 5f, out distance2);
				pathEvaluator2.Distance = distance2;
			}
		}
	}

	// Token: 0x06001248 RID: 4680 RVA: 0x0006658C File Offset: 0x0006478C
	private void OnDestroy()
	{
		if (this.m_pathManager)
		{
			this.DestroyPathEvaluator();
			if (this.m_pathComponent != null)
			{
				for (int i = 0; i < 3; i++)
				{
					if (this.m_pathComponent[i] != null)
					{
						this.m_pathManager.DestroyComponent(this.m_pathComponent[i]);
					}
					this.m_pathComponent[i] = null;
				}
				this.m_pathComponent = null;
			}
			this.m_pathManager = null;
		}
	}

	// Token: 0x06001249 RID: 4681 RVA: 0x0006660C File Offset: 0x0006480C
	public void Initialize(string stageName, int numBlock, int activateID, PathManager manager, Vector3 rootPosition)
	{
		this.m_numBlock = numBlock;
		this.m_pathManager = manager;
		this.m_activateID = activateID;
		this.m_playerInformation = GameObjectUtil.FindGameObjectComponent<PlayerInformation>("PlayerInformation");
		this.m_pathComponent = new PathComponent[3];
		for (int i = 0; i < 3; i++)
		{
			string name = stageName + "Terrain" + numBlock.ToString("D2") + BlockPathController.path_name_suffix[i];
			this.m_pathComponent[i] = manager.CreatePathComponent(name, rootPosition);
			if (i > 0 && this.m_pathComponent[i] == null && this.m_pathComponent[0] != null)
			{
				Vector3 rootPosition2 = rootPosition + ((i != 1) ? new Vector3(0f, 5f, 0f) : new Vector3(0f, -2f, 0f));
				this.m_pathComponent[i] = manager.CreatePathComponent(name, rootPosition2);
			}
		}
		base.transform.position = rootPosition;
	}

	// Token: 0x0600124A RID: 4682 RVA: 0x00066718 File Offset: 0x00064918
	public void SetCurrent(bool value)
	{
		if (!this.IsNowCurrent() && value)
		{
			this.CreatePathEvaluator();
		}
		else if (this.IsNowCurrent() && !value)
		{
			this.m_nowCurrent = value;
		}
		this.m_nowCurrent = value;
	}

	// Token: 0x0600124B RID: 4683 RVA: 0x00066758 File Offset: 0x00064958
	public bool IsNowCurrent()
	{
		return this.m_nowCurrent;
	}

	// Token: 0x0600124C RID: 4684 RVA: 0x00066760 File Offset: 0x00064960
	public PathEvaluator GetEvaluator(BlockPathController.PathType type)
	{
		if (this.m_pathEvaluator == null)
		{
			return null;
		}
		return this.m_pathEvaluator[(int)type];
	}

	// Token: 0x0600124D RID: 4685 RVA: 0x00066778 File Offset: 0x00064978
	public PathComponent GetComponent(BlockPathController.PathType type)
	{
		if (this.m_pathComponent == null)
		{
			return null;
		}
		return this.m_pathComponent[(int)type];
	}

	// Token: 0x0600124E RID: 4686 RVA: 0x00066790 File Offset: 0x00064990
	public bool GetPNT(BlockPathController.PathType type, ref Vector3? pos, ref Vector3? nrm, ref Vector3? tan)
	{
		PathEvaluator evaluator = this.GetEvaluator(type);
		if (evaluator == null)
		{
			return false;
		}
		evaluator.GetPNT(ref pos, ref nrm, ref tan);
		return true;
	}

	// Token: 0x170002EA RID: 746
	// (get) Token: 0x0600124F RID: 4687 RVA: 0x000667B8 File Offset: 0x000649B8
	public int BlockNo
	{
		get
		{
			return this.m_numBlock;
		}
	}

	// Token: 0x170002EB RID: 747
	// (get) Token: 0x06001250 RID: 4688 RVA: 0x000667C0 File Offset: 0x000649C0
	public int ActivateID
	{
		get
		{
			return this.m_activateID;
		}
	}

	// Token: 0x06001251 RID: 4689 RVA: 0x000667C8 File Offset: 0x000649C8
	private void CreatePathEvaluator()
	{
		if (this.m_pathComponent[0] != null)
		{
			this.m_pathEvaluator = new PathEvaluator[3];
			for (int i = 0; i < 3; i++)
			{
				if (!(this.m_pathComponent[i] == null))
				{
					if (this.m_pathComponent[i].IsValid())
					{
						this.m_pathEvaluator[i] = new PathEvaluator();
						this.m_pathEvaluator[i].SetPathObject(this.m_pathComponent[i]);
						if (this.m_playerInformation != null)
						{
							Vector3 position = this.m_playerInformation.Position;
							float distance = 0f;
							this.m_pathEvaluator[i].GetClosestPositionAlongSpline(position, 0f, this.m_pathEvaluator[i].GetLength(), out distance);
							this.m_pathEvaluator[i].Distance = distance;
						}
					}
				}
			}
		}
	}

	// Token: 0x06001252 RID: 4690 RVA: 0x000668AC File Offset: 0x00064AAC
	private void DestroyPathEvaluator()
	{
		if (this.m_pathEvaluator != null)
		{
			for (int i = 0; i < 3; i++)
			{
				this.m_pathEvaluator[i] = null;
			}
			this.m_pathEvaluator = null;
		}
	}

	// Token: 0x06001253 RID: 4691 RVA: 0x000668E8 File Offset: 0x00064AE8
	private void OnDrawGizmos()
	{
	}

	// Token: 0x04001066 RID: 4198
	private int m_numBlock;

	// Token: 0x04001067 RID: 4199
	private int m_activateID;

	// Token: 0x04001068 RID: 4200
	private bool m_nowCurrent;

	// Token: 0x04001069 RID: 4201
	private PlayerInformation m_playerInformation;

	// Token: 0x0400106A RID: 4202
	private PathManager m_pathManager;

	// Token: 0x0400106B RID: 4203
	[SerializeField]
	private bool m_drawGismos;

	// Token: 0x0400106C RID: 4204
	[SerializeField]
	private bool m_dispInfo;

	// Token: 0x0400106D RID: 4205
	private Rect m_window;

	// Token: 0x0400106E RID: 4206
	private PathComponent[] m_pathComponent;

	// Token: 0x0400106F RID: 4207
	private PathEvaluator[] m_pathEvaluator;

	// Token: 0x04001070 RID: 4208
	private static readonly string[] path_name_suffix = new string[]
	{
		"_sv",
		"_dr",
		"_ls"
	};

	// Token: 0x0200029C RID: 668
	public enum PathType
	{
		// Token: 0x04001072 RID: 4210
		SV,
		// Token: 0x04001073 RID: 4211
		DRILL,
		// Token: 0x04001074 RID: 4212
		LASER,
		// Token: 0x04001075 RID: 4213
		NUM_PATH
	}
}
