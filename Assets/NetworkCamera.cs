using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkCamera : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		if (isLocalPlayer) 
		{
			transform.Find("Camera").gameObject.SetActive(true);
		}
    }
}
