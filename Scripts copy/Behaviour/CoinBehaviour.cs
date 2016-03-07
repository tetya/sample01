using UnityEngine;
using System.Collections;

public class CoinBehaviour : MonoBehaviour {

	public ParticleSystem parSys;
	public int score = 10;//<! コインによる獲得スコア
	private SpawnManager spawn; //<! SpawnManagerで設定
	private string [] objectNames = {"SunnySD","LunaSD","StarSD"};//<! Hierarchy上のオブジェクト名を変更の際は注意
	private int gettableIndex = 0;//<! コインを入手できるキャラクターのindex。objectNamesに対応。
	private ScoreManager scoreManager;

	void Start(){
		//HierarchyからScoreManagerを見つけ、管理クラスをscoreManagerに代入
		GameObject manager = GameObject.FindWithTag("Manager");
		scoreManager = manager.GetComponent<ScoreManager>();
		//コインを入手できるキャラクターをランダムで決定
		gettableIndex = Random.Range (0, objectNames.Length);
		//パーティクルの色を変える
		SetCollor(gettableIndex);
	}

	// Update is called once per frame
	void Update () {
		//回転の演出
		transform.Rotate(Vector3.up, 5, Space.World);
	}

	//このコインをInstantiateしたSpawnManagerを受け取る
	public void SetSpawnManager(SpawnManager spaMan){
		spawn = spaMan;
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

	void OnTriggerEnter(Collider other){
		//Playerタグを持っているか確認
		if(other.tag == "Player"){
			//このコインを入手可能なキャラが現在アクティブか確認
			Transform chaTra = other.transform.Find("Character");
			GameObject obj = chaTra.Find (objectNames [gettableIndex]).gameObject;
			if (obj.activeSelf) {
				//コインの入手
				GetCoin();
			}
		}
	}

	void GetCoin(){
		//対応するスポーンのカウンターを減らす
		spawn.counter--;
		//スコアの処理
		scoreManager.GetScore(score);
		//このオブジェクトを破壊
		Destroy (gameObject);
	}
}
