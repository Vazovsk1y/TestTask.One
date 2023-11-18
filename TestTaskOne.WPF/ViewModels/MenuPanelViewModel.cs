using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Windows;
using TestTaskOne.DAL;
using TestTaskOne.WPF.Infrastructure;
using TestTaskOne.WPF.Windows;

namespace TestTaskOne.WPF.ViewModels;

internal partial class MenuPanelViewModel : ObservableObject
{
	private readonly IUserDialog<ChangeDatabaseWindow> _userDialog;
	private readonly IServiceScopeFactory _serviceScopeFactory;

	public MenuPanelViewModel(IUserDialog<ChangeDatabaseWindow> userDialog, IServiceScopeFactory serviceScopeFactory)
	{
		_userDialog = userDialog;
		_serviceScopeFactory = serviceScopeFactory;
	}

	[RelayCommand]
	private void ChangeDatabase()
	{
		_userDialog.ShowDialog();
	}

	[RelayCommand(CanExecute = nameof(CanSaveInDocx))]
	private async Task SaveInDocx()
	{
		var storagePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
		using var scope = _serviceScopeFactory.CreateScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<TestTaskContext>();
		await dbContext.SaveInDocxAsync(storagePath, new DocxDocumentOptions { DocumentFontFamily = "Times new Roman", DocumentFontSize = 28});
		MessageBox.Show("Successfully saved.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
	}
	private bool CanSaveInDocx() => App.ConnectedToDatabase;
}