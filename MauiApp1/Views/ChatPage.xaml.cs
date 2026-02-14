using MauiApp1.ViewModels;

namespace MauiApp1.Views;

public partial class ChatPage : ContentPage
{
    public ChatPage()
    {
        InitializeComponent();
    }

    private async void OnConfigClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ConfigPage());
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ChatViewModel vm)
        {
            vm.Messages.CollectionChanged += Messages_CollectionChanged;
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (BindingContext is ChatViewModel vm)
        {
            vm.Messages.CollectionChanged -= Messages_CollectionChanged;
        }
    }

    private void Messages_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
        {
            if (BindingContext is ChatViewModel vm && vm.Messages.Count > 0)
            {
                // Delay slightly to allow UI to render the new item
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Delay(100);
                    MessagesCollection.ScrollTo(vm.Messages.Last(), position: ScrollToPosition.End, animate: true);
                });
            }
        }
    }
}
