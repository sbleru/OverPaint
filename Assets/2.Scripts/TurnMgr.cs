using UnityEngine;
using System.Collections;

public class TurnMgr : MonoBehaviour {

	#region public property

	public bool isRedTurn;
	public bool isFirst;

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
		
	private RoomMgr _room_mgr;
	private RoomMgr room_mgr
	{
		get { 
			_room_mgr = _room_mgr ?? (GameObject.FindWithTag("ROOT").GetComponent<RoomMgr>());
			return this._room_mgr; 
		}
	}

	PhotonView photonView;
	private bool isDone = false;

	#endregion

	// Use this for initialization
	void Start () {
//		isRedTurn = true;
		photonView = PhotonView.Get(this);
	}
	
	// Update is called once per frame
	void Update () {

		/* 
		 * photon接続 
		 */
		if (room_mgr.keyLock) {		

			if (!isDone) {
				/* ターン情報を共有する　赤ターンから */
				photonView.RPC ("ShareTurnInfo", PhotonTargets.All);
				isDone = true;
			}

			/* はじめにroomに入った人は赤パネル */
			if (isFirst) {

				/* 青パネルターンの時は操作できないようにする */
				if (isRedTurn) {
					ui_ctrl.enabled = true;
				} else {
					ui_ctrl.enabled = false;
				}

			} else {
			
				/* 赤パネルターンの時は操作できないようにする */
				if (isRedTurn) {
					ui_ctrl.enabled = false;
				} else {
					ui_ctrl.enabled = true;
				}
			}

		}
	}

	[PunRPC]
	void ShareTurnInfo(){
		isRedTurn = true;
	}
		
	[PunRPC]
	void ChatMessage(string a)
	{
		Debug.Log("ChatMessage " + a);
	}
}
