

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief コードコンバート。
*/


/** define
*/
#if((ASMDEF_BLUEBACK_ASSETLIB||USERDEF_BLUEBACK_ASSETLIB))
#define ASMDEF_TRUE
#else
#warning "ASMDEF_TRUE"
#endif


/** BlueBack.Code.Editor
*/
#if(UNITY_EDITOR)
namespace BlueBack.Code.Editor
{
	/** FileNameCheck
	*/
	public static class FileNameCheck
	{
		/** MenuItem_BlueBack_Code_FileNameCheck
		*/
		[UnityEditor.MenuItem("BlueBack/Code/Check/FileNameCheck")]
		private static void MenuItem_BlueBack_Code_Check_FileNameCheck()
		{
			System.Collections.Generic.List<string> t_list = BlueBack.AssetLib.Editor.FindFileWithAssetsPath.FindAll("","^.*$","^.*\\.cs$");
			for(int ii=0;ii<t_list.Count;ii++){
				string t_codetext = BlueBack.AssetLib.Editor.LoadTextWithAssetsPath.Load(t_list[ii]);
				Inner_Check(t_list[ii],t_codetext);
			}
		}

		/** MenuItem_BlueBack_Code_Check_Test
		*/
		[UnityEditor.MenuItem("BlueBack/Code/Check/Test")]
		private static void MenuItem_BlueBack_Code_Check_Test()
		{
			System.Collections.Generic.List<string> t_list = BlueBack.AssetLib.Editor.FindFileWithAssetsPath.FindAll("","^.*$","^.*\\.cs$");
			for(int ii=0;ii<t_list.Count;ii++){
				if(System.IO.Path.GetFileNameWithoutExtension(t_list[ii]) == "TestClass"){
					string t_codetext = BlueBack.AssetLib.Editor.LoadTextWithAssetsPath.Load(t_list[ii]);
					CommentCut.Replace(t_codetext);
				}
			}
		}

		/** KeywordType
		*/
		private enum KeywordType
		{
			None,
			Struct,
			Class,
			Enum,
			Interface,
		};

		/** Inner_FindKeyword

			TODO:コメントのスキップ。正規化。

		*/
		private static KeywordType Inner_FindKeyword(string a_codetext)
		{
			int t_index_struct = a_codetext.IndexOf("struct");
			int t_index_class = a_codetext.IndexOf("class");
			int t_index_enum = a_codetext.IndexOf("enum");
			int t_index_interface = a_codetext.IndexOf("interface");

			if((t_index_struct >= 0)&&((t_index_class < 0)||(t_index_struct < t_index_class))&&((t_index_enum < 0)||(t_index_struct < t_index_enum))&&((t_index_interface < 0)||(t_index_struct < t_index_interface))){
				return KeywordType.Struct;
			}

			if((t_index_class >= 0)&&((t_index_enum < 0)||(t_index_class < t_index_enum))&&((t_index_interface < 0)||(t_index_class < t_index_interface))){
				return KeywordType.Class;
			}

			if((t_index_enum >= 0)&&((t_index_interface < 0)||(t_index_enum < t_index_interface))){
				return KeywordType.Enum;
			}

			if(t_index_interface >= 0){
				return KeywordType.Interface;
			}

			return KeywordType.None;
		}

		/** ReplaceEscapeSequence
		*/
		private static string ReplaceEscapeSequence(string a_value)
		{
			return a_value.Replace("\r","<r>").Replace("\n","<n>").Replace("\t","<tab>").Replace(" ","<space>");
		}


