using UnityEngine;
using System.Collections;

public class PunchBehaviour : MonoBehaviour {

	public float time_Destroy = 0.5f;
	public LifeManager lifeManager;//このオブジェクトはHierarchyに常に存在するので直接参照可能
	public GameObject attacker;
	private string attackerTag;

	void Start(){
		attackerTag = attacker.tag;
	}

	void OnTriggerEnter(Collider other){
		//ヒット先が発射元の陣営の場合は何もせず処理を終了
		if(other.tag == attackerTag){return;}
		//プレイヤーの場合はダメージ処理
		if(other.tag == "Player"){
			//一度に２ダメージ
			lifeManager.Damage();
			lifeManager.Damage();
		}
	}
}
