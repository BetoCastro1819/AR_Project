using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAndroid : IInput
{
	public bool FireButton() 
	{
		return Input.GetKeyDown(KeyCode.Space);
	}

	public float HorizontalAxis() 
	{
        return Input.GetAxis("Horizontal");
        // Joystick.Horizontal;
    }


    public float VerticalAxis() 
	{
        return Input.GetAxis("Vertical");
        // Joystick.Vertical;
	}
}
