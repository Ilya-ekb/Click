using Ecs.Components;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Views
{
    public class MainView : IView
    {
        private readonly Label balance;

        public MainView(Bootstrap bootstrap, VisualElement visualRoot)
        {
            balance = visualRoot.Q("balance").Q<Label>("value");
            var restartButton = visualRoot.Q<Button>("restart");
            var exitButton = visualRoot.Q<Button>("exit");
            restartButton.RegisterCallback<ClickEvent>(_ => bootstrap.Restart());
            exitButton.RegisterCallback<ClickEvent>(_ => bootstrap.Exit());
        }

        public void Update<TData>(ref TData data) where TData : IComponent
        {
            switch (data)
            {
                case Balance player:
                    balance.text = $"{player.Value}$";
                    break;
            }
        }
    }
}