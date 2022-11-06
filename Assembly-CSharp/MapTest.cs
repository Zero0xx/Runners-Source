using System;
using UnityEngine;

// Token: 0x02000A22 RID: 2594
public class MapTest : MonoBehaviour
{
	// Token: 0x060044B8 RID: 17592 RVA: 0x00161E8C File Offset: 0x0016008C
	private void Start()
	{
	}

	// Token: 0x060044B9 RID: 17593 RVA: 0x00161E90 File Offset: 0x00160090
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Application.LoadLevel("MapTest2");
		}
	}
}
