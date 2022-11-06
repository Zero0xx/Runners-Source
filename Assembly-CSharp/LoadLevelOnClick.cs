using System;
using UnityEngine;

// Token: 0x02000065 RID: 101
[AddComponentMenu("NGUI/Examples/Load Level On Click")]
public class LoadLevelOnClick : MonoBehaviour
{
	// Token: 0x060002DF RID: 735 RVA: 0x0000CB9C File Offset: 0x0000AD9C
	private void OnClick()
	{
		if (!string.IsNullOrEmpty(this.levelName))
		{
			Application.LoadLevel(this.levelName);
		}
	}

	// Token: 0x04000194 RID: 404
	public string levelName;
}
