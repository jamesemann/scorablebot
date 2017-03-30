using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace ScorableTest.Dialogs.Scorable2
{
    [Serializable]
    public class Scorable2Dialog : IDialog<object>
    {
        private string value;

        public Scorable2Dialog(string value)
        {
            this.value = value;
        }

        public async Task StartAsync(IDialogContext context)
        {
            // State transition - wait for 'start' message from user
            await context.PostAsync(
                "[Scorable2Dialog] You are now in the scorable dialog. Say something to complete the dialog.");
            context.Wait(MessageReceivedStart);
        }

        public async Task MessageReceivedStart(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            await context.PostAsync("[Scorable2Dialog] Done, returning to parent dialog.");
            context.Done(new object());
        }
    }
}