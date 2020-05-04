using UnityEngine;

public class MusicButton : MonoBehaviour
{
    AudioSource music;
    Color color;
    GameManager gm;
    bool isPlaying = false;
    Material mat;
    // Start is called before the first frame update
    void Start()  {
        gm = GameManager.gm;
        music = GetComponent<AudioSource>();
        MeshRenderer mr = GetComponent<MeshRenderer>();
        mat = mr.material;
        color = mat.color;
    }

    public void StopMusic() {
        music.Stop();
        isPlaying = false;
        mat.color = color;
    }
    public void StartMusic() {
        music.Play();
        isPlaying = true;
        mat.color = color * 2;
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
