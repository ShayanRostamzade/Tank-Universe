using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

    public GameObject shell;

	void Start () {
        Destroy(shell, 2f);
	}
	
	
}
