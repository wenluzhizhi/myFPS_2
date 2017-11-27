using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


public class MoveControllerPanel:UIBehaviour,IPointerClickHandler,IDragHandler,IBeginDragHandler,IEndDragHandler,IPointerUpHandler,IPointerDownHandler  {



	#region IPointerDownHandler implementation

	public void OnPointerDown (PointerEventData eventData)
	{
		//isDraging = true;
	}

	#endregion

	#region IPointerUpHandler implementation

	public void OnPointerUp (PointerEventData eventData)
	{
		
		//isDraging = false;
	}

	#endregion

	

	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		isDraging = true;
		HightLightTime = HightLightTimeMax;
	}


	public void OnDrag (PointerEventData eventData)
	{
		
		Vector2 v= eventData.delta;
		if (isDraging)
			centerImageRect.anchoredPosition += v*0.4f;
	}
	public void OnEndDrag (PointerEventData eventData)
	{
		isDraging = false;
	}



	#endregion




	private float HightLightTime = 0.0f;
	public float HightLightTimeMax = 60.0f;
	public RectTransform centerImageRect;
	private Image centerImage;

	#region IPointerClickHandler implementation
	public void OnPointerClick (PointerEventData eventData)
	{
		
	}
	#endregion

	public Image MainPanel;
	public bool isDraging = false;
	public Vector2 MoveRate = Vector2.zero;
	public Action<Vector2> moveAc;
	void Start () {
		centerImage = centerImageRect.gameObject.GetComponent<Image> ();
	}
	

	void Update () {
		posOriginalPos ();

		if (MoveRate != centerImageRect.anchoredPosition) {
			MoveRate = centerImageRect.anchoredPosition;
			if (moveAc != null) {
				moveAc (MoveRate);
			}
		}
		CaclulatePanelColor ();
	}
	string str1="";
	void OnGUI1(){
		GUILayout.Label ("===="+str1);
	}
	private Vector2 centerPos=new Vector2(0,0);
	private void posOriginalPos()
	{
		if (!isDraging) 
		{
			str1 = ""+Time.time;
			Vector2 v = centerImageRect.anchoredPosition;
			if (Vector2.Distance (v, centerPos) < 0.01) 
			{
				centerImageRect.anchoredPosition = centerPos;
			} 
			else
			{
				centerImageRect.anchoredPosition = Vector2.Lerp (v, centerPos, Time.deltaTime * 5);
			}
		}
	}

	private void CaclulatePanelColor(){
		HightLightTime -= Time.deltaTime;
		if (HightLightTime <= 0.0) {
			HightLightTime = 0.0f;
		}
		centerImage.color = new Color (1,1,1,HightLightTime/HightLightTimeMax);
	}
}
