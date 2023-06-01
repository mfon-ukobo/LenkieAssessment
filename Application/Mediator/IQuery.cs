using MediatR;

namespace Application.Mediator
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
