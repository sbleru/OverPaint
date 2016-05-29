using UnityEngine;
using System.Collections;

public class UICtrl : MonoBehaviour {

	#region private property

	private Vector2 _attacker_pos;
	public Vector2 attacker_pos
	{
		get { return this._attacker_pos; }
		private set { this._attacker_pos = value; }
	}
		
	/*0:なし, 1:上, 2:右, 3:下, 4:左*/
	private int _attacker_direction;
	public int attacker_direction
	{
		get { return this._attacker_direction; }
		set { this._attacker_direction = value; }
	}

	#endregion


	#region private property

	private AttackerMgr _attacker_mgr;
	public AttackerMgr attacker_mgr
	{
		get { 
			_attacker_mgr = _attacker_mgr ?? (GameObject.FindWithTag("ROOT").GetComponent<AttackerMgr>());
			return this._attacker_mgr;
		}
	}

	private float currentXpos, currentYpos, startXpos, startYpos;
	private bool touchStart, isTap;

	#endregion

	// Use this for initialization
	void Start () {
		touchStart = false;
		isTap = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	#region public method
	/* パネルにタッチしていればtrueを返す */
	public bool isTouchPanel(){

		/* if this script is non-available */
		if(this.enabled == false){
			return false;
		}

		if (Input.GetMouseButton(0)) {

			if(!isTap){
				
				Vector3    aTapPoint   = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Collider2D aCollider2d = Physics2D.OverlapPoint(aTapPoint);

				if (aCollider2d) { 	/* パネルをタッチした場合はAttackerの生成場所として登録 */
					
					Vector2 pos = aCollider2d.transform.position;
					attacker_pos = pos;
					isTap = true;
					return true;

				} else { /* パネル以外の場所をタッチしている場合 */
					
					return false;
				}
			}
			return  true;

		} else {
			// 画面に指が触れていない場合
			currentXpos = 0.0f;
			currentYpos = 0.0f;
			startXpos = 0.0f;
			startYpos = 0.0f;
			touchStart = false;
			isTap = false;

			return false;
		}
	}


	public void GetAttackerDirection(){

		/* if this script is non-available */
		if(this.enabled == false){
			return;
		}	

		// デバッグ用
		if (!Application.isMobilePlatform) {
			if(Input.GetMouseButton(0)){
				// 指があった場合、座標を格納
				currentXpos = Input.mousePosition.x;
				currentYpos = Input.mousePosition.y;
				if (!touchStart) {
					// タッチした瞬間の座標を保存
					startXpos = currentXpos;
					startYpos = currentYpos;

					touchStart = true;
				}

			}
			else {
				// 画面に指が触れていない場合
				currentXpos = 0.0f;
				currentYpos = 0.0f;
				startXpos = 0.0f;
				startYpos = 0.0f;
				touchStart = false;

				return;
			}
		}


		// 仮想操作パッド
		for(int i = 0; i < Input.touchCount; i++){
			// 
			if(Input.GetTouch(i).position.x > 0.0f){
				// 指があった場合、座標を格納
				currentXpos = Input.GetTouch(i).position.x;
				currentYpos = Input.GetTouch(i).position.y;
				if(!touchStart){
					// タッチした瞬間の座標を保存
					startXpos = currentXpos;
					startYpos = currentYpos;
					touchStart = true;
				}
			}
		}

		if (Application.isMobilePlatform) {
			if(Input.touchCount == 0){
				// 画面に指が触れていない場合
				currentXpos = 0.0f;
				currentYpos = 0.0f;
				startXpos = 0.0f;
				startYpos = 0.0f;
				touchStart = false;

				return;
			}
		}

		// 移動地計算 X軸
		if((startXpos - currentXpos) < (Screen.width * -0.06f)){	   /* 右方向 */
	
			attacker_direction = 2;
			return;

		} else if((startXpos - currentXpos) > (Screen.width * 0.06f)){ /* 左方向 */

			attacker_direction = 4;
			return;

		}

		// 移動地計算 Y軸
		if((startYpos - currentYpos) < (Screen.height * -0.10f)){	   /* 上方向 */
			
			attacker_direction = 1;
			return;

		} else if ((startYpos - currentYpos) > (Screen.height * 0.10f)){ /* 下方向 */

			attacker_direction = 3;
			return;

		}

		return;
	}

	#endregion


	#region private method

	private void GetGeneratorPosition(){
		
	}

	#endregion

}