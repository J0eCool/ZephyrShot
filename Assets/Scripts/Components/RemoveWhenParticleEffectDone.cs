using UnityEngine;

public class RemoveWhenParticleEffectDone : MonoBehaviour {
    private ParticleSystem particles;

    void Start() {
        particles = GetComponent<ParticleSystem>();
    }

    void Update() {
        if (particles.isStopped) {
            GameObject.Destroy(gameObject);
        }
    }
}