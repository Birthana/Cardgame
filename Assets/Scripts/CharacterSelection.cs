using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    private int selectedCharacterIndex;
    private Color desiredColor;

    [Header("List of characters")]
    [SerializeField] private List<CharacterSelectObject> characterList = new List<CharacterSelectObject>();

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private Image characterSplash;
    [SerializeField] private Image backgroundColor;

    [Header("Tweaks")]
    [SerializeField] private float backgroundColorTransitionSpeed = 10.0f;
    private void Start()
    {
        UpdateCharacterSelectionUI();
    }
    private void Update()
    {
        //Used to transition between colors of different characters.
        backgroundColor.color = Color.Lerp(backgroundColor.color, desiredColor, Time.deltaTime * backgroundColorTransitionSpeed);
    }
    public void LeftArrow() 
    {
        selectedCharacterIndex--;
        if (selectedCharacterIndex < 0)
            selectedCharacterIndex = characterList.Count - 1;
        UpdateCharacterSelectionUI();
    }

    public void RightArrow()
    {
        selectedCharacterIndex++;
        if (selectedCharacterIndex == characterList.Count)
            selectedCharacterIndex = 0;
        UpdateCharacterSelectionUI();
    }

    public void Confirm()
    {
        Debug.Log(string.Format("Character {0}:{1} has been chosen", selectedCharacterIndex, characterList[selectedCharacterIndex].characterName));
        //SceneManager.LoadScene("CardEffects");
    }

    private void UpdateCharacterSelectionUI() 
    {
        //Spash, name, desired color
        //https://www.youtube.com/watch?v=Wp0e-Wlnlmk&t=565s for the tutorial

        characterSplash.sprite = characterList[selectedCharacterIndex].splash;
        characterName.text = characterList[selectedCharacterIndex].characterName;
        desiredColor = characterList[selectedCharacterIndex].characterColor;
    }

    [System.Serializable]
    public class CharacterSelectObject 
    {
        public Sprite splash;
        public string characterName;
        public Color characterColor;
    }
}
