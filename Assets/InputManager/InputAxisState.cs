using UnityEngine;
using System.Collections;

public struct InputAxisState
{
	public double timestamp;
	public float[] axisValues;

	public override string ToString ()
	{
		string str = "Axis: ";
		for(int i = 0; i < axisValues.Length; i++)
			str += axisValues[i] +" ";
		return str;
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
	
}