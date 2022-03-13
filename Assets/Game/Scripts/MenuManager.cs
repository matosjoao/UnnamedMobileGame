using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject panelMenu;
    [SerializeField] GameObject panelLevels;
    [SerializeField] GameObject panelSettings;

    public void LoadLevels()
    {
        panelMenu.SetActive(false);
        panelLevels.SetActive(true);
    }

    public void LoadSettings()
    {
        panelMenu.SetActive(false);
        panelSettings.SetActive(true);
    }
}
