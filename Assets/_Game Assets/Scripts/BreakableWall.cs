using System.Collections;
using UnityEngine;
using static WallManager;

public class BreakableWall : MonoBehaviour
{
    public GameObject Destroyed_Version;
    public int maxHP = 100;
    public float timeout = 2f;
    private int currentHP;

    private Renderer originalRenderer;

    void Start()
    {
        currentHP = maxHP;

        // Pre-cache renderers
        originalRenderer = GetComponent<Renderer>();
    }

    void OnCollisionEnter(Collision collision)
    {
        float impactSpeed = collision.relativeVelocity.magnitude;
        int damage = Mathf.RoundToInt(impactSpeed * 10);
        TakeDamage(damage);
    }

    void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            BreakObject();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            BreakObject();
    }

    public void BreakObject()
    {
        GameObject destroyedInstance = Instantiate(Destroyed_Version, transform.position, transform.rotation);
        Debug.Log($"{transform.position}, {transform.localScale} vs new {destroyedInstance.transform.position}, {destroyedInstance.transform.localScale}");
        // After instantiation, we set the local scale of the clone to match the source object's scale
        destroyedInstance.transform.localScale = transform.localScale;

        Renderer[] childRenderers = destroyedInstance.GetComponentsInChildren<Renderer>();
        if (originalRenderer && originalRenderer.material != null)
        {
            foreach (Renderer childRenderer in childRenderers)
            {
                if (childRenderer != null)
                {
                    childRenderer.material = originalRenderer.material;
                }
            }
        }
       gameObject.SetActive(false);
       Destroy(destroyedInstance, timeout);
        
    }

    // Static method to handle deactivation
}