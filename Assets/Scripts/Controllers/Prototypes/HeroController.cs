using UnityEngine;

public class HeroController : MonoBehaviour {

    public CapsuleCollider heroCollider;
    public float moveSpeed = 5f;

    private EnemyController enemyController;

    private RaycastHit hit;
    private Ray ray;

    public float rayDistance = 4;

	// Use this for initialization
	void Start () {

        heroCollider = GetComponent<CapsuleCollider>();
	}
	
	// Update is called once per frame
	void Update () {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);

        transform.Translate(movement * Time.deltaTime * moveSpeed);

        ray = new Ray(transform.position + new Vector3(0f, heroCollider.center.y, 0f), transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);

        if (Physics.Raycast(ray, out hit)) {

            if (hit.distance < rayDistance) {

                if (hit.collider.gameObject.tag.ToLower().Equals("enemy")) {

                    Debug.Log("Enemy Ahead!");
                }
            }
        }
	}

    void OnCollisionEnter(Collision collision) {
        
        if (collision.gameObject.tag.ToLower().Equals("enemy")) {

            enemyController = collision.gameObject.GetComponent<EnemyController>();
            enemyController.enemyHealth--;

            if (enemyController.enemyHealth <= 0) {

                Debug.Log("Battle dummy destroyed!");
            }
        }
    }
}
