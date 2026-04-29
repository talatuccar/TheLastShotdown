using UnityEngine;

public class NightModeApplier : MonoBehaviour
{
    public Material nightSkybox;
    public Material daySkybox;

    void Start()
    {
        // default gündüz
        bool isNight = PlayerPrefs.GetInt("IsNightMode", 0) == 1;

        if (isNight)
        {
            
            ApplyAtmosphere(nightSkybox, 0.3f, 0.2f, Color.black);
        }
        else
        {
          
            ApplyAtmosphere(daySkybox, 1.0f, 1.0f, Color.white);
        }
    }

    void ApplyAtmosphere(Material skyBox, float ambientIntensity, float reflectionIntensity, Color ambientLight)
    {
        RenderSettings.skybox = skyBox;
        RenderSettings.ambientIntensity = ambientIntensity;
        RenderSettings.reflectionIntensity = reflectionIntensity;
        RenderSettings.ambientLight = ambientLight;
        DynamicGI.UpdateEnvironment();
    }
}