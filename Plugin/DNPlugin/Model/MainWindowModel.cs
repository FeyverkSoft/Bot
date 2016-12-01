using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using ImgComparer.Helpers;
using ImgComparer.Neuro.Domain;
using ImgComparer.Neuro.Interface;
using ImgComparer.Neuro.InterfaceImpl;
using WpfConverters.Extensions.Commands;

namespace ImgComparer.Model
{
    public class MainWindowModel : BaseViewModel
    {
        private String _posPath;
        private String _negPath;

        public String PosPath
        {
            get { return _posPath; }
            set
            {
                _posPath = value;
                OnPropertyChanged();
            }
        }

        public String NegPath
        {
            get { return _negPath; }
            set
            {
                _negPath = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Выбор папки с позитивными образцами
        /// </summary>
        private ICommand _selectPosPathCommand;
        /// <summary>
        /// Выбор папки с негативными образцами
        /// </summary>
        private ICommand _selectNegPathCommand;
        /// <summary>
        /// Запустить обучение
        /// </summary>
        private ICommand _runCommand;

        /// <summary>
        /// Команда выбор папки с позитивными образцами
        /// </summary>
        public ICommand SelectPosPathCommand => _selectPosPathCommand ?? (_selectPosPathCommand = new DelegateCommand(SelectPosPathMethod));
        /// <summary>
        /// Команда выбор папки с негативными образцами
        /// </summary>
        public ICommand SelectNegPathCommand => _selectNegPathCommand ?? (_selectNegPathCommand = new DelegateCommand(SelectNegPathMethod));
        /// <summary>
        /// Команда выбор папки с негативными образцами
        /// </summary>
        public ICommand RunCommand => _runCommand ?? (_runCommand = new DelegateCommand(RunCommandMethod));

        private void RunCommandMethod()
        {
            IPerceptron perceptron = new Perceptron(new[] { "pos", "neg" }, 0, 0);
            ITeacher teacher = new Teacher(perceptron);
            var sempls = new List<ImageData>();
            foreach (var VARIABLE in COLLECTION)
            {
                
            }
            sempls.Add(new ImageData
            {
                Class = "pos",
                Data = ImgHelpers.ImgPreProcess(img, perceptron.X, perceptron.Y),
            });
            teacher.Teach(sempls, 85);
            var open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK || open.ShowDialog() == DialogResult.Yes)
            {
                perceptron.Save(open.FileName);
            }

        }

        private void SelectNegPathMethod()
        {
            var open = new FolderBrowserDialog { Description = "Выбор папки с негативными образцами" };
            if (open.ShowDialog() == DialogResult.OK)
            {
                NegPath = open.SelectedPath;
            }
        }

        private void SelectPosPathMethod()
        {
            var open = new FolderBrowserDialog { Description = "Выбор папки с позитивными образцами" };
            if (open.ShowDialog() == DialogResult.OK)
            {
                PosPath = open.SelectedPath;
            }
        }

    }
}
