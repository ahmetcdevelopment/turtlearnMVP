namespace turtlearnMVP.WEB.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public int? ErrorCode { get; set; } // Hata kodunu burada tutuyoruz.
        public string? ErrorMessage { get; set; } // Hata kodunu burada tutuyoruz.
        public string? ErrorDescription { get; set; } // Hata kodunu burada tutuyoruz.

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public bool ShowErrorCode => ErrorCode.HasValue; // Hata kodu varsa göster.
        public bool ShowErrorDescription => ErrorCode.HasValue; // Hata kodu varsa göster.
        public bool ShowErrorMessage => ErrorCode.HasValue; // Hata kodu varsa göster.
    }
}