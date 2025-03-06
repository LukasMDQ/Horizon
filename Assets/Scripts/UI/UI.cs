using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once InconsistentNaming

public class UI : MonoBehaviour
{    
    [SerializeField] // ReSharper disable once InconsistentNaming
    //private Slider _slider;
    //public TextMeshProUGUI textMesh;
    public CanvasGroup AchievementCapsule;
    public Animator AchievementAnimator;
    public TextMeshProUGUI AchievementText;
   
    private void Awake()
    {
        UI UIInstance = this;
        RewardManager.SetUIReference(UIInstance);

        AchievementCapsule.alpha = 0.0f;
        AchievementText.alpha = 0.0f;
    }

    //---------------LOGROS---------------------------//

    public void AchievementShow(string rewardName)
    {
        if (RewardManager.TryGetReward(rewardName, out Reward reward))
        {
            AchievementAnimator.SetTrigger("FadeIn");
            AchievementText.SetText(reward.Description);
        }
        else
        {
            Debug.LogWarning($"El logro {rewardName} no está registrado en RewardManager.");
        }
    }


    //---------------LOGICA BARRA HP--------------//    
    /*public void ChangeMaxHp(float maxLife)
    {
        _slider.maxValue = maxLife;       
    }    

    public void ChangeCurrentHp(float curLife) 
    {
        _slider.value = curLife;
    }    
    public void StartHpBar(float curLife)
    {
        ChangeMaxHp(curLife);
        ChangeCurrentHp( curLife);
    }*/

}
