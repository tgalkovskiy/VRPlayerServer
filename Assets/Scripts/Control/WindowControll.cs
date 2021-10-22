
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Serialization;
using SFB;

public class WindowControll : MonoBehaviour
{
    [SerializeField] private GameObject _changeContent = default;
    [SerializeField] private GameObject _chooseVideoPanel = default;
    [SerializeField] private GameObject _listCat = default;
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
    [SerializeField] private Image _loadImage = default;
    [SerializeField] private Text _videoDescription = default;
    
    public static WindowControll Instance;
    private bool isScalePreview = false;
    private string _nameContent;
    private string _description;
    private string _imagePath;
    public Sprite _defaultImage;
    public Button delete;
    public Button showListCatButton;
    public Button play;
    public Button pause;
    public Button stop;
    public Button back;
    public Button mute;
    public Button addImage;
    public Slider volume;
    public Slider time;
    public Button addVideoButton;
    public Button addPlayListButton;
    public Button categoryBackButton;
    public bool isShowList = false;
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
        showListCatButton.onClick.AddListener(ShowListCat);
        addImage.onClick.AddListener(AddImageContent);
        //_defaultImage = _loadImage.sprite;
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

    private void ShowListCat()
    {
        isShowList = !isShowList;
        _listCat.SetActive(isShowList);
    }

    public void CloseWindow(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
    public void OpenWindowAddContent()
    {
        _namePanel.SetActive(true);
    }

    public void OpenWindowChangeContent()
    {
        _changeContent.SetActive(true);
    }
    public void ApplyChangeContent()
    {
        _changeContent.SetActive(false);
    }
    public void GetName(InputField _field)
    {
        _nameContent = _field.text;
    }
    private void AddImageContent()
    {
        var extensions = new[]
            {
                //какие файлы вообще можно открыть
                new ExtensionFilter("Image Files", "jpg"),
                new ExtensionFilter("All Files", "*"),
            };
            foreach (string path in StandaloneFileBrowser.OpenFilePanel("Add File", "", extensions, true))
            {
                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(File.ReadAllBytes(path));
                Sprite NewSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),new Vector2(0,0));
                _loadImage.sprite = NewSprite;
                _imagePath = path;
            }
        
    }
    public void GetDescription(InputField _field)
    {
        _description = _field.text;
    }
    public void CreateVideo()
    {
        if (_nameContent.IsNullOrEmpty()){ _nameContent = "content";}
        string ext = string.Empty;
        if (!_imagePath.IsNullOrEmpty())
        {
            ext = Path.GetExtension(_imagePath);
            File.Copy(_imagePath, Path.Combine(Application.persistentDataPath, $"{_nameContent}{Path.GetExtension(_imagePath)}"));
        }
        ServerController.Instance.videoLoader.OpenFile(_nameContent, _description, ext);
        _namePanel.SetActive(false);
        _imagePath = string.Empty;
        _loadImage.sprite = _defaultImage;
    }
    public void CreateCategory(CategoryCell _categoryCell)
    {
        if(_nameContent.IsNullOrEmpty()){ _nameContent = "content";}
        string ext = string.Empty;
        if (!_imagePath.IsNullOrEmpty())
        {
            ext = Path.GetExtension(_imagePath);
            File.Copy(_imagePath, Path.Combine(Application.persistentDataPath, $"{_nameContent}{Path.GetExtension(_imagePath)}"));
        }
        ServerController.Instance.videoLoader.AddCategory(_nameContent, _description, ext);
        _namePanel.SetActive(false);
        _imagePath = string.Empty;
        _loadImage.sprite = _defaultImage;
    }
    //public void 
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
