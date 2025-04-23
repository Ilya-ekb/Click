using Configs;
using Data;
using Ecs.Systems;
using Leopotam.EcsLite;
using Providers;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Views;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private BusinessConfig[] businessesConfigs;
    [SerializeField] private BaseDataProviderConfig dataProviderConfig;
    [SerializeField] private NamesProvider namesProvider;

    [SerializeField] private UIDocument uiDocument;

    private IDataProvider dataProvider;
    private BalanceProvider balanceProvider;
    private BusinessProvider[] businessProviders;
    private IView[] businessViews;
    private EcsWorld world;
    private EcsSystems systems;

    public void Restart()
    {
        balanceProvider.Reset();
        foreach (var businessProvider in businessProviders)
            businessProvider.Reset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        DisposeProviders();
        DisposeSystems();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void InitProviders()
    {
        dataProvider = dataProviderConfig?.GetDataProvider();
        businessProviders = new BusinessProvider[businessesConfigs.Length];
        for (var i = 0; i < businessesConfigs.Length; i++)
        {
            businessProviders[i] = new BusinessProvider(businessesConfigs[i], world, dataProvider);
        }

        balanceProvider = new BalanceProvider(world, dataProvider);
    }

    private void InitSystems()
    {
        systems.Add(new IncomeSystem());
        systems.Add(new CanBuyUpdater());
        systems.Add(new BuySystem());
        systems.Add(new UpgradeSystem());
        systems.Init();
    }

    private void InitViews()
    {
        if (uiDocument is null) return;
        namesProvider?.Init();
        var mainVew = new MainView(this, uiDocument.rootVisualElement);
        balanceProvider.SetView(mainVew);
        foreach (var business in businessProviders)
            business.SetView(new BusinessItem(namesProvider, uiDocument.rootVisualElement));
    }

    private void DisposeProviders()
    {
        balanceProvider.Dispose();
        foreach (var business in businessProviders)
            business.Dispose();
    }

    private void DisposeSystems()
    {
        systems?.Destroy();
        world?.Destroy();
        systems = null;
        world = null;
    }

    #region Unity Loop
    private void Start()
    {
        world = new EcsWorld();
        systems = new EcsSystems(world);
        InitSystems();
        InitProviders();
        InitViews();
    }

    private void Update() => systems.Run();

    private void OnDestroy()
    {
        DisposeProviders();
        DisposeSystems();
    }

    #endregion
}