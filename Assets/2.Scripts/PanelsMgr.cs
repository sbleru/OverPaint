using UnityEngine;
using System.Collections;

public class PanelsMgr : MonoBehaviour {

	#region const

	private const int VERTICAL_BLOCK_NUM = 5;
	private const int HORIZONTAL_BLOCK_NUM = 8;

	#endregion


	#region private property

	[SerializeField]
	GameObject panel_generator;

	#endregion

	// Use this for initialization
	void Start () {
		CreatePanelGenerator ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region private method

	private void CreatePanelGenerator(){
		int i, j;

		for(i=0; i<HORIZONTAL_BLOCK_NUM; i++){
			for(j=0; j<VERTICAL_BLOCK_NUM; j++){
				Instantiate (panel_generator, new Vector2 (i, j), Quaternion.identity);
			}
		}
	}

	#endregion
}
