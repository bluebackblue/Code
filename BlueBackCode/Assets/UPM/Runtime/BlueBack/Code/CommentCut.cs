

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief コメントカット。
*/


/** BlueBack.Code
*/
namespace BlueBack.Code
{
	/** CommentCut
	*/
	public static class CommentCut
	{
		/** Replace
		*/
		public static string Replace(string a_data)
		{
			string t_data = a_data;;

			//「\\r」の後に「\\n」が来ない。
			{
				if(System.Text.RegularExpressions.Regex.IsMatch(t_data,"[\\r][^\\n]") == true){
					UnityEngine.Debug.LogError("ReturnError");
				}
			}

			//1行コメント。
			{
				string t_pattern_comment = "//(?<comment>([^\\r\\n])*)(?<return>(\\r)?\\n)";
				t_data = System.Text.RegularExpressions.Regex.Replace(t_data,t_pattern_comment,(System.Text.RegularExpressions.Match a_a_match)=>{
					return a_a_match.Groups["return"].Value;
				},System.Text.RegularExpressions.RegexOptions.Multiline);
			}

			//複数行コメント。
			{
				string t_pattern_comment = "(?<comment>/\\*([^\\*]|\\*[^/])*\\*/)";
				t_data = System.Text.RegularExpressions.Regex.Replace(t_data,t_pattern_comment,(System.Text.RegularExpressions.Match a_a_match)=>{
					return "";
				},System.Text.RegularExpressions.RegexOptions.Multiline);
			}

			return t_data;
		}
	}
}

