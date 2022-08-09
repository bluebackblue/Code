

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
		public static void Replace(string a_data)
		{
			UnityEngine.Debug.Log(string.Format("ARGUMENT : {0}",a_data));

			string t_data;

			string t_comment = "XXX";

			//「\\r」の後に「\\n」が来ない場合がある。
			{
				if(System.Text.RegularExpressions.Regex.IsMatch(a_data,"[\\r][^\\n]") == true){
					UnityEngine.Debug.LogError("ReturnError");
				}
			}

			{
				string t_pattern_comment = "//(?<comment>([^\\r\\n])*)(?<return>(\\r)?\\n)";
				t_data = System.Text.RegularExpressions.Regex.Replace(a_data,t_pattern_comment,(System.Text.RegularExpressions.Match a_a_match)=>{

					UnityEngine.Debug.Log(string.Format("RETURN : [{0}] : COMMENT : [{1}]",a_a_match.Groups["return"].Value.Replace("\r","<r>").Replace("\n","<n>"),a_a_match.Groups["comment"].Value.Replace("\r","<r>").Replace("\n","<n>")));

					return t_comment + a_a_match.Groups["return"].Value;
				},System.Text.RegularExpressions.RegexOptions.Multiline);
			}

			{
				string t_pattern_comment = "(?<comment>/\\*([^\\*]|\\*[^/])*\\*/)";
				t_data = System.Text.RegularExpressions.Regex.Replace(t_data,t_pattern_comment,(System.Text.RegularExpressions.Match a_a_match)=>{
					UnityEngine.Debug.Log(string.Format("COMMENT : [{0}]",a_a_match.Groups["comment"].Value.Replace("\r","<r>").Replace("\n","<n>")));
					return t_comment + "";
				},System.Text.RegularExpressions.RegexOptions.Multiline);
			}

			UnityEngine.Debug.Log(string.Format("DATA : {0}",t_data));
		}
	}
}

