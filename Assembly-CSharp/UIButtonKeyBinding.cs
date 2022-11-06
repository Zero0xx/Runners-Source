using System;
using UnityEngine;

// Token: 0x02000078 RID: 120
[AddComponentMenu("Game/UI/Button Key Binding")]
public class UIButtonKeyBinding : MonoBehaviour
{
	// Token: 0x0600032B RID: 811 RVA: 0x0000E2B4 File Offset: 0x0000C4B4
	private void Update()
	{
		if (!UICamera.inputHasFocus)
		{
			if (this.keyCode == KeyCode.None)
			{
				return;
			}
			if (Input.GetKeyDown(this.keyCode))
			{
				base.SendMessage("OnPress", true, SendMessageOptions.DontRequireReceiver);
			}
			if (Input.GetKeyUp(this.keyCode))
			{
				base.SendMessage("OnPress", false, SendMessageOptions.DontRequireReceiver);
				base.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Token: 0x040001D7 RID: 471
	public KeyCode keyCode;
}
