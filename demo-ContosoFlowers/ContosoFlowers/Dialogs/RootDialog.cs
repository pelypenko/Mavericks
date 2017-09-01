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
            var text = context.Activity.AsMessageActivity().Text.ToLower();

            if ((text.Contains("get") || text.Contains("show") || text.Contains("browse")) && text.Contains("request"))
            {
                //await context.Forward(new IntakeRequestsDialog(), this.ResumeAfterRequestDialog, text, CancellationToken.None);
                context.Call(new IntakeRequestsDialog(), this.ResumeAfterRequestDialog);
            }
            else if ((new[] { "thanks", "thank you", "tnk", "hi", "hey", "hello", "good day" }).Contains(text))
            {
                //await context.Forward(new HiDialog(), this.ResumeAfterHiDialog, text, CancellationToken.None);
                context.Call(new HiDialog(), this.ResumeAfterHiDialog);
            }
        }

        private async Task ResumeAfterHiDialog(IDialogContext context, IAwaitable<string> result)
        {
            var message = await result;

            //await context.PostAsync(message);
            context.Wait(this.MessageReceivedAsync);
        }

        private async Task ResumeAfterRequestDialog(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var reply = await result;

            //await context.PostAsync(reply);
            context.Wait(this.MessageReceivedAsync);
        }

    }

}