

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
