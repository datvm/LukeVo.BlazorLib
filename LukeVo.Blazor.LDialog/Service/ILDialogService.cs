
using LukeVo.Blazor.LDialog.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace LukeVo.Blazor.LDialog
{
    public interface ILDialogService
    {
        Task<LDialogResult<bool?>> ShowAsync<TDialogComponent>()
            where TDialogComponent : LDialogBase, new();
        Task<LDialogResult<bool?>> ShowAsync<TDialogComponent>(TDialogComponent component)
            where TDialogComponent : LDialogBase;

        Task<LDialogResult<TResult>> ShowAsync<TDialogComponent, TResult>()
            where TDialogComponent : LDialogBase, new();
        Task<LDialogResult<TResult>> ShowAsync<TDialogComponent, TResult>(TDialogComponent component)
            where TDialogComponent : LDialogBase;
    }

    internal interface ILDialogInternalService : ILDialogService
    {
        void SetContainerComponent(LDialogContainer component);
        void CloseDialog();
        void SetConfirm(bool confirm);
        void SetResult<TResult>(TResult result);
    }

}