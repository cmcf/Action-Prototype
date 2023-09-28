using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterInfo
{
    public GameObject character;
}

public class CharacterSwitcher : MonoBehaviour
{
    public List<CharacterInfo> characters = new List<CharacterInfo>();
    private int currentCharacterIndex = 0;
    private Vector3 previousCharacterPosition = Vector3.zero; // Store the previous character's position
    public Transform playerTransform;
    public CinemachineVirtualCamera virtualCamera;



    void Start()
    {
        // Initialize by activating the first character
        SwitchCharacter(currentCharacterIndex);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            
            // Store the position of the currently equipped character before deactivating them
            CharacterInfo currentCharacter = characters[currentCharacterIndex];
            previousCharacterPosition = currentCharacter.character.transform.position;

            // Deactivate the currently equipped character
            currentCharacter.character.SetActive(false);

            // Switch to the next character
            currentCharacterIndex = (currentCharacterIndex + 1) % characters.Count;

            // Activate the new character and set their position based on the previous character's position
            SwitchCharacter(currentCharacterIndex);
        }
    }

    private void SwitchCharacter(int characterIndex)
    {
        
        if (characterIndex >= 0 && characterIndex < characters.Count)
        {
            CharacterInfo characterInfo = characters[characterIndex];
            characterInfo.character.SetActive(true);

            // Set the position of the character based on the previous character's position
            characterInfo.character.transform.position = previousCharacterPosition;

            // Update the playerTransform reference to the new character's transform
            playerTransform = characterInfo.character.transform;
            // Camera follows new player
            virtualCamera.Follow = playerTransform;
        }
    }
}