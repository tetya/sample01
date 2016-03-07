using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public Text scoreText; 
	public int Score { get; private set;}

	void Start(){
		Score = 0;
	}


	//スコア獲得時の処理
	public void GetScore(int point){
		Score += point;
		scoreText.text = Score.ToString("#,0");
	}
}
