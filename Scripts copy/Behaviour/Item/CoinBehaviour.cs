using UnityEngine;
using System.Collections;

public class CoinBehaviour : ItemBehaviour {

	public ParticleSystem parSys;
	public int score = 10;//<! コインによる獲得スコア
	private GameObject [] characters = new GameObject[3];
	private int gettableIndex = 0;//<! コインを入手できるキャラクターのindex。objectNamesに対応。
	private ScoreManager scoreManager;

	protected override void Start(){
		//基本処理の呼び出し
		base.Start ();
		//基本クラスで取得済みのmanagerから必要なコンポーネントを取得
		scoreManager = manager.GetComponent<ScoreManager>();
		//現在のキャラクターの取得
		GameObject player = GameObject.FindWithTag("Player");
		Transform chaTra = player.transform.Find("Character");
		characters[0] = chaTra.Find ("SunnySD").gameObject;
		characters[1] = chaTra.Find ("LunaSD").gameObject;
		characters[2] = chaTra.Find ("StarSD").gameObject;
		//コインを入手できるキャラクターをランダムで決定
		gettableIndex = Random.Range (0, characters.Length);
		//パーティクルの色を変える
		SetCollor(gettableIndex);
	}

	void SetCollor(int index){
		//インデックスに応じてパーティクルの色を変更
		switch(index){
		case 0:
			parSys.startColor = Color.red;
			break;
		case 1:
			parSys.startColor = Color.white;
			break;
		case 2:
			parSys.startColor = Color.blue;
			break;
		}
	}

	protected override void GetItem(){
		//アクティブならアイテム入手の処理を行う
		if (characters[gettableIndex].activeSelf) {
			//基本処理の呼び出し
			base.GetItem();
			//スコアの処理
			scoreManager.GetScore(score);
		}
	}
}
