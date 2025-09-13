using UnityEngine;

namespace Tanchiki.Entity
{
    [RequireComponent(typeof(AudioSource))]
    public class DistanceBasedAudio : MonoBehaviour
    {
        private AudioSource audioSource;
        private Transform listener;
        [SerializeField] private bool setOnlyOnce;
        [SerializeField] private float maxDistance;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            listener = GameObject.FindGameObjectWithTag("Player").transform;
            Refresh();
        }

        private void Refresh()
        {
            float distance = Vector2.Distance(transform.position, listener.position);
            float volume = Mathf.Clamp01(1 - (distance / maxDistance));

            float stereoPan = Mathf.Clamp((transform.position - listener.position).x / maxDistance, -1f, 1f);

            audioSource.panStereo = stereoPan;
            audioSource.volume = volume;
        }

        private void Update()
        {
            if (setOnlyOnce) return;
            Refresh();
            
        }
    }
}