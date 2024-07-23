using BadmintonEventsWebApp.Interfaces;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace BadmintonEventsWebApp.Services
{
    public class PhotoServiceLocal : IPhotoService
    {
        private IWebHostEnvironment _hostingEnvironment;
        private IHttpContextAccessor _httpContextAccessor;

        public PhotoServiceLocal( IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor )
        {
            _hostingEnvironment = environment;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetBaseUrl()
        {
            var request = _httpContextAccessor.HttpContext.Request;

            var host = request.Host.ToUriComponent();

            var pathBase = request.PathBase.ToUriComponent();

            return $"{request.Scheme}://{host}{pathBase}";
        }

        public async Task<ImageUploadResult> AddPhotoAsync( IFormFile file )
        {
            var uploadResult = new ImageUploadResult();
            if ( file.Length > 0 )
            {
                string uploads = Path.Combine( _hostingEnvironment.WebRootPath, "uploads" );
                if ( !Directory.Exists( uploads ) )
                    Directory.CreateDirectory( uploads );

                string ext = Path.GetExtension( file.FileName );
                string randomFile = Guid.NewGuid().ToString() + ext;
                string filePath = Path.Combine( uploads, randomFile );
                using ( Stream fileStream = new FileStream( filePath, FileMode.Create ) )
                {
                    await file.CopyToAsync( fileStream );

                    string uri = $"{GetBaseUrl()}/uploads/{randomFile}";

                    uploadResult.Url = new Uri( uri );
                }
            }
            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync( string publicUrl )
        {
            string baseUrl = GetBaseUrl();

            if ( publicUrl.StartsWith( baseUrl ) )
            {
                string filePath = Path.Combine( _hostingEnvironment.WebRootPath, publicUrl.Substring( GetBaseUrl().Length + 1 ) );

                if ( File.Exists( filePath ) )
                    File.Delete( filePath );
            }

            return await Task.FromResult<DeletionResult>( new DeletionResult() 
                { 
                    Result = "Delete successfully",
                    StatusCode = System.Net.HttpStatusCode.OK
                } );
        }
    }
}
