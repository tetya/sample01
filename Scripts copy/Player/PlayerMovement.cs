using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	[SerializeField]
	private float speed =3.5f;
	private Rigidbody playerRig;
	private Vector3 movement;//<! 移動先の座標（Updateで更新する）

	// Use this for initialization
	void Start () {
		playerRig = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		//移動先座標の更新
		UpdateMovement ();
		//移動
		Move();
		//回転
		Turn();
	}

	void UpdateMovement(){
		//ワールド空間でのカメラのforwardを取得、XZ成分を抽出したもので正規化
		Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
		forward.y = 0;
		forward.Normalize();
		//ワールド空間でのカメラのrigntを取得し、XZ成分を抽出したもので正規化
		Vector3 right = Camera.main.transform.TransformDirection(Vector3.right);
		right.y = 0;
		right.Normalize();
		//正規化されている方向ベクトルに入力とspeedをかける
		forward *= Input.GetAxis ("Vertical") * speed * Time.deltaTime;
		right *= Input.GetAxis ("Horizontal") * speed * Time.deltaTime;
		//movementの更新
		movement = right + forward;
	}

	void Move(){
		//移動
		playerRig.MovePosition (transform.position + movement);
	}


	void Turn(){
		//動いていない場合、角度が取得できないので処理を除外
		if(movement != Vector3.zero){
			//ローカル空間における移動ベクトルを取得
			Vector3 localMovement = transform.InverseTransformPoint(transform.position + movement);
			//xz平面における原点(Player)と移動先の成す角度を取得
			float angle = Mathf.Atan2(localMovement.z, localMovement.x);
			//Z軸(0,0,1)との成す角度が知りたいので90°減算
			angle -= Mathf.PI / 2;
			//Quaternionは時計回りが正なので、得られた向きを逆転させる
			angle *= -1;
			//Rotateメドッドを使用するのでオイラーに変換
			angle *= Mathf.Rad2Deg;
			//ワールド座標でPlayerを回転
			transform.Rotate(Vector3.up, angle, Space.World);
			//回転
			//transform.rotation = Quaternion.LookRotation (movement);
		}
	}
}
