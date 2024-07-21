using CloudinaryDotNet.Actions;

namespace BadmintonEventsWebApp.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync( IFormFile file );

        Task<DeletionResult> DeletePhotoAsync( string publicUrl );
    }
}
