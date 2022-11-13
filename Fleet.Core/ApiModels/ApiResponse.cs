using Fleet.Core.Dtos;

namespace Fleet.Core.ApiModels
{
    /// <summary>
    /// Odpowiedź zwrotna do klienta
    /// </summary>
    public class ApiResponse
    {
        #region Public properties

        /// <summary>
        /// Http status kod odpowiedzi
        /// </summary>
        public short StatusCode { get; set; }

        /// <summary>
        /// Wiadomość zwrotna dla wywołaego serwisu
        /// </summary>
        public string Message { get; set; }

        #endregion
        
        #region Constructors

        public ApiResponse( short statusCode, string mesage )
        {
            StatusCode = statusCode;
            Message = mesage;
        }
        
        #endregion
    }

    
    /// <summary>
    /// Odpowiedź zwrotna z dołączonymi danymi konkretnego typu
    /// </summary>
    /// <typeparam name="T">Typ danych do odpowiedzi</typeparam>
    public class ApiResponse<T> : ApiResponse// where T : BaseDto
    {
        /// <summary>
        /// Obiekt odpowiedzi zwrotnej
        /// </summary>
        public object Response { get; set; }

        
        public ApiResponse( short statusCode, string mesage, object response ) : base( statusCode, mesage )
        {
            Response = response;
        }
    }
    
}