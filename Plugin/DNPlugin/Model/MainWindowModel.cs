using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            if (PosPath == null || NegPath == null)
                throw new Exception("Выбирете директории образцов");

            if (PosPath.Equals(NegPath, StringComparison.InvariantCultureIgnoreCase))
                throw new Exception("Папка с негативными и позитивными образцами не может совпадать.");

            if (Directory.GetFiles(PosPath, "*.png|*.jpg|*.bmp|*.jpeg").Length == 0)
                throw new Exception("Отсутсвуют позитивные корректные образцы, png,jpg,bmp,jpeg ");

            if (Directory.GetFiles(NegPath, "*.png|*.jpg|*.bmp|*.jpeg").Length == 0)
                throw new Exception("Отсутсвуют негативные корректные образцы, png,jpg,bmp,jpeg ");

            Int32 width, height;
            using (var bmmp = new Bitmap(Directory.GetFiles(PosPath, "*.png|*.jpg|*.bmp|*.jpeg").First()))
            {
                width = bmmp.Width;
                height = bmmp.Height;
            }

            IPerceptron perceptron = new Perceptron(new[] { "pos", "neg" }, width, height);
            ITeacher teacher = new Teacher(perceptron);

            var sempls = new List<ImageData>();
            sempls.AddRange(GetSemp(PosPath, "pos", perceptron.X, perceptron.Y));
            sempls.AddRange(GetSemp(NegPath, "neg", perceptron.X, perceptron.Y));

            teacher.Teach(sempls, 85);

            using (var open = new OpenFileDialog { Filter = "*.aidb", FilterIndex = 0 })
                if (open.ShowDialog() == DialogResult.OK || open.ShowDialog() == DialogResult.Yes)
                {
                    perceptron.Save(open.FileName);
                }

        }

        private List<ImageData> GetSemp(String path, String @class, Int32 x, Int32 y)
        {
            var sempls = new List<ImageData>();
            Parallel.ForEach(Directory.GetFiles(path, "*.png|*.jpg|*.bmp|*.jpeg"), (file) =>
            {
                using (var bm = new Bitmap(file))
                {
                    var data = ImgHelpers.ImgPreProcess(bm, x, y);
                    lock (sempls)
                    {
                        sempls.Add(new ImageData
                        {
                            Class = @class,
                            Data = data,
                        });
                    }
                }
            });
            return sempls;
        }

        private void SelectNegPathMethod()
        {
            using (var open = new FolderBrowserDialog { Description = @"Выбор папки с негативными образцами" })
                if (open.ShowDialog() == DialogResult.OK)
                {
                    NegPath = open.SelectedPath;
                }
        }

        private void SelectPosPathMethod()
        {
            using (var open = new FolderBrowserDialog { Description = @"Выбор папки с позитивными образцами" })
                if (open.ShowDialog() == DialogResult.OK)
                {
                    PosPath = open.SelectedPath;
                }
        }

    }
}
