using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;

namespace WpfConverters.Behaviors
{
    public static class RichTextBox
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.RegisterAttached(
            "Text", typeof(string), typeof(RichTextBox), new PropertyMetadata(default(string), TextChangedCallback));

        private static void TextChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var richTextBox = dependencyObject as System.Windows.Controls.RichTextBox;
            if (richTextBox == null)
                return;

            var text = e.NewValue as string ?? "";
            var paragraph = new Paragraph(new Run(text));
            DetectUrLs(paragraph);
            richTextBox.Document = new FlowDocument(paragraph);
        }

        public static void SetText(DependencyObject element, string value)
        {
            element.SetValue(TextProperty, value);
        }

        public static string GetText(DependencyObject element)
        {
            return (string)element.GetValue(TextProperty);
        }


        private static readonly Regex UrlRegex = new Regex(@"(?#Protocol)(?:(?:ht|f)tp(?:s?)\:\/\/|~/|/)?(?#Username)(?:[\w\._]+@)?(?#Subdomains)(?:(?:[-\w]+\.)+(?#TopLevel Domains)(?:com|org|net|gov|mil|biz|info|mobi|name|aero|jobs|museum|travel|[a-z]{2,4}))(?#Port)(?::[\d]{1,5})?(?#Directories)(?:(?:(?:/(?:[-\w~!$+|.,=]|%[a-f\d]{2})+)+|/)+|\?|#)?(?#Query)(?:(?:\?(?:[-\w~!$+|.,*:]|%[a-f\d{2}])+=(?:[-\w~!$+|.,*:=]|%[a-f\d]{2})*)(?:&amp;(?:[-\w~!$+|.,*:]|%[a-f\d{2}])+=(?:[-\w~!$+|.,*:=]|%[a-f\d]{2})*)*)*(?#Anchor)(?:#(?:[-\w~!$+|.,*:=]|%[a-f\d]{2})*)?");

        private static void DetectUrLs(Paragraph par)
        {
            var paragraphText = new TextRange(par.ContentStart, par.ContentEnd).Text;

            var matches = UrlRegex.Matches(paragraphText);

            foreach (Match match in matches)
            {
                Uri uri;
                try
                {
                    uri = new Uri(match.Value, UriKind.RelativeOrAbsolute);
                }
                catch (Exception)
                {
                    continue;
                }

                var position = par.ContentStart;

                while (position != null)
                {
                    if (position.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                    {
                        var textRun = position.GetTextInRun(LogicalDirection.Forward);
                        
                        var indexInRun = textRun.IndexOf(match.Value, StringComparison.Ordinal);
                        if (indexInRun > 0)
                        {
                            var start = position.GetPositionAtOffset(indexInRun);
                            var end = start?.GetPositionAtOffset(match.Value.Length);

                            if (end != null)
                            {
                                var link = new System.Windows.Documents.Hyperlink(start, end)
                                {
                                    NavigateUri = uri,
                                };
                                link.Click += Hyperlink_Click;
                                break;
                            }
                        }
                    }

                    position = position.GetNextContextPosition(LogicalDirection.Forward);
                }
            }
        }

        private static void Hyperlink_Click(object sender, EventArgs e)
        {
            var hyperlink = sender as System.Windows.Documents.Hyperlink;
            if (hyperlink?.NavigateUri?.AbsoluteUri == null)
                return;

            Process.Start(hyperlink.NavigateUri.AbsoluteUri);
        }
    }
}
