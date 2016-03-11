using UnityEngine;
using System.Collections;

public class PunchBehaviour : MonoBehaviour {

	public float time_Destroy = 0.5f;
	private LifeManager lifeManager;//このオブジェクトはHierarchyに常に存在するので直接参照可能

	void Start(){
		//HierarchyからLifeManagerを見つけて代入
		GameObject manager = GameObject.FindWithTag("Manager");
		lifeManager = manager.GetComponent<LifeManager>();
	}

	void OnTriggerEnter(Collider other){
		//ヒット先が発射元の陣営の場合は何もせず処理を終了（このキャラがEnemyであることは不変）
		if(other.tag == "Enemy"){return;}
		//プレイヤーの場合はダメージ処理
		if(other.tag == "Player"){
			//一度に２ダメージ
			lifeManager.Damage();
			lifeManager.Damage();
		}
	}
}