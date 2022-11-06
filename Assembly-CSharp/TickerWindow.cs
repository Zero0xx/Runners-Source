using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000564 RID: 1380
public class TickerWindow : MonoBehaviour
{
	// Token: 0x06002A85 RID: 10885 RVA: 0x00107804 File Offset: 0x00105A04
	private void Update()
	{
		if (this.m_uiLabel != null)
		{
			switch (this.m_mode)
			{
			case TickerWindow.Mode.Start:
				this.m_timer = 1f;
				this.m_mode = TickerWindow.Mode.Wait1;
				break;
			case TickerWindow.Mode.Wait1:
				this.m_timer -= Time.deltaTime;
				if (this.m_timer <= 0f)
				{
					this.m_mode = TickerWindow.Mode.SpeedUpMove;
				}
				break;
			case TickerWindow.Mode.SpeedUpMove:
			{
				this.UpdateMovePos(this.m_info.moveSpeedUp * Time.deltaTime * 60f);
				float x = this.m_uiLabel.transform.localPosition.x;
				float num = 24f;
				if (x < num)
				{
					this.SetResetPos(num);
					this.m_timer = 1f;
					this.m_mode = TickerWindow.Mode.Wait2;
				}
				break;
			}
			case TickerWindow.Mode.Wait2:
				this.m_timer -= Time.deltaTime;
				if (this.m_timer <= 0f)
				{
					this.m_textSize = (float)this.m_uiLabel.width;
					this.m_mode = TickerWindow.Mode.Move;
				}
				break;
			case TickerWindow.Mode.Move:
			{
				this.UpdateMovePos(this.m_info.moveSpeed * Time.deltaTime * 60f);
				float x2 = this.m_uiLabel.transform.localPosition.x;
				float num2 = 0f - this.m_textSize;
				if (x2 < num2)
				{
					this.SetupNext();
					this.m_mode = TickerWindow.Mode.Start;
				}
				break;
			}
			}
		}
	}

	// Token: 0x06002A86 RID: 10886 RVA: 0x00107990 File Offset: 0x00105B90
	public void Setup(TickerWindow.CInfo info)
	{
		this.m_info = info;
		this.m_count = 0;
		UIPanel component = base.gameObject.GetComponent<UIPanel>();
		if (component != null)
		{
			this.m_uiLabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, this.m_info.labelName);
			if (this.m_uiLabel != null && this.m_info.tickerList != null && this.m_info.tickerList.Count > 0)
			{
				this.m_uiLabel.text = this.m_info.tickerList[this.m_count].Param;
				this.m_startPos = component.clipRange.z;
				this.SetResetPos(this.m_startPos);
				this.m_mode = TickerWindow.Mode.Start;
			}
		}
	}

	// Token: 0x06002A87 RID: 10887 RVA: 0x00107A64 File Offset: 0x00105C64
	private void SetupNext()
	{
		if (this.m_info.tickerList != null && this.m_uiLabel != null)
		{
			this.m_count++;
			if (this.m_count >= this.m_info.tickerList.Count)
			{
				this.m_count = 0;
			}
			this.m_uiLabel.text = this.m_info.tickerList[this.m_count].Param;
		}
		this.SetResetPos(this.m_startPos);
	}

	// Token: 0x06002A88 RID: 10888 RVA: 0x00107AF4 File Offset: 0x00105CF4
	public void ResetWindow()
	{
		this.SetResetPos(this.m_startPos);
		this.m_mode = TickerWindow.Mode.Idle;
	}

	// Token: 0x06002A89 RID: 10889 RVA: 0x00107B0C File Offset: 0x00105D0C
	private void SetResetPos(float startPos)
	{
		if (this.m_uiLabel != null)
		{
			Vector3 localPosition = this.m_uiLabel.transform.localPosition;
			this.m_uiLabel.transform.localPosition = new Vector3(startPos, localPosition.y, localPosition.z);
		}
	}

	// Token: 0x06002A8A RID: 10890 RVA: 0x00107B60 File Offset: 0x00105D60
	private void UpdateMovePos(float move)
	{
		if (this.m_uiLabel != null)
		{
			this.m_uiLabel.transform.localPosition -= Vector3.right * move;
		}
	}

	// Token: 0x040025F1 RID: 9713
	private TickerWindow.Mode m_mode;

	// Token: 0x040025F2 RID: 9714
	private TickerWindow.CInfo m_info = default(TickerWindow.CInfo);

	// Token: 0x040025F3 RID: 9715
	private UILabel m_uiLabel;

	// Token: 0x040025F4 RID: 9716
	private float m_textSize;

	// Token: 0x040025F5 RID: 9717
	private float m_startPos;

	// Token: 0x040025F6 RID: 9718
	private int m_count;

	// Token: 0x040025F7 RID: 9719
	private float m_timer;

	// Token: 0x02000565 RID: 1381
	public struct CInfo
	{
		// Token: 0x040025F8 RID: 9720
		public List<ServerTickerData> tickerList;

		// Token: 0x040025F9 RID: 9721
		public string labelName;

		// Token: 0x040025FA RID: 9722
		public float moveSpeed;

		// Token: 0x040025FB RID: 9723
		public float moveSpeedUp;
	}

	// Token: 0x02000566 RID: 1382
	private enum Mode
	{
		// Token: 0x040025FD RID: 9725
		Idle,
		// Token: 0x040025FE RID: 9726
		Start,
		// Token: 0x040025FF RID: 9727
		Wait1,
		// Token: 0x04002600 RID: 9728
		SpeedUpMove,
		// Token: 0x04002601 RID: 9729
		Wait2,
		// Token: 0x04002602 RID: 9730
		Move
	}
}
