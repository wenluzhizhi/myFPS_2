using UnityEngine;
using System.Collections;

public enum EnemyState{
	idle,run,attack,death,
}

public class EnemyAI : MonoBehaviour
{

	public Animator AIanimator;
	public EnemyState state =EnemyState.idle;
	public float moveTime = 0.0f;
	public float maxLength = 0.0f;
	public float currentLength=0.0f;
	public Transform MainPlayer;
	void Start ()
	{
	
	}

	public void hitUp(){
		if (state != EnemyState.death) {
			AIanimator.SetTrigger ("death");
			state = EnemyState.death;
			Invoke ("destroy", 2.0f);
		}
	}

	private void destroy(){
		transform.position = new Vector3 (Random.Range(-100,0),30,Random.Range(-5,5));
		state =EnemyState.idle;
	}

	void Update () 
	{
		moveTime -= Time.deltaTime;
		currentLength = Vector3.Distance (transform.position, MainPlayer.transform.position);
		if (currentLength > maxLength) {
			transform.LookAt (MainPlayer);
		}
		if (moveTime <= 0) {
			moveTime = Random.Range (5, 10);
			if (state == EnemyState.idle)
			{
				state = EnemyState.run;
				AIanimator.SetTrigger ("run");
				transform.Rotate(new Vector3(0,Random.Range(90,270),0));
			}
			else  if(state == EnemyState.run) 
			{
				state = EnemyState.idle;
				AIanimator.SetTrigger ("idle");
			}
		}
		if (state == EnemyState.idle) 
		{
			

		} else if (state == EnemyState.run) {
			transform.Translate (transform.forward*0.1f, Space.World);

		}
	}
}
