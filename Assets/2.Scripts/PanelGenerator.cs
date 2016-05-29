using UnityEngine;
using System.Collections;

public class PanelGenerator: MonoBehaviour {

	#region const

	#endregion

	#region public property

	private TurnMgr _turn_mgr;
	public TurnMgr turn_mgr
	{
		get { 
			_turn_mgr = _turn_mgr ?? (GameObject.FindWithTag("ROOT").GetComponent<TurnMgr>());
			return this._turn_mgr; 
		}
	}

	public int panel_type;	/* 0:白, 1:赤, 2:青 */

	#endregion

	#region private property

	[SerializeField]
	private GameObject red_panel,blue_panel,white_panel;
	private GameObject temp_obj;

	#endregion

	// Use this for initialization
	void Start () {
		Initialize ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region private method

	private void Initialize(){
		// Generatorが生み出された時はまず白パネルをその場所に生成
		temp_obj = Instantiate (white_panel, this.transform.position, Quaternion.identity) as GameObject;
		panel_type = 0;
	}

	#endregion


	#region public method

	// Attackerが通過したら呼び出される
	void OnTriggerEnter2D(Collider2D attacker){
		
		if(turn_mgr.isRedTurn){
			
//			Debug.Log ("Red turn");

			switch(panel_type){
			case 0:
			case 2:
//				Debug.Log ("white or blue");

				Destroy (temp_obj);
				temp_obj = Instantiate (red_panel, this.transform.position, Quaternion.identity) as GameObject;
				panel_type = 1;
				break;
			default:
				break;
			}

		} else {
			
//			Debug.Log ("Blue turn");

			switch(panel_type){
			case 0:
			case 1:
//				Debug.Log ("white or red");

				Destroy (temp_obj);
				temp_obj = Instantiate (blue_panel, this.transform.position, Quaternion.identity) as GameObject;
				panel_type = 2;
				break;
			default:
				break;
			}
		}
	}

	#endregion
}
