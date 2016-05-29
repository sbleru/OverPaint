using UnityEngine;
using System.Collections;

public class RoomMgr: Photon.MonoBehaviour {

	private TurnMgr _turn_mgr;
	private TurnMgr turn_mgr
	{
		get { 
			_turn_mgr = _turn_mgr ?? (GameObject.FindWithTag("ROOT").GetComponent<TurnMgr>());
			return this._turn_mgr; 
		}
	}

	public bool keyLock;

	// Use this for initialization
	void Start () {
		
		keyLock = false;
		//Photon Realtimeのサーバへの接続、ロビーへ入室
		PhotonNetwork.ConnectUsingSettings ("0.2");
	}

	//ロビーに入室した
	// OnJoinedLobbyはなんか知らんが使えなかった
	void OnConnectedToMaster(){
		//とりあえずどこかのルームへ入室する
		PhotonNetwork.JoinRandomRoom ();
	}

	//ルームへ入室した
	void OnJoinedRoom(){
		
		// Roomに参加しているプレイヤー情報を配列で取得.
		/* TODO
		 * このやり方でどちらのパネルを操作するか決めると再戦する際にまた接続しなおさなくてはならない
		 */
		PhotonPlayer [] player = PhotonNetwork.playerList;
		if(player.Length < 2){
			turn_mgr.isFirst = true;
//			turn_mgr.isRedTurn = true;
		} else {
			turn_mgr.isFirst = false;
//			turn_mgr.isRedTurn = false;
		}

		// プレイヤー名とIDを表示.
		for (int i = 0; i < player.Length; i++) {
			Debug.Log((i).ToString() + " : " + player[i].name + " ID = " + player[i].ID);
		}
		//入室が完了した事を出力し、キーロック解除
		print ("ルームへ入室しました");
		keyLock = true;
	}

	//ルームの入室に失敗
	void OnPhotonRandomJoinFailed(){
		//自分でルームを作成して入室
		PhotonNetwork.CreateRoom (null);
	}

	void Update(){
		//左クリックが押されたらオブジェクトを読み込み
//		if(Input.GetMouseButtonDown(0) && keyLock){
//			GameObject mySyncObj = 
//				PhotonNetwork.Instantiate ("Cube", new Vector3 (9.0f, 0f, 0f), Quaternion.identity, 0);
//			//動きをつけるためにRigidbodyを取得し、力を加える
//			Rigidbody mySyncObjRB = mySyncObj.GetComponent<Rigidbody> ();
//			mySyncObjRB.isKinematic = false;
//			float rndPow = Random.Range (1.0f, 5.0f);
//			mySyncObjRB.AddForce (Vector3.left * rndPow, ForceMode.Impulse);
//		}
	}

	/// <summary>
	/// UnityのGameウィンドウに表示させる
	/// </summary>
	void OnGUI()
	{
		// Photonのステータスをラベルで表示させています
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}
}