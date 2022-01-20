using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scopeEffect : MonoBehaviour
{
    public Animator animm;
    public Camera cameraa;
    public GameObject scopeOverLay;
    public GameObject gunCamera;
    public GameObject crooshair;
    public float scopedFOV;
    public float normalFOV;
    public bool scoped = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            scoped = !scoped;
            animm.SetBool("isScoped", scoped);
            if (scoped)
            {
                StartCoroutine(onScoped());
            }else
            {
                onUnscoped();
            }
        }


    }

    IEnumerator onScoped()
    {
        yield return new WaitForSeconds(0.15f);
        gunCamera.SetActive(false);
        scopeOverLay.SetActive(true);
        crooshair.SetActive(false);
        normalFOV = cameraa.fieldOfView;
        cameraa.fieldOfView = scopedFOV;
    }
    void onUnscoped()
    {
        scopeOverLay.SetActive(false);
        gunCamera.SetActive(true);
        crooshair.SetActive(true);
        cameraa.fieldOfView = normalFOV;
    }
}
