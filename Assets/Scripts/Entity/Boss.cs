using System.Collections;
using FiniteStateMachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
public class Boss : Entity
{
    public BossStateMachine bossStateMachine;
    public Image hpBar;
    public Image hpBarLerp;
    public Animator grateAnimator;
    public TextMeshProUGUI bossHPText;

    private float lerpSpeed = 0.02f;
    
    private static readonly int GrateOff = Animator.StringToHash("GrateOFF");

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        StartCoroutine(UpdateHealthHud());
        bossStateMachine.ReactAttack();
    }

    public override void Death()
    {
        if (_sounds.Length > 0) _spawnSound.PlayOneShot(_sounds[1]); // 1 = deathSound
        if(_destroyEffect != null && _drops != null)
        {
            var myTransform = transform;
            Instantiate(_destroyEffect, myTransform.position, myTransform.rotation);
        }
        bossStateMachine.SetBossAnimations(BossStateMachine.BossAnimationsType.Death);
    }

    private IEnumerator UpdateHealthHud()
    {
        hpBar.fillAmount = curHp / maxHp;
        bossHPText.text = $"{curHp}|{maxHp}";
        
        while (hpBarLerp.fillAmount > hpBar.fillAmount)
        {
            hpBarLerp.fillAmount = Mathf.Lerp(hpBarLerp.fillAmount, curHp/maxHp, lerpSpeed);
            yield return 0;
        }
    }

    protected override void Start()
    {
        base.Start();
        bossHPText.text = $"{curHp}|{maxHp}";
    }

    // ReSharper disable once UnusedMember.Global
    public void StartGrateAnim() // This should be called from an animation event
    {
        grateAnimator.SetTrigger(GrateOff);
    }
}