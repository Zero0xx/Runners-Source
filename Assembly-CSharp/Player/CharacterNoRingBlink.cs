using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	// Token: 0x0200098E RID: 2446
	public class CharacterNoRingBlink : MonoBehaviour
	{
		// Token: 0x06004049 RID: 16457 RVA: 0x0014D748 File Offset: 0x0014B948
		private void Awake()
		{
		}

		// Token: 0x0600404A RID: 16458 RVA: 0x0014D74C File Offset: 0x0014B94C
		private void Start()
		{
		}

		// Token: 0x0600404B RID: 16459 RVA: 0x0014D750 File Offset: 0x0014B950
		public void SetEnable()
		{
			this.m_nowColor = this.RedColor;
			this.UpdateColor(this.m_nowColor);
			base.enabled = true;
		}

		// Token: 0x0600404C RID: 16460 RVA: 0x0014D774 File Offset: 0x0014B974
		public void SetDisable()
		{
			this.UpdateColor(this.m_defaultColor);
			base.enabled = false;
		}

		// Token: 0x0600404D RID: 16461 RVA: 0x0014D78C File Offset: 0x0014B98C
		private void Update()
		{
			float num = 0.25f;
			this.m_timer += Time.deltaTime;
			if (this.m_timer < num)
			{
				this.m_nowColor = Color.Lerp(this.RedColor, this.m_defaultColor, Mathf.Clamp(this.m_timer / num, 0f, 1f));
			}
			else
			{
				this.m_nowColor = Color.Lerp(this.m_defaultColor, this.RedColor, Mathf.Clamp((this.m_timer - num) / num, 0f, 1f));
			}
			if (this.m_timer >= 0.5f)
			{
				this.m_timer = Mathf.Max(this.m_timer - 0.5f, 0f);
			}
			this.UpdateColor(this.m_nowColor);
		}

		// Token: 0x0600404E RID: 16462 RVA: 0x0014D858 File Offset: 0x0014BA58
		public void Setup(GameObject model)
		{
			foreach (object obj in model.transform)
			{
				Transform transform = (Transform)obj;
				Renderer component = transform.GetComponent<Renderer>();
				if (component != null)
				{
					Material[] materials = component.materials;
					foreach (Material material in materials)
					{
						if (material.HasProperty("_OutlineColor"))
						{
							this.m_materialList.Add(material);
						}
					}
				}
			}
			if (this.m_materialList.Count > 0)
			{
				this.m_defaultColor = this.m_materialList[0].GetColor("_OutlineColor");
			}
		}

		// Token: 0x0600404F RID: 16463 RVA: 0x0014D94C File Offset: 0x0014BB4C
		public void UpdateColor(Color color)
		{
			foreach (Material material in this.m_materialList)
			{
				material.SetColor("_OutlineColor", color);
			}
		}

		// Token: 0x04003708 RID: 14088
		private const string ShaderName = "ykChrLine_dme1";

		// Token: 0x04003709 RID: 14089
		private const string ChangeParamName = "_OutlineColor";

		// Token: 0x0400370A RID: 14090
		private const float BlinkTime = 0.5f;

		// Token: 0x0400370B RID: 14091
		private readonly Color RedColor = Color.red;

		// Token: 0x0400370C RID: 14092
		private Color m_defaultColor;

		// Token: 0x0400370D RID: 14093
		private Color m_nowColor;

		// Token: 0x0400370E RID: 14094
		private List<Material> m_materialList = new List<Material>();

		// Token: 0x0400370F RID: 14095
		private float m_timer;
	}
}
