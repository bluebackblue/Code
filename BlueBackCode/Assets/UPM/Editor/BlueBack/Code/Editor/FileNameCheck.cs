

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief 定義名とファイル名の一致チェック。
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
		/** Check
		*/
		public static void Check()
		{
			System.Collections.Generic.List<string> t_list = BlueBack.AssetLib.Editor.FindFileWithAssetsPath.FindAll("","^.*$","^.*\\.cs$");
			for(int ii=0;ii<t_list.Count;ii++){
				string t_codetext = BlueBack.AssetLib.Editor.LoadTextWithAssetsPath.Load(t_list[ii]);
				Inner_Check(t_list[ii],t_codetext);
			}
		}

		/** MenuItem_BlueBack_Code_FileNameCheck
		*/
		[UnityEditor.MenuItem("BlueBack/Code/Check/FileNameCheck")]
		private static void MenuItem_BlueBack_Code_Check_FileNameCheck()
		{
			Check();
		}

		/** KeywordType
		*/
		private enum KeywordType
		{
			/** None
			*/
			None,

			/** Struct
			*/
			Struct,

			/** Class
			*/
			Class,

			/** Enum
			*/
			Enum,

			/** Interface
			*/
			Interface,
		};

		/** Inner_FindKeyword
		*/
		private static KeywordType Inner_FindKeyword(string a_codetext)
		{
			string t_codetext = BlueBack.Code.CommentCut.Replace(a_codetext);

			int t_index_struct = t_codetext.IndexOf("struct");
			int t_index_class = t_codetext.IndexOf("class");
			int t_index_enum = t_codetext.IndexOf("enum");
			int t_index_interface = t_codetext.IndexOf("interface");

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

		/** Inner_ReplaceEscapeSequence
		*/
		private static string Inner_ReplaceEscapeSequence(string a_value)
		{
			return a_value.Replace("\r","<<r>>").Replace("\n","<<n>>").Replace("\t","<<tab>>").Replace(" ","<<space>>");
		}

		/** Check
		*/
		private static void Inner_Check(string a_path,string a_codetext)
		{
			KeywordType t_findkeyword = Inner_FindKeyword(a_codetext);

			switch(t_findkeyword){
			case KeywordType.Struct:
				{
					string t_pattern = string.Format("{0}{1}{2}{3}","struct","(?<space>[\\r\\n \\t]*)",System.IO.Path.GetFileNameWithoutExtension(a_path),"(?<last>[\\r\\n \\t<:])");

					System.Text.RegularExpressions.Match t_match = System.Text.RegularExpressions.Regex.Match(a_codetext,t_pattern,System.Text.RegularExpressions.RegexOptions.Multiline);
					if(t_match.Success == true){
						string t_value_space = Inner_ReplaceEscapeSequence(t_match.Groups["space"].Value);
						string t_value_last = Inner_ReplaceEscapeSequence(t_match.Groups["last"].Value);

						UnityEngine.Debug.Log(string.Format("path = {0} : keyword = {1} : space = [{2}][{3}]\n{4}",a_path,t_findkeyword,t_value_space,t_value_last,a_codetext));

						return;
					}else{
						UnityEngine.Debug.LogError(string.Format("{0} : {1}",a_path,a_codetext));
					}
				}break;
			case KeywordType.Enum:
				{
					string t_pattern = string.Format("{0}{1}{2}{3}","enum","(?<space>[\\r\\n \\t]*)",System.IO.Path.GetFileNameWithoutExtension(a_path),"(?<last>[\\r\\n \\t<:])");

					System.Text.RegularExpressions.Match t_match = System.Text.RegularExpressions.Regex.Match(a_codetext,t_pattern,System.Text.RegularExpressions.RegexOptions.Multiline);
					if(t_match.Success == true){
						string t_value_space = Inner_ReplaceEscapeSequence(t_match.Groups["space"].Value);
						string t_value_last = Inner_ReplaceEscapeSequence(t_match.Groups["last"].Value);

						UnityEngine.Debug.Log(string.Format("path = {0} : keyword = {1} : space = [{2}][{3}]\n{4}",a_path,t_findkeyword,t_value_space,t_value_last,a_codetext));

						return;
					}else{
						UnityEngine.Debug.LogError(string.Format("{0} : {1}",a_path,a_codetext));
					}
				}break;
			case KeywordType.Interface:
				{
					string t_pattern = string.Format("{0}{1}{2}{3}","interface","(?<space>[\\r\\n \\t]*)",System.IO.Path.GetFileNameWithoutExtension(a_path),"(?<last>[\\r\\n \\t<:])");

					System.Text.RegularExpressions.Match t_match = System.Text.RegularExpressions.Regex.Match(a_codetext,t_pattern,System.Text.RegularExpressions.RegexOptions.Multiline);
					if(t_match.Success == true){
						string t_value_space = Inner_ReplaceEscapeSequence(t_match.Groups["space"].Value);
						string t_value_last = Inner_ReplaceEscapeSequence(t_match.Groups["last"].Value);

						UnityEngine.Debug.Log(string.Format("path = {0} : keyword = {1} : space = [{2}][{3}]\n{4}",a_path,t_findkeyword,t_value_space,t_value_last,a_codetext));

						return;
					}else{
						UnityEngine.Debug.LogError(string.Format("{0} : {1}",a_path,a_codetext));
					}
				}break;
			case KeywordType.Class:
				{
					string t_pattern = string.Format("{0}{1}{2}{3}","class","(?<space>[\\r\\n \\t]*)",System.IO.Path.GetFileNameWithoutExtension(a_path),"(?<last>[\\r\\n \\t<:])");

					System.Text.RegularExpressions.Match t_match = System.Text.RegularExpressions.Regex.Match(a_codetext,t_pattern,System.Text.RegularExpressions.RegexOptions.Multiline);
					if(t_match.Success == true){
						string t_value_space = Inner_ReplaceEscapeSequence(t_match.Groups["space"].Value);
						string t_value_last = Inner_ReplaceEscapeSequence(t_match.Groups["last"].Value);

						UnityEngine.Debug.Log(string.Format("path = {0} : keyword = {1} : space = [{2}][{3}]\n{4}",a_path,t_findkeyword,t_value_space,t_value_last,a_codetext));

						return;
					}else{
						UnityEngine.Debug.LogError(string.Format("{0} : {1}",a_path,a_codetext));
					}
				}break;
			default:
				{
					UnityEngine.Debug.LogError(string.Format("error : {0}",t_findkeyword));
				}break;
			}
		}
	}
}
#endif

