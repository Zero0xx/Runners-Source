using System;
using UnityEngine;

// Token: 0x02000348 RID: 840
public class CameraFade : MonoBehaviour
{
	// Token: 0x170003C3 RID: 963
	// (get) Token: 0x060018F0 RID: 6384 RVA: 0x0008FEB8 File Offset: 0x0008E0B8
	private static CameraFade instance
	{
		get
		{
			if (CameraFade.mInstance == null)
			{
				GameObject gameObject = new GameObject("CameraFade");
				if (gameObject != null)
				{
					UnityEngine.Object.DontDestroyOnLoad(gameObject);
					CameraFade.mInstance = gameObject.AddComponent<CameraFade>();
				}
			}
			return CameraFade.mInstance;
		}
	}

	// Token: 0x060018F1 RID: 6385 RVA: 0x0008FF04 File Offset: 0x0008E104
	private void Awake()
	{
		if (CameraFade.mInstance == null)
		{
			CameraFade.mInstance = this;
			CameraFade.instance.init();
		}
	}

	// Token: 0x060018F2 RID: 6386 RVA: 0x0008FF34 File Offset: 0x0008E134
	public void init()
	{
		CameraFade.instance.m_FadeTexture = new Texture2D(1, 1);
		CameraFade.instance.m_BackgroundStyle.normal.background = CameraFade.instance.m_FadeTexture;
	}

	// Token: 0x060018F3 RID: 6387 RVA: 0x0008FF68 File Offset: 0x0008E168
	private void OnGUI()
	{
		if (Time.time > CameraFade.instance.m_FadeDelay && CameraFade.instance.m_CurrentScreenOverlayColor != CameraFade.instance.m_TargetScreenOverlayColor)
		{
			if (Mathf.Abs(CameraFade.instance.m_CurrentScreenOverlayColor.a - CameraFade.instance.m_TargetScreenOverlayColor.a) < Mathf.Abs(CameraFade.instance.m_DeltaColor.a) * Time.deltaTime)
			{
				CameraFade.instance.m_CurrentScreenOverlayColor = CameraFade.instance.m_TargetScreenOverlayColor;
				CameraFade.SetScreenOverlayColor(CameraFade.instance.m_CurrentScreenOverlayColor);
				CameraFade.instance.m_DeltaColor = new Color(0f, 0f, 0f, 0f);
				if (CameraFade.instance.m_OnFadeFinish != null)
				{
					CameraFade.instance.m_OnFadeFinish();
				}
			}
			else
			{
				CameraFade.SetScreenOverlayColor(CameraFade.instance.m_CurrentScreenOverlayColor + CameraFade.instance.m_DeltaColor * Time.deltaTime);
			}
		}
		if (this.m_CurrentScreenOverlayColor.a > 0f)
		{
			GUI.depth = CameraFade.instance.m_FadeGUIDepth;
			GUI.Label(new Rect(-20f, -20f, (float)(Screen.width + 20), (float)(Screen.height + 20)), CameraFade.instance.m_FadeTexture, CameraFade.instance.m_BackgroundStyle);
		}
	}

	// Token: 0x060018F4 RID: 6388 RVA: 0x000900DC File Offset: 0x0008E2DC
	private static void SetScreenOverlayColor(Color newScreenOverlayColor)
	{
		CameraFade.instance.m_CurrentScreenOverlayColor = newScreenOverlayColor;
		CameraFade.instance.m_FadeTexture.SetPixel(0, 0, CameraFade.instance.m_CurrentScreenOverlayColor);
		CameraFade.instance.m_FadeTexture.Apply();
	}

	// Token: 0x060018F5 RID: 6389 RVA: 0x00090114 File Offset: 0x0008E314
	public static void StartAlphaFade(Color newScreenOverlayColor, bool isFadeIn, float fadeDuration)
	{
		if (fadeDuration <= 0f)
		{
			CameraFade.SetScreenOverlayColor(newScreenOverlayColor);
		}
		else
		{
			if (isFadeIn)
			{
				CameraFade.instance.m_TargetScreenOverlayColor = new Color(newScreenOverlayColor.r, newScreenOverlayColor.g, newScreenOverlayColor.b, 0f);
				CameraFade.SetScreenOverlayColor(newScreenOverlayColor);
			}
			else
			{
				CameraFade.instance.m_TargetScreenOverlayColor = newScreenOverlayColor;
				CameraFade.SetScreenOverlayColor(new Color(newScreenOverlayColor.r, newScreenOverlayColor.g, newScreenOverlayColor.b, 0f));
			}
			CameraFade.instance.m_DeltaColor = (CameraFade.instance.m_TargetScreenOverlayColor - CameraFade.instance.m_CurrentScreenOverlayColor) / fadeDuration;
		}
	}

