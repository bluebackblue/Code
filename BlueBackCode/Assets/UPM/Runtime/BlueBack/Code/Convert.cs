

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief コンバート。
*/


/** BlueBack.Code
*/
namespace BlueBack.Code
{
	/** IndexType
	*/
	using IndexType = System.Int32;

	/** Convert
	*/
	public static class Convert
	{
		/** 置き換え。

			string --> replacelist --> string

			a_replace_list			: 置換リスト。
			a_data					: データ。
			a_loop					: ループ回数。

		*/
		public static string Replace(System.Collections.Generic.Dictionary<string,string> a_replace_list,string a_data,int a_loop)
		{
			string t_data = a_data;

			for(int ii=0;ii<a_loop;ii++){
				bool t_change = false;
				foreach(System.Collections.Generic.KeyValuePair<string,string> t_pair in a_replace_list){
					string t_string_new = t_data.Replace(t_pair.Key,t_pair.Value);
					if(t_data != t_string_new){
						t_data = t_string_new;
						t_change = true;
					}
				}
				if(t_change == false){
					break;
				}
			}

			return t_data;
		}

		/** 置き換え。

			a_replace_list			: 置換リスト。
			a_data					: データ。

		*/
		public static void Replace(System.Collections.Generic.Dictionary<string,string> a_replace_list,string[] a_data)
		{
			int ii_max = a_data.Length;
			for(int ii=0;ii<ii_max;ii++){
				a_data[ii] = Replace(a_replace_list,a_data[ii],16);
			}
		}

		/** 置き換え。

			a_replace_list			: 置換リスト。
			a_data					: データ。

		*/
		public static void Replace(System.Collections.Generic.Dictionary<string,string> a_replace_list,System.Collections.Generic.List<string> a_data)
		{
			int ii_max = a_data.Count;
			for(int ii=0;ii<ii_max;ii++){
				a_data[ii] = Replace(a_replace_list,a_data[ii],16);
			}
		}

		/** 追加。

			string[] --> Add --> List<string>

			a_out_list				: 追加先。
			a_replace_list			: 置換リスト。
			a_data					: データ。

		*/
		public static void Add(System.Collections.Generic.List<string> a_out_list,System.Collections.Generic.Dictionary<string,string> a_replace_list,string[] a_data)
		{
			int ii_max = a_data.Length;
			if(a_replace_list != null){
				for(int ii=0;ii<ii_max;ii++){
					string t_temp = a_data[ii];
					t_temp = Replace(a_replace_list,t_temp,16);
					a_out_list.Add(t_temp);
				}
			}else{
				for(int ii=0;ii<ii_max;ii++){
					string t_temp = a_data[ii];
					a_out_list.Add(t_temp);
				}
			}
		}

		/** 追加。

			List<string> --> Add --> List<string>

			a_out_list				: 追加先。
			a_replace_list			: 置換リスト。
			a_data					: データ。

		*/
		public static void Add(System.Collections.Generic.List<string> a_out_list,System.Collections.Generic.Dictionary<string,string> a_replace_list,System.Collections.Generic.List<string> a_data)
		{
			int ii_max = a_data.Count;
			if(a_replace_list != null){
				for(int ii=0;ii<ii_max;ii++){
					string t_temp = a_data[ii];
					t_temp = Replace(a_replace_list,t_temp,16);
					a_out_list.Add(t_temp);
				}
			}else{
				for(int ii=0;ii<ii_max;ii++){
					string t_temp = a_data[ii];
					a_out_list.Add(t_temp);
				}
			}
		}

		/** 複製。

			「a_data」を「a_count」の数だけ複製、「a_index_key」をインデックスに置換。

			a_out_list				: 追加先。
			a_index_key				: 例 : <<INDEX>>
			a_index_count			: 総数。
			a_index_offset			: 開始インデックス。
			a_replace_list			: 置換リスト。
			a_data					: データ。

		*/
		public static void Duplicate(System.Collections.Generic.List<string> a_out_list,string a_index_key,int a_index_count,int a_index_offset,System.Collections.Generic.Dictionary<string,string> a_replace_list,string[] a_data)
		{
			int ii_max = a_index_count + a_index_offset;
			if(a_replace_list != null){
				for(int ii=a_index_offset;ii<ii_max;ii++){
					string ii_string = ii.ToString();
					int jj_max = a_data.Length;
					for(int jj=0;jj<jj_max;jj++){
						string t_temp = a_data[jj].Replace(a_index_key,ii_string);
						t_temp = Replace(a_replace_list,t_temp,16);
						a_out_list.Add(t_temp);
					}
				}
			}else{
				for(int ii=a_index_offset;ii<ii_max;ii++){
					string ii_string = ii.ToString();
					int jj_max = a_data.Length;
					for(int jj=0;jj<jj_max;jj++){
						string t_temp = a_data[jj].Replace(a_index_key,ii_string);
						a_out_list.Add(t_temp);
					}
				}
			}
		}

