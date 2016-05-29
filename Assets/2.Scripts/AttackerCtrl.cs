using UnityEngine;
using System.Collections;

public class AttackerCtrl : MonoBehaviour {

	#region const

	private const int SPEED = 10;

	#endregion

	#region enum

	private enum Direction{
		UP = 1,
		RIGHT = 2,
		DOWN,
		LEFT
	}

	#endregion

	#region public property

	private AttackerMgr _attacker_mgr;
	public AttackerMgr attacker_mgr
	{
		get { 
			_attacker_mgr = _attacker_mgr ?? (GameObject.FindWithTag("ROOT").GetComponent<AttackerMgr>());
			return this._attacker_mgr; 
		}
	}

	#endregion


	#region private property

	private int attacker_dir;

	#endregion

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		attacker_dir = attacker_mgr.attacker_dir;

		/* 与えられた方向へ直進 */
		switch(attacker_dir){
		case 0:
			break;
		case 1:
			this.gameObject.transform.Translate (Vector2.up * Time.deltaTime * SPEED);
			break;
		case 2:
			this.gameObject.transform.Translate (Vector2.right * Time.deltaTime * SPEED);
			break;
		case 3:
			this.gameObject.transform.Translate (Vector2.down * Time.deltaTime * SPEED);
			break;
		case 4:
			this.gameObject.transform.Translate (Vector2.left * Time.deltaTime * SPEED);
			break;
		default:
			break;
		}
			
	}


	#region private method

	private void GoStraight(){
		
	}

	#endregion
}