	// Token: 0x060018F6 RID: 6390 RVA: 0x000901CC File Offset: 0x0008E3CC
	public static void StartAlphaFade(Color nowScreenOverlayColor, Color newScreenOverlayColor, bool isFadeIn, float fadeDuration)
	{
		if (fadeDuration <= 0f)
		{
			CameraFade.SetScreenOverlayColor(newScreenOverlayColor);
		}
		else
		{
			if (isFadeIn)
			{
				CameraFade.instance.m_TargetScreenOverlayColor = new Color(newScreenOverlayColor.r, newScreenOverlayColor.g, newScreenOverlayColor.b, 0f);
				CameraFade.SetScreenOverlayColor(nowScreenOverlayColor);
			}
			else
			{
				CameraFade.instance.m_TargetScreenOverlayColor = newScreenOverlayColor;
				CameraFade.SetScreenOverlayColor(new Color(nowScreenOverlayColor.r, nowScreenOverlayColor.g, nowScreenOverlayColor.b, 0f));
			}
			CameraFade.instance.m_DeltaColor = (CameraFade.instance.m_TargetScreenOverlayColor - CameraFade.instance.m_CurrentScreenOverlayColor) / fadeDuration;
		}
	}

	// Token: 0x060018F7 RID: 6391 RVA: 0x00090284 File Offset: 0x0008E484
	public static void StartAlphaFade(Color newScreenOverlayColor, bool isFadeIn, float fadeDuration, float fadeDelay)
	{
		if (fadeDuration <= 0f)
		{
			CameraFade.SetScreenOverlayColor(newScreenOverlayColor);
		}
		else
		{
			CameraFade.instance.m_FadeDelay = Time.time + fadeDelay;
			if (isFadeIn)
			{
				CameraFade.instance.m_TargetScreenOverlayColor = new Color(newScreenOverlayColor.r, newScreenOverlayColor.g, newScreenOverlayColor.b, 0f);
				CameraFade.SetScreenOverlayColor(newScreenOverlayColor);
			}
			else
			{
				CameraFade.instance.m_TargetScreenOverlayColor = newScreenOverlayColor;
				CameraFade.SetScreenOverlayColor(new Color(newScreenOverlayColor.r, newScreenOverlayColor.g, newScreenOverlayColor.b, 0f));
			}
			CameraFade.instance.m_DeltaColor = (CameraFade.instance.m_TargetScreenOverlayColor - CameraFade.instance.m_CurrentScreenOverlayColor) / fadeDuration;
		}
	}

	// Token: 0x060018F8 RID: 6392 RVA: 0x0009034C File Offset: 0x0008E54C
	public static void StartAlphaFade(Color newScreenOverlayColor, bool isFadeIn, float fadeDuration, float fadeDelay, Action OnFadeFinish)
	{
		if (fadeDuration <= 0f)
		{
			CameraFade.SetScreenOverlayColor(newScreenOverlayColor);
		}
		else
		{
			CameraFade.instance.m_OnFadeFinish = OnFadeFinish;
			CameraFade.instance.m_FadeDelay = Time.time + fadeDelay;
			if (isFadeIn)
			{
				CameraFade.instance.m_TargetScreenOverlayColor = new Color(newScreenOverlayColor.r, newScreenOverlayColor.g, newScreenOverlayColor.b, 0f);
				CameraFade.SetScreenOverlayColor(newScreenOverlayColor);
			}
			else
			{
				CameraFade.instance.m_TargetScreenOverlayColor = newScreenOverlayColor;
				CameraFade.SetScreenOverlayColor(new Color(newScreenOverlayColor.r, newScreenOverlayColor.g, newScreenOverlayColor.b, 0f));
			}
			CameraFade.instance.m_DeltaColor = (CameraFade.instance.m_TargetScreenOverlayColor - CameraFade.instance.m_CurrentScreenOverlayColor) / fadeDuration;
		}
	}

	// Token: 0x060018F9 RID: 6393 RVA: 0x00090420 File Offset: 0x0008E620
	private void OnApplicationQuit()
	{
		CameraFade.mInstance = null;
	}

	// Token: 0x0400164E RID: 5710
	private static CameraFade mInstance;

	// Token: 0x0400164F RID: 5711
	public GUIStyle m_BackgroundStyle = new GUIStyle();

	// Token: 0x04001650 RID: 5712
	public Texture2D m_FadeTexture;

	// Token: 0x04001651 RID: 5713
	public Color m_CurrentScreenOverlayColor = new Color(0f, 0f, 0f, 0f);

	// Token: 0x04001652 RID: 5714
	public Color m_TargetScreenOverlayColor = new Color(0f, 0f, 0f, 0f);

	// Token: 0x04001653 RID: 5715
	public Color m_DeltaColor = new Color(0f, 0f, 0f, 0f);

	// Token: 0x04001654 RID: 5716
	public int m_FadeGUIDepth = -1000;

	// Token: 0x04001655 RID: 5717
	public float m_FadeDelay;

	// Token: 0x04001656 RID: 5718
	public Action m_OnFadeFinish;
}
