using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamara : MonoBehaviour
{
    private GameObject player;
    public Vector3 distancia;

    void Update()
    {
        this.player = GameObject.Find("Player");
        this.distancia = new Vector3(0f, 0f, 5f);
    }
    void LateUpdate()
    {
        gameObject.transform.position = this.player.transform.position - this.distancia;
    }
}