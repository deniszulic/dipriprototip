using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject thirdcam;
    public GameObject firstcam;
    public int cammode;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Camera"))
        {
            if (cammode == 1)
            {
                cammode = 0;
            }
            else
            {
                cammode += 1;
            }
            StartCoroutine(CamChange());
        }
    }
    IEnumerator CamChange()
    {
        yield return new WaitForSeconds(0.01f);
        if (cammode == 0)
        {
            thirdcam.SetActive(true);
            firstcam.SetActive(false);
        }
        if (cammode == 1)
        {
            thirdcam.SetActive(false);
            firstcam.SetActive(true);
        }
    }
}
