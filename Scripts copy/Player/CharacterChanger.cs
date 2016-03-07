using UnityEngine;
using System.Collections;

public class CharacterChanger : MonoBehaviour {

	public Avatar [] avatars = new Avatar[3];
	public GameObject [] characters = new GameObject[3];
	private int index = 0;
	private Animator playerAnimator;

	//アタッチされているPlayerのAnimatorを取得
	void Start(){
		playerAnimator = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		Change ();
	}

	//キャラクターの切り替え
	void Change(){
		if(Input.GetButtonDown("Fire2")){
			index++;
			//上限を超えたら0に戻す
			if(index == 3){index = 0;}
			//まず全て非アクティブにする
			foreach(GameObject gamObj in characters){
				gamObj.SetActive (false);
			}
			//GameObjectのアクティブとAvatarを対応させる
			characters [index].SetActive (true);
			playerAnimator.avatar = avatars[index];
		}
	}
}
