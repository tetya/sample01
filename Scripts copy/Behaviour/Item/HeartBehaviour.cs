using UnityEngine;
using System.Collections;

public class HeartBehaviour : ItemBehaviour {

	private LifeManager lifeManager;

	protected override void Start(){
		//基本処理の呼び出し
		base.Start ();
		//基本クラスで取得済みのmanagerから必要なコンポーネントを取得
		lifeManager = manager.GetComponent<LifeManager>();
	}

	protected override void GetItem(){
		//基本処理の呼び出し
		base.GetItem();
		//スコアの処理
		lifeManager.Recover();
	}
}
