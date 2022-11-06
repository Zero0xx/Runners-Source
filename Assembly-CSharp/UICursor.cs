using System;
using UnityEngine;

// Token: 0x0200004E RID: 78
[RequireComponent(typeof(UISprite))]
[AddComponentMenu("NGUI/Examples/UI Cursor")]
public class UICursor : MonoBehaviour
{
	// Token: 0x06000288 RID: 648 RVA: 0x0000AE10 File Offset: 0x00009010
	private void Awake()
	{
		UICursor.mInstance = this;
	}

	// Token: 0x06000289 RID: 649 RVA: 0x0000AE18 File Offset: 0x00009018
	private void OnDestroy()
	{
		UICursor.mInstance = null;
	}

	// Token: 0x0600028A RID: 650 RVA: 0x0000AE20 File Offset: 0x00009020
	private void Start()
	{
		this.mTrans = base.transform;
		this.mSprite = base.GetComponentInChildren<UISprite>();
		this.mAtlas = this.mSprite.atlas;
		this.mSpriteName = this.mSprite.spriteName;
		this.mSprite.depth = 100;
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
	}

	// Token: 0x0600028B RID: 651 RVA: 0x0000AE9C File Offset: 0x0000909C
	private void Update()
	{
		if (this.mSprite.atlas != null)
		{
			Vector3 mousePosition = Input.mousePosition;
			if (this.uiCamera != null)
			{
				mousePosition.x = Mathf.Clamp01(mousePosition.x / (float)Screen.width);
				mousePosition.y = Mathf.Clamp01(mousePosition.y / (float)Screen.height);
				this.mTrans.position = this.uiCamera.ViewportToWorldPoint(mousePosition);
				if (this.uiCamera.isOrthoGraphic)
				{
					Vector3 scale = new Vector3((float)this.mSprite.width, (float)this.mSprite.height, 1f);
					this.mTrans.localPosition = NGUIMath.ApplyHalfPixelOffset(this.mTrans.localPosition, scale);
				}
			}
			else
			{
				mousePosition.x -= (float)Screen.width * 0.5f;
				mousePosition.y -= (float)Screen.height * 0.5f;
				Vector3 scale2 = new Vector3((float)this.mSprite.width, (float)this.mSprite.height, 1f);
				this.mTrans.localPosition = NGUIMath.ApplyHalfPixelOffset(mousePosition, scale2);
			}
		}
	}

	// Token: 0x0600028C RID: 652 RVA: 0x0000AFE0 File Offset: 0x000091E0
	public static void Clear()
	{
		UICursor.Set(UICursor.mInstance.mAtlas, UICursor.mInstance.mSpriteName);
	}

	// Token: 0x0600028D RID: 653 RVA: 0x0000AFFC File Offset: 0x000091FC
	public static void Set(UIAtlas atlas, string sprite)
	{
		if (UICursor.mInstance != null)
		{
			UICursor.mInstance.mSprite.atlas = atlas;
			UICursor.mInstance.mSprite.spriteName = sprite;
			UICursor.mInstance.mSprite.MakePixelPerfect();
			UICursor.mInstance.Update();
		}
	}

	// Token: 0x0400011B RID: 283
	private static UICursor mInstance;

	// Token: 0x0400011C RID: 284
	public Camera uiCamera;

	// Token: 0x0400011D RID: 285
	private Transform mTrans;

	// Token: 0x0400011E RID: 286
	private UISprite mSprite;

	// Token: 0x0400011F RID: 287
	private UIAtlas mAtlas;

	// Token: 0x04000120 RID: 288
	private string mSpriteName;
}
