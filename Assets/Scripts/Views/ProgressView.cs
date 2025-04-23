using Ecs.Components;
using UnityEngine.UIElements;

namespace Views
{
    public class ProgressView : IView
    {
        private readonly ProgressBar progressBar;

        public ProgressView(ProgressBar progressBar)
        {
            this.progressBar = progressBar;
        }

        public void Update<TData>(ref TData data) where TData : IComponent
        {
            if(data is not Progress progress) return;
            progressBar.value = progress.Value * progressBar.highValue;
        }
    }
}