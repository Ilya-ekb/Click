using System;
using System.Globalization;
using Ecs.Components;
using Providers;
using UnityEngine.UIElements;

namespace Views
{
    public class UpgradeItem : IView
    {
        public event Action OnUpgradeClick;
        private Upgrade currentUpgrade;
        private readonly VisualElement rootVisualElement;
        private readonly Label title;
        private readonly Label incomeInfo;
        private readonly Label price;
        private readonly Label priceTitle;
        private readonly NamesProvider namesProvider;
        private const string purchasedValue = "Purchased";

        public UpgradeItem(NamesProvider namesProvider, VisualElement rootVisualElement)
        {
            this.rootVisualElement = rootVisualElement;
            this.rootVisualElement.RegisterCallback<ClickEvent>(OnUpgrade);
            this.namesProvider = namesProvider;
            title = rootVisualElement.Q<Label>("title");
            incomeInfo = rootVisualElement.Q("income-info").Q<Label>("value");
            priceTitle = rootVisualElement.Q("price").Q<Label>("title");
            price = rootVisualElement.Q("price").Q<Label>("value");
        }

        ~UpgradeItem()
        {
            rootVisualElement.UnregisterCallback<ClickEvent>(OnUpgrade);
            OnUpgradeClick = null;
        }

        public void Update<TData>(ref TData data) where TData : IComponent
        {
            switch (data)
            {
                case Upgrade upgradeData:
                    currentUpgrade = upgradeData;
                    title.text = namesProvider?[currentUpgrade.Name];
                    incomeInfo.text = $"+{currentUpgrade.Multiplier * 100}%";
                    var priceValue = upgradeData.IsPurchased
                        ? string.Empty
                        : currentUpgrade.Cost.ToString(CultureInfo.InvariantCulture) + "$";
                    price.text = priceValue;
                    if (upgradeData.IsPurchased)
                        priceTitle.text = purchasedValue;
                    break;
                case Balance player:
                    rootVisualElement.enabledSelf = CanUpgrade(player);
                    break;
            }
        }

        private bool CanUpgrade(Balance player) => !currentUpgrade.IsPurchased && currentUpgrade.Cost <= player.Value;

        private void OnUpgrade(ClickEvent evt) => OnUpgradeClick?.Invoke();
    }
}