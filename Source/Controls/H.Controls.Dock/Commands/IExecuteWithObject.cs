/************************************************************************
  AvalonDock

  Copyright (C) 2007-2013 Xceed Software Inc.

  This program is provided to you under the terms of the Microsoft Public
  License (Ms-PL) as published at https://opensource.org/licenses/MS-PL
************************************************************************/
namespace H.Controls.Dock.Commands
{
    internal interface IExecuteWithObject
    {
        #region Public Properties

        /// <summary>
        /// The target of the WeakAction.
        /// </summary>
        /// <value>The target.</value>
        object Target
        {
            get;
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Executes an action.
        /// </summary>
        /// <param name="parameter">A parameter passed as an object,
        /// to be casted to the appropriate type.</param>
        void ExecuteWithObject(object parameter);

        /// <summary>
        /// Deletes all references, which notifies the cleanup method
        /// that this entry must be deleted.
        /// </summary>
        void MarkForDeletion();

        #endregion Public Methods
    }
}