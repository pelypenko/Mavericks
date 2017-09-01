using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;

namespace ContosoFlowers.Dialogs
{
    [Serializable]
    public class MeetingTitleDialog : IDialog<object>
    {

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Please set meeting title");
            context.Wait(this.MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            if (message.Text != null)
            {
                context.Done(message.Text);
            }
            else
            {
                await StartAsync(context);
            }

        }
    }
}