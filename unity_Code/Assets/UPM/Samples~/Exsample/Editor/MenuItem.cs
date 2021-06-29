

/** Samples.Code.Exsample.Editor
*/
namespace Samples.Code.Exsample.Editor
{
	/** MenuItem
	*/
	#if(UNITY_EDITOR)
	public class MenuItem
	{
		/** テスト。
		*/
		[UnityEditor.MenuItem("サンプル/Code/Exsample/Test")]
		private static void MenuItem_Test()
		{
			string[] t_data = new string[]
			{
				"aa",
				"bb",
				"cc",
			};

			System.Collections.Generic.List<string> t_template = new System.Collections.Generic.List<string>();

			{
				BlueBack.Code.Convert.Add(new string[]{
					"/** <<NAMESPACE>>",
					"*/",
					"namespace <<NAMESPACE>>",
					"{",
					"	/** <<CLASS>>",
					"	*/",
					"	public class <<CLASS>>",
					"	{",
				},t_template);

				BlueBack.Code.Convert.Duplicate(new string[]{
					"		public int <<VALUE.<<INDEX>>>>;",
				},t_data.Length,"<<INDEX>>",t_template);

				BlueBack.Code.Convert.Add(new string[]{
					"",
					"		/** constructor",
					"		*/",
					"		public AAA()",
					"		{",
				},t_template);

				BlueBack.Code.Convert.Duplicate(new string[]{
					"			<<VALUE.<<INDEX>>>> = 0;",
				},t_data.Length,"<<INDEX>>",t_template);

				BlueBack.Code.Convert.Add(new string[]{
					"		}",
					"	}",
					"}",
				},t_template);
			}

			System.Collections.Generic.Dictionary<string,string> t_replace_list = new System.Collections.Generic.Dictionary<string,string>();
			{
				t_replace_list.Add("<<NAMESPACE>>","Aaa");
				t_replace_list.Add("<<CLASS>>","AAA");

				for(int ii=0;ii<t_data.Length;ii++){
					t_replace_list.Add("<<VALUE." + ii.ToString()  + ">>",t_data[ii]);
				}
			}

			System.Text.StringBuilder t_stringbuilder = new System.Text.StringBuilder();
			BlueBack.Code.Convert.Replace(t_template,t_stringbuilder,t_replace_list);
			
			UnityEngine.Debug.Log(t_stringbuilder.ToString());
		}
	}
	#endif
}

