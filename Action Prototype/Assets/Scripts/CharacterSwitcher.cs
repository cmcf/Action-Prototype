using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterSwitcher : MonoBehaviour
{

    public List<GameObject> playableCharacters; // List of character GameObjects
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
            playableCharacters[currentCharacterIndex].SetActive(false);

            // Switch to the next character
            currentCharacterIndex = (currentCharacterIndex + 1) % playableCharacters.Count;

            // Activate the new character
            SwitchCharacter(currentCharacterIndex);
        }
    }

    // Activate the character at the specified index and deactivate others
    private void SwitchCharacter(int characterIndex)
    {
        // Ensure the index is within the valid range
        if (characterIndex >= 0 && characterIndex < playableCharacters.Count)
        {
            playableCharacters[characterIndex].SetActive(true);
        }
    }
}
