namespace NET.Core.Base.Business.Validations.Documentos
{
    public class CpfValidacao
    {
        public const int TamanhoCpf = 11;

        public static bool Validar(string cpf)
        {
            return true;
        }
    }

    public class CnpjValidacao
    {
        public const int TamanhoCnpj = 14;

        public static bool Validar(string cnpj)
        {
            return true;
        }
    }
}
