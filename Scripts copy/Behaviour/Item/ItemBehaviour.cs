using UnityEngine;
using System.Collections;

public class ItemBehaviour : MonoBehaviour {

	private SpawnManager spawn; //<! SpawnManagerで設定
	protected GameObject manager;

	// 派生先で処理を拡張する
	protected virtual void Start () {
		//HierarchyからScoreManagerを見つけ、管理クラスをlifeManagerに代入
		manager = GameObject.FindWithTag("Manager");
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
			GetItem ();
		}
	}

	protected virtual void GetItem(){
		//対応するスポーンのカウンターを減らす
		spawn.counter--;
		//SEを鳴らす
		spawn.PlaySE();
		//このオブジェクトを破壊
		Destroy (gameObject);
	}
}
