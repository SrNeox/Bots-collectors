using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private Base _base;

    private void Start()
    {
        UpdateScore();
    }

    public void UpdateScore()
    {
        _textMeshPro.text = $"Русурсов {_base.CountResource}";
    }
}
