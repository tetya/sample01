using UnityEngine;
using System.Collections;

public class ReimuSpawnManager : MonoBehaviour {

	public GameObject reimuPrefab;

	// Use this for initialization
	void Start () {
		//設定した周期でスポーンメソッドを呼び出す
		InvokeRepeating ("Spawn", 0f, 30f);
	}
	
	void Spawn(){
		Instantiate (reimuPrefab, transform.position, transform.rotation);
	}
}
