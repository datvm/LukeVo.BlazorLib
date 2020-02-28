using LukeVo.Blazor.LDialog.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LukeVo.Blazor.LDialog
{

    internal class LDialogService : ILDialogInternalService
    {

        LDialogContainer container;
        TaskCompletionSource<bool> confirmTcs;
        TaskCompletionSource<object> resultTcs;

        public Task<LDialogResult<bool?>> ShowAsync<TDialogComponent>()
            where TDialogComponent : LDialogBase, new()
        {
            return this.ShowAsync<TDialogComponent, bool?>();
        }

        public Task<LDialogResult<bool?>> ShowAsync<TDialogComponent>(TDialogComponent component)
            where TDialogComponent : LDialogBase
        {
            return this.ShowAsync<TDialogComponent, bool?>(component);
        }

        public Task<LDialogResult<TResult>> ShowAsync<TDialogComponent, TResult>()
            where TDialogComponent : LDialogBase, new()
        {
            var component = new TDialogComponent();
            return this.ShowAsync<TDialogComponent, TResult>(component);
        }

        public async Task<LDialogResult<TResult>> ShowAsync<TDialogComponent, TResult>(TDialogComponent component)
            where TDialogComponent : LDialogBase
        {
            var properties = component.GetType().GetProperties();

            RenderFragment fragment = builder =>
            {
                builder.OpenComponent(0, component.GetType());

                foreach (var property in properties)
                {
                    if (property.GetCustomAttribute<ParameterAttribute>() == null)
                    {
                        continue;
                    }

                    var value = property.GetValue(component);

                    builder.AddAttribute(0, property.Name, value);
                }

                builder.CloseComponent();
            };

            this.container.SetFragments(fragment);
            this.container.IsShowing = true;

            this.confirmTcs = new TaskCompletionSource<bool>();
            this.resultTcs = new TaskCompletionSource<object>();

            var confirm = await confirmTcs.Task;
            var result = await this.resultTcs.Task;

            return new LDialogResult<TResult>()
            {
                Confirm = confirm,
                Result = (TResult)(object)result,
            };
        }

        void ILDialogInternalService.SetConfirm(bool confirm)
        {
            this.confirmTcs.TrySetResult(confirm);
        }

        void ILDialogInternalService.SetResult<TResult>(TResult result)
        {
            this.resultTcs.TrySetResult(result);
        }

        void ILDialogInternalService.CloseDialog()
        {
            this.container.Close();
        }

        void ILDialogInternalService.SetContainerComponent(LDialogContainer container)
        {
            this.container = container;
        }

    }

}
