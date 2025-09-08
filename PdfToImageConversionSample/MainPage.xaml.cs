using Microsoft.Maui.Graphics.Platform;
using Syncfusion.Maui.PdfToImageConverter;
using System.Reflection;
using Syncfusion.Pdf.Security;
using Syncfusion.Pdf.Parsing;

namespace PdfToImageConversionSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("YOUR LICENSE KEY");
        }

        private void ConvertSinglePdfPageToImage_Clicked(object sender, EventArgs e)
        {

            //Get the PDF file stream to convert it as image.
            Stream? pdfFile = typeof(MainPage).GetTypeInfo().Assembly
                                             .GetManifestResourceStream("PdfToImageConversionSample.Resources.Pdf.input.pdf");

            //Initialize the PDF to Image converter
            PdfToImageConverter converter = new PdfToImageConverter();

            //Load the document
            converter.Load(pdfFile!);

            //Convert the PDF page as image.
            Stream? imageStream = converter.Convert(0);

            if (imageStream != null)
            {
                this.ImageContainer.Clear(); // Clear previous content if needed
                                             // Add the image to the layout using the stream
                ImageContainer.Children.Add(new Image
                {
                    Source = ImageSource.FromStream(() => imageStream)
                });
            }

            //Dispose the converter
            converter.Dispose();
        }

        private void ConvertMultiplePagesToImages_Clicked(object sender, EventArgs e)
        {
            //Get the PDF file stream to convert it as image.
            Stream? pdfFile = typeof(MainPage).GetTypeInfo().Assembly
                                             .GetManifestResourceStream("PdfToImageConversionSample.Resources.Pdf.input.pdf");

            //Initialize the PDF to Image converter
            PdfToImageConverter converter = new PdfToImageConverter();

            //Load the document
            converter.Load(pdfFile!);

            Stream[]? images = converter.Convert();

            if (images != null)
            {
                this.ImageContainer.Clear(); // Clear previous content if needed

                foreach (Stream imageStream in images)
                {
                    ImageContainer.Children.Add(new Image
                    {
                        Source = ImageSource.FromStream(() => imageStream)
                    });
                    // Add a light gray divider line
                    ImageContainer.Children.Add(new BoxView
                    {
                        HeightRequest = 1.5,
                        BackgroundColor = Colors.LightGray,
                        HorizontalOptions = LayoutOptions.Fill
                    });
                }
            }
            //Dispose the converter
            converter.Dispose();

        }

        private void ConvertWithCustomScaleImages_Clicked(object sender, EventArgs e)
        {
            //Get the PDF file stream to convert it as image.
            Stream? pdfFile = typeof(MainPage).GetTypeInfo().Assembly
                                             .GetManifestResourceStream("PdfToImageConversionSample.Resources.Pdf.input.pdf");

            //Initialize the PDF to Image converter
            PdfToImageConverter converter = new PdfToImageConverter();

            //Load the document
            converter.Load(pdfFile!);

            Stream[]? images = converter.Convert(0.2f);

            if (images != null)
            {
                this.ImageContainer.Clear(); // Clear previous content if needed

                foreach (Stream imageStream in images)
                {
                    // Get image dimensions
                    Microsoft.Maui.Graphics.IImage platformImage = PlatformImage.FromStream(imageStream);
                    float width = platformImage.Width;
                    float height = platformImage.Height;

                    platformImage.Dispose();

                    imageStream.Position = 0;

                    Image image = new Image
                    {
                        Source = ImageSource.FromStream(() => imageStream),
                        WidthRequest = width,
                        HeightRequest = height
                    };

                    // Create label with size info
                    var sizeLabel = new Label
                    {
                        Text = $"{width} x {height}",
                        FontSize = 12,
                        BackgroundColor = Colors.Black.WithAlpha(0.5f),
                        TextColor = Colors.White,
                        Padding = new Thickness(4),
                        HorizontalOptions = LayoutOptions.End,
                        VerticalOptions = LayoutOptions.End,
                        Margin = new Thickness(5)
                    };

                    // Wrap image and label in a Grid
                    var imageGrid = new Grid();
                    imageGrid.Children.Add(image);
                    imageGrid.Children.Add(sizeLabel);

                    ImageContainer.Children.Add(imageGrid);

                    // Add a light gray divider line
                    ImageContainer.Children.Add(new BoxView
                    {
                        HeightRequest = 1.5,
                        BackgroundColor = Colors.LightGray,
                        HorizontalOptions = LayoutOptions.Fill
                    });
                }
            }
            //Dispose the converter
            converter.Dispose();
        }

        private void ConvertWithCustomSizeImages_Clicked(object sender, EventArgs e)
        {
            //Get the PDF file stream to convert it as image.
            Stream? pdfFile = typeof(MainPage).GetTypeInfo().Assembly
                                             .GetManifestResourceStream("PdfToImageConversionSample.Resources.Pdf.input.pdf");

            //Initialize the PDF to Image converter
            PdfToImageConverter converter = new PdfToImageConverter();

            //Load the document
            converter.Load(pdfFile!);

            Stream[]? images = converter.Convert(new SizeF(1200, 1800));

            if (images != null)
            {
                this.ImageContainer.Clear(); // Clear previous content if needed

                foreach (Stream imageStream in images)
                {
                    // Get image dimensions
                    Microsoft.Maui.Graphics.IImage platformImage = PlatformImage.FromStream(imageStream);
                    float width = platformImage.Width;
                    float height = platformImage.Height;

                    platformImage.Dispose();

                    imageStream.Position = 0;

                    Image image = new Image
                    {
                        Source = ImageSource.FromStream(() => imageStream),
                        WidthRequest = 400,
                        HeightRequest = 504
                    };

                    // Create label with size info
                    var sizeLabel = new Label
                    {
                        Text = $"{width} x {height}",
                        FontSize = 12,
                        BackgroundColor = Colors.Black.WithAlpha(0.5f),
                        TextColor = Colors.White,
                        Padding = new Thickness(4),
                        HorizontalOptions = LayoutOptions.End,
                        VerticalOptions = LayoutOptions.End,
                        Margin = new Thickness(5)
                    };

                    // Wrap image and label in a Grid
                    var imageGrid = new Grid();
                    imageGrid.Children.Add(image);
                    imageGrid.Children.Add(sizeLabel);

                    ImageContainer.Children.Add(imageGrid);

                    // Add a light gray divider line
                    ImageContainer.Children.Add(new BoxView
                    {
                        HeightRequest = 1.5,
                        BackgroundColor = Colors.LightGray,
                        HorizontalOptions = LayoutOptions.Fill
                    });
                }
            }
            //Dispose the converter
            converter.Dispose();
        }

        private async void ConvertPdfToImageAsync_Clicked(object sender, EventArgs e)
        {
            //Get the PDF file stream to convert it as image.
            Stream? pdfFile = typeof(MainPage).GetTypeInfo().Assembly
                                             .GetManifestResourceStream("PdfToImageConversionSample.Resources.Pdf.input.pdf");

            //Initialize the PDF to Image converter
            using (PdfToImageConverter converter = new PdfToImageConverter())
            {
                //Load the document
                converter.Load(pdfFile!);

                //Convert the PDF pages as images asynchronously.
                Stream[]? images = await converter.ConvertAsync();

                ImageContainer.Children.Clear();

                if (images != null)
                {
                    this.ImageContainer.Clear(); // Clear previous content if needed

                    foreach (Stream imageStream in images)
                    {
                        ImageContainer.Children.Add(new Image
                        {
                            Source = ImageSource.FromStream(() => imageStream)
                        });
                        // Add a light gray divider line
                        ImageContainer.Children.Add(new BoxView
                        {
                            HeightRequest = 1.5,
                            BackgroundColor = Colors.LightGray,
                            HorizontalOptions = LayoutOptions.Fill
                        });
                    }
                }
            }
        }

        private async void ConvertPassword_Clicked(object sender, EventArgs e)
        {
            //Get the PDF file stream to convert it as image.
            Stream? pdfFile = typeof(MainPage).GetTypeInfo().Assembly
                                             .GetManifestResourceStream("PdfToImageConversionSample.Resources.Pdf.input-password.pdf");

            //Initialize the PDF to Image converter
            using (PdfToImageConverter converter = new PdfToImageConverter())
            {
#if ANDROID
                //Load the document with password
                PdfLoadedDocument document = new PdfLoadedDocument(pdfFile, "syncfusion");
                //Remove the password protection
                document.Security.UserPassword = string.Empty;
                document.Security.OwnerPassword = string.Empty;
                MemoryStream stream = new MemoryStream();
                document.Save(stream);
                stream.Position = 0;
                document.Close(true);
                converter.Load(stream);
#else
                //Load the document with password
                converter.Load(pdfFile!, "syncfusion");
#endif
                //Convert the PDF pages as images asynchronously.
                Stream[]? images = await converter.ConvertAsync();

                ImageContainer.Children.Clear();

                if (images != null)
                {
                    this.ImageContainer.Clear(); // Clear previous content if needed

                    foreach (Stream imageStream in images)
                    {
                        ImageContainer.Children.Add(new Image
                        {
                            Source = ImageSource.FromStream(() => imageStream)
                        });
                        // Add a light gray divider line
                        ImageContainer.Children.Add(new BoxView
                        {
                            HeightRequest = 1.5,
                            BackgroundColor = Colors.LightGray,
                            HorizontalOptions = LayoutOptions.Fill
                        });
                    }
                }
            }
        }
    }
}
