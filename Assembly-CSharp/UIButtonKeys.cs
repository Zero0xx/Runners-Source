using System;
using UnityEngine;

// Token: 0x02000079 RID: 121
[RequireComponent(typeof(Collider))]
[AddComponentMenu("NGUI/Interaction/Button Keys")]
public class UIButtonKeys : MonoBehaviour
{
	// Token: 0x0600032D RID: 813 RVA: 0x0000E330 File Offset: 0x0000C530
	private void OnEnable()
	{
		if (this.startsSelected && UICamera.selectedObject == null)
		{
			if (!NGUITools.GetActive(UICamera.selectedObject))
			{
				UICamera.selectedObject = base.gameObject;
			}
			else
			{
				UICamera.Notify(base.gameObject, "OnHover", true);
			}
		}
	}

	// Token: 0x0600032E RID: 814 RVA: 0x0000E390 File Offset: 0x0000C590
	private void OnKey(KeyCode key)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject))
		{
			switch (key)
			{
			case KeyCode.UpArrow:
				if (this.selectOnUp != null)
				{
					UICamera.selectedObject = this.selectOnUp.gameObject;
				}
				break;
			case KeyCode.DownArrow:
				if (this.selectOnDown != null)
				{
					UICamera.selectedObject = this.selectOnDown.gameObject;
				}
				break;
			case KeyCode.RightArrow:
				if (this.selectOnRight != null)
				{
					UICamera.selectedObject = this.selectOnRight.gameObject;
				}
				break;
			case KeyCode.LeftArrow:
				if (this.selectOnLeft != null)
				{
					UICamera.selectedObject = this.selectOnLeft.gameObject;
				}
				break;
			default:
				if (key == KeyCode.Tab)
				{
					if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
					{
						if (this.selectOnLeft != null)
						{
							UICamera.selectedObject = this.selectOnLeft.gameObject;
						}
						else if (this.selectOnUp != null)
						{
							UICamera.selectedObject = this.selectOnUp.gameObject;
						}
						else if (this.selectOnDown != null)
						{
							UICamera.selectedObject = this.selectOnDown.gameObject;
						}
						else if (this.selectOnRight != null)
						{
							UICamera.selectedObject = this.selectOnRight.gameObject;
						}
					}
					else if (this.selectOnRight != null)
					{
						UICamera.selectedObject = this.selectOnRight.gameObject;
					}
					else if (this.selectOnDown != null)
					{
						UICamera.selectedObject = this.selectOnDown.gameObject;
					}
					else if (this.selectOnUp != null)
					{
						UICamera.selectedObject = this.selectOnUp.gameObject;
					}
					else if (this.selectOnLeft != null)
					{
						UICamera.selectedObject = this.selectOnLeft.gameObject;
					}
				}
				break;
			}
		}
	}

	// Token: 0x0600032F RID: 815 RVA: 0x0000E5CC File Offset: 0x0000C7CC
	private void OnClick()
	{
		if (base.enabled && this.selectOnClick != null)
		{
			UICamera.selectedObject = this.selectOnClick.gameObject;
		}
	}

	// Token: 0x040001D8 RID: 472
	public bool startsSelected;

	// Token: 0x040001D9 RID: 473
	public UIButtonKeys selectOnClick;

	// Token: 0x040001DA RID: 474
	public UIButtonKeys selectOnUp;

	// Token: 0x040001DB RID: 475
	public UIButtonKeys selectOnDown;

	// Token: 0x040001DC RID: 476
	public UIButtonKeys selectOnLeft;

	// Token: 0x040001DD RID: 477
	public UIButtonKeys selectOnRight;
}
