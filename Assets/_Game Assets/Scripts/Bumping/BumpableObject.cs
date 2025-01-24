using System;
using NaughtyAttributes;
using UnityEngine;

namespace _Game_Assets.Scripts.Bumping
{
    public class BumpableObject : MonoBehaviour, IBumpable
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private float bumpCooldown;
        [SerializeField] private bool canBump;
        private float elapsedTime;

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
            // playerController.enabled = false;
            
            rb.AddForce((rb.position - direction) * force, ForceMode.Impulse);
        }
    }
}