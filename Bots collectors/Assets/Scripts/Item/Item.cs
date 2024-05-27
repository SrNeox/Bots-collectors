using UnityEngine;

public class Item : MonoBehaviour
{
    public bool IsBusy { get; private set; }

    public void SetBusy(bool isBusy)
    {
        IsBusy = isBusy;
    }
}

