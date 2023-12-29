using Groovy.Services.Contracts;
using Microsoft.Toolkit.Uwp.Notifications;

namespace Groovy.Services.Repository
{
    public class WindowsNotificationHelper : INotificationHelper
    {
        public void ShowNotification(string title, string text)
        {
            CreateBasicToast().Show();
        }

        private ToastContentBuilder CreateBasicToast()
        {
            var builder = new ToastContentBuilder()
                .SetBackgroundActivation()
                .AddArgument("action", "toastClicked")
                .AddText("My Toast Notification")
                .AddText("Check this out, this is a cool toast description!");
            return builder;
        }

        private ToastContentBuilder CreateBarToast()
        {
            var builder = new ToastContentBuilder()
                .AddVisualChild(new AdaptiveProgressBar()
                 {
                     Title = "Weekly playlist",
                     Value = new BindableProgressBarValue("progressValue"),
                     ValueStringOverride = new BindableString("progressValueString"),
                     Status = new BindableString("progressStatus")
                 });
            return builder;
        }

        private void ShowToastOkBtn()
        {
            var builder = CreateBasicToast()
                .AddButton(new ToastButton()
                    .SetContent("Ok")
                    .AddArgument("action", "ok")
                    .SetBackgroundActivation());
        }

        public void ShowToastYesNoBtn()
        {
            var builder = CreateBasicToast()
                .AddButton(new ToastButton()
                    .SetContent("Yes")
                    .AddArgument("action", "yes")
                    .SetBackgroundActivation())
                .AddButton(new ToastButton()
                    .SetContent("No")
                    .AddArgument("action", "no")
                    .SetBackgroundActivation());
        }

        public ToastContentBuilder CreateToastWithImage(bool useHeroImage)
        {
            var builder = CreateBasicToast();

            var uri = new Uri("https://picsum.photos/360/202?image=1043");
            if (!useHeroImage)
                builder.AddInlineImage(uri);
            else
                builder.AddHeroImage(uri);

            return builder;
        }

        public void ShowToastWithImageAndThumbs()
        {
            var builder = CreateToastWithImage(true)
                .AddButton(new ToastButton()
                    .SetContent("Thumbs up")
                    .AddArgument("action", "thumbsUp")
                    .SetBackgroundActivation())
                .AddButton(new ToastButton()
                    .SetContent("Thumbs Down")
                    .AddArgument("action", "thumbsDown")
                    .SetBackgroundActivation());
        }

        public void ShowToastWithDropdown()
        {
            var builder = CreateBasicToast()
                .AddButton(new ToastButton()
                    .SetContent("Select")
                    .AddArgument("action", "select")
                    .SetBackgroundActivation())

                // the ID "options" becomes the key that we use to access the selected value.
                .AddToastInput(new ToastSelectionBox("options")
                {
                    // default item is based off of the ID, not the value. Value is just for display.
                    DefaultSelectionBoxItemId = "lunch",
                    Items =
                    {
                    // the ID is what gets passed as an argument
                    // the value is the text that gets displayed in the app.
                    new ToastSelectionBoxItem("breakfast", "Breakfast"),
                    new ToastSelectionBoxItem("lunch", "Lunch"),
                    new ToastSelectionBoxItem("dinner", "Dinner"),
                    }
                });
        }
    }
}
