
using LukeVo.Blazor.LDialog.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace LukeVo.Blazor.LDialog
{
    public interface ILDialogService
    {
        Task<LDialogResult<TResult>> ShowAsync<TResult, TDialog>() where TDialog : LDialogBase<TResult>, new();
        Task<LDialogResult<TResult>> ShowAsync<TResult>(LDialogBase<TResult> component);
    }

    internal interface ILDialogInternalService : ILDialogService
    {
        void SetContainerComponent(LDialogContainer component);
        void CloseDialog();
        void SetConfirm(bool confirm);
        void SetResult<TResult>(TResult result);
    }

}