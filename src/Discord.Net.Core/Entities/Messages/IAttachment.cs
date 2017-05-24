namespace Discord
{
    public interface IAttachment
    {
        ulong Id { get; }

        /// <summary>
        /// The display name of the attached file
        /// </summary>
        string Filename { get; }
        /// <summary>
        /// The URL of this attachment
        /// </summary>
        string Url { get; }
        /// <summary>
        /// The URL of this attachent, behind Discord's proxy
        /// </summary>
        string ProxyUrl { get; }
        /// <summary>
        /// The size of the attachment
        /// </summary>
        int Size { get; }
        /// <summary>
        /// The height of the attachment, if it is an image
        /// </summary>
        int? Height { get; }
        /// <summary>
        /// The width of the attachment, if it is an image
        /// </summary>
        int? Width { get; }
    }
}
