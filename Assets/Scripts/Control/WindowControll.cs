
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Serialization;


public class WindowControll : MonoBehaviour
{
    [SerializeField] private GameObject _chooseVideoPanel = default;
    [SerializeField] private GameObject _content = default;
    [SerializeField] private GameObject _namePanel = default;
    [SerializeField] private GameObject _VideoPanel = default;
    [SerializeField] private GameObject _chooseExpPanel = default;
    [SerializeField] private GameObject _chooseFavorit = default;
    [SerializeField] private GameObject _chooseEvent = default;
    [SerializeField] private GameObject _blockPanel = default;
    [SerializeField] private Transform _posScalePreview = default;
    [SerializeField] private Transform _posPreviewOriginal = default;
    [SerializeField] private Image _videoIcon = default;
    [SerializeField] private Text _videoDescription = default;
    public static WindowControll Instance;
    private bool isScalePreview = false;
    private string _nameCategory;
    
    public Button play;
    public Button pause;
    public Button stop;
    public Button back;
    public Slider volume;
    public Slider time;

    public Button addVideoButton;
    public Button addPlayListButton;
    [FormerlySerializedAs("backButton")] public Button categoryBackButton;
   
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

    public void ChooseVideo()
    {
        UnShowPanels();
        _chooseVideoPanel.SetActive(true);
        _VideoPanel.SetActive(true);
    }

    public void ChooseExp()
    {
        UnShowPanels();
        _chooseExpPanel.SetActive(true);
    }

    public void ChooseFavorite()
    {
        UnShowPanels();
        _chooseFavorit.SetActive(true);
    }

    public void ChooseEvent()
    {
        UnShowPanels();
        _chooseEvent.SetActive(true);
    }
    private void UnShowPanels()
    {
        _chooseVideoPanel.SetActive(false);
        _VideoPanel.SetActive(false);
        _chooseExpPanel.SetActive(false);
        _chooseFavorit.SetActive(false);
        _chooseEvent.SetActive(false);
    }
    
    public void OpenDialogCategory()
    {
        _namePanel.SetActive(true);
    }
    public void GetNameCategory(InputField _field)
    {
        _nameCategory = _field.text;
    }
    public void CreateCategory(CategoryCell _categoryCell)
    {
        ServerController.Instance.videoLoader.AddCategory(_nameCategory);
       _namePanel.SetActive(false);
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
