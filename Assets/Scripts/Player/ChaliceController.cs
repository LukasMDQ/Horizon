using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChaliceController : MonoBehaviour
{
    private bool isChargeChalice = true;
    [SerializeField]
    private int chargeMaxUses = 5;
    private int chargeActualUses = 5;

    [SerializeField]
    private TextMeshProUGUI textChaliceUses;

    private void Start()
    {
        UpdateChaliceUsesUI();
    }
    public bool IsChargeChalice
    {
        get { return isChargeChalice; }
        private set { isChargeChalice = value; }
    }

    public int ChargeMaxUses
    {
        get { return chargeMaxUses; }
        private set { chargeMaxUses = Mathf.Max(1, value); }
    }

    public int ChargeActualUses
    {
        get { return chargeActualUses; }
        private set { chargeActualUses = Mathf.Clamp(value, 0, ChargeMaxUses); }
    }

    public void ChargeChalice()
    {
        IsChargeChalice = true;
        ChargeActualUses = chargeMaxUses;
        UpdateChaliceUsesUI();
    }

    public void UseChalice()
    {
        ChargeActualUses--;
        UpdateChaliceUsesUI();
        if (ChargeActualUses <= 0)
        {
            IsChargeChalice = false;
        }
    }

    public void UpdateChaliceUsesUI()
    {
        textChaliceUses.text = $"{chargeActualUses}/{chargeMaxUses}";
    }
}
