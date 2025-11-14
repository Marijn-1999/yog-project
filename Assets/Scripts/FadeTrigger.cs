using UnityEngine;

public class FadeTrigger : MonoBehaviour
{
    public SceneFader fader;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("FadeTrigger hit by: " + collision.name);

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered fade trigger — starting fade");
            fader.FadeOut();
        }
    }
}
