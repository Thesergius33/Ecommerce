using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ecommerce.Mobile.Services;
using Ecommerce.Shared.Entities;
using System.Collections.ObjectModel;

namespace Ecommerce.Mobile.ViewModels
{
    public partial class CategoriesViewModel : ObservableValidator
    {
        private readonly CategoryService _categoryService;

        [ObservableProperty]
        private ObservableCollection<Category> _categories;

        [ObservableProperty]
        private Category? _selectedCategory;

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private string _newCategory = string.Empty;

        [ObservableProperty]
        private string _errorMessage = string.Empty;

        public CategoriesViewModel(CategoryService categoryService)
        {
            _categoryService = categoryService;
            _categories = new ObservableCollection<Category>();
        }

        [RelayCommand]
        public async Task LoadCategories()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;
                var categories = await _categoryService.GetCategoriesAsync();

                if (categories != null)
                {
                    Categories.Clear();
                    foreach (var category in categories)
                    {
                        Categories.Add(category);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"No se pudieron cargar las categorías: {ex.Message}";
                await Shell.Current.DisplayAlert("Error", ErrorMessage, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private void OnCategorySelected()
        {
            if (SelectedCategory != null)
            {
                NewCategory = SelectedCategory.Name;
                ErrorMessage = string.Empty;
            }
        }

        [RelayCommand]
        public async Task AddCategory()
        {
            if (IsBusy) return;

            // Validaciones
            if (string.IsNullOrWhiteSpace(NewCategory))
            {
                ErrorMessage = "El campo es obligatorio";
                return;
            }

            if (Categories.Any(c => c.Name.Equals(NewCategory, StringComparison.OrdinalIgnoreCase)))
            {
                ErrorMessage = "La categoría ya existe";
                return;
            }

            if (NewCategory.Length > 100)
            {
                ErrorMessage = "El nombre de la categoría no puede tener más de 100 caracteres";
                return;
            }

            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;

                var newCategory = new Category
                {
                    Name = NewCategory.Trim(),
                    ProductCategoryId = 1 // Esto debería ser seleccionado por el usuario o tener un valor por defecto
                };

                var success = await _categoryService.AddCategoryAsync(newCategory);
                if (success)
                {
                    await LoadCategories();
                    NewCategory = string.Empty;
                    SelectedCategory = null;
                }
                else
                {
                    ErrorMessage = "No se pudo agregar la categoría";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"No se pudo agregar la categoría: {ex.Message}";
                await Shell.Current.DisplayAlert("Error", ErrorMessage, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public void EditCategory(Category category)
        {
            if (category != null)
            {
                SelectedCategory = category;
                NewCategory = category.Name;
                ErrorMessage = string.Empty;
            }
        }

        [RelayCommand]
        public async Task UpdateCategory()
        {
            if (SelectedCategory == null)
            {
                ErrorMessage = "Seleccione una categoría para actualizar";
                return;
            }

            if (string.IsNullOrWhiteSpace(NewCategory))
            {
                ErrorMessage = "El campo es obligatorio";
                return;
            }

            if (Categories.Any(c => c.Name.Equals(NewCategory, StringComparison.OrdinalIgnoreCase) && c.Id != SelectedCategory.Id))
            {
                ErrorMessage = "La categoría ya existe";
                return;
            }

            if (NewCategory.Length > 100)
            {
                ErrorMessage = "El nombre de la categoría no puede tener más de 100 caracteres";
                return;
            }

            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;

                var updatedCategory = new Category
                {
                    Id = SelectedCategory.Id,
                    Name = NewCategory.Trim(),
                    ProductCategoryId = SelectedCategory.ProductCategoryId
                };

                var success = await _categoryService.UpdateCategoryAsync(updatedCategory);
                if (success)
                {
                    await LoadCategories();
                    NewCategory = string.Empty;
                    SelectedCategory = null;
                }
                else
                {
                    ErrorMessage = "No se pudo actualizar la categoría";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"No se pudo actualizar la categoría: {ex.Message}";
                await Shell.Current.DisplayAlert("Error", ErrorMessage, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task DeleteCategory(Category? category = null)
        {
            var categoryToDelete = category ?? SelectedCategory;
            if (categoryToDelete == null) return;

            bool confirm = await Shell.Current.DisplayAlert(
                "Confirmar",
                $"¿Está seguro de que desea eliminar la categoría '{categoryToDelete.Name}'?",
                "Sí",
                "No");

            if (!confirm) return;

            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;

                var success = await _categoryService.DeleteCategoryAsync(categoryToDelete.Id);
                if (success)
                {
                    Categories.Remove(categoryToDelete);
                    if (SelectedCategory?.Id == categoryToDelete.Id)
                    {
                        NewCategory = string.Empty;
                        SelectedCategory = null;
                    }
                }
                else
                {
                    ErrorMessage = "No se pudo eliminar la categoría";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"No se pudo eliminar la categoría: {ex.Message}";
                await Shell.Current.DisplayAlert("Error", ErrorMessage, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}