using System;
using UnityEngine;

// Token: 0x02000069 RID: 105
[AddComponentMenu("NGUI/Examples/Set Color on Selection")]
[ExecuteInEditMode]
[RequireComponent(typeof(UIWidget))]
public class SetColorOnSelection : MonoBehaviour
{
	// Token: 0x060002EA RID: 746 RVA: 0x0000D008 File Offset: 0x0000B208
	private void OnSelectionChange(string val)
	{
		if (this.mWidget == null)
		{
			this.mWidget = base.GetComponent<UIWidget>();
		}
		switch (val)
		{
		case "White":
			this.mWidget.color = Color.white;
			break;
		case "Red":
			this.mWidget.color = Color.red;
			break;
		case "Green":
			this.mWidget.color = Color.green;
			break;
		case "Blue":
			this.mWidget.color = Color.blue;
			break;
		case "Yellow":
			this.mWidget.color = Color.yellow;
			break;
		case "Cyan":
			this.mWidget.color = Color.cyan;
			break;
		case "Magenta":
			this.mWidget.color = Color.magenta;
			break;
		}
	}

	// Token: 0x040001A3 RID: 419
	private UIWidget mWidget;
}
