using System;
using UnityEngine;

// Token: 0x02000513 RID: 1299
public class RouletteInformationUtility
{
	// Token: 0x06002758 RID: 10072 RVA: 0x000F3AAC File Offset: 0x000F1CAC
	public static void ShowNewsWindow(InformationWindow.Information informationParam)
	{
		if (string.IsNullOrEmpty(informationParam.imageId))
		{
			return;
		}
		GameObject cameraUIObject = HudMenuUtility.GetCameraUIObject();
		if (cameraUIObject != null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(cameraUIObject, "NewsWindow");
			if (gameObject != null)
			{
				SoundManager.SePlay("sys_menu_decide", "SE");
				InformationWindow informationWindow = gameObject.GetComponent<InformationWindow>();
				if (informationWindow == null)
				{
					informationWindow = gameObject.AddComponent<InformationWindow>();
				}
				if (informationWindow != null)
				{
					informationWindow.Create(informationParam, gameObject);
				}
			}
		}
	}
}
