using FluentValidation;
using FornecedoresApi.Models;

namespace FornecedoresApi.Validators
{
    public class ValidarFornecedor : AbstractValidator<Fornecedor>
    {
        public ValidarFornecedor()
        {
            RuleFor(f => f.Nome).NotEmpty();
            RuleFor(f => f.Cnpj).NotEmpty()
                .Matches(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$")
                .WithMessage("O CNPJ deve estar no formato 99.999.999/9999-99.");
            RuleFor(f => f.Endereco).NotEmpty();
            RuleFor(f => f.Telefone).NotEmpty();
        }       
    }
}