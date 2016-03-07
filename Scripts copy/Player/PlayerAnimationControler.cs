using UnityEngine;
using System.Collections;

public class PlayerAnimationControler : MonoBehaviour {

	private Animator animator;

	void Start(){
		animator = GetComponent<Animator>();
	}
		
	void Update () {
		Move();
	}

	void Move(){
		bool isMoving = Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0;
		animator.SetBool ("Moving", isMoving);
		animator.SetBool ("Running", isMoving);
	}

}
