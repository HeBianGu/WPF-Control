

using System.Windows.Controls;
using System.Windows.Documents.Serialization;
using System.Windows.Documents;
using System.Windows.Input;
using System.Printing;

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
