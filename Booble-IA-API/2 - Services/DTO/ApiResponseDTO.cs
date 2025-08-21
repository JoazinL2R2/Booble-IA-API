namespace Booble_IA_API._2___Services.DTO
{
    public class ApiResponseDTO<T>
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public T? Dados { get; set; }
        public List<string>? Erros { get; set; }

        public ApiResponseDTO()
        {
            Erros = new List<string>();
        }

        public static ApiResponseDTO<T> CriarSucesso(T dados, string mensagem = "Operação realizada com sucesso")
        {
            return new ApiResponseDTO<T>
            {
                Sucesso = true,
                Mensagem = mensagem,
                Dados = dados
            };
        }

        public static ApiResponseDTO<T> CriarErro(string mensagem, List<string>? erros = null)
        {
            return new ApiResponseDTO<T>
            {
                Sucesso = false,
                Mensagem = mensagem,
                Erros = erros ?? new List<string>()
            };
        }
    }
}