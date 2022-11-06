using System;
using UnityEngine;

// Token: 0x02000344 RID: 836
public class AgeVerificationYear : MonoBehaviour
{
	// Token: 0x170003C0 RID: 960
	// (get) Token: 0x060018D4 RID: 6356 RVA: 0x0008F580 File Offset: 0x0008D780
	public bool NoInput
	{
		get
		{
			foreach (AgeVerificationButton ageVerificationButton in this.m_buttons)
			{
				if (!(ageVerificationButton == null))
				{
					if (ageVerificationButton.NoInput)
					{
						return true;
					}
				}
			}
			return false;
		}
	}

	// Token: 0x060018D5 RID: 6357 RVA: 0x0008F5CC File Offset: 0x0008D7CC
	public void SetCallback(AgeVerificationButton.ButtonClickedCallback callback)
	{
		this.m_callback = callback;
	}

	// Token: 0x060018D6 RID: 6358 RVA: 0x0008F5D8 File Offset: 0x0008D7D8
	public void Setup()
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "year_set");
		if (gameObject == null)
		{
			return;
		}
		AgeVerificationButton.ButtonClickedCallback[] array = new AgeVerificationButton.ButtonClickedCallback[]
		{
			new AgeVerificationButton.ButtonClickedCallback(this.ThousandButtonClickedCallback),
			new AgeVerificationButton.ButtonClickedCallback(this.HundredButtonClickedCallback),
			new AgeVerificationButton.ButtonClickedCallback(this.TenButtonClickedCallback),
			new AgeVerificationButton.ButtonClickedCallback(this.OneButtonClickedCallback)
		};
		for (int i = 0; i < 4; i++)
		{
			string name = "Lbl_" + AgeVerificationYear.YearName[i];
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, name);
			if (!(gameObject2 == null))
			{
				AgeVerificationButton ageVerificationButton = gameObject2.AddComponent<AgeVerificationButton>();
				UILabel component = gameObject2.GetComponent<UILabel>();
				ageVerificationButton.SetLabel(AgeVerificationButton.LabelType.TYPE_ONE, component);
				string str = "Btn_" + AgeVerificationYear.YearName[i];
				GameObject upObject = GameObjectUtil.FindChildGameObject(gameObject, str + "_up");
				GameObject downObject = GameObjectUtil.FindChildGameObject(gameObject, str + "_down");
				ageVerificationButton.SetButton(upObject, downObject);
				ageVerificationButton.Setup(array[i]);
				this.m_buttons[i] = ageVerificationButton;
			}
		}
		this.m_buttons[0].AddValuePreset(1);
		this.m_buttons[0].AddValuePreset(2);
		this.m_buttons[0].SetDefaultValue(1);
		this.m_buttons[1].AddValuePreset(0);
		this.m_buttons[1].AddValuePreset(9);
		this.m_buttons[1].SetDefaultValue(0);
		for (int j = 0; j <= 9; j++)
		{
			this.m_buttons[2].AddValuePreset(j);
			this.m_buttons[3].AddValuePreset(j);
		}
		this.m_buttons[2].SetDefaultValue(0);
		this.m_buttons[3].SetDefaultValue(0);
	}

	// Token: 0x170003C1 RID: 961
	// (get) Token: 0x060018D7 RID: 6359 RVA: 0x0008F7A4 File Offset: 0x0008D9A4
	// (set) Token: 0x060018D8 RID: 6360 RVA: 0x0008F814 File Offset: 0x0008DA14
	public int CurrentValue
	{
		get
		{
			int num = 1;
			for (int i = 0; i < 4; i++)
			{
				if (this.m_buttons[i] == null)
				{
					return 1970;
				}
				num *= 10;
			}
			num /= 10;
			int num2 = 0;
			for (int j = 0; j < 4; j++)
			{
				num2 += this.m_buttons[j].CurrentValue * num;
				num /= 10;
			}
			return num2;
		}
		private set
		{
		}
	}

	// Token: 0x060018D9 RID: 6361 RVA: 0x0008F818 File Offset: 0x0008DA18
	private void Start()
	{
	}

	// Token: 0x060018DA RID: 6362 RVA: 0x0008F81C File Offset: 0x0008DA1C
	private void Update()
	{
	}

	// Token: 0x060018DB RID: 6363 RVA: 0x0008F820 File Offset: 0x0008DA20
	private void ThousandButtonClickedCallback()
	{
		if (this.m_callback != null)
		{
			this.m_callback();
		}
	}

	// Token: 0x060018DC RID: 6364 RVA: 0x0008F838 File Offset: 0x0008DA38
	private void HundredButtonClickedCallback()
	{
		if (this.m_callback != null)
		{
			this.m_callback();
		}
	}

	// Token: 0x060018DD RID: 6365 RVA: 0x0008F850 File Offset: 0x0008DA50
	private void TenButtonClickedCallback()
	{
		if (this.m_callback != null)
		{
			this.m_callback();
		}
	}

	// Token: 0x060018DE RID: 6366 RVA: 0x0008F868 File Offset: 0x0008DA68
	private void OneButtonClickedCallback()
	{
		if (this.m_callback != null)
		{
			this.m_callback();
		}
	}

	// Token: 0x04001638 RID: 5688
	private static readonly string[] YearName = new string[]
	{
		"year_0xxx",
		"year_x0xx",
		"year_xx0x",
		"year_xxx0"
	};

	// Token: 0x04001639 RID: 5689
	private AgeVerificationButton[] m_buttons = new AgeVerificationButton[4];

	// Token: 0x0400163A RID: 5690
	private AgeVerificationButton.ButtonClickedCallback m_callback;

	// Token: 0x02000345 RID: 837
	public enum ButtonType
	{
		// Token: 0x0400163C RID: 5692
		TYPE_NONE = -1,
		// Token: 0x0400163D RID: 5693
		TYPE_THOUSAND,
		// Token: 0x0400163E RID: 5694
		TYPE_HUNDRED,
		// Token: 0x0400163F RID: 5695
		TYPE_TEN,
		// Token: 0x04001640 RID: 5696
		TYPE_ONE,
		// Token: 0x04001641 RID: 5697
		TYPE_COUNT
	}
}
