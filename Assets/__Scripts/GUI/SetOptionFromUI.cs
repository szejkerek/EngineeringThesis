using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SetOptionFromUI : MonoBehaviour
{
    public Scrollbar volumeSlider;
    public TMP_Dropdown turnDropdown;
    
    SetPlayerPreferences turnTypeFromPlayerPref;

    private void Awake()
    {
        turnTypeFromPlayerPref = FindObjectOfType<SetPlayerPreferences>();
        volumeSlider.onValueChanged.AddListener(SetGlobalVolume);
        turnDropdown.onValueChanged.AddListener(SetTurnPlayerPref);

        if (PlayerPrefs.HasKey("turn"))
            turnDropdown.value = PlayerPrefs.GetInt("turn");
        
        if (PlayerPrefs.HasKey("volume"))
            volumeSlider.value = PlayerPrefs.GetFloat("turn");
    }

    public void SetGlobalVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("volume", value);
    }

    public void SetTurnPlayerPref(int value)
    {
        turnTypeFromPlayerPref.ApplyPlayerPref();
        PlayerPrefs.SetInt("turn", value); 
    }
}