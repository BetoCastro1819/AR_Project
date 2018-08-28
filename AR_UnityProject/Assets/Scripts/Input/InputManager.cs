using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour 
{
	IInput input;

	#region Singleton
	private static InputManager instance;
	public static InputManager GetInstance() 
	{
		return instance;
	}

	void Awake () 
	{
#if UNITY_ANDROID
		input = new InputAndroid();
#else
		input = new InputPC();
#endif

		instance = this;
	}
	#endregion

	public bool FireButton() 
	{
		return input.FireButton();
	}

	public float HorizontalAxis() 
	{
		return input.HorizontalAxis();
	}

	public float VerticalAxis() 
	{
		return input.VerticalAxis();
	}
}
