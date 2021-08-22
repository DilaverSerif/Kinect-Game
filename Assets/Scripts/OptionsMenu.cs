using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    private InputField gravityPower, playerMass, playerSensibility, JumpSensibility,bridgeExtraPower;
    private Toggle noTrigger;
    private Button apply, reset;
    private Text label;
    [SerializeField]
    private Transform menu;

    private bool stat;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OpenCloseMenu);
        noTrigger = menu.Find("NoTrigger").GetComponent<Toggle>();
            gravityPower = menu.Find("PlayerGravityPower").GetComponent<InputField>();
        playerMass = menu.Find("PlayerMass").GetComponent<InputField>();
        playerSensibility = menu.Find("PlayerLeftRight").GetComponent<InputField>();
        JumpSensibility = menu.Find("JumpSensibility").GetComponent<InputField>();
        
        bridgeExtraPower = menu.Find("BridgeExtraPower").GetComponent<InputField>();
        
        apply = menu.Find("Apply").GetComponent<Button>();
        apply.onClick.AddListener(SaveOption);
        reset = menu.Find("Reset").GetComponent<Button>();
        reset.onClick.AddListener(Reset);

        label = menu.Find("Text").GetComponent<Text>();
        
        if (PlayerPrefs.HasKey("NoTrigger"))
        {
            if (PlayerPrefs.GetInt("NoTrigger") == 1)
            {
                
                noTrigger.isOn = true;
            }
            else noTrigger.isOn = false;
        }

    }

    private void OpenCloseMenu()
    {
        stat = !stat;
        
        if (stat)
        {
            menu.gameObject.SetActive(true);
        }
        else menu.gameObject.SetActive(false);
    }
    private void SaveOption()
    {
        
        if (gravityPower.text != "")
        {
            PlayerPrefs.SetFloat("GravityPower",float.Parse(gravityPower.text));
        }

        if (playerMass.text != "")
        {
            PlayerPrefs.SetFloat("PlayerMass",float.Parse(playerMass.text));
        }
        
        if (playerSensibility.text != "")
        {
            PlayerPrefs.SetFloat("PlayerSensibility",float.Parse(playerSensibility.text));
            
        }
        
        if (JumpSensibility.text != "")
        {
            PlayerPrefs.SetFloat("JumpSensibility", float.Parse(JumpSensibility.text));
            
        }
        
        if (bridgeExtraPower.text != "")
        {
            PlayerPrefs.SetInt("BridgeExtraPower", Convert.ToInt32(bridgeExtraPower.text));
        }

        if (noTrigger.isOn)
        {
            PlayerPrefs.SetInt("NoTrigger",1);
        }
        else PlayerPrefs.SetInt("NoTrigger",0);
        
        LabelAnim("save successful");
    }

    private void Reset()
    {
        PlayerPrefs.DeleteAll();
        LabelAnim("reset successful");
    }

    private void LabelAnim(string text)
    {
        DOTween.Kill("anim");
        label.DOFade(1, 0).SetId("anim");
        label.text = text;
        label.DOFade(0, 1F).SetId("anim").SetDelay(0.25f);
    }
}
