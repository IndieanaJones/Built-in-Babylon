using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    //Where to spawn the player
    public Vector3 spawnLocation = new Vector3(0, 0, 0);
    public Vector3 SkipTutorialSpawnLoc = new Vector3(6.25f, 2.36f, 350f);

    public string CharacterToSpawn = "Prefabs/Builder";

    public static GameObject ThePlayerRef;

    public void Start()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        GameObject playerPrefab = (GameObject)Resources.Load(CharacterToSpawn);
        GameObject playerObject = Instantiate(playerPrefab, PlayerPrefs.GetInt("skiptutorial") == 0 ? new Vector3(spawnLocation.x, spawnLocation.y + 3, spawnLocation.z) : SkipTutorialSpawnLoc, Quaternion.Euler(0, 0, 0));
        BaseBody playerBody = playerObject.GetComponent<BaseBody>();
        if (playerBody == null)
            return;
        if (playerBody.fpsPerspective != null)
            playerBody.fpsPerspective.EnableFPS(true);
        playerBody.characterMaster = playerBody.gameObject.AddComponent(typeof(PlayerCharacterMaster)) as CharacterMaster;
        ThePlayerRef = playerObject;
    }
}
