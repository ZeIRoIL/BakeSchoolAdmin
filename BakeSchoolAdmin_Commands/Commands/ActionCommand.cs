namespace BakeSchoolAdmin_Commands.Commands
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Class for the action commands
    /// Derives from <see cref="ICommand"/>
    /// </summary>
    public class ActionCommand : ICommand
    {
        /// <summary>
        /// this method is called when Execute() is invoked
        /// </summary>
        private readonly Action<object> handlerExecute;

        /// <summary>
        /// this method is called when CanExecute() is invoked
        /// </summary>
        private readonly Func<object, bool> handlerCanExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionCommand"/> class
        /// </summary>
        /// <param name="execute">this method called when execute is invoked.</param>
        /// <param name="canExecute">the method called when CanExecute() is invoked</param>
        public ActionCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            this.handlerExecute = execute
                ?? throw new ArgumentNullException("Execute cannot be null");
            this.handlerCanExecute = canExecute;
        }

        /// <summary>
        /// Gets executed when changes happen which affect whether or not the command should execute
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        ///  Defines the method that determines if the command can execute in this current state
        /// </summary>
        /// <param name="parameter"> Data used by the command</param>
        /// <returns><c>true</c>if the command can execute, otherwise <c>false</c></returns>
        public bool CanExecute(object parameter)
        {
            if (this.handlerCanExecute == null)
            {
                return true;
            }

            return this.handlerCanExecute(parameter);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked
        /// </summary>
        /// <param name="parameter">Data used by the command</param>
        public void Execute(object parameter)
        {
            this.handlerExecute(parameter);
        }
    }
}
