using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalProto : MonoBehaviour
{
	static public bool 	goalMet = false;
	public GameObject finishTextObject;

	// Start is called before the first frame update
	void Start()
	{
		finishTextObject.SetActive(false);
	}

	void OnTriggerEnter(Collider other) {
		// when the trigger is hit by something
		// check to see if it's a Projectile 
		if (other.gameObject.tag == "Projectile") {
			// if so, set goalMet = true
			GoalProto.goalMet = true;
			finishTextObject.SetActive(true);
			SceneManager.LoadScene("Main-Prototype 1");

			// also set the alpha of the color of higher opacity
			Material mat = GetComponent<Renderer>().material;
			Color c = mat.color;
			c.a = 1;
			mat.color = c;
		}
	}


	// Update is called once per frame
	void Update()
    {
        
    }
}
