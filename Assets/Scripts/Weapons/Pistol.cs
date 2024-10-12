namespace Weapons
{
    public class Pistol : RangedWeapon
    {
        public override void Reload()
        {
            ammo = maxAmmo;
            audioSource.PlayOneShot(reloadClip);
        }
    }
}