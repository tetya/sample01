using UnityEngine;
using System.Collections;

public class ClockBehaviour : MonoBehaviour {

	private float recoverablTime = 15f;
	private SpawnManager spawn; //<! SpawnManagerで設定
	private TimeManager timeManager;

	void Start(){
		//HierarchyからScoreManagerを見つけ、管理クラスをlifeManagerに代入
		GameObject manager = GameObject.FindWithTag("Manager");
		timeManager = manager.GetComponent<TimeManager>();
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


	void OnTriggerEnter(Collider other){
		//Playerタグを持っているか確認
		if(other.tag == "Player"){
			GetClock ();
		}
	}

	void GetClock(){
		//対応するスポーンのカウンターを減らす
		spawn.counter--;
		//スコアの処理
		timeManager.ExtendTimeLimit(recoverablTime);
		//このオブジェクトを破壊
		Destroy (gameObject);
	}
}
