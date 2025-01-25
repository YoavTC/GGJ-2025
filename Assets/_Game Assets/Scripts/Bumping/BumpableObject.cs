using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace _Game_Assets.Scripts.Bumping
{
    public class BumpableObject : MonoBehaviour, IBumpable
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private float bumpCooldown;
        [SerializeField] public bool canBump;
        private float elapsedTime;

        public UnityEvent<Vector3> BumpUnityEvent;

        private void Start()
        {
            if (rb == null) rb = GetComponent<Rigidbody>();
            if (playerController == null) GetComponent<PlayerController>();
            
        }

        private void Update()
        {
            if (canBump) return;
            
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= bumpCooldown) canBump = true;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out IBumpable bumpable))
            {
                bumpable.ReceiveBump(rb.position, rb.linearVelocity.magnitude);
            }
        }

        public void ReceiveBump(Vector3 direction, float force)
        {
            if (!canBump) return;
            Debug.Log("what");
            
            BumpUnityEvent?.Invoke(rb.position);
            rb.AddForce((rb.position - direction) * force * 30, ForceMode.Impulse);

        }
    }
}