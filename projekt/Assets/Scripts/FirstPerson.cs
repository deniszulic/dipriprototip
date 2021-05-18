using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPerson : MonoBehaviour
{
    public Transform player;
    void LateUpdate()
    {
        Vector3 igrac = new Vector3(player.transform.position.x , player.transform.position.y+1.77f , player.transform.position.z-0.5f);
        transform.position = igrac;
    }
}
