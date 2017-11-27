using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class Main12 : MonoBehaviour {

	public Canvas main1;
	public EventSystem ve;
	void Start () {
		
	}

	void OnGUI(){
		if (GUILayout.Button ("button")) {
			Camera c1= main1.worldCamera;
		
			if (c1 == null) {
				Debug.Log ("c1==null");
			} else {
				Debug.Log ("c1!=null");
			}
		}
	}
	

	void Update () {
	
	}
}
