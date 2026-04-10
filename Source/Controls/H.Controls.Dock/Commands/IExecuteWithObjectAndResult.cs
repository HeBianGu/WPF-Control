/************************************************************************
  AvalonDock

  Copyright (C) 2007-2013 Xceed Software Inc.

  This program is provided to you under the terms of the Microsoft Public
  License (Ms-PL) as published at https://opensource.org/licenses/MS-PL
************************************************************************/
namespace H.Controls.Dock.Commands
{
    internal interface IExecuteWithObjectAndResult
    {
        #region Public Methods

        /// <summary>
        /// Executes a Func and returns the result.
        /// </summary>
        /// <param name="parameter">A parameter passed as an object,
        /// to be casted to the appropriate type.</param>
        /// <returns>The result of the operation.</returns>
        object ExecuteWithObject(object parameter);

        #endregion Public Methods
    }
}