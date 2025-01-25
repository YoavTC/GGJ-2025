using System.Collections.Generic;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerJoinScript : MonoBehaviour
{
    private PlayerInputManager playerInputManager;
    [SerializeField] CinemachineTargetGroup cinemachineTargetGroup;
    [SerializeField] public List<Transform> spawnPoints = new List<Transform>(); // List of spawn points>
    [SerializeField] private GameObject playerPrefabB;

    private void OnEnable() => playerInputManager.onPlayerJoined += PositionPlayerTransforms;
    private void OnDisable() => playerInputManager.onPlayerJoined -= PositionPlayerTransforms;

    public UnityEvent NotEnoughControllersUnityEvent;

    private void Awake()
    {
        AddPlayers();
    }

    public void AddPlayers()
    {
        playerInputManager = FindFirstObjectByType<PlayerInputManager>();

        Gamepad[] gamepads = Gamepad.all.ToArray();
        InputDevice[] inputDevices = new InputDevice[2];

        if (gamepads.Length == 0)
        {
            NotEnoughControllersUnityEvent?.Invoke();
            return;
        }
        
        inputDevices[0] = gamepads[0];


        // Set second input as either Gamepad or Keyboard
        if (gamepads.Length == 2)
        {
            inputDevices[1] = gamepads[1];
        }
        else inputDevices[1] = Keyboard.current;

        // Join the players with the correct input devices
        for (int i = 0; i < inputDevices.Length; i++)
        {
            playerInputManager.JoinPlayer(i, -1, null, inputDevices[i]);
            playerInputManager.playerPrefab = playerPrefabB;
        }
        
    }

    private void PositionPlayerTransforms(PlayerInput playerInput)
    {
        Transform playerParent = playerInput.transform.root;
        playerParent.gameObject.tag = "Player";
        playerParent.position = spawnPoints[playerInput.playerIndex].position;

        playerParent.gameObject.name = $"player {playerInput.playerIndex}";

        if (cinemachineTargetGroup != null) cinemachineTargetGroup.AddMember(playerParent, 1f, 1f);
    }
}