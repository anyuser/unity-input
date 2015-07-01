using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// GUI for SceneEditor.
/// </summary>
/// /

[CustomEditor(typeof(InputDevice),true)]
public class InputDeviceEditor : Editor
{	
	void Start()
	{
	}

	void OnDestroy()
	{
	}

	public override void OnInspectorGUI(){	

		base.OnInspectorGUI ();

		InputDevice device = (InputDevice)target;

		GUILayout.Label ("Type: "+device.GetType().ToString());

		GUILayout.Label ("Mapped Axes", EditorStyles.boldLabel);
		for(int i = 0; i < InputDevice.allMappedAxisTypes.Count; i++)
			GUILayout.Label( string.Format("{0} ({1}): {2}",InputDevice.allMappedAxisTypes[i],device.GetAxisName(InputDevice.allMappedAxisTypes[i]),device.GetAxis(InputDevice.allMappedAxisTypes[i])));

		GUILayout.Space (10);
		
		GUILayout.Label ("Mapped Buttons", EditorStyles.boldLabel);

		for(int i = 0; i < InputDevice.allMappedButtonTypes.Count; i++)
			GUILayout.Label( string.Format("{0} ({1}): {2}",InputDevice.allMappedButtonTypes[i],device.GetButtonName(InputDevice.allMappedButtonTypes[i]),device.GetButton(InputDevice.allMappedButtonTypes[i])));

	}

	public override bool RequiresConstantRepaint ()
	{
		return true;
	}
}