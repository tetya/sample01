using UnityEngine;
using System.Collections;

public class CameraFollowing : MonoBehaviour {

	public Transform target;
	public float distance = 1f;
	private Vector3 tarPosXZ;//<! ワールド空間
	private Vector3 camPosXZ;//<! ワールド空間
	private Vector3 direction;//<! ワールド空間

	void Start(){
		//初期位置の設定
		float x = target.transform.position.x;
		float y = transform.position.y;//<! yはカメラの高さのままにしておく
		float z = target.transform.position.y - distance;//<! 距離分引く
		transform.position.Set (x, y, z);
		//targetのxz平面座標の用意
		tarPosXZ = target.transform.position;
		tarPosXZ.y = 0;
		//カメラのxz平面座標の用意
		camPosXZ = transform.position;
		camPosXZ.y = 0;
		//カメラからターゲットに向かう方向ベクトル
		direction = tarPosXZ - camPosXZ;
	}

	void Update() {
		//ベクトルの更新
		UpdateVector ();
		//ターゲットの方を向く
		LookTargetXZ ();
		//ターゲットに追従
		Follow ();
	}

	void UpdateVector(){
		//tarPosXZの更新
		tarPosXZ.x = target.transform.position.x;
		tarPosXZ.z = target.transform.position.z;
		//camPosXZの更新
		camPosXZ.x = transform.position.x;
		camPosXZ.z = transform.position.z;
		//方向ベクトルの更新と正規化
		direction = tarPosXZ - camPosXZ;
		direction.Normalize();
	}

	void LookTargetXZ(){
		//ローカルXZ空間におけるtargetの座標を取得
		Vector3 localTarPosXZ = transform.InverseTransformPoint(tarPosXZ);
		//ローカル座標の原点(=カメラ)とtargetの座標の角度を取得
		float angle = Mathf.Atan2(localTarPosXZ.z, localTarPosXZ.x);
		//Z軸(0,0,1)との成す角度が知りたいので90°減算
		angle -= Mathf.PI / 2;
		//Quaternionは時計回りが正なので、得られた向きを逆転させる
		angle *= -1;
		//Rotateメドッドを使用するのでオイラーに変換
		angle *= Mathf.Rad2Deg;
		//ワールド座標でカメラを回転
		transform.Rotate(Vector3.up, angle, Space.World);
		//transform.rotation *= Quaternion.AngleAxis(angle, upVector);//<! transform.rotationの空間に置き換えたとき、ちょうど真上を向くベクトルが必要
		//transform.rotation = Quaternion.LookRotation (direction);//<! 部分的なパラメータではなく全て帰るので、カメラの傾きも上書きしてしまう
	}

	void Follow(){
		//現在の距離と指定距離との差を調べる
		float difference = Vector3.Distance (camPosXZ, tarPosXZ) - distance;
		//ターゲット方向に距離を詰める
		transform.Translate (direction * difference, Space.World);//<! forwardを使うと、カメラがキャラを向いていない場合移動するほどキャラから離れる
	}
}
