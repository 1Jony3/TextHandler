
namespace Domain
{
    public class Invoker
    {
        private ICommand _onStart;

        public void SetOnStart(ICommand command) => this._onStart = command;
        public void DoSomething()
        {
            
            if (this._onStart is ICommand)
            {
                this._onStart.Execute();
            }

        }
    }

}
