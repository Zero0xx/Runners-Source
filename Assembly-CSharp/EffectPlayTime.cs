using System;
using UnityEngine;

// Token: 0x020002EE RID: 750
public class EffectPlayTime : MonoBehaviour
{
	// Token: 0x0600159A RID: 5530 RVA: 0x000779D8 File Offset: 0x00075BD8
	private void Update()
	{
		this.m_passedTime += Time.deltaTime;
		if (this.m_passedTime > this.m_endTime)
		{
			base.gameObject.SetActive(false);
			if (StageEffectManager.Instance != null)
			{
				StageEffectManager.Instance.SleepEffect(base.gameObject);
			}
		}
	}

	// Token: 0x0600159B RID: 5531 RVA: 0x00077A34 File Offset: 0x00075C34
	public void PlayEffect()
	{
		this.m_passedTime = 0f;
		if (!base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(true);
		}
		ParticleSystem[] componentsInChildren = base.GetComponentsInChildren<ParticleSystem>();
		foreach (ParticleSystem particleSystem in componentsInChildren)
		{
			particleSystem.time = 0f;
			particleSystem.Play();
		}
	}

	// Token: 0x0400131A RID: 4890
	private float m_passedTime;

	// Token: 0x0400131B RID: 4891
	public float m_endTime = 1f;
}
