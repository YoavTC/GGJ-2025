using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public GameObject Destroyed_Version;
    public int maxHP = 100;

    private int currentHP;

    void Start()
    {
        currentHP = maxHP;
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
        if (Input.GetKeyDown(KeyCode.H))
            BreakObject();
    }

    public void BreakObject() {
        Instantiate(Destroyed_Version, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
