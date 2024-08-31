using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowObjectInfoController : MonoBehaviour
{
    public string title;
    public string text;
    public float height;
    public float declination;
    private Transform player;
    private GameObject canvas;
    private float minDistance;
    private GameObject mcanvas;
    private bool configured;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!configured)
            return;

        float dist = Vector3.Distance(new Vector3(player.position.x, this.transform.position.y, player.position.z), this.transform.position);
        if(dist < minDistance)
        {
            mcanvas.transform.localScale = Vector3.one;
        }
        else
        {
            mcanvas.transform.localScale = Vector3.zero;
        }

        if (this.title != null && this.text != null && this.title.Length > 0 && this.text.Length > 0)
        {
            mcanvas.transform.GetChild(0).GetComponentInChildren<Text>().text = this.title;
            mcanvas.transform.GetChild(1).GetComponentInChildren<Text>().text = this.text;
        }
    }

    public void Setup(Transform player, GameObject canvas, float minDistance)
    {
        this.player = player;
        this.canvas = canvas;
        this.minDistance = minDistance;

        Vector3 pos = this.transform.position + this.transform.forward * 0.1f + this.transform.up * height;
        mcanvas = GameObject.Instantiate(canvas, pos, this.transform.rotation * Quaternion.AngleAxis(90.0f, this.transform.up) * Quaternion.AngleAxis(declination, this.transform.forward));
        mcanvas.transform.parent = this.transform;

        mcanvas.transform.GetChild(0).GetComponentInChildren<Text>().text = "Titulo de prueba";
        mcanvas.transform.GetChild(1).GetComponentInChildren<Text>().text = "Texto de prueba";

        configured = true;
    }
}
