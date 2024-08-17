using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    //Where to spawn the player
    public Vector3 spawnLocation = new Vector3(0, 0, 0);

    public string CharacterToSpawn = "Prefabs/Builder";

    public static GameObject ThePlayerRef;

    public void Start()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        GameObject playerPrefab = (GameObject)Resources.Load(CharacterToSpawn);
        GameObject playerObject = Instantiate(playerPrefab, new Vector3(spawnLocation.x, spawnLocation.y + 3, spawnLocation.z), Quaternion.Euler(0, 0, 0));
        BaseBody playerBody = playerObject.GetComponent<BaseBody>();
        if (playerBody == null)
            return;
        if (playerBody.fpsPerspective != null)
            playerBody.fpsPerspective.EnableFPS(true);
        playerBody.characterMaster = playerBody.gameObject.AddComponent(typeof(PlayerCharacterMaster)) as CharacterMaster;
        ThePlayerRef = playerObject;
    }
}
