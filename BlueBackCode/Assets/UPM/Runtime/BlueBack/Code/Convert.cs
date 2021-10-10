

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * @brief コンバート。
*/


/** BlueBack.Code
*/
namespace BlueBack.Code
{
	/** Convert
	*/
	public static class Convert
	{
		/** 置き換え。
		*/
		public static string ReplaceString(System.Collections.Generic.Dictionary<string,string> a_replace_list,string a_template)
		{
			string t_string = a_template;

			for(int ii=0;ii<16;ii++){
				bool t_change = false;
				foreach(System.Collections.Generic.KeyValuePair<string,string> t_pair in a_replace_list){
					string t_string_new = t_string.Replace(t_pair.Key,t_pair.Value);
					if(t_string != t_string_new){
						t_string = t_string_new;
						t_change = true;
					}
				}
				if(t_change == false){
					break;
				}
			}

			return t_string;
		}

		/** 追加。
		*/
		public static void Add(System.Collections.Generic.List<string> a_out_list,string[] a_template)
		{
			int ii_max = a_template.Length;
			for(int ii=0;ii<ii_max;ii++){
				a_out_list.Add(a_template[ii]);
			}
		}

		/** 追加。
		*/
		public static void Add(System.Collections.Generic.List<string> a_out_list,System.Collections.Generic.List<string> a_template)
		{
			int ii_max = a_template.Count;
			for(int ii=0;ii<ii_max;ii++){
				a_out_list.Add(a_template[ii]);
			}
		}

		/** 複製。

			「a_template」を「a_count」の数だけ複製、「a_index_key」をインデックスに置換。

		*/
		public static void Duplicate(System.Collections.Generic.List<string> a_out_list,string a_index_key,int a_count,string[] a_template)
		{
			for(int ii=0;ii<a_count;ii++){
				string ii_string = ii.ToString();
				int jj_max = a_template.Length;
				for(int jj=0;jj<jj_max;jj++){
					a_out_list.Add(a_template[jj].Replace(a_index_key,ii_string));
				}
			}
		}

		/** 複製。

			「a_template」を「a_count」の数だけ複製、「a_index_key」をインデックスに置換。

		*/
		public static void Duplicate(System.Collections.Generic.List<string> a_out_list,string a_index_key,int a_count,System.Collections.Generic.List<string> a_template)
		{
			for(int ii=0;ii<a_count;ii++){
				string ii_string = ii.ToString();
				int jj_max = a_template.Count;
				for(int jj=0;jj<jj_max;jj++){
					a_out_list.Add(a_template[jj].Replace(a_index_key,ii_string));
				}
			}
		}

		/** 置き換え。
		*/
		public static void Replace(System.Text.StringBuilder a_stringbuilder,System.Collections.Generic.Dictionary<string,string> a_replace_list,string[] a_template)
		{
			int ii_max = a_template.Length;
			for(int ii=0;ii<ii_max;ii++){
				a_stringbuilder.Append(ReplaceString(a_replace_list,a_template[ii]));
				a_stringbuilder.Append("\n");
			}
		}

		/** 置き換え。
		*/
		public static void Replace(System.Text.StringBuilder a_stringbuilder,System.Collections.Generic.Dictionary<string,string> a_replace_list,System.Collections.Generic.List<string> a_template)
		{
			int ii_max = a_template.Count;
			for(int ii=0;ii<ii_max;ii++){
				a_stringbuilder.Append(ReplaceString(a_replace_list,a_template[ii]));
				a_stringbuilder.Append("\n");
			}
		}
	}
}

