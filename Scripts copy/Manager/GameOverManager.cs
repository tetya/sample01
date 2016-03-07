using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {

	public GameObject player;
	public GameObject gameOverObj;
	public Text textGameOver;
	public Text textMassage;
	public ScoreManager scoreManager;
	private Animator animator;
	private bool isAccepting = false;

	public bool IsCalled{ get; private set;}

	void Start(){
		animator = gameOverObj.GetComponent<Animator>();
		IsCalled = false;
	}

	public void CallGameOver(string str = "ゲームオーバー"){
		//外部からゲームオーバーが呼び出されたことを分かるようにする
		IsCalled = true;
		//テキストの更新
		textGameOver.text = str;
		WriteMassage ();
		//プレイヤーの入力を受け取るクラスを非アクティブに（敵の行動は対応するクラス側でUpdateをreturnしている）
		player.GetComponent<PlayerMovement>().enabled=false;
		player.GetComponent<CharacterChanger>().enabled=false;
		Camera.main.GetComponent<CameraMovement_Mouse> ().enabled = false;
		//CanvasのGameOverをアクティブに
		gameOverObj.SetActive(true);
		//GameOverのアニメーションを再生
		animator.SetTrigger("gameOver");
		//入力受付の予約
		Invoke("EndAnimation",3f);
	}

	void Update(){
		//コンティニューの受付
		Continue ();
	}

	void Continue(){
		//アニメーションが終わったらキー入力を受け付ける
		if(isAccepting){
			//クリックでコンティニュー
			if(Input.GetButtonDown("Fire1")){
				//シーンの再読み込み
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}

	void WriteMassage(){
		if (scoreManager.Score < 300) {
			textMassage.text = "骨折り損の\nくたびれ儲け";
		} else {
			textMassage.text = "あなたが神か";
		}
	}

	//Invokeで呼び出し
	void EndAnimation(){
		isAccepting = true;
	}

	//プレイヤーの死亡処理
	void RemovePlayer(){
		//妖精たちと影を非アクティブに

		//パーティクルシステムを再生
	}
}
