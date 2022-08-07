

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
	/** CodeConvert
	*/
	public static class CodeConvert
	{
		/** SingletonParam
		*/
		public sealed class SingletonParam : UnityEditor.ScriptableSingleton<SingletonParam>
		{
			/** Item
			*/
			[System.Serializable]
			public struct Item
			{
				/** key
				*/
				public string key;

				/** value
				*/
				public string value;
			}

			/** hashlist_serialize
			*/
			[UnityEngine.SerializeField]
			public System.Collections.Generic.List<Item> hashlist_serialize;

			/** hashlist
			*/
			public System.Collections.Generic.Dictionary<string,string> hashlist;
		}

		/** singletonparam
		*/
		private static SingletonParam singletonparam = null;

		/** Inner_LoadSingletonParam
		*/
		private static SingletonParam Inner_LoadSingletonParam()
		{
			if(CodeConvert.singletonparam == null){
				CodeConvert.singletonparam = SingletonParam.instance;
				if(CodeConvert.singletonparam.hashlist_serialize == null){
					CodeConvert.singletonparam.hashlist_serialize = new System.Collections.Generic.List<SingletonParam.Item>();
				}
				CodeConvert.singletonparam.hashlist = new System.Collections.Generic.Dictionary<string,string>();
				foreach(SingletonParam.Item t_item in CodeConvert.singletonparam.hashlist_serialize){
					CodeConvert.singletonparam.hashlist.Add(t_item.key,t_item.value);
				}
			}
			return CodeConvert.singletonparam;
		}

		/** MenuItem_BlueBack_Code_UpdatePackage
		*/
		[UnityEditor.MenuItem("BlueBack/Code/CodeConvert/NoBomUtf8Crlf")]
		private static void MenuItem_BlueBack_Code_UpdatePackage()
		{
			Convert();
		}

		/** Inner_SaveSingletonParam
		*/
		private static void Inner_SaveSingletonParam()
		{
			if(CodeConvert.singletonparam != null){
				if(CodeConvert.singletonparam.hashlist != null){
					CodeConvert.singletonparam.hashlist_serialize = new System.Collections.Generic.List<SingletonParam.Item>();
					foreach(System.Collections.Generic.KeyValuePair<string,string> t_pair in CodeConvert.singletonparam.hashlist){
						CodeConvert.singletonparam.hashlist_serialize.Add(new SingletonParam.Item(){
							key = t_pair.Key,
							value = t_pair.Value,
						});
					}
				}
			}
		}

		/** Inner_CalcHash
		*/
		#if(ASMDEF_TRUE)
		private static string Inner_CalcHash(string a_filename)
		{
			byte[] t_file_binary = BlueBack.AssetLib.Editor.LoadBinaryWithAssetsPath.Load(a_filename);
			System.Security.Cryptography.MD5CryptoServiceProvider t_service = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] t_hash_binary = t_service.ComputeHash(t_file_binary);

			System.Text.StringBuilder t_stringbuilder = new System.Text.StringBuilder();
			foreach(byte t_byte in t_hash_binary) {
				t_stringbuilder.Append(t_byte.ToString("X2"));
			}
			return t_stringbuilder.ToString();
		}
		#endif

		/** Convert
		*/
		public static void Convert()
		#if(ASMDEF_TRUE)
		{
			SingletonParam t_singletonparam = Inner_LoadSingletonParam();
			{
				System.Collections.Generic.List<string> t_filename_list = BlueBack.AssetLib.Editor.FindFileWithAssetsPath.FindAll("",".*","^(.*)(\\.cs)$");
				foreach(string t_filename in t_filename_list){
					string t_hash_new = Inner_CalcHash(t_filename);
					bool t_file_change = false;
					{
						if(t_singletonparam.hashlist.TryGetValue(t_filename,out string t_hash_old) == true){
							if(t_hash_new != t_hash_old){
								UnityEngine.Debug.Log("Chainge : " + t_filename);
								t_singletonparam.hashlist.Remove(t_filename);
								t_file_change = true;
							}
						}else{
							UnityEngine.Debug.Log("New : " + t_filename);
							t_file_change = true;
						}
					}

					if(t_file_change == true){
						string t_file_text = BlueBack.AssetLib.Editor.LoadTextWithAssetsPath.Load(t_filename);
						string t_file_text_new = Convert_File(t_filename,t_file_text);
						if(t_file_text != t_file_text_new){
							BlueBack.AssetLib.Editor.SaveTextWithAssetsPath.SaveNoBomUtf8(t_file_text_new,t_filename,BlueBack.AssetLib.LineFeedOption.CRLF);
							t_hash_new = Inner_CalcHash(t_filename);
						}

						t_singletonparam.hashlist.Add(t_filename,t_hash_new);
					}
				}
			}
			Inner_SaveSingletonParam();
		}
		#else
		{
			#warning "ASMDEF_TRUE"
		}
		#endif

		/** Convert_File
		*/
		private static string Convert_File(string a_file_name,string a_text)
		{
			//line_list
			string[] t_line_list = a_text.Replace("\r","").Split(new char[]{'\n'});
			int t_line_list_max = t_line_list.Length;

			//有効行数。
			{
				for(int ii=t_line_list_max-1;ii>=0;ii--){
					if(t_line_list[ii].Length == 0){
						t_line_list_max--;
					}else{
						break;
					}
				}
			}

			System.Text.StringBuilder t_stringbuilder = new System.Text.StringBuilder();

			{
				for(int ii=0;ii<t_line_list_max;ii++){
					System.Text.RegularExpressions.Regex t_regex = new System.Text.RegularExpressions.Regex("^(?<nest>[\\t ]*)(?! \\t)(?<main>[\\d\\D]*)$",System.Text.RegularExpressions.RegexOptions.Multiline);
					System.Text.RegularExpressions.Match t_match = t_regex.Match(t_line_list[ii]);
					if(t_match.Success == true){
						string t_nest = t_match.Groups["nest"].Value;
						string t_main = t_match.Groups["main"].Value;

						//ネストのスペースをタブに変換。
						t_nest = t_nest.Replace("   ","\t").Replace(" ","\t");

						//本文なしの場合ネストを削除。
						if(t_main.Length == 0){
							t_line_list[ii] = "";
						}else{
							t_line_list[ii] = t_nest + t_main;
						}
					}else{
						UnityEngine.Debug.LogError(t_line_list[ii] + " : " + a_file_name + "(" + (ii+1).ToString() + ")");
					}

					t_stringbuilder.Append(t_line_list[ii]);
					t_stringbuilder.Append("\n");
				}
			}

			t_stringbuilder.Append("\n");
			return t_stringbuilder.ToString();
		}
	}
}
#endif

