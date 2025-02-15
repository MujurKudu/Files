﻿// Copyright (c) 2023 Files Community
// Licensed under the MIT License. See the LICENSE.

using Files.Core.Storage.Extensions;
using Files.Core.Storage.LocatableStorage;

namespace Files.Core.ViewModels.Widgets.FileTagsWidget
{
	public sealed partial class FileTagsItemViewModel : ObservableObject
	{
		private readonly ILocatableStorable _associatedStorable;

		// A workaround for lack of MVVM-compliant navigation support.
		// This workaround must be kept until further refactor of navigation code is completed
		private readonly Func<string, Task> _openAction;

		private IImageModel? _Icon;
		public IImageModel? Icon
		{
			get => _Icon;
			set => SetProperty(ref _Icon, value);
		}

		private string _Name;
		public string Name
		{
			get => _Name;
			set => SetProperty(ref _Name, value);
		}

		private string? _Path;
		public string? Path
		{
			get => _Path;
			set => SetProperty(ref _Path, value);
		}

		public FileTagsItemViewModel(ILocatableStorable associatedStorable, Func<string, Task> openAction, IImageModel? icon)
		{
			_associatedStorable = associatedStorable;
			_openAction = openAction;
			_Icon = icon;
			_Name = PathHelpers.FormatName(associatedStorable.Path);
			_Path = associatedStorable.TryGetPath();
		}

		[RelayCommand]
		private Task ClickAsync(CancellationToken cancellationToken)
		{
			return _openAction(_associatedStorable.Path);
		}
	}
}
