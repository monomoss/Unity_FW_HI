namespace HI
{
	public delegate void DG_Normal();

	public delegate void DG_Objects(params object[] args);

	public delegate void DG_Strings(params string[] args);

	public delegate void DG_Ints(params int[] args);



	public delegate void DG_GameObject(UnityEngine.GameObject arg1);

	public delegate void DG_ByteArray(byte[] arg1);
}
