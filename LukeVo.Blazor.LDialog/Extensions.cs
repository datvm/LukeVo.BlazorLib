using LukeVo.Blazor.LDialog;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LDialogExtensions
    {

        static LDialogService instance;
        public static IServiceCollection AddLDialog(this IServiceCollection services)
        {
            return services
                .AddSingleton<ILDialogService>(CreateInstance)
                .AddSingleton<ILDialogInternalService>(CreateInstance);
        }

        static LDialogService CreateInstance(IServiceProvider services)
        {
            if (instance == null)
            {
                instance = new LDialogService();
            }

            return instance;
        }

    }
}
