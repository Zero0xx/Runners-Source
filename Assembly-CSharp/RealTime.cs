using System;
using UnityEngine;

// Token: 0x020000AF RID: 175
public class RealTime : MonoBehaviour
{
	// Token: 0x170000AC RID: 172
	// (get) Token: 0x060004F1 RID: 1265 RVA: 0x000191A0 File Offset: 0x000173A0
	public static float time
	{
		get
		{
			if (RealTime.mInst == null)
			{
				RealTime.Spawn();
			}
			return RealTime.mInst.mRealTime;
		}
	}

	// Token: 0x170000AD RID: 173
	// (get) Token: 0x060004F2 RID: 1266 RVA: 0x000191C4 File Offset: 0x000173C4
	public static float deltaTime
	{
		get
		{
			if (RealTime.mInst == null)
			{
				RealTime.Spawn();
			}
			return RealTime.mInst.mRealDelta;
		}
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x000191E8 File Offset: 0x000173E8
	private static void Spawn()
	{
		GameObject gameObject = new GameObject("_RealTime");
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
		RealTime.mInst = gameObject.AddComponent<RealTime>();
		RealTime.mInst.mRealTime = Time.realtimeSinceStartup;
	}

	// Token: 0x060004F4 RID: 1268 RVA: 0x00019220 File Offset: 0x00017420
	private void Update()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		this.mRealDelta = realtimeSinceStartup - this.mRealTime;
		this.mRealTime = realtimeSinceStartup;
	}

	// Token: 0x0400035A RID: 858
	private static RealTime mInst;

	// Token: 0x0400035B RID: 859
	private float mRealTime;

	// Token: 0x0400035C RID: 860
	private float mRealDelta;
}
