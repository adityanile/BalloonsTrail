using UnityEngine;

public class BlowerManager : MonoBehaviour
{
    public static BlowerManager instance;
    public ParticleSystem sprayer;

    // Smooth rotation
    private Quaternion intitialRot;
    float time = 0;
    private bool revertBack = false;

    [SerializeField]
    private float waitTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        if (!instance)
        {
            instance = this;
            intitialRot = transform.rotation;
            return;
        }
        Destroy(instance);
    }

    private void Update()
    {
        if (revertBack)
        {
            SmoothSlerp();
        }
    }

    public void SprayWater()
    {
        sprayer.Play();

        // Ensure Smooth rotation back to original Angle
        Invoke("StartSlerp", waitTime);
    }

    private void StartSlerp()
    {
        revertBack = true;
    }
    private void SmoothSlerp()
    {
        if (time <= 1)
        {
            Quaternion currRot = transform.rotation;
            transform.rotation = Quaternion.Slerp(currRot, intitialRot, time);
            time += Time.fixedDeltaTime;
        }
        else
        {
            revertBack = false;
            time = 0;
        }
    }
}
