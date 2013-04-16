using UnityEngine;
using System.Collections;

public class InputDevice : MonoBehaviour
{
	public InputDeviceConfig config;
	public int deviceId = -1;
	
	InputAxisState lastAxes = new InputAxisState();
	InputAxisState currentAxes = new InputAxisState();
	InputButtonState lastButtons = new InputButtonState();
	InputButtonState currentButtons = new InputButtonState();
	
	public InputAxisState axis{
		get{
			return currentAxes;
		}
	}
	public InputButtonState buttons{
		get{
			return currentButtons;
		}
	}
	
	public float lastInputTime {get;private set;}
	
	void Awake()
	{
	}
	
	void Update()
	{		
		// update axis
		lastAxes = currentAxes;
		currentAxes = new InputAxisState();

		// go through all axis
		for(int i = 0; i < System.Enum.GetValues(typeof(AxisType)).Length; i++)
			UpdateAxisSmooth( (AxisType)i );

		currentAxes.timestamp = Time.time;
		
		// update buttons
		lastButtons = currentButtons;
		currentButtons = new InputButtonState();

		// go through all buttons (except ButtonType.None)
		int[] buttonValues = (int[]) System.Enum.GetValues(typeof(ButtonType));
		for(int i = 1; i < buttonValues.Length; i++)
			UpdateButton((ButtonType)buttonValues[i]);

		currentButtons.timestamp = Time.time;

		bool axisChanged = false;
		for( int i = 0; i < currentAxes.axisValues.Length; i++)
			axisChanged |= Mathf.Abs(currentAxes.axisValues[i] - lastAxes.axisValues[i]) / Time.deltaTime > 0.1f;

		if( currentButtons.buttons != lastButtons.buttons || axisChanged )
			lastInputTime = Time.time;
	}
	
	public bool GetButton(ButtonType buttonType)
	{
		return currentButtons.GetButton(buttonType);
	}

	public bool GetButtonDown(ButtonType buttonType)
	{
		if( currentButtons.GetButton(buttonType) && !lastButtons.GetButton(buttonType))
			return true;
		
		return false;
	}
	
	public bool GetButtonUp(ButtonType buttonType)
	{
		if( !currentButtons.GetButton(buttonType) && lastButtons.GetButton(buttonType))
			return true;
		
		return false;
	}
	
	void UpdateAxisSmooth(AxisType axis)
	{
		float lastVal = lastAxes.GetAxis(axis);
		float currentVal = GetAxisRaw(axis);
		if( Mathf.Abs(currentVal) < 0.5f )  // set axis to zero if abs value is smaller than this
			currentVal = 0;
		
		float maxDelta = Time.deltaTime*config.sensitivity;
		
		if( Mathf.Sign(lastVal) != Mathf.Sign(currentVal)) // move faster towards zero
		   maxDelta *= 2;
		
		currentVal = Mathf.Clamp( lastVal + Mathf.Clamp(currentVal-lastVal,-maxDelta,maxDelta), -1, 1 );
		currentAxes.SetAxis(axis,currentVal);
	}
	
	public float GetAxisRaw(AxisType axisType)
	{
		string axisStr = "";
		
		axisStr += config.GetAxis(axisType);

		// add new axis here
		
		axisStr = axisStr.Replace("#",deviceId.ToString());
		
		return Input.GetAxisRaw(axisStr);
	}
	
	void UpdateButton(ButtonType buttonType)
	{
		currentButtons.SetButton( buttonType, GetButtonRaw(buttonType) );
	}
	
	bool GetButtonRaw(ButtonType buttonType)
	{		
		string key = "";
		
		key += config.GetButton(buttonType);

		// add new buttons here
		
		if( key == "" )
			return false;
		
		if( config.isJoystick )
			key = "joystick " + deviceId + " " + key;
		
		return Input.GetKey(key);
	}
}

