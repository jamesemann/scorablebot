using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Builder.Scorables.Internals;
using Microsoft.Bot.Connector;

namespace ScorableTest.Dialogs.Scorable2
{
    public class Scorable2 : ScorableBase<IActivity, string, double>
    {
        private readonly IDialogStack _stack;

        public Scorable2(IDialogStack stack)
        {
            SetField.NotNull(out _stack, nameof(stack), stack);
        }

        protected override Task DoneAsync(IActivity item, string state, CancellationToken token)
        {
            return Task.CompletedTask;
        }

        protected override double GetScore(IActivity item, string state)
        {
            return state != null && state == "scorable2-triggered" ? 1 : 0;
        }

        protected override bool HasScore(IActivity item, string state)
        {
            return state != null && state == "scorable2-triggered";
        }

        protected override Task PostAsync(IActivity item, string state, CancellationToken token)
        {
            var message = item as IMessageActivity;
            var dialog = new Scorable2Dialog(state);
            var interruption = dialog.Void(_stack);
            _stack.Call(interruption, null);
            return Task.CompletedTask;
        }

        protected override async Task<string> PrepareAsync(IActivity item, CancellationToken token)
        {
            var message = item.AsMessageActivity();
            if (message == null)
                return null;

            var messageText = message.Text;

            if (messageText == "scorable2")
                return "scorable2-triggered";
                    // this value is passed to GetScore/HasScore/PostAsync and can be anything meaningful to the scoring
            return null;
        }
    }
}