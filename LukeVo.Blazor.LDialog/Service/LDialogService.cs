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
        
        public Task<LDialogResult<TResult>> ShowAsync<TResult, TDialog>()
            where TDialog : LDialogBase<TResult>, new()
        {
            var component = new TDialog();
            return this.ShowAsync(component);
        }

        public async Task<LDialogResult<TResult>> ShowAsync<TResult>(LDialogBase<TResult> component)
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

                builder.AddAttribute(0, "IsShowing", true);

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
                Value = (TResult)(object)result,
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
