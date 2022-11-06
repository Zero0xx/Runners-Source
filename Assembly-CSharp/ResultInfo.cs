using System;
using UnityEngine;

// Token: 0x020002E3 RID: 739
public class ResultInfo : MonoBehaviour
{
	// Token: 0x06001563 RID: 5475 RVA: 0x000766BC File Offset: 0x000748BC
	private void Start()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x06001564 RID: 5476 RVA: 0x000766CC File Offset: 0x000748CC
	private void Update()
	{
	}

	// Token: 0x06001565 RID: 5477 RVA: 0x000766D0 File Offset: 0x000748D0
	public void SetInfo(ResultData data)
	{
		this.m_resultData = data;
	}

	// Token: 0x06001566 RID: 5478 RVA: 0x000766DC File Offset: 0x000748DC
	public ResultData GetInfo()
	{
		return this.m_resultData;
	}

	// Token: 0x06001567 RID: 5479 RVA: 0x000766E4 File Offset: 0x000748E4
	public void ResetData()
	{
		this.m_resultData = new ResultData();
	}

	// Token: 0x06001568 RID: 5480 RVA: 0x000766F4 File Offset: 0x000748F4
	public static ResultInfo CreateResultInfo()
	{
		ResultInfo resultInfo = GameObjectUtil.FindGameObjectComponent<ResultInfo>("ResultInfo");
		if (resultInfo)
		{
			UnityEngine.Object.Destroy(resultInfo.gameObject);
		}
		GameObject gameObject = new GameObject("ResultInfo");
		ResultInfo resultInfo2 = gameObject.AddComponent<ResultInfo>();
		resultInfo2.ResetData();
		UnityEngine.Object.DontDestroyOnLoad(resultInfo2.gameObject);
		return resultInfo2;
	}

	// Token: 0x06001569 RID: 5481 RVA: 0x00076748 File Offset: 0x00074948
	public static void CreateOptionTutorialResultInfo()
	{
		ResultInfo resultInfo = GameObjectUtil.FindGameObjectComponent<ResultInfo>("ResultInfo");
		if (resultInfo)
		{
			UnityEngine.Object.Destroy(resultInfo.gameObject);
		}
		GameObject gameObject = new GameObject("ResultInfo");
		ResultInfo resultInfo2 = gameObject.AddComponent<ResultInfo>();
		resultInfo2.ResetData();
		resultInfo2.m_resultData.m_fromOptionTutorial = true;
		resultInfo2.m_resultData.m_validResult = false;
		UnityEngine.Object.DontDestroyOnLoad(resultInfo2.gameObject);
	}

	// Token: 0x040012D4 RID: 4820
	private ResultData m_resultData;
}
