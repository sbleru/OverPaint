using UnityEngine;
using System.Collections;

public class TurnMgr : MonoBehaviour {

	#region public property

	public bool isRedTurn;
	public bool isFirst;

	#endregion


	#region private property

	private UICtrl _ui_ctrl;
	public UICtrl ui_ctrl
	{
		get { 
			_ui_ctrl = _ui_ctrl ?? (GameObject.FindWithTag("ROOT").GetComponent<UICtrl>());
			return this._ui_ctrl; 
		}
	}

	private AttackerMgr _attacker_mgr;
	public AttackerMgr attacker_mgr
	{
		get { 
			_attacker_mgr = _attacker_mgr ?? (GameObject.FindWithTag("ROOT").GetComponent<AttackerMgr>());
			return this._attacker_mgr;
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

			/* はじめにroomに入った人は赤パネル */
			if (isFirst) {

				/* 青パネルターンの時は操作できないようにする */
				if (!isRedTurn) {
					ui_ctrl.enabled = false;
//					attacker_mgr.enabled = false;
				} else {
					ui_ctrl.enabled = true;
//					attacker_mgr.enabled = true;
				}

			} else {
			
				/* 赤パネルターンの時は操作できないようにする */
				if (isRedTurn) {
					ui_ctrl.enabled = false;
//					attacker_mgr.enabled = false;
				} else {
					ui_ctrl.enabled = true;
//					attacker_mgr.enabled = true;
				}
			}

		}
	}


	[PunRPC]
	void ChatMessage(string a)
	{
		Debug.Log("ChatMessage " + a);
	}
}
