using System;
using UnityEngine;

// Token: 0x02000120 RID: 288
public class ScreenUtil
{
	// Token: 0x060008DC RID: 2268 RVA: 0x00031DF4 File Offset: 0x0002FFF4
	public static ScreenType GetScreenType()
	{
		float num = (float)Screen.width;
		float num2 = (float)Screen.height;
		float num3 = num / num2;
		Vector2[] array = new Vector2[]
		{
			new Vector2(16f, 9f),
			new Vector2(3f, 2f),
			new Vector2(4f, 3f)
		};
		for (int i = 0; i < 3; i++)
		{
			float num4 = array[i].x / array[i].y;
			if (num3 > num4 - 0.01f && num3 < num4 + 0.01f)
			{
				return (ScreenType)i;
			}
		}
		return ScreenType.Undefined;
	}
}
