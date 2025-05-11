// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Printing;
using System.Windows.Controls;

namespace H.Controls.PrintBox
{
    public class PrintDocumentViewer : DocumentViewer
    {
        protected override void OnPrintCommand()
        {
            var pq = LocalPrintServer.GetDefaultPrintQueue();
            var writer = PrintQueue.CreateXpsDocumentWriter(pq);
            var paginator = this.Document.DocumentPaginator;
            writer.Write(paginator);
        }
    }

}
