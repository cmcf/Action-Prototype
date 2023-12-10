using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

/* This script was adapted based on code from the tutorial "Creating Hidden Rooms in Unity - Devlog 01" by Third Arm Labs. 
 * The tutorial was used to implement the hide and reveal tilemap functionality
 * The tutorial can be found on YouTube at https://www.youtube.com/watch?v=cyw2SdC_MDI
 */
public class HiddenRoom : MonoBehaviour
{
    BoundsInt area;
    Tilemap tilemap;
    BoxCollider2D boxCollider;
    public GameObject lever;
    bool enableLever = false;
    bool isRevealed = false;
    void Start()
    {
        // Gets all hidden tilemaps
        tilemap = GameObject.FindGameObjectWithTag("Hidden").GetComponent<Tilemap>();
        boxCollider = GetComponent<BoxCollider2D>();
        HideTiles();
    }

    void HideTiles()
    {
        // Creates boundary area
        Vector3Int position = Vector3Int.FloorToInt(boxCollider.bounds.min);
        Vector3Int size = Vector3Int.FloorToInt(boxCollider.bounds.size + new Vector3Int(0, 0, 1));
        area = new BoundsInt(position, size);

        // Loops through all the tiles in the hidden room 
        foreach (Vector3Int point in area.allPositionsWithin)
        {
            // Enables colour change
            tilemap.SetTileFlags(point, TileFlags.None);
            // Sets colour of each tile and sets alpha to 0
            tilemap.SetColor(point, new Color(255f, 255f, 255f, 0f));

        }
        if (lever != null)
        {
            lever.SetActive(false);
        }
    }


    public void RevealRoom()
    {
        isRevealed = true;
        enableLever = true;
        // Change the layer of the TilemapRenderer
        // Loops through tiles and sets them to be visible
        foreach (Vector3Int point in area.allPositionsWithin)
        {
            tilemap.SetTileFlags(point, TileFlags.None);
            tilemap.SetColor(point, new Color(1f, 196f / 255f, 244f / 255f, 1f));
        }
        if (lever != null)
        {
            lever.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Dog")
        {
            RevealRoom();
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Hidden"), false);
        }
        else if (collision.tag == "Player")
        {
            HideTiles();
            // Disable collision for the player so that the human falls through the hidden area
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Hidden"), true);
        }
    }
}



