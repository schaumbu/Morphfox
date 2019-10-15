using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUD3D : MonoBehaviour
{
    public float GammaCorrection;
    public Slider GammaSlider;
    public GameObject PauseMenu;
    public GameObject SoundMenu;
    public GameObject GraphicMenu;

    private void Start()
    {
        PauseMenu.SetActive(false);
        SoundMenu.SetActive(false);
        GraphicMenu.SetActive(false);
    }

    public void ClosePauseMenu()
    {
        ClosePauseMenuToOpenOther();
        Time.timeScale = 1f;
    }

    public void OpenPauseMenu()
    {
        PauseMenu.SetActive(true);
        Debug.Log("Paused");
        Time.timeScale = 0.1f;
    }

    public void ClosePauseMenuToOpenOther()
    {
        PauseMenu.SetActive(false);
        Debug.Log("Unpaused");
    }

    public void OpenSoundOptions()
    {
        ClosePauseMenuToOpenOther();
        SoundMenu.SetActive(true);
    }

    public void CloseSoundOptions()
    {
        SoundMenu.SetActive(false);
        OpenPauseMenu();
    }

    public void OpenGraphicOptions()
    {
        ClosePauseMenuToOpenOther();
        GraphicMenu.SetActive(true);
    }

    public void CloseGraphicOptions()
    {
        GraphicMenu.SetActive(false);
        OpenPauseMenu();
    }

    public void QuitButton()
    {
        Debug.Log("STOP");
        Application.Quit();
    }





    private void Update()
    {
        GammaCorrection = GammaSlider.value;
        RenderSettings.ambientLight = new Color(GammaCorrection, GammaCorrection, GammaCorrection, 1.0f);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!PauseMenu.activeSelf && !SoundMenu.activeSelf && !GraphicMenu.activeSelf)
            {
                OpenPauseMenu();
            } else if (PauseMenu.activeSelf)
            {
                ClosePauseMenu();
            } else if (SoundMenu.activeSelf)
            {
                CloseSoundOptions();
            } else
            {
                CloseGraphicOptions();
            }
        }
        
    }

}
