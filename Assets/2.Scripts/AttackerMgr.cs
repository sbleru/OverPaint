using UnityEngine;
using System.Collections;

// Attackerの生成、削除
public class AttackerMgr : MonoBehaviour {

	#region public property

	public int attacker_dir { get; private set; }

	#endregion

	#region private property

	private UICtrl _ui_ctrl;
	private UICtrl ui_ctrl
	{
		get { 
			_ui_ctrl = _ui_ctrl ?? (GameObject.FindWithTag("ROOT").GetComponent<UICtrl>());
			return this._ui_ctrl; 
		}
	}

	private TurnMgr _turn_mgr;
	private TurnMgr turn_mgr
	{
		get { 
			_turn_mgr = _turn_mgr ?? (GameObject.FindWithTag("ROOT").GetComponent<TurnMgr>());
			return this._turn_mgr; 
		}
	}

	private RoomMgr _room_mgr;
	private RoomMgr room_mgr
	{
		get { 
			_room_mgr = _room_mgr ?? (GameObject.FindWithTag("ROOT").GetComponent<RoomMgr>());
			return this._room_mgr; 
		}
	}

	[SerializeField]
	private GameObject attacker;
	private GameObject temp_attacker;

	private Vector2 attacker_pos;

	PhotonView photonView;
	private bool isConnect = false;
	private bool isDelete = false;
	private bool isChange = false;

	#endregion

	// Use this for initialization
	void Start () {
		temp_attacker = null;
		photonView = PhotonView.Get(this);
	}
	
	// Update is called once per frame
	void Update () {
	
		if (ui_ctrl.isTouchPanel()) {

			/* Attackerを生成していなければ */
			if(temp_attacker == null){

				/* Attacker position を同期させる */
				photonView.RPC("CreateAttacker", PhotonTargets.All, ui_ctrl.attacker_pos);
				/* ターンチェンジの共有 */
				photonView.RPC ("ChangeTurn", PhotonTargets.All);

				/* 進行方向の初期化 */
				ui_ctrl.attacker_direction = 0;
				isDelete = false;
				isChange = false;

			} else {
				
				/* 進む方向を決める */
				ui_ctrl.GetAttackerDirection ();
				/* 進行方向を与える */
				photonView.RPC ("SetAttackerDirection", PhotonTargets.All, ui_ctrl.attacker_direction);

				/* 連続でアタッカーを生成できないようにUIを無効にする */
				/* TODO:ターン交代までにアタッカーがdeleteされているとギリギリもう一回生成できてしまう 
				 *		ひとまずDeleteまでの時間を長引かせることで解決、でもなんかなー。
				 */
				ui_ctrl.enabled = false;
			}

		} else {
			
			if(temp_attacker != null){
				Destroy (temp_attacker, 3.0f);
			}
		}
			
		/* 
		 * photon接続テスト 
		 */
		if(room_mgr.keyLock){
			if(!isConnect){
				photonView.RPC("ChatMessage", PhotonTargets.All, "jup", "and jup!");
				isConnect = true;
			}
		}
	}


	#region public method

	public void DeleteAttacker(){
		Destroy (temp_attacker, 0.5f);
	}

	public void isOutOfField(GameObject obj){
		
	}

	[PunRPC]
	void ChatMessage(string a, string b)
	{
		Debug.Log("ChatMessage " + a + " " + b);
	}

	[PunRPC]
	void CreateAttacker(Vector2 pos){
		temp_attacker = Instantiate (attacker, pos, Quaternion.identity) as GameObject;
	}

	[PunRPC]
	void SetAttackerDirection(int dir){
		attacker_dir = dir;
	}

	[PunRPC]
	IEnumerator ChangeTurn(){
		/* ターン交代まで時間を取る */
		yield return new WaitForSeconds (3.0f);

		turn_mgr.isRedTurn ^= true;
	}

	#endregion
}
