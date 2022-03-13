using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UILevelSelect : MonoBehaviour
{
    [SerializeField] private LevelController levelController;
    [SerializeField] private UILevel levelUI;
    [SerializeField] private LevelPopup levelPopup;
    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject rightArrow;
    [SerializeField] private int itemsPerPage = 12;

    private Transform _levelSelectPanel;
    private int _currentPage;
    private List<UILevel> _levelList;
    private List<UILevel> _visibleLevelList;

    private void Start()
    {
        // Fazer cache do transform
        _levelSelectPanel = transform;

        // Iniciar Lista
        _levelList = new List<UILevel>();
        _visibleLevelList = new List<UILevel>();

        // Criamos os elementos UI para cada level que tivermos
        for (int i = 0; i < levelController.Levels.Count; i++)
        {
            _levelList.Add(levelUI);
        }

        //////////////////////////////////////////////////////
        for (int i = 0; i < itemsPerPage; i++)
        {
            UILevel uiLevelInstance = Instantiate(levelUI);
            uiLevelInstance.transform.SetParent(_levelSelectPanel, false);
            _visibleLevelList.Add(uiLevelInstance);
        }
        //////////////////////////////////////////////////////


        // Contruimos a página de níveis 0
        BuildLevelPage(0);
    }

    private void BuildLevelPage(int page)
    {
        // Limpar painel de páginas
        //RemoveItemsFromPage();

        // Atribuir valor de nova página
        _currentPage = page;

        // Get total pages
        int totalPages = (int)Mathf.Ceil(((float)_levelList.Count / itemsPerPage));

        // Ajust buttons
        if (_currentPage == 0)
        {
            leftArrow.SetActive(false);
            rightArrow.SetActive(true);
        }
        else if(_currentPage == totalPages-1)
        {
            leftArrow.SetActive(true);
            rightArrow.SetActive(false);
        }
        else
        {
            leftArrow.SetActive(true);
            rightArrow.SetActive(true);
        }

        // Obter a posição inicial da página
        int startPos = _currentPage * itemsPerPage;

        // Obter a list de UILevels
        // Avançar [startPos] UILeves e pegar nos próximos [itemsPerPage]
        List<UILevel> pageList = _levelList.Skip(startPos).Take(itemsPerPage).ToList();

        //////////////////////////////////////////////////////
        for (int i = 0; i < itemsPerPage; i++)
        {
            UILevel uiLevelInstance = _visibleLevelList[i];

            if (i < pageList.Count)
            {
                uiLevelInstance.gameObject.SetActive(true);

                // Obter o Level Data
                Level level = levelController.Levels[(startPos + i)];

                // Colocar as stars
                uiLevelInstance.SetStars(level.Stars);
                // Adicionar o listener
                uiLevelInstance.GetComponent<Button>().onClick.RemoveAllListeners();
                uiLevelInstance.GetComponent<Button>().onClick.AddListener(() => SelectLevel(level));

                // Colocar lock
                if (!level.Locked)
                {
                    uiLevelInstance.UnLock();
                    uiLevelInstance.SetLevelName(level.ID.ToString());
                }
                else
                {
                    uiLevelInstance.Lock();
                }
            }
            else
            {
                uiLevelInstance.gameObject.SetActive(false);
            }
        }
        //////////////////////////////////////////////////////

        /*
        // Percorrer a lista
        for (int i = 0; i < pageList.Count; i++)
        {
            // Obter o Level Data
            Level level = levelController.Levels[(startPos + i)];

            // TODO:: Colocar em pool
            // Instanciar o UILevel
            UILevel uiLevelInstance = Instantiate(pageList[i]);

            // Colocar as stars
            uiLevelInstance.SetStars(level.Stars);
            // Colocar o parent o panel dos níveis
            uiLevelInstance.transform.SetParent(_levelSelectPanel, false);
            // Adicionar o listener
            uiLevelInstance.GetComponent<Button>().onClick.RemoveAllListeners();
            uiLevelInstance.GetComponent<Button>().onClick.AddListener(() => SelectLevel(level));

            // Colocar lock
            if (!level.Locked)
            {
                uiLevelInstance.UnLock();
                uiLevelInstance.SetLevelName(level.ID.ToString());
            }
            else
            {
                uiLevelInstance.Lock();
            }
        }
        */
    }

    private void RemoveItemsFromPage()
    {
        for (int i = 0; i < _levelSelectPanel.childCount; i++)
        {
            // TODO:: Coloar em pool
            Destroy(_levelSelectPanel.GetChild(i).gameObject) ;
        }
    }

    public void NextLevelPage()
    {
        if (_currentPage < Mathf.Abs(_levelList.Count / itemsPerPage))
        {
            BuildLevelPage(_currentPage+=1);
        }
    }

    public void PreviousLevelPage()
    {
        if (_currentPage > 0)
        {
            BuildLevelPage(_currentPage-=1);
        }
    }

    public void SelectLevel(Level level)
    {
        if (level.Locked)
        {
            // Mostrar Popup
            levelPopup.SetText("<b>Level" + level.ID + " is currently locked. </b> \n Complete level "+ (level.ID-1) + " to unlock it!");
        }
        else
        {
            // Ir para o próximo nível
            levelController.StartLevel(level.LevelName);
        }
    }
}
