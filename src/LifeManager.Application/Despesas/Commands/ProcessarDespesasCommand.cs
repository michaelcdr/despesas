using LifeManager.Application.Despesas.Responses;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace LifeManager.Application.Despesas.Commands
{
    public class ProcessarDespesasCommand : IRequest<ProcessarDespesasResponse>
    {
        
        public ProcessarDespesasCommand()
        {
        }
    }
}