		/** Check
		*/
		private static void Inner_Check(string a_path,string a_codetext)
		{
			KeywordType t_findkeyword = Inner_FindKeyword(a_codetext);

			switch(t_findkeyword){
			case KeywordType.Struct:
				{
					string t_pattern_keyword = "struct";
					string t_pattern_space = "(?<space>[\\r\\n \\t]*)";
					string t_pattern_name = System.IO.Path.GetFileNameWithoutExtension(a_path);
					string t_pattern_last = "(?<last>[\\r\\n \\t<:]*)";

					string t_pattern = "";
					{
						t_pattern += t_pattern_keyword;
						t_pattern += t_pattern_space;
						t_pattern += t_pattern_name;
						t_pattern += t_pattern_last;
					}

					System.Text.RegularExpressions.Match t_match = System.Text.RegularExpressions.Regex.Match(a_codetext,t_pattern,System.Text.RegularExpressions.RegexOptions.Multiline);
					if(t_match.Success == true){
						string t_value_space = ReplaceEscapeSequence(t_match.Groups["space"].Value);
						string t_value_last = ReplaceEscapeSequence(t_match.Groups["last"].Value);

						UnityEngine.Debug.Log(string.Format("path = {0} : keyword = {1} : space = [{2}][{3}]\n{4}",a_path,t_findkeyword,t_value_space,t_value_last,a_codetext));

						return;
					}else{
						UnityEngine.Debug.LogError(string.Format("{0} : {1}",a_path,a_codetext));
					}
				}break;
			case KeywordType.Enum:
				{
					string t_pattern_keyword = "enum";
					string t_pattern_space = "(?<space>[\\r\\n \\t]*)";
					string t_pattern_name = System.IO.Path.GetFileNameWithoutExtension(a_path);
					string t_pattern_last = "(?<last>[\\r\\n \\t<:]*)";

					string t_pattern = "";
					{
						t_pattern += t_pattern_keyword;
						t_pattern += t_pattern_space;
						t_pattern += t_pattern_name;
						t_pattern += t_pattern_last;
					}

					System.Text.RegularExpressions.Match t_match = System.Text.RegularExpressions.Regex.Match(a_codetext,t_pattern,System.Text.RegularExpressions.RegexOptions.Multiline);
					if(t_match.Success == true){
						string t_value_space = ReplaceEscapeSequence(t_match.Groups["space"].Value);
						string t_value_last = ReplaceEscapeSequence(t_match.Groups["last"].Value);

						UnityEngine.Debug.Log(string.Format("path = {0} : keyword = {1} : space = [{2}][{3}]\n{4}",a_path,t_findkeyword,t_value_space,t_value_last,a_codetext));

						return;
					}else{
						UnityEngine.Debug.LogError(string.Format("{0} : {1}",a_path,a_codetext));
					}
				}break;
			case KeywordType.Interface:
				{
					string t_pattern_keyword = "interface";
					string t_pattern_space = "(?<space>[\\r\\n \\t]*)";
					string t_pattern_name = System.IO.Path.GetFileNameWithoutExtension(a_path);
					string t_pattern_last = "(?<last>[\\r\\n \\t<:]*)";

					string t_pattern = "";
					{
						t_pattern += t_pattern_keyword;
						t_pattern += t_pattern_space;
						t_pattern += t_pattern_name;
						t_pattern += t_pattern_last;
					}

					System.Text.RegularExpressions.Match t_match = System.Text.RegularExpressions.Regex.Match(a_codetext,t_pattern,System.Text.RegularExpressions.RegexOptions.Multiline);
					if(t_match.Success == true){
						string t_value_space = ReplaceEscapeSequence(t_match.Groups["space"].Value);
						string t_value_last = ReplaceEscapeSequence(t_match.Groups["last"].Value);

						UnityEngine.Debug.Log(string.Format("path = {0} : keyword = {1} : space = [{2}][{3}]\n{4}",a_path,t_findkeyword,t_value_space,t_value_last,a_codetext));

						return;
					}else{
						UnityEngine.Debug.LogError(string.Format("{0} : {1}",a_path,a_codetext));
					}
				}break;
			case KeywordType.Class:
				{
					string t_pattern_keyword = "class";
					string t_pattern_space = "(?<space>[\\r\\n \\t]*)";
					string t_pattern_name = System.IO.Path.GetFileNameWithoutExtension(a_path);
					string t_pattern_last = "(?<last>[\\r\\n \\t<:]*)";

					string t_pattern = "";
					{
						t_pattern += t_pattern_keyword ;
						t_pattern += t_pattern_space;
						t_pattern += t_pattern_name;
						t_pattern += t_pattern_last;
					}

					System.Text.RegularExpressions.Match t_match = System.Text.RegularExpressions.Regex.Match(a_codetext,t_pattern,System.Text.RegularExpressions.RegexOptions.Multiline);
					if(t_match.Success == true){
						string t_value_space = ReplaceEscapeSequence(t_match.Groups["space"].Value);
						string t_value_last = ReplaceEscapeSequence(t_match.Groups["last"].Value);

						UnityEngine.Debug.Log(string.Format("path = {0} : keyword = {1} : space = [{2}][{3}]\n{4}",a_path,t_findkeyword,t_value_space,t_value_last,a_codetext));

						return;
					}else{
						UnityEngine.Debug.LogError(string.Format("{0} : {1}",a_path,a_codetext));
					}
				}break;

			}
		}
	}
}
#endif

