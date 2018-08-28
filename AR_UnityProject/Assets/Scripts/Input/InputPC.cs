using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPC : IInput 
{
	public bool FireButton()
	{
		return Input.GetKeyDown(KeyCode.Space);
	}
	
	public float HorizontalAxis () 
	{
		return Input.GetAxis("Horizontal");
	}


	public float VerticalAxis() 
	{
		return Input.GetAxis("Vertical");
	}
}
