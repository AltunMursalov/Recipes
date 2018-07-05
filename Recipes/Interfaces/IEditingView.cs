using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Interfaces {
    public interface IEditingView {
        void BindDataContext(IEditingViewModel viewModel);
        void ShowAlert(string text, string caption);
        void Hide();
        bool? ShowDialog();
    }
}
