using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class RGameManager : MonoBehaviour
{
    [SerializeField] public GameObject BuyScreen, LevelScreen;
    [SerializeField] Text _textLevels;
    [SerializeField] public const int LevelsCount = 5;
    private void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MainCamera");


        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);

            //InitializePurchasing();
            //PurchaseManager.OnPurchaseSubscription += OnPurchase_Subscription;
            //if(PlayerPrefs.GetInt("Completed_Levels", 0) > 0)
            //SceneManager.LoadScene(PlayerPrefs.GetInt("Completed_Levels", 0) <= LevelsCount ? PlayerPrefs.GetInt("Completed_Levels", 0)- 1:
            //LevelsCount - 1);

        }


    }
    /* void OnPurchase_Subscription(PurchaseEventArgs args){
         Debug.Log("sub the " + args.purchasedProduct.definition.id);
         if(args.purchasedProduct.definition.id == "subscription")
         PlayerPrefs.SetInt("IsBought", 1);

     }*/
    //scene loader functions
    public void RestartScene()
    {
        if (SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCountInBuildSettings - 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //something what reset object positions in the generated scene
        }


    }
    public void GotoNextScene()
    {
        //Debug.Log(PurchaseManager.CheckBuyState("subscription"));
        // if(/*PlayerPrefs.GetInt("IsBought") == 1*/ PurchaseManager.CheckBuyState("subscription")){
        if (SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCountInBuildSettings - 1)
        {
            SetComleteLevel();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }

        else
        {
            SetComleteLevel();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }

        // }else{
        //     ShowBuyScreen();
        // }

    }
    public void MuteAudio()
    {
        GetComponent<AudioSource>().mute = !GetComponent<AudioSource>().mute;
    }
    //отметка пройденных уровней
    public static void SetComleteLevel()
    {
        //Debug.Log(SceneManager.GetActiveScene().buildIndex + 1 + " " + PlayerPrefs.GetInt("Completed_Levels", 0));
        if (SceneManager.GetActiveScene().buildIndex + 1 > PlayerPrefs.GetInt("Completed_Levels", 0))
        {
            PlayerPrefs.SetInt("Completed_Levels", PlayerPrefs.GetInt("Completed_Levels", 0) + 1);
        }
        if (SceneManager.GetActiveScene().buildIndex + 1 == LevelsCount)
        {
            PlayerPrefs.SetInt("Completed_Levels", PlayerPrefs.GetInt("Completed_Levels", 0) + 1);
        }
    }
    public void ShowBuyScreen()
    {
        BuyScreen.SetActive(!BuyScreen.activeSelf);
    }
    //показывает экран с уровнями
    public void ShowLevelsScreen()
    {
        LevelScreen.SetActive(!LevelScreen.activeSelf);
        _textLevels.text = "Completed levels " + PlayerPrefs.GetInt("Completed_Levels", 0) + "/" + LevelsCount;
    }
    // Start is called before the first frame update


    /* void OnPurchase_nonConsumable(PurchaseEventArgs args){
         Debug.Log("nonConsume the " + args.purchasedProduct.definition.id);
         //if(args.purchasedProduct.definition.id == "get_levels")
         PlayerPrefs.SetInt("IsBought", 1);

     }*/


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) PlayerPrefs.SetInt("Completed_Levels", 0);
    }
    // private static IStoreController m_StoreController;
    //private static IExtensionProvider m_StoreExtensionProvider;
    //private int currentProductIndex;

    // [Tooltip("Не многоразовые товары. Больше подходит для отключения рекламы и т.п.")]
    //public string[] NC_PRODUCTS;
    // [Tooltip("Многоразовые товары. Больше подходит для покупки игровой валюты и т.п.")]
    //public string[] C_PRODUCTS;

    //public string kProductIDSubscription =  "subscription";

    /// <summary>
    /// Событие, которое запускается при удачной покупке многоразового товара.
    /// </summary>
    // public static event OnSuccessConsumable OnPurchaseConsumable;
    /// <summary>
    /// Событие, которое запускается при удачной покупке не многоразового товара.
    /// </summary>
    //public static event OnSuccessNonConsumable OnPurchaseNonConsumable;
    /// <summary>
    /// Событие, которое запускается при неудачной покупке какого-либо товара.
    /// </summary>
    //public static event OnFailedPurchase PurchaseFailed;

    //public static event OnSuccessSubscription OnPurchaseSubscription;


    /// <summary>
    /// Проверить, куплен ли товар.
    /// </summary>
    /// <param name="id">Индекс товара в списке.</param>
    /// <returns></returns>
    /*public static bool CheckBuyState(string id)
    {
        Product product = m_StoreController.products.WithID(id);
        if (product.hasReceipt) { return true; }
        else { return false; }
    }
 
    public void InitializePurchasing()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        foreach (string s in C_PRODUCTS) builder.AddProduct(s, ProductType.Consumable);
        foreach (string s in NC_PRODUCTS) builder.AddProduct(s, ProductType.NonConsumable);
        builder.AddProduct(kProductIDSubscription, ProductType.Subscription);
        UnityPurchasing.Initialize(this, builder);
    }
 
    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }
 
    public void BuyConsumable(int index)
    {
        currentProductIndex = index;
        BuyProductID(C_PRODUCTS[index]);
    }
 
    public void BuyNonConsumable(int index)
    {
        currentProductIndex = index;
        BuyProductID(NC_PRODUCTS[index]);
    }
 
    public void BuySubscription(){
        BuyProductID(kProductIDSubscription);
    }

    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);
 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                OnPurchaseFailed(product, PurchaseFailureReason.ProductUnavailable);
            }
        }
    }
 
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
 
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }
 
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }
 
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (C_PRODUCTS.Length > 0 && String.Equals(args.purchasedProduct.definition.id, C_PRODUCTS[currentProductIndex], StringComparison.Ordinal))
            OnSuccessC(args);
        else if (NC_PRODUCTS.Length > 0 && String.Equals(args.purchasedProduct.definition.id, NC_PRODUCTS[currentProductIndex], StringComparison.Ordinal))
            OnSuccessNC(args);
        else if (kProductIDSubscription != null && String.Equals(args.purchasedProduct.definition.id, kProductIDSubscription, StringComparison.Ordinal))
        OnSuccessS(args);
        else Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        return PurchaseProcessingResult.Complete;
    }
 
    public delegate void OnSuccessConsumable(PurchaseEventArgs args);
    protected virtual void OnSuccessC(PurchaseEventArgs args)
    {
        if (OnPurchaseConsumable != null) OnPurchaseConsumable(args);
        Debug.Log(C_PRODUCTS[currentProductIndex] + " Buyed!");
    }
    public delegate void OnSuccessNonConsumable(PurchaseEventArgs args);
    protected virtual void OnSuccessNC(PurchaseEventArgs args)
    {
        if (OnPurchaseNonConsumable != null) OnPurchaseNonConsumable(args);
        Debug.Log(NC_PRODUCTS[currentProductIndex] + " Buyed!");
    }
    public delegate void OnSuccessSubscription(PurchaseEventArgs args);
    protected virtual void OnSuccessS(PurchaseEventArgs args)
    {
        if (OnPurchaseSubscription != null) OnPurchaseSubscription(args);
        Debug.Log(kProductIDSubscription + " Buyed!");
    }
    public delegate void OnFailedPurchase(Product product, PurchaseFailureReason failureReason);
    protected virtual void OnFailedP(Product product, PurchaseFailureReason failureReason)
    {
        if (PurchaseFailed != null) PurchaseFailed(product,failureReason);
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
 
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        OnFailedP(product,failureReason);
    }
    */
}
