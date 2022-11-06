using System;
using System.Collections.Generic;
using Text;
using UnityEngine;

// Token: 0x0200039F RID: 927
public class GlowUpExpBar : MonoBehaviour
{
	// Token: 0x06001B3D RID: 6973 RVA: 0x000A12C4 File Offset: 0x0009F4C4
	public void SetBaseSlider(UISlider slider)
	{
		if (slider == null)
		{
			return;
		}
		this.m_baseSlider = slider;
		this.m_baseSlider.value = 0f;
	}

	// Token: 0x06001B3E RID: 6974 RVA: 0x000A12F8 File Offset: 0x0009F4F8
	public void SetGlowUpSlider(UISlider slider)
	{
		if (slider == null)
		{
			return;
		}
		this.m_glowUpSlider = slider;
		this.m_glowUpSlider.value = 0f;
	}

	// Token: 0x06001B3F RID: 6975 RVA: 0x000A132C File Offset: 0x0009F52C
	public void SetStartExp(GlowUpExpBar.ExpInfo startInfo)
	{
		if (startInfo == null)
		{
			return;
		}
		this.m_startInfo = startInfo;
		float num = GlowUpExpBar.CalcSliderValue(startInfo);
		if (this.m_baseSlider != null)
		{
			this.m_baseSlider.value = num;
		}
		if (this.m_glowUpSlider != null)
		{
			this.m_glowUpSlider.value = num;
		}
		int cost = this.m_startInfo.cost;
		int exp = (int)((float)cost * num);
		string text = GlowUpExpBar.CalcExpString(exp, cost);
		if (!this.m_expLabel.gameObject.activeSelf)
		{
			this.m_expLabel.gameObject.SetActive(true);
		}
		this.m_expLabel.text = text;
	}

	// Token: 0x06001B40 RID: 6976 RVA: 0x000A13D4 File Offset: 0x0009F5D4
	public void SetExpLabel(UILabel expLabel)
	{
		this.m_expLabel = expLabel;
	}

	// Token: 0x06001B41 RID: 6977 RVA: 0x000A13E0 File Offset: 0x0009F5E0
	public void SetEndExp(GlowUpExpBar.ExpInfo endInfo)
	{
		if (endInfo == null)
		{
			return;
		}
		this.m_endInfo = endInfo;
	}

	// Token: 0x06001B42 RID: 6978 RVA: 0x000A13F0 File Offset: 0x0009F5F0
	public void SetCallback(GlowUpExpBar.LevelUpCallback levelUpCallback, GlowUpExpBar.EndCallback endCallback)
	{
		this.m_levelUpCallback = levelUpCallback;
		this.m_endCallback = endCallback;
	}

	// Token: 0x06001B43 RID: 6979 RVA: 0x000A1400 File Offset: 0x0009F600
	public void SetLevelUpCostList(List<int> expList)
	{
		if (expList == null)
		{
			return;
		}
		this.m_costList = new List<int>();
		foreach (int item in expList)
		{
			this.m_costList.Add(item);
		}
	}

