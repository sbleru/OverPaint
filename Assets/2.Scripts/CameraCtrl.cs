using UnityEngine;
using System.Collections;

public class CameraCtrl : MonoBehaviour {

	#region private property

	#endregion


	#region event

	// Use this for initialization
	void Start () {
		// カメラに写っていないオブジェクトがある場合
		while(isOutOfCamera ()){
			GetComponent<Camera> ().orthographicSize++;
		}
	}

	#endregion


	#region private method

	// オブジェクトがカメラの範囲から外れていないかチェック
	bool isOutOfCamera() 
	{ 
		Vector3 view_pos = GetComponent<Camera>().WorldToViewportPoint( new Vector3(0f,0f,0f) );
		if (view_pos.x < -0.0f ||
			view_pos.x >  1.0f ||
			view_pos.y < -0.0f ||
			view_pos.y >  1.0f) { 
			// 範囲外 
			return true; 
		}

		return false;
	} 

	#endregion
}
