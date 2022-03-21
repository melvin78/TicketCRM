using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using MimeKit;
using Microsoft.Extensions.Logging;
using TicketCRM.DataLayer.EmailTemplates.Services;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;


namespace IdentityServerAspNetIdentity.EmailService
{
    public class EmailTemplateResolver<T>: IEmailTemplateResolver<T>
    {
        
        private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;
        
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<IEmailTemplateResolver<T>> _logger;


        public EmailTemplateResolver(IRazorViewToStringRenderer razorViewToStringRenderer,
            IWebHostEnvironment webHostEnvironment,ILogger<IEmailTemplateResolver<T>> logger)
        {
            _razorViewToStringRenderer = razorViewToStringRenderer;
            
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }
        

        public async Task<BodyBuilder> BuildEmailBodyAsync(MailViewModelDTO mailViewModelDto, T model)
        { 
            var headerImagePath = string.Format("{0}/{1}", 
                _webHostEnvironment.ContentRootPath, mailViewModelDto.LinkedResourceContentPath);
            
            _logger.LogInformation(headerImagePath);
            
            

            Bitmap bmp = new Bitmap(headerImagePath);
            byte[] imageBytes = null;
            using (var ms = new MemoryStream())
            {
                bmp.Save(ms, bmp.RawFormat);
                imageBytes = ms.ToArray();
            }
    
            var bodyBuilder = new BodyBuilder();
            var contentType =
                new ContentType("image", $"{GetImageExtension(bmp.RawFormat).Replace(".", string.Empty)}");
            var header = bodyBuilder.LinkedResources.Add("webmelvin",imageBytes, contentType);

          var finalRazorString =  await _razorViewToStringRenderer.
              RenderViewToStringAsync(mailViewModelDto.EmailTemplatePath, model);
      
          
          
          header.ContentId = mailViewModelDto.LinkedResourceContentId;

          
          bodyBuilder.HtmlBody = finalRazorString;

          return bodyBuilder;
          

        }
        
        private string GetImageExtension(System.Drawing.Imaging.ImageFormat format)
        {
            var extension = ImageCodecInfo.GetImageEncoders()
                .Where(ie => ie.FormatID == format.Guid)
                .Select(ie => ie.FilenameExtension
                    .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .First()
                    .Trim('*')
                    .ToLower())
                .FirstOrDefault();
            return extension;
        }
    }
}