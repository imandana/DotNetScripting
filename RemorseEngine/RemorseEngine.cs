using System;
using System.Runtime.InteropServices;

namespace RemorseEngine
{
	[StructLayout(LayoutKind.Sequential)]
	public class GameObject
	{
		public IntPtr name;
		public int posX;
		public int posY;
		public int isChanged;
		
		private static GameObject theStaticGO;
		
		public void SetStaticGO(GameObject go)
		{
			theStaticGO = go;
		}
		
		public void MoveTo(int x, int y)
		{
			theStaticGO = this;
			
			isChanged = 1;
			theStaticGO.posX = x ;
			theStaticGO.posY = y ;
		}
		
/* 		public void ChangeName( string pName )
		{
			theStaticGO = this;
			string strChanged = new string(pName);
			////////////// sek rawan
			
			Console.WriteLine($"Ini dari IntPtr Sting { Marshal.PtrToStringAuto(theStaticGO.name) }");
			IntPtr stringPointer = (IntPtr)Marshal.StringToHGlobalAnsi( strChanged );
			theStaticGO.name = stringPointer;
			
			Marshal.FreeHGlobal(stringPointer);
		} */
		
		public string GetName()
		{
			string tName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
			? Marshal.PtrToStringUni(name)
			: Marshal.PtrToStringUTF8(name);
			
			return tName;
		}

		public delegate IntPtr UpdateNativeGODelegate();
		public static IntPtr UpdateNativeGO()
		{	
			IntPtr pnt = Marshal.AllocHGlobal(Marshal.SizeOf(theStaticGO));
            Marshal.StructureToPtr(theStaticGO, pnt, false);
	
			Marshal.FreeHGlobal(pnt);
			
			return pnt;
		}
		
		public delegate int GetIsChangedDelegate();
		public static int GetIsChanged()
		{	
			return theStaticGO.isChanged;
		}
	}
	
	public class Debug
	{
		public static void Log(string str)
		{
			Console.WriteLine(str);
		}
	}
	
	public class DotNetBehaviour
	{
		// Masih satu instance, nanti mau dibuat banyak
		protected static DotNetBehaviour self;
		protected static GameObject go;
		public DotNetBehaviour(IntPtr pGo, int length)
		{
			go = Marshal.PtrToStructure<GameObject>(pGo);
			go.SetStaticGO( go );
		}

		protected static void Init(DotNetBehaviour obj)
        {
			self = obj;
		}
		
		public virtual void RealStart(){}
		public virtual void RealUpdate(){}
		
		protected delegate void AwakeDelegate(IntPtr pGo, int length);
        protected delegate void StartDelegate();
		protected delegate void UpdateDelegate();
		
		public static void Start()
		{
			self.RealStart();
		}
		public static void Update()
		{
			self.RealUpdate();
		}
		
	}
}

