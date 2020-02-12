﻿using RPedretti.RazorComponents.Input.Radio;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sample.Shared.Pages.Inputs
{
    public partial class InputsPage
    {
        #region Fields

        private readonly List<string> someList = new List<string>() {
            "olar 1", "olar 2", "banana", "apple", "bacalhau", "blabous", "bla", "abacate","abacate"
        };

        private bool _loadingSuggestions;

        private string? _query;

        private RadioButton? _selectedRadioButton1;
        private RadioButton? _selectedRadioButton2;
        private RadioButton? _selectedRadioButton3;
        private string? _selectedSuggestion;
        private bool _someChecked;

        private bool _someChecked2;

        private bool _someToggled;

        private bool _someToggled2;

        #endregion Fields

        #region Properties

        protected List<string>? FilteredList { get; set; }

        protected bool HasSelection =>
            SelectedRadioButton1 != null ||
            SelectedRadioButton2 != null ||
            SelectedRadioButton3 != null;

        protected bool LoadingSuggestions
        {
            get => _loadingSuggestions;
            set => SetParameter(ref _loadingSuggestions, value);
        }

        protected bool SomeChecked
        {
            get => _someChecked;
            set => SetParameter(ref _someChecked, value, StateHasChanged);
        }

        protected bool SomeChecked2
        {
            get => _someChecked2;
            set => SetParameter(ref _someChecked2, value, StateHasChanged);
        }

        protected bool SomeToggled
        {
            get => _someToggled;
            set => SetParameter(ref _someToggled, value, StateHasChanged);
        }

        protected bool SomeToggled2
        {
            get => _someToggled2;
            set => SetParameter(ref _someToggled2, value, StateHasChanged);
        }

        public string? Query
        {
            get => _query;
            set => SetParameter(ref _query, value);
        }

        public RadioButton[] RadioButtons { get; set; } = new RadioButton[]
        {
            new RadioButton { Label = "Button 1", Value = 4 },
            new RadioButton { Label = "Button 2", Value = "olar"},
            new RadioButton { Label = "Button 3", Value = null, Disabled = true },
            new RadioButton { Label = "Button 4", Value = false }
        };

        public RadioButton? SelectedRadioButton1
        {
            get => _selectedRadioButton1;
            set => SetParameter(ref _selectedRadioButton1, value);
        }

        public RadioButton? SelectedRadioButton2
        {
            get => _selectedRadioButton2;
            set => SetParameter(ref _selectedRadioButton2, value);
        }

        public RadioButton? SelectedRadioButton3
        {
            get => _selectedRadioButton3;
            set => SetParameter(ref _selectedRadioButton3, value);
        }

        public string? SelectedSuggestion
        {
            get => _selectedSuggestion;
            set => SetParameter(ref _selectedSuggestion, value);
        }

        #endregion Properties

        #region Methods

        protected async Task FetchSuggestions(string query)
        {
            Query = query;
            if (!string.IsNullOrWhiteSpace(query))
            {
                LoadingSuggestions = true;
                await Task.Delay(1000);

                FilteredList = someList.Where(s => s.ToLower().StartsWith(query.ToLower())).ToList();
                LoadingSuggestions = false;
            }
            else
            {
                FilteredList = null;
            }
        }

        protected void ResetSelectedRadios()
        {
            SelectedRadioButton1 = null;
            SelectedRadioButton2 = null;
            SelectedRadioButton3 = null;
        }

        protected void SuggestionSelected(string? suggestion)
        {
            FilteredList = null;
            Query = suggestion;
            SelectedSuggestion = suggestion;
        }

        #endregion Methods
    }
}
