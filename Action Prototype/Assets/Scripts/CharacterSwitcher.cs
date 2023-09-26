using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterSwitcher : MonoBehaviour
{

    [System.Serializable]
    public class CharacterInfo
    {
        public GameObject character;
        public Vector3 position;
    }

    public List<CharacterInfo> characters = new List<CharacterInfo>(); // List of character GameObjects and their positions
    private int currentCharacterIndex = 0; // Index of the currently active character


    void Start()
    {
        // Initialize by activating the first character and deactivating others
        SwitchCharacter(currentCharacterIndex);
        
    }

    void Update()
    {
        NextCharacter();
    }

    private void NextCharacter()
    {
        // Check for character switch input, e.g., a keyboard key or button press
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Deactivate the current character
            characters[currentCharacterIndex].character.SetActive(false);

            // Switch to the next character
            currentCharacterIndex = (currentCharacterIndex + 1) % characters.Count;

            // Activate the new character and set its position
            SwitchCharacter(currentCharacterIndex);
        }
    }

    // Activate the character at the specified index and deactivate others
    private void SwitchCharacter(int characterIndex)
    {
        // Ensure the index is within the valid range
        if (characterIndex >= 0 && characterIndex < characters.Count)
        {
            CharacterInfo characterInfo = characters[characterIndex];
            characterInfo.character.SetActive(true);

            // Set the position of the character
            characterInfo.character.transform.position = characterInfo.position;
        }
    }
}
