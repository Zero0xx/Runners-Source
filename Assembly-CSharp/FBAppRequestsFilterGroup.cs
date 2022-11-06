using System;
using System.Collections.Generic;

// Token: 0x0200000E RID: 14
public sealed class FBAppRequestsFilterGroup : Dictionary<string, object>
{
	// Token: 0x06000073 RID: 115 RVA: 0x00003A5C File Offset: 0x00001C5C
	public FBAppRequestsFilterGroup(string name, List<string> user_ids)
	{
		this["name"] = name;
		this["user_ids"] = user_ids;
	}
}
