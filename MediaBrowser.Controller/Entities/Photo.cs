using MediaBrowser.Model.Drawing;
using MediaBrowser.Model.Serialization;

namespace MediaBrowser.Controller.Entities
{
    public class Photo : BaseItem
    {
        [IgnoreDataMember]
        public override bool SupportsLocalMetadata => false;

        [IgnoreDataMember]
        public override string MediaType => Model.Entities.MediaType.Photo;

        [IgnoreDataMember]
        public override Folder LatestItemsIndexContainer => AlbumEntity;


        [IgnoreDataMember]
        public PhotoAlbum AlbumEntity
        {
            get
            {
                var parents = GetParents();
                foreach (var parent in parents)
                {
                    var photoAlbum = parent as PhotoAlbum;
                    if (photoAlbum != null)
                    {
                        return photoAlbum;
                    }
                }
                return null;
            }
        }

        public override bool CanDownload()
        {
            return true;
        }

        public override double GetDefaultPrimaryImageAspectRatio()
        {
            // REVIEW: @bond
            if (Width.HasValue && Height.HasValue)
            {
                double width = Width.Value;
                double height = Height.Value;

                if (Orientation.HasValue)
                {
                    switch (Orientation.Value)
                    {
                        case ImageOrientation.LeftBottom:
                        case ImageOrientation.LeftTop:
                        case ImageOrientation.RightBottom:
                        case ImageOrientation.RightTop:
                            var temp = height;
                            height = width;
                            width = temp;
                            break;
                    }
                }

                return width / height;
            }

            return base.GetDefaultPrimaryImageAspectRatio();
        }

        public new int? Width { get; set; }
        public new int? Height { get; set; }
        public string CameraMake { get; set; }
        public string CameraModel { get; set; }
        public string Software { get; set; }
        public double? ExposureTime { get; set; }
        public double? FocalLength { get; set; }
        public ImageOrientation? Orientation { get; set; }
        public double? Aperture { get; set; }
        public double? ShutterSpeed { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? Altitude { get; set; }
        public int? IsoSpeedRating { get; set; }
    }
}
