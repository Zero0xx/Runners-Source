using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000092 RID: 146
[AddComponentMenu("NGUI/Interaction/Popup List")]
public class UIPopupList : UIWidgetContainer
{
	// Token: 0x1700007B RID: 123
	// (get) Token: 0x060003BB RID: 955 RVA: 0x00012410 File Offset: 0x00010610
	// (set) Token: 0x060003BC RID: 956 RVA: 0x00012418 File Offset: 0x00010618
	[Obsolete("Use EventDelegate.Add(popup.onChange, YourCallback) instead, and UIPopupList.current.value to determine the state")]
	public UIPopupList.LegacyEvent onSelectionChange
	{
		get
		{
			return this.mLegacyEvent;
		}
		set
		{
			this.mLegacyEvent = value;
		}
	}

	// Token: 0x1700007C RID: 124
	// (get) Token: 0x060003BD RID: 957 RVA: 0x00012424 File Offset: 0x00010624
	public bool isOpen
	{
		get
		{
			return this.mChild != null;
		}
	}

	// Token: 0x1700007D RID: 125
	// (get) Token: 0x060003BE RID: 958 RVA: 0x00012434 File Offset: 0x00010634
	// (set) Token: 0x060003BF RID: 959 RVA: 0x0001243C File Offset: 0x0001063C
	public string value
	{
		get
		{
			return this.mSelectedItem;
		}
		set
		{
			bool flag = false;
			if (this.mSelectedItem != value)
			{
				this.mSelectedItem = value;
				if (this.mSelectedItem == null)
				{
					return;
				}
				if (this.textLabel != null)
				{
					this.textLabel.text = ((!this.isLocalized) ? value : Localization.Localize(value));
				}
				flag = true;
			}
			if (this.mSelectedItem != null && (flag || this.textLabel == null))
			{
				UIPopupList.current = this;
				if (this.mLegacyEvent != null)
				{
					this.mLegacyEvent(this.mSelectedItem);
				}
				if (EventDelegate.IsValid(this.onChange))
				{
					EventDelegate.Execute(this.onChange);
				}
				else if (this.eventReceiver != null && !string.IsNullOrEmpty(this.functionName))
				{
					this.eventReceiver.SendMessage(this.functionName, this.mSelectedItem, SendMessageOptions.DontRequireReceiver);
				}
				UIPopupList.current = null;
			}
		}
	}

	// Token: 0x1700007E RID: 126
	// (get) Token: 0x060003C0 RID: 960 RVA: 0x00012548 File Offset: 0x00010748
	// (set) Token: 0x060003C1 RID: 961 RVA: 0x00012550 File Offset: 0x00010750
	[Obsolete("Use 'value' instead")]
	public string selection
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

	// Token: 0x1700007F RID: 127
	// (get) Token: 0x060003C2 RID: 962 RVA: 0x0001255C File Offset: 0x0001075C
	// (set) Token: 0x060003C3 RID: 963 RVA: 0x00012588 File Offset: 0x00010788
	private bool handleEvents
	{
		get
		{
			UIButtonKeys component = base.GetComponent<UIButtonKeys>();
			return component == null || !component.enabled;
		}
		set
		{
			UIButtonKeys component = base.GetComponent<UIButtonKeys>();
			if (component != null)
			{
				component.enabled = !value;
			}
		}
	}

	// Token: 0x060003C4 RID: 964 RVA: 0x000125B4 File Offset: 0x000107B4
	private void Start()
	{
		if (EventDelegate.IsValid(this.onChange))
		{
			this.eventReceiver = null;
			this.functionName = null;
		}
		if (string.IsNullOrEmpty(this.mSelectedItem))
		{
			if (this.items.Count > 0)
			{
				this.value = this.items[0];
			}
		}
		else
		{
			string value = this.mSelectedItem;
			this.mSelectedItem = null;
			this.value = value;
		}
	}

