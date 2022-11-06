using System;
using UnityEngine;

namespace UI
{
	// Token: 0x0200056A RID: 1386
	[RequireComponent(typeof(UISprite))]
	public class UICursor : MonoBehaviour
	{
		// Token: 0x06002AA4 RID: 10916 RVA: 0x001081CC File Offset: 0x001063CC
		private void Awake()
		{
			UICursor.mInstance = this;
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x001081D4 File Offset: 0x001063D4
		private void OnDestroy()
		{
			UICursor.mInstance = null;
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x001081DC File Offset: 0x001063DC
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

		// Token: 0x06002AA7 RID: 10919 RVA: 0x00108258 File Offset: 0x00106458
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

		// Token: 0x06002AA8 RID: 10920 RVA: 0x0010839C File Offset: 0x0010659C
		public static void Clear()
		{
			UICursor.Set(UICursor.mInstance.mAtlas, UICursor.mInstance.mSpriteName);
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x001083B8 File Offset: 0x001065B8
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

		// Token: 0x04002620 RID: 9760
		private static UICursor mInstance;

		// Token: 0x04002621 RID: 9761
		public Camera uiCamera;

		// Token: 0x04002622 RID: 9762
		private Transform mTrans;

		// Token: 0x04002623 RID: 9763
		private UISprite mSprite;

		// Token: 0x04002624 RID: 9764
		private UIAtlas mAtlas;

		// Token: 0x04002625 RID: 9765
		private string mSpriteName;
	}
}
