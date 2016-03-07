using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

	public GameOverManager gameOverManager;
	public Text timeText;
	private float timer = 45f;
	
	// Update is called once per frame
	void Update () {
		//ゲームーオーバーしたならカウントダウンしない
		if(gameOverManager.IsCalled){return;}
		//カウントダウン
		CountDown ();
	}

	//カウントダウン処理
	void CountDown(){
		//タイマーを減らす
		timer -= Time.deltaTime;
		//マイナスにならないようにする
		if(timer < 0){
			timer = 0;
			//ゲームオーバーの呼び出し
			gameOverManager.CallGameOver ("タイムアップ");
		}
		//テキストの更新
		timeText.text = timer.ToString("0");
	}

	//時間の延長
	public void ExtendTimeLimit(float value){
		timer += value;
	}
}
