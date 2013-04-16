using UnityEngine;
using System.Collections;

public struct InputButtonState
{
	public double timestamp;
	public ButtonType buttons;
		
	public void SetButton(ButtonType btn, bool value)
	{
		if( value )
			buttons = buttons | btn; // set on
		else
			buttons = buttons & ~btn; // set off
	}
	
	public bool GetButton(ButtonType btn)
	{		
		return (buttons & btn) != ButtonType.None;
	}
	
	public override string ToString ()
	{
		return string.Format ("Buttons: {0}",buttons);		
	}
}