using UnityEngine;

public class GameManager : MonoBehaviour {
    static public GameManager gm;
    public Color[] prefColors = new Color[7];
    public Transform birthPlace;
    public Transform cubePref;
    bool firstChoosen = false;

    public MusicButton playingMusic = null;
    enum MouseState {
        Free,
        Cube
    }
    MouseState mouseState = MouseState.Free;

    float range = 50.0f;
    float clickDelay = 0.5f;
    float lastClickTime;
    float currCubeZ;
    Vector3 currCubeShift = Vector3.zero;
    Vector3 targetPos;
    Cube currCube;

    float cursorZ = -7;
    public GameObject cursor;

    void Awake() {
        gm = this;
    }

    void Start() {
        cursor.transform.position = MouseSpacePosition(cursorZ);
        Cursor.visible = false;
        Instantiate(cubePref, birthPlace);
    }

    Vector3 MouseSpacePosition(float Z) {
        Camera cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 origin = ray.origin;
        Vector3 direction = ray.direction;
        float dZ = Z - origin.z;
        direction /= direction.z;
        direction *= dZ;
        return origin + direction;
    }

    void MoveCurrCube() {
        currCubeZ += Input.GetAxis("Mouse ScrollWheel") * 2;
        targetPos = MouseSpacePosition(currCubeZ) + currCubeShift;
        currCube.transform.position = targetPos;
    }

    void Update() {
        if (mouseState == MouseState.Cube) {
            MoveCurrCube();
        } else {
            cursor.transform.position = MouseSpacePosition(cursorZ);
        }

        if (Time.time - lastClickTime > clickDelay) {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
                lastClickTime = Time.time;
                if (mouseState == MouseState.Free) {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    // Debug.DrawRay(ray.origin, ray.direction * range, Color.green);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, range)) {
                        GameObject go = hit.collider.gameObject;

                        if (go != null) {
                            if (go.tag == "Cube") {
                                currCube = go.GetComponent<Cube>();
                                currCubeZ = currCube.transform.position.z;
                                currCubeShift = currCube.transform.position - MouseSpacePosition(currCubeZ);
                                targetPos = currCube.transform.position;
                                mouseState = MouseState.Cube;
                                firstChoosen = currCube.Choose(true);
                                cursor.SetActive(false);
                            } else {
                                MusicButton mb = go.GetComponent<MusicButton>();
                                if (mb != null) {
                                    mb.ProcessMusic();
                                }
                            }
                        }

                    }
                } else {
                    currCube.Choose(false);
                    currCube = null;
                    mouseState = MouseState.Free;
                    cursor.SetActive(true);
                    if (firstChoosen) {
                        Instantiate(cubePref, birthPlace);
                        firstChoosen = false;
                    }
                }
            }
        }
    }
}
