namespace Carpool.CoreApi.Core.Errors
{
    public class ModelStateErrorResponse
    {
        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Errors list
        /// </summary>
        public List<string> Errors { get; set; }
    }
}
