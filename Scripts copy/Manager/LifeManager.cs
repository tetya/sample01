using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour {
	
	public GameObject hearts;
	public GameOverManager gameOverManager;
	private int limit = 8;
	private int startMax = 3;
	private int currentHeart;
	private int currentMax;
	private Color normalColor = new Color(1f, 1f, 1f, 1f);
	private Color damagedColor = new Color(1f, 1f, 1f, 0.5f);

	void Start(){
		//現在のライフを指定の値に設定
		currentHeart = startMax;
		//現在の最大ライフを指定の値に設定
		currentMax = startMax;
	}

	//回復処理（2以上の回復の場合は、呼び出し元で複数回呼び出すこと）
	public void Recover(){
		//満タンでないときのみ処理を実行
		if(currentHeart != currentMax){
			//ライフを回復させる
			currentHeart++;
			//アイコンを不透明にする
			Transform tra = hearts.transform.Find ("Heart " + currentHeart);
			tra.GetComponent<Image> ().color = normalColor;
		}
	}

	//ダメージ処理（2以上のダメージの場合は、呼び出し元で複数回呼び出すこと）
	public void Damage(){
		//0でないときのみ処理を実行
		if(currentHeart != 0){
			//先にアイコンを半透明にする（例:3→2の場合、半透明になるのは"Heaet 3"）
			Transform tra = hearts.transform.Find ("Heart " + currentHeart);
			tra.GetComponent<Image> ().color = damagedColor;
			//ライフを減らす
			currentHeart--;
			//ライフが０ならゲームオーバー
			if(currentHeart == 0){
				GameOver ();
			}
		}
	}

	//最大ライフの増加
	public void MaxUp(){
		//上限に達してないときのみ処理を実行
		if(currentMax != limit){
			//ライフ上限を１つ増やす
			currentMax++;
			//対応するスプライトのtransformを取得
			Transform tra = hearts.transform.Find("Heart " + currentMax);
			//アクティブにする
			tra.gameObject.SetActive (true);
			//新たに増えたハートは半透明にする
			tra.GetComponent<Image> ().color = damagedColor;
		}
		//回復効果は常に発揮
		Recover();
	}

	void GameOver(){
		if(!gameOverManager.IsCalled){
			gameOverManager.CallGameOver ();
		}
	}
}
