using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using PlServer.Server.API.Bindings;
using PlServer.Server.Services.DTOs;

namespace PlServer.Server.API.Binders;

public class CustomBindingProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType == typeof(UserSummaryDTO))
            return new BinderTypeModelBinder(typeof(UserBinder));

        return null;
    }
}
