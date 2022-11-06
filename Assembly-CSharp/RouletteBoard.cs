using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200050E RID: 1294
public class RouletteBoard : RoulettePartsBase
{
	// Token: 0x17000526 RID: 1318
	// (get) Token: 0x060026EC RID: 9964 RVA: 0x000EECD4 File Offset: 0x000ECED4
	public ServerWheelOptionsData wheelData
	{
		get
		{
			if (this.m_parent == null)
			{
				return null;
			}
			return this.m_parent.wheelData;
		}
	}

	// Token: 0x060026ED RID: 9965 RVA: 0x000EECF4 File Offset: 0x000ECEF4
	public override void Setup(RouletteTop parent)
	{
		base.Setup(parent);
		this.m_isEffectLock = false;
		if (this.m_parent != null && this.m_parent.wheelData != null && this.m_parent.wheelData.category == RouletteCategory.ITEM)
		{
			this.m_isEffectLock = true;
		}
		this.ONE_FPS_TIME = 1f / (float)Application.targetFrameRate;
		this.SetupBoard(this.m_parent.wheelData);
		this.SetupArrow(this.m_parent.wheelData);
		this.UpdateEffectSetting();
	}

	// Token: 0x060026EE RID: 9966 RVA: 0x000EED88 File Offset: 0x000ECF88
	public override void OnUpdateWheelData(ServerWheelOptionsData data)
	{
		this.m_isEffectLock = false;
		if (data != null && data.category == RouletteCategory.ITEM)
		{
			this.m_isEffectLock = true;
		}
		this.SetupBoard(data);
		this.SetupArrow(data);
		this.UpdateEffectSetting();
	}

	// Token: 0x060026EF RID: 9967 RVA: 0x000EEDCC File Offset: 0x000ECFCC
	private void SetupBoard(ServerWheelOptionsData data)
	{
		if (data != null)
		{
			int rouletteBoardPattern = data.GetRouletteBoardPattern();
			if (this.m_pattern != null && this.m_pattern.Count > 0)
			{
				for (int i = 0; i < this.m_pattern.Count; i++)
				{
					RouletteBoardPattern rouletteBoardPattern2 = this.m_pattern[i];
					if (rouletteBoardPattern2 != null)
					{
						if (rouletteBoardPattern == i)
						{
							rouletteBoardPattern2.Setup(this, this.m_orgRouletteItem, 0);
							this.m_currentBoardPattern = rouletteBoardPattern2;
						}
						else
						{
							rouletteBoardPattern2.Reset();
						}
					}
				}
			}
		}
	}

