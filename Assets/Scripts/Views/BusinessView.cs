using System;
using Ecs.Components;
using Providers;
using UnityEngine;
using UnityEngine.UIElements;

namespace Views
{
    public class BusinessItem : IView
    {
        public event Action OnLevelUp; 
        private readonly Label businessName;
        private readonly Label lvl;
        private readonly Label income;
        private readonly VisualElement lvlUpBtn;
        private readonly Label lvlUpPrice;
        private readonly IView progressView;
        private readonly IView[] upgradesViews;
        private readonly NamesProvider namesProvider;
        private Business currentBusiness;

        public BusinessItem(NamesProvider namesProvider, VisualElement parentElement)
        {
            this.namesProvider = namesProvider;
            var businessesContainer = parentElement.Q<ScrollView>("businesses");
            var assetTree = Resources.Load<VisualTreeAsset>($"UI/{nameof(BusinessItem)}");
            var root = assetTree.CloneTree().contentContainer;
            businessesContainer.Add(root);
            
            progressView = new ProgressView(root.Q<ProgressBar>());
            businessName = root.Q("name").Q<Label>("value");
            lvl = root.Q("lvl").Q<Label>("value");
            income = root.Q("income").Q<Label>("value");
            lvlUpBtn = root.Q("lvlup-btn");
            lvlUpBtn.RegisterCallback<ClickEvent>(OnLevelUpClick);
            lvlUpPrice = lvlUpBtn.Q<Label>("value");
            var upgradeButtons = root.Q("upgrade-control");
            upgradesViews = new IView[upgradeButtons.childCount];
            for (var i = 0; i < upgradeButtons.childCount; i++)
                upgradesViews[i] = new UpgradeItem(namesProvider, upgradeButtons[i]);
        }

        public IView GetUpgradeView(int upgradeIndex) => upgradesViews[upgradeIndex];

        public void Update<TData>(ref TData data) where TData : IComponent
        {
            switch (data)
            {
                case Business business:
                    currentBusiness = business;
                    var name = namesProvider?[business.Name];
                    businessName.text = name;
                    lvl.text = business.Level.ToString();
                    income.text = $"{business.CurrentIncome}$";
                    lvlUpPrice.text = $"{business.CurrentLevelUpCost}$";
                    break;
                case Progress:
                    data.View = progressView;
                    break;
                case Balance player:
                    lvlUpBtn.enabledSelf = currentBusiness.CurrentLevelUpCost <= player.Value;
                    break;
            }
        }

        private void OnLevelUpClick(ClickEvent evt) => OnLevelUp?.Invoke();
    }
}