		/** 複製。

			「a_data」を「a_count」の数だけ複製、「a_index_key」をインデックスに置換。

			a_out_list				: 追加先。
			a_index_key				: 例 : <<INDEX>>
			a_index_count			: 総数。
			a_index_offset			: 開始インデックス。
			a_replace_list			: 置換リスト。
			a_data_creator			: データ作成。

		*/
		public static void Duplicate(System.Collections.Generic.List<string> a_out_list,string a_index_key,int a_index_count,int a_index_offset,System.Collections.Generic.Dictionary<string,string> a_replace_list,System.Func<IndexType,string[]> a_data_creator)
		{
			int ii_max = a_index_count + a_index_offset;
			if(a_replace_list != null){
				for(int ii=a_index_offset;ii<ii_max;ii++){
					string ii_string = ii.ToString();
					string[] t_template = a_data_creator(ii);
					int jj_max = t_template.Length;
					for(int jj=0;jj<jj_max;jj++){
						string t_temp = t_template[jj].Replace(a_index_key,ii_string);
						t_temp = Replace(a_replace_list,t_temp,16);
						a_out_list.Add(t_temp);
					}
				}
			}else{
				for(int ii=a_index_offset;ii<ii_max;ii++){
					string ii_string = ii.ToString();
					string[] t_template = a_data_creator(ii);
					int jj_max = t_template.Length;
					for(int jj=0;jj<jj_max;jj++){
						string t_temp = t_template[jj].Replace(a_index_key,ii_string);
						a_out_list.Add(t_temp);
					}
				}
			}
		}

		/** 複製。

			「a_data」を「a_count」の数だけ複製、「a_index_key」をインデックスに置換。

			a_out_list				: 追加先。
			a_index_key				: 例 : <<INDEX>>
			a_index_count			: 総数。
			a_index_offset			: 開始インデックス。
			a_replace_list			: 置換リスト。
			a_data					: データ。

		*/
		public static void Duplicate(System.Collections.Generic.List<string> a_out_list,string a_index_key,int a_index_count,int a_index_offset,System.Collections.Generic.Dictionary<string,string> a_replace_list,System.Collections.Generic.List<string> a_data)
		{
			int ii_max = a_index_count + a_index_offset;
			if(a_replace_list != null){
				for(int ii=a_index_offset;ii<ii_max;ii++){
					string ii_string = ii.ToString();
					int jj_max = a_data.Count;
					for(int jj=0;jj<jj_max;jj++){
						string t_temp = a_data[jj].Replace(a_index_key,ii_string);
						t_temp = Replace(a_replace_list,t_temp,16);
						a_out_list.Add(t_temp);
					}
				}
			}else{
				for(int ii=a_index_offset;ii<ii_max;ii++){
					string ii_string = ii.ToString();
					int jj_max = a_data.Count;
					for(int jj=0;jj<jj_max;jj++){
						string t_temp = a_data[jj].Replace(a_index_key,ii_string);
						a_out_list.Add(t_temp);
					}
				}
			}
		}

		/** 追加。

			string[] --> Append --> StringBuilder

			a_out_stringbuilder		: 追加先。
			a_replace_list			: 置換リスト。
			a_data					: データ。

		*/
		public static void Add(System.Text.StringBuilder a_out_stringbuilder,System.Collections.Generic.Dictionary<string,string> a_replace_list,string[] a_data)
		{
			int ii_max = a_data.Length;
			if(a_replace_list != null){
				for(int ii=0;ii<ii_max;ii++){
					string t_temp = a_data[ii];
					t_temp = Convert.Replace(a_replace_list,t_temp,16);
					a_out_stringbuilder.Append(t_temp);
					a_out_stringbuilder.Append("\n");
				}
			}else{
				for(int ii=0;ii<ii_max;ii++){
					string t_temp = a_data[ii];
					a_out_stringbuilder.Append(t_temp);
					a_out_stringbuilder.Append("\n");
				}
			}
		}

		/** 追加。StringBuilder。

			List<string> --> Append --> StringBuilder

			a_out_stringbuilder		: 追加先。
			a_replace_list			: 置換リスト。
			a_data					: データ。

		*/
		public static void Add(System.Text.StringBuilder a_out_stringbuilder,System.Collections.Generic.Dictionary<string,string> a_replace_list,System.Collections.Generic.List<string> a_data)
		{
			int ii_max = a_data.Count;
			if(a_replace_list != null){
				for(int ii=0;ii<ii_max;ii++){
					string t_temp = a_data[ii];
					t_temp = Convert.Replace(a_replace_list,t_temp,16);
					a_out_stringbuilder.Append(t_temp);
					a_out_stringbuilder.Append("\n");
				}
			}else{
				for(int ii=0;ii<ii_max;ii++){
					string t_temp = a_data[ii];
					a_out_stringbuilder.Append(t_temp);
					a_out_stringbuilder.Append("\n");
				}
			}
		}
	}
}

