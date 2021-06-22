

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
		}
	}
	#endif
}

