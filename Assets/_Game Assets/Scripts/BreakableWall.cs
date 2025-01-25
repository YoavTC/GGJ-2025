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
        // disable the destroy version
        Destroyed_Version.SetActive(false);
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
        // used for debug mode - break all walls
        if (Input.GetKeyDown(KeyCode.F))
            BreakObject();
    }

    public void BreakObject()
    {
        //NO Need to spawn - we will use the sibling version attached
        //OLD//GameObject destroyedInstance = Instantiate(Destroyed_Version, transform.position, transform.rotation);
        //OLD//Debug.Log($"{transform.position}, {transform.localScale} vs new {destroyedInstance.transform.position}, {destroyedInstance.transform.localScale}");
        //OLD//// After instantiation, we set the local scale of the clone to match the source object's scale
        //OLD//destroyedInstance.transform.localScale = transform.localScale;

        //OLD//Renderer[] childRenderers = destroyedInstance.GetComponentsInChildren<Renderer>();

        Destroyed_Version.SetActive(true);
        Renderer[] childRenderers = Destroyed_Version.GetComponentsInChildren<Renderer>();

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
        //OLD//Destroy(destroyedInstance, timeout);
        Destroy(Destroyed_Version, timeout);        
    }

    // Static method to handle deactivation
}