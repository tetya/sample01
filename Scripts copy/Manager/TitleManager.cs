using UnityEngine;
using System.Collections;

//画面のボタンを押させることで、カーソルを画面内に捕える
public class TitleManager : MonoBehaviour {

	public GameObject title;

	// Use this for initialization
	void Start () {
		//カーソルのロック解除（コンテニュー時用）
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		//マウス入力のUpdateを止める
		Camera.main.GetComponent<CameraMovement_Mouse> ().enabled = false;
		//ゲームの進行を止める
		Time.timeScale = 0;
	}

	public void GameStart(){
		//マウス入力のUpdateを開始
		Camera.main.GetComponent<CameraMovement_Mouse> ().enabled = true;
		//タイトルを消す
		title.SetActive (false);
		//時間停止を解除
		Time.timeScale = 1;
	}
}
