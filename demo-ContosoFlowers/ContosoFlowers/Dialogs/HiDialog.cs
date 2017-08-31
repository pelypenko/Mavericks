namespace ContosoFlowers.Dialogs
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using System.Linq;

    [Serializable]
    public class HiDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            var text = activity.Text.ToLower();
            var responce = string.Empty; 

            if ((new[] { "hi", "hey", "hello", "good day" }).Contains(text))
            {
                responce = "Hello! How can I help you?";
            }
            else if ((new[] { "thanks", "thank you", "tnk" }).Contains(text))
            {
                responce = ";)";
            }

            await context.PostAsync(responce);

            context.Wait(MessageReceivedAsync);
        }
    }
}
