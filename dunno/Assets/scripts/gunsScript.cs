using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gunsScript : MonoBehaviour
{

    [SerializeField]float scopedFOV;
    [SerializeField] float normalFOV;
    [SerializeField] float damage = 10f;
    [SerializeField] float range = 100f;
    [SerializeField] float fireRate = 15f;
    [SerializeField] int maxAmmo;
    [SerializeField] float reloadTime;


    private int currentAmmo;
    private float nextTimeToFire = 0f;

    private bool isReloading = false;
    private bool scoped = false;

    public Camera fpscamera;
    public ParticleSystem muzzleflash;
    public GameObject impactEffect;
    public Animator anim;
    public TextMeshProUGUI text;
    public Animator animm;
    public Camera cameraa;
    public GameObject scopeOverLay;
    public GameObject gunCamera;
    public GameObject crooshair;



    // Update is called once per frame

    private void Start()
    {
        currentAmmo = maxAmmo;
        normalFOV = 60f;
    }
    private void OnEnable()
    {
        isReloading = false;
        anim.SetBool("isreloading", false);
    }
    void Update()
    {
        text.SetText(currentAmmo + " / " + maxAmmo);

        if (!scoped)
        {
            cameraa.fieldOfView = normalFOV;
        }

        if (isReloading)
            return;

       
        if (currentAmmo <= 0 || (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo < maxAmmo))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }


        if (Input.GetButtonDown("Fire2"))
        {
            scoped = !scoped;
            animm.SetBool("isScoped", scoped);
            if (scoped)
            {
                StartCoroutine(onScoped());
            }
            else
            {
                onUnscoped();
            }
        }

    }

    IEnumerator Reload()
    {
        isReloading = true;
        scopeOverLay.SetActive(false);
        scoped = false;
        Debug.Log("reloading....");

        anim.SetBool("isreloading", true);

        yield return new WaitForSeconds(reloadTime - 0.25f);
        anim.SetBool("isreloading", false);
        yield return new WaitForSeconds(0.25f);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot()
    {
        if (!scoped)
        {
            muzzleflash.Play();
        }
        

        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpscamera.transform.position, fpscamera.transform.forward, out hit, range))
        {
            UnityEngine.Debug.Log(hit.transform.name);

            enemy target = hit.transform.GetComponent<enemy>();
            if (target != null)
            {
                target.TakeDamage(damage);
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
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
