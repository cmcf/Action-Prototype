using Abertay.Analytics;
using Cinemachine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

[System.Serializable]
public class CharacterInfo
{
    public GameObject character;
}

public enum CharacterState
{
    Player,
    Dog,
}

public class CharacterSwitcher : MonoBehaviour
{
    public List<CharacterInfo> characters = new List<CharacterInfo>();
    private int currentCharacterIndex = 0;
    private Vector3 previousCharacterPosition = Vector3.zero; // Store the previous character's position
    private CharacterState currentCharacterState;

    [SerializeField] float switchVFXDuration = 0.2f;

    public GameObject dogObject;
    public GameObject switchVFX;

    public AudioClip switchSFX;

    // Reference to the GameObject with the player movement script
    public GameObject playerObject;
    public Transform playerTransform;
    public CinemachineVirtualCamera virtualCamera;
    // Reference to an array of spawn points
    public Transform[] playerSpawnPoints;

    public int timesPlayerHasSwitched = 0;

    private bool canSwitch = true;
    public bool isSwitching = false;

    public bool canMovePlayer = true;
    public bool canMoveDog = false;

    void Start()
    {
        AnalyticsManager.Initialise("development");

        // Initialize by activating the first character
        SwitchCharacter(currentCharacterIndex);
        playerTransform.position = playerSpawnPoints[currentCharacterIndex].transform.position;

        // Find and store the player movement script component
        PlayerMovement playerMovement = playerObject.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            // Access to the the canMovePlayer variable
            playerMovement.canMovePlayer = canMovePlayer;
        }

        // Find and store the dog movement script component
        Dog dogMovement = dogObject.GetComponent<Dog>();
        if (dogMovement != null)
        {
            dogMovement.canMoveDog = canMoveDog;
        }
    }

    void Update()
    {
        // Handle input for the current character
        if (currentCharacterState == CharacterState.Player && !isSwitching)
        {
            // Handle input for the player character
            canMoveDog = true;

        }
        else if (currentCharacterState == CharacterState.Dog && !isSwitching)
        {
            // Handle input for the dog character
            canMovePlayer = true;

        }

        HandleSwitchInput();
    }

    void HandleSwitchInput()
    {
        // Checks if player has pressed switch button on keyboard or controller
        if (Gamepad.current != null && canSwitch && Gamepad.current.buttonNorth.wasPressedThisFrame || Input.GetKeyDown(KeyCode.Tab))
        {
            // Disable movement for the current character during switching
            canMovePlayer = false;
            canMoveDog = false;

            // Play switch VFX and SFX
            Vector3 vfxPosition = playerTransform.position;
            GameObject vfxInstance = Instantiate(switchVFX, vfxPosition, playerTransform.rotation);
            AudioSource.PlayClipAtPoint(switchSFX, Camera.main.transform.position, 0.2f);
            Destroy(vfxInstance, switchVFXDuration);
           

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
        timesPlayerHasSwitched++;
        canSwitch = false;
        isSwitching = true;

        Dictionary<string, object> switchData = new Dictionary<string, object>();
        switchData.Add("timesPlayerHasSwitched", timesPlayerHasSwitched);
        AnalyticsManager.SendCustomEvent("TimesSwitched", switchData);

        Rigidbody2D playerRigidbody = playerObject.GetComponent<Rigidbody2D>();
        Rigidbody2D dogRigidbody = dogObject.GetComponent<Rigidbody2D>();

        // Stop previous and current character movement
        if (currentCharacterState == CharacterState.Player)
        {
            PlayerMovement playerMovement = playerObject.GetComponent<PlayerMovement>();
            playerMovement.DisableInput();

            dogRigidbody.velocity = Vector2.zero;
            playerRigidbody.velocity = Vector2.zero;

        }
        else if (currentCharacterState == CharacterState.Dog)
        {
            // Resets move input 
            Dog dogMovement = dogObject.GetComponent<Dog>();
            dogMovement.DisableInput();
            // Resets velocity 
            playerRigidbody.velocity = Vector2.zero;
            dogRigidbody.velocity = Vector2.zero;

        }

        // Change the current character state
        currentCharacterState = (currentCharacterState == CharacterState.Player) ?
        CharacterState.Dog : CharacterState.Player;

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
            playerRigidbody.velocity = Vector2.zero;
            dogRigidbody.velocity = Vector2.zero;
            Invoke("EnableSwitch", 1f);

        }
    }

    void EnableSwitch()
    {
        canSwitch = true;
        isSwitching = false;
    }
}