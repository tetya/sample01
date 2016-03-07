using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

	public float spawnTime = 3f;
	public GameObject coinPrefab;
	public GameObject heartPrefab;
	public GameObject clockPrefab;
	public GameObject maxUpPrefab;
	public float rad = 3f;
	public int limit = 15;
	public int counter = 0;

	// Use this for initialization
	void Start () {
		//設定した周期でスポーンメソッドを呼び出す
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}

	void Spawn (){
		//生み出したアイテムの数が、スポーンポイントの上限に達しているか確認
		if(counter <= limit){
			//カウントを増やす
			counter++;
			//アイテムの生成
			InstantiateItem();
		}
	}

	void InstantiateItem(){
		//出現アイテムの乱数を用意
		int percentage = Random.Range (1, 100);
		//出現座標の乱数を用意
		Vector3 randomRad = Random.insideUnitSphere * rad;
		//乱数に応じて出てくるプレハブを決める
		if (percentage <= 1) {
			//MaxUpの生成
			InstantiateMaxUp (randomRad);
		} else if (percentage <= 14) {
			//回復アイテムの生成
			InstantiateHeart (randomRad);
		} else if (percentage <= 25) {
			//時計の生成
			InstantiateClock (randomRad);
		} else {
			//コインの生成
			InstantiateCoin (randomRad);
		}
	}

	//コインの生成
	void InstantiateCoin(Vector3 randomRad){
		GameObject obj = Instantiate (coinPrefab, transform.position + randomRad, coinPrefab.transform.rotation) as GameObject;
		obj.GetComponent<CoinBehaviour> ().SetSpawnManager (this);
	}

	//回復アイテムの生成
	void InstantiateHeart(Vector3 randomRad){
		GameObject obj = Instantiate (heartPrefab, transform.position + randomRad, coinPrefab.transform.rotation) as GameObject;
		obj.GetComponent<HeartBehaviour> ().SetSpawnManager (this);
	}

	//時計の生成
	void InstantiateClock(Vector3 randomRad){
		GameObject obj = Instantiate (clockPrefab, transform.position + randomRad, coinPrefab.transform.rotation) as GameObject;
		obj.GetComponent<ClockBehaviour> ().SetSpawnManager (this);
	}

	//MaxUpの生成
	void InstantiateMaxUp(Vector3 randomRad){
		GameObject obj = Instantiate (maxUpPrefab, transform.position + randomRad, coinPrefab.transform.rotation) as GameObject;
		obj.GetComponent<MaxUpBehaviour> ().SetSpawnManager (this);
	}
}
