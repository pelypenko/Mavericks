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
        private Meeting meeting = new Meeting();

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var text = context.Activity.AsMessageActivity().Text.ToLower();

            if ((text.Contains("get") || text.Contains("show") || text.Contains("browse")) && text.Contains("request"))
            {
                context.Call(new IntakeRequestsDialog(), this.ResumeAfterRequestDialog);
            }
            else if ((new[] { "thanks", "thank you", "tnk", "hi", "hey", "hello", "good day" }).Contains(text))
            {
                context.Call(new HiDialog(), this.ResumeAfterHiDialog);
            }
            else if ((text.Contains("find") || text.Contains("get")) && (text.Contains("responsible") || text.Contains("resp")) && (text.Contains("lawyer") || text.Contains("person")))
            {
                context.Call(new ResponsibleLawyersRequestsDialog(), this.ResumeAfterHiDialog);
            }
            else if (text.Contains("book") && text.Contains("meeting") && text.Contains("with") && text.Contains("larry"))
            {
                context.Call(new BookMeetingRequestsDialog(), this.ResumeAfterDurationDialogAsync);
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

        private async Task ResumeAfterDurationDialogAsync(IDialogContext context, IAwaitable<string> result)
        {
            var duration = await result;
            this.meeting.Duration = duration;

            context.Call(new MeetingTitleDialog(), this.AfterMeetingTitleDialogResumeAsync);
        }

        private async Task AfterMeetingTitleDialogResumeAsync(IDialogContext context, IAwaitable<object> result)
        {
            var allowDates = new string[]
           {   $"Mon, Sep 2, 2017 | 10:00 - 10:{meeting.Duration}",
                $"Mon, Sep 2, 2017 | 13:00 - 13:{meeting.Duration}",
                $"Mon, Sep 2, 2017 | 17:30 - 17:{meeting.Duration}"
           };
            var title = await result;
            this.meeting.Title = title.ToString();
            PromptDialog.Choice(
               context,
               this.AfterDateChoiceSelected,
               allowDates,
               "Which do you like?",
               @"I am sorry but there are no suit time for this meeting",
               attempts: allowDates.Length);
        }

        private async Task AfterDateChoiceSelected(IDialogContext context, IAwaitable<string> result)
        {
            var date = await result;
            meeting.Person = "Larry Tomson";
            meeting.Date = date.ToString();
            await context.PostAsync($"Meeting: {meeting.Title} with {meeting.Person}" +
                $" on {meeting.Date} has succesfully booked!");
        }

        private async Task ResumeAfterSupportDialog(IDialogContext context, IAwaitable<int> result)
        {
            var ticketNumber = await result;

            //await context.PostAsync($"Thanks for contacting our support team. Your ticket number is {ticketNumber}.");
            context.Wait(this.MessageReceivedAsync);
        }

    }

}