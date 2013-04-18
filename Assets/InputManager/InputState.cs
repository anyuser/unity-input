using UnityEngine;
using System.Collections;

public struct InputState
{
	public double timestamp;
	public float[] axisValues;
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
	
	public float GetAxis(AxisType axis)
	{
		if( axisValues == null )
			axisValues = new float[System.Enum.GetValues(typeof(AxisType)).Length];

		return axisValues[(int)axis];
	}
	
	public void SetAxis(AxisType axis, float val)
	{
		if( axisValues == null )
			axisValues = new float[System.Enum.GetValues(typeof(AxisType)).Length];

		axisValues[(int)axis] = val;
	}

	public override string ToString ()
	{
		string str = "Axis: ";
		for(int i = 0; i < axisValues.Length; i++)
			str += axisValues[i] +" ";
		
		str += string.Format("Buttons: {0}",buttons);
		
		return str;
	}
}