using System;
using UnityEngine;

// Token: 0x02000200 RID: 512
public class ykTextureSheetAnimation : MonoBehaviour
{
	// Token: 0x06000D9B RID: 3483 RVA: 0x0004FFE4 File Offset: 0x0004E1E4
	private void Start()
	{
		this._material = this.GetMaterial();
	}

	// Token: 0x06000D9C RID: 3484 RVA: 0x0004FFF4 File Offset: 0x0004E1F4
	private void Update()
	{
		this.tOffset += Time.deltaTime * this.speed;
		if (this.count <= 0)
		{
			this.nOffset = (int)Mathf.Repeat(this.tOffset, (float)this.x * (float)this.y) + this.first;
		}
		else
		{
			this.nOffset = (int)Mathf.Repeat(this.tOffset, (float)this.count) + this.first;
		}
		float num = Mathf.Repeat((float)this.nOffset, (float)this.x);
		float num2 = (float)(this.y - Mathf.FloorToInt((float)((this.nOffset + this.x) / this.x)));
		Vector2 mainTextureScale = new Vector2(1f / (float)this.x, 1f / (float)this.y);
		this._material.mainTextureScale = mainTextureScale;
		Vector2 mainTextureOffset = new Vector2(mainTextureScale.x * num, mainTextureScale.y * num2);
		this._material.mainTextureOffset = mainTextureOffset;
	}

	// Token: 0x06000D9D RID: 3485 RVA: 0x000500FC File Offset: 0x0004E2FC
	protected virtual Material GetMaterial()
	{
		return base.renderer.material;
	}

	// Token: 0x06000D9E RID: 3486 RVA: 0x0005010C File Offset: 0x0004E30C
	protected virtual bool IsValidChange()
	{
		return true;
	}

	// Token: 0x06000D9F RID: 3487 RVA: 0x00050110 File Offset: 0x0004E310
	public void SetSpeed(float in_speed)
	{
		if (this.IsValidChange())
		{
			this.speed = in_speed;
		}
	}

	// Token: 0x04000B7F RID: 2943
	[SerializeField]
	private int x = 1;

	// Token: 0x04000B80 RID: 2944
	[SerializeField]
	private int y = 1;

	// Token: 0x04000B81 RID: 2945
	[SerializeField]
	private int first;

	// Token: 0x04000B82 RID: 2946
	[SerializeField]
	private int count;

	// Token: 0x04000B83 RID: 2947
	[SerializeField]
	private float speed = 1f;

	// Token: 0x04000B84 RID: 2948
	private float tOffset;

	// Token: 0x04000B85 RID: 2949
	private int nOffset;

	// Token: 0x04000B86 RID: 2950
	private Material _material;
}
