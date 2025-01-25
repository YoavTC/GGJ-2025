using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoinScript : MonoBehaviour
{
    private PlayerInputManager playerInputManager;
    [SerializeField] CinemachineTargetGroup cinemachineTargetGroup;
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>(); // List of spawn points>

    private void OnEnable() => playerInputManager.onPlayerJoined += PositionPlayerTransforms;
    private void OnDisable() => playerInputManager.onPlayerJoined -= PositionPlayerTransforms;
    
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
    
    private void PositionPlayerTransforms(PlayerInput playerInput)
    {
        Transform playerParent = playerInput.transform.root;
        playerParent.position = spawnPoints[playerInput.playerIndex].position;
        
        if (cinemachineTargetGroup != null) cinemachineTargetGroup.AddMember(playerParent, 1f, 1f);
    }
}
