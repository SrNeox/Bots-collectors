using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private Base _base;
    [SerializeField] private SpawnerUnit _spawnerUnit;

    private void OnEnable()
    {
        _base.AmountResourceUpdated += UpdateScore;
    }

    private void OnDisable()
    {
        _base.AmountResourceUpdated -= UpdateScore;
    }

    public void UpdateScore(int resourceCount)
    {
        _textMeshPro.text = $"Ресурса:{resourceCount}";
    }

    public void SetText(TextMeshProUGUI text)
    {
        _textMeshPro = text;
    }

    public TextMeshProUGUI GiveInfoText()
    {
        return _textMeshPro;
    }
}