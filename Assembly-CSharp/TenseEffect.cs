using System;
using UnityEngine;

// Token: 0x020002BF RID: 703
[ExecuteInEditMode]
public class TenseEffect : MonoBehaviour
{
	// Token: 0x06001416 RID: 5142 RVA: 0x0006C600 File Offset: 0x0006A800
	private void Start()
	{
		this.m_MaterialProperty = new MaterialPropertyBlock();
		this.m_TenseColorA = TenseEffectTable.GetItemData(this.m_tenseTypeA);
		this.m_TenseColorB = TenseEffectTable.GetItemData(this.m_tenseTypeB);
		if (TenseEffectManager.Instance != null)
		{
			this.m_tenseType = TenseEffectManager.Instance.GetTenseType();
		}
		Color color = (this.m_tenseType != TenseEffectManager.Type.TENSE_A) ? this.m_TenseColorB : this.m_TenseColorA;
		this.ModifyMaterialLightColor(color);
	}

	// Token: 0x06001417 RID: 5143 RVA: 0x0006C680 File Offset: 0x0006A880
	private void Update()
	{
	}

	// Token: 0x06001418 RID: 5144 RVA: 0x0006C684 File Offset: 0x0006A884
	private void ModifyMaterialLightColor(Color color)
	{
		if (this.m_MaterialProperty != null)
		{
			this.m_MaterialProperty.Clear();
			this.m_MaterialProperty.AddColor("_AmbientColor", color);
		}
		base.renderer.SetPropertyBlock(this.m_MaterialProperty);
	}

	// Token: 0x04001183 RID: 4483
	[SerializeField]
	private string m_tenseTypeA = "DEFAULT";

	// Token: 0x04001184 RID: 4484
	[SerializeField]
	private string m_tenseTypeB = "DEFAULT";

	// Token: 0x04001185 RID: 4485
	private Color m_TenseColorA = Color.white;

	// Token: 0x04001186 RID: 4486
	private Color m_TenseColorB = Color.white;

	// Token: 0x04001187 RID: 4487
	private MaterialPropertyBlock m_MaterialProperty;

	// Token: 0x04001188 RID: 4488
	private TenseEffectManager.Type m_tenseType;
}
