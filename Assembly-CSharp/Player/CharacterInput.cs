using System;
using App;
using App.Utility;
using Message;
using UnityEngine;

namespace Player
{
	// Token: 0x02000969 RID: 2409
	public class CharacterInput : MonoBehaviour
	{
		// Token: 0x06003F02 RID: 16130 RVA: 0x00147840 File Offset: 0x00145A40
		private void Start()
		{
			this.m_status.Set(0, true);
			this.m_stick = Vector3.zero;
			if (this.m_levelInformation == null)
			{
				this.m_levelInformation = GameObjectUtil.FindGameObjectComponent<LevelInformation>("LevelInformation");
			}
		}

		// Token: 0x06003F03 RID: 16131 RVA: 0x00147888 File Offset: 0x00145A88
		private void Update()
		{
			if (App.Math.NearZero(Time.deltaTime, 1E-06f) || App.Math.NearZero(Time.timeScale, 1E-06f))
			{
				return;
			}
			if (this.m_levelInformation.RequestPause)
			{
				return;
			}
			if (this.m_levelInformation.RequestEqitpItem)
			{
				return;
			}
			if (this.m_levelInformation.RequestCharaChange)
			{
				return;
			}
			this.m_touchedStatus.Reset();
			bool flag = !this.m_status.Test(1);
			if (flag)
			{
				if (this.m_status.Test(0))
				{
					float axis = Input.GetAxis("Vertical");
					float axis2 = Input.GetAxis("Horizontal");
					Vector3 stick = Camera.main.transform.right * axis2 + Camera.main.transform.up * axis;
					this.m_stick = stick;
				}
				else
				{
					this.m_stick = Vector3.zero;
				}
				if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
				{
					this.m_touchedStatus.Set(0, true);
					this.m_touchedStatus.Set(1, true);
				}
				else if (Input.GetMouseButton(0) || (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary)))
				{
					this.m_touchedStatus.Set(1, true);
				}
				if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
				{
					this.m_touchedStatus.Set(2, true);
				}
			}
			if (this.m_history != null)
			{
				this.m_historyIndex += 1;
				this.m_history[(int)this.m_historyIndex].touchedStatus = this.m_touchedStatus;
				this.m_history[(int)this.m_historyIndex].time = Time.deltaTime;
			}
		}

		// Token: 0x06003F04 RID: 16132 RVA: 0x00147AA4 File Offset: 0x00145CA4
		public Vector3 GetStick()
		{
			return this.m_stick;
		}

		// Token: 0x06003F05 RID: 16133 RVA: 0x00147AAC File Offset: 0x00145CAC
		public bool IsTouched()
		{
			return this.m_touchedStatus.Test(0);
		}

		// Token: 0x06003F06 RID: 16134 RVA: 0x00147ABC File Offset: 0x00145CBC
		public bool IsHold()
		{
			return this.m_touchedStatus.Test(1);
		}

		// Token: 0x06003F07 RID: 16135 RVA: 0x00147ACC File Offset: 0x00145CCC
		public bool IsReleased()
		{
			return this.m_touchedStatus.Test(2);
		}

		// Token: 0x06003F08 RID: 16136 RVA: 0x00147ADC File Offset: 0x00145CDC
		public void CreateHistory()
		{
			this.m_history = new CharacterInput.History[256];
			this.m_historyIndex = 0;
		}

		// Token: 0x06003F09 RID: 16137 RVA: 0x00147AF8 File Offset: 0x00145CF8
		public bool IsTouchedLastSecond(float lastSecond)
		{
			float num = 0f;
			byte historyIndex = this.m_historyIndex;
			while (num < lastSecond)
			{
				if (this.m_history[(int)historyIndex].touchedStatus.Test(0))
				{
					return true;
				}
				if (this.m_history[(int)historyIndex].time <= 0f)
				{
					break;
				}
				num += this.m_history[(int)historyIndex].time;
			}
			return false;
		}

		// Token: 0x06003F0A RID: 16138 RVA: 0x00147B74 File Offset: 0x00145D74
		private void OnInputDisable(MsgDisableInput msg)
		{
			this.m_status.Set(1, msg.m_disable);
		}

		// Token: 0x040035F4 RID: 13812
		private Bitset32 m_status;

		// Token: 0x040035F5 RID: 13813
		private Bitset32 m_touchedStatus;

		// Token: 0x040035F6 RID: 13814
		private Vector3 m_stick;

		// Token: 0x040035F7 RID: 13815
		private byte m_historyIndex;

		// Token: 0x040035F8 RID: 13816
		private CharacterInput.History[] m_history;

		// Token: 0x040035F9 RID: 13817
		private LevelInformation m_levelInformation;

		// Token: 0x0200096A RID: 2410
		private enum TouchedStatus
		{
			// Token: 0x040035FB RID: 13819
			touched,
			// Token: 0x040035FC RID: 13820
			hold,
			// Token: 0x040035FD RID: 13821
			released
		}

		// Token: 0x0200096B RID: 2411
		private enum Status
		{
			// Token: 0x040035FF RID: 13823
			STATUS_STICKENABLED,
			// Token: 0x04003600 RID: 13824
			STATUS_DISABLE
		}

		// Token: 0x0200096C RID: 2412
		private struct History
		{
			// Token: 0x04003601 RID: 13825
			public Bitset32 touchedStatus;

			// Token: 0x04003602 RID: 13826
			public float time;
		}
	}
}
