namespace CreateNewConversationBot
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.Internals.Fibers;
    using Microsoft.Bot.Connector;

    [Serializable]
    public class CreateNewClientNewMatterConversationDialog : IDialog<object>
    {
        private readonly ISurveyService surveyService;

        public CreateNewClientNewMatterConversationDialog(ISurveyService surveyService)
        {
            SetField.NotNull(out this.surveyService, nameof(surveyService), surveyService);
        }

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var value = await result;
            var message = value.Text.ToLower();

            await context.PostAsync("New request will start in a few seconds...");

            await this.surveyService.QueueSurveyAsync();

            context.Wait(this.MessageReceivedAsync);
        }
    }
}