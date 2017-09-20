using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEksempel : MonoBehaviour {

	Vector3 startpos; 


	void Start () {
		startpos = transform.position; 

		// Event1: "Ambience" sættes igang ved start 
		AkSoundEngine.PostEvent ("Ambience", gameObject); 

	}
	

	void Update () {
		transform.position = new Vector3 (transform.position.x + (Input.GetAxis ("Horizontal") / 10), transform.position.y, transform.position.z); 

		float xpos = startpos.x - transform.position.x;


		// Event2: "PressSpace" kaldes ved tryk af knap 
		if (Input.GetKeyDown (KeyCode.Space)) {
			AkSoundEngine.PostEvent ("PressSpace", gameObject); 
			transform.localScale = new Vector3 (Random.Range (0.2f, 1.5f), Random.Range (0.2f, 1.5f), Random.Range (0.2f, 1.5f)); 
		}

		// GameSync1 (Switch): "LeftRight" skifter mellem PressSpace-filer ud fra binær x 
		// GameSync2 (State): "LeftRight" fader mellem Ambience-filer ud fra binær x
		if (transform.position.x <= startpos.x) {
			AkSoundEngine.SetSwitch ("LeftRight", "Left", gameObject); 
			AkSoundEngine.SetState ("LeftRight", "Left"); 
		
		} else if (transform.position.x > startpos.x) { 
			AkSoundEngine.SetSwitch ("LeftRight", "Right", gameObject); 
			AkSoundEngine.SetState ("LeftRight", "Right"); 

		}

		// GameSync3 (RTPC): "XPosition" lader x som talværdi styre parametre for PressSpace & Ambience 
		AkSoundEngine.SetRTPCValue ("XPosition", xpos);


	}
}
