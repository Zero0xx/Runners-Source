using System;

// Token: 0x020007C2 RID: 1986
public class URLRequestParam
{
	// Token: 0x0600344F RID: 13391 RVA: 0x0011CF68 File Offset: 0x0011B168
	public URLRequestParam(string propertyName, string value)
	{
		this.mPropertyName = propertyName;
		this.mValue = value;
	}

	// Token: 0x17000722 RID: 1826
	// (get) Token: 0x06003450 RID: 13392 RVA: 0x0011CF80 File Offset: 0x0011B180
	public string propertyName
	{
		get
		{
			return this.mPropertyName;
		}
	}

	// Token: 0x17000723 RID: 1827
	// (get) Token: 0x06003451 RID: 13393 RVA: 0x0011CF88 File Offset: 0x0011B188
	public string value
	{
		get
		{
			return this.mValue;
		}
	}

	// Token: 0x04002C12 RID: 11282
	private string mPropertyName;

	// Token: 0x04002C13 RID: 11283
	private string mValue;
}
