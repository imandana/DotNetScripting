using System;

using RemorseEngine;

namespace MyGame
{
	public class Lulu : DotNetBehaviour
	{
		public Lulu(IntPtr pGo, int length) : base(pGo, length)
		{ }
		public static void Awake(IntPtr pGo, int length)
        {
			Init( new Lulu(pGo, length) );
		}
		///////
		////// Your Code Goes Here
		
		int adder = 0;
		public override void RealStart()
		{
			Debug.Log($"From C# This Start LULU named {go.GetName()}");
		}
		
		public override void RealUpdate()
		{
			adder += 8;
			go.MoveTo( adder , 33333);
			
			Debug.Log($"From C# UPDATE Nilai Y {go.posY} \n");
			
		}
		////// Your Code Goes Here
	}
}