	// Token: 0x06001B44 RID: 6980 RVA: 0x000A1478 File Offset: 0x0009F678
	public void PlayStart()
	{
		int startValue = (int)(((float)this.m_startInfo.level + GlowUpExpBar.CalcSliderValue(this.m_startInfo)) * (float)GlowUpExpBar.RATIO_TO_VALUE);
		int endValue = (int)(((float)this.m_endInfo.level + GlowUpExpBar.CalcSliderValue(this.m_endInfo)) * (float)GlowUpExpBar.RATIO_TO_VALUE);
		this.m_interpolate.Setup(startValue, endValue, GlowUpExpBar.BAR_SPEED_PER_SEC);
		if (this.m_baseSlider != null)
		{
			this.m_baseSlider.value = GlowUpExpBar.CalcSliderValue(this.m_startInfo);
		}
		if (this.m_glowUpSlider != null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "eff_thumb");
			if (gameObject != null)
			{
				gameObject.SetActive(true);
			}
		}
		this.m_isPlaying = true;
	}

	// Token: 0x06001B45 RID: 6981 RVA: 0x000A153C File Offset: 0x0009F73C
	public void PlaySkip()
	{
		if (this.m_interpolate == null)
		{
			return;
		}
		int currentValue = this.m_interpolate.CurrentValue;
		int num = currentValue / GlowUpExpBar.RATIO_TO_VALUE;
		int num2 = num + 1;
		int forceValue = num2 * GlowUpExpBar.RATIO_TO_VALUE - 1;
		this.m_interpolate.SetForceValue(forceValue);
	}

	// Token: 0x06001B46 RID: 6982 RVA: 0x000A1584 File Offset: 0x0009F784
	private void Start()
	{
	}

	// Token: 0x06001B47 RID: 6983 RVA: 0x000A1588 File Offset: 0x0009F788
	private void Update()
	{
		if (!this.m_isPlaying)
		{
			return;
		}
		int num = this.m_interpolate.Update(Time.deltaTime);
		int prevValue = this.m_interpolate.PrevValue;
		float num2 = 0f;
		if (this.m_glowUpSlider != null)
		{
			float num3 = (float)num / (float)GlowUpExpBar.RATIO_TO_VALUE;
			int num4 = num / GlowUpExpBar.RATIO_TO_VALUE;
			float num5 = num3 - (float)num4;
			this.m_glowUpSlider.value = num5;
			num2 = num5;
		}
		int num6 = num / GlowUpExpBar.RATIO_TO_VALUE;
		int num7 = prevValue / GlowUpExpBar.RATIO_TO_VALUE;
		if (num6 > num7)
		{
			if (this.m_levelUpCallback != null)
			{
				this.m_levelUpCallback(num6);
			}
			if (this.m_baseSlider != null)
			{
				this.m_baseSlider.value = 0f;
			}
			if (this.m_costList != null && this.m_costList.Count > 0 && num7 != this.m_startInfo.level)
			{
				int item = this.m_costList[0];
				this.m_costList.Remove(item);
			}
		}
		if (this.m_interpolate.IsEnd)
		{
			this.m_isPlaying = false;
			if (this.m_endCallback != null)
			{
				this.m_endCallback();
			}
			if (this.m_glowUpSlider != null)
			{
				GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "eff_thumb");
				if (gameObject != null)
				{
					gameObject.SetActive(false);
				}
			}
		}
		if (this.m_expLabel != null)
		{
			string text = string.Empty;
			if (!this.m_isPlaying)
			{
				int cost = this.m_endInfo.cost;
				int exp = this.m_endInfo.exp;
				text = GlowUpExpBar.CalcExpString(exp, cost);
			}
			else if (num6 == this.m_startInfo.level)
			{
				int cost2 = this.m_startInfo.cost;
				int exp2 = (int)((float)cost2 * num2);
				text = GlowUpExpBar.CalcExpString(exp2, cost2);
			}
			else if (num6 == this.m_endInfo.level)
			{
				int cost3 = this.m_endInfo.cost;
				int exp3 = (int)((float)cost3 * num2);
				text = GlowUpExpBar.CalcExpString(exp3, cost3);
			}
			else if (this.m_costList != null && this.m_costList.Count > 0)
			{
				int num8 = this.m_costList[0];
				int exp4 = (int)((float)num8 * num2);
				text = GlowUpExpBar.CalcExpString(exp4, num8);
			}
			if (!this.m_expLabel.gameObject.activeSelf)
			{
				this.m_expLabel.gameObject.SetActive(true);
			}
			this.m_expLabel.text = text;
		}
	}

	// Token: 0x06001B48 RID: 6984 RVA: 0x000A1830 File Offset: 0x0009FA30
	private static float CalcSliderValue(GlowUpExpBar.ExpInfo info)
	{
		int cost = info.cost;
		int exp = info.exp;
		if (cost == 0)
		{
			return 0f;
		}
		return (float)exp / (float)cost;
	}

	// Token: 0x06001B49 RID: 6985 RVA: 0x000A1860 File Offset: 0x0009FA60
	private static string CalcExpString(int exp, int cost)
	{
		string formatNumString = HudUtility.GetFormatNumString<int>(exp);
		string text = HudUtility.GetFormatNumString<int>(cost);
		TextObject text2 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MileageMap", "point");
		text2.ReplaceTag("{VALUE}", formatNumString);
		string text3 = text2.text;
		string text4 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Item", "ring").text;
		text += text4;
		return text3 + " / " + text;
	}

	// Token: 0x040018CB RID: 6347
	private UISlider m_baseSlider;

	// Token: 0x040018CC RID: 6348
	private UISlider m_glowUpSlider;

	// Token: 0x040018CD RID: 6349
	private GlowUpExpBar.ExpInfo m_startInfo = new GlowUpExpBar.ExpInfo();

	// Token: 0x040018CE RID: 6350
	private GlowUpExpBar.ExpInfo m_endInfo = new GlowUpExpBar.ExpInfo();

	// Token: 0x040018CF RID: 6351
	private GlowUpExpBar.LevelUpCallback m_levelUpCallback;

	// Token: 0x040018D0 RID: 6352
	private GlowUpExpBar.EndCallback m_endCallback;

	// Token: 0x040018D1 RID: 6353
	private HudInterpolateConstant m_interpolate = new HudInterpolateConstant();

	// Token: 0x040018D2 RID: 6354
	private bool m_isPlaying;

	// Token: 0x040018D3 RID: 6355
	private UILabel m_expLabel;

	// Token: 0x040018D4 RID: 6356
	private List<int> m_costList;

	// Token: 0x040018D5 RID: 6357
	private static readonly int RATIO_TO_VALUE = 10000;

	// Token: 0x040018D6 RID: 6358
	private static readonly float BAR_SPEED_PER_SEC = 0.33333334f * (float)GlowUpExpBar.RATIO_TO_VALUE;

	// Token: 0x020003A0 RID: 928
	public class ExpInfo
	{
		// Token: 0x040018D7 RID: 6359
		public int level;

		// Token: 0x040018D8 RID: 6360
		public int cost;

		// Token: 0x040018D9 RID: 6361
		public int exp;
	}

	// Token: 0x02000A83 RID: 2691
	// (Invoke) Token: 0x06004846 RID: 18502
	public delegate void LevelUpCallback(int level);

	// Token: 0x02000A84 RID: 2692
	// (Invoke) Token: 0x0600484A RID: 18506
	public delegate void EndCallback();
}
