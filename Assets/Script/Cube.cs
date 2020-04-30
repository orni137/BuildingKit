using UnityEngine;

public class Cube : MonoBehaviour {
    GameManager gm;
    Material mat;
    Rigidbody rb;
    Color color;
    static int colorInd = 0;
    int colorMax = 7;

    void Start()  {
        OnBirth();
    }

    public void OnBirth() {
        gm = GameManager.gm;
        mat = GetComponent<MeshRenderer>().material;
        rb = GetComponent<Rigidbody>();
        color = gm.prefColors[colorInd++];
        if (colorInd >= colorMax) colorInd = 0;
        mat.color = color;
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    public bool Choose(bool b) {
        bool first = rb.isKinematic;
        rb.isKinematic = false;
        if (b) {
            mat.color = 2 * color;
            rb.useGravity = false;
        } else {
            mat.color = color;
            rb.useGravity = true;
        }
        return first;
    }

    void Update() {
        Vector3 pos = transform.position;
        if (pos.sqrMagnitude > 400) {
            Debug.Log(name + " is destroied");
            Destroy(gameObject);
        }
    }
}
