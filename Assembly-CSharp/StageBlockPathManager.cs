using System;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x020002A4 RID: 676
public class StageBlockPathManager : MonoBehaviour
{
	// Token: 0x060012C5 RID: 4805 RVA: 0x0006816C File Offset: 0x0006636C
	private void Start()
	{
	}

	// Token: 0x060012C6 RID: 4806 RVA: 0x00068170 File Offset: 0x00066370
	private void OnDestroy()
	{
		if (this.m_blockPathController != null)
		{
			foreach (GameObject obj in this.m_blockPathController)
			{
				UnityEngine.Object.Destroy(obj);
			}
			this.m_blockPathController = null;
		}
	}

	// Token: 0x060012C7 RID: 4807 RVA: 0x000681E8 File Offset: 0x000663E8
	public void SetPathManager(PathManager manager)
	{
		this.m_pathManager = manager;
	}

	// Token: 0x060012C8 RID: 4808 RVA: 0x000681F4 File Offset: 0x000663F4
	public void Setup()
	{
		this.m_blockPathController = new List<GameObject>();
	}

	// Token: 0x060012C9 RID: 4809 RVA: 0x00068204 File Offset: 0x00066404
	public BlockPathController GetCurrentController()
	{
		foreach (GameObject gameObject in this.m_blockPathController)
		{
			BlockPathController component = gameObject.GetComponent<BlockPathController>();
			if (component && component.IsNowCurrent())
			{
				return component;
			}
		}
		return null;
	}

	// Token: 0x060012CA RID: 4810 RVA: 0x0006828C File Offset: 0x0006648C
	public PathComponent GetCurentSVPath(ref float? distance)
	{
		return this.GetCurentPath(BlockPathController.PathType.SV, ref distance);
	}

	// Token: 0x060012CB RID: 4811 RVA: 0x00068298 File Offset: 0x00066498
	public PathComponent GetCurentDrillPath(ref float? distance)
	{
		return this.GetCurentPath(BlockPathController.PathType.DRILL, ref distance);
	}

	// Token: 0x060012CC RID: 4812 RVA: 0x000682A4 File Offset: 0x000664A4
	public PathComponent GetCurentLaserPath(ref float? distance)
	{
		return this.GetCurentPath(BlockPathController.PathType.LASER, ref distance);
	}

	// Token: 0x060012CD RID: 4813 RVA: 0x000682B0 File Offset: 0x000664B0
	private void ActivateBlock(int block, int blockActivateID, Vector3 originPoint)
	{
		GameObject gameObject = new GameObject("BlockPathController");
		gameObject.transform.parent = base.transform;
		BlockPathController blockPathController = gameObject.AddComponent<BlockPathController>();
		if (this.m_pathManager != null)
		{
			string stageName = StageTypeUtil.GetStageName(this.m_pathManager.GetSVPathName());
			blockPathController.Initialize(stageName, block, blockActivateID, this.m_pathManager, originPoint);
		}
		if (this.m_blockPathController != null)
		{
			this.m_blockPathController.Add(gameObject);
		}
	}

	// Token: 0x060012CE RID: 4814 RVA: 0x0006832C File Offset: 0x0006652C
	private void DeactivateBlock(int activateId)
	{
		BlockPathController controllerByActivateID = this.GetControllerByActivateID(activateId);
		if (controllerByActivateID != null)
		{
			this.m_blockPathController.Remove(controllerByActivateID.gameObject);
			UnityEngine.Object.Destroy(controllerByActivateID.gameObject);
		}
	}

	// Token: 0x060012CF RID: 4815 RVA: 0x0006836C File Offset: 0x0006656C
	private BlockPathController GetControllerByActivateID(int activateID)
	{
		foreach (GameObject gameObject in this.m_blockPathController)
		{
			BlockPathController component = gameObject.GetComponent<BlockPathController>();
			if (component && component.ActivateID == activateID)
			{
				return component;
			}
		}
		return null;
	}

	// Token: 0x060012D0 RID: 4816 RVA: 0x000683F4 File Offset: 0x000665F4
	public PathComponent GetCurentPath(BlockPathController.PathType pathType, ref float? distance)
	{
		BlockPathController currentController = this.GetCurrentController();
		if (currentController == null)
		{
			return null;
		}
		PathEvaluator evaluator = currentController.GetEvaluator(pathType);
		if (evaluator != null && evaluator.IsValid())
		{
			if (distance != null)
			{
				distance = new float?(evaluator.Distance);
			}
			return evaluator.GetPathObject();
		}
		return null;
	}

	// Token: 0x060012D1 RID: 4817 RVA: 0x00068454 File Offset: 0x00066654
	public PathEvaluator GetCurentPathEvaluator(BlockPathController.PathType pathType)
	{
		BlockPathController currentController = this.GetCurrentController();
		if (currentController == null)
		{
			return null;
		}
		PathEvaluator evaluator = currentController.GetEvaluator(pathType);
		if (evaluator != null && evaluator.IsValid())
		{
			return evaluator;
		}
		return null;
	}

	// Token: 0x060012D2 RID: 4818 RVA: 0x00068494 File Offset: 0x00066694
	public void OnActivateBlock(MsgActivateBlock msg)
	{
		if (msg != null)
		{
			this.ActivateBlock(msg.m_blockNo, msg.m_activateID, msg.m_originPosition);
		}
	}

	// Token: 0x060012D3 RID: 4819 RVA: 0x000684B4 File Offset: 0x000666B4
	private void OnDeactivateBlock(MsgDeactivateBlock msg)
	{
		if (msg != null)
		{
			this.DeactivateBlock(msg.m_activateID);
		}
	}

	// Token: 0x060012D4 RID: 4820 RVA: 0x000684C8 File Offset: 0x000666C8
	private void OnDeactivateAllBlock(MsgDeactivateAllBlock msg)
	{
		foreach (GameObject obj in this.m_blockPathController)
		{
			UnityEngine.Object.Destroy(obj);
		}
		this.m_blockPathController.Clear();
	}

	// Token: 0x060012D5 RID: 4821 RVA: 0x00068538 File Offset: 0x00066738
	private void OnChangeCurerntBlock(MsgChangeCurrentBlock msg)
	{
		if (msg != null)
		{
			BlockPathController currentController = this.GetCurrentController();
			BlockPathController controllerByActivateID = this.GetControllerByActivateID(msg.m_activateID);
			if (currentController != null)
			{
				currentController.SetCurrent(false);
			}
			if (controllerByActivateID != null)
			{
				controllerByActivateID.SetCurrent(true);
			}
		}
	}

	// Token: 0x04001095 RID: 4245
	private string m_pathStageName;

	// Token: 0x04001096 RID: 4246
	private PathManager m_pathManager;

	// Token: 0x04001097 RID: 4247
	private List<GameObject> m_blockPathController;
}
