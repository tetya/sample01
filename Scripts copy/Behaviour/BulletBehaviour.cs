using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {

	public float speed = 20f;
	public float time_Destroy = 2f;
	private Rigidbody bulRig;
	private string shooterTag;
	private LifeManager lifeManager;

	// Use this for initialization
	void Start () {
		//HierarchyからScoreManagerを見つけ、管理クラスをlifeManagerに代入
		GameObject manager = GameObject.FindWithTag("Manager");
		lifeManager = manager.GetComponent<LifeManager>();
		//進行方向の設定
		bulRig = GetComponent<Rigidbody> ();
		bulRig.velocity = transform.forward * speed;
		//指定時間経過で消えるようにしておく
		Destroy(gameObject, time_Destroy);
	}

	public void SetShooterTag(string tag){
		//発射元のタグを受け取る
		shooterTag = tag;
	}

	void OnTriggerEnter(Collider other){
		//ヒット先が発射元の陣営の場合は何もせず処理を終了
		if(other.tag == shooterTag){return;}
		//ヒット先がアイテムの場合は何もせず処理を終了
		if(other.tag == "Item"){return;}
		//プレイヤーの場合はダメージ処理
		if(other.tag == "Player"){lifeManager.Damage();}
		//発射元以外に当たったので、この弾丸を消す
		Destroy (gameObject);
	}
}
