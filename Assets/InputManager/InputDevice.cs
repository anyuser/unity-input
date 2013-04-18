using UnityEngine;
using System.Collections;

public class InputDevice : MonoBehaviour
{
	public InputDeviceConfig config;
	public int deviceId = -1;
	
	InputState lastState = new InputState();
	InputState currentState = new InputState();
	
	public float[] axes{
		get{
			return currentState.axisValues;
		}
	}
	public ButtonType buttons{
		get{
			return currentState.buttons;
		}
	}
	
	public InputState state
	{
		get{
			return currentState;
		}
	}
	
	public float lastInputTime {get;private set;}
	
	void Awake()
	{
	}
	
	void Update()
	{		
		// update state
		lastState = currentState;
		currentState = new InputState();
		currentState.timestamp = Time.time;

		// update axes
		for(int i = 0; i < System.Enum.GetValues(typeof(AxisType)).Length; i++)
			UpdateAxisSmooth( (AxisType)i );
		
		// update buttons (except ButtonType.None)
		int[] buttonValues = (int[]) System.Enum.GetValues(typeof(ButtonType));
		for(int i = 1; i < buttonValues.Length; i++)
			UpdateButton((ButtonType)buttonValues[i]);

		// check if state has changed
		if( StateChanged(lastState,currentState) )
			lastInputTime = Time.time;
	}
	
	bool StateChanged(InputState s1, InputState s2)
	{
		if( s1.buttons != s2.buttons )
			return true;
		
		bool axisChanged = false;
		for( int i = 0; i < s1.axisValues.Length; i++)
			axisChanged |= Mathf.Abs(s1.axisValues[i] - s2.axisValues[i]) / Time.deltaTime > 0.1f;

		if( axisChanged )
			return true;
		
		return false;
	}
	
	void UpdateAxisSmooth(AxisType axis)
	{
		float lastVal = lastState.GetAxis(axis);
		float currentVal = GetAxisRaw(axis);
		
		currentVal = Mathf.InverseLerp(config.deadZone,1,Mathf.Abs(currentVal)) * Mathf.Sign(currentVal); // apply dead zone (todo: radial dead zone based on 2d distance)
		
		float maxDelta = Time.deltaTime*config.sensitivity;
		
		if( Mathf.Sign(lastVal) != Mathf.Sign(currentVal)) // move faster towards zero
		   maxDelta *= 2;
		
		currentVal = Mathf.Clamp( lastVal + Mathf.Clamp(currentVal-lastVal,-maxDelta,maxDelta), -1, 1 );
		currentState.SetAxis(axis,currentVal);
	}
	
	void UpdateButton(ButtonType buttonType)
	{
		currentState.SetButton( buttonType, GetButtonRaw(buttonType) );
	}
	
	public bool GetButton(ButtonType buttonType)
	{
		return currentState.GetButton(buttonType);
	}

	public bool GetButtonDown(ButtonType buttonType)
	{
		if( currentState.GetButton(buttonType) && !lastState.GetButton(buttonType))
			return true;
		
		return false;
	}
	
	public bool GetButtonUp(ButtonType buttonType)
	{
		if( !currentState.GetButton(buttonType) && lastState.GetButton(buttonType))
			return true;
		
		return false;
	}
	
	public float GetAxisRaw(AxisType axisType)
	{
		string axisStr = config.GetAxis(axisType);
		if( config.isJoystick )
		{
			axisStr = axisStr.Replace("#",deviceId.ToString());
			return Input.GetAxisRaw(axisStr);
		}
		else
		{
			string[] axisStringSplit = axisStr.Split(' ');
			int axisVal = 0;
			if( Input.GetKey(axisStringSplit[0]) )
				axisVal++;
			if( Input.GetKey(axisStringSplit[1]) )
				axisVal--;
			return axisVal;
		}
	}
	
	bool GetButtonRaw(ButtonType buttonType)
	{		
		string key = "";
		
		key += config.GetButton(buttonType);
		
		if( key == "" )
			return false;
		
		if( config.isJoystick )
			key = "joystick " + deviceId + " " + key;
		
		return Input.GetKey(key);
	}
}

