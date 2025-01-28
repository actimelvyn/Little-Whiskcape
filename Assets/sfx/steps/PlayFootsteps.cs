using UnityEngine;

public class PlayFootsteps : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] public AudioClip[] metalClips;
    public AudioClip[] hardwoodClips;
    public AudioClip[] dirtClips;

    RaycastHit hit;
    public Transform RayStart;
    public float range;
    public LayerMask layerMask;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            Debug.LogError("AudioSource component missing!");
    }

    public void AllFootsteps()
    {
        Debug.DrawRay(RayStart.position, Vector3.down * range, Color.red);

        if (Physics.Raycast(RayStart.position, Vector3.down, out hit, range, layerMask))
        {
            Debug.Log($"Hit detected: {hit.collider.tag}");

            if (hit.collider.CompareTag("Untagged"))
            {
                AudioClip metalClip = GetRandomClip(metalClips);
                PlayAllFootsteps(metalClip);
                Debug.Log("Metal detected! Playing metal SFX");
            }
            else if (hit.collider.CompareTag("Hardwood"))
            {
                AudioClip hardwoodClip = GetRandomClip(hardwoodClips);
                PlayAllFootsteps(hardwoodClip);
                Debug.Log("Hardwood detected! Playing hardwood SFX");
            }
            else if (hit.collider.CompareTag("Dirt"))
            {
                AudioClip dirtClip = GetRandomClip(dirtClips);
                PlayAllFootsteps(dirtClip);
                Debug.Log("Dirt detected! Playing dirt SFX");
            }
        }
    }

    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0)
        {
            Debug.LogWarning("No audio clips assigned!");
            return null;
        }
        return clips[Random.Range(0, clips.Length)];
    }

    public void PlayAllFootsteps(AudioClip audio)
    {
        if (audio == null)
            return;

        audioSource.pitch = Random.Range(0.8f, 1f);
        audioSource.volume = Random.Range(0.8f, 1f);
        audioSource.PlayOneShot(audio);
    }
}
