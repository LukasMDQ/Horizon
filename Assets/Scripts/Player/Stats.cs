using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stats : Entity
{
    [SerializeField] private TextMeshProUGUI textBullet; // TODO delete this later
    [SerializeField] private GameObject _lostMenu;
    public Image hpBar;
    public Image staminaBar;
    public CanvasGroup hpVignette;

    public int jewels;
    public float stamina, maxStamina;
    public int damage;
    public float ChargeRate;

    protected override void MyStart()
    {
        stamina = maxStamina;
    }

    private void Update()
    {
        UpdateStamina();
        UIUpdate();
    }

    //----------- Stamina Management -----------  
    private void UpdateStamina()
    {
        if (stamina > maxStamina) stamina = maxStamina;
        if (stamina < 0) stamina = 0;
    }

    //----------- Player-Specific Actions -----------  
    public void AddJewel(int jewelCount)
    {
        jewels += jewelCount;
        Debug.Log("Jewel added to grial");
    }

    public void Buff(int powerUp)
    {
        damage += powerUp;
        Debug.Log("Buff applied");
    }

    public void MaxLifeUp(int healPower)//AUMENTAR VIDA MAXIMA
    {
        maxHp += healPower;
        Debug.Log("VIDA MAXIMA AUMENTADA! ");
    }

    //----------- Death Management -----------  
    public override void Death()
    {
        if (curHp <= 0)
        {
            _lostMenu.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    //----------- UI Management -----------  
    private void UIUpdate()
    {
        hpBar.fillAmount = curHp / maxHp;
        hpVignette.alpha = 1 - (curHp / maxHp);
        staminaBar.fillAmount = stamina / maxStamina;
    }
}
