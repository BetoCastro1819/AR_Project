using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInput 
{
	bool FireButton();
	float HorizontalAxis();
	float VerticalAxis();
}
