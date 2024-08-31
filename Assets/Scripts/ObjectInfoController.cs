using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfoController : MonoBehaviour
{
    public Transform player;
    public GameObject canvas;
    public float minDistance;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i < this.transform.childCount; i++)
        {
            if(this.transform.GetChild(i).gameObject.GetComponent<ShowObjectInfoController>() != null)
            {
                this.transform.GetChild(i).gameObject.GetComponent<ShowObjectInfoController>().Setup(player, canvas, minDistance);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
