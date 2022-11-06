using System;
using UnityEngine;

// Token: 0x020001B5 RID: 437
public class DebugTraceDisplayMenu : MonoBehaviour
{
	// Token: 0x06000C91 RID: 3217 RVA: 0x00047640 File Offset: 0x00045840
	public void Setup()
	{
		this.m_textBox = base.gameObject.AddComponent<DebugTraceTextBox>();
		this.m_textBox.Setup(new Vector2(210f, 10f));
		this.m_textBoxSizeBar = base.gameObject.AddComponent<DebugTraceScrollBar>();
		this.m_textBoxSizeBar.SetUp("BoxSize", new DebugTraceScrollBar.ValueChangedCallback(this.DebugScrollBarChangedCallback), new Vector2(10f, 200f));
		this.m_textScrollRatioBar = base.gameObject.AddComponent<DebugTraceScrollBar>();
		this.m_textScrollRatioBar.SetUp("ScrollRatio", new DebugTraceScrollBar.ValueChangedCallback(this.DebugScrollBarChangedCallback), new Vector2(10f, 300f));
		this.m_nextTypeButton = base.gameObject.AddComponent<DebugTraceButton>();
		this.m_nextTypeButton.Setup("NextType", new DebugTraceButton.ButtonClickedCallback(this.DebugButtonClickedCallback), new Vector2(110f, 100f), new Vector2(100f, 50f));
		this.m_prevTypeButton = base.gameObject.AddComponent<DebugTraceButton>();
		this.m_prevTypeButton.Setup("PrevType", new DebugTraceButton.ButtonClickedCallback(this.DebugButtonClickedCallback), new Vector2(10f, 100f), new Vector2(100f, 50f));
		this.m_clearButton = base.gameObject.AddComponent<DebugTraceButton>();
		this.m_clearButton.Setup("Clear", new DebugTraceButton.ButtonClickedCallback(this.DebugButtonClickedCallback), new Vector2(10f, 400f));
	}

	// Token: 0x06000C92 RID: 3218 RVA: 0x000477C4 File Offset: 0x000459C4
	public void SetActive(bool isActive)
	{
		this.m_isActive = isActive;
		if (this.m_textBox != null)
		{
			this.m_textBox.SetActive(isActive);
		}
		if (this.m_textBoxSizeBar != null)
		{
			this.m_textBoxSizeBar.SetActive(isActive);
		}
		if (this.m_textScrollRatioBar != null)
		{
			this.m_textScrollRatioBar.SetActive(isActive);
		}
		if (this.m_nextTypeButton != null)
		{
			this.m_nextTypeButton.SetActive(isActive);
		}
		if (this.m_prevTypeButton != null)
		{
			this.m_prevTypeButton.SetActive(isActive);
		}
		if (this.m_clearButton != null)
		{
			this.m_clearButton.SetActive(isActive);
		}
	}

	// Token: 0x06000C93 RID: 3219 RVA: 0x00047888 File Offset: 0x00045A88
	private void Start()
	{
	}

	// Token: 0x06000C94 RID: 3220 RVA: 0x0004788C File Offset: 0x00045A8C
	private void Update()
	{
		DebugTraceManager instance = DebugTraceManager.Instance;
		if (instance == null)
		{
			return;
		}
		if (this.m_textBox != null)
		{
			string text = instance.GetTraceText(this.m_traceType);
			if (string.IsNullOrEmpty(text))
			{
				text = "+Empty";
			}
			this.m_textBox.SetText(text);
		}
	}

	// Token: 0x06000C95 RID: 3221 RVA: 0x000478E8 File Offset: 0x00045AE8
	private void OnGUI()
	{
		if (!this.m_isActive)
		{
			return;
		}
		GUI.Label(new Rect(10f, 150f, 200f, 50f), DebugTraceManager.TypeName[(int)this.m_traceType]);
	}

	// Token: 0x06000C96 RID: 3222 RVA: 0x0004792C File Offset: 0x00045B2C
	private void DebugScrollBarChangedCallback(string name, float value)
	{
		if (this.m_textBox == null)
		{
			return;
		}
		if (name == "BoxSize")
		{
			float sizeScale = value * DebugTraceDisplayMenu.TextMaxScale / DebugTraceScrollBar.MaxValue + 1f;
			this.m_textBox.SetSizeScale(sizeScale);
		}
		else if (name == "ScrollRatio")
		{
			float num = value * DebugTraceDisplayMenu.ScrollMaxScale / DebugTraceScrollBar.MaxValue + 1f;
			this.m_textBox.SetScrollScale(new Vector2(num, num));
		}
	}

	// Token: 0x06000C97 RID: 3223 RVA: 0x000479B8 File Offset: 0x00045BB8
	private void DebugButtonClickedCallback(string buttonName)
	{
		if (buttonName == "NextType")
		{
			if (this.m_traceType == DebugTraceManager.TraceType.GAME)
			{
				this.m_traceType = DebugTraceManager.TraceType.ALL;
			}
			else
			{
				this.m_traceType++;
			}
		}
		else if (buttonName == "PrevType")
		{
			if (this.m_traceType == DebugTraceManager.TraceType.ALL)
			{
				this.m_traceType = DebugTraceManager.TraceType.GAME;
			}
			else
			{
				this.m_traceType--;
			}
		}
		else if (buttonName == "Clear")
		{
			DebugTraceManager instance = DebugTraceManager.Instance;
			if (instance != null)
			{
				instance.ClearTrace(this.m_traceType);
			}
		}
	}

	// Token: 0x040009D1 RID: 2513
	private bool m_isActive;

	// Token: 0x040009D2 RID: 2514
	private DebugTraceTextBox m_textBox;

	// Token: 0x040009D3 RID: 2515
	private DebugTraceScrollBar m_textBoxSizeBar;

	// Token: 0x040009D4 RID: 2516
	private DebugTraceScrollBar m_textScrollRatioBar;

	// Token: 0x040009D5 RID: 2517
	private DebugTraceButton m_nextTypeButton;

	// Token: 0x040009D6 RID: 2518
	private DebugTraceButton m_prevTypeButton;

	// Token: 0x040009D7 RID: 2519
	private DebugTraceButton m_clearButton;

	// Token: 0x040009D8 RID: 2520
	private DebugTraceManager.TraceType m_traceType;

	// Token: 0x040009D9 RID: 2521
	private static readonly float TextMaxScale = 4f;

	// Token: 0x040009DA RID: 2522
	private static readonly float ScrollMaxScale = 200f;
}
