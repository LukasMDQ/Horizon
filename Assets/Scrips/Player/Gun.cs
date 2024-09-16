using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject _FlashEffect, _instancePoint;
    public Transform spawnPoint;
    public GameObject bullet;
    //public int bulletCount, maxBulletCount = default;
   //SerializeField] TextMeshProUGUI textBullet;
    private void Update()
    {
        //textBullet.text = bulletCount.ToString();
        if (Time.timeScale >= 1)
        {
            Pistol();
        }
    }
    private void flash()
    {
        Instantiate(_FlashEffect, _instancePoint.transform.position, Quaternion.identity);
    }
   
    void Pistol()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //anim.SetTrigger("Shoot");
            flash();
            Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        }
    }   

}
