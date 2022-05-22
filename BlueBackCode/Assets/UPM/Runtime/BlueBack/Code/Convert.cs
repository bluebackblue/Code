

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief コンバート。
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

			string --> replacelist --> string

			a_replace_list	: 置換リスト。
			a_template		: 元データ。

		*/
		public static string Replace(System.Collections.Generic.Dictionary<string,string> a_replace_list,string a_template)
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

			string[] --> Add --> List<string>

			a_out_list		: 追加先。
			a_replace_list	: 置換リスト。
			a_template		: 元データ。

		*/
		public static void Add(System.Collections.Generic.List<string> a_out_list,System.Collections.Generic.Dictionary<string,string> a_replace_list,string[] a_template)
		{
			int ii_max = a_template.Length;

			if(a_replace_list != null){
				for(int ii=0;ii<ii_max;ii++){
					string t_temp = a_template[ii];
					t_temp = Replace(a_replace_list,t_temp);
					a_out_list.Add(t_temp);
				}
			}else{
				for(int ii=0;ii<ii_max;ii++){
					string t_temp = a_template[ii];
					a_out_list.Add(t_temp);
				}
			}
		}

		/** 追加。

			List<string> --> Add --> List<string>

			a_out_list		: 追加先。
			a_replace_list	: 置換リスト。
			a_template		: 元データ。

		*/
		public static void Add(System.Collections.Generic.List<string> a_out_list,System.Collections.Generic.Dictionary<string,string> a_replace_list,System.Collections.Generic.List<string> a_template)
		{
			int ii_max = a_template.Count;
			if(a_replace_list != null){
				for(int ii=0;ii<ii_max;ii++){
					string t_temp = a_template[ii];
					t_temp = Replace(a_replace_list,t_temp);
					a_out_list.Add(t_temp);
				}
			}else{
				for(int ii=0;ii<ii_max;ii++){
					string t_temp = a_template[ii];
					a_out_list.Add(t_temp);
				}
			}
		}

		/** 複製。

			「a_template」を「a_count」の数だけ複製、「a_index_key」をインデックスに置換。

			a_out_list		: 追加先。
			a_index_key		: 例 : <<INDEX>>
			a_index_count	: 総数。
			a_index_offset	: 開始インデックス。
			a_replace_list	: 置換リスト。
			a_template		: 元データ。

		*/
		public static void Duplicate(System.Collections.Generic.List<string> a_out_list,string a_index_key,int a_index_count,int a_index_offset,System.Collections.Generic.Dictionary<string,string> a_replace_list,string[] a_template)
		{
			int ii_max = a_index_count + a_index_offset;
			if(a_replace_list != null){
				for(int ii=a_index_offset;ii<ii_max;ii++){
					string ii_string = ii.ToString();
					int jj_max = a_template.Length;
					for(int jj=0;jj<jj_max;jj++){
						string t_temp = a_template[jj].Replace(a_index_key,ii_string);
						t_temp = Replace(a_replace_list,t_temp);
						a_out_list.Add(t_temp);
					}
				}
			}else{
				for(int ii=a_index_offset;ii<ii_max;ii++){
					string ii_string = ii.ToString();
					int jj_max = a_template.Length;
					for(int jj=0;jj<jj_max;jj++){
						string t_temp = a_template[jj].Replace(a_index_key,ii_string);
						a_out_list.Add(t_temp);
					}
				}
			}
		}

		/** 複製。

			「a_template」を「a_count」の数だけ複製、「a_index_key」をインデックスに置換。

			a_out_list		: 追加先。
			a_template		: 元データ。

			a_index_key		: 例 : <<INDEX>>
			a_index_count	: 総数。
			a_index_offset	: 開始インデックス。

		*/
		public static void Duplicate(System.Collections.Generic.List<string> a_out_list,string a_index_key,int a_index_count,int a_index_offset,System.Collections.Generic.Dictionary<string,string> a_replace_list,System.Collections.Generic.List<string> a_template)
		{
			int ii_max = a_index_count + a_index_offset;
			if(a_replace_list != null){
				for(int ii=a_index_offset;ii<ii_max;ii++){
					string ii_string = ii.ToString();
					int jj_max = a_template.Count;
					for(int jj=0;jj<jj_max;jj++){
						string t_temp = a_template[jj].Replace(a_index_key,ii_string);
						t_temp = Replace(a_replace_list,t_temp);
						a_out_list.Add(t_temp);
					}
				}
			}else{
				for(int ii=a_index_offset;ii<ii_max;ii++){
					string ii_string = ii.ToString();
					int jj_max = a_template.Count;
					for(int jj=0;jj<jj_max;jj++){
						string t_temp = a_template[jj].Replace(a_index_key,ii_string);
						a_out_list.Add(t_temp);
					}
				}
			}
		}

		/** 追加。

			string[] --> Append --> StringBuilder

			a_out_stringbuilder		: 追加先。
			a_replace_list			: 置換リスト。
			a_template				: 元データ。

		*/
		public static void Add(System.Text.StringBuilder a_out_stringbuilder,System.Collections.Generic.Dictionary<string,string> a_replace_list,string[] a_template)
		{
			int ii_max = a_template.Length;
			if(a_replace_list != null){
				for(int ii=0;ii<ii_max;ii++){
					string t_temp = a_template[ii];
					t_temp = Convert.Replace(a_replace_list,t_temp);
					a_out_stringbuilder.Append(t_temp);
					a_out_stringbuilder.Append("\n");
				}
			}else{
				for(int ii=0;ii<ii_max;ii++){
					string t_temp = a_template[ii];
					a_out_stringbuilder.Append(t_temp);
					a_out_stringbuilder.Append("\n");
				}
			}
		}

		/** 追加。StringBuilder。

			List<string> --> Append --> StringBuilder

			a_out_stringbuilder		: 追加先。
			a_replace_list			: 置換リスト。
			a_template				: 元データ。

		*/
		public static void Add(System.Text.StringBuilder a_out_stringbuilder,System.Collections.Generic.Dictionary<string,string> a_replace_list,System.Collections.Generic.List<string> a_template)
		{
			int ii_max = a_template.Count;
			if(a_replace_list != null){
				for(int ii=0;ii<ii_max;ii++){
					string t_temp = a_template[ii];
					t_temp = Convert.Replace(a_replace_list,t_temp);
					a_out_stringbuilder.Append(t_temp);
					a_out_stringbuilder.Append("\n");
				}
			}else{
				for(int ii=0;ii<ii_max;ii++){
					string t_temp = a_template[ii];
					a_out_stringbuilder.Append(t_temp);
					a_out_stringbuilder.Append("\n");
				}
			}
		}
	}
}

