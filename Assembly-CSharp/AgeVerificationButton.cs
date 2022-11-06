using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000340 RID: 832
public class AgeVerificationButton : MonoBehaviour
{
	// Token: 0x170003BB RID: 955
	// (get) Token: 0x060018AE RID: 6318 RVA: 0x0008EA94 File Offset: 0x0008CC94
	// (set) Token: 0x060018AF RID: 6319 RVA: 0x0008EAB4 File Offset: 0x0008CCB4
	public int CurrentValue
	{
		get
		{
			if (this.m_valuePreset != null)
			{
				return this.m_valuePreset[this.m_currentIndex];
			}
			return -1;
		}
		private set
		{
		}
	}

	// Token: 0x170003BC RID: 956
	// (get) Token: 0x060018B0 RID: 6320 RVA: 0x0008EAB8 File Offset: 0x0008CCB8
	// (set) Token: 0x060018B1 RID: 6321 RVA: 0x0008EAC0 File Offset: 0x0008CCC0
	public bool NoInput
	{
		get
		{
			return this.m_noInput;
		}
		private set
		{
		}
	}

	// Token: 0x060018B2 RID: 6322 RVA: 0x0008EAC4 File Offset: 0x0008CCC4
	public void SetLabel(AgeVerificationButton.LabelType labelType, UILabel label)
	{
		this.m_labelType = labelType;
		this.m_label = label;
	}

	// Token: 0x060018B3 RID: 6323 RVA: 0x0008EAD4 File Offset: 0x0008CCD4
	public void SetButton(GameObject upObject, GameObject downObject)
	{
		if (upObject == null)
		{
			return;
		}
		if (downObject == null)
		{
			return;
		}
		this.m_upObject = upObject;
		this.m_downObject = downObject;
		UIButtonMessage uibuttonMessage = this.m_upObject.GetComponent<UIButtonMessage>();
		if (uibuttonMessage == null)
		{
			uibuttonMessage = this.m_upObject.AddComponent<UIButtonMessage>();
		}
		uibuttonMessage.target = base.gameObject;
		uibuttonMessage.functionName = "ButtonMessageUpClicked";
		UIButtonMessage uibuttonMessage2 = this.m_downObject.GetComponent<UIButtonMessage>();
		if (uibuttonMessage2 == null)
		{
			uibuttonMessage2 = this.m_downObject.AddComponent<UIButtonMessage>();
		}
		uibuttonMessage2.target = base.gameObject;
		uibuttonMessage2.functionName = "ButtonMessageDownClicked";
	}

	// Token: 0x060018B4 RID: 6324 RVA: 0x0008EB80 File Offset: 0x0008CD80
	public void Setup(AgeVerificationButton.ButtonClickedCallback callback)
	{
		this.m_callback = callback;
	}

	// Token: 0x060018B5 RID: 6325 RVA: 0x0008EB8C File Offset: 0x0008CD8C
	public void AddValuePreset(int value)
	{
		this.m_valuePreset.Add(value);
	}

	// Token: 0x060018B6 RID: 6326 RVA: 0x0008EB9C File Offset: 0x0008CD9C
	public void SetDefaultValue(int value)
	{
		for (int i = 0; i < this.m_valuePreset.Count; i++)
		{
			if (value == this.m_valuePreset[i])
			{
				this.m_currentIndex = i;
				this.SetValue(this.m_valuePreset[i]);
			}
		}
	}

	// Token: 0x060018B7 RID: 6327 RVA: 0x0008EBF0 File Offset: 0x0008CDF0
	private void SetValue(int value)
	{
		string str = string.Empty;
		string str2 = string.Empty;
		if (this.m_noInput)
		{
			str = string.Empty;
			str2 = "-";
		}
		else
		{
			if (this.m_labelType == AgeVerificationButton.LabelType.TYPE_TEN)
			{
				str = (value / 10).ToString();
			}
			str2 = (value % 10).ToString();
		}
		if (this.m_label != null)
		{
			string text = str + str2;
			this.m_label.text = text;
		}
	}

	// Token: 0x060018B8 RID: 6328 RVA: 0x0008EC74 File Offset: 0x0008CE74
	private void Start()
	{
	}

	// Token: 0x060018B9 RID: 6329 RVA: 0x0008EC78 File Offset: 0x0008CE78
	private void Update()
	{
	}

	// Token: 0x060018BA RID: 6330 RVA: 0x0008EC7C File Offset: 0x0008CE7C
	private void ButtonMessageUpClicked()
	{
		if (this.m_noInput)
		{
			this.m_noInput = false;
		}
		else
		{
			this.m_currentIndex++;
			if (this.m_currentIndex >= this.m_valuePreset.Count)
			{
				this.m_currentIndex = 0;
			}
		}
		int value = this.m_valuePreset[this.m_currentIndex];
		this.SetValue(value);
		this.m_callback();
	}

	// Token: 0x060018BB RID: 6331 RVA: 0x0008ECF4 File Offset: 0x0008CEF4
	private void ButtonMessageDownClicked()
	{
		if (this.m_noInput)
		{
			this.m_noInput = false;
		}
		else
		{
			this.m_currentIndex--;
			if (this.m_currentIndex < 0)
			{
				this.m_currentIndex = this.m_valuePreset.Count - 1;
			}
		}
		int value = this.m_valuePreset[this.m_currentIndex];
		this.SetValue(value);
		this.m_callback();
	}

	// Token: 0x04001624 RID: 5668
	private AgeVerificationButton.ButtonClickedCallback m_callback;

	// Token: 0x04001625 RID: 5669
	private AgeVerificationButton.LabelType m_labelType;

	// Token: 0x04001626 RID: 5670
	private UILabel m_label;

	// Token: 0x04001627 RID: 5671
	private GameObject m_upObject;

	// Token: 0x04001628 RID: 5672
	private GameObject m_downObject;

	// Token: 0x04001629 RID: 5673
	private int m_currentIndex;

	// Token: 0x0400162A RID: 5674
	private List<int> m_valuePreset = new List<int>();

	// Token: 0x0400162B RID: 5675
	private bool m_noInput = true;

	// Token: 0x02000341 RID: 833
	public enum LabelType
	{
		// Token: 0x0400162D RID: 5677
		TYPE_NONE = -1,
		// Token: 0x0400162E RID: 5678
		TYPE_ONE,
		// Token: 0x0400162F RID: 5679
		TYPE_TEN,
		// Token: 0x04001630 RID: 5680
		TYPE_COUNT
	}

	// Token: 0x02000A7F RID: 2687
	// (Invoke) Token: 0x06004836 RID: 18486
	public delegate void ButtonClickedCallback();
}
