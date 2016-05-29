using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScreenTransition : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	#region public method

	public void ToPlay(){
		SceneManager.LoadScene ("scPlay");
	}

	public void ToTitle(){
		SceneManager.LoadScene ("scTitle");
	}

	#endregion
}
