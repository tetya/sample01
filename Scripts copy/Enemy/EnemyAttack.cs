using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

	public float range_punch = 1f;
	public float range_shooting = 10f;
	public float interval = 3f;
	public AudioSource soundPunch;
	public AudioSource soundShoot;
	public GameObject bulletPrefab;
	public GameObject hand;
	private Transform tarCenTra;
	private float intervalCounter = 0;
	private Animator animator;
	private GameOverManager gameOverManager;

	// Use this for initialization
	void Start () {
		//HierarchyからGameOverManagerを見つけて代入
		GameObject manager = GameObject.FindWithTag("Manager");
		gameOverManager = manager.GetComponent<GameOverManager>();
		//HierarchyからPlayerを見つけて代入
		GameObject player = GameObject.FindWithTag("Player");
		tarCenTra = player.transform.Find ("CenterPosition");//<! オブジェクト名
		//アニメーターの取得
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		//古いトリガーをオフにする
		animator.ResetTrigger ("Attack1Trigger");
		animator.ResetTrigger ("Attack2Trigger");
		//ゲームオーバーの監視
		if(gameOverManager.IsCalled){return;}
		//インターバルの更新
		CounteInterval ();
		//攻撃の処理
		Search ();
	}

	void CounteInterval(){
		intervalCounter += Time.deltaTime;
		intervalCounter = Mathf.Clamp (intervalCounter, 0f, interval);
	}

	void Search (){
		//プレイヤーに向けてレイを飛ばす
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, range_shooting)) {
			//レイの当たったコライダーがPlayerの場合（＝射線が確保されている）
			if (hit.collider.tag == "Player") {
				Attack (hit.collider.transform.position);
			}
		}
	}

	void Attack(Vector3 playerPos){
		//プレイヤーとの距離で攻撃手段を変更
		if(Vector3.Distance(transform.position, playerPos) < range_punch){
			//パンチ攻撃
			animator.SetTrigger ("Attack2Trigger");
		}else{
			//インターバルを確認
			if(intervalCounter == interval){
				//射撃攻撃
				animator.SetTrigger ("Attack1Trigger");
				//インターバルのカウントをリセット
				intervalCounter = 0f;
			}
		}
	}

	//霊夢パンチ（AnimationEventで呼び出す）
	public void ReimuPunch(){
		//エフェクトと判定をアクティブにする
		hand.SetActive (true);
	}

	//ReimuPunchで使用
	public void SoundPunch(){
		//効果音
		soundPunch.Play ();
	}

	//ReimuPunchで使用
	public void ResetPunch(){
		//非アクティブに戻す
		hand.SetActive (false);
	}

	//霊夢ショット（AnimationEventで呼び出す）
	public void ReimuShoot(){
		//効果音
		soundShoot.Play ();
		//発射座標から目標座標への回転ベクトルを作成
		Vector3 direction = tarCenTra.position - hand.transform.position;
		Quaternion rotation = Quaternion.LookRotation(direction);
		//オブジェクトの生成
		GameObject bullet = Instantiate (bulletPrefab, hand.transform.position, rotation) as GameObject;//<! 情報を渡したいのでインスタンス化
		bullet.GetComponent<BulletBehaviour>().SetShooterTag(tag);
	}
}
