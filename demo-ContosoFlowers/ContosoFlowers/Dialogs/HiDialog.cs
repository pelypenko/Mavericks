namespace ContosoFlowers.Dialogs
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using System.Linq;
    using System.Threading;

    [Serializable]
    public class HiDialog : IDialog<string>
    {
        public Task StartAsync(IDialogContext context)
        {
            var text = context.Activity.AsMessageActivity().Text.ToLower();
            var responce = string.Empty;

            if ((new[] { "hi", "hey", "hello", "good day" }).Contains(text))
            {
                responce = "Hello!";
            }
            else if ((new[] { "thanks", "thank you", "tnk" }).Contains(text))
            {
                responce = ";)";
            }

            context.PostAsync(responce);
            context.Done("");

            //context.Wait(MessageReceivedAsync);
            return Task.CompletedTask;
        }

        //private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        //{
        //    var text = context.Activity.AsMessageActivity().Text.ToLower();
        //    var responce = string.Empty;

        //    if ((new[] { "hi", "hey", "hello", "good day" }).Contains(text))
        //    {
        //        responce = "Hello! How can I help you?";
        //    }
        //    else if ((new[] { "thanks", "thank you", "tnk" }).Contains(text))
        //    {
        //        responce = ";)";
        //    }

        //    await context.PostAsync(responce);

        //    context.Done("");
        //}
    }
}