	// Token: 0x060003C5 RID: 965 RVA: 0x0001262C File Offset: 0x0001082C
	private void OnLocalize(Localization loc)
	{
		if (this.isLocalized && this.textLabel != null)
		{
			this.textLabel.text = loc.Get(this.mSelectedItem);
		}
	}

	// Token: 0x060003C6 RID: 966 RVA: 0x00012664 File Offset: 0x00010864
	private void Highlight(UILabel lbl, bool instant)
	{
		if (this.mHighlight != null)
		{
			TweenPosition component = lbl.GetComponent<TweenPosition>();
			if (component != null && component.enabled)
			{
				return;
			}
			this.mHighlightedLabel = lbl;
			UISpriteData atlasSprite = this.mHighlight.GetAtlasSprite();
			if (atlasSprite == null)
			{
				return;
			}
			float pixelSize = this.atlas.pixelSize;
			float num = (float)atlasSprite.borderLeft * pixelSize;
			float y = (float)atlasSprite.borderTop * pixelSize;
			Vector3 vector = lbl.cachedTransform.localPosition + new Vector3(-num, y, 1f);
			if (instant || !this.isAnimated)
			{
				this.mHighlight.cachedTransform.localPosition = vector;
			}
			else
			{
				TweenPosition.Begin(this.mHighlight.gameObject, 0.1f, vector).method = UITweener.Method.EaseOut;
			}
		}
	}

	// Token: 0x060003C7 RID: 967 RVA: 0x00012744 File Offset: 0x00010944
	private void OnItemHover(GameObject go, bool isOver)
	{
		if (isOver)
		{
			UILabel component = go.GetComponent<UILabel>();
			this.Highlight(component, false);
		}
	}

	// Token: 0x060003C8 RID: 968 RVA: 0x00012768 File Offset: 0x00010968
	private void Select(UILabel lbl, bool instant)
	{
		this.Highlight(lbl, instant);
		UIEventListener component = lbl.gameObject.GetComponent<UIEventListener>();
		this.value = (component.parameter as string);
		UIPlaySound[] components = base.GetComponents<UIPlaySound>();
		int i = 0;
		int num = components.Length;
		while (i < num)
		{
			UIPlaySound uiplaySound = components[i];
			if (uiplaySound.trigger == UIPlaySound.Trigger.OnClick)
			{
				NGUITools.PlaySound(uiplaySound.audioClip, uiplaySound.volume, 1f);
			}
			i++;
		}
	}

	// Token: 0x060003C9 RID: 969 RVA: 0x000127E4 File Offset: 0x000109E4
	private void OnItemPress(GameObject go, bool isPressed)
	{
		if (isPressed)
		{
			this.Select(go.GetComponent<UILabel>(), true);
		}
	}

