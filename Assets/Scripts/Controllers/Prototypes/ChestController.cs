using UnityEngine;

public class ChestController : MonoBehaviour {

    public bool interactable = false;

    private Animator anim;

    public Rigidbody coinPrefab;
    public Transform spawner;

    // Use this for initialization
    void Start () {

        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
        if (interactable && Input.GetKeyDown(KeyCode.Space)) {

            anim.SetBool("openChest", true);

            Rigidbody coinInstance;
            coinInstance = Instantiate(coinPrefab, spawner.position, spawner.rotation) as Rigidbody;

            coinInstance.AddForce(spawner.up * 100);
        } 
	}

    void OnTriggerEnter(Collider other) {
        
        if (other.gameObject.tag.ToLower().Equals("player")) {

            interactable = true;
        }
    }

    void OnTriggerExit(Collider other) {

        if (other.gameObject.tag.ToLower().Equals("player")) {

            interactable = false;
        }
    }
}
