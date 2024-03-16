using FluentValidation;

namespace RankedReadyApi.Attributes
{
    public class ValidationFilterAttribute<T> : IEndpointFilter where T : class
    {
        private readonly IValidator<T> _validator;

        public ValidationFilterAttribute(IValidator<T> validator)
        {
            _validator = validator;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context,
            EndpointFilterDelegate next)
        {
            var model = context.Arguments.FirstOrDefault(a => a.GetType() == typeof(T)) as T;
            var result = await _validator.ValidateAsync(model!);

            if (!result.IsValid)
                return Results.Json(result.Errors, statusCode: 400);

            return await next(context);
        }
    }
}
