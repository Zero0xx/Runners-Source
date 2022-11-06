using System;
using UnityEngine;

// Token: 0x02000A5C RID: 2652
public class RatateCube : MonoBehaviour
{
	// Token: 0x06004785 RID: 18309 RVA: 0x00178768 File Offset: 0x00176968
	private void Update()
	{
		base.transform.Rotate(0f, 15f * Time.deltaTime, 0f);
	}
}
