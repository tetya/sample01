using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour { 

	public float slowSpeed;
	private GameObject player;
	private NavMeshAgent nav;
	private Animator animator;
	private int currentPath=0;
	private GameOverManager gameOverManager;
	readonly int attack1Path = Animator.StringToHash ("Base Layer.Attack1");
	readonly int attack2Path = Animator.StringToHash ("Base Layer.Attack2");

	// Use this for initialization
	void Start () {
		//HierarchyからGameOverManagerを見つけて代入
		GameObject manager = GameObject.FindWithTag("Manager");
		gameOverManager = manager.GetComponent<GameOverManager>();
		//HierarchyからPlayerを見つけて代入
		player = GameObject.FindWithTag("Player");
		//コンポーネントの取得
		nav = GetComponent <NavMeshAgent> ();
		animator = GetComponent <Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		//ゲームオーバーの監視
		if(gameOverManager.IsCalled){
			nav.enabled = false;
			return;
		}
		//常にプレイヤーを向かせる
		transform.LookAt(player.transform.position);
		//目的地の更新（Playerの現在の位置）
		nav.SetDestination (player.transform.position);
		//アニメーターに現在の速度を渡す
		animator.SetFloat("Speed", nav.velocity.magnitude);//<! 値が荒ぶるのでsqrMagnitudeを使えず
		//アニメーション中の挙動
		BehaviourOnAnimation();
	}


	void BehaviourOnAnimation(){
		//現在再生しているアニメーションが攻撃モーションか確認
		currentPath = animator.GetCurrentAnimatorStateInfo (0).fullPathHash;
		if (currentPath == attack1Path || currentPath == attack2Path) {
			nav.velocity = nav.velocity.normalized * slowSpeed;
		}
	}
}
