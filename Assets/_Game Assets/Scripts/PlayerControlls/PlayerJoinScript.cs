using NUnit.Framework;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoinScript : MonoBehaviour
{
    private List<PlayerInput> playerInputs = new List<PlayerInput>();
    [SerializeField]
    private List<Transform> spawnPoints = new List<Transform>(); // List of spawn points>
    public GameObject playerPrefabB;

    private PlayerInputManager playerInputManager;
    [SerializeField] CinemachineTargetGroup cinemachineTargetGroup;

    private void Awake()
    {
        playerInputManager = FindFirstObjectByType<PlayerInputManager>();

    }

    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayerInput;
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= AddPlayerInput;
    }
    public void AddPlayerInput(PlayerInput playerInput)
    {
        playerInputs.Add(playerInput);
        
        //if (playerPrefabB != null) Instantiate(playerPrefabB, playerParent.position, playerParent.rotation);
        if (playerInputs.Count == 1) 
        { 
            Debug.Log("Player 2 Joined");
            playerInputManager.playerPrefab = playerPrefabB;
        }
        Transform playerParent = playerInput.transform.root;
        playerParent.gameObject.tag = "Player";
        cinemachineTargetGroup.AddMember(playerParent, 1f, 1f);
        playerParent.position = spawnPoints[playerInputs.Count - 1].position;

      
        

    }

}
