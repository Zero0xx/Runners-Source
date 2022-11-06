using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace LitJson
{
	// Token: 0x020003E8 RID: 1000
	public class JsonWriter
	{
		// Token: 0x06001D6F RID: 7535 RVA: 0x000ACE44 File Offset: 0x000AB044
		public JsonWriter()
		{
			this.inst_string_builder = new StringBuilder();
			this.writer = new StringWriter(this.inst_string_builder);
			this.Init();
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x000ACE7C File Offset: 0x000AB07C
		public JsonWriter(StringBuilder sb) : this(new StringWriter(sb))
		{
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x000ACE8C File Offset: 0x000AB08C
		public JsonWriter(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.writer = writer;
			this.Init();
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001D73 RID: 7539 RVA: 0x000ACECC File Offset: 0x000AB0CC
		// (set) Token: 0x06001D74 RID: 7540 RVA: 0x000ACED4 File Offset: 0x000AB0D4
		public int IndentValue
		{
			get
			{
				return this.indent_value;
			}
			set
			{
				this.indentation = this.indentation / this.indent_value * value;
				this.indent_value = value;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001D75 RID: 7541 RVA: 0x000ACEF4 File Offset: 0x000AB0F4
		// (set) Token: 0x06001D76 RID: 7542 RVA: 0x000ACEFC File Offset: 0x000AB0FC
		public bool PrettyPrint
		{
			get
			{
				return this.pretty_print;
			}
			set
			{
				this.pretty_print = value;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001D77 RID: 7543 RVA: 0x000ACF08 File Offset: 0x000AB108
		public TextWriter TextWriter
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001D78 RID: 7544 RVA: 0x000ACF10 File Offset: 0x000AB110
		// (set) Token: 0x06001D79 RID: 7545 RVA: 0x000ACF18 File Offset: 0x000AB118
		public bool Validate
		{
			get
			{
				return this.validate;
			}
			set
			{
				this.validate = value;
			}
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x000ACF24 File Offset: 0x000AB124
		private void DoValidation(Condition cond)
		{
			if (!this.context.ExpectingValue)
			{
				this.context.Count++;
			}
			if (!this.validate)
			{
				return;
			}
			if (this.has_reached_end)
			{
				throw new JsonException("A complete JSON symbol has already been written");
			}
			switch (cond)
			{
			case Condition.InArray:
				if (!this.context.InArray)
				{
					throw new JsonException("Can't close an array here");
				}
				break;
			case Condition.InObject:
				if (!this.context.InObject || this.context.ExpectingValue)
				{
					throw new JsonException("Can't close an object here");
				}
				break;
			case Condition.NotAProperty:
				if (this.context.InObject && !this.context.ExpectingValue)
				{
					throw new JsonException("Expected a property");
				}
				break;
			case Condition.Property:
				if (!this.context.InObject || this.context.ExpectingValue)
				{
					throw new JsonException("Can't add a property here");
				}
				break;
			case Condition.Value:
				if (!this.context.InArray && (!this.context.InObject || !this.context.ExpectingValue))
				{
					throw new JsonException("Can't add a value here");
				}
				break;
			}
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x000AD088 File Offset: 0x000AB288
		private void Init()
		{
			this.has_reached_end = false;
			this.hex_seq = new char[4];
			this.indentation = 0;
			this.indent_value = 4;
			this.pretty_print = false;
			this.validate = true;
			this.ctx_stack = new Stack<WriterContext>();
			this.context = new WriterContext();
			this.ctx_stack.Push(this.context);
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x000AD0EC File Offset: 0x000AB2EC
		private static void IntToHex(int n, char[] hex)
		{
			for (int i = 0; i < 4; i++)
			{
				int num = n % 16;
				if (num < 10)
				{
					hex[3 - i] = (char)(48 + num);
				}
				else
				{
					hex[3 - i] = (char)(65 + (num - 10));
				}
				n >>= 4;
			}
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x000AD13C File Offset: 0x000AB33C
		private void Indent()
		{
			if (this.pretty_print)
			{
				this.indentation += this.indent_value;
			}
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x000AD15C File Offset: 0x000AB35C
		private void Put(string str)
		{
			if (this.pretty_print && !this.context.ExpectingValue)
			{
				for (int i = 0; i < this.indentation; i++)
				{
					this.writer.Write(' ');
				}
			}
			this.writer.Write(str);
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x000AD1B4 File Offset: 0x000AB3B4
		private void PutNewline()
		{
			this.PutNewline(true);
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x000AD1C0 File Offset: 0x000AB3C0
		private void PutNewline(bool add_comma)
		{
			if (add_comma && !this.context.ExpectingValue && this.context.Count > 1)
			{
				this.writer.Write(',');
			}
			if (this.pretty_print && !this.context.ExpectingValue)
			{
				this.writer.Write('\n');
			}
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x000AD22C File Offset: 0x000AB42C
		private void PutString(string str)
		{
			this.Put(string.Empty);
			this.writer.Write('"');
			int length = str.Length;
			for (int i = 0; i < length; i++)
			{
				char c = str[i];
				switch (c)
				{
				case '\b':
					this.writer.Write("\\b");
					break;
				case '\t':
					this.writer.Write("\\t");
					break;
				case '\n':
					this.writer.Write("\\n");
					break;
				default:
					if (c != '"' && c != '\\')
					{
						if (str[i] >= ' ' && str[i] <= '~')
						{
							this.writer.Write(str[i]);
						}
						else
						{
							JsonWriter.IntToHex((int)str[i], this.hex_seq);
							this.writer.Write("\\u");
							this.writer.Write(this.hex_seq);
						}
					}
					else
					{
						this.writer.Write('\\');
						this.writer.Write(str[i]);
					}
					break;
				case '\f':
					this.writer.Write("\\f");
					break;
				case '\r':
					this.writer.Write("\\r");
					break;
				}
			}
			this.writer.Write('"');
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x000AD3A8 File Offset: 0x000AB5A8
		private void Unindent()
		{
			if (this.pretty_print)
			{
				this.indentation -= this.indent_value;
			}
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x000AD3C8 File Offset: 0x000AB5C8
		public override string ToString()
		{
			if (this.inst_string_builder == null)
			{
				return string.Empty;
			}
			return this.inst_string_builder.ToString();
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x000AD3E8 File Offset: 0x000AB5E8
		public void Reset()
		{
			this.has_reached_end = false;
			this.ctx_stack.Clear();
			this.context = new WriterContext();
			this.ctx_stack.Push(this.context);
			if (this.inst_string_builder != null)
			{
				this.inst_string_builder.Remove(0, this.inst_string_builder.Length);
			}
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x000AD448 File Offset: 0x000AB648
		public void Write(bool boolean)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			this.Put((!boolean) ? "false" : "true");
			this.context.ExpectingValue = false;
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x000AD48C File Offset: 0x000AB68C
		public void Write(decimal number)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			this.Put(Convert.ToString(number, JsonWriter.number_format));
			this.context.ExpectingValue = false;
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x000AD4C4 File Offset: 0x000AB6C4
		public void Write(double number)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			string text = Convert.ToString(number, JsonWriter.number_format);
			this.Put(text);
			if (text.IndexOf('.') == -1 && text.IndexOf('E') == -1)
			{
				this.writer.Write(".0");
			}
			this.context.ExpectingValue = false;
		}

		// Token: 0x06001D88 RID: 7560 RVA: 0x000AD52C File Offset: 0x000AB72C
		public void Write(int number)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			this.Put(Convert.ToString(number, JsonWriter.number_format));
			this.context.ExpectingValue = false;
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x000AD564 File Offset: 0x000AB764
		public void Write(long number)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			this.Put(Convert.ToString(number, JsonWriter.number_format));
			this.context.ExpectingValue = false;
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x000AD59C File Offset: 0x000AB79C
		public void Write(string str)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			if (str == null)
			{
				this.Put("null");
			}
			else
			{
				this.PutString(str);
			}
			this.context.ExpectingValue = false;
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x000AD5E0 File Offset: 0x000AB7E0
		public void Write(ulong number)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			this.Put(Convert.ToString(number, JsonWriter.number_format));
			this.context.ExpectingValue = false;
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x000AD618 File Offset: 0x000AB818
		public void WriteArrayEnd()
		{
			this.DoValidation(Condition.InArray);
			this.PutNewline(false);
			this.ctx_stack.Pop();
			if (this.ctx_stack.Count == 1)
			{
				this.has_reached_end = true;
			}
			else
			{
				this.context = this.ctx_stack.Peek();
				this.context.ExpectingValue = false;
			}
			this.Unindent();
			this.Put("]");
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x000AD68C File Offset: 0x000AB88C
		public void WriteArrayStart()
		{
			this.DoValidation(Condition.NotAProperty);
			this.PutNewline();
			this.Put("[");
			this.context = new WriterContext();
			this.context.InArray = true;
			this.ctx_stack.Push(this.context);
			this.Indent();
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x000AD6E0 File Offset: 0x000AB8E0
		public void WriteObjectEnd()
		{
			this.DoValidation(Condition.InObject);
			this.PutNewline(false);
			this.ctx_stack.Pop();
			if (this.ctx_stack.Count == 1)
			{
				this.has_reached_end = true;
			}
			else
			{
				this.context = this.ctx_stack.Peek();
				this.context.ExpectingValue = false;
			}
			this.Unindent();
			this.Put("}");
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x000AD754 File Offset: 0x000AB954
		public void WriteObjectStart()
		{
			this.DoValidation(Condition.NotAProperty);
			this.PutNewline();
			this.Put("{");
			this.context = new WriterContext();
			this.context.InObject = true;
			this.ctx_stack.Push(this.context);
			this.Indent();
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x000AD7A8 File Offset: 0x000AB9A8
		public void WritePropertyName(string property_name)
		{
			this.DoValidation(Condition.Property);
			this.PutNewline();
			this.PutString(property_name);
			if (this.pretty_print)
			{
				if (property_name.Length > this.context.Padding)
				{
					this.context.Padding = property_name.Length;
				}
				for (int i = this.context.Padding - property_name.Length; i >= 0; i--)
				{
					this.writer.Write(' ');
				}
				this.writer.Write(": ");
			}
			else
			{
				this.writer.Write(':');
			}
			this.context.ExpectingValue = true;
		}

		// Token: 0x04001AE0 RID: 6880
		private static NumberFormatInfo number_format = NumberFormatInfo.InvariantInfo;

		// Token: 0x04001AE1 RID: 6881
		private WriterContext context;

		// Token: 0x04001AE2 RID: 6882
		private Stack<WriterContext> ctx_stack;

		// Token: 0x04001AE3 RID: 6883
		private bool has_reached_end;

		// Token: 0x04001AE4 RID: 6884
		private char[] hex_seq;

		// Token: 0x04001AE5 RID: 6885
		private int indentation;

		// Token: 0x04001AE6 RID: 6886
		private int indent_value;

		// Token: 0x04001AE7 RID: 6887
		private StringBuilder inst_string_builder;

		// Token: 0x04001AE8 RID: 6888
		private bool pretty_print;

		// Token: 0x04001AE9 RID: 6889
		private bool validate;

		// Token: 0x04001AEA RID: 6890
		private TextWriter writer;
	}
}