	// Token: 0x060003CA RID: 970 RVA: 0x000127FC File Offset: 0x000109FC
	private void OnKey(KeyCode key)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.handleEvents)
		{
			int num = this.mLabelList.IndexOf(this.mHighlightedLabel);
			if (key == KeyCode.UpArrow)
			{
				if (num > 0)
				{
					this.Select(this.mLabelList[num - 1], false);
				}
			}
			else if (key == KeyCode.DownArrow)
			{
				if (num + 1 < this.mLabelList.Count)
				{
					this.Select(this.mLabelList[num + 1], false);
				}
			}
			else if (key == KeyCode.Escape)
			{
				this.OnSelect(false);
			}
		}
	}

	// Token: 0x060003CB RID: 971 RVA: 0x000128B8 File Offset: 0x00010AB8
	private void OnSelect(bool isSelected)
	{
		if (!isSelected && this.mChild != null)
		{
			this.mLabelList.Clear();
			this.handleEvents = false;
			if (this.isAnimated)
			{
				UIWidget[] componentsInChildren = this.mChild.GetComponentsInChildren<UIWidget>();
				int i = 0;
				int num = componentsInChildren.Length;
				while (i < num)
				{
					UIWidget uiwidget = componentsInChildren[i];
					Color color = uiwidget.color;
					color.a = 0f;
					TweenColor.Begin(uiwidget.gameObject, 0.15f, color).method = UITweener.Method.EaseOut;
					i++;
				}
				Collider[] componentsInChildren2 = this.mChild.GetComponentsInChildren<Collider>();
				int j = 0;
				int num2 = componentsInChildren2.Length;
				while (j < num2)
				{
					componentsInChildren2[j].enabled = false;
					j++;
				}
				UnityEngine.Object.Destroy(this.mChild, 0.15f);
			}
			else
			{
				UnityEngine.Object.Destroy(this.mChild);
			}
			this.mBackground = null;
			this.mHighlight = null;
			this.mChild = null;
		}
	}

	// Token: 0x060003CC RID: 972 RVA: 0x000129B8 File Offset: 0x00010BB8
	private void AnimateColor(UIWidget widget)
	{
		Color color = widget.color;
		widget.color = new Color(color.r, color.g, color.b, 0f);
		TweenColor.Begin(widget.gameObject, 0.15f, color).method = UITweener.Method.EaseOut;
	}

	// Token: 0x060003CD RID: 973 RVA: 0x00012A08 File Offset: 0x00010C08
	private void AnimatePosition(UIWidget widget, bool placeAbove, float bottom)
	{
		Vector3 localPosition = widget.cachedTransform.localPosition;
		Vector3 localPosition2 = (!placeAbove) ? new Vector3(localPosition.x, 0f, localPosition.z) : new Vector3(localPosition.x, bottom, localPosition.z);
		widget.cachedTransform.localPosition = localPosition2;
		GameObject gameObject = widget.gameObject;
		TweenPosition.Begin(gameObject, 0.15f, localPosition).method = UITweener.Method.EaseOut;
	}

	// Token: 0x060003CE RID: 974 RVA: 0x00012A80 File Offset: 0x00010C80
	private void AnimateScale(UIWidget widget, bool placeAbove, float bottom)
	{
		GameObject gameObject = widget.gameObject;
		Transform cachedTransform = widget.cachedTransform;
		float num = (float)this.font.size * this.textScale + this.mBgBorder * 2f;
		cachedTransform.localScale = new Vector3(1f, num / (float)widget.height, 1f);
		TweenScale.Begin(gameObject, 0.15f, Vector3.one).method = UITweener.Method.EaseOut;
		if (placeAbove)
		{
			Vector3 localPosition = cachedTransform.localPosition;
			cachedTransform.localPosition = new Vector3(localPosition.x, localPosition.y - (float)widget.height + num, localPosition.z);
			TweenPosition.Begin(gameObject, 0.15f, localPosition).method = UITweener.Method.EaseOut;
		}
	}

	// Token: 0x060003CF RID: 975 RVA: 0x00012B3C File Offset: 0x00010D3C
	private void Animate(UIWidget widget, bool placeAbove, float bottom)
	{
		this.AnimateColor(widget);
		this.AnimatePosition(widget, placeAbove, bottom);
	}

	// Token: 0x060003D0 RID: 976 RVA: 0x00012B50 File Offset: 0x00010D50
	private void OnClick()
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.mChild == null && this.atlas != null && this.font != null && this.items.Count > 0)
		{
			this.mLabelList.Clear();
			this.handleEvents = true;
			if (this.mPanel == null)
			{
				this.mPanel = UIPanel.Find(base.transform, true);
			}
			Transform transform = base.transform;
			Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(transform.parent, transform);
			this.mChild = new GameObject("Drop-down List");
			this.mChild.layer = base.gameObject.layer;
			Transform transform2 = this.mChild.transform;
			transform2.parent = transform.parent;
			transform2.localPosition = bounds.min;
			transform2.localRotation = Quaternion.identity;
			transform2.localScale = Vector3.one;
			this.mBackground = NGUITools.AddSprite(this.mChild, this.atlas, this.backgroundSprite);
			this.mBackground.pivot = UIWidget.Pivot.TopLeft;
			this.mBackground.depth = NGUITools.CalculateNextDepth(this.mPanel.gameObject);
			this.mBackground.color = this.backgroundColor;
			Vector4 border = this.mBackground.border;
			this.mBgBorder = border.y;
			this.mBackground.cachedTransform.localPosition = new Vector3(0f, border.y, 0f);
			this.mHighlight = NGUITools.AddSprite(this.mChild, this.atlas, this.highlightSprite);
			this.mHighlight.pivot = UIWidget.Pivot.TopLeft;
			this.mHighlight.color = this.highlightColor;
			UISpriteData atlasSprite = this.mHighlight.GetAtlasSprite();
			if (atlasSprite == null)
			{
				return;
			}
			float num = (float)atlasSprite.borderTop;
			float num2 = (float)this.font.size * this.font.pixelSize * this.textScale;
			float num3 = 0f;
			float num4 = -this.padding.y;
			List<UILabel> list = new List<UILabel>();
			int i = 0;
			int count = this.items.Count;
			while (i < count)
			{
				string text = this.items[i];
				UILabel uilabel = NGUITools.AddWidget<UILabel>(this.mChild);
				uilabel.pivot = UIWidget.Pivot.TopLeft;
				uilabel.font = this.font;
				uilabel.text = ((!this.isLocalized || !(Localization.instance != null)) ? text : Localization.instance.Get(text));
				uilabel.color = this.textColor;
				uilabel.cachedTransform.localPosition = new Vector3(border.x + this.padding.x, num4, -1f);
				uilabel.overflowMethod = UILabel.Overflow.ResizeFreely;
				uilabel.MakePixelPerfect();
				if (this.textScale != 1f)
				{
					uilabel.cachedTransform.localScale = Vector3.one * this.textScale;
				}
				list.Add(uilabel);
				num4 -= num2;
				num4 -= this.padding.y;
				num3 = Mathf.Max(num3, num2);
				UIEventListener uieventListener = UIEventListener.Get(uilabel.gameObject);
				uieventListener.onHover = new UIEventListener.BoolDelegate(this.OnItemHover);
				uieventListener.onPress = new UIEventListener.BoolDelegate(this.OnItemPress);
				uieventListener.parameter = text;
				if (this.mSelectedItem == text)
				{
					this.Highlight(uilabel, true);
				}
				this.mLabelList.Add(uilabel);
				i++;
			}
			num3 = Mathf.Max(num3, bounds.size.x - (border.x + this.padding.x) * 2f);
			Vector3 center = new Vector3(num3 * 0.5f, -num2 * 0.5f, 0f);
			Vector3 size = new Vector3(num3, num2 + this.padding.y, 1f);
			int j = 0;
			int count2 = list.Count;
			while (j < count2)
			{
				UILabel uilabel2 = list[j];
				BoxCollider boxCollider = NGUITools.AddWidgetCollider(uilabel2.gameObject);
				center.z = boxCollider.center.z;
				boxCollider.center = center;
				boxCollider.size = size;
				j++;
			}
			num3 += (border.x + this.padding.x) * 2f;
			num4 -= border.y;
			this.mBackground.width = Mathf.RoundToInt(num3);
			this.mBackground.height = Mathf.RoundToInt(-num4 + border.y);
			float num5 = 2f * this.atlas.pixelSize;
			float f = num3 - (border.x + this.padding.x) * 2f + (float)atlasSprite.borderLeft * num5;
			float f2 = num2 + num * num5;
			this.mHighlight.width = Mathf.RoundToInt(f);
			this.mHighlight.height = Mathf.RoundToInt(f2);
			bool flag = this.position == UIPopupList.Position.Above;
			if (this.position == UIPopupList.Position.Auto)
			{
				UICamera uicamera = UICamera.FindCameraForLayer(base.gameObject.layer);
				if (uicamera != null)
				{
					flag = (uicamera.cachedCamera.WorldToViewportPoint(transform.position).y < 0.5f);
				}
			}
			if (this.isAnimated)
			{
				float bottom = num4 + num2;
				this.Animate(this.mHighlight, flag, bottom);
				int k = 0;
				int count3 = list.Count;
				while (k < count3)
				{
					this.Animate(list[k], flag, bottom);
					k++;
				}
				this.AnimateColor(this.mBackground);
				this.AnimateScale(this.mBackground, flag, bottom);
			}
			if (flag)
			{
				transform2.localPosition = new Vector3(bounds.min.x, bounds.max.y - num4 - border.y, bounds.min.z);
			}
		}
		else
		{
			this.OnSelect(false);
		}
	}

	// Token: 0x04000296 RID: 662
	private const float animSpeed = 0.15f;

	// Token: 0x04000297 RID: 663
	public static UIPopupList current;

	// Token: 0x04000298 RID: 664
	public UIAtlas atlas;

	// Token: 0x04000299 RID: 665
	public UIFont font;

	// Token: 0x0400029A RID: 666
	public UILabel textLabel;

	// Token: 0x0400029B RID: 667
	public string backgroundSprite;

	// Token: 0x0400029C RID: 668
	public string highlightSprite;

	// Token: 0x0400029D RID: 669
	public UIPopupList.Position position;

	// Token: 0x0400029E RID: 670
	public List<string> items = new List<string>();

	// Token: 0x0400029F RID: 671
	public Vector2 padding = new Vector3(4f, 4f);

	// Token: 0x040002A0 RID: 672
	public float textScale = 1f;

	// Token: 0x040002A1 RID: 673
	public Color textColor = Color.white;

	// Token: 0x040002A2 RID: 674
	public Color backgroundColor = Color.white;

	// Token: 0x040002A3 RID: 675
	public Color highlightColor = new Color(0.59607846f, 1f, 0.2f, 1f);

	// Token: 0x040002A4 RID: 676
	public bool isAnimated = true;

	// Token: 0x040002A5 RID: 677
	public bool isLocalized;

	// Token: 0x040002A6 RID: 678
	public List<EventDelegate> onChange = new List<EventDelegate>();

	// Token: 0x040002A7 RID: 679
	[HideInInspector]
	[SerializeField]
	private string mSelectedItem;

	// Token: 0x040002A8 RID: 680
	private UIPanel mPanel;

	// Token: 0x040002A9 RID: 681
	private GameObject mChild;

	// Token: 0x040002AA RID: 682
	private UISprite mBackground;

	// Token: 0x040002AB RID: 683
	private UISprite mHighlight;

	// Token: 0x040002AC RID: 684
	private UILabel mHighlightedLabel;

	// Token: 0x040002AD RID: 685
	private List<UILabel> mLabelList = new List<UILabel>();

	// Token: 0x040002AE RID: 686
	private float mBgBorder;

	// Token: 0x040002AF RID: 687
	[SerializeField]
	[HideInInspector]
	private GameObject eventReceiver;

	// Token: 0x040002B0 RID: 688
	[HideInInspector]
	[SerializeField]
	private string functionName = "OnSelectionChange";

	// Token: 0x040002B1 RID: 689
	private UIPopupList.LegacyEvent mLegacyEvent;

	// Token: 0x02000093 RID: 147
	public enum Position
	{
		// Token: 0x040002B3 RID: 691
		Auto,
		// Token: 0x040002B4 RID: 692
		Above,
		// Token: 0x040002B5 RID: 693
		Below
	}

	// Token: 0x02000A64 RID: 2660
	// (Invoke) Token: 0x060047CA RID: 18378
	public delegate void LegacyEvent(string val);
}
