using System;
using UnityEngine;

// Token: 0x0200006A RID: 106
[AddComponentMenu("NGUI/Examples/Shader Quality")]
[ExecuteInEditMode]
public class ShaderQuality : MonoBehaviour
{
	// Token: 0x060002EC RID: 748 RVA: 0x0000D188 File Offset: 0x0000B388
	private void Update()
	{
		int num = (QualitySettings.GetQualityLevel() + 1) * 100;
		if (this.mCurrent != num)
		{
			this.mCurrent = num;
			Shader.globalMaximumLOD = this.mCurrent;
		}
	}

	// Token: 0x040001A5 RID: 421
	private int mCurrent = 600;
}
