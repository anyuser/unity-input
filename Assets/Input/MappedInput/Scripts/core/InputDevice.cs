using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class InputDevice : MonoBehaviour 
{	
	public static EnumValuesCache<MappedAxis> allMappedAxisTypes = new EnumValuesCache<MappedAxis> ();
	public static EnumValuesCache<MappedButton> allMappedButtonTypes = new EnumValuesCache<MappedButton> ();

	public bool autoActive = true;
	public float lastInputTime { get; private set;}
	internal bool HasAnyInputLastFrame = false;
	
	protected float[] prevAxes = new float[System.Enum.GetValues(typeof(MappedAxis)).Length];
	protected float[] axes = new float[System.Enum.GetValues(typeof(MappedAxis)).Length];
	protected float[] axesRaw = new float[System.Enum.GetValues(typeof(MappedAxis)).Length];
	protected AxisDirection[] prevAxisButtonsActive = new AxisDirection[System.Enum.GetValues(typeof(MappedAxis)).Length];
	protected AxisDirection[] axisButtonsActive = new AxisDirection[System.Enum.GetValues(typeof(MappedAxis)).Length];
	protected float axisButtonResetThreshold = 0.2f;
	protected float axisButtonActivateThreshold = 0.4f;
	protected float sensitivity = 3;
	protected float lastKeyPressTime;

	void Awake()
	{
	}

	protected virtual void Update()
	{
		HasAnyInputLastFrame = false;

		for (int i = 0; i < allMappedAxisTypes.Count; i++)
		{
			HasAnyInputLastFrame |= UpdateAxis (allMappedAxisTypes [i]); // update axis and check if there is any change
		}

		for (int i = 0; i < allMappedButtonTypes.Count; i++)
		{
			HasAnyInputLastFrame |= GetButton (allMappedButtonTypes[i]); // check if any mapped button is pressed
		}

		if (HasAnyInputLastFrame)
			lastInputTime = Time.time;
	
	}


	public abstract string GetButtonName (MappedButton button);	
	public abstract string GetAxisName (MappedAxis axis);

	public abstract bool GetButton (MappedButton button);
	public abstract bool GetButtonDown (MappedButton button);
	public abstract bool GetButtonUp (MappedButton button);

	protected abstract float GetAxisValueRaw (MappedAxis axis);

	// use axis as buttons (eg. analog stick for menu selection). 
	public bool GetAxisButtonDown(MappedAxis axis, AxisDirection dir)
	{
		return axisButtonsActive[(int)axis] == dir && prevAxisButtonsActive[(int)axis] != dir;
	}
	
	public bool GetAxisButton(MappedAxis axis, AxisDirection dir)
	{
		return axisButtonsActive[(int)axis] == dir;
	}
	
	public bool GetAxisButtonUp(MappedAxis axis, AxisDirection dir)
	{
		return axisButtonsActive[(int)axis] != dir && prevAxisButtonsActive[(int)axis] == dir;
	}

	public float GetAxis(MappedAxis axis)
	{ 
		return axes[(int)axis];
	}
	
	public float GetAxisRaw(MappedAxis axis)
	{ 
		return axesRaw[(int)axis];
	}


	bool UpdateAxis(MappedAxis axis)
	{
		bool changed = false;

		axesRaw[(int)axis] = GetAxisValueRaw (axis);
		
		prevAxes [(int)axis] = axes [(int)axis];

		axes [(int)axis] = GetSmoothValue (axes [(int)axis],axesRaw [(int)axis],sensitivity);

		if (!Mathf.Approximately(axes[(int)axis], prevAxes[(int)axis]))
			changed = true;
		
		prevAxisButtonsActive [(int)axis] = axisButtonsActive [(int)axis];

		int dir = 0;
		if (axesRaw [(int)axis] > axisButtonActivateThreshold)
			dir ++;
		
		if (axesRaw [(int)axis] < -axisButtonActivateThreshold)
			dir --;
		
		if (Mathf.Abs(axesRaw [(int)axis]) < axisButtonResetThreshold)
			dir = 0;

		axisButtonsActive [(int)axis] = (AxisDirection)dir;

		return changed;
	}

	public Vector2 GetAxis2DCircleClamp(MappedAxis axis1, MappedAxis axis2)
	{
		return Vector2.ClampMagnitude( GetAxis2D(axis1,axis2), 1.0f );
	}

	public Vector2 GetAxis2D(MappedAxis axis1, MappedAxis axis2)
	{
		return new Vector2(GetAxis(axis1),GetAxis(axis2));
	}
	
	public Vector2 GetAxisRaw2DCircleClamp(MappedAxis axis1, MappedAxis axis2)
	{
		return Vector2.ClampMagnitude(GetAxisRaw2D(axis1,axis2), 1.0f );
	}

	public Vector2 GetAxisRaw2D(MappedAxis axis1, MappedAxis axis2)
	{
		return new Vector2(GetAxisRaw(axis1),GetAxisRaw(axis2));
	}
	
	public virtual float GetSmoothValue(float lastVal, float currentValRaw, float sensitivity)
	{
		float currentVal = currentValRaw;
		float maxDelta = Time.deltaTime * sensitivity;
		
		if( Mathf.Sign(lastVal) != Mathf.Sign(currentValRaw)) // move faster towards zero
			maxDelta *= 2;
		
		currentVal = Mathf.Clamp( lastVal + Mathf.Clamp(currentVal-lastVal,-maxDelta,maxDelta), -1, 1 );
		
		return currentVal;
	}

}

public enum AxisDirection
{
	Positive = 1,
	Centered = 0,
	Negative = -1
}