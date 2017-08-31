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
    public class RootDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            var text = message.Text.ToLower();

            if (text.Contains("get") || text.Contains("show") || text.Contains("browse") && text.Contains("request"))
            {
                await context.Forward(new IntakeRequestsDialog(), null, message, CancellationToken.None);
            }
            else if ((new[] { "thanks", "thank you", "tnk", "hi", "hey", "hello", "good day" }).Contains(text))
            {
                await context.Forward(new HiDialog(), null, message, CancellationToken.None);
            }
        }

        private async Task ResumeAfterSupportDialog(IDialogContext context, IAwaitable<int> result)
        {
            var ticketNumber = await result;

            //await context.PostAsync($"Thanks for contacting our support team. Your ticket number is {ticketNumber}.");
            context.Wait(this.MessageReceivedAsync);
        }
    }

}