using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000D6 RID: 214
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Input Field")]
public class UIInput : UIWidgetContainer
{
	// Token: 0x1700010E RID: 270
	// (get) Token: 0x06000635 RID: 1589 RVA: 0x000221DC File Offset: 0x000203DC
	// (set) Token: 0x06000636 RID: 1590 RVA: 0x000221E4 File Offset: 0x000203E4
	[Obsolete("Use UIInput.value instead")]
	public string text
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	// Token: 0x1700010F RID: 271
	// (get) Token: 0x06000637 RID: 1591 RVA: 0x000221F0 File Offset: 0x000203F0
	// (set) Token: 0x06000638 RID: 1592 RVA: 0x0002220C File Offset: 0x0002040C
	public string value
	{
		get
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			return this.mText;
		}
		set
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			if (this.mText != value)
			{
				this.mText = value;
				this.SaveToPlayerPrefs(this.mText);
			}
			if (this.mKeyboard != null)
			{
				this.mKeyboard.text = value;
			}
			if (this.label != null)
			{
				if (string.IsNullOrEmpty(value))
				{
					value = this.mDefaultText;
				}
				this.label.supportEncoding = false;
				this.label.text = ((!this.selected) ? value : (value + this.caratChar));
				this.label.showLastPasswordChar = this.selected;
				this.label.color = ((!this.selected && !(value != this.mDefaultText)) ? this.mDefaultColor : this.activeColor);
			}
		}
	}

	// Token: 0x17000110 RID: 272
	// (get) Token: 0x06000639 RID: 1593 RVA: 0x00022308 File Offset: 0x00020508
	// (set) Token: 0x0600063A RID: 1594 RVA: 0x0002231C File Offset: 0x0002051C
	public bool selected
	{
		get
		{
			return UICamera.selectedObject == base.gameObject;
		}
		set
		{
			if (!value && UICamera.selectedObject == base.gameObject)
			{
				UICamera.selectedObject = null;
			}
			else if (value)
			{
				UICamera.selectedObject = base.gameObject;
			}
		}
	}

	// Token: 0x17000111 RID: 273
	// (get) Token: 0x0600063B RID: 1595 RVA: 0x00022360 File Offset: 0x00020560
	// (set) Token: 0x0600063C RID: 1596 RVA: 0x00022368 File Offset: 0x00020568
	public string defaultText
	{
		get
		{
			return this.mDefaultText;
		}
		set
		{
			if (this.label.text == this.mDefaultText)
			{
				this.label.text = value;
			}
			this.mDefaultText = value;
		}
	}

	// Token: 0x0600063D RID: 1597 RVA: 0x000223A4 File Offset: 0x000205A4
	protected void Init()
	{
		if (this.mDoInit)
		{
			this.mDoInit = false;
			if (this.label == null)
			{
				this.label = base.GetComponentInChildren<UILabel>();
			}
			if (this.label != null)
			{
				if (this.useLabelTextAtStart)
				{
					this.mText = this.label.text;
				}
				this.mDefaultText = this.label.text;
				this.mDefaultColor = this.label.color;
				this.label.supportEncoding = false;
				this.label.password = this.isPassword;
				this.label.maxLineCount = 1;
				this.mPivot = this.label.pivot;
				this.mPosition = this.label.cachedTransform.localPosition.x;
			}
			else
			{
				base.enabled = false;
			}
		}
	}

	// Token: 0x0600063E RID: 1598 RVA: 0x00022494 File Offset: 0x00020694
	private void SaveToPlayerPrefs(string val)
	{
		if (!string.IsNullOrEmpty(this.playerPrefsField))
		{
			if (string.IsNullOrEmpty(val))
			{
				PlayerPrefs.DeleteKey(this.playerPrefsField);
			}
			else
			{
				PlayerPrefs.SetString(this.playerPrefsField, val);
			}
		}
	}

	// Token: 0x0600063F RID: 1599 RVA: 0x000224D0 File Offset: 0x000206D0
	private void Awake()
	{
		if (this.label == null)
		{
			this.label = base.GetComponentInChildren<UILabel>();
		}
		if (!string.IsNullOrEmpty(this.playerPrefsField) && PlayerPrefs.HasKey(this.playerPrefsField))
		{
			this.value = PlayerPrefs.GetString(this.playerPrefsField);
		}
	}

	// Token: 0x06000640 RID: 1600 RVA: 0x0002252C File Offset: 0x0002072C
	private void Start()
	{
		if (EventDelegate.IsValid(this.onSubmit))
		{
			if (this.eventReceiver != null || !string.IsNullOrEmpty(this.functionName))
			{
				this.eventReceiver = null;
				this.functionName = null;
			}
		}
		else if (this.eventReceiver == null && !EventDelegate.IsValid(this.onSubmit))
		{
			this.eventReceiver = base.gameObject;
		}
	}

	// Token: 0x06000641 RID: 1601 RVA: 0x000225AC File Offset: 0x000207AC
	private void OnEnable()
	{
		if (UICamera.IsHighlighted(base.gameObject))
		{
			this.OnSelect(true);
		}
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x000225C8 File Offset: 0x000207C8
	private void OnDisable()
	{
		if (UICamera.IsHighlighted(base.gameObject))
		{
			this.OnSelect(false);
		}
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x000225E4 File Offset: 0x000207E4
	private void OnSelect(bool isSelected)
	{
		if (this.mDoInit)
		{
			this.Init();
		}
		if (this.label != null && base.enabled && NGUITools.GetActive(base.gameObject))
		{
			if (isSelected)
			{
				this.mText = ((this.useLabelTextAtStart || !(this.label.text == this.mDefaultText)) ? this.label.text : string.Empty);
				this.label.color = this.activeColor;
				if (this.isPassword)
				{
					this.label.password = true;
				}
				if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
				{
					if (this.isPassword)
					{
						this.mKeyboard = TouchScreenKeyboard.Open(this.mText, TouchScreenKeyboardType.Default, false, false, true);
					}
					else
					{
						this.mKeyboard = TouchScreenKeyboard.Open(this.mText, (TouchScreenKeyboardType)this.type, this.autoCorrect);
					}
				}
				else
				{
					Input.imeCompositionMode = IMECompositionMode.On;
					Input.compositionCursorPos = UICamera.currentCamera.WorldToScreenPoint(this.label.worldCorners[0]);
				}
				this.UpdateLabel();
			}
			else
			{
				if (this.mKeyboard != null)
				{
					this.mKeyboard.active = false;
				}
				if (string.IsNullOrEmpty(this.mText))
				{
					this.label.text = this.mDefaultText;
					this.label.color = this.mDefaultColor;
					if (this.isPassword)
					{
						this.label.password = false;
					}
				}
				else
				{
					this.label.text = this.mText;
				}
				this.label.showLastPasswordChar = false;
				Input.imeCompositionMode = IMECompositionMode.Off;
				this.RestoreLabel();
			}
		}
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x000227C4 File Offset: 0x000209C4
	private void Update()
	{
		if (this.mKeyboard != null)
		{
			string text = this.mKeyboard.text;
			if (this.mText != text)
			{
				this.mText = string.Empty;
				foreach (char c in text)
				{
					if (this.validator != null)
					{
						c = this.validator(this.mText, c);
					}
					if (c != '\0')
					{
						this.mText += c;
					}
				}
				if (this.maxChars > 0 && this.mText.Length > this.maxChars)
				{
					this.mText = this.mText.Substring(0, this.maxChars);
				}
				this.UpdateLabel();
				if (this.mText != text)
				{
					this.mKeyboard.text = this.mText;
				}
				base.SendMessage("OnInputChanged", this, SendMessageOptions.DontRequireReceiver);
			}
			if (this.mKeyboard.done)
			{
				this.mKeyboard = null;
				this.Submit();
				this.selected = false;
			}
		}
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x000228F4 File Offset: 0x00020AF4
	private void OnInput(string input)
	{
		if (this.mDoInit)
		{
			this.Init();
		}
		if (this.selected && base.enabled && NGUITools.GetActive(base.gameObject))
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				return;
			}
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				return;
			}
			this.Append(input);
		}
	}

	// Token: 0x06000646 RID: 1606 RVA: 0x00022958 File Offset: 0x00020B58
	private void Submit()
	{
		UIInput.current = this;
		if (EventDelegate.IsValid(this.onSubmit))
		{
			EventDelegate.Execute(this.onSubmit);
		}
		else if (this.eventReceiver != null && !string.IsNullOrEmpty(this.functionName))
		{
			this.eventReceiver.SendMessage(this.functionName, this.mText, SendMessageOptions.DontRequireReceiver);
		}
		this.SaveToPlayerPrefs(this.mText);
		UIInput.current = null;
	}

	// Token: 0x06000647 RID: 1607 RVA: 0x000229D8 File Offset: 0x00020BD8
	private void Append(string input)
	{
		int i = 0;
		int length = input.Length;
		while (i < length)
		{
			char c = input[i];
			if (c == '\b')
			{
				if (this.mText.Length > 0)
				{
					this.mText = this.mText.Substring(0, this.mText.Length - 1);
					base.SendMessage("OnInputChanged", this, SendMessageOptions.DontRequireReceiver);
				}
			}
			else if (c == '\r' || c == '\n')
			{
				if ((UICamera.current.submitKey0 == KeyCode.Return || UICamera.current.submitKey1 == KeyCode.Return) && (!this.label.multiLine || (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl))))
				{
					this.Submit();
					this.selected = false;
					return;
				}
				if (this.validator != null)
				{
					c = this.validator(this.mText, c);
				}
				if (c != '\0')
				{
					if (c == '\n' || c == '\r')
					{
						if (this.label.multiLine)
						{
							this.mText += "\n";
						}
					}
					else
					{
						this.mText += c;
					}
					base.SendMessage("OnInputChanged", this, SendMessageOptions.DontRequireReceiver);
				}
			}
			else if (c >= ' ')
			{
				if (this.validator != null)
				{
					c = this.validator(this.mText, c);
				}
				if (c != '\0')
				{
					this.mText += c;
					base.SendMessage("OnInputChanged", this, SendMessageOptions.DontRequireReceiver);
				}
			}
			i++;
		}
		this.UpdateLabel();
	}

	// Token: 0x06000648 RID: 1608 RVA: 0x00022BA8 File Offset: 0x00020DA8
	private void UpdateLabel()
	{
		if (this.mDoInit)
		{
			this.Init();
		}
		if (this.maxChars > 0 && this.mText.Length > this.maxChars)
		{
			this.mText = this.mText.Substring(0, this.maxChars);
		}
		if (this.label.font != null)
		{
			string text;
			if (this.isPassword && this.selected)
			{
				text = string.Empty;
				int i = 0;
				int length = this.mText.Length;
				while (i < length)
				{
					text += "*";
					i++;
				}
				text = text + Input.compositionString + this.caratChar;
			}
			else
			{
				text = ((!this.selected) ? this.mText : (this.mText + Input.compositionString + this.caratChar));
			}
			this.label.supportEncoding = false;
			if (this.label.overflowMethod == UILabel.Overflow.ClampContent)
			{
				if (this.label.multiLine)
				{
					this.label.font.WrapText(text, out text, this.label.width, this.label.height, 0, false, UIFont.SymbolStyle.None);
				}
				else
				{
					string endOfLineThatFits = this.label.font.GetEndOfLineThatFits(text, (float)this.label.width, false, UIFont.SymbolStyle.None);
					if (endOfLineThatFits != text)
					{
						text = endOfLineThatFits;
						Vector3 localPosition = this.label.cachedTransform.localPosition;
						localPosition.x = this.mPosition + (float)this.label.width;
						if (this.mPivot == UIWidget.Pivot.Left)
						{
							this.label.pivot = UIWidget.Pivot.Right;
						}
						else if (this.mPivot == UIWidget.Pivot.TopLeft)
						{
							this.label.pivot = UIWidget.Pivot.TopRight;
						}
						else if (this.mPivot == UIWidget.Pivot.BottomLeft)
						{
							this.label.pivot = UIWidget.Pivot.BottomRight;
						}
						this.label.cachedTransform.localPosition = localPosition;
					}
					else
					{
						this.RestoreLabel();
					}
				}
			}
			this.label.text = text;
			this.label.showLastPasswordChar = this.selected;
		}
	}

	// Token: 0x06000649 RID: 1609 RVA: 0x00022DE4 File Offset: 0x00020FE4
	private void RestoreLabel()
	{
		if (this.label != null)
		{
			this.label.pivot = this.mPivot;
			Vector3 localPosition = this.label.cachedTransform.localPosition;
			localPosition.x = this.mPosition;
			this.label.cachedTransform.localPosition = localPosition;
		}
	}

	// Token: 0x04000498 RID: 1176
	public static UIInput current;

	// Token: 0x04000499 RID: 1177
	public UILabel label;

	// Token: 0x0400049A RID: 1178
	public int maxChars;

	// Token: 0x0400049B RID: 1179
	public string caratChar = "|";

	// Token: 0x0400049C RID: 1180
	public string playerPrefsField;

	// Token: 0x0400049D RID: 1181
	public UIInput.Validator validator;

	// Token: 0x0400049E RID: 1182
	public UIInput.KeyboardType type;

	// Token: 0x0400049F RID: 1183
	public bool isPassword;

	// Token: 0x040004A0 RID: 1184
	public bool autoCorrect;

	// Token: 0x040004A1 RID: 1185
	public bool useLabelTextAtStart;

	// Token: 0x040004A2 RID: 1186
	public Color activeColor = Color.white;

	// Token: 0x040004A3 RID: 1187
	public GameObject selectOnTab;

	// Token: 0x040004A4 RID: 1188
	public List<EventDelegate> onSubmit = new List<EventDelegate>();

	// Token: 0x040004A5 RID: 1189
	[SerializeField]
	[HideInInspector]
	private GameObject eventReceiver;

	// Token: 0x040004A6 RID: 1190
	[SerializeField]
	[HideInInspector]
	private string functionName = "OnSubmit";

	// Token: 0x040004A7 RID: 1191
	private string mText = string.Empty;

	// Token: 0x040004A8 RID: 1192
	private string mDefaultText = string.Empty;

	// Token: 0x040004A9 RID: 1193
	private Color mDefaultColor = Color.white;

	// Token: 0x040004AA RID: 1194
	private UIWidget.Pivot mPivot = UIWidget.Pivot.Left;

	// Token: 0x040004AB RID: 1195
	private float mPosition;

	// Token: 0x040004AC RID: 1196
	private TouchScreenKeyboard mKeyboard;

	// Token: 0x040004AD RID: 1197
	private bool mDoInit = true;

	// Token: 0x020000D7 RID: 215
	public enum KeyboardType
	{
		// Token: 0x040004AF RID: 1199
		Default,
		// Token: 0x040004B0 RID: 1200
		ASCIICapable,
		// Token: 0x040004B1 RID: 1201
		NumbersAndPunctuation,
		// Token: 0x040004B2 RID: 1202
		URL,
		// Token: 0x040004B3 RID: 1203
		NumberPad,
		// Token: 0x040004B4 RID: 1204
		PhonePad,
		// Token: 0x040004B5 RID: 1205
		NamePhonePad,
		// Token: 0x040004B6 RID: 1206
		EmailAddress
	}

	// Token: 0x02000A72 RID: 2674
	// (Invoke) Token: 0x06004802 RID: 18434
	public delegate char Validator(string currentText, char nextChar);
}
