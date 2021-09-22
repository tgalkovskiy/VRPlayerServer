
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Serialization;


public class MenuControll : MonoBehaviour
{
    [SerializeField] private GameObject _chooseVideoPanel = default;
    [SerializeField] private GameObject _chooseExpPanel = default;
    [SerializeField] private GameObject _chooseFavorit = default;
    [SerializeField] private GameObject _chooseEvent = default;
    [SerializeField] private GameObject _blockPanel = default;
    [SerializeField] private Transform _posScalePreview = default;
    [SerializeField] private Transform _posPreviewOriginal = default;
    [SerializeField] private Image _videoIcon = default;
    [SerializeField] private Text _videoDescription = default;
    public static MenuControll Instance;
    private bool isScalePreview = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ShowPanels(GameObject panel)
    {
        _chooseVideoPanel.SetActive(false);
        _chooseExpPanel.SetActive(false);
        _chooseFavorit.SetActive(false);
        _chooseEvent.SetActive(false);
        panel.SetActive(true);
    }
    public void ChangeVideoPanel(Sprite sprite, string description)
    {
        _videoIcon.sprite = sprite;
        _videoDescription.text = description;
    }

    public void ScalePanelPreview(GameObject gameObject)
    {
        isScalePreview = !isScalePreview;
        float scale = isScalePreview == true ? (3) : (1);
        Vector3 pos = isScalePreview == true ? (_posScalePreview.position) : (_posPreviewOriginal.position);
        _blockPanel.SetActive(isScalePreview);
        Sequence _sequence = DOTween.Sequence();
        _sequence.Append(gameObject.transform.DOScale(scale, 0.5f));
        _sequence.Join(gameObject.transform.DOMove(pos, 0.5f));
        _sequence.Play().OnComplete(()=>_sequence.Kill());
    }
}
