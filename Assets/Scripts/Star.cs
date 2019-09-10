using UnityEngine;

public class Star : MonoBehaviour
{
    public bool IsActivated { get; set; }

    private void Start()
    {
        IsActivated = false;
    }
}
