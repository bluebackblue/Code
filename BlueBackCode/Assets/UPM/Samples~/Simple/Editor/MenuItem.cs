

/** BlueBack.Code.Samples.Simple.Editor
*/
#if(!DEF_BLUEBACK_CODE_SAMPLES_DISABLE)
namespace BlueBack.Code.Samples.Simple.Editor
{
	/** MenuItem
	*/
	#if(UNITY_EDITOR)
	public class MenuItem
	{
		/** テスト。
		*/
		[UnityEditor.MenuItem("Samples/BlueBack.Code/Simple/Replace")]
		private static void MenuItem_Replace()
		{
			string[] t_data = new string[]
			{
				"aa",
				"bb",
				"cc",
			};

			System.Collections.Generic.List<string> t_template = new System.Collections.Generic.List<string>();
			{
				BlueBack.Code.Convert.Add(t_template,new string[]{
					"/** <<NAMESPACE>>",
					"*/",
					"namespace <<NAMESPACE>>",
					"{",
					"	/** <<CLASS>>",
					"	*/",
					"	public class <<CLASS>>",
					"	{",
				});

				BlueBack.Code.Convert.Duplicate(t_template,"<<INDEX>>",t_data.Length,new string[]{
					"		public int <<VALUE.<<INDEX>>>>;",
				});

				BlueBack.Code.Convert.Add(t_template,new string[]{
					"",
					"		/** constructor",
					"		*/",
					"		public <<CLASS>>()",
					"		{",
				});

				BlueBack.Code.Convert.Duplicate(t_template,"<<INDEX>>",t_data.Length,new string[]{
					"			<<VALUE.<<INDEX>>>> = 0;",
				});

				BlueBack.Code.Convert.Add(t_template,new string[]{
					"		}",
					"	}",
					"}",
				});
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
			BlueBack.Code.Convert.Add(t_stringbuilder,t_replace_list,t_template);

			UnityEngine.Debug.Log(t_stringbuilder.ToString());
		}
	}
	#endif
}
#endif

