// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Runtime.CompilerServices;

namespace H.Services.Operation;

public interface IOperationService
{
    void Log<T>(string title, string message = null, OperationType operationType = OperationType.Default, bool result = true, [CallerMemberName] string methodName = null);
}