namespace NavigationRendererRepro;

public class TestPage : ContentPage
{
    public TestPage()
    {
        var button = new Button()
        {
            Text = "Test"
        };

        button.Clicked += (sender, args) =>
        {
            DocumentService.OpenDocumentMenu("https://www.africau.edu/images/default/sample.pdf");
        };

        Content = new StackLayout()
        {
            Children =
            {
                button
            }
        };
    }
}