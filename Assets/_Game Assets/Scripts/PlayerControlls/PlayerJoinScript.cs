using NUnit.Framework;
using System.Collections.Generic;
using NaughtyAttributes;
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

    private void OnEnable() => playerInputManager.onPlayerJoined += AddPlayerInput;
    private void OnDisable() => playerInputManager.onPlayerJoined -= AddPlayerInput;
    
    private void Awake()
    {
        playerInputManager = FindFirstObjectByType<PlayerInputManager>();
        
        Gamepad[] gamepads = Gamepad.all.ToArray();
        InputDevice[] inputDevices = new InputDevice[2];

        // Set first input as Gamepad
        inputDevices[0] = gamepads[0];
        
        // Set second input as either Gamepad or Keyboard
        if (gamepads.Length == 2)
        {
            inputDevices[1] = gamepads[1];
        } else inputDevices[1] = Keyboard.current;
        
        // Join the players with the correct input devices
        for (int i = 0; i < inputDevices.Length; i++)
        {
            playerInputManager.JoinPlayer(i, -1, null, inputDevices[i]);
        }
    }
    
    private void AddPlayerInput(PlayerInput playerInput)
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
        if (cinemachineTargetGroup != null) cinemachineTargetGroup.AddMember(playerParent, 1f, 1f);
        playerParent.position = spawnPoints[playerInputs.Count - 1].position;
    }
}