	// Token: 0x060026F0 RID: 9968 RVA: 0x000EEE60 File Offset: 0x000ED060
	private void SetupArrow(ServerWheelOptionsData data)
	{
		if (this.m_arrow != null)
		{
			this.m_arrow.SetActive(true);
			UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_arrow, "img_roulette_arrow_0");
			if (uisprite != null && data != null)
			{
				uisprite.spriteName = data.GetRouletteArrowSprite();
			}
			this.m_currentDegree = 0f;
			this.m_currentArrowPos = -1;
			this.m_arrow.transform.rotation = Quaternion.Euler(42f, 9.139092E-05f, 0f);
			if (this.m_currentBoardPattern != null)
			{
				int cellIndex = this.m_currentBoardPattern.GetCellIndex(this.m_currentDegree);
				if (cellIndex >= 0)
				{
					this.m_currentArrowPos = cellIndex;
					this.m_currentBoardPattern.SetCurrentCell(this.m_currentArrowPos);
				}
			}
		}
	}

	// Token: 0x060026F1 RID: 9969 RVA: 0x000EEF34 File Offset: 0x000ED134
	protected override void UpdateParts()
	{
		if (base.isSpin && this.m_currentDegreeMax > 0f && this.m_arrow != null)
		{
			bool flag = false;
			if (this.m_currentDegree < this.m_currentDegreeMax)
			{
				float arrowMove = this.GetArrowMove();
				if (this.m_arrowSpeed > arrowMove)
				{
					flag = true;
				}
				this.m_currentDegree += arrowMove;
				if (this.m_currentDegreeMax <= this.m_currentDegree)
				{
					if (this.m_parent != null)
					{
						this.m_parent.OnRouletteSpinEnd();
					}
					this.m_currentDegree = this.m_currentDegreeMax;
					int currentDegreeRot = this.GetCurrentDegreeRot();
					if (currentDegreeRot > 0)
					{
						this.m_currentDegree -= (float)currentDegreeRot * 360f;
					}
					this.m_currentDegreeMax = 0f;
				}
			}
			if (this.m_partsUpdateCount % 2L == 0L || flag)
			{
				int cellIndex = this.m_currentBoardPattern.GetCellIndex(this.m_currentDegree);
				if (cellIndex >= 0)
				{
					if (this.m_currentArrowPos != cellIndex)
					{
						base.wheel.PlaySe(ServerWheelOptionsData.SE_TYPE.Arrow, 0f);
					}
					this.m_currentArrowPos = cellIndex;
					this.m_currentBoardPattern.SetCurrentCell(this.m_currentArrowPos);
				}
			}
			this.m_arrow.transform.rotation = Quaternion.Euler(42f, 9.139092E-05f, -this.m_currentDegree);
		}
	}

	// Token: 0x060026F2 RID: 9970 RVA: 0x000EF094 File Offset: 0x000ED294
	public override void UpdateEffectSetting()
	{
		if (this.m_pattern != null && this.m_pattern.Count > 0)
		{
			for (int i = 0; i < this.m_pattern.Count; i++)
			{
				RouletteBoardPattern rouletteBoardPattern = this.m_pattern[i];
				if (rouletteBoardPattern != null)
				{
					rouletteBoardPattern.UpdateEffectSetting();
				}
			}
		}
		if (!base.parent.IsEffect(RouletteTop.ROULETTE_EFFECT_TYPE.BOARD))
		{
			this.m_isEffectLock = true;
		}
	}

	// Token: 0x060026F3 RID: 9971 RVA: 0x000EF110 File Offset: 0x000ED310
	public override void DestroyParts()
	{
		if (this.m_currentBoardPattern != null)
		{
			this.m_currentBoardPattern.Reset();
		}
		this.m_currentBoardPattern = null;
		base.DestroyParts();
	}

	// Token: 0x060026F4 RID: 9972 RVA: 0x000EF13C File Offset: 0x000ED33C
	public override void OnSpinStart()
	{
		this.ONE_FPS_TIME = 1f / (float)Application.targetFrameRate;
		this.m_degreeSlow = 180f;
		this.m_degreeSlowLast = 1f;
		this.m_currentDegreeMax = 9999999f;
	}

	// Token: 0x060026F5 RID: 9973 RVA: 0x000EF174 File Offset: 0x000ED374
	public override void OnSpinSkip()
	{
		if (base.spinDecisionIndex == -1)
		{
			return;
		}
		float num = this.m_currentDegreeMax - this.m_currentDegree;
		if (num >= this.m_degreeSlow * 0.5f)
		{
			float num2 = (float)((int)((num - this.m_degreeSlow * 0.5f) / 360f));
			if (num2 > 0f)
			{
				this.m_currentDegree += 360f * num2;
			}
		}
	}

	// Token: 0x060026F6 RID: 9974 RVA: 0x000EF1E4 File Offset: 0x000ED3E4
	public override void OnSpinDecision()
	{
		if (base.spinDecisionIndex == -1)
		{
			return;
		}
		int currentDegreeRot = this.GetCurrentDegreeRot();
		int num = 3;
		if (currentDegreeRot >= 2)
		{
			num = currentDegreeRot + 1;
		}
		if (this.m_currentBoardPattern != null)
		{
			float num2;
			float num3;
			float num4;
			this.m_currentBoardPattern.GetCellData(base.spinDecisionIndex, out num2, out num3, out num4);
			float num5 = num3 - num2;
			float num6 = UnityEngine.Random.Range(0f, 1f);
			float num7;
			if (num4 > 0.8f)
			{
				num7 = num2 + num5 * num6;
			}
			else
			{
				num6 = 1f - num6 * num6;
				if ((int)(this.m_currentDegree * 100f) % 2 == 0)
				{
					num7 = num2 + num5 * num6;
				}
				else
				{
					num7 = num3 - num5 * num6;
				}
				if (num6 < 0.8f && num4 < 0.5f)
				{
					num4 = 0.5f;
				}
			}
			float currentDegreeMax = (float)num * 360f + num7;
			this.m_degreeSlow = 180f + UnityEngine.Random.Range(0f, 30f);
			if (num4 < 1f)
			{
				this.m_degreeSlow += UnityEngine.Random.Range(10f, 30f);
				if (num4 < 0.5f)
				{
					this.m_degreeSlow += UnityEngine.Random.Range(30f, 50f);
				}
			}
			this.m_currentDegreeMax = currentDegreeMax;
			this.m_degreeSlowLast = num4;
		}
	}

	// Token: 0x060026F7 RID: 9975 RVA: 0x000EF350 File Offset: 0x000ED550
	public override void OnSpinEnd()
	{
	}

	// Token: 0x060026F8 RID: 9976 RVA: 0x000EF354 File Offset: 0x000ED554
	public override void OnSpinError()
	{
	}

	// Token: 0x060026F9 RID: 9977 RVA: 0x000EF358 File Offset: 0x000ED558
	private float GetLastSlowPoint()
	{
		float result = this.m_degreeSlow * 0.4f;
		if (this.m_degreeSlowLast < 1f)
		{
			result = this.m_degreeSlow * (0.4f + (1f - this.m_degreeSlowLast) * 0.02f);
		}
		return result;
	}

	// Token: 0x060026FA RID: 9978 RVA: 0x000EF3A4 File Offset: 0x000ED5A4
	private int GetCurrentDegreeRot()
	{
		return (int)(this.m_currentDegree / 360f);
	}

	// Token: 0x060026FB RID: 9979 RVA: 0x000EF3B4 File Offset: 0x000ED5B4
	private int GetEndDegreeRot()
	{
		float num = this.m_currentDegreeMax - this.m_currentDegree;
		int num2 = (int)(num / 360f);
		if (num2 < 0)
		{
			num2 = 0;
		}
		return num2;
	}

	// Token: 0x060026FC RID: 9980 RVA: 0x000EF3E4 File Offset: 0x000ED5E4
	private float GetEndDegreeRotFloat()
	{
		float num = this.m_currentDegreeMax - this.m_currentDegree;
		float num2 = num / 360f;
		if (num2 < 0f)
		{
			num2 = 0f;
		}
		return num2;
	}

	// Token: 0x060026FD RID: 9981 RVA: 0x000EF41C File Offset: 0x000ED61C
	private float GetArrowMove()
	{
		if (this.m_partsUpdateCount % 7L == 0L || this.m_arrowSpeed <= 0.5f)
		{
			this.m_timeRate = Time.deltaTime / this.ONE_FPS_TIME;
			if (this.m_timeRate > 1.2f)
			{
				this.m_timeRate = 1.2f;
			}
			else if (this.m_timeRate < 0.9f)
			{
				this.m_timeRate = 0.9f;
			}
			this.m_arrowSpeed = 10f * this.m_timeRate;
		}
		float num = this.m_arrowSpeed;
		float num2 = this.GetEndDegreeRotFloat();
		if (num2 > 3f)
		{
			num2 = 3f;
		}
		if (num2 > 0f)
		{
			num *= 1f + num2 * 0.25f;
		}
		if (num2 < 1.5f && this.m_currentDegreeMax - this.m_currentDegree <= this.m_degreeSlow)
		{
			float num3 = this.m_currentDegreeMax - this.m_currentDegree;
			float num4 = num3 / this.m_degreeSlow;
			if (num4 < 0f)
			{
				num4 = 0f;
			}
			if (num4 > 1f)
			{
				num4 = 1f;
			}
			float num5 = num4;
			if (num5 > 1f)
			{
				num5 = 1f;
			}
			if (num5 < 0.025f * this.m_degreeSlowLast)
			{
				num5 = 0.025f * this.m_degreeSlowLast;
			}
			num *= num5;
		}
		return num;
	}

	// Token: 0x0400234F RID: 9039
	public const int ROULETTE_SPIN_MIN_ROT = 2;

	// Token: 0x04002350 RID: 9040
	public const float ROULETTE_SPIN_SPEED = 10f;

	// Token: 0x04002351 RID: 9041
	public const float ROULETTE_SPIN_SLOW_SPEED_RATE = 0.4f;

	// Token: 0x04002352 RID: 9042
	public const float ROULETTE_SPIN_SLOW_SPEED_LAST = 0.025f;

	// Token: 0x04002353 RID: 9043
	public const float ROULETTE_SPIN_SLOW_DEG = 180f;

	// Token: 0x04002354 RID: 9044
	public const float ROULETTE_SPIN_SKIP_POINT_RATE = 0.5f;

	// Token: 0x04002355 RID: 9045
	public const float ROULETTE_SPIN_MAX = 9999999f;

	// Token: 0x04002356 RID: 9046
	private const float ARROW_ROTATION_X = 42f;

	// Token: 0x04002357 RID: 9047
	private const float ARROW_ROTATION_Y = 9.139092E-05f;

	// Token: 0x04002358 RID: 9048
	[SerializeField]
	private GameObject m_arrow;

	// Token: 0x04002359 RID: 9049
	[SerializeField]
	private List<RouletteBoardPattern> m_pattern;

	// Token: 0x0400235A RID: 9050
	[SerializeField]
	private RouletteItem m_orgRouletteItem;

	// Token: 0x0400235B RID: 9051
	private float m_currentDegree;

	// Token: 0x0400235C RID: 9052
	private float m_degreeSlow;

	// Token: 0x0400235D RID: 9053
	private float m_degreeSlowLast;

	// Token: 0x0400235E RID: 9054
	private float m_currentDegreeMax;

	// Token: 0x0400235F RID: 9055
	private float m_timeRate = 1f;

	// Token: 0x04002360 RID: 9056
	private float m_arrowSpeed;

	// Token: 0x04002361 RID: 9057
	private int m_currentArrowPos = -1;

	// Token: 0x04002362 RID: 9058
	private RouletteBoardPattern m_currentBoardPattern;

	// Token: 0x04002363 RID: 9059
	private float ONE_FPS_TIME = 0.016666668f;
}
