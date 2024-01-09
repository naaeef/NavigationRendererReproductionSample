using Foundation;
using UIKit;

namespace NavigationRendererRepro;

public class DocumentService
{
    public static async Task<bool> OpenDocumentMenu(string url)
    {
        var path = Path.Combine(FileSystem.AppDataDirectory + '/' + "TestPdf" + ".pdf");
        
        using var client = new HttpClient();
        var downloadStream = await client.GetStreamAsync(url);
        await using var fileStream = File.Create(path);
        await downloadStream.CopyToAsync(fileStream);
        
        var currentController = UIApplication.SharedApplication.KeyWindow?.RootViewController;

        var currentView = currentController?.View;

        if (currentView == null) {
            return false;
        }

        var previewController = UIDocumentInteractionController.FromUrl(
            NSUrl.FromFilename(path)
        );

        previewController.Delegate = new DocumentInteractionDelegate(currentController);

        // show the Document Options
        return previewController.PresentOptionsMenu(currentView.Frame, currentView, true);
    }
    
    private class DocumentInteractionDelegate : UIDocumentInteractionControllerDelegate
    {
        private readonly UIViewController _parent;

        public DocumentInteractionDelegate(UIViewController controller)
        {
            _parent = controller;
        }

        public override UIViewController ViewControllerForPreview(UIDocumentInteractionController controller)
        {
            return _parent;
        }
    }
}
