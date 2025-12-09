using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    
    // BULLET
    public GameObject bullet;

    // force-urile bullet-ului
    public float shootForce, upwardForce;

    // Gun Balancing
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonhold;
    int bulletsLeft, bulletsShot;

    // Verificari
    bool shooting, readyToShoot, reloading;

    // Reference
    public Camera fpsCam;
    public Transform attackPoint;

    // gaza fizing
    public bool allowInvoke = true;
    
    private void Awake()
    {
        // Verifica daca Magazine-ul e full
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void MyInput()
    {
        //Verifica daca ai voie sa dai hold down la buton pentru inputul corespunzator
        if (allowButtonhold == true)
            shooting = Input.GetKey(KeyCode.Mouse0);
        else
            shooting = Input.GetKeyDown(KeyCode.Mouse0);

        //Shooting
        if( readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            //Seteaza bullets la 0
            bulletsShot = 0;

            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;
        //Gaseste hit position-ul exact folosind un Raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));//Ray-ul merge din mijlocul ecranului
        RaycastHit hit;

        //verfica daca ray-ul a lovit ceva
        Vector3 targetpoint;
        if(Physics.Raycast())
        bulletsLeft--;
        bulletsShot++;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
    }

}
