using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityStandardAssets.Characters.FirstPerson;

public class FirstPlayerController : MonoBehaviour {

	[SerializeField] private Transform myTransform;
	[SerializeField] private RectTransform moveCenter;
	[SerializeField] private FirstPersonController fspersion;
	[SerializeField] private Transform myGun;
	[SerializeField] private MoveControllerPanel gunMoveController;


	[SerializeField] private Transform gunPos;
	[SerializeField] private Transform zhunxing;

	public Canvas UICanvas;
	private Camera UICamera;

	[SerializeField] private RectTransform CrossHairImage;
	[SerializeField] private RectTransform myCanvas;
	[SerializeField] private AudioSource ad;
	void Start ()
	{
		myTransform = this.transform;
		gunMoveController.moveAc += gunMove;
		UICamera = UICanvas.worldCamera;

		if (UICamera == null) {
			Debug.Log ("UICamera==null");
		} else {
			Debug.Log ("UICamera!=null");
		}
	}
	
	RaycastHit rayHit;
	void Update () 
	{
		/*
		if (Input.GetKey (KeyCode.W)) {
			playerForward ();
		} else if (Input.GetKey (KeyCode.S)) {
			playerRetreat ();
		} else {
			fspersion.verticalMove=0.0f;
		}

		if (Input.GetKey (KeyCode.A)) {
			rotateLeft ();
		} else if (Input.GetKey (KeyCode.D)) {
			rotateRight ();
		} else {
			fspersion.horizontalMove = 0.0f;
		}
        */

		if (moveCenter.anchoredPosition.x > 10) {
			rotateRight ();
		} else if (moveCenter.anchoredPosition.x < -10) {
			rotateLeft ();
		} else {
			FirstPersonController.horizontalMove = 0.0f;
		}

		if (moveCenter.anchoredPosition.y > 10) {
			playerForward ();
		} else if (moveCenter.anchoredPosition.y < -10) {
			playerRetreat ();
		} else {
			FirstPersonController.verticalMove = 0.0f;
			
		}


		#region 射线检查
	
		if(Physics.Raycast(gunPos.position,gunPos.transform.forward,out rayHit,1000)){
			//Debug.Log(rayHit.transform.gameObject.name);
			Vector3 v=rayHit.point;
			zhunxing.position=v;
			CrossHairImage.gameObject.SetActive(true);

		
			screenPos=RectTransformUtility.WorldToScreenPoint(Camera.main,v);

			RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas,screenPos,null,out imageVe);
			//RectTransformUtility.

			CrossHairImage.anchoredPosition=imageVe;
			if(rayHit.transform.gameObject.tag=="enemy")
			{
				if(!ad.isPlaying)
				{
				  ad.Play();
				}
				rayHit.transform.transform.gameObject.GetComponent<EnemyAI>().hitUp();
			}


		}
		else{
			CrossHairImage.gameObject.SetActive(false);
		}

		#endregion

	}
	public  Vector2 imageVe;
	public Vector3 screenPos;

	#region  内部回调用函数
	public float yy1 = 0.0f;
	public Vector2 vv1;
	public float kk = 0;
	public void gunMove(Vector2 v){
		vv1 = v;
		float y1 = myGun.transform.localRotation.eulerAngles.y;
		float x1 = myGun.transform.localRotation.eulerAngles.x;
		if (y1 >= 180 && y1 < 360) {
			y1 = y1 - 360;
		}
		yy1 = y1;


		float _x = 0.0f;
		float _y = 0.0f;
		if (v.x > 10&&yy1<20) {
			_x = 1;
		} 
		else if(v.x<-10&&yy1>-20)
		{
			_x = -1;
		}




		if (x1 >= 180 && x1 < 360) {
			x1 = x1 - 360;
		}
		if (v.y> 10&&x1>-30) {
			_y = 1;
		} 
		else if(v.y<-10&&x1<10)
		{
			_y = -1;
		}



		kk = _x;
		myGun.transform.Rotate (new Vector3 (-_y,_x,0));

	}

	#endregion



	#region  内部函数


	private void rotateLeft(){
		//myTransform.Rotate (new Vector3 (0, -1, 0), Space.World);
		//fspersion.horizontalMove = 1.0f;
		FirstPersonController.horizontalMove=-1.0f;
	}

	private void rotateRight(){
		//myTransform.Rotate (new Vector3 (0, 1, 0), Space.World); 
		//fspersion.horizontalMove = -1.0f;
		FirstPersonController.horizontalMove=1.0f;
	}


	private void playerForward(){
		//myTransform.position += transform.forward;
		//fspersion.verticalMove=1.0f;
		FirstPersonController.verticalMove=1.0f;
	}

	private void playerRetreat(){
		//myTransform.position -= transform.forward;
		//fspersion.verticalMove=-1.0f;
		FirstPersonController.verticalMove=-1.0f;
	}


	public void UpliftGun(){
		
	}

	public void lowerGun(){
		
	}


	#endregion



	#region  external

	public void OnScrollValuechage(Vector2 v){
		
	}


	#endregion

}
