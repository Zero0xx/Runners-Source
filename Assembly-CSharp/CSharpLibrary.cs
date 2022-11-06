using System;
using System.Runtime.InteropServices;

// Token: 0x02000A30 RID: 2608
public class CSharpLibrary
{
	// Token: 0x0600453A RID: 17722
	[DllImport("CppLibrary")]
	private static extern float TestMultiply(float a, float b);

	// Token: 0x0600453B RID: 17723
	[DllImport("CppLibrary")]
	private static extern float TestDivide(float a, float b);

	// Token: 0x0600453C RID: 17724 RVA: 0x00163CBC File Offset: 0x00161EBC
	public static float CppMultiply(float a, float b)
	{
		return CSharpLibrary.TestMultiply(a, b);
	}

	// Token: 0x0600453D RID: 17725 RVA: 0x00163CC8 File Offset: 0x00161EC8
	public static float CppDivide(float a, float b)
	{
		return CSharpLibrary.TestDivide(a, b);
	}
}
