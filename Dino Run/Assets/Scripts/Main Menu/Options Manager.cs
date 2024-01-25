using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public List<GameObject> uiElements = new List<GameObject>();
    public List<Material> dinoMats = new List<Material>();


    //currently, 0 is default Dino Red
    int selectedColourIndex = 0;

    bool isSFX = true;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Material GetSelectedDinoMaterial()
    {
        return dinoMats[selectedColourIndex];
    }

    public bool hasSFXOption()
    { 
        return isSFX; 
    }   

    public void ToggleVisibility()
    {
        // go throughy list, enable what isnt enabled, disable what is
        for (int i = 0; i < uiElements.Count; i++)
        {
            uiElements[i].SetActive(!uiElements[i].activeSelf);
        }
    }

    public void SetActiveColour(TMP_Dropdown dropdown)
    {
        selectedColourIndex = dropdown.value;
        Debug.Log("Colour: " + selectedColourIndex);    
    }
    public void PreviewActiveColour(MeshRenderer colourPreview)
    {
        colourPreview.material = dinoMats[selectedColourIndex];
    }

    public void ToggleSFX(bool toggle)
    {
        isSFX = toggle;
        Debug.Log ("SFX: " + isSFX.ToString());
    }
}
