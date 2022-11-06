using System;
using UnityEngine;

// Token: 0x02000162 RID: 354
public class ChaoPartsObjectMagnet : MonoBehaviour
{
	// Token: 0x06000A4C RID: 2636 RVA: 0x0003D8D8 File Offset: 0x0003BAD8
	private void Start()
	{
	}

	// Token: 0x06000A4D RID: 2637 RVA: 0x0003D8DC File Offset: 0x0003BADC
	private void Update()
	{
		if (this.m_pauseFlag)
		{
			return;
		}
		ChaoPartsObjectMagnet.Mode mode = this.m_mode;
		if (mode != ChaoPartsObjectMagnet.Mode.Magnet)
		{
			if (mode == ChaoPartsObjectMagnet.Mode.Respite)
			{
				this.m_time -= Time.deltaTime;
				if (this.m_time < 0f)
				{
					this.SetDisable();
					this.m_mode = ChaoPartsObjectMagnet.Mode.Idle;
				}
			}
		}
		else
		{
			this.m_time -= Time.deltaTime;
			if (this.m_time < 0f)
			{
				this.SetRespite();
				this.m_time = 1f;
				this.m_mode = ChaoPartsObjectMagnet.Mode.Respite;
			}
		}
	}

	// Token: 0x06000A4E RID: 2638 RVA: 0x0003D984 File Offset: 0x0003BB84
	public void Setup()
	{
		string layerName = "HitRing";
		ChaoPartsObjectMagnet.HitType hitType = this.m_hitType;
		if (hitType != ChaoPartsObjectMagnet.HitType.RING)
		{
			if (hitType == ChaoPartsObjectMagnet.HitType.CRYSTAL)
			{
				layerName = "HitCrystal";
			}
		}
		base.gameObject.layer = LayerMask.NameToLayer(layerName);
		this.m_animator = base.gameObject.GetComponent<Animator>();
		this.m_collider = base.gameObject.AddComponent<SphereCollider>();
		if (this.m_collider != null)
		{
			this.m_collider.radius = this.m_colliRadius;
			this.m_collider.isTrigger = true;
			this.m_collider.enabled = false;
		}
		this.m_magnetObj = new GameObject();
		if (this.m_magnetObj != null)
		{
			this.m_magnetObj.name = "magnet";
			this.m_magnetObj.transform.parent = base.gameObject.transform;
			this.m_magnetObj.layer = LayerMask.NameToLayer(layerName);
			this.m_magnet = this.m_magnetObj.AddComponent<ChaoMagnet>();
			if (this.m_magnet != null)
			{
				this.m_magnet.Setup(this.m_magnetRadius, this.m_hitLayer);
			}
		}
		base.enabled = false;
	}

	// Token: 0x06000A4F RID: 2639 RVA: 0x0003DAC8 File Offset: 0x0003BCC8
	public void SetEnable(float time)
	{
		this.m_time = time;
		if (this.m_effect != null)
		{
			UnityEngine.Object.Destroy(this.m_effect);
			this.m_effect = null;
		}
		if (!string.IsNullOrEmpty(this.m_effectName))
		{
			this.m_effect = ObjUtil.PlayChaoEffect(base.gameObject, this.m_effectName, -1f);
		}
		if (this.m_collider != null)
		{
			this.m_collider.enabled = true;
		}
		if (this.m_magnet != null)
		{
			this.m_magnet.SetEnable(true);
		}
		SoundManager.SePlay("obj_magnet", "SE");
		this.m_mode = ChaoPartsObjectMagnet.Mode.Magnet;
		base.enabled = true;
		if (this.m_pauseFlag)
		{
			this.SetPause(this.m_pauseFlag);
		}
		else
		{
			this.SetAnimation(true);
		}
	}

	// Token: 0x06000A50 RID: 2640 RVA: 0x0003DBA8 File Offset: 0x0003BDA8
	public void SetPause(bool flag)
	{
		this.m_pauseFlag = flag;
		ChaoPartsObjectMagnet.Mode mode = this.m_mode;
		if (mode != ChaoPartsObjectMagnet.Mode.Magnet)
		{
			if (mode != ChaoPartsObjectMagnet.Mode.Respite)
			{
			}
		}
		else
		{
			this.m_effect.SetActive(!this.m_pauseFlag);
			if (this.m_magnet != null)
			{
				this.m_magnet.SetEnable(!this.m_pauseFlag);
			}
			this.SetAnimation(!this.m_pauseFlag);
			if (!this.m_pauseFlag)
			{
				SoundManager.SePlay("obj_magnet", "SE");
			}
		}
	}

	// Token: 0x06000A51 RID: 2641 RVA: 0x0003DC44 File Offset: 0x0003BE44
	private void SetRespite()
	{
		if (this.m_effect != null)
		{
			UnityEngine.Object.Destroy(this.m_effect);
			this.m_effect = null;
		}
		if (this.m_magnet != null)
		{
			this.m_magnet.SetEnable(false);
		}
		this.SetAnimation(false);
	}

	// Token: 0x06000A52 RID: 2642 RVA: 0x0003DC98 File Offset: 0x0003BE98
	private void SetDisable()
	{
		if (this.m_collider != null)
		{
			this.m_collider.enabled = false;
		}
		this.SetAnimation(false);
		base.enabled = false;
	}

	// Token: 0x06000A53 RID: 2643 RVA: 0x0003DCC8 File Offset: 0x0003BEC8
	private void SetAnimation(bool flag)
	{
		if (this.m_animator != null)
		{
			this.m_animator.SetBool("Ability", flag);
		}
	}

	// Token: 0x040007F0 RID: 2032
	public float m_colliRadius = 2.5f;

	// Token: 0x040007F1 RID: 2033
	public float m_magnetRadius = 4f;

	// Token: 0x040007F2 RID: 2034
	public string m_effectName = string.Empty;

	// Token: 0x040007F3 RID: 2035
	public string m_hitLayer = string.Empty;

	// Token: 0x040007F4 RID: 2036
	public ChaoPartsObjectMagnet.HitType m_hitType;

	// Token: 0x040007F5 RID: 2037
	private SphereCollider m_collider;

	// Token: 0x040007F6 RID: 2038
	private GameObject m_magnetObj;

	// Token: 0x040007F7 RID: 2039
	private ChaoMagnet m_magnet;

	// Token: 0x040007F8 RID: 2040
	private GameObject m_effect;

	// Token: 0x040007F9 RID: 2041
	private Animator m_animator;

	// Token: 0x040007FA RID: 2042
	private ChaoPartsObjectMagnet.Mode m_mode;

	// Token: 0x040007FB RID: 2043
	private float m_time;

	// Token: 0x040007FC RID: 2044
	private bool m_pauseFlag;

	// Token: 0x02000163 RID: 355
	public enum HitType
	{
		// Token: 0x040007FE RID: 2046
		RING,
		// Token: 0x040007FF RID: 2047
		CRYSTAL
	}

	// Token: 0x02000164 RID: 356
	private enum Mode
	{
		// Token: 0x04000801 RID: 2049
		Idle,
		// Token: 0x04000802 RID: 2050
		Magnet,
		// Token: 0x04000803 RID: 2051
		Respite
	}
}
