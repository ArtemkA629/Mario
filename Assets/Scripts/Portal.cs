using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    [SerializeField] private WindowShower _windowShower;

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        _windowShower.ShowWindow(true);
    }
}
