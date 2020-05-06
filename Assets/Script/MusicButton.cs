using UnityEngine;

public class MusicButton : MonoBehaviour
{
    AudioSource music;
    Color color;
    GameManager gm;
    bool isPlaying = false;
    Material mat;
    public float rotationSpeed = 0.5f;
    float rotY= 0.0f;
    Quaternion initRot;

    void Start()  {
        gm = GameManager.gm;
        music = GetComponent<AudioSource>();
        MeshRenderer mr = GetComponent<MeshRenderer>();
        mat = mr.material;
        color = mat.color;
        initRot = transform.localRotation;
    }

    private void FixedUpdate() {
        rotY += rotationSpeed;
        if (rotY >= 360) rotY -= 360;
        if (rotY < 0) rotY += 360;
        transform.localRotation = initRot * Quaternion.Euler(0, rotY, 0);
    }

    public void StopMusic() {
        music.Stop();
        isPlaying = false;
        mat.color = color;
    }
    public void StartMusic() {
        music.Play();
        isPlaying = true;
        mat.color = color * 3;
    }

    public void ProcessMusic() {
        if (isPlaying) {
            StopMusic();
        } else {
            if (gm.playingMusic != null) {
                gm.playingMusic.StopMusic();
            }
            gm.playingMusic = this;
            StartMusic();
        }
    }

}
