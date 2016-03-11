using UnityEngine;
using System.Collections;

public class ClockBehaviour : ItemBehaviour{

	private float recoverablTime = 15f;
	private TimeManager timeManager;

	protected override void Start(){
		//基本処理の呼び出し
		base.Start ();
		//基本クラスで取得済みのmanagerから必要なコンポーネントを取得
		timeManager = manager.GetComponent<TimeManager>();
	}

	protected override void GetItem(){
		//基本処理の呼び出し
		base.GetItem ();
		//時間の処理
		timeManager.ExtendTimeLimit(recoverablTime);
	}
}